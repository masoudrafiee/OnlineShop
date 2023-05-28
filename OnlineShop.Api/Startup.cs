using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OnlineShop.Api.ErrorHandling;
using OnlineShop.Api.Init;
using OnlineShop.Application;
using OnlineShop.Application.Commands;
using OnlineShop.Application.Services;
using OnlineShop.Domain.AggregatesModel.BasketAggregate;
using OnlineShop.Domain.AggregatesModel.BuyerAggregate;
using OnlineShop.Domain.AggregatesModel.OrderAggregate;
using OnlineShop.Domain.AggregatesModel.ProductAggregate;
using OnlineShop.Infrastructure.Repositories;

namespace OnlineShop.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCustomDbContext(Configuration);
            services.AddCustomizedMediatR();
            services.AddSwaggerGen();
            services.AddAutoMapper();
            services.AddScoped<IBasketRepository, BasketRepository>(); 
            services.AddScoped<IOrderRepository, OrderRepository>(); 
            services.AddScoped<IProductRepository, ProductRepository>(); 
            services.AddScoped<IBuyerRepository, BuyerRepository>(); 
            services.AddScoped<IOrderTimeService, OrderTimeService>();
            services.AddSingleton<OrderTime>(Configuration.GetSection("OrderTime").Get<OrderTime>());
           // services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ICustomService).Assembly));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
            app.MigrateDbContext();
        }
    }
}
