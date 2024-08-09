using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Attributes
{
    /// <summary>
    /// 属性形式定义生命周期（有接口，一般业务类）
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceAttribute : Attribute
    {
        public ServiceLifetime LifeTime { get; set; }
        public ServiceAttribute(ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            LifeTime = serviceLifetime;
        }
    }
}
