
/*
class Menu {
    id;
    parentId;
    key;
    route;
    path;
    icon;
    title;
    enable; 0启用 1禁用
    pem; 权限字符
    type; 1目录、2菜单、3按钮
    show; 1显示 0不显示
    target: 0正常、1外链
    order: 0排序
    children;
}
*/

export const newmenus = [
    {
        id : 1,
        parentId : 0,
        key: "console1",
        keepAlive: "console",
        route : "/console",
        path : "/src/views/admin/home/index.vue",
        icon : "MailOutlined",
        title : "控制台",
        enable: 1,
        pem : "sys:console",
        type : 2,
        show : 1,
        target : 0,
        order: 0,
        createdTime:'2024-05-22 12:50:10',
        children : [

        ]
    },
    {
        id : 223,
        parentId : 0,
        key: "system",
        route : "",
        path : "",
        icon : "MailOutlined",
        title : "系统管理",
        enable: 1,
        pem : "sys",
        type : 1,
        show : 1,
        target : 0,
        order: 10,
        children : [
            {
                id : 1332,
                parentId : 223,
                key: "roleList",
                keepAlive: "role",
                route : "/role/list",
                path : "/src/views/admin/role/index.vue",
                icon : "MailOutlined",
                title : "角色管理",
                enable: 1,
                pem : "sys:role:list",
                type : 2,
                show : 1,
                target : 0,
                order: 101,
                children : [
                    {
                        id : 44432,
                        parentId : 1332,
                        key: "roleAdd1",
                        keepAlive: "roleAdd",
                        route : "",
                        path : "",
                        icon : "MailOutlined",
                        title : "添加角色",
                        enable: 1,
                        pem : "sys:role:add",
                        type : 3,
                        show : 1,
                        target : 0,
                        order: 101,
                    }
                ]
            },
            {
                id : 1322232,
                parentId : 223,
                key: "userList",
                keepAlive: "user",
                route : "/user/list",
                path : "/src/views/admin/user/index.vue",
                icon : "MailOutlined",
                title : "用户管理",
                enable: 1,
                pem : "sys:user:list",
                type : 2,
                show : 1,
                target : 0,
                order: 11,

            },
            {
                id : 122111,
                parentId : 223,
                key: "menulist1",
                keepAlive: "menuList",
                route : "/menu/list",
                path : "/src/views/admin/menu/index.vue",
                icon : "MailOutlined",
                title : "添加菜单",
                enable: 1,
                pem : "sys:menu:list",
                type : 2,
                show : 1,
                target : 0,
                order: 101,
            }

        ]
    },



]