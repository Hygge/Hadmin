INSERT INTO public.sys_menu (id,parent_id,title,"type",icon,route,keep_alive,"key","path",pem,"enable","show",target,query,"order",created_time) VALUES
	 (570827982655877,0,'控制台',2,'BarChartOutlined','/console','console','','/src/views/admin/home/index.vue','sys:console',1,1,0,'',10,'2024-08-02 13:29:44.149647'),
	 (575126765666693,0,'系统管理',1,'SettingOutlined','','','','','sys:system',1,1,0,'',10,'2024-08-02 13:36:50.044853'),
	 (575128720146821,575126765666693,'菜单管理',2,'MenuOutlined','/menu/list','menuList','','/src/views/admin/menu/index.vue','sys:menu:list',1,1,0,'',10,'2024-08-02 13:44:47.212497'),
	 (575129424138629,575126765666693,'角色管理',2,'ContactsOutlined','/role/list','role','','/src/views/admin/role/index.vue','sys:role:list',1,1,0,'',10,'2024-08-02 13:47:39.085017'),
	 (575129862402437,575126765666693,'用户管理',2,'UserOutlined','/user/list','user','','/src/views/admin/user/index.vue','sys:user:list',1,1,0,'',10,'2024-08-02 13:49:26.083501'),
	 (575130165617029,0,'数据点运维',1,'BulbOutlined','','','','','data:om:list',1,1,0,'',10,'2024-08-02 13:50:40.110197'),
	 (575130498392453,575129424138629,'添加角色',3,'BoldOutlined','','','','','sys:role:add',1,1,0,'',10,'2024-08-02 13:52:01.354102'),
	 (576664005095813,575129424138629,'删除角色',3,'','','','','','sys:role:del',1,1,0,'',99,'2024-08-06 21:51:52.639889'),
	 (576664658825605,575129424138629,'修改角色',3,'','','','','','sys:role:update',1,1,0,'',99,'2024-08-06 21:54:32.241537'),
	 (576664829567365,575129424138629,'菜单权限',3,'','','','','','sys:role:menu',1,1,0,'',99,'2024-08-06 21:55:13.926732');
INSERT INTO public.sys_menu (id,parent_id,title,"type",icon,route,keep_alive,"key","path",pem,"enable","show",target,query,"order",created_time) VALUES
	 (576665025974661,575129424138629,'分配用户',3,'','','','','','sys:role:user',1,1,0,'',99,'2024-08-06 21:56:01.877996'),
	 (577684586348933,575128720146821,'添加菜单',3,'AppstoreFilled','','','','','sys:menu:add',1,1,0,'',99,'2024-08-09 19:04:37.98504'),
	 (577684861866373,575128720146821,'修改菜单',3,'BookOutlined','','','','','sys:menu:update',1,1,0,'',99,'2024-08-09 19:05:45.249246'),
	 (577685273330053,575128720146821,'删除菜单',3,'BookFilled','','','','','sys:menu:del',1,1,0,'',99,'2024-08-09 19:07:25.704142'),
	 (577685436129669,575129862402437,'添加用户',3,'','','','','','sys:user:add',1,1,0,'',99,'2024-08-09 19:08:05.450005'),
	 (577685695037829,575129862402437,'删除用户',3,'','','','','','sys:user:del',1,1,0,'',99,'2024-08-09 19:09:08.66042'),
	 (577685991534981,575129862402437,'重置密码',3,'','','','','','sys:user:resetpassword',1,1,0,'',99,'2024-08-09 19:10:21.047429'),
	 (587128534323589,585074955129221,'新增计划',3,'','','','','','monitor:quartz:add',1,0,0,'',10,'2024-09-05 11:32:09.345765'),
	 (582883698139525,575126765666693,'操作日志',2,'AlertOutlined','/sylog/list','syslog','','/src/views/admin/syslog/index.vue','sys:syslog:list',1,1,0,'',10,'2024-08-24 11:39:52.386369'),
	 (582963474264453,582883698139525,'删除日志',3,'RestFilled','','','','','sys:log:del',1,1,0,'',10,'2024-08-24 17:04:28.979506');
INSERT INTO public.sys_menu (id,parent_id,title,"type",icon,route,keep_alive,"key","path",pem,"enable","show",target,query,"order",created_time) VALUES
	 (587128641663365,585074955129221,'修改计划',3,'','','','','','monitor:quartz:update',1,0,0,'',10,'2024-09-05 11:32:35.551074'),
	 (584968378159493,0,'系统监控',1,'UnorderedListOutlined','','','','','sys:monitor',1,1,0,'',10,'2024-08-30 09:02:27.469929'),
	 (587128943649158,585074955129221,'修改状态',3,'','','','','','monitor:quartz:state',1,0,0,'',10,'2024-09-05 11:33:49.27875'),
	 (585074955129221,584968378159493,'定时任务',2,'ClockCircleOutlined','/quartz/list','sysquartz','','/src/views/admin/quartz/index.vue','monitor:quartz:list',1,1,0,'',10,'2024-08-30 16:16:07.237163'),
	 (587129128956293,585500219978117,'删除调度日志',3,'','','','','','monitor:quartz:Log:del',1,0,0,'',10,'2024-09-05 11:34:34.51947'),
	 (587129249612165,585500219978117,'清空调度日志',3,'','','','','','monitor:quartz:Log:clear',1,0,0,'',10,'2024-09-05 11:35:03.976017'),
	 (577685783937413,575129862402437,'修改用户',3,'','','','','','sys:user:update',1,1,0,'',99,'2024-08-09 19:09:30.364694'),
	 (585500219978117,585074955129221,'调度日志',2,'ControlOutlined','/quartz/log','quartzLog','','/src/views/admin/quartz/logList.vue','monitor:quartz:Log:list',1,0,0,'',10,'2024-08-31 21:06:31.663643'),
	 (587128431866245,585074955129221,'删除计划',3,'DollarCircleOutlined','','','','','monitor:quartz:del',1,0,0,'',10,'2024-09-05 11:31:44.331686');

INSERT INTO public.sys_menu
(id, parent_id, title, "type", icon, route, keep_alive, "key", "path", pem, "enable", "show", target, query, "order", created_time)
VALUES(589689102934405, 589671762661765, '删除其他系统日志', 3, '', '', '', '', '', 'om:othersyslog:del', 1, 1, 0, '', 10, '2024-09-12 17:11:08.166');
INSERT INTO public.sys_menu
(id, parent_id, title, "type", icon, route, keep_alive, "key", "path", pem, "enable", "show", target, query, "order", created_time)
VALUES(589339000910213, 0, '系统运维', 1, 'RobotOutlined', '', '', '', '', 'sys:om', 1, 1, 0, '', 10, '2024-09-11 17:26:34.039');
INSERT INTO public.sys_menu
(id, parent_id, title, "type", icon, route, keep_alive, "key", "path", pem, "enable", "show", target, query, "order", created_time)
VALUES(589647154471301, 589574326399365, '新增其他系统', 3, '', '', '', '', '', 'om:othersys:add', 1, 1, 0, '', 10, '2024-09-12 14:20:26.842');
INSERT INTO public.sys_menu
(id, parent_id, title, "type", icon, route, keep_alive, "key", "path", pem, "enable", "show", target, query, "order", created_time)
VALUES(589647330173318, 589574326399365, '删除其他系统', 3, '', '', '', '', '', 'om:othersys:del', 1, 1, 0, '', 10, '2024-09-12 14:21:09.738');
INSERT INTO public.sys_menu
(id, parent_id, title, "type", icon, route, keep_alive, "key", "path", pem, "enable", "show", target, query, "order", created_time)
VALUES(589647456719238, 589574326399365, '删除其他系统', 3, '', '', '', '', '', 'om:othersys:update', 1, 1, 0, '', 1, '2024-09-12 14:21:40.633');
INSERT INTO public.sys_menu
(id, parent_id, title, "type", icon, route, keep_alive, "key", "path", pem, "enable", "show", target, query, "order", created_time)
VALUES(589647658971526, 589574326399365, '刷新密钥', 3, '', '', '', '', '', 'om:othersys:buildappkey', 1, 1, 0, '', 10, '2024-09-12 14:22:30.011');
INSERT INTO public.sys_menu
(id, parent_id, title, "type", icon, route, keep_alive, "key", "path", pem, "enable", "show", target, query, "order", created_time)
VALUES(589574326399365, 589339000910213, '其他系统管理', 2, 'ClusterOutlined', '/om/othersys/list', 'otherSysInfo', '', '/src/views/sysom/othersys/sysinfo/index.vue', 'om:othersys:list', 1, 1, 0, '', 10, '2024-09-12 09:24:06.551');
INSERT INTO public.sys_menu
(id, parent_id, title, "type", icon, route, keep_alive, "key", "path", pem, "enable", "show", target, query, "order", created_time)
VALUES(589671762661765, 589339000910213, '其他系统日志', 2, 'HistoryOutlined', '/sysom/othersyslog/list', 'othersyslog', '', '/src/views/sysom/othersys/log/index.vue', 'om:othersyslog:list', 1, 1, 0, '', 3, '2024-09-12 16:00:34.701');
