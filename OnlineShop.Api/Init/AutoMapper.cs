using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace OnlineShop.Api.Init
{
    public static partial class Init
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            var assemblyNames = new string[] { "OnlineShop.Api",};
            var assemblies = assemblyNames.Select(o => Assembly.Load(o)).ToArray();

            var profiles = assemblies
                .SelectMany(o => o.GetExportedTypes())
                .Where(t => typeof(Profile).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()))
                .Where(t => !t.GetTypeInfo().IsAbstract);

            var mapperConfig = new MapperConfiguration(mc =>
            {
                foreach (var profile in profiles)
                {
                    mc.AddProfile(profile);
                }
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}
