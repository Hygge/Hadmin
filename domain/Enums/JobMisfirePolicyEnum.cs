namespace domain.Enums;

public enum JobMisfirePolicyEnum
{
    
    /// <summary>
    /// 立即执行
    /// </summary>
    NOW = 1,
    /// <summary>
    /// 计划执行
    /// </summary>
    PLAN = 2,
    /// <summary>
    /// 执行一次
    /// </summary>
    ONCE,
    /// <summary>
    /// 放弃执行
    /// </summary>
    GIVE,
    
}
