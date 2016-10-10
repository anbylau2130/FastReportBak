CREATE PROCEDURE UP_ShowPage
@strGetFields varchar(MAX) = '*', --  Need to return the column, the default  *        
@tblName   varchar(MAX),       --  Table name          
@strWhere varchar(MAX) = '', --  Query criteria   ( Note  :  Do not add   where)        
@strOrder varchar(MAX)='',      --  Sort of a field name, required          
@strOrderType varchar(max)='ASC', --  By default, the sort of way  ASC        
@PageIndex int = 1,           --  The page number, the default  1        
@PageSize   int = 10          --  By default, the page size  10         
AS        
declare @strSQL   varchar(MAX)       
declare @TEST   varchar(MAX)    
if @strWhere !=''        
set @strWhere=' where '+@strWhere   
set @strSQL=     
cast('SELECT * FROM (' as varchar(max)) +        
        cast(' SELECT convert(bigint,ROW_NUMBER() OVER (ORDER BY ' as varchar(max)) + @strOrder+cast(' ' as varchar(max))+ @strOrderType+cast(')) AS RowNo,convert(bigint,COUNT(0) OVER ()) as RowCnt,* ' as varchar(max)) +  
        cast(' FROM (SELECT ' as varchar(max))+ @strGetFields + cast(' ' as varchar(max)) +        
        cast(' FROM ' as varchar(max)) + @tblName + cast(' ' as varchar(max)) + @strWhere+        
cast(') AS a) AS SP ' as varchar(max))  
if not(@PageIndex=0 and @PageSize=0)  
begin  
 set @strSQL= @strSQL   
  +cast('WHERE RowNo BETWEEN ' as varchar(max)) + convert(varchar(max),(@PageIndex-1)*@PageSize+1) + cast(' AND ' as varchar(max)) + convert(varchar(max),@PageIndex*@PageSize)        
end       
exec (@strSQL)  

go

CREATE PROCEDURE UP_ShowArea
@PageIndex int = 1,
@PageSize int = 10,
@strOrder varchar(MAX) = '',
@strOrderType varchar(max) = 'ASC'
as
select * from(
    select convert(bigint, ROW_NUMBER() OVER(order by code asc)) as RowNo,
    convert(bigint, COUNT(0) OVER()) as RowCnt, *
    FROM(
        SELECT
        Parent,
        Code,
        Name,
        LocationX,
        LocationY,
        Reserve,
        Remark,
        CreateTime FROM SysArea
    ) AS a
)
AS temp WHERE RowNo BETWEEN (@PageIndex-1)*@PageSize+1 AND @PageIndex*@PageSize
go

CREATE PROCEDURE UP_AddMenu
@Name varchar(100),
@Icon varchar(100)
as
set @Icon= ISNULL(@Icon,'')
if not exists(select 1 from SysMenu where name=@Name and Parent=0)
begin
    insert into SysMenu(Direction, Parent,Name, Icon, Clazz, Area, Controller, Method, Parameter, Url, Creator,createTime)
    values(0,0,@Name,@Icon,'','','','','','',0,getdate())
end
go


CREATE PROCEDURE UP_AddMenuItem
@Parent varchar(100),
@Name varchar(100),
@Icon varchar(100),
@Class varchar(100),
@Area varchar(100),
@Controller varchar(200),
@Action varchar(100),
@Parameter varchar(1000),
@Url varchar(max)
as
declare @ParentID bigint
set @Icon= ISNULL(@Icon,'')
if @Parent=@Name
begin
    if not exists(select 1 from SysMenu where name=@Parent)
    begin
        insert into SysMenu(Direction,Parent,Name,Icon,Clazz,Area, Controller, method,Parameter,Url,Creator,CreateTime)
        values(0,0,@Parent,@Icon,'','','','','','',0,getdate())
    end
    return
end
if exists(select 1 from SysMenu where name=@Parent)
begin
    select @ParentID=ID from SysMenu where name=@Parent
    if exists(select 1 from SysMenu where Name=@Name and Parent=@ParentID)
    begin
        return
    end
end
else
begin
    insert into SysMenu(Direction,Parent,Name,Icon,Clazz,Area, Controller,Method,Parameter,Url,Creator,CreateTime)
    values(0,0,@Parent,'','','','','','','',0,getdate())
    select @ParentID=@@IDENTITY
end
insert into SysMenu(Direction,Parent,Name,Icon,Clazz,Area,Controller,Method,Parameter,Url,Creator,CreateTime)
    values(0,@ParentID,@Name,@Icon,@Class,@Area,@Controller,@Action,@Parameter,@Url,0,getdate())
go

CREATE PROCEDURE UP_AddPrivilege
@Menu varchar(100),
@Parent varchar(100),
@Name varchar(100),
@Class varchar(100),
@Area varchar(100),
@Controller varchar(200),
@Action varchar(100),
@Parameter varchar(1000),
@Url varchar(1000)
as
declare @MenuID bigint
declare @ParentID bigint
select @MenuID=ID from SysMenu where Name=@Menu
select @MenuID=IsNUll(@MenuID,0)
select @ParentID=ID from SysPrivilege where Name=@Parent
select @ParentID=IsNUll(@ParentID,0)
select @Parent=IsNUll(@Parent,'')
if @Parent!=''
begin
	if exists(select 1 from SysPrivilege where Name=@Parent)
	begin
	    select @ParentID=ID from SysPrivilege where Name=@Parent
	    if exists(select 1 from SysPrivilege where Name=@Name and Parent=@ParentID)
	    begin
	        return
	    end
	end
	else
	begin
	    insert into SysPrivilege(Menu,Parent,Name,Clazz,Area,Controller,Method,Parameter,Url,Creator,CreateTime)
	    values(@MenuID,@ParentID,@Parent,'','','','','','',0,getdate())
	    select @ParentID=@@IDENTITY
	end
end
if not exists(select 1 from SysPrivilege where Name=@Name and Menu=@MenuID and Parent=@ParentID)
begin
	insert into SysPrivilege(Menu,Parent,Name,Clazz,Area,Controller,Method,Parameter,Url,Creator,CreateTime)
		values(@MenuID,@ParentID,@Name,@Class,@Area,@Controller,@Action,@Parameter,@Url,0,getdate())
end
go

CREATE PROCEDURE UP_Login
@LoginName varchar(100),
@Password varchar(32),
@Session varchar(100),
@IP varchar(100)
as
if exists(select 1 from  SysOperator where LoginName = @LoginName and [Password]=@Password and Status=0)
begin
    update SysOperator set LoginCount = LoginCount +1 ,LoginTime=getdate(), LoginIP=@IP, Session=@Session
    where LoginName = @LoginName
    and [Password]=@Password
    and Status=0

    select * from SysOperator
    where LoginName = @LoginName
    and [Password]=@Password
    and Status=0
end
else
begin
    update SysOperator set LoginErrorCount = LoginErrorCount +1 ,LoginTime=getdate(), LoginIP=@IP
    where LoginName = @LoginName

    select * from SysOperator where 1=2
end
go

CREATE PROCEDURE UP_GetOperatorMenu
@Operator bigint
as
    select a.* from SysMenu a,SysRoleMenu b,SysRoleOperator c,SysOperator d
    where a.ID=b.Menu
    and b.Role=c.Role
    and c.Operator=d.ID
    and d.ID = @Operator
    and d.Status=0
go

CREATE PROCEDURE UP_GetOperatorPrivilege
@Operator bigint
as
    select a.* from SysPrivilege a,SysRolePrivilege b,SysRoleOperator c,SysOperator d
    where a.ID=b.Privilege
    and b.Role=c.Role
    and c.Operator=d.ID
    and d.ID = @Operator
    and d.Status=0
go

CREATE Procedure UP_CheckSSO
@Operator bigint,
@Session varchar(100)
as
Declare @result varchar(1000)
Declare @VirtualIntegral bigint
Declare @RealIntegral bigint
Declare @Balance decimal
Declare @FrozenBalance decimal
Declare @IncomingBalance decimal
Declare @Grade int
Declare @Star int
select @VirtualIntegral= VirtualIntegral,@RealIntegral = RealIntegral,@Balance=Balance,@FrozenBalance=FrozenBalance,@IncomingBalance=IncomingBalance,@Grade=Grade,@Star=Star
from SysOperator with(nolock)
where ID = @Operator
if exists(select 1 from SysOperator with(nolock) where ID = @Operator and Session = @Session)
begin
    select @result = 'true|' + convert(varchar(100),@VirtualIntegral) + '|' + convert(varchar(100),@RealIntegral) + '|' + convert(varchar(100),@Balance)+ '|' + convert(varchar(100),@FrozenBalance)+ '|' + convert(varchar(100),@IncomingBalance)+ '|' + convert(varchar(100),@Grade)+ '|' + convert(varchar(100),@Star)
end
else
begin
    select @result = 'false|' + convert(varchar(100),@VirtualIntegral) + '|' + convert(varchar(100),@RealIntegral) + '|' + convert(varchar(100),@Balance)+ '|' + convert(varchar(100),@FrozenBalance)+ '|' + convert(varchar(100),@IncomingBalance)+ '|' + convert(varchar(100),@Grade)+ '|' + convert(varchar(100),@Star)
end
select @result
go


--分页存储过程允许多列排序
Create PROCEDURE [dbo].[UP_ShowPageMultiSort]
@strGetFields varchar(MAX) = '*', --  Need to return the column, the default  *        
@tblName   varchar(MAX),       --  Table name          
@strWhere varchar(MAX) = '', --  Query criteria   ( Note  :  Do not add   where)        
@strOrder varchar(MAX)='',      --  Sort of a field name, required     like :  ID ASC  or    ID ASC,Age DESC        
@PageIndex int = 1,           --  The page number, the default  1        
@PageSize   int = 10          --  By default, the page size  10        
AS        
declare @strSQL   varchar(MAX)       
declare @TEST   varchar(MAX)    
if @strWhere !=''        
set @strWhere=' where '+@strWhere   
set @strSQL=     
cast('SELECT * FROM (' as varchar(max)) +        
        cast(' SELECT convert(bigint,ROW_NUMBER() OVER (ORDER BY ' as varchar(max)) + @strOrder+cast(' ' as varchar(max))+cast(')) AS RowNo,convert(bigint,COUNT(0) OVER ()) as RowCnt,* ' as varchar(max)) +  
        cast(' FROM (SELECT ' as varchar(max))+ @strGetFields + cast(' ' as varchar(max)) +        
        cast(' FROM ' as varchar(max)) + @tblName + cast(' ' as varchar(max)) + @strWhere+        
cast(') AS a) AS SP ' as varchar(max))  
if not(@PageIndex=0 and @PageSize=0)  
begin  
 set @strSQL= @strSQL   
  +cast('WHERE RowNo BETWEEN ' as varchar(max)) + convert(varchar(max),(@PageIndex-1)*@PageSize+1) + cast(' AND ' as varchar(max)) + convert(varchar(max),@PageIndex*@PageSize)        
end       
exec (@strSQL)  


GO




--alter table SysCorp
--alter column Mobile varchar(32)
--alter table SysCorp
--alter column IdCard varchar(32)

--alter table SysOperator
--alter column Mobile varchar(32)
--alter table SysOperator
--alter column IdCard varchar(32)

--GO
       
ALTER proc [dbo].[UP_AddCorp]              
   @CorpName varchar(100),              
   @CorpType bigint,    
   @Operator bigint,     
   @ParentCorp bigint,     
   @LoginName nvarchar(50),    
   @Password  varchar(50)      
as  
BEGIN    
  Set XACT_ABORT ON  
  set nocount off     
  declare @SysCorpID bigint     
  declare @SysOperatorID bigint   
  declare @SysRoleID bigint   
       
  declare @datetime nvarchar(50)=GETDATE()              
  declare  @IsSuccess  varchar(50)='true'         
  declare  @ProcMsg varchar(500) =''    
              
   --判断类型表是否存在相应的数据   
   if  exists(select 1 from SysCorp where Name=@CorpName)  
   begin  
		 set @IsSuccess='false'              
		 set @ProcMsg='已存在相同的公司名称'  
		 select @IsSuccess+'|' +@ProcMsg
		 return  
   end        
            
  --增加一条操作员数据              
  if  exists(select 1 from SysOperator where SysOperator.LoginName=@LoginName)              
  begin              
		set @IsSuccess='false'              
		set @ProcMsg='已存在相同的管理员账号'    
		select @IsSuccess+'|' +@ProcMsg          
		return              
  end              
                  
  begin try              
  begin tran globalTran              
      --如果不存在parent=0系统菜单--则生成系统数据              
          insert into SysCorp              
          (              
            Parent, Priority, Name,              
            BriefName, Certificate, CertificateNumber,              
            Ceo, Postcode, Faxcode,              
            Linkman, Mobile, Email,              
            Qq, Wechat, Weibo,              
            VirtualIntegral, RealIntegral, FeeType,              
            PrepayValve, Balance, FrozenBalance,              
            IncomingBalance, Commission, Discount,              
            Province, Area, County,              
            Community, Address, Status,              
            Type, Grade, Vocation,              
            Reserve, Remark, Creator,              
            CreateTime, Auditor, AuditTime,              
            Canceler, CancelTime              
          )              
          values              
          (              
            @ParentCorp, 0, @CorpName,              
            @CorpName, '', '',              
            '', '', '',              
            '', '', '',              
            '', '', '',              
            0, 0, 0,              
            0, 0, 0,              
            0, 0, 1,              
            '110000', '110000', '110000',              
            '110000', '', 0,              
            @CorpType, 0, 0,              
            '', '', @Operator,              
            @datetime, 0, GETDATE(),              
            null, null              
           );       
          select @SysCorpID=SCOPE_IDENTITY() from SysCorp    
                        
          insert into SysOperator              
            (              
              Corp, LoginName, RealName,              
              Password, Mobile, IdCard,              
              Email, WechatOpenid, AlipayOpenid,              
              Weibo,AvailableIP, WeatherCode,              
              VirtualIntegral,RealIntegral, Balance,              
              FrozenBalance, IncomingBalance, Commission,              
              Discount, Province, Area,              
              County, Community,Address,              
              Status, Skin, Grade,              
              Star,Session, LoginTime,              
              LoginIP, LoginAgent,LoginCount,              
              LoginErrorCount, FrozenStartTime,FrozenEndTime,              
              Reserve, Remark, Creator,              
              CreateTime,Auditor, AuditTime,              
              Canceler, CancelTime              
            )              
            values (              
                @SysCorpID, @LoginName, @CorpName+'系统管理员',              
                @Password,@datetime, @datetime,              
                '', '', '',              
                '', '', '110000',              
				0, 0, 0,              
                0, 0, 0,              
				1, '110000', '110000',              
                '110000', '110000', '',              
                0, 0, 0,              
                0, '', null,              
                null, null, 0,              
                0, null, null,              
                '', '', @Operator,              
                @datetime, 0, GETDATE(),              
                null, null              
              );      
          select @SysOperatorID=SCOPE_IDENTITY() from  SysOperator           
              
            --增加一条角色信息                        
                          
          insert into SysRole (              
              Corp, Name, Type,              
              Reserve, Remark, Creator,              
              CreateTime, Auditor, AuditTime,              
              Canceler, CancelTime              
           )              
          values (              
              @SysCorpID, 'administrators', 1,              
              null, null, @Operator,              
              @datetime, 0, GETDATE(),              
              null, null              
           );              
            
          select @SysRoleID=SCOPE_IDENTITY() from  SysRole   
          
          --添加角色操作员
          insert into SysRoleOperator(Role,Operator,Creator)
          values(@SysRoleID,@SysOperatorID,@Operator)
                     
          --添加菜单权限         
          insert into SysRoleMenu              
          (              
            Role,Menu,Reserve,              
            Remark,Creator,CreateTime,              
            Auditor,AuditTime,Canceler,              
            CancelTime              
          )  select              
          @SysRoleID,Menu,null,  
          null,@Operator,@datetime,  
          0,GETDATE(),null,  
          null from SysMenuTemplate where CorpType=@CorpType
          
          --添加操作权限
          insert into SysRolePrivilege
          (Role,Privilege,Creator)
          select             
          @SysRoleID,Privilege,@Operator from SysPrivilegeTemplate where CorpType=@CorpType
                   
  COMMIT TRANSACTION globalTran  
  end try  
  begin catch  
   set @IsSuccess=0  
   set @ProcMsg= ERROR_MESSAGE()  
   rollback tran globalTran            
  end catch 
  
   select @IsSuccess+'|'+@ProcMsg as Result                 
end 
go


create PROC [dbo].[UP_GetOperatorRole]
@operator BIGINT
AS

SELECT *FROM SysRole WHERE ID IN( SELECT Role FROM  SysRoleOperator WHERE Operator=@operator)
      
 go                   
create proc [dbo].[UP_AddPrivilegeTemplate]                   
   @CorpType bigint,                 
   @Privileges nvarchar(500),            
   @Creator bigint                
as     
  
BEGIN                    
  Set XACT_ABORT ON                    
  declare @IsSuccess  VARCHAR(50)='true'                     
  declare @ProcMsg varchar(max)='模板设置成功'    
  declare @sqlStr varchar(max)=''                     
  begin try   
    begin tran globalTran                
  if exists (select 1 from SysPrivilegeTemplate where CorpType=@CorpType)                 
  begin                    
   delete from SysPrivilegeTemplate where CorpType=@CorpType            
  end                    
    
  if @Privileges!=''  
  begin  
   set @sqlStr='insert into SysPrivilegeTemplate (CorpType,Privilege,Creator,CreateTime)   
     select '+cast(@CorpType as varchar(max))+',ID,'+cast(@Creator as varchar(max))+',getdate()   
     from SysPrivilege where ID in('+@Privileges+')'  
  end  
  print @sqlStr  
  exec(@sqlStr)  
    
 select @IsSuccess+'|'+@ProcMsg   
 COMMIT TRANSACTION globalTran    
  end try  
  begin catch  
     set @IsSuccess='false'  
     set @ProcMsg= ERROR_MESSAGE()   
     select @IsSuccess+'|'+@ProcMsg  as Result  
     rollback tran globalTran  
  end catch   
  select @IsSuccess+'|'+@ProcMsg  as Result      
end 
GO

create proc [dbo].[UP_AuditorCorp]                 
   @Corp bigint,     
   @Auditor bigint           
as                  
BEGIN                  
  declare @IsSuccess  varchar(50)='true'                     
  declare @ProcMsg varchar(500)='审核成功'                   
  begin try    
		update SysCorp set Auditor=@Auditor,AuditTime=GETDATE() where ID=@Corp   
		select @IsSuccess+'|'+  @ProcMsg as Result        
  end try                  
  begin catch                  
	   set @IsSuccess='false'                  
	   set @ProcMsg= ERROR_MESSAGE()                  
	   select @IsSuccess+'|'+  @ProcMsg as Result                  
  end catch     
  select @IsSuccess+'|'+  @ProcMsg as Result              
end   
  

  GO

      
create proc [dbo].[UP_AuditorPrivilegeTemplate]               
   @CorpType bigint,             
   @Privileges nvarchar(max),        
   @Auditor bigint               
as          
BEGIN                
  Set XACT_ABORT ON                
              
  declare  @IsSuccess  varchar(50)='true'  
  declare   @ProcMsg varchar(500)='权限模板成功'       
  declare @strSql varchar(max)=''
  begin try                
  begin tran globalTran                
			set @strSql	='update SysPrivilegeTemplate  
				set Auditor='+cast(@Auditor as varchar(max))+',AuditTime=getdate()  
				where CorpType='+cast(@CorpType as varchar(max))+' and Privilege in ( '+@Privileges+' )' 
			print @strSql
			exec(@strSql)	
			
  select @IsSuccess+'|'+@ProcMsg as Result    
  COMMIT TRANSACTION globalTran                
  end try                
  begin catch                
		   set @IsSuccess='false'                
		   SET @ProcMsg= ERROR_MESSAGE()  
		   rollback tran globalTran
		   select @IsSuccess+'|'+@ProcMsg as Result  
  end catch  
  select @IsSuccess+'|'+@ProcMsg as Result           
end 

GO

create proc [dbo].[UP_CancelCorp]               
   @Corp bigint,   
   @Canceler bigint         
as                
BEGIN                
  Set XACT_ABORT ON                
  set nocount off                
  begin try 
  begin tran globalTran  
		declare @IsSuccess varchar(max)='true'          
		declare @ProcMsg varchar(max)='公司注销成功'                        
		update SysCorp set Canceler=@Canceler,CancelTime=getdate() where ID=@Corp     
		select @IsSuccess+'|'+@ProcMsg as Result
  COMMIT TRANSACTION globalTran
  end try                
  begin catch                
	   set @IsSuccess=0                
	   SET @ProcMsg= ERROR_MESSAGE() 
	   select @IsSuccess+'|'+@ProcMsg as Result    
	   rollback tran globalTran           
  end catch       
  select @IsSuccess+'|'+@ProcMsg as Result         
end 

GO



    
create proc [dbo].[UP_EnableCorp]               
   @Corp bigint,   
   @Operator bigint      
as                
BEGIN                
  Set XACT_ABORT ON                
  set nocount off                
  begin try  
	begin tran globalTran
		declare @IsSuccess nvarchar(max)='true'          
		declare @ProcMsg nvarchar(max)='公司启用成功'                        
		update SysCorp set Auditor=@Operator,AuditTime=GETDATE(),Canceler='',CancelTime='' where ID=@Corp
		select @IsSuccess+'|'+@ProcMsg as Result 
	COMMIT TRANSACTION globalTran         
  end try                
  begin catch                
	   set @IsSuccess=0                
	   set @ProcMsg= ERROR_MESSAGE() 
	   select @IsSuccess+'|'+@ProcMsg as Result                 
	   rollback tran  globalTran               
  end catch  
  select @IsSuccess+'|'+@ProcMsg as Result              
end 
GO



ALTER PROCEDURE [dbo].[UP_ShowArea]
@PageIndex int = 1,
@PageSize int = 10,
@strOrder varchar(MAX) = '',
@strOrderType varchar(max) = 'ASC'
as
select * from(
    select convert(bigint, ROW_NUMBER() OVER(order by code asc)) as RowNo,
    convert(bigint, COUNT(0) OVER()) as RowCnt, *
    FROM(
        SELECT
        Parent,
        Code,
        Name,
        LocationX,
        LocationY,
        Reserve,
        Remark,
        CreateTime FROM SysArea
    ) AS a
)
AS temp WHERE RowNo BETWEEN (@PageIndex-1)*@PageSize+1 AND @PageIndex*@PageSize

GO


ALTER proc [dbo].[UP_ShowCity]  
@Province char(6)       
as         
      if @Province=''
      begin
		  --市级    
		  select [Code]    
		  ,[Parent]    
		  ,[Name]    
		  ,[WeatherCode]    
		  ,[LocationX]    
		  ,[LocationY]    
		  ,[Reserve]    
		  ,[Remark]    
		  from SysArea where [Parent] in     
		  (    
				select [Code] from SysArea where Code=Parent    
		  ) and Code!=Parent 
      end
      else 
      begin
			--市级    
		  select [Code]    
		  ,[Parent]    
		  ,[Name]    
		  ,[WeatherCode]    
		  ,[LocationX]    
		  ,[LocationY]    
		  ,[Reserve]    
		  ,[Remark]    
		  from SysArea where [Parent] in     
		  (    
				select [Code] from SysArea where Code=Parent    
		  ) and Code!=Parent and Parent=@Province
      
      end
         
     GO

 Create PROCEDURE [UP_ShowCorp]      
@PageIndex int = 1,      
@PageSize int = 10,   
@WhereStr nvarchar(200)='',   
@strOrder varchar(MAX) = '',      
@strOrderType varchar(max) = 'ASC'      
as      
DECLARE @sqlstr VARCHAR(max) 
SET @sqlstr='
SELECT * from(      
    select convert(bigint, ROW_NUMBER() OVER(order by Name asc)) as RowNo,      
    convert(bigint, COUNT(0) OVER()) as RowCnt, *      
    FROM(      
         SELECT      
    a.[ID] ,b.Name as [Parent] ,a.[Priority] ,      
    a.[Name] ,a.LogoIcon,a.LogoUrl,a.[BriefName] ,a.[Certificate] ,      
    a.[CertificateNumber] ,a.[Ceo] ,a.[Postcode] ,      
    a.[Faxcode] ,a.[Linkman] ,a.[Mobile] ,      
    a.[Email] ,a.[Qq] ,a.[Wechat] ,      
    a.[Weibo] ,a.[VirtualIntegral] ,a.[RealIntegral] ,      
    c.[Name] as  FeeType,a.[PrepayValve] ,a.[Balance] ,      
    a.[FrozenBalance] ,a.[IncomingBalance] ,      
    a.[Commission] ,a.[Discount] ,j.Name as [Province] ,      
    k.Name as [Area] ,l.Name as [County] ,a.[Community] ,      
    a.[Address] ,d.Name as [Status] ,e.Name as [Type] ,      
    a.[Grade] ,a.[Vocation] ,a.[Reserve] ,      
    a.[Remark] ,g.RealName as [Creator] ,a.[CreateTime] ,      
    h.RealName as [Auditor] ,a.[AuditTime] ,i.RealName [Canceler] ,      
    a.[CancelTime]      
    FROM SysCorp  a      
         left join SysCorp b on a.Parent=b.ID      
         left join SysCorpFeeType c on a.FeeType=c.ID      
         left join SysCorpStatus d on a.Status=d.ID      
         left join SysCorpType e on a.Type=e.ID      
         left join SysCorpVocation f on a.Vocation =f.ID      
         left join SysOperator g on a.Creator=g.ID      
         left join SysOperator h on a.Auditor=h.ID      
         left join SysOperator i on a.Canceler=i.ID      
         left join SysArea j on a.Province=j.Code      
         left join SysArea k on a.Area = k.Code      
         left join SysArea l on a.County =l.Code      
         where a.ID !=0  '
         	IF @WhereStr<>''
			BEGIN
				SET @sqlstr+='   AND  '+ @WhereStr
			END  
			SET @sqlstr+='
    ) AS a 
)      
AS temp   
WHERE RowNo BETWEEN '+CONVERT(VARCHAR, (@PageIndex-1)*@PageSize+1) +' AND ' +CONVERT (VARCHAR,@PageIndex*@PageSize) 

exec(@sqlstr)
GO

CREATE proc [dbo].[UP_ShowCounty]  
@City char(6)      
as         
	if @City=''
	begin
		 --县级    
		 select [Code]    
		  ,[Parent]    
		  ,[Name]    
		  ,[WeatherCode]    
		  ,[LocationX]    
		  ,[LocationY]    
		  ,[Reserve]    
		  ,[Remark]    
		  from SysArea where [Parent] in(    
				  select [Code]    
				  from SysArea where [Parent] in     
				  (    
					 select [Code] from SysArea where Code=Parent    
				  ) and Code!=Parent
		  ) 
	end
	else 
	begin
	 --县级    
		 select [Code]    
		  ,[Parent]    
		  ,[Name]    
		  ,[WeatherCode]    
		  ,[LocationX]    
		  ,[LocationY]    
		  ,[Reserve]    
		  ,[Remark]    
		  from SysArea where [Parent] in(    
				  select [Code]    
				  from SysArea where [Parent] in     
				  (    
					 select [Code] from SysArea where Code=Parent    
				  ) and Code!=Parent
		  ) and Parent=@City
	end
	
	GO

	
  
create PROCEDURE [dbo].[UP_ShowPrivilegeTemplate] 
@PageIndex int = 1,  
@PageSize int = 10,
@WhereStr nvarchar(200)='',  
@strOrder varchar(MAX) = '',  
@strOrderType varchar(max) = 'ASC'  
as  
select * from(  
    select convert(bigint, ROW_NUMBER() OVER(order by CreateTime desc)) as RowNo,  
    convert(bigint, COUNT(0) OVER()) as RowCnt, *  
    FROM(  
		SELECT [ID]
			  ,[CorpType]
			  ,[Privilege]
			  ,[Reserve]
			  ,[Remark]
			  ,[Creator]
			  ,[CreateTime]
			  ,[Auditor]
			  ,[AuditTime]
			  ,[Canceler]
			  ,[CancelTime]
		  FROM [USP].[dbo].[SysPrivilegeTemplate]
    ) AS a where a.CorpType like '%'+@WhereStr+'%'
)  
AS temp WHERE RowNo BETWEEN (@PageIndex-1)*@PageSize+1 AND @PageIndex*@PageSize  
GO



CREATE proc [dbo].[UP_ShowProvince]      
as       
 --省级  
 select [Code]  
      ,[Parent]  
      ,[Name]  
      ,[WeatherCode]  
      ,[LocationX]  
      ,[LocationY]  
      ,[Reserve]  
      ,[Remark]   
      from SysArea  where Code=Parent    
go


---------------新增脚本(ZhouShuaiJie)------------------

--根据角色获取菜单
CREATE PROCEDURE UP_GetRoleMenu
@Role bigint
as
    select a.* from SysMenu a,SysRoleMenu b
    where a.ID=b.Menu
    and b.Role=@Role
go


--根据角色获取权限
CREATE PROCEDURE UP_GetRolePrivilege
@Role bigint
as
    select a.* from SysPrivilege a,SysRolePrivilege b
    where a.ID=b.Privilege
    and b.Role=@Role
go


--添加角色
CREATE PROCEDURE UP_AddRole
@Corp bigint,
@Name varchar(100),
@Type bit,
@Remark varchar(250),
@Creator bigint,
@Menus varchar(max),
@Privileges varchar(max)
as
declare @RoleID bigint 
if exists(select 1 from SysRole where Name=@Name and Corp=@Corp)
begin	
	set @RoleID=-1
	select @RoleID
end
else 
begin	
	begin tran	tranAddRole
	begin try 
	
	--添加角色
	insert into SysRole(Corp,Name,Type,Remark,Creator,Auditor) values(@Corp,@Name,@Type,@Remark,@Creator,@Creator)
	select @RoleID=IDENT_CURRENT('SysRole')
	
	--添加菜单和权限
	declare @sqlmenu  varchar(max),@sqlprivilege varchar(max)
	set @sqlmenu='insert into SysRoleMenu(Role,Menu,Creator,Auditor) select '+cast(@RoleID as varchar(max))+',ID,'+cast(@Creator as varchar(max))+','+cast(@Creator as varchar(max))+' from SysMenu where ID in ('+@Menus+')'
	exec (@sqlmenu)
	
	set @sqlprivilege='insert into SysRolePrivilege(Role,Privilege,Creator,Auditor) select '+cast(@RoleID as varchar(max))+',ID,'+cast(@Creator as varchar(max))+','+cast(@Creator as varchar(max))+' from SysPrivilege where ID in ('+@Privileges+')'
	exec (@sqlprivilege)
	
	commit tran tranAddRole --没有异常，提交事务
    select @RoleID
	
	end try
	
	begin catch
		rollback tran tranAddRole
		set @RoleID=-1
		select @RoleID
	end catch 
end


GO

--修改角色
CREATE PROCEDURE UP_EditRole
@RoleID bigint,
@Name varchar(100),
@Remark varchar(250),
@Creator bigint,
@Menus varchar(max),
@Privileges varchar(max)
as

declare @Result bigint 

--角色不存在返回
if not exists(select 1 from SysRole where ID=@RoleID)
begin
	set @Result=-1
	select @Result
end
else 
begin

	begin tran tranEditRole
	
	begin try 
	update SysRole set Name=@Name,Remark=@Remark,Creator=@Creator,CreateTime=getdate() where ID=@RoleID

	--菜单处理
	if @Menus is null
	begin
	delete from SysRoleMenu where Role=@RoleID
	end
	else
	begin
	declare @sqlmenudelete varchar(max),@sqlmenuadd varchar(max)
	set @sqlmenudelete='delete from SysRoleMenu where Role='+cast(@RoleID as varchar(max))+' and Menu not in ('+@Menus+')'
	exec (@sqlmenudelete)

	set @sqlmenuadd='insert into SysRoleMenu(Role,Menu,Creator,Auditor) (select '+cast(@RoleID as varchar(max))+',ID,'+cast(@Creator as varchar(max))+','+cast(@Creator as varchar(max))+' from SysMenu where ID in ('+@Menus+') and ID not in (select menu from SysRoleMenu where Role='+cast(@RoleID as varchar(max))+'))'
	exec (@sqlmenuadd)
	end

	--权限处理
	if @Privileges is null
	begin
	delete from SysRolePrivilege where Role=@RoleID
	end
	else
	begin
	declare @sqlprivilegedelete varchar(max),@sqlprivilegeadd varchar(max)
	set @sqlprivilegedelete='delete from SysRolePrivilege where Role='+cast(@RoleID as varchar(max))+' and Privilege not in ('+@Privileges+')'
	exec (@sqlprivilegedelete)

	set @sqlprivilegeadd='insert into SysRolePrivilege(Role,Privilege,Creator,Auditor) (select '+cast(@RoleID as varchar(max))+',ID,'+cast(@Creator as varchar(max))+','+cast(@Creator as varchar(max))+' from SysPrivilege where ID in ('+@Privileges+') and ID not in (select Privilege from SysRolePrivilege where Role='+cast(@RoleID as varchar(max))+'))'
	exec (@sqlprivilegeadd)
	end

	commit tran tranEditRole --没有异常，提交事务
	set @Result=1
	select @Result
	
	end try
	
	begin catch
		rollback tran tranEditRole --执行出错，回滚事务
		set @Result=-1
        select @Result
	end catch 	
end

GO

--根据用户获取角色
CREATE PROCEDURE UP_GetRoleByOperator
@Operator bigint
as
    select a.* from SysRole a,SysRoleOperator b,SysOperator c
    where a.ID=b.Role
    and b.Operator=c.ID
    and c.ID = @Operator
    and c.Status=0
go


--标记删除权限
CREATE PROCEDURE UP_DeletPrivilege
@Id bigint,
@Creator bigint
as

declare @result bigint

if not exists(select 1 from SysPrivilege where ID=@Id)
begin
	set @result=-1
	select @result
end
else 
begin
	begin tran tranDeletePrivilege
	
	begin try 
	
	--标记删除权限
	update SysPrivilege set Canceler=@Creator,CancelTime=GETDATE() where ID=@Id
	
	--删除权限模板中的数据
	delete from SysPrivilegeTemplate where Privilege=@Id
	
	--删除角色权限中数据	
	delete from SysRolePrivilege where Privilege=@Id
	
	commit tran tranDeletePrivilege--没有异常，提交事务
    set @result=1
    select @result
	end try
	
	begin catch
		rollback tran tranDeletePrivilege
		set @result=-1
		select @result
	end catch 
end


GO

create procedure UP_AddMenus

	@Parent varchar(100),
	@Name varchar(100),
	@Icon varchar(100),
	@Class varchar(100),
	@Area varchar(100),
	@Controller varchar(200),
	@Action varchar(100),
	@Parameter varchar(1000),
	@Url varchar(max),
	@Creator bigint
as
	declare @result varchar(1000)
	set @Icon = ISNULL(@Icon,'')
	set @Class=ISNULL(@Class,'')
	set @Area=ISNULL(@Area,'')
	set @Controller=ISNULL(@Controller,'')
	set @Action=ISNULL(@Action,'')
	set @Parameter=ISNULL(@Action,'')
	set @Url=ISNULL(@Url,'')
	
	if(@Parent=0)
	begin
		if exists(select 1 from SysMenu where name=@Name and Parent=0)
		begin
			 select @result = 'false|菜单名称已存在' 
		end
		else
		begin
		     begin try
			 insert into SysMenu(Direction, Parent,Name, Icon, Clazz, Area, Controller, Method, Parameter, Url, Creator,createTime) values(0,0,@Name,@Icon,'','','','','','',@Creator,getdate())  
		     select @result='true|添加成功'   
		     end try
		     begin catch
		     select @result='false|添加失败'   
		     end catch
		end
	end
	else
	begin
		if exists(select 1 from SysMenu where name=@Name and Parent!=0)
		begin
			select @result = 'false|菜单名称已存在'  
		end
		else
		begin
		    begin try
			insert into SysMenu(Direction, Parent,Name, Icon, Clazz, Area, Controller,Method, Parameter, Url, Creator,createTime) values(0,@Parent,@Name,@Icon,@Class,@Area,@Controller,@Action,@Parameter,@Url,@Creator,getdate())
		    select @result='true|添加成功'   
		    end try
		    begin catch
		    select @result='false|添加失败'   
		    end catch
		end
	end
	
	select @result
GO

create procedure UP_EditMenus
	@Id bigint,
	@Parent varchar(50),
	@Name varchar(100),
	@Icon varchar(100),
	@Class varchar(100),
	@Area varchar(100),
	@Controller varchar(200),
	@Action varchar(100),
	@Parameter varchar(1000),
	@Url varchar(max)
as
	declare @result varchar(500)
	set @Icon = ISNULL(@Icon,'')
	set @Class=ISNULL(@Class,'')
	set @Area=ISNULL(@Area,'')
	set @Controller=ISNULL(@Controller,'')
	set @Action=ISNULL(@Action,'')
	set @Parameter=ISNULL(@Action,'')
	set @Url=ISNULL(@Url,'')
	if(@Parent=0)
	begin
		if exists(select 1 from SysMenu where name=@Name and Parent=0 and ID!=@Id)
		begin
			select @result = 'false|菜单名称已存在' 
		end
		else
		begin
		    begin try
			update SysMenu set Parent=@Parent, Name=@Name,Icon=@Icon,AuditTime=null,Auditor=null where ID=@Id
			
			select @result='true|编辑成功'
		    end try
		    begin catch
		    select @result='false|编辑失败'
		    end catch
		end
	end
	else
	begin
		if exists(select 1 from SysMenu where name=@Name and Parent!=0 and ID!=@Id)
		begin
			select @result = 'false|菜单名称已存在'
		end	
		else
		begin
		    begin try
			update SysMenu set Parent=@Parent, Name=@Name,Icon=@Icon,Clazz=@Class,Area=@Area,Controller=@Controller,Method=@Action,Parameter=@Parameter,Url=@Url,AuditTime=null,Auditor=null  where ID=@Id
		    select @result='true|编辑成功'
		    end try
		    begin catch
		    select @result='false|编辑失败'
		    end catch
		    
		end		 
	end
	
	select @result
	
	
	go

create procedure UP_CancelMenu
@Id bigint,
@Canceler bigint    
as
begin
    begin try
       begin tran
       if((select COUNT(1) from SysMenu where Parent=@Id)<=0)
		   begin
					update SysMenu set CancelTime=getdate(),Canceler=cast(@Canceler as varchar(50)) where Id in(cast(@Id as varchar(50)))
           			update  SysPrivilege  set CancelTime=getdate(),Canceler=@Canceler where Menu=@Id	
					update  SysRoleMenu set CancelTime=getdate(),Canceler=@Canceler where Menu=@Id
					update  SysMenuTemplate set CancelTime=getdate(),Canceler=@Canceler where Menu=@Id
		  end
		  else 
		  begin
			   update SysMenu set CancelTime=getdate(),Canceler=cast(@Canceler as varchar(50)) where Id in(cast(@Id as varchar(50)))
			   update  SysMenu set CancelTime=getdate(),Canceler=@Canceler where ID in(select ID from SysMenu where Parent=@Id)
			   update  SysPrivilege  set CancelTime=getdate(),Canceler=@Id where Menu=@Id	
			   update  SysRoleMenu set CancelTime=getdate(),Canceler=@Canceler where Menu=@Id
			   update  SysMenuTemplate set CancelTime=getdate(),Canceler=@Canceler where Menu=@Id
			   update  SysPrivilege  set CancelTime=getdate(),Canceler=@Canceler where Menu in(select ID from SysMenu where Parent=@Id)	
			   update  SysRoleMenu set CancelTime=getdate(),Canceler=@Canceler where Menu in(select ID from SysMenu where Parent=@Id)	
			   update  SysMenuTemplate set CancelTime=getdate(),Canceler=@Canceler where Menu in(select ID from SysMenu where Parent=@Id)	
		  end 

      commit tran 
    end try
    begin catch
        if @@TRANCOUNT> 0
        begin
            rollback tran 
        end 
        exec UP_CancelMenu @Id,@Canceler --执行存储过程将错误信息记录在表当中
    end catch
end
go



--获取菜单模板列表
create procedure UP_GetMenuTemplate 
@id bigint
as
begin
select a.ID,a.CorpType,b.Name as CorpTypeName,a.Menu,SysMenu.Name as MenuName,a.Creator,a.CreateTime,a.Auditor,a.AuditTime,a.Canceler,a.CancelTime  
from SysMenuTemplate  as a
left join   SysCorpType as b  on  a.CorpType=b.ID
left join  SysMenu on  a.Menu=SysMenu.ID
where  b.ID=@id
end
go

--保存菜单模板
create procedure UP_SaveMenuTemplate
@corpType bigint,
@creator bigint,
@menus varchar(max)
as
declare @sql varchar(1000)
begin
	begin try
		begin tran 
			    delete from SysMenuTemplate where CorpType=@corpType
                set @sql='insert into SysMenuTemplate(CorpType,Creator,CreateTime,Menu) select '+CAST(@corpType as varchar(10))+','+CAST(@creator as varchar(100))+',GETDATE() ,ID from SysMenu where ID in('+@menus+')'
                exec (@sql)
		commit tran 
	end try
	begin catch
		if(@@ERROR<>0)
			Begin
				rollback transaction
			End
	end catch
end
go

--审核菜单模板
create procedure UP_AuditMenuTemplate
@corpType bigint,
@auditor bigint,
@menus varchar(max)
as
declare @strSlq varchar(500)
begin  
	set @strSlq='update SysMenuTemplate set Auditor='+CAST(@auditor as varchar(20))+',AuditTime=GETDATE() where CorpType='+CAST(@corpType as varchar(20))+' and Menu in('+CAST(@menus as varchar(max))+')'
	exec(@strSlq)
end
go	


CREATE PROCEDURE [dbo].[UP_AddOperator]
@Corp bigint,
@LoginName varchar(100),
@RealName varchar(100),
@Password varchar(100),
@IdCard varchar(100),
@Email varchar(100),
@Mobile varchar(100),
@Creator bigint,
@Role varchar(max)
as

if exists(select 1 from SysOperator where LoginName=@LoginName and Corp=@Corp)
begin
	select null
end
else 
begin
	declare @tran_error int;
	set @tran_error = 0;
	begin tran
	
	   declare @OperatorID bigint 
	
	   begin try 

	     insert into SysOperator(
			Corp,
			LoginName,
			RealName,
			Password,
			IdCard,
			Email,
			Mobile,
			Creator,		
			Province,
			Area,
			County,
			Community,
			CreateTime) 
	    values(
			 @Corp, 
			 @LoginName, 
			 @RealName,
			 @Password,
			 @IdCard,
			 @Email,
             @Mobile,
			 @Creator,
			 0,
			 0,
			 0,
			 0,
	         getdate())
	    select @OperatorID=@@IDENTITY
	
	    --insert into SysRoleOperator(Role,Operator,Creator,CreateTime) values(@Role,@OperatorID,@Creator,getdate())
	    
	    declare @addRoleOpeartor varchar(max)
	    
	    set @addRoleOpeartor='insert into SysRoleOperator(Role,Operator,Creator,CreateTime) select ID,'+cast(@OperatorID as varchar(max))+','+cast(@Creator as varchar(max))+',GETDATE() from SysRole where ID in ('+@Role+')'

        exec (@addRoleOpeartor)

	  end try
	
	  begin catch
	    set @tran_error = @tran_error + 1
	    print ERROR_MESSAGE()  
	  end catch 
	
	  if(@tran_error > 0)
        begin        
			rollback tran;
			select -1
        end
	  else
		begin        
			commit tran;
			select @OperatorID
		end	
end
GO

CREATE PROCEDURE [dbo].[UP_AlterStatus]
@Status bigint,
@AlterOperater bigint,
@ID bigint,
@type int--type=0,激活；type=2，注销；type=3，审核；
as
declare @OperatorID bigint 

begin
	declare @tran_error int;
	set @tran_error = 0;
	begin tran
	
	begin try 
	
    if(@type=3)
    begin
    UPDATE SysOperator SET Status = @Status, Auditor = @AlterOperater,AuditTime=getdate()
    WHERE ID = @ID	
    end
    
    if(@type=0)
    begin
    UPDATE SysOperator SET Status = @Status
    WHERE ID = @ID	
    end
    
    if(@type=2)
    begin
    UPDATE SysOperator SET Status = @Status, Canceler = @AlterOperater,CancelTime=getdate()
    WHERE ID = @ID	
    end
    
	select @OperatorID=@ID
	
	end try
	
	begin catch
	set @tran_error = @tran_error + 1
	select -1
	end catch 
	
	if(@tran_error > 0)
    begin        
        rollback tran;
        select -1
    end
	else
    begin        
        commit tran;
        select @OperatorID
    end	
end
GO

CREATE PROCEDURE [dbo].[UP_AlterPassword]
@opid bigint,
@newPassword varchar(100)
as

begin
	declare @tran_error int;
	set @tran_error = 0;
	begin tran
	
	begin try 
	
	update SysOperator set Password=@newPassword where ID=@opid
	
	end try
	
	begin catch
	set @tran_error = @tran_error + 1
	end catch 
	
	if(@tran_error > 0)
    begin        
        rollback tran;
		set @opid=-1
        select @opid
    end
	else
    begin        
        commit tran;
        select @opid
    end	
end
GO



CREATE  proc UP_EditOperator
@id BIGINT,
@loginName VARCHAR(max),
@RealName VARCHAR(max),
@Creator BIGINT,
@Role VARCHAR(max),
@Mobile varchar(max),
@IdCard varchar(max),
@Email varchar(max)
AS
DECLARE @sqlstr VARCHAR(MAX)
declare  @IsSuccess  varchar(50)='true'         
declare  @ProcMsg varchar(500) ='修改成功'    
--用户不存在返回
if not exists(select 1 from dbo.SysOperator where ID=@id)
begin
		 SET @IsSuccess='false'              
		 set @ProcMsg='不存在对应的员工'  
		 select @IsSuccess+'|' +@ProcMsg
		 return  
END
	begin tran tranEditRole
	
	begin try 

			DELETE FROM SysRoleOperator WHERE Operator=@id

			UPDATE  SysOperator 
			SET loginName= @loginName,
			RealName =@RealName,
			Creator=@Creator,
			Mobile=@Mobile,
			IdCard =@IdCard,
			Email=@Email
			WHERE ID=@id
			SET @sqlstr='
			 INSERT INTO SysRoleOperator
			 (Role,Operator,creator,CreateTime,Auditor,AuditTime)
			 SELECT ID,'+CONVERT(VARCHAR,@id)+','+CONVERT(VARCHAR,@Creator)+',GETDATE(),'+CONVERT(VARCHAR,@Creator)+',GETDATE() FROM SysRole 
			 WHERE ID IN ('+@Role+')
			 '
		EXEC(@sqlstr)
	commit tran tranEditRole --没有异常，提交事务
	end try
	
	begin CATCH
		SET  @IsSuccess='false' 
		SET  @ProcMsg= ERROR_MESSAGE() 
		rollback tran tranEditRole --执行出错，回滚事务
	end catch 	

	SELECT @IsSuccess+'|'+@ProcMsg as Result

	go

	
create  PROC UP_ShowOperatorInfo
@Server VARCHAR(max)='10.1.1.50',
@DataBase VARCHAR(max)='20160418',
@UID VARCHAR(max)='XXB',
@PWD VARCHAR(max)='xinxibu',
@PageIndex int = 1,  
@PageSize int = 10,
@WhereStr nvarchar(200)='',  
@strOrder varchar(MAX) = '',  
@strOrderType varchar(max) = 'ASC'  
AS

DECLARE @sqlstr VARCHAR(max)


EXEC sp_droplinkedsrvlogin K3Cloud,Null  
Exec sp_dropserver K3Cloud  
EXEC  sp_addlinkedserver  
      @server='K3Cloud',--被访问的服务器别名   
      @srvproduct='',  
      @provider='SQLOLEDB',  
      @datasrc=@Server   --要访问的服务器  
EXEC sp_addlinkedsrvlogin   
     'K3Cloud', --被访问的服务器别名  
     'false',   
     NULL,   
     @UID, --帐号  
     @PWD --密码  
     
     SET @sqlstr='
select * from(  
    select convert(bigint, ROW_NUMBER() OVER(order by CreateTime desc)) as RowNo,  
    convert(bigint, COUNT(0) OVER()) as RowCnt, *  
    FROM(  
		SELECT a.[ID]
			  ,a.[Corp]
			  ,a.[LoginName]
			  ,a.[RealName]
			  ,a.[Password]
			  ,a.[Mobile]
			  ,a.[IdCard]
			  ,a.[Email]
			  ,a.[WechatOpenid]
			  ,a.[AlipayOpenid]
			  ,a.[Weibo]
			  ,a.[AvailableIP]
			  ,a.[WeatherCode]
			  ,a.[VirtualIntegral]
			  ,a.[RealIntegral]
			  ,a.[Balance]
			  ,a.[FrozenBalance]
			  ,a.[IncomingBalance]
			  ,a.[Commission]
			  ,a.[Discount]
			  ,a.[Province]
			  ,a.[Area]
			  ,a.[County]
			  ,a.[Community]
			  ,a.[Address]
			  ,a.[Status]
			  ,a.[Skin]
			  ,a.[Grade]
			  ,a.[Star]
			  ,a.[Session]
			  ,a.[LoginTime]
			  ,a.[LoginIP]
			  ,a.[LoginAgent]
			  ,a.[LoginCount]
			  ,a.[LoginErrorCount]
			  ,a.[FrozenStartTime]
			  ,a.[FrozenEndTime]
			  ,a.[Reserve]
			  ,a.[Remark]
			  ,a.[Creator]
			  ,a.[CreateTime]
			  ,a.[Auditor]
			  ,a.[AuditTime]
			  ,a.[Canceler]
			  ,a.[CancelTime]
			  ,b.Supplier
			  ,d.Fname as SupplierName
		  FROM [SysOperator] a
		  LEFT JOIN  SysOperatorSupplier b ON a.id=b.Operator
		  LEFT JOIN  K3Cloud.['+@DataBase+'].dbo.T_BD_SUPPLIER c ON b.Supplier=c.FSUPPLIERID
		  LEFT JOIN K3Cloud.['+@DataBase+'].dbo.T_BD_SUPPLIER_L d ON c.FSUPPLIERID=d.FSUPPLIERID
		  ) AS a where 1=1'
		   IF @WhereStr<>''
			BEGIN
				SET @sqlstr+= @WhereStr
			END  
			SET @sqlstr+='
)  
AS temp WHERE RowNo  BETWEEN '+CONVERT(VARCHAR, (@PageIndex-1)*@PageSize+1)+' AND '+CONVERT (VARCHAR,@PageIndex*@PageSize)


EXEC(@sqlstr)
--SELECT *,1 AS RowCnt, 1 AS RowNo,'' as Supplier ,'' AS SupplierName FROM [SysOperator]
go