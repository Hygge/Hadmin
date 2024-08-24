using adminModule.Bll;
using domain.Result;
using infrastructure.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using domain.Pojo.sys;
using infrastructure.Attributes;

namespace adminModule.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        private readonly ISysUserBll sysUserBll;
        private readonly JwtUtil _jwtUtil;
        private HttpContext _httpContext;
        public AccountController(ILogger<AccountController> logger, IServiceProvider serviceProvider, IHttpContextAccessor contextAccessor)
        {
            this._logger = logger;
            this.sysUserBll = serviceProvider.GetRequiredService<ISysUserBll>();
            this._jwtUtil = serviceProvider.GetRequiredService<JwtUtil>();
            this._httpContext = contextAccessor.HttpContext;
        }
        
        [HttpPost]
        public ApiResult login(string userName, string password)
        {
            SysLog sysLog = new();
            _logger.LogInformation($"登录账号：{userName},密码：{password}");
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["accessToken"] = _jwtUtil.CreateToken(userName, sysUserBll.Login(userName, password));
            return ApiResult.succeed(dictionary);
        }

       
        [Authorize]
        [HttpGet]
        public ApiResult info()
        {
            return ApiResult.succeed(sysUserBll.GetInfo(Convert.ToInt64(HttpContextUtil.getUserId(_httpContext))));
        }

        [SysLog("12333")]
        [HttpGet]
        public ApiResult get1()
        {
            return ApiResult.succeed();
        }


    }
}
