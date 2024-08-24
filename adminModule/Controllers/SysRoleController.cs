using domain.Pojo.sys;
using domain.Result;
using infrastructure.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using adminModule.Bll;
using domain.Records;
using infrastructure.Attributes;

namespace adminModule.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class SysRoleController : ControllerBase
    {

        private readonly ILogger<SysRoleController> _logger;
        private readonly ISysRoleBll _sysRoleBll;
        private readonly HttpContext _httpContext;


        public SysRoleController(ILogger<SysRoleController> logger, IServiceProvider serviceProvider, IHttpContextAccessor contextAccessor)
        {
            this._logger = logger;
            this._sysRoleBll = serviceProvider.GetService<ISysRoleBll>();
            this._httpContext = contextAccessor.HttpContext;
        }

        [HttpGet]
        public ApiResult list(string? roleName, string? key, int? status, DateTime? start, DateTime? end)
        {

            return ApiResult.succeed(_sysRoleBll.List(roleName, key, status, start, end));
        }
        [SysLog("添加角色")]
        [RequiredPermission("sys:role:add")]
        [HttpPost]
        public ApiResult add(string roleName, string key, int? status, string? remark)
        {
            _sysRoleBll.Add(roleName, key, remark, status, HttpContextUtil.getUserName(HttpContext));
            return ApiResult.succeed();
        }
        [SysLog("删除角色")]
        [RequiredPermission("sys:role:del")]
        [HttpDelete("{id}")]
        public ApiResult del(long id)
        {
            _sysRoleBll.Delete(id);
            return ApiResult.succeed();
        }
        [SysLog("修改角色信息")]
        [RequiredPermission("sys:role:update")]
        [HttpPut]
        public ApiResult put(SysRole sysRole)
        {
            _logger.LogInformation(JsonConvert.SerializeObject(sysRole));
            _sysRoleBll.Update(sysRole);
            return ApiResult.succeed();
        }
        [SysLog("修改角色菜单权限")]
        [RequiredPermission("sys:role:menu")]
        [HttpPost]
        public ApiResult addPermissons([FromBody]RoleBindMenu roleBindMenu)
        {

            _sysRoleBll.AddPermissons(roleBindMenu.roleId, roleBindMenu.menuIds);
            return ApiResult.succeed();
        }
        [SysLog("修改角色绑定用户")]
        [RequiredPermission("sys:role:user")]
        [HttpPost]
        public ApiResult addBindUserRole([FromBody] RoleBindUser roleBindUser)
        {
           _sysRoleBll.RoleBindUser(roleBindUser.roleId, roleBindUser.userIds);
            return ApiResult.succeed();
        }

        [HttpGet("{roleId}")]
        public ApiResult getRoleBindUser([FromRoute]long roleId)
        {
            return ApiResult.succeed(_sysRoleBll.GetRoleBindUserIds(roleId));
        }
        

        [HttpGet("{roleId}")]
        public ApiResult getPermissonMenuIds(long roleId)
        {
            return ApiResult.succeed(_sysRoleBll.GetPermissonsMenuIdsByRole(roleId));
        }

    }
}
