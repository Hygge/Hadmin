using domain.Enums;
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
public class QuartzController : ControllerBase
{
    
    private ILogger<QuartzController> _logger;
    private IJobInfoBll _jobInfoBll;

    public QuartzController(ILogger<QuartzController> logger, IJobInfoBll _jobInfoBll)
    {
        this._logger = logger;
        this._jobInfoBll = _jobInfoBll;
    }
    
    [SysLog("添加执行计划")]
    [RequiredPermission("monitor:quartz:add")]
    [HttpPost]
    public async Task<ApiResult> addJob([FromBody] JobInfo jobInfo)
    {
        await _jobInfoBll.Save(jobInfo);
        return ApiResult.succeed();
    }
    
    [SysLog("修改执行计划状态")]
    [RequiredPermission("monitor:quartz:state")]
    [HttpPost]
    public async Task<ApiResult> statusJob(long id, int status)
    {
        if (status == (int)JobStatuEnum.PAUSED)
        {
            await  _jobInfoBll.PausedJob(id);
        }
        else
        {
            await _jobInfoBll.RuningJob(id);
        }
        return ApiResult.succeed();
    }
    
    [SysLog("删除执行计划")]
    [RequiredPermission("monitor:quartz:del")]
    [HttpPost]
    public async Task<ApiResult> delJob(List<long> ids)
    {
        await _jobInfoBll.Delete(ids);
        return ApiResult.succeed();
    }

    [HttpPost]
    public async Task<ApiResult> list(string? jobName, string? category, int? status, int pageNum , int pageSize )
    {
        Pager<JobInfo> pager = await _jobInfoBll.GetList(jobName, category, status, pageNum, pageSize);
        return ApiResult.succeed(pager);
    }

    [SysLog("修改执行计划")]
    [RequiredPermission("monitor:quartz:update")]
    [HttpPut]
    public async Task<ApiResult> update([FromBody] JobInfo jobInfo)
    {
        await _jobInfoBll.Update(jobInfo);
        return ApiResult.succeed();
    }
    
    
}