using System.Collections.Generic;
using domain.Pojo.protocol;
using infrastructure.Utils;

namespace dataPointsModule.Bll;

public interface IModbusDataBll
{
    /// <summary>
    /// 保存数据点
    /// </summary>
    /// <param name="modbusData"></param>
    public void Save(ModbusData modbusData);

    /// <summary>
    /// 删除数据点
    /// </summary>
    /// <param name="ids"></param>
    public void Remove(List<long> ids);

    
    public Pager<ModbusData> Find();



}