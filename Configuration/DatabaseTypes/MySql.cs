using System.Data.Common;
using System.Data.SqlClient;
using DeviceManager.Api.Configuration.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DeviceManager.Api.Configuration.DatabaseTypes
{
    public class MySql : IDatabaseType
    {
        public IServiceCollection EnableDatabase(IServiceCollection services, IOptions<ConnectionSettings> connectionOptions)
        {
            return services;
        }

        /// <inherit/>
        public DbConnectionStringBuilder GetConnectionBuilder(string connectionString)
        {
            return new SqlConnectionStringBuilder(connectionString);
        }

        /// <inherit/>
        public DbContextOptionsBuilder GetContextBuilder(DbContextOptionsBuilder optionsBuilder, IOptions<ConnectionSettings> connectionOptions, string connectionString)
        {
            return optionsBuilder.UseMySql(connectionString, b => EntityFrameworkConfiguration.GetMigrationInformation(b));
        }

        /// <inherit/>
        public DbContextOptionsBuilder<TContext> SetConnectionString<TContext>(DbContextOptionsBuilder<TContext> contextOptionsBuilder, string connectionString) where TContext : DbContext
        {
            return contextOptionsBuilder.UseMySql(connectionString);
        }
    }
}
