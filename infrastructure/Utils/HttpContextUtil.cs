using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Utils
{
    public class HttpContextUtil
    {
        public static string getUserName(HttpContext _httpContext)
        {
            return _httpContext.User.Claims.FirstOrDefault(c => c.Type == "userName").Value;
        }

        public static string getUserId(HttpContext _httpContext)
        {
            return _httpContext.User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
        }

    }
}
