using System;
using domain.Pojo.protocol;
using infrastructure.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace dataPointsModule.Managers;

/// <summary>
/// S7长连接管理
/// </summary>
[Component(ServiceLifetime.Singleton)]
public class S7Manager : IDataPoint<S7Data>
{
    public void Connect(S7Data t)
    {
        throw new NotImplementedException();
    }

    public void Disconnect(S7Data t)
    {
        throw new NotImplementedException();
    }

    public void Reconnect(S7Data t)
    {
        throw new NotImplementedException();
    }

    public S7Data GetDevice(string ip)
    {
        throw new NotImplementedException();
    }
}