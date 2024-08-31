using domain.Pojo.quartz;
using infrastructure.Db;
using infrastructure.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using quartzModeule.Bll;

namespace quartzModeule.Job;

/// <summary>
/// 测试定时任务
/// </summary>
public class TestJob : IJob
{
    private readonly DbClientFactory _dbClientFactory = ServiceUtil.ServiceProvider.GetService<DbClientFactory>();
    private readonly IJobLogBll _jobLogBll = ServiceUtil.ServiceProvider.GetService<IJobLogBll>();
    private readonly ILogger<TestJob> logger = ServiceUtil.ServiceProvider.GetService<ILogger<TestJob>>();
    
    public Task Execute(IJobExecutionContext context)
    {
        JobLog jobLog = new JobLog();
        jobLog.jobName = "测试定时任务";
        jobLog.category = "系统";
        jobLog.typeName = "quartzModeule.Job.TestJob";
        logger.LogInformation("测试定时任务------" + DateTime.Now.ToString("HH:mm:ss zz"));
        Thread.Sleep(1000);
        jobLog.status = true;
        jobLog.stopTime = DateTime.Now;
        
        _jobLogBll.Save(jobLog);
        
        return Task.CompletedTask;
    }
}