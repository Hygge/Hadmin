using adminModule.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace adminModule
{
    public static class AdminModule
    {
        public static void AddAdmin(this IServiceCollection services)
        {
            services.AddControllers().AddApplicationPart(typeof(SysRoleController).Assembly);

        }

    }
}
