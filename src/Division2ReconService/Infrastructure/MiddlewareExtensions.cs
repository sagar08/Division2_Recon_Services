using Division2ReconService.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using NLog;
using System.Net;

namespace Division2ReconService.Infrastructure
{
    /// <summary>
    /// Middleware extensions
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Configure Logger Service
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        /// <summary>
        /// Configure Swagger
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Division 2 Recon API Service",
                    Version = "v1"
                });

                swagger.IncludeXmlComments(
                    Path.Combine(System.AppContext.BaseDirectory, "SwaggerDocumentation.xml")
                    );
            });
        }

        /// <summary>
        /// Configure Global Exception
        /// </summary>
        /// <param name="app"></param>
        /// <param name="logger"></param>
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            var logger = LogManager.GetCurrentClassLogger();
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.Error($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ResponseErrorDto()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        }
    }
}
