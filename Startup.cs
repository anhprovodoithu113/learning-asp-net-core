using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SampleAPI.CustomRouting;
using SampleAPI.Demo;
using SampleAPI.Filters;
using SampleAPI.Repositories;
using SampleAPI.Services;

namespace SampleAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;

            // How to register the services by condition
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Config global filter
            //services.AddControllers(config => config.Filters.Add(new CustomFilter()));

            //config Logging
            services.AddSingleton<ILogger>(provider => provider.GetRequiredService<ILogger<CustomActionFilter>>());

            services.AddControllers();
            // How to register the services by condition
            if (Environment.IsDevelopment())
            {
                services.AddTransient<IPaymentService, PaymentService>();
            }
            else
            {
                services.AddTransient<IPaymentService, ExternalPaymentService>();
            }

            // Register the custom service by route.
            services.Configure<RouteOptions>(ops => ops.ConstraintMap.Add("currency", typeof(CurrencyConstraint)));

            //services.AddSingleton<IOrderRepository, MemoryOrderRepository>()
            //    .AddControllers()
            //    .AddNewtonsoftJson();

            services.AddSingleton<IOrderRepository, MemoryOrderRepository>().AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SampleAPI", Version = "v1" });
            });

            // register the service
            //services.AddTransient<IPaymentService, PaymentService>().AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SampleAPI v1"));
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseSampleMiddleware();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Good, Job");
            //});
        }
    }
}
