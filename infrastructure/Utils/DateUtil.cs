using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Utils
{
    public class DateUtil
    {


        /// <summary>
        /// 获取当前日期前缀    默认yyyy-MM-dd、
        /// </summary>
        /// <returns></returns>
        public static string getCurrentDatePre(string str = "yyyy-MM-dd")
        {
            return DateTime.Now.ToString(str);
        }
        /// <summary>
        /// 获取当前视觉字符串
        /// </summary>
        /// <returns></returns>
        public static string getCurrentDateTimeFormat(string format = "yyyy-MM-dd HH:mm:ss")
        {
            return DateTime.Now.ToString(format);
        }

    }
}
