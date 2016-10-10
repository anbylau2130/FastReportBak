CREATE TABLE [dbo].[SysOperatorSupplier](
	[ID] [BIGINT] IDENTITY(1,1) NOT NULL,
	[Operator] [BIGINT] NOT  NULL,
	[Supplier] [BIGINT] NOT NULL,
	[Creator] [BIGINT] NOT NULL,
	[CreateTime] [DATETIME] NOT NULL,
	[Auditor] [BIGINT] NULL,
	[AuditTime] [DATETIME] NULL,
	[Canceler] [BIGINT] NULL,
	[CancelTime] [DATETIME] NULL,
	[Remark] [VARCHAR](50) NULL,
	[Reserve] [VARCHAR](50) NULL,
)