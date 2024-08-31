using Microsoft.Extensions.DependencyInjection;
using Quartz;
using quartzModeule.Controller;
using quartzModeule.Job;

namespace quartzModeule;

public static class QuartzModule
{

    public static void AddQuartzModule(this IServiceCollection services)
    {
        services.AddQuartz(options =>
        {
            options.SchedulerId = "Scheduler-core"; 
            /*options.SchedulerId = string.Empty;
            options.SchedulerName = string.Empty;*/
            options.InterruptJobsOnShutdown = true;
            options.InterruptJobsOnShutdownWithWait = true;
            options.BatchTriggerAcquisitionFireAheadTimeWindow = TimeSpan.Zero;
        });
       
        services.AddQuartzHostedService(q =>
        {
            q.WaitForJobsToComplete = true;
        });
        
        services.AddControllers().AddApplicationPart(typeof(QuartzController).Assembly);

    }
    
   
    

}