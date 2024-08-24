using adminModule.Bll;
using api.Common.DTO;
using domain.Result;
using infrastructure.Attributes;
using infrastructure.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adminModule.Controllers
{

    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class SysMenuController : ControllerBase
    {

        private ISysMenuBll _sysMenuBll { set; get; }
        private HttpContext _httpContext { set; get; }

        private readonly ILogger<SysMenuController> _logger;

        public SysMenuController(ILogger<SysMenuController> logger, ISysMenuBll sysMenuBll, IHttpContextAccessor contextAccessor)
        {
            this._logger = logger;
            this._httpContext = contextAccessor.HttpContext;
            this._sysMenuBll = sysMenuBll;

        }

        [HttpGet]
        public ApiResult tree(string? title)
        {
            return ApiResult.succeed(_sysMenuBll.GetTree(Convert.ToInt64(HttpContextUtil.getUserId(_httpContext)), title));
        }

        [RequiredPermission("sys:menu:list")]
        [HttpGet]
        public ApiResult list(int? status, string? title)
        {
            return ApiResult.succeed(_sysMenuBll.GetList(status, title));
        }

        [SysLog("添加菜单")]
        [RequiredPermission("sys:menu:add")]
        [HttpPost]
        public ApiResult addMenu([FromBody] SysMenuDto dto)
        {
            _sysMenuBll.AddMenu(dto);
            return ApiResult.succeed();
        }
        
        [SysLog("修改菜单")]
        [RequiredPermission("sys:menu:update")]
        [HttpPut]
        public ApiResult modify([FromBody] SysMenuDto dto)
        {
            _sysMenuBll.UpdateMenu(dto);
            return ApiResult.succeed();
        }
        
        [SysLog("删除菜单")]
        [RequiredPermission("sys:menu:del")]
        [HttpDelete("{menuId}")]
        public ApiResult delMenu([FromRoute] long menuId)
        {
            _sysMenuBll.Remove(menuId);
            return ApiResult.succeed();
        }


    }
}
