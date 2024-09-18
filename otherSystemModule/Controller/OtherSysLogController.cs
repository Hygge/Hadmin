using domain.Result;
using infrastructure.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using otherSystemModule.Bll;

namespace otherSystemModule.Controller;

[Authorize]
[Route("[controller]/[action]")]
[ApiController]
public class OtherSysLogController : ControllerBase
{
    private readonly ILogger<OtherSysLogController> _logger;
    private readonly IOtherSysLogBll otherSysLogBll;
    
    public OtherSysLogController(ILogger<OtherSysLogController> logger, IOtherSysLogBll otherSysLogBll)
    {
        this._logger = logger;
        this.otherSysLogBll = otherSysLogBll;
    }
    
    [HttpGet]
    public ApiResult getList(string? path, bool? executeStatus, string? sysName, DateTime? startTime, DateTime? endTime, int pageNum, int pageSize)
    {
        return ApiResult.succeed(otherSysLogBll.GetList(path, executeStatus, sysName, startTime, endTime, pageNum, pageSize));
    }
    
    [SysLog("删除其他系统日志记录")]
    [RequiredPermission("om:othersyslog:del")]
    [HttpDelete]
    public ApiResult del([FromBody] List<long> ids)
    {
        otherSysLogBll.Delete(ids);
        return ApiResult.succeed();
    }
    
    
    
}