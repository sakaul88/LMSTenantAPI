using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using DeviceManager.Api.Helpers;
using DeviceManager.Api.Configuration.Settings;
using DeviceManager.Api.Configuration.DatabaseTypes;
using DeviceManager.Api.Common;

namespace DeviceManager.Api.Data.Management
{
    /// <summary>
    /// Entity Framework context service
    /// (Switches the db context according to tenant id field)
    /// </summary>
    /// <seealso cref="IContextFactory"/>
    public class ContextFactory : IContextFactory
    {
        private const string TenantIdFieldName = Constants.TenantId;
        private const string DatabaseFieldKeyword = Constants.Database;

        private readonly HttpContext httpContext;

        private readonly IOptions<ConnectionSettings> connectionOptions;

        private readonly IDataBaseManager dataBaseManager;

        private readonly IDatabaseType databaseType;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextFactory"/> class.
        /// </summary>
        /// <param name="httpContentAccessor">The HTTP content accessor.</param>
        /// <param name="connectionOptions">The connection options.</param>
        /// <param name="dataBaseManager">The data base manager.</param>
        /// <param name="databaseType"></param>
        public ContextFactory(
            IHttpContextAccessor httpContentAccessor,
            IOptions<ConnectionSettings> connectionOptions,
            IDataBaseManager dataBaseManager,
            IDatabaseType databaseType
            )
        {
            httpContext = httpContentAccessor.HttpContext;
            this.connectionOptions = connectionOptions;
            this.dataBaseManager = dataBaseManager;
            this.databaseType = databaseType;
        }

        /// <inheritdoc />
        public IDbContext DbContext => new DeviceContext(ChangeDatabaseNameInConnectionString(TenantId).Options);

        /// <summary>
        /// Gets tenant id from HTTP header
        /// </summary>
        /// <value>
        /// The tenant identifier.
        /// </value>
        /// <exception cref="ArgumentNullException">
        /// httpContext
        /// or
        /// tenantId
        /// </exception>
        private int TenantId
        {
            get
            {
                ValidateHttpContext();
                int tenantId = int.Parse(httpContext.Request.Headers["tenantId"]);
                //int.Parse(httpContext.Request.Headers[TenantIdFieldName]);
                return tenantId;
            }
        }

        private DbContextOptionsBuilder<DeviceContext> ChangeDatabaseNameInConnectionString(int tenantId)
        {
            ValidateDefaultConnection();

            // 1. Create Connection String Builder using Default connection string
            var connectionBuilder = databaseType.GetConnectionBuilder(connectionOptions.Value.DefaultConnection);

            // 2. Remove old Database Name from connection string
            connectionBuilder.Remove(DatabaseFieldKeyword);

            // 3. Obtain Database name from DataBaseManager and Add new DB name to 
            connectionBuilder.Add(DatabaseFieldKeyword, dataBaseManager.GetDataBaseName(connectionOptions.Value.DefaultConnection, tenantId));

            // 4. Create DbContextOptionsBuilder with new Database name
            var contextOptionsBuilder = new DbContextOptionsBuilder<DeviceContext>();

            databaseType.SetConnectionString(contextOptionsBuilder, connectionBuilder.ConnectionString);

            return contextOptionsBuilder;
        }

        private void ValidateDefaultConnection()
        {
            if (string.IsNullOrEmpty(connectionOptions.Value.DefaultConnection))
            {
                throw new ArgumentNullException(nameof(connectionOptions.Value.DefaultConnection));
            }
        }

        private void ValidateHttpContext()
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }
        }
    }
}
