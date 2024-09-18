using domain.Pojo.ortherSystems;
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
public class OtherSysInfoController : ControllerBase
{

    private readonly ILogger<OtherSysInfoController> _logger;
    private readonly IOtherSysInfoBll _otherSysInfoBll;
    
    public OtherSysInfoController(ILogger<OtherSysInfoController> logger, IOtherSysInfoBll otherSysInfoBll)
    {
        this._logger = logger;
        this._otherSysInfoBll = otherSysInfoBll;
    }

    [HttpGet]
    public ApiResult getList(string? sysName, DateTime? startTime, DateTime? endTime, int pageNum, int pageSize)
    {
        return ApiResult.succeed(_otherSysInfoBll.GetList(sysName, startTime, endTime, pageNum, pageSize));
    }
    [SysLog("新增接入第三方系统")]
    [RequiredPermission("om:othersys:add")]
    [HttpPost]
    public ApiResult add([FromBody] OtherSysInfo otherSysInfo)
    {
        _otherSysInfoBll.Save(otherSysInfo);
        return ApiResult.succeed();
    }
    [SysLog("修改第三方系统信息")]
    [RequiredPermission("om:othersys:update")]
    [HttpPut]
    public ApiResult update([FromBody] OtherSysInfo otherSysInfo)
    {
        _otherSysInfoBll.Update(otherSysInfo);
        return ApiResult.succeed();
    }
    [SysLog("删除第三方系统信息")]
    [RequiredPermission("om:othersys:del")]
    [HttpDelete]
    public ApiResult del([FromBody] List<long> ids)
    {
        _otherSysInfoBll.Delete(ids);
        return ApiResult.succeed();
    }

    [SysLog("生成第三方系统密钥")]
    [RequiredPermission("om:othersys:buildappkey")]
    [HttpPost]
    public ApiResult buildAppkey(long id)
    {
        _otherSysInfoBll.BuildAppkey(id);
        return ApiResult.succeed();
    }
    
}