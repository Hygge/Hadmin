using domain.Pojo.quartz;
using infrastructure.Utils;

namespace quartzModeule.Bll;

public interface IJobLogBll
{

    void Save(JobLog jobLog);
    
    Task<Pager<JobLog>> GetList(string? jobName, string? category, bool? status, int pageNum = 1, int pageSize = 10);

    Task Delete(List<long> ids);

    Task ClearTable();

}