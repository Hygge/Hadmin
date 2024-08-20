namespace infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public class SysLogAttribute : Attribute
{

    public string name;

    public SysLogAttribute(string s)
    {
        this.name = s;
    }

}