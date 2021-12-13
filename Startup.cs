using AutoMapper;
using DeviceManager.Api.ActionFilters;
using DeviceManager.Api.Configuration;
using DeviceManager.Api.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DeviceManager.Api
{
    /// <summary>
    /// Configuration class for dotnet core application
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Instance of application configuration
        /// </summary>
        /// <value></value>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">The env.</param>
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigurationOptions.ConfigureService(services, Configuration);

            // Add framework services.
            services.AddMvc(
                options =>
                {
                    options.Filters.Add(typeof(ValidateModelStateAttribute));
                });

            services.AddJwtAuthentication();

            // Remove commented code and above semicolon 
            // if the assembly of the API Controllers is different than project which contains Startup class 
            //.AddApplicationPart(typeof(BaseController<>).Assembly);
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("http://localhost:5000", "https://localhost:5000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            
            //Mapper.Reset();
            // https://github.com/AutoMapper/AutoMapper.Extensions.Microsoft.DependencyInjection/issues/28
            services.AddAutoMapper(typeof(Startup));
            services.AddCors();
            //services.AddCors(x => { x.AddPolicy("AllowMyOrigin", y => y.WithOrigins("http://localhost:4200")); });
            // Swagger API documentation
            SwaggerConfiguration.ConfigureService(services);

            // IOC containers / Entity Framework
            EntityFrameworkConfiguration.ConfigureService(services, Configuration);
            IocContainerConfiguration.ConfigureService(services, Configuration);
            ApiVersioningConfiguration.ConfigureService(services);

            //VERY IMPORTANT TO WORK WITH SELF REFFERENCING ENTITIES
            //services.AddMvc(options => { }).AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddMvc().AddNewtonsoftJson(options => { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });

            services.AddApplicationInsightsTelemetry();

        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseStaticFiles();
            //Cunfigure the Swagger API documentation
            SwaggerConfiguration.Configure(app);
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}