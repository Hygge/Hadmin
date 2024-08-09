using domain.Enums;
using domain.Pojo.sys;
using domain.Vo;
using infrastructure.Attributes;
using infrastructure.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using adminModule.Bll;

namespace webApi.Authorizations
{
    public class PermissionsAuthorizationHandler : AuthorizationHandler<RequiredPermission>
    {
        private readonly ILogger<PermissionsAuthorizationHandler> _logger;
        private readonly ISysRoleBll roleBll;
        public PermissionsAuthorizationHandler(ILogger<PermissionsAuthorizationHandler> logger, ISysRoleBll roleBll)
        {
            _logger = logger;
            this.roleBll = roleBll;
        }

        /// <summary>
        /// 自定义授权校验方法
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement">权限字符串</param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            RequiredPermission requirement)
        {
            // 认证未通过就不用往下走了
            if (!context.User.Identity.IsAuthenticated)
            {
                return Task.CompletedTask;
            }
            _logger.LogWarning("权限字符校验-- {Key}", requirement.Key);
            if (string.IsNullOrEmpty(requirement.Key))
            {
                context.Succeed(requirement);
            }

            // 需要考虑权限控制是否放入webApi模块
            try
            {
                var userId = long.Parse(context.User?.Claims?.FirstOrDefault(c => c.Type == "Id").Value); 
                var userName = context.User.Claims.FirstOrDefault(c => c.Type == "userName").Value;
                var perms = roleBll.GetPermissons(userId);
                if (perms == null || perms.Count() == 0)
                {
                    return Task.CompletedTask;
                }
                if (perms.Any(p => p.Equals(requirement.Key)))
                {
                    //放行
                    context.Succeed(requirement);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"权限校验失败请检查数据库：{ex.Message}");
            }
            return Task.CompletedTask;
        }
    }


}