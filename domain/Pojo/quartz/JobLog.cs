using SqlSugar;

namespace domain.Pojo.quartz;

public class JobLog
{
    [SugarColumn(IsPrimaryKey = true)]
    public long id { set; get; }
    /// <summary>
    /// 计划名称
    /// </summary>
    public string jobName { set; get; } = string.Empty;
    /// <summary>
    /// 计划类别
    /// </summary>
    public string category { set; get; } = string.Empty;
    /// <summary>
    /// 计划内容全类名
    /// </summary>
    public string typeName { set; get; } = string.Empty;
    /// <summary>
    /// 日志信息
    /// </summary>
    public string jobMessage { set; get; } = string.Empty;
    /// <summary>
    /// 异常信息
    /// </summary>
    public string exceptionInfo { set; get; } = string.Empty;
    /// <summary>
    /// 执行状态
    /// </summary>
    public bool status { set; get; } = true;
    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime startTime { set; get; } = DateTime.Now;
    /// <summary>
    /// 停止时间
    /// </summary>
    public DateTime stopTime { set; get; } = DateTime.Now;
    
    
}