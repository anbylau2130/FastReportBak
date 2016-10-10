-----用于清空usp测试数据

--删除公司
delete from SysCorp where ID>0
go

--删除公司类型
delete from SysCorpType where ID>0
go

--删除操作员
delete from SysOperator where ID>0
go

--删除登录日志
delete from SysLoginLog where ID>0
go

--删除权限模板
delete from SysPrivilegeTemplate where CorpType>0
go

--删除菜单模板
delete from SysMenuTemplate where CorpType>0
go

--删除角色菜单
delete from SysRoleMenu where Role>0
go

--删除角色权限
delete from SysRolePrivilege  where Role>0
go

--删除角色操作员
delete from SysRoleOperator  where Operator>0
go

--删除角色
delete from SysRole where ID>0
go

--删除权限
--delete from SysPrivilege where ID>0
go

--删除菜单
--delete from SysMenu where ID>0
--go

-----如果删除了菜单和权限可以使用一下方式初始化顶级超级管理员权限:在系统中添加了菜单（/system/system/Menus）和权限（/system/system/Privileges）  后执行最后的初始化超级管理员的菜单和权限

--初始化超级管理员 菜单权限
--insert into SysRoleMenu(Role,Menu,Creator,Auditor) select 0,ID,0,0 from SysMenu
--go

--初始化超级管理员 按钮权限
--insert into SysRolePrivilege(Role,Privilege,Creator,Auditor) select 0,ID,0,0 from SysPrivilege
--go

  --insert into SysRolePrivilege(Role,Privilege,Creator,Auditor) select 0,ID,0,0 from SysPrivilege
  --WHERE id NOT IN  (SELECT privilege FROM SysRolePrivilege WHERE Role=0 )

 -- insert into SysRoleMenu(Role,Menu,Creator,Auditor) select 0,ID,0,0 from SysMenu
 -- WHERE ID NOT IN (SELECT Menu FROM dbo.SysRoleMenu WHERE role=0)