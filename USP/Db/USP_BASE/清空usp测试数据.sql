-----�������usp��������

--ɾ����˾
delete from SysCorp where ID>0
go

--ɾ����˾����
delete from SysCorpType where ID>0
go

--ɾ������Ա
delete from SysOperator where ID>0
go

--ɾ����¼��־
delete from SysLoginLog where ID>0
go

--ɾ��Ȩ��ģ��
delete from SysPrivilegeTemplate where CorpType>0
go

--ɾ���˵�ģ��
delete from SysMenuTemplate where CorpType>0
go

--ɾ����ɫ�˵�
delete from SysRoleMenu where Role>0
go

--ɾ����ɫȨ��
delete from SysRolePrivilege  where Role>0
go

--ɾ����ɫ����Ա
delete from SysRoleOperator  where Operator>0
go

--ɾ����ɫ
delete from SysRole where ID>0
go

--ɾ��Ȩ��
--delete from SysPrivilege where ID>0
go

--ɾ���˵�
--delete from SysMenu where ID>0
--go

-----���ɾ���˲˵���Ȩ�޿���ʹ��һ�·�ʽ��ʼ��������������ԱȨ��:��ϵͳ������˲˵���/system/system/Menus����Ȩ�ޣ�/system/system/Privileges��  ��ִ�����ĳ�ʼ����������Ա�Ĳ˵���Ȩ��

--��ʼ����������Ա �˵�Ȩ��
--insert into SysRoleMenu(Role,Menu,Creator,Auditor) select 0,ID,0,0 from SysMenu
--go

--��ʼ����������Ա ��ťȨ��
--insert into SysRolePrivilege(Role,Privilege,Creator,Auditor) select 0,ID,0,0 from SysPrivilege
--go

  --insert into SysRolePrivilege(Role,Privilege,Creator,Auditor) select 0,ID,0,0 from SysPrivilege
  --WHERE id NOT IN  (SELECT privilege FROM SysRolePrivilege WHERE Role=0 )

 -- insert into SysRoleMenu(Role,Menu,Creator,Auditor) select 0,ID,0,0 from SysMenu
 -- WHERE ID NOT IN (SELECT Menu FROM dbo.SysRoleMenu WHERE role=0)