using infrastructure.Attributes;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webApi.Authorizations
{

    public class CustomizeAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler defaultHandler = new();

        public async Task HandleAsync(
            RequestDelegate next,
            HttpContext context,
            AuthorizationPolicy policy,
            PolicyAuthorizationResult authorizeResult)
        {

            if (authorizeResult.Forbidden && authorizeResult.AuthorizationFailure!.FailedRequirements
                    .OfType<RequiredPermission>().Any())
            {
                //自定义自己想要返回的数据结果，我这里要返回的是Json对象，通过引用Newtonsoft.Json库进行转换
                var payload = JsonConvert.SerializeObject(new { code = "403", message = "很抱歉，您暂无权限访问该接口！" });
                //自定义返回的数据类型
                context.Response.ContentType = "application/json";
                //自定义返回状态码，默认为401 我这里改成 200
                context.Response.StatusCode = StatusCodes.Status200OK;
                //输出Json数据结果
                context.Response.WriteAsync(payload);
                return;
            }

            // Fall back to the default implementation.
            await defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }
}
