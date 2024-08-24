using SqlSugar;

namespace domain.Pojo.sys;

/// <summary>
/// 系统操作日志
/// </summary>
public class SysLog
{
    /// <summary>
    /// 日志编号
    /// </summary>
    [SugarColumn(IsPrimaryKey = true)]
    public long id { set; get; }

    /// <summary>
    /// 日志操作名称
    /// </summary>
    public string operation { set; get; } = string.Empty;
    
    /// <summary>
    /// 请求操作参数
    /// </summary>
    public string requestParam { set; get; } = string.Empty;
    public string responseParam { set; get; } = string.Empty;
    
    /// <summary>
    /// 操作人
    /// </summary>
    public string operatorName { set; get; } = string.Empty;
    /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime operateTime { set; get; } = DateTime.Now;
    /// <summary>
    /// 执行时间
    /// </summary>
    public long executeTime { set; get; }

    /// <summary>
    /// 执行状态
    /// </summary>
    public bool executeStatus { set; get; } = true;
    
    /// <summary>
    /// 异常原因
    /// </summary>
    public string reason { set; get; } = string.Empty;
    public string path { set; get; } = string.Empty;
    
}