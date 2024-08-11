namespace domain.Pojo.sys;

/// <summary>
/// 系统操作日志
/// </summary>
public class SysLog
{
    /// <summary>
    /// 日志编号
    /// </summary>
    public long id { set; get; }
    
    /// <summary>
    /// 日志操作名称
    /// </summary>
    public string operation { set; get; }
    
    /// <summary>
    /// 请求操作参数
    /// </summary>
    public string requestParam { set; get; }
    
    /// <summary>
    /// 操作人
    /// </summary>
    public string operatorName { set; get; }
    /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime operateTime { set; get; }
    /// <summary>
    /// 执行时间
    /// </summary>
    public long executeTime { set; get; }
    /// <summary>
    /// 执行状态
    /// </summary>
    public bool executeStatus { set; get; }
    
}