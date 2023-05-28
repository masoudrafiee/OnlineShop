using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OnlineShop.Api.Init
{
    public static partial class Init
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlServer()
                   .AddDbContext<OnlineShopContext>(options =>
                   {
                       options.UseSqlServer(configuration["ConnectionString"],
                           sqlServerOptionsAction: sqlOptions =>
                           {
                               sqlOptions.MigrationsAssembly("OnlineShop.Infrastructure");
                               sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                           });
                   },
                       ServiceLifetime.Scoped
                   );

            return services;
        }
        public static void MigrateDbContext(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<OnlineShopContext>();
            dbContext.Database.Migrate();
        }
    }
}
