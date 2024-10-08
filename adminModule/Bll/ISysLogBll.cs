﻿using domain.Pojo.sys;
using infrastructure.Utils;

namespace adminModule.Bll;

public interface ISysLogBll
{


    public void Save(SysLog log);

    public Pager<SysLog> GetList(string? path, string? operation, string? operatorName,
        DateTime? startTime, DateTime? endTime, int pageNum, int pageSize);

    public void Delete(List<long> ids);

}