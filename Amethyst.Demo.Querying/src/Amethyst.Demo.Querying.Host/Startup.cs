using Amethyst.Demo.Querying.Host.Composition;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Amethyst.Demo.Querying.Host
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString  = _configuration.GetConnectionString("Postgres");
            services.AddPricingReadModel(connectionString);
            services.AddConfiguredSwagger();
            services.AddStores();
            services.AddSubscription(_configuration);
            
            services
                .AddMvc(opt => opt.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Latest);
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseConfiguredSwagger();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}