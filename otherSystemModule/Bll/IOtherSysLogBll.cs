using domain.Pojo.ortherSystems;
using infrastructure.Utils;

namespace otherSystemModule.Bll;

public interface IOtherSysLogBll
{

    void Save(OtherSysLog otherSysLog);


    void Delete(List<long> ids);
    
    
    Pager<OtherSysLog> GetList(string? path, bool? executeStatus, string? sysName, DateTime? startTime, DateTime? endTime, int pageNum, int pageSize);


}