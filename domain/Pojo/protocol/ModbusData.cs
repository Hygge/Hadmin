namespace domain.Pojo.protocol;


/// <summary>
/// 数据点
/// </summary>
public class ModbusData
{
    public long id { set; get; }
    /// <summary>
    /// 数据点id
    /// </summary>
    public string name { set; get; } = string.Empty;
    /// <summary>
    /// 类别
    /// </summary>
    public string category { set; get; } = string.Empty;
    /// <summary>
    /// ip
    /// </summary>
    public string ip { set; get; } = string.Empty;
    /// <summary>
    /// 端口号
    /// </summary>
    public int port { set; get; } = 502;
    /// <summary>
    /// 开始地址
    /// </summary>
    public int startAddress { set; get; } = 0;
    /// <summary>
    /// 长度
    /// </summary>
    public int length { set; get; } = 1;
    /// <summary>
    /// 值
    /// </summary>
    public int value { set; get; } = 0;
    /// <summary>
    /// 备注
    /// </summary>
    public string remark { set; get; } = string.Empty;
    
    /// <summary>
    /// 其他
    /// </summary>
    public int hardwareType { set; get; } = 0;

}