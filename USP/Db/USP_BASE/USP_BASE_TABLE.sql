
/*
source /Users/baogui/Documents/newWorkspace/openPlatform/schema/db_table.sql;
*/
/*
地市(区)表
*/
CREATE TABLE [dbo].[Area](
	[ID] [bigint] NOT NULL,
	[Code] [varchar](20) NULL,
	[Parent] [varchar](20) NULL,
	[Name] [varchar](100) NULL,
	[Type] [int] NOT NULL
)
create table SysArea
(
	Code char(6) not null,--区域代码
	Parent char(6) not null,--父区域
	Name varchar(100) not null,--名称
	WeatherCode varchar(6),--天气城市编码
	LocationX decimal default(0),--地理位置维度
	LocationY decimal default(0),--地理位置经度
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint,--注销人
	CancelTime datetime,--注销时间
	CONSTRAINT PK_SysArea_Code primary key(Code),
	CONSTRAINT FK_SysArea_Parent foreign key(Parent) references SysArea(Code)
)
go
/*
公司行业
*/
create table SysCorpVocation
(
	ID bigint not null identity(0,1),--唯一标识
	Parent bigint not null,--父ID
	Name varchar(100) not null,--名字
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint,--注销人
	CancelTime datetime,--注销时间
	CONSTRAINT PK_SysCorpVocation_ID primary key(ID),
	CONSTRAINT UK_SysCorpVocation unique(Parent,Name)
)
go
/*
公司状态说明表
*/
create table SysCorpStatus(
	ID bigint not null identity(0,1),--唯一标识
	Name varchar(100) not null,--名字
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint,--注销人
	CancelTime datetime,--注销时间
	CONSTRAINT PK_SysCorpStatus_ID primary key(ID),
	CONSTRAINT UK_SysCorpStatus_Name unique(Name)
)
go

/*
公司类别
*/
create table SysCorpType
(
	ID bigint not null identity(0,1),--唯一标识
	Name varchar(100) not null,--名字
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint,--注销人
	CancelTime datetime,--注销时间
	CONSTRAINT PK_SysCorpType_ID primary key(ID),
	CONSTRAINT UK_SysCorpType_Name unique(Name)
)
go

/*
公司级别
*/
create table SysCorpGrade
(
	ID bigint not null identity(0,1),--唯一标识
	NAme varchar(100) not null,--名字
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint,--注销人
	CancelTime datetime,--注销时间
	CONSTRAINT PK_SysCorpGrade_ID primary key(ID),
	CONSTRAINT UK_SysCorpGrade_Name unique(Name)
)
go

/*
公司付费类型
*/
create table SysCorpFeeType
(
	ID bigint not null identity(0,1),--唯一标识
	Name varchar(100) not null,--名字
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint,--注销人
	CancelTime datetime,--注销时间
	CONSTRAINT PK_SysCorpFeeType_ID primary key(ID),
	CONSTRAINT UK_SysCorpFeeType_Name unique(Name)
)
go

/*
公司信息表
*/
create table SysCorp
(
	ID bigint not null identity(0,1),--唯一标识
	Parent bigint not null,--父
	Priority tinyint not null,--公司优先级(0-9)
	Name varchar(50) not null default(''),--公司名称
	BriefName varchar(50) not null default(''),--公司简称
	Certificate varchar(50) not null default(''),--证书名称
	CertificateNumber varchar(50) not null default(''),--证书号码
	Ceo varchar(100) not null default(''),--法人代表
	Postcode varchar(6) not null default(''),--邮编
	Faxcode varchar(32) not null default(''),--传真号
	Linkman varchar(50) not null default(''),--联系人
	Mobile varchar(32) not null default(''),--联系手机
	Email varchar(10) not null default(''),--email
	Qq varchar(50) not null default(''),--联系QQ
	Wechat varchar(100) not null default(''),--微信
	Weibo varchar(100) not null default(''),--微博
	VirtualIntegral bigint not null default(0),--虚拟积分
	RealIntegral bigint not null default(0),--真实积分
	FeeType bigint not null,--付费类型
	PrepayValve decimal(20,8) not null default(0),--预付阀值
	Balance decimal(20,8) not null default(0),--余额
	FrozenBalance decimal(20,8) not null default(0),--冻结余额
	IncomingBalance decimal(20,8) not null default(0),--在途余额
	Commission decimal(20,8) not null default(0),--佣金比例
	Discount decimal(20,8) not null default(1),--折扣比例
	Province varchar(100) not null,--省份
	Area varchar(100) not null,--地区
	County varchar(100) not null,--县
	Community varchar(100) not null,--社区
	Address varchar(100) not null default(''),--地址
	Status bigint not null,--状态
	Type bigint not null,--类型
	Grade bigint not null,--级别
	Vocation bigint not null,--行业类型
	Reserve varchar(250),
	Remark varchar(250),
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint null,--注销人
	CancelTime datetime null,--注销时间
	 LogoIcon varchar(50) not null default(''), --公司logo
	 LogoUrl varchar(50) not null default(''), --公司Icon
	CONSTRAINT PK_SysCorp_ID primary key(ID),
	CONSTRAINT FK_SysCorp_Status foreign key(Status) references SysCorpStatus(ID),
	CONSTRAINT FK_SysCorp_Type foreign key(Type) references SysCorpType(ID),
	CONSTRAINT FK_SysCorp_Grade foreign key(Grade) references SysCorpGrade(ID),
	CONSTRAINT FK_SysCorp_FeeType foreign key(FeeType) references SysCorpFeeType(ID),
	CONSTRAINT FK_SysCorp_Vocation foreign key(Vocation) references SysCorpVocation(ID)
)
go

/*
皮肤表
*/
create table SysSkin
(
	ID bigint not null identity(0,1),--唯一标识
	Name	Varchar(100) not null unique,--皮肤名字
	Path	Varchar(100) not null unique,--皮肤路径
	DemoPicture	Varchar(100) not null,--皮肤demo
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint,--注销人
	CancelTime datetime,--注销时间
	CONSTRAINT PK_SysSkin_ID primary key(ID),
	CONSTRAINT UK_SysSkin_Name unique(Name)
)
go

/*
操作员状态说明表
*/
create table SysOperatorStatus(
	ID bigint not null identity(0,1),--唯一标识
	Name varchar(100) not null,--名字
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint,--注销人
	CancelTime datetime,--注销时间
	CONSTRAINT PK_SysOperatorStatus_ID primary key(ID),
	CONSTRAINT UK_SysOperatorStatus_Name unique(Name)
)
go
/*
操作员级别说明表
*/
create table SysOperatorGrade(
	ID bigint not null identity(0,1),--唯一标识
	Name varchar(100) not null,--名字
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint,--注销人
	CancelTime datetime,--注销时间
	CONSTRAINT PK_SysOperatorGrade_ID primary key(ID),
	CONSTRAINT UK_SysOperatorGrade_Name unique(Name)
)
go
/*
操作员星数说明表
*/
create table SysOperatorStar(
	ID bigint not null identity(0,1),--唯一标识
	Name varchar(100) not null,--名字
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint,--注销人
	CancelTime datetime,--注销时间
	CONSTRAINT PK_SysOperatorStar_ID primary key(ID),
	CONSTRAINT UK_SysOperatorStar_Name unique(Name)
)
go
/*
操作员表
*/
create table SysOperator(
	ID bigint not null identity(0,1),--唯一标识
	Corp bigint not null,--所属公司
	LoginName varchar(100) not null,--登陆名
	RealName varchar(100) not null,--真实姓名
	Password varchar(32) not null,--密码(MD5)
	Mobile varchar(32) not null default(''),--手机
	IdCard varchar(32) not null,--身份证
	Email varchar(100) not null default(''),--email
	WechatOpenid varchar(100) not null default(''),--微信openid
	AlipayOpenid varchar(100) not null default(''),--阿里openid
	Weibo varchar(100) not null default(''),--微博
	AvailableIP varchar(40) not null default('0.0.0.0'),--允许登陆的客户IP段
	WeatherCode varchar(6),--天气城市编码
	VirtualIntegral bigint not null default(0),--虚拟积分
	RealIntegral bigint not null default(0),--真实积分
	Balance decimal(20,8) not null default(1),--余额
	FrozenBalance decimal(20,8) not null default(1),--冻结余额
	IncomingBalance decimal(20,8) not null default(1),--在途余额
	Commission decimal(20,8) not null default(0),--佣金比例
	Discount decimal(20,8) not null default(1),--折扣比例
	Province varchar(100) not null,--省份
	Area varchar(100) not null,--地区
	County varchar(100) not null,--县
	Community varchar(100) not null,--社区
	Address varchar(100) not null default(''),--地址
	Status bigint not null default(0),--状态
	Skin bigint not null default(0),--皮肤ID
	Grade bigint default(0),--级别
	Star bigint default(0),--星数
	Session varchar(100),--当前的Session
	LoginTime datetime,--当前登录时间
	LoginIP varchar(40),--当前登录IP
	LoginAgent varchar(250),--登录浏览器
	LoginCount bigint default(0),--总共登录次数
	LoginErrorCount bigint default(0),--总共登录错误次数
	FrozenStartTime datetime,--冻结开始时间
	FrozenEndTime datetime,--冻结结束时间
	Reserve varchar(50),--保留
	Remark varchar(250),--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint,--注销人
	CancelTime datetime,--注销时间
	CONSTRAINT PK_SysOperator_ID primary key(ID),
	CONSTRAINT UK_SysOperator_LoginName unique(LoginName),
	CONSTRAINT UK_SysOperator_Mobile unique(Mobile),
	CONSTRAINT UK_SysOperator_IdCard unique(IdCard),
	CONSTRAINT FK_SysOperator_Status foreign key(Status) references SysOperatorStatus(ID),
	CONSTRAINT FK_SysOperator_Grade foreign key(Grade) references SysOperatorGrade(ID),
	CONSTRAINT FK_SysOperator_Star foreign key(Star) references SysOperatorStar(ID),
	CONSTRAINT FK_SysOperator_Skin foreign key(Skin) references SysSkin(ID)
)
go
/*
 * 操作员身份证图片
 */
create table IdCardImg(
	ID bigint not null identity(0,1),--唯一标识
	Operator bigint not null,--操作员
	ImageUrl varchar(100) not null,--图片url
	Front bit not null default(0),--0:正面 1:背面
	Reserve varchar(50),--保留
	Remark varchar(250),--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	CONSTRAINT PK_IdCardImg_ID PRIMARY KEY  (ID),
	CONSTRAINT FK_IdCardImg_Creator foreign key(Creator) references SysOperator(ID)
)
go
/*
 * 系统操作员登录日志表
 */
CREATE TABLE SysLoginLog (
	ID bigint NOT NULL identity(1,1),
	Operator bigint not NULL,--操作员id
	Ip varchar(40) NOT NULL default(''),--登录ip
	LoginAgent varchar(250),--登录浏览器
	Success bit not null default(0) ,--成功标志:0.成功,1.失败
	Time datetime NOT NULL,--登录时间
	Reserve varchar(50) null,--保留
	Remark varchar(250) null,--备注
	CONSTRAINT PK_SysLoginLog_ID PRIMARY KEY  (ID),
	CONSTRAINT FK_SysLoginLog_Operator FOREIGN KEY (Operator) REFERENCES SysOperator(ID)
)
go
/*
系统菜单
*/
create table SysMenu
(
	ID bigint not null identity(0,1),--唯一标识
	Direction bit,--方向(0水平,1垂直)
	Parent bigint not null,--父ID
	Name varchar(100) not null default(''),--名称
	Icon varchar(100) not null default(''),--图标
	Clazz varchar(100) not null default(''),--控制器Class
	Area varchar(100) not null default(''),--控制器Area
	Controller varchar(100) not null default(''),--控制器Controller
	Method varchar(100) not null default(''),--控制器Method
	Parameter varchar(100) not null default(''),--初始化参数
	Url varchar(100) not null default(''),--链接
	Reserve varchar(50) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint null,--注销人
	CancelTime datetime null,--注销时间
	CONSTRAINT PK_SysMenu_ID primary key(ID),
	CONSTRAINT UK_SysMenu_Name unique(direction,Parent,Name),
	CONSTRAINT FK_SysMenu_Creator foreign key(Creator) references SysOperator(ID),
	CONSTRAINT UK_SysMenu_Controller unique(Parent, Name, Clazz, Area, Controller, Method, Url)
)
go
/*
系统权限
*/
create table SysPrivilege
(
	ID bigint not null identity(0,1),--唯一标识
	Parent bigint not null,--父ID
	Menu bigint not null,--'所属菜单'
	Name varchar(100) not null default(''),--名称
	Clazz varchar(100) not null default(''),--控制器Class
	Area varchar(100) not null default(''),--控制器Area
	Controller varchar(100) not null default(''),--控制器Controller
	Method varchar(100) not null default(''),--控制器Mothod
	Parameter varchar(100) not null default(''),--初始化参数
	Url varchar(100) not null default(''),--链接
	Reserve varchar(50) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint null,--注销人
	CancelTime datetime null,--注销时间
	CONSTRAINT PK_SysPrivilege_ID primary key(ID),
	CONSTRAINT FK_SysPrivilege_Menu foreign key(Menu) references SysMenu(ID),
	CONSTRAINT UK_SysPrivilege_Name unique(Menu, Parent, Name),
	CONSTRAINT FK_SysPrivilege_Creator foreign key(Creator) references SysOperator(ID),
	CONSTRAINT UK_SysPrivilege_controller unique(Parent, Name, Area, Clazz, Controller, Method, Url)
)
go
/*
操作员组
*/
create table SysRole
(
	ID bigint not null identity(0,1),--唯一标识
	Corp bigint not null,--所属公司ID
	Name varchar(100) not null,--组名称
	Type bit not null default(0),--是否系统管理组(1是,0否)
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint null,--注销人
	CancelTime datetime null,--注销时间
	CONSTRAINT PK_SysRole_ID primary key(ID),
	CONSTRAINT UK_SysRole_Name unique(Corp, Name),
	CONSTRAINT FK_SysRole_Corp foreign key(Corp) references SysCorp(ID),
	CONSTRAINT FK_SysRole_Creator foreign key(Creator) references SysOperator(ID)
)
go
/*
操作员组成员
*/
create table SysRoleOperator
(
	ID bigint not null identity(1,1),--唯一标识
	Role bigint not null,--所属组ID
	Operator bigint not null,--操作员ID
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint null,--注销人
	CancelTime datetime null,--注销时间
	CONSTRAINT PK_SysRoleOperator_ID primary key(ID),
	CONSTRAINT UK_SysRoleOperator_Role_Operator unique(Role, Operator),
	CONSTRAINT FK_SysRoleOperator_Role foreign key(Role) references SysRole(ID),
	CONSTRAINT FK_SysRoleOperator_Operator foreign key(Operator) references SysOperator(ID),
	CONSTRAINT FK_SysRoleOperator_Creator foreign key(Creator) references SysOperator(ID)
)
go
/*
操作员组菜单
*/
create table SysRoleMenu
(
	ID bigint not null identity(1,1),--操作员组菜单
	Role bigint not null,--所属组ID
	Menu bigint not null,--菜单ID
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint null,--注销人
	CancelTime datetime null,--注销时间
	CONSTRAINT PK_SysRoleMenu_ID primary key(ID),
	CONSTRAINT UK_SysRoleMenu_Menu unique(Role, Menu),
	CONSTRAINT FK_SysRoleMenu_Role foreign key(Role) references SysRole(ID),
	CONSTRAINT FK_SysRoleMenu_Menu foreign key(Menu) references SysMenu(ID),
	CONSTRAINT FK_SysRoleMenu_Creator foreign key(Creator) references SysOperator(ID)
)
go

/*
操作员组权限
*/
create table SysRolePrivilege
(
	ID bigint not null identity(1,1),--唯一标识
	Role bigint not null,--所属组ID
	Privilege bigint not null,--权限ID
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint null,--注销人
	CancelTime datetime null,--注销时间
	CONSTRAINT PK_SysRolePrivilege_ID primary key(ID),
	CONSTRAINT UK_SysRolePrivilege_Privilege unique(Role, Privilege),
	CONSTRAINT FK_SysRolePrivilege_Role foreign key(Role) references SysRole(ID),
	CONSTRAINT FK_SysRolePrivilege_Privilege foreign key(Privilege) references SysPrivilege(ID),
	CONSTRAINT FK_SysRolePrivilege_Creator foreign key(Creator) references SysOperator(ID)
)
go

/*
公司类型菜单模板
*/
create table SysMenuTemplate
(
	ID bigint not null identity(1,1),--操作员组菜单
	CorpType bigint not null,--公司类型ID
	Menu bigint not null,--菜单ID
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint null,--注销人
	CancelTime datetime null,--注销时间
	CONSTRAINT PK_SysMenuTemplate_ID primary key(ID),
	CONSTRAINT UK_SysMenuTemplate_Menu unique(CorpType, Menu),
	CONSTRAINT FK_SysMenuTemplate_CorpType foreign key(CorpType) references SysCorpType(ID),
	CONSTRAINT FK_SysMenuTemplate_Menu foreign key(Menu) references SysMenu(ID),
	CONSTRAINT FK_SysMenuTemplate_Creator foreign key(Creator) references SysOperator(ID)
)
go
/*
公司类型权限模板
*/
create table SysPrivilegeTemplate
(
	ID bigint not null identity(1,1),--唯一标识
	CorpType bigint not null,--所属组ID
	Privilege bigint not null,--权限ID
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint null,--注销人
	CancelTime datetime null,--注销时间
	CONSTRAINT PK_SysPrivilegeTemplate_ID primary key(ID),
	CONSTRAINT UK_SysPrivilegeTemplate_Privilege unique(CorpType, Privilege),
	CONSTRAINT FK_SysPrivilegeTemplate_CorpType foreign key(CorpType) references SysCorpType(ID),
	CONSTRAINT FK_SysPrivilegeTemplate_Privilege foreign key(Privilege) references SysPrivilege(ID),
	CONSTRAINT FK_SysPrivilegeTemplate_Creator foreign key(Creator) references SysOperator(ID)
)
go

/*
 * 开放平台类型
 */
CREATE TABLE OpenPlatformType (
	Type varchar(100) not NULL,--平台类型weixin,alipay
	Name varchar(100) not null,--名称
	Reserve varchar(50) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint,--注销人
	CancelTime datetime,--注销时间
	CONSTRAINT PK_OpenPlatformType unique(Type)
)
go

/*
 * 开放平台
 */
CREATE TABLE OpenPlatform (
	ID bigint NOT NULL identity(1,1),--id
	Corp bigint not NULL,--所属公司
	PlatformType varchar(100) not NULL,--平台类型
	Token varchar(100) NOT NULL default(''),--令牌
	Appid varchar(200) NOT NULL default(''),--平台appid
	AppSecret varchar(50) NOT NULL default(''),--平台appSecret
	Openid varchar(32) not null default('') ,--平台openid
	AesKey varchar(44) NOT NULL default(''),--平台AesKey
	PrivateKey varchar(1000) NOT NULL default(''),--平台privateKey
	PublicKey varchar(1000) NOT NULL default(''),--平台publicKey阿里
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint null,--注销人
	CancelTime datetime null,--注销时间
	CONSTRAINT PK_OpenPlatform_ID PRIMARY KEY(ID),
	CONSTRAINT FK_OpenPlatform_Corp FOREIGN KEY (Corp) REFERENCES SysCorp(ID),
	CONSTRAINT FK_OpenPlatform_PlatformType FOREIGN KEY (PlatformType) REFERENCES OpenPlatformType(Type),
	CONSTRAINT FK_OpenPlatform_Creator FOREIGN KEY (Creator) REFERENCES SysOperator(ID),
	CONSTRAINT UK_OpenPlatform_Openid unique(Corp, Openid)
)
go

/*
 * 开放平台用户信息表
 */
CREATE TABLE OpenPlatformUserInfo(
	Subscribe tinyint not null default(0),--是否订阅,0未订，1已订
	PlatformType varchar(100) not NULL,--平台类型
	Openid varchar(32) not null,--用户的唯一标识
	Nickname varchar(50) not null,--用户昵称
	Sex tinyint not null,--用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
	Province varchar(50) not null,--用户个人资料填写的省份
	City varchar(20) not null,--普通用户个人资料填写的城市
	Country varchar(20) not null,--国家，如中国为CN
	Headimgurl varchar(200) not null,--用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空
	Privilege varchar(200) null,--用户特权信息，json 数组，如微信沃卡用户为（chinaunicom)
	SubscribeTime datetime null,--用户关注时间，为时间戳。如果用户曾多次关注，则取最后关注时间
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	CreateTime datetime not null default(getdate()),--创建时间
	CONSTRAINT PK_OpenPlatformUserInfo_Openid PRIMARY KEY(Openid),
	CONSTRAINT FK_OpenPlatformUserInfo_PlatformType FOREIGN KEY (PlatformType) REFERENCES OpenPlatformType(Type)
)
go

/*
 * 开放平台AccessToken
 */
CREATE TABLE OpenPlatformAccessToken(
	Corp bigint not null,
	PlatformType varchar(100) not NULL,--平台类型
	AccessToken varchar(1024) not null,
	AccessTime datetime not null,
	ExpiresIn bigint not null,
	Expires bigint not null,
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	CreateTime datetime not null default(getdate()),--创建时间
	CONSTRAINT PK_OpenPlatformAccessToken PRIMARY KEY(Corp),
	CONSTRAINT FK_OpenPlatformAccessToken_Corp FOREIGN KEY (Corp) REFERENCES SysCorp(ID),
	CONSTRAINT FK_OpenPlatformAccessToken_PlatformType FOREIGN KEY (PlatformType) REFERENCES OpenPlatformType(Type)
)
go

/*
*开放平台已订制用户表
*/
CREATE TABLE OpenPlatformSubscriber(
	Corp bigint not NULL,--所属公司
	Openid varchar(32) not NULL,--微信openid
	PlatformType varchar(100) not NULL,--平台类型
	Nickname varchar(100) not NULL default(''),--微信昵称
	Headimgurl varchar(200) not NULL default(''),--微信头像
	Name varchar(100) not NULL default(''),--姓名
	Alias varchar(100) not NULL default(''),--别名
	Gender varchar(2) not NULL default('男'),--性别
	TelePhone varchar(12) not NULL default(''),--电话
	MobilePhone varchar(11) not NULL default(''),--手机
	Integrate int not null default(0) ,--积分
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	EnrollTime datetime not NULL,--定制时间
	CancelTime datetime NULL,--取消时间
	CONSTRAINT PK_OpenPlatformSubscriber_openid PRIMARY KEY(openid),
	CONSTRAINT FK_OpenPlatformSubscriber_PlatformType FOREIGN KEY (PlatformType) REFERENCES OpenPlatformType(Type),
	CONSTRAINT FK_OpenPlatformSubscriber_Corp FOREIGN KEY (Corp) REFERENCES SysCorp(ID)
)
go


/*
 * 开放平台菜单类型表
 */
CREATE TABLE OpenPlatformMenuType (
	PlatformType varchar(100) not NULL,--平台类型
	Type varchar(50) NOT NULL default(''),--微信菜单类型
	Name varchar(50) NOT NULL default(''),--类型名称
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint,--注销人
	CancelTime datetime,--注销时间
	CONSTRAINT PK_OpenPlatformMenuType_type PRIMARY KEY(Type),
	CONSTRAINT UK_OpenPlatformMenuType_Name unique(platformType,name),
	CONSTRAINT FK_OpenPlatformMenuType_PlatformType FOREIGN KEY (PlatformType) REFERENCES OpenPlatformType(Type)
)
go

/*
 * 开放平台菜单
 */
CREATE TABLE OpenPlatformMenu (
	ID bigint NOT NULL identity(1,1),
	Corp bigint not NULL,--所属公司
	PlatformType varchar(100)not NULL,--平台类型
	Parent bigint NOT NULL,--父
	Name varchar(50) NOT NULL default(''),--菜单显示名称
	Type varchar(50) NOT NULL,--菜单类型
	Parameter varchar(50) NOT NULL,--菜单参数
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint,--注销人
	CancelTime datetime,--注销时间
	CONSTRAINT PK_OpenPlatformMenu_ID PRIMARY KEY(ID),
	CONSTRAINT FK_OpenPlatformMenu_Corp FOREIGN KEY (Corp) REFERENCES SysCorp(ID),
	CONSTRAINT FK_OpenPlatformMenu_PlatformType FOREIGN KEY (PlatformType) REFERENCES OpenPlatformType(Type),
	CONSTRAINT FK_OpenPlatformMenu_Type FOREIGN KEY (Type) REFERENCES OpenPlatformMenuType(Type),
	CONSTRAINT FK_OpenPlatformMenu_Creator FOREIGN KEY (Creator) REFERENCES SysOperator(ID)
)
go

/*
 * 开放平台消息类型表
 */
CREATE TABLE OpenPlatformMsgType (
	PlatformType varchar(100) not NULL,--平台类型
	Type varchar(50) NOT NULL default(''),--消息类型
	Name varchar(50) NOT NULL default(''),--类型名称
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint,--注销人
	CancelTime datetime,--注销时间
	CONSTRAINT PK_OpenPlatformMsgType_type PRIMARY KEY(Type),
	CONSTRAINT UK_OpenPlatformMsgType_Name unique(platformType,name),
	CONSTRAINT FK_OpenPlatformMsgType_PlatformType FOREIGN KEY (PlatformType) REFERENCES OpenPlatformType(Type)
)
go

/*
 * 开放平台消息
 */
CREATE TABLE OpenPlatformMsg (
	ID bigint NOT NULL identity(1,1),
	Corp bigint not NULL,--所属公司
	PlatformType varchar(100) not NULL,--平台类型
	MsgType varchar(50) NOT NULL default(''),--消息类型
	Title varchar(1000) NOT NULL default(''),--标题
	Description varchar(1000) NOT NULL default(''),--图文消息描述
	PicUrl varchar(1000) NOT NULL default(''),--图片链接
	Url varchar(100) NULL,--点击图文消息跳转链接
	Content text NULL,--内容
	Children varchar(1000) NULL,--子图文消息ID
	Sort tinyint NOT NULL default(0),--排序，优先顺序从小到大
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not NULL,--创建人
	CreateTime datetime NOT NULL,--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint null,--注销人
	CancelTime datetime null,--注销时间
	CONSTRAINT PK_OpenPlateform_ID PRIMARY KEY(ID),
	CONSTRAINT FK_OpenPlateform_Corp FOREIGN KEY (Corp) REFERENCES SysCorp(ID),
	CONSTRAINT FK_OpenPlateform_PlatformType FOREIGN KEY (PlatformType) REFERENCES OpenPlatformType(Type),
	CONSTRAINT FK_OpenPlateform_Creator FOREIGN KEY (Creator) REFERENCES SysOperator(ID)
)
go



/*
 * 开放平台子消息
 */
CREATE TABLE OpenPlatformMsgChildren (
	ID bigint NOT NULL identity(1,1),
	Parent bigint not NULL,--父消息
	Child bigint not NULL,--子消息'
	Sort tinyint NOT NULL default(0),--排序，优先顺序从小到大
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint,--注销人
	CancelTime datetime,--注销时间
	CONSTRAINT PK_OpenPlateformMsgChildren_ID PRIMARY KEY(ID),
	CONSTRAINT FK_OpenPlatformMsgChildren_Parent FOREIGN KEY (Parent) REFERENCES OpenPlatformMsg(ID),
	CONSTRAINT FK_OpenPlatformMsgChildren_Child FOREIGN KEY (Child) REFERENCES OpenPlatformMsg(ID)
)
go


/*
 * 开放平台消息关键字
 */
CREATE TABLE OpenPlatformMsgKey (
	ID bigint NOT NULL identity(1,1),
	Corp bigint not NULL,--所属公司
	Msg bigint not NULL,--消息
	Command varchar(255) NOT NULL default(''),--命令（关键字）
	Matching tinyint NOT NULL default(0),--0:完全匹配 1:包含匹配
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not NULL,--创建人
	CreateTime datetime NOT NULL,--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	CONSTRAINT PK_OpenPlatformMsgKey_ID PRIMARY KEY(ID),
	CONSTRAINT FK_OpenPlatformMsgKey_Corp FOREIGN KEY (Corp) REFERENCES SysCorp(ID),
	CONSTRAINT UK_OpenPlatformMsgKey unique(Msg,command),
	CONSTRAINT FK_OpenPlatformMsgKey_msg FOREIGN KEY (Msg) REFERENCES OpenPlatformMsg(ID) ON DELETE CASCADE,
	CONSTRAINT FK_OpenPlatformMsgKey_Creator FOREIGN KEY (Creator) REFERENCES SysOperator(ID)
)
go


/*
 * 缺省消息
 */
CREATE TABLE OpenPlatformDefaultMsg(
	ID bigint NOT NULL identity(1,1),
	Corp bigint not NULL,--所属公司
	PlatformType varchar(100) not NULL,--平台类型
	Type tinyint NOT NULL default(0),--0:关注时回复消息 1:用户上行无匹配时回复消息
	Msg bigint not NULL,--消息
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint,--注销人
	CancelTime datetime,--注销时间
	CONSTRAINT PK_OpenPlatformDefaultMsg_ID PRIMARY KEY(ID),
	/*CONSTRAINT FK_DefaultMessage_PlatformType FOREIGN KEY (PlatformType) REFERENCES OpenPlatform_Type(Type),*/
	CONSTRAINT FK_OpenPlatformDefaultMsg_Corp FOREIGN KEY (Corp) REFERENCES SysCorp(ID),
	CONSTRAINT FK_OpenPlatformDefaultMsg_Creator FOREIGN KEY (Creator) REFERENCES SysOperator(ID)
)
go


/*
 * 开放平台MO表
 */
CREATE TABLE OpenPlatformMO(
	ID bigint NOT NULL identity(1,1),
	PlatformType varchar(100) not NULL,--平台类型
	ToUserName varchar(32)not NULL,--接收微信号
	FromUserName varchar(32)not NULL,--发送方帐号(一个OpenID)
	CreateTime bigint not NULL,--消息创建时间
	MsgType varchar(50) not NULL,--类型
	MsgID bigint,--消息ID
	Content text,--文本消息
	PicUrl varchar(200),--图片消息URL
	MediaId varchar(100),--语音消息媒体id
	Location_X decimal default(0),--地理位置维度
	Location_Y decimal default(0),--地理位置经度
	Scale decimal default(0),--地图缩放大小
	Label varchar(100),--地理位置信息
	Title varchar(100),--链接消息标题
	Description varchar(200),--链接消息描述
	Url varchar(100),--链接消息链接
	Format varchar(100),--语音格式
	Recognition varchar(100),--语音识别结果
	Event varchar(100),--事件类型
	EventKey varchar(100),--事件KEY值,qrscene_为前缀,后面为二维码的参数值
	Ticket varchar(100),--二维码的ticket,可用来换取二维码图片
	Latitude decimal default(0),--地理位置纬度
	Longitude decimal default(0),--地理位置经度
	Precision decimal default(0),--地理位置精度
	ReceiveTime datetime not null default(getdate()) ,--接收时间
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	PerformTime datetime ,--处理时间
	Performer varchar(20) ,--处理者
	CONSTRAINT PK_OpenPlatformMO_ID PRIMARY KEY(ID),
	CONSTRAINT FK_OpenPlatformMO_PlatformType FOREIGN KEY (PlatformType) REFERENCES OpenPlatformType(Type),
	CONSTRAINT FK_OpenPlatformMO_MsgType FOREIGN KEY (MsgType) REFERENCES OpenPlatformMsgType(Type)
)
go

/*
 * 开放平台MT表
 */
CREATE TABLE OpenPlatformMT(
	ID bigint NOT NULL identity(1,1),
	PlatformType varchar(100) not NULL,--平台类型
	Mo bigint not NULL default(0),--上行信息ID
	Msg bigint not NULL default(0),--消息ID
	ToUserName varchar(32)not NULL,--接收微信号
	FromUserName varchar(32)not NULL,--发送方帐号(一个OpenID)
	MsgType varchar(50) not NULL,--类型
	Title varchar(1000) NOT NULL default(''),--标题
	Description varchar(1000) NOT NULL default(''),--图文消息描述
	PicUrl varchar(1000) NOT NULL default(''),--图片链接
	Content text not NULL,--消息内容
	Children varchar(200) not NULL default(''),--图文子消息
	Priority tinyint not null default(0),--优先权(0-9),越大优先权越高
	SendTime datetime not null,--发送时间
	ErrCode int not null default(-1),--发送状态
	ErrMsg varchar(100) not null default(''),--发送错误信息
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	PerformTime datetime ,--处理时间
	PresentTime datetime not null,--请求时间
	Presenter bigint not NULL default(0),--请求人
	AuditTime datetime,--审核时间
	Auditor bigint,--审核人
	CONSTRAINT PK_OpenPlatformMID PRIMARY KEY(ID),
	CONSTRAINT FK_OpenPlatformMPlatformType FOREIGN KEY (PlatformType) REFERENCES OpenPlatformType(Type),
	CONSTRAINT FK_OpenPlatformMMsgType FOREIGN KEY (MsgType) REFERENCES OpenPlatformMsgType(Type)
)
go


CREATE TABLE [dbo].[SysBank](
	[ID] [BIGINT] IDENTITY(1,1) NOT NULL,
	[Number] [BIGINT] NULL,
	[Url] [VARCHAR](100) NULL,
	[Type] [INT] NOT NULL,
	[Name] [VARCHAR](50) NULL,
	[ShortName] [VARCHAR](20) NULL,
	[NiceName] [VARCHAR](20) NULL,
	[Status] [INT] NULL,
	[Reserve] [VARCHAR](250) NULL,
	[Remark] [VARCHAR](250) NULL,
	[Creator] [BIGINT] NULL,
	[CreateTime] [DATETIME] NULL,
	[Auditor] [BIGINT] NULL,
	[AuditTime] [DATETIME] NULL,
	[Canceler] [BIGINT] NULL,
	[CancelTime] [DATETIME] NULL
)