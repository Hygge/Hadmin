using domain.Pojo.sys;
using infrastructure.Attributes;
using infrastructure.Db;
using Microsoft.Extensions.DependencyInjection;

namespace adminModule.Bll.Impl;


[Service(ServiceLifetime.Singleton)]
public class SysLogBll : ISysLogBll
{
    private readonly DbClientFactory _dbClientFactory;

    public SysLogBll(DbClientFactory dbClientFactory)
    {
        this._dbClientFactory = dbClientFactory;
    }
    
    
    public void save(SysLog log)
    {
        using var db = _dbClientFactory.GetSqlSugarClient();
        db.Insertable(log).ExecuteCommand();
    }
}