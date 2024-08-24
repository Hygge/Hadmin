using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace domain.Pojo.sys
{
    /// <summary>
    /// 系统角色
    /// </summary>
    public class SysRole
    {
        /// <summary>
        /// id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long id { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string roleName { get; set; }
        /// <summary>
        /// 角色权限字符
        /// </summary>
        public string roleKey { get; set; }

        /// <summary>
        /// 0启用 1禁用
        /// </summary>
        public int disabled { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string createdBy { set; get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createdTime { set; get; } = DateTime.Now;

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { set; get; }

    }
}
