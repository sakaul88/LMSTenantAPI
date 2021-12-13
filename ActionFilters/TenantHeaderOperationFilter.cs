using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DeviceManager.Api.ActionFilters
{
    /// <summary>
    /// Adds Tenant Id field to API endpoints
    /// </summary>
    /// <seealso cref="IOperationFilter" />
    public class TenantHeaderOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Applies the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="context">The context.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            //if (operation.Parameters == null)
            //{
            //    operation.Parameters = new List<IParameter>();
            //}

            //operation.Parameters.Add(new NonBodyParameter
            //{
            //    Name = "tenantid",
            //    In = "header",
            //    Description = "tenantid",
            //    Required = true,
            //    Type = "string",
            //});

        }
    }
}
