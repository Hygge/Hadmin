using infrastructure.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Extensions
{

    public static class ServiceCollectionExptension
    {

        public static void AddServices(this IServiceCollection services)
        {
            List<Type> types = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttributes(typeof(ServiceAttribute), false).Length > 0)
                .ToList();

            types.ForEach(impl =>
            {
                Type[] interfaces = impl.GetInterfaces();

                var lifeTime = impl.GetCustomAttribute<ServiceAttribute>().LifeTime;

                interfaces.ToList().ForEach(i =>
                {
                    switch (lifeTime)
                    {
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(i, impl);
                            break;
                        case ServiceLifetime.Scoped:
                            services.AddScoped(i, impl);
                            break;
                        case ServiceLifetime.Transient:
                            services.AddTransient(i, impl);
                            break;
                    }
                });
            });
        }
    }
}
