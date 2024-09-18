using System;
using System.Collections.Generic;
using domain.Pojo.protocol;
using domain.Result;
using infrastructure.Attributes;
using infrastructure.Db;
using infrastructure.Exceptions;
using infrastructure.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Yitter.IdGenerator;

namespace dataPointsModule.Bll.Impl;

[Service(ServiceLifetime.Singleton)]
public class ModbusDataBll : IModbusDataBll
{
    private readonly ILogger<ModbusDataBll> _logger;
    private readonly DbClientFactory _dbClientFactory;

    public ModbusDataBll(ILogger<ModbusDataBll> logger, DbClientFactory dbClientFactory)
    {
        this._logger = logger;
        this._dbClientFactory = dbClientFactory;
    }
    
    public async void Save(ModbusData modbusData)
    {
        using var db = _dbClientFactory.GetSqlSugarClient();
        ModbusData? m1 = await db.Queryable<ModbusData>().SingleAsync(x => x.name.Equals(modbusData.name));
        if (null != m1)
        {
            throw new BusinessException(HttpCode.FAILED_CODE, "数据点名称已存在");
        }

        modbusData.id = YitIdHelper.NextId();
        db.Insertable<ModbusData>(modbusData).ExecuteCommand();

    }

    public void Remove(List<long> ids)
    {
        if (ids.Count == 0)
        {
            return;
        }
        using var db = _dbClientFactory.GetSqlSugarClient();
        db.Deleteable<ModbusData>().In(ids);
    }

    public Pager<ModbusData> Find()
    {
        throw new NotImplementedException();
    }
}