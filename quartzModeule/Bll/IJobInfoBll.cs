using domain.Pojo.quartz;
using infrastructure.Utils;

namespace quartzModeule.Bll;

public interface IJobInfoBll
{
    /// <summary>
    /// 初始化恢复正常计划执行
    /// </summary>
    void InitJobs();

    /// <summary>
    /// 保存计划
    /// </summary>
    /// <param name="jobInfo"></param>
    Task Save(JobInfo jobInfo);


    /// <summary>
    /// 修改计划
    /// </summary>
    /// <param name="jobInfo"></param>
    Task Modify(JobInfo jobInfo);

    /// <summary>
    /// 执行计划
    /// </summary>
    /// <param name="id"></param>
    Task RuningJob(long id);

    /// <summary>
    /// 删除计划
    /// </summary>
    /// <param name="id"></param>
    Task Remove(long id);

    /// <summary>
    /// 暂停计划
    /// </summary>
    /// <param name="id"></param>
     Task PausedJob(long id);

    /// <summary>
    /// 查询计划列表
    /// </summary>
    /// <param name="jobName"></param>
    /// <param name="category"></param>
    /// <param name="status"></param>
    /// <param name="pageNum"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<Pager<JobInfo>> FindJob(string? jobName, string? category, int? status, int pageNum = 1, int pageSize = 10);


}