using System;
using System.Reflection;
using Amethyst.Demo.Cards.Host.Composition;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Amethyst.Demo.Cards.Host
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;
        
        public Startup(
            IConfiguration configuration, 
            ILoggerFactory loggerFactory)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServices();
            services.AddRepositories();
            services.AddConfiguredSwagger();
            services.ConfigureEventStore(_configuration, _loggerFactory);
            
            services.AddMvc(opt => opt.EnableEndpointRouting = false)
                .AddFluentValidation(opt =>
                    opt.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseConfiguredSwagger();
            app.UseMvc();
        }
    }
}