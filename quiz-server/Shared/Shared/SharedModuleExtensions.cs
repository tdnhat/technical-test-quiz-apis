using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Shared
{
    public static class SharedModuleExtensions
    {
        public static WebApplicationBuilder AddCoreServices(this WebApplicationBuilder builder)
        {
            // Add HTTP context accessor for dependency injection
            builder.Services.AddHttpContextAccessor();

            // Add session services
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Add services to the container.
            builder.Services.AddEndpointsApiExplorer();

            // Configure Swagger with authentication
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Quiz API",
                    Version = "v1",
                    Description = "API for Quiz application"
                });
            });

            return builder;
        }

        public static WebApplication UseCoreMiddleware(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseSession();

            return app;
        }
    }
}
