namespace api.Common.DTO;

public class SysUserDto
{
    public long? id { get; set; } 
    /// <summary>
    /// 用户名称
    /// </summary>
    public string userName { get; set; }
    /// <summary>
    /// 用户昵称
    /// </summary>
    public string nickName { get; set; }
    /// <summary>
    /// 密码
    /// </summary>
    public string? password { get; set; }
    /// <summary>
    /// 手机号码
    /// </summary>
    public string phoneNumber { get; set; }
    
    public string remark { get; set; }
    /// <summary>
    /// 0启用 1禁用
    /// </summary>
    public int disabled { get; set; }
    
    public List<long>? roleIds { set; get; }

}