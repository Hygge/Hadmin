using domain.Pojo.quartz;
using domain.Result;
using infrastructure.Attributes;
using infrastructure.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using quartzModeule.Bll;

namespace quartzModeule.Controller;

[Route("[controller]/[action]")]
[ApiController]
public class JobLogController
{
    private ILogger<JobLogController> _logger;
    private IJobLogBll _jobLogBll;

    public JobLogController(ILogger<JobLogController> logger, IJobLogBll _jobLogBll)
    {
        this._logger = logger;
        this._jobLogBll = _jobLogBll;
    }
    
    [HttpPost]
    public async Task<ApiResult> list(string? jobName, string? category, bool? status, int pageNum , int pageSize )
    {
        Pager<JobLog> pager = await _jobLogBll.GetList(jobName, category, status, pageNum, pageSize);
        return ApiResult.succeed(pager);
    }
    [SysLog("删除调度日志")]
    [RequiredPermission("monitor:quartz:Log:del")]
    [HttpPost]
    public async Task<ApiResult> delLog(List<long> ids)
    {
        await _jobLogBll.Delete(ids);
        return ApiResult.succeed();
    }

    [SysLog("清空调度日志")]
    [RequiredPermission("monitor:quartz:Log:clear")]
    [HttpDelete]
    public  async Task<ApiResult>  delAll()
    {
        await _jobLogBll.ClearTable();
        return ApiResult.succeed();
    }
    
    
    
}