using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace infrastructure.Utils
{
    public class HttpContextUtil
    {
        public static string getUserName(HttpContext _httpContext)
        {
           var  claim = _httpContext.User.Claims.FirstOrDefault(c => c.Type == "userName");
           if (null == claim)
           {
               return string.Empty;
           }
           return claim.Value;
        }

        public static string getUserId(HttpContext _httpContext)
        {
            var claim = _httpContext.User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (null == claim)
            {
                return string.Empty;
            }
            return claim.Value;
        }
        public static async Task<string> getRequestParams(HttpContext context)
        {
            // 保证request.body可以重复读取
            context.Request.EnableBuffering();
            var contentType = context.Request.ContentType;
            Dictionary<string, string> dictionary = new();
            var query = context.Request.Query;
            if (query.Count > 0)
            {
                Dictionary<string, string> qs = new();
                foreach (var queryKey in query.Keys)
                {
                    qs.Add(queryKey, query[queryKey]);
                }
                dictionary.Add("Query", JsonConvert.SerializeObject(qs));
            }
            if (contentType != null && contentType.Contains("multipart/form-data"))
            {
                try
                {
                    var form = context.Request.Form;
                    if (form.Count > 0)
                    {
                        dictionary.Add("Form", JsonConvert.SerializeObject(context.Request.Form));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
           
                }
            }
            if (contentType != null && contentType.Contains("application/json"))
            {
                StreamReader stream = new StreamReader(context.Request.Body);
                string body = await stream.ReadToEndAsync();
                context.Request.Body.Position = 0;
                if (!string.IsNullOrEmpty(body))
                {
                    dictionary.Add("body", body);
                }
            }
            
            return dictionary.Count > 0? JsonConvert.SerializeObject(dictionary) : string.Empty;
        }
        
        

    }
}
