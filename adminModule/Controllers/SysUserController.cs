using adminModule.Bll;
using api.Common.DTO;
using domain.Result;
using infrastructure.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using infrastructure.Attributes;

namespace adminModule.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SysUserController : ControllerBase
    {

        private readonly ILogger<SysUserController> _logger;
        private readonly ISysUserBll sysUserBll;
        private readonly HttpContext _httpContext;

        public SysUserController(ILogger<SysUserController> logger, ISysUserBll sysUserBll, IHttpContextAccessor contextAccessor)
        {
            this._logger = logger;
            this.sysUserBll = sysUserBll;
            this._httpContext = contextAccessor.HttpContext;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [SysLog("添加用户")]
        [RequiredPermission("sys:user:add")]
        [HttpPost]
        public ApiResult add([FromBody] SysUserDto userDto)
        {
            sysUserBll.Add(userDto, HttpContextUtil.getUserName(_httpContext));
            return ApiResult.succeed();
        }


        [HttpGet]
        public ApiResult getList(int current, int pageSize, string? userName, string? phone, int? status,
            DateTime? startTime, DateTime? endTime)
        {
            if (current == 0 || pageSize == 0)
            {
                current = 1;
                pageSize = 10;
            }
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("pageNumber", current);
            dictionary.Add("pageSize", pageSize);
            dictionary.Add("userName", userName);
            dictionary.Add("phone", phone);
            dictionary.Add("status", status);
            dictionary.Add("startTime", startTime);
            dictionary.Add("endTime", endTime);



            return ApiResult.succeed(sysUserBll.GetList(dictionary));
        }

        [SysLog("删除用户")]
        [RequiredPermission("sys:user:del")]
        [HttpDelete("{id}")]
        public ApiResult del([FromRoute] long id)
        {
            sysUserBll.Remove(id);
            return ApiResult.succeed();
        }
        [SysLog("更新用户信息")]
        [RequiredPermission("sys:user:update")]
        [HttpPut("{id}")]
        public ApiResult update([FromRoute] long id, [FromBody] SysUserDto dto)
        {

            _logger.LogInformation(JsonConvert.SerializeObject(dto) + " \t" + id);
            sysUserBll.Modify(dto);
            return ApiResult.succeed();
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [SysLog("重置用户密码")]
        [RequiredPermission("sys:user:resetpassword")]
        [HttpPatch("{id}")]
        public ApiResult resetPassowrd([FromRoute] long id, string password)
        {
            sysUserBll.ModifyPassword(id, null, password);
            return ApiResult.succeed();
        }



    }
}
