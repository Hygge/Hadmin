using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace domain.Pojo.sys
{
    /// <summary>
    /// 系统菜单
    /// </summary>
    public class SysMenu
    {
        /// <summary>
        /// id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long id { get; set; } 
        /// <summary>
        /// 父级菜单id
        /// </summary>
        public long parentId { get; set; } = 0;
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 1目录 2菜单 3按钮
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 图标字符串
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 路由地址
        /// </summary>
        public string route { get; set; }
        /// <summary>
        /// 页面缓存
        /// </summary>
        public string keepAlive { get; set; }
        /// <summary>
        /// 菜单唯一key
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// 组件全路径
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// 权限字符串
        /// </summary>
        public string pem { get; set; }
        /// <summary>
        /// 1启用 0禁用
        /// </summary>
        public int enable { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public int show { get; set; }
        /// <summary>
        /// 0 内链 1外链
        /// </summary>
        public int target { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public string query { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int order { get; set; } = 99;

        public DateTime createdTime { get; set; } = DateTime.Now;
    }
}
