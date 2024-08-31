using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace infrastructure.Utils;

public static class ServiceUtil
{

    public static IServiceProvider ServiceProvider;
    
    public static void UseServiceProvider(this IApplicationBuilder app)
    {
        ServiceProvider = app.ApplicationServices;
    }
    

    
}