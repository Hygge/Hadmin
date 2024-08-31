using System.Reflection;

namespace infrastructure.Utils;

public class ReflectionUtil
{

    public static Type GetType(string assemblyName, string typeName)
    {
        Assembly assembly = Assembly.Load(assemblyName);
        Type? type = assembly.GetType(typeName);
        if (null == type)
        {
            throw new ArgumentNullException("程序集或类名找不到");
        }
        return type;
    }
    
    
}