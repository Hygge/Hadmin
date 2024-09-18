using domain.Enums;
using SqlSugar;

namespace domain.Pojo.ortherSystems;


/// <summary>
/// 第三方系统信息
/// </summary>
public class OtherSysInfo
{
    [SugarColumn(IsPrimaryKey = true)]
    public long id { set; get; }

    /// <summary>
    /// 第三方系统名称
    /// </summary>
    public string name { set; get; } = string.Empty;
    
    /// <summary>
    /// 密钥
    /// </summary>
    public string appKey { set; get; } = string.Empty;

    /// <summary>
    /// 状态 0正常 1停用
    /// </summary>
    public int state { set; get; } = (int)DisabledEnum.ZERO;
    public DateTime createdTime { set; get; } = DateTime.Now;

}