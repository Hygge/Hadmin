using SqlSugar;

namespace domain.Pojo.quartz;

public class JobInfo
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
    /// 程序集名称
    /// </summary>
    public string assemblyName { set; get; } = string.Empty;
    /// <summary>
    /// 计划内容全类名
    /// </summary>
    public string typeName { set; get; } = string.Empty;
    /// <summary>
    /// 计划key
    /// </summary>
    public string jobKey { set; get; } = string.Empty;
    /// <summary>
    /// 计划分组
    /// </summary>
    public string jobGroup { set; get; } = string.Empty;
    /// <summary>
    /// cron 执行表达式
    /// </summary>
    public string cronExpression { set; get; } = string.Empty;
    /// <summary>
    /// 计划策略 1立即执行、2计划执行
    /// </summary>
    public int misfirePolicy  { set; get; } = 2;
    /// <summary>
    /// 并发执行  true并发 false串行
    /// </summary>
    public bool concurrent  { set; get; } = false;
    /// <summary>
    /// 任务状态 0正常 1暂停 2放弃
    /// </summary>
    public int status  { set; get; } = 0;
    /// <summary>
    /// 执行周期 默认0秒，如果 cron存在则不执行此执行策略
    /// </summary>
    public int seconds { set; get; } = 0;
    /// <summary>
    /// 执行次数 -1默认无限制
    /// </summary>
    public int repeat { set; get; } = -1;





}