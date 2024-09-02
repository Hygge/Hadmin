using domain.Enums;
using domain.Pojo.quartz;
using domain.Result;
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
    
    [HttpPost]
    public async Task<ApiResult> addJob([FromBody] JobInfo jobInfo)
    {
        await _jobInfoBll.Save(jobInfo);
        return ApiResult.succeed();
    }
    
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

    
    
}