using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Pojo.sys
{
    /// <summary>
    /// 系统角色菜单关系
    /// </summary>
    public class SysRoleAndMenu
    {
        public long menuId { set; get; }
        public long roleId { set; get; }

    }
}
