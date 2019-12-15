using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Amethyst.Demo.Cards.Host.Composition
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddConfiguredSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Amethyst Demo API", Version = "v1" });
            });

            return services;
        }

        public static IApplicationBuilder UseConfiguredSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Amethyst Demo V1");
            });

            return app;
        }
    }
}