using adminModule.Bll;
using domain.Result;
using infrastructure.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace adminModule.Controllers;

[Authorize]
[Route("[controller]/[action]")]
[ApiController]
public class SysLogController : ControllerBase
{

    private readonly ILogger<SysLogController> _logger;
    private readonly ISysLogBll _logBll;

    public SysLogController(ILogger<SysLogController> logger, ISysLogBll logBll)
    {
        this._logger = logger;
        this._logBll = logBll;
    }


    [HttpPost]
    public ApiResult list(string? path, string? operation, string? operatorName,  
        DateTime? startTime, DateTime? endTime, int pageNum , int pageSize)
    {
        return ApiResult.succeed(_logBll.GetList(path, operation, operatorName, startTime, endTime, pageNum, pageSize ));
    }

    [SysLog("删除操作日志")]
    [RequiredPermission("sys:log:del")]
    [HttpDelete]
    public ApiResult del([FromBody]List<long> ids)
    {
        _logBll.Delete(ids);
        return ApiResult.succeed();
    }
    
    
}