using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Pojo.sys
{
    /// <summary>
    /// 系统用户角色关系
    /// </summary>
    public class SysUserAndRole
    {
        public long userId { set; get; }
        public long roleId { set; get; }
    }
}
