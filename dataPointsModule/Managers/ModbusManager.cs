using System;
using domain.Pojo.protocol;
using infrastructure.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace dataPointsModule.Managers;

/// <summary>
/// Modbus长连接管理
/// </summary>
[Component(ServiceLifetime.Singleton)]
public class ModbusManager : IDataPoint<ModbusData>
{
    public void Connect(ModbusData t)
    {
        throw new NotImplementedException();
    }

    public void Disconnect(ModbusData t)
    {
        throw new NotImplementedException();
    }

    public void Reconnect(ModbusData t)
    {
        throw new NotImplementedException();
    }

    public ModbusData GetDevice(string ip)
    {
        throw new NotImplementedException();
    }
}