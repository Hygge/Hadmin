using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace domain.Pojo.sys
{
    /// <summary>
    /// 系统用户
    /// </summary>
    public class SysUser
    {
        /// <summary>
        /// id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long id { get; set; } 
        /// <summary>
        /// 用户名称
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string nickName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string phoneNumber { get; set; }
        public string remark { get; set; }
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
    }
}
