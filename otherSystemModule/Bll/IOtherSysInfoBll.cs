using domain.Pojo.ortherSystems;
using infrastructure.Utils;

namespace otherSystemModule.Bll;

public interface IOtherSysInfoBll
{

    /// <summary>
    /// 保存第三方系统信息
    /// </summary>
    /// <param name="otherSysInfo"></param>
    void Save(OtherSysInfo otherSysInfo);

    /// <summary>
    /// 校验第三方系统密钥
    /// </summary>
    /// <param name="appKey"></param>
    /// <returns></returns>
    void IsPermissions(string? appKey, OtherSysLog otherSysLog);

    /// <summary>
    /// 生成第三方系统接口密钥
    /// </summary>
    /// <param name="id"></param>
    void BuildAppkey(long id);

    /// <summary>
    /// 批量删除第三方系统
    /// </summary>
    /// <param name="ids"></param>
    void Delete(List<long> ids);

    /// <summary>
    /// 更新第三方系统信息
    /// </summary>
    /// <param name="otherSysInfo"></param>
    void Update(OtherSysInfo otherSysInfo);

    /// <summary>
    /// 获取分页系统配置
    /// </summary>
    /// <param name="sysName"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <param name="pageNum"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Pager<OtherSysInfo> GetList(string? sysName, DateTime? startTime, DateTime? endTime, int pageNum, int pageSize);

}