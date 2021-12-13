using System.IO;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using DeviceManager.Api.ActionFilters;
using DeviceManager.Api.Controllers;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;

namespace DeviceManager.Api.Configuration
{
    /// <summary>
    /// Swagger API documentation components start-up configuration
    /// </summary>
    public static class SwaggerConfiguration
    {
        /// <summary>
        /// Configures the service.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureService(IServiceCollection services)
        {
            // Swagger API documentation
            services.AddSwaggerGen(c =>
            {
                // TODO: Need to push hardcoded strings to resource file
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API for TCLC System | Identity", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                         new OpenApiSecurityScheme
                         {
                           Reference = new OpenApiReference
                           {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                           }
                         },
                            new string[] { }
                     }
                });

                //c.AddSecurityDefinition("uid", new ApiKeyScheme()
                //{
                //    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                //    Name = "Authorization",
                //    In = "header",
                //    Type = "apiKey"
                //});
                //c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                //{
                //    { "uid  ", new string[] { } }
                //});
                c.OperationFilter<TenantHeaderOperationFilter>();
                var xmlFile = $"{typeof(BaseController<>).Assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void Configure(IApplicationBuilder app)
        {

            // This will redirect default url to Swagger url
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
              //  c.SwaggerGeneratorOptions.IgnoreObsoleteActions = true;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TCLC Api Generic | Identity v1.0");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TCLC Api v1.1");
            });
        }
    }
}