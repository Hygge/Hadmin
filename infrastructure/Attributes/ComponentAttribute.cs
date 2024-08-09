using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Attributes
{
    /// <summary>
    /// 属性形式定义生命周期（无接口类情况，一般工具）
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute : Attribute
    {

        public ServiceLifetime LifeTime { get; set; }
        public ComponentAttribute(ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            LifeTime = serviceLifetime;
        }


    }
}
