﻿using domain.Pojo.quartz;
using infrastructure.Attributes;
using infrastructure.Db;
using infrastructure.Exceptions;
using infrastructure.Utils;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
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

    public async Task<Pager<JobLog>> GetList(string? jobName, string? category, bool? status, int pageNum = 1, int pageSize = 10)
    {
        Pager<JobLog> pager = new Pager<JobLog>(pageNum, pageSize);
        var expr = Expressionable.Create<JobLog>();
        expr.AndIF(!string.IsNullOrEmpty(jobName), j => j.jobName.Contains(jobName));
        expr.AndIF(!string.IsNullOrEmpty(category), j => j.category.Contains(category));
        expr.AndIF(null != status , j => j.status == status);
        
        using var db = _dbClientFactory.GetSqlSugarClient();
        pager.rows = await db.Queryable<JobLog>().Where(expr.ToExpression()).OrderByDescending( x => x.startTime).Skip(pager.getSkip()).Take(pager.pageSize).ToListAsync();
        pager.total = await db.Queryable<JobLog>().Where(expr.ToExpression()).CountAsync();

        return pager;
    }

    public Task Delete(List<long> ids)
    {
        using var db = _dbClientFactory.GetSqlSugarClient();
        if (ids.Count == 0)
        {
            return Task.CompletedTask;
        }

        db.Deleteable<JobLog>(ids);
       return Task.CompletedTask;
    }


    public async Task ClearTable()
    {
        using var db = _dbClientFactory.GetSqlSugarClient();
        await db.Ado.BeginTranAsync();
        try
        {
            int ret =  await db.Ado.ExecuteCommandAsync("truncate table job_log");
        }
        catch (Exception e)
        {
           await db.Ado.RollbackTranAsync();
            throw new BusinessException("清空失败");
        }
        await db.Ado.CommitTranAsync();
    }
    
    
}