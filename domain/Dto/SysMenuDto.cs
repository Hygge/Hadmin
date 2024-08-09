namespace api.Common.DTO;

public class SysMenuDto
{
            public long? id { get; set; } 
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
            public string? icon { get; set; }
            /// <summary>
            /// 路由地址
            /// </summary>
            public string? route { get; set; }
            /// <summary>
            /// 页面缓存
            /// </summary>
            public string? keepAlive { get; set; }
            /// <summary>
            /// 组件全路径
            /// </summary>
            public string? path { get; set; }
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
            public int? show { get; set; }
            /// <summary>
            /// 0 内链 1外链
            /// </summary>
            public int? target { get; set; }
            
            /// <summary>
            /// 参数
            /// </summary>
            public string? query { get; set; }
    
            /// <summary>
            /// 排序
            /// </summary>
            public int order { get; set; } = 99;
}