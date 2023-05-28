using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OnlineShop.Api.Init
{
    public static class MediateR
    {
        public static void AddCustomizedMediatR(this IServiceCollection services)
        {
            var assemblyNames = new string[] { "OnlineShop.Application" };
            var assemblies = assemblyNames.Select(o => Assembly.Load(o)).FirstOrDefault();
            //services.AddMediatR(assemblies);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assemblies));
        }
    }
}
