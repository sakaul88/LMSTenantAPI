using System;
using System.Net;
using System.Threading.Tasks;
using DeviceManager.Api.Common;
using DeviceManager.Api.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DeviceManager.Api.Middlewares
{
    /// <summary>
    /// Central error/exception handler Middleware
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate request;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlerMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            request = next;
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public Task Invoke(HttpContext context) => InvokeAsync(context); 

        async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await request(context);
            }
            catch (DomainException exception)
            {
                context.Response.Clear();
                context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { (string)context.Request.Headers["Origin"] });
                context.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Origin, X-Requested-With, Content-Type, Accept" });
                context.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "GET, POST, PUT, DELETE, OPTIONS" });
                context.Response.Headers.Add("Access-Control-Allow-Credentials", new[] { "true" });
                context.Response.StatusCode = (int)exception.StatusCode;
                context.Response.ContentType = "application/json";

                var response = JsonConvert.SerializeObject(new DomainErrors() { Error_code = exception.ErrorCode, Message = exception.Message });
                
                //Write error to log here

                await context.Response.WriteAsync(response).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                var httpStatusCode = ConfigurateExceptionTypes(exception);
                context.Response.StatusCode = httpStatusCode;
                //TODO: [Temp fix] Rebuild this later to proper JSON error object serialization 
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{error: "+ exception.Message + "}");
                context.Response.Headers.Clear();
            }
        }

        private static int ConfigurateExceptionTypes(Exception exception)
        {
            int httpStatusCode;

            // Exception type To Http Status configuration 
            switch (exception)
            {
                case var _ when exception is ValidationException:
                    httpStatusCode = (int) HttpStatusCode.BadRequest;
                   break;
                default:
                    httpStatusCode = (int) HttpStatusCode.InternalServerError;
                  break;
            }

            return httpStatusCode;
        }
    }
}
