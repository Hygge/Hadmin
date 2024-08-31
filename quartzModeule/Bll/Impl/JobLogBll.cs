using domain.Pojo.quartz;
using infrastructure.Attributes;
using infrastructure.Db;
using Microsoft.Extensions.DependencyInjection;
using Yitter.IdGenerator;

namespace quartzModeule.Bll.Impl;

[Service(ServiceLifetime.Singleton)]
public class JobLogBll : IJobLogBll
{
    private readonly DbClientFactory _dbClientFactory;

    public JobLogBll(DbClientFactory dbClientFactory)
    {
        this._dbClientFactory = dbClientFactory;
    }
    
    
    public void Save(JobLog jobLog)
    {
        using var db = _dbClientFactory.GetSqlSugarClient();
        jobLog.id = YitIdHelper.NextId();
        db.Insertable<JobLog>(jobLog).ExecuteCommand();
    }
}