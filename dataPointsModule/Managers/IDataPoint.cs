namespace dataPointsModule.Managers;

public interface IDataPoint<T>
{

    /// <summary>
    /// 设备连接
    /// </summary>
    /// <param name="t"></param>
    void Connect(T t);
    
    /// <summary>
    /// 设备断开连接
    /// </summary>
    /// <param name="t"></param>
    void Disconnect(T t);

    /// <summary>
    /// 设备重连
    /// </summary>
    /// <param name="t"></param>
    void Reconnect(T t);

    /// <summary>
    /// 获取设备
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    T GetDevice(string ip);


}