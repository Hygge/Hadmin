using domain.Pojo.sys;
using infrastructure.Attributes;
using infrastructure.Db;
using infrastructure.Utils;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace adminModule.Bll.Impl;


[Service(ServiceLifetime.Singleton)]
public class SysLogBll : ISysLogBll
{
    private readonly DbClientFactory _dbClientFactory;

    public SysLogBll(DbClientFactory dbClientFactory)
    {
        this._dbClientFactory = dbClientFactory;
    }
    
    
    public void Save(SysLog log)
    {
        using var db = _dbClientFactory.GetSqlSugarClient();
        db.Insertable(log).ExecuteCommand();
    }

    public Pager<SysLog> FindLog(string? path, string? operation, string? operatorName, DateTime? startTime, DateTime? endTime,
        int pageNum, int pageSize)
    {
        Pager<SysLog> pager = new(pageNum, pageSize);
        using var db = _dbClientFactory.GetSqlSugarClient();
        var exp = Expressionable.Create<SysLog>();
        exp.AndIF(!string.IsNullOrEmpty(path), x => x.path.Contains(path));
        exp.AndIF(!string.IsNullOrEmpty(operation), x => x.operation.Contains(operation));
        exp.AndIF(!string.IsNullOrEmpty(operatorName), x => x.operatorName.Contains(operatorName));
        exp.AndIF( null != startTime, x => x.operateTime >= startTime);
        exp.AndIF( null != endTime, x => x.operateTime <= endTime);
        
        pager.total = db.Queryable<SysLog>().Where(exp.ToExpression()).Count();
        pager.rows = db.Queryable<SysLog>().Where(exp.ToExpression()).OrderByDescending(x => x.operateTime)
            .Skip(pager.getSkip()).Take(pager.pageSize)
            .ToList();

        return pager;
    }

    public void Remove(List<long> ids)
    {
        if (ids.Count == 0)
        {
            return;
        }
        using var db = _dbClientFactory.GetSqlSugarClient();
        db.Deleteable<SysLog>().In(ids).ExecuteCommand();
    }
}