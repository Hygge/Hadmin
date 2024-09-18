using domain.Pojo.sys;

namespace domain.Pojo.ortherSystems;

/// <summary>
/// 第三方系统执行日志
/// </summary>
public class OtherSysLog : SysLog
{
    /// <summary>
    /// 调用方系统名称
    /// </summary>
    public string sysName { get; set; } = string.Empty;
    
    /// <summary>
    /// 目标系统名称
    /// </summary>
    public string sysTargetName { get; set; } = string.Empty;

    /// <summary>
    /// 系统ip
    /// </summary>
    public string ip { set; get; } = string.Empty;


}