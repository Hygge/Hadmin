namespace infrastructure.Attributes;

/// <summary>
/// 其他系统接口标识
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method )]
public class OtherSystemsInterfaceAttribute : Attribute
{
    public OtherSystemsInterfaceAttribute(string title) => name = title;
    public string name { get; }
}