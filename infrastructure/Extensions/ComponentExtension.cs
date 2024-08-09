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

    public static class ComponentExtension
    {

        public static void AddComponents(this IServiceCollection services)
        {
            // 获取使用该特性类型并且不是抽象类
            List<Type> types = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttributes(typeof(ComponentAttribute), false).Length > 0)
                .ToList();

            types.ForEach(t =>
            {
                var lifeTime = t.GetCustomAttribute<ComponentAttribute>().LifeTime;
                switch (lifeTime)
                {
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(t);
                        break;
                    case ServiceLifetime.Scoped:
                        services.AddScoped(t);
                        break;
                    case ServiceLifetime.Transient:
                        services.AddTransient(t);
                        break;
                }
            });

        }


    }
}
