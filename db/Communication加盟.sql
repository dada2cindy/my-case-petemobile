/****** Object:  ForeignKey [FKFB4A43CBD604F076]    Script Date: 05/09/2016 22:42:38 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FKFB4A43CBD604F076]') AND parent_object_id = OBJECT_ID(N'[dbo].[AUTH_FUNCTION_PATH]'))
ALTER TABLE [dbo].[AUTH_FUNCTION_PATH] DROP CONSTRAINT [FKFB4A43CBD604F076]
GO
/****** Object:  ForeignKey [FK2843E431E76F1629]    Script Date: 05/09/2016 22:42:38 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK2843E431E76F1629]') AND parent_object_id = OBJECT_ID(N'[dbo].[AUTH_MENU_FUNC]'))
ALTER TABLE [dbo].[AUTH_MENU_FUNC] DROP CONSTRAINT [FK2843E431E76F1629]
GO
/****** Object:  ForeignKey [FK5567A438D604F076]    Script Date: 05/09/2016 22:42:38 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK5567A438D604F076]') AND parent_object_id = OBJECT_ID(N'[dbo].[AUTH_ROLE_FUNC]'))
ALTER TABLE [dbo].[AUTH_ROLE_FUNC] DROP CONSTRAINT [FK5567A438D604F076]
GO
/****** Object:  ForeignKey [FK5567A438FB046CB]    Script Date: 05/09/2016 22:42:38 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK5567A438FB046CB]') AND parent_object_id = OBJECT_ID(N'[dbo].[AUTH_ROLE_FUNC]'))
ALTER TABLE [dbo].[AUTH_ROLE_FUNC] DROP CONSTRAINT [FK5567A438FB046CB]
GO
/****** Object:  ForeignKey [FKCF567B35D7C9EC4B]    Script Date: 05/09/2016 22:42:39 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FKCF567B35D7C9EC4B]') AND parent_object_id = OBJECT_ID(N'[dbo].[AUTH_USER_IN_ROLE]'))
ALTER TABLE [dbo].[AUTH_USER_IN_ROLE] DROP CONSTRAINT [FKCF567B35D7C9EC4B]
GO
/****** Object:  ForeignKey [FKCF567B35FB046CB]    Script Date: 05/09/2016 22:42:39 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FKCF567B35FB046CB]') AND parent_object_id = OBJECT_ID(N'[dbo].[AUTH_USER_IN_ROLE]'))
ALTER TABLE [dbo].[AUTH_USER_IN_ROLE] DROP CONSTRAINT [FKCF567B35FB046CB]
GO
/****** Object:  ForeignKey [FK2EA96F7C33026F24]    Script Date: 05/09/2016 22:42:39 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK2EA96F7C33026F24]') AND parent_object_id = OBJECT_ID(N'[dbo].[POST]'))
ALTER TABLE [dbo].[POST] DROP CONSTRAINT [FK2EA96F7C33026F24]
GO
/****** Object:  ForeignKey [FK2EA96F7C64A6D7BA]    Script Date: 05/09/2016 22:42:39 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK2EA96F7C64A6D7BA]') AND parent_object_id = OBJECT_ID(N'[dbo].[POST]'))
ALTER TABLE [dbo].[POST] DROP CONSTRAINT [FK2EA96F7C64A6D7BA]
GO
/****** Object:  ForeignKey [FKD3EE24B717ED9912]    Script Date: 05/09/2016 22:42:39 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FKD3EE24B717ED9912]') AND parent_object_id = OBJECT_ID(N'[dbo].[POST_NODE]'))
ALTER TABLE [dbo].[POST_NODE] DROP CONSTRAINT [FKD3EE24B717ED9912]
GO
/****** Object:  ForeignKey [FKE05F5297ABDB002C]    Script Date: 05/09/2016 22:42:39 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FKE05F5297ABDB002C]') AND parent_object_id = OBJECT_ID(N'[dbo].[POST_POST_MESSAGE]'))
ALTER TABLE [dbo].[POST_POST_MESSAGE] DROP CONSTRAINT [FKE05F5297ABDB002C]
GO
/****** Object:  ForeignKey [FKE05F5297DCEF9320]    Script Date: 05/09/2016 22:42:39 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FKE05F5297DCEF9320]') AND parent_object_id = OBJECT_ID(N'[dbo].[POST_POST_MESSAGE]'))
ALTER TABLE [dbo].[POST_POST_MESSAGE] DROP CONSTRAINT [FKE05F5297DCEF9320]
GO
/****** Object:  Table [dbo].[POST_POST_MESSAGE]    Script Date: 05/09/2016 22:42:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POST_POST_MESSAGE]') AND type in (N'U'))
DROP TABLE [dbo].[POST_POST_MESSAGE]
GO
/****** Object:  Table [dbo].[AUTH_FUNCTION_PATH]    Script Date: 05/09/2016 22:42:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_FUNCTION_PATH]') AND type in (N'U'))
DROP TABLE [dbo].[AUTH_FUNCTION_PATH]
GO
/****** Object:  Table [dbo].[AUTH_ROLE_FUNC]    Script Date: 05/09/2016 22:42:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_ROLE_FUNC]') AND type in (N'U'))
DROP TABLE [dbo].[AUTH_ROLE_FUNC]
GO
/****** Object:  Table [dbo].[AUTH_USER_IN_ROLE]    Script Date: 05/09/2016 22:42:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_USER_IN_ROLE]') AND type in (N'U'))
DROP TABLE [dbo].[AUTH_USER_IN_ROLE]
GO
/****** Object:  Table [dbo].[POST]    Script Date: 05/09/2016 22:42:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POST]') AND type in (N'U'))
DROP TABLE [dbo].[POST]
GO
/****** Object:  Table [dbo].[POST_MESSAGE]    Script Date: 05/09/2016 22:42:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POST_MESSAGE]') AND type in (N'U'))
DROP TABLE [dbo].[POST_MESSAGE]
GO
/****** Object:  Table [dbo].[POST_NODE]    Script Date: 05/09/2016 22:42:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POST_NODE]') AND type in (N'U'))
DROP TABLE [dbo].[POST_NODE]
GO
/****** Object:  Table [dbo].[COMMON_SERIAL]    Script Date: 05/09/2016 22:42:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[COMMON_SERIAL]') AND type in (N'U'))
DROP TABLE [dbo].[COMMON_SERIAL]
GO
/****** Object:  Table [dbo].[MEMBER]    Script Date: 05/09/2016 22:42:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MEMBER]') AND type in (N'U'))
DROP TABLE [dbo].[MEMBER]
GO
/****** Object:  Table [dbo].[AUTH_LOGIN_ROLE]    Script Date: 05/09/2016 22:42:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_LOGIN_ROLE]') AND type in (N'U'))
DROP TABLE [dbo].[AUTH_LOGIN_ROLE]
GO
/****** Object:  Table [dbo].[AUTH_LOGIN_USER]    Script Date: 05/09/2016 22:42:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_LOGIN_USER]') AND type in (N'U'))
DROP TABLE [dbo].[AUTH_LOGIN_USER]
GO
/****** Object:  Table [dbo].[AUTH_MENU_FUNC]    Script Date: 05/09/2016 22:42:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_MENU_FUNC]') AND type in (N'U'))
DROP TABLE [dbo].[AUTH_MENU_FUNC]
GO
/****** Object:  Table [dbo].[STORAGE_FILE]    Script Date: 05/09/2016 22:42:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STORAGE_FILE]') AND type in (N'U'))
DROP TABLE [dbo].[STORAGE_FILE]
GO
/****** Object:  Table [dbo].[SYSTEM_ITEM_PARAM]    Script Date: 05/09/2016 22:42:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SYSTEM_ITEM_PARAM]') AND type in (N'U'))
DROP TABLE [dbo].[SYSTEM_ITEM_PARAM]
GO
/****** Object:  Table [dbo].[SYSTEM_LOG_SYSTEM]    Script Date: 05/09/2016 22:42:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SYSTEM_LOG_SYSTEM]') AND type in (N'U'))
DROP TABLE [dbo].[SYSTEM_LOG_SYSTEM]
GO
/****** Object:  Table [dbo].[SYSTEM_SYSTEM_PARAM]    Script Date: 05/09/2016 22:42:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SYSTEM_SYSTEM_PARAM]') AND type in (N'U'))
DROP TABLE [dbo].[SYSTEM_SYSTEM_PARAM]
GO
/****** Object:  Table [dbo].[SYSTEM_TEMPLATE]    Script Date: 05/09/2016 22:42:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SYSTEM_TEMPLATE]') AND type in (N'U'))
DROP TABLE [dbo].[SYSTEM_TEMPLATE]
GO
/****** Object:  Table [dbo].[ACCOUNTINT_TARGET]    Script Date: 05/09/2016 22:42:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACCOUNTINT_TARGET]') AND type in (N'U'))
DROP TABLE [dbo].[ACCOUNTINT_TARGET]
GO
/****** Object:  Table [dbo].[ACCOUNTINT_TARGET]    Script Date: 05/09/2016 22:42:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACCOUNTINT_TARGET]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ACCOUNTINT_TARGET](
	[Id] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NOT NULL,
	[Name] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Amount] [float] NULL,
 CONSTRAINT [PK__ACCOUNTI__3214EC0710566F31] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[ACCOUNTINT_TARGET] ([Id], [Name], [Amount]) VALUES (N'201510宋昌益', N'宋昌益', 0)
INSERT [dbo].[ACCOUNTINT_TARGET] ([Id], [Name], [Amount]) VALUES (N'201510總合', N'總合', 10000)
/****** Object:  Table [dbo].[SYSTEM_TEMPLATE]    Script Date: 05/09/2016 22:42:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SYSTEM_TEMPLATE]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SYSTEM_TEMPLATE](
	[TemplateId] [int] IDENTITY(1,1) NOT NULL,
	[TemplateType] [int] NULL,
	[Name] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NOT NULL,
	[CSS] [nvarchar](200) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[FileName] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[StartDate] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[EndDate] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Flag] [int] NULL,
 CONSTRAINT [PK__SYSTEM_T__F87ADD27534D60F1] PRIMARY KEY CLUSTERED 
(
	[TemplateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[SYSTEM_SYSTEM_PARAM]    Script Date: 05/09/2016 22:42:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SYSTEM_SYSTEM_PARAM]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SYSTEM_SYSTEM_PARAM](
	[SystemParamId] [int] IDENTITY(1,1) NOT NULL,
	[SendEmail] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[MailSmtp] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Account] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Password] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[MailPort] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[EnableSSL] [bit] NULL,
	[PageTitle] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[PageKeyWord] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[PageDescription] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[GoogleAnalytics] [nvarchar](max) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[FacebookCode] [nvarchar](max) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[FilePassword] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
 CONSTRAINT [PK__SYSTEM_S__D62825574BAC3F29] PRIMARY KEY CLUSTERED 
(
	[SystemParamId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[SYSTEM_SYSTEM_PARAM] ON
INSERT [dbo].[SYSTEM_SYSTEM_PARAM] ([SystemParamId], [SendEmail], [MailSmtp], [Account], [Password], [MailPort], [EnableSSL], [PageTitle], [PageKeyWord], [PageDescription], [GoogleAnalytics], [FacebookCode], [FilePassword]) VALUES (1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'')
SET IDENTITY_INSERT [dbo].[SYSTEM_SYSTEM_PARAM] OFF
/****** Object:  Table [dbo].[SYSTEM_LOG_SYSTEM]    Script Date: 05/09/2016 22:42:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SYSTEM_LOG_SYSTEM]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SYSTEM_LOG_SYSTEM](
	[LogSystemId] [int] IDENTITY(1,1) NOT NULL,
	[UpdateDate] [datetime] NULL,
	[Fucntion] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[SubFucntion] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Action] [nvarchar](15) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[UpdateId] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Note] [nvarchar](500) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[UpdateClassName] [nvarchar](150) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[IpAddress] [nvarchar](150) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
 CONSTRAINT [PK__SYSTEM_L__CCC52DC24F7CD00D] PRIMARY KEY CLUSTERED 
(
	[LogSystemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[SYSTEM_LOG_SYSTEM] ON
INSERT [dbo].[SYSTEM_LOG_SYSTEM] ([LogSystemId], [UpdateDate], [Fucntion], [SubFucntion], [Action], [UpdateId], [Note], [UpdateClassName], [IpAddress]) VALUES (628, CAST(0x0000A60101758EDC AS DateTime), N'登入記錄', N'登入記錄', N'登入記錄', N'admin', NULL, NULL, N'::1')
SET IDENTITY_INSERT [dbo].[SYSTEM_LOG_SYSTEM] OFF
/****** Object:  Table [dbo].[SYSTEM_ITEM_PARAM]    Script Date: 05/09/2016 22:42:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SYSTEM_ITEM_PARAM]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SYSTEM_ITEM_PARAM](
	[ItemParamId] [int] IDENTITY(1,1) NOT NULL,
	[Classify] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NOT NULL,
	[Name] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NOT NULL,
	[Value] [nvarchar](max) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK__SYSTEM_I__59D4D9DE47DBAE45] PRIMARY KEY CLUSTERED 
(
	[ItemParamId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[STORAGE_FILE]    Script Date: 05/09/2016 22:42:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STORAGE_FILE]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[STORAGE_FILE](
	[StorageFileId] [int] IDENTITY(1,1) NOT NULL,
	[DisplayName] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NOT NULL,
	[HtmlContent] [nvarchar](max) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[FileName] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NOT NULL,
	[IsTemporary] [bit] NULL,
	[SourceUri] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[CurrentPath] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[FileSize] [bigint] NULL,
	[SourceType] [int] NULL,
	[SourceId] [int] NULL,
	[IsCover] [bit] NULL,
	[SortNo] [int] NULL,
	[CreatedBy] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[UpdatedBy] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[UpdatedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK__STORAGE___5979C6B67E37BEF6] PRIMARY KEY CLUSTERED 
(
	[StorageFileId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[AUTH_MENU_FUNC]    Script Date: 05/09/2016 22:42:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_MENU_FUNC]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AUTH_MENU_FUNC](
	[MenuFuncId] [int] IDENTITY(1,1) NOT NULL,
	[MenuFuncName] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[ParentId] [int] NULL,
	[ListOrder] [int] NULL,
	[MainPath] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Note] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
 CONSTRAINT [PK__AUTH_MEN__448EF7C46754599E] PRIMARY KEY CLUSTERED 
(
	[MenuFuncId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[AUTH_MENU_FUNC] ON
INSERT [dbo].[AUTH_MENU_FUNC] ([MenuFuncId], [MenuFuncName], [ParentId], [ListOrder], [MainPath], [Note]) VALUES (1, N'客戶/庫存管理', NULL, 1000, NULL, NULL)
INSERT [dbo].[AUTH_MENU_FUNC] ([MenuFuncId], [MenuFuncName], [ParentId], [ListOrder], [MainPath], [Note]) VALUES (2, N'客戶列表', 1, 1001, N'admin/UC05/0511.aspx', NULL)
INSERT [dbo].[AUTH_MENU_FUNC] ([MenuFuncId], [MenuFuncName], [ParentId], [ListOrder], [MainPath], [Note]) VALUES (3, N'庫存列表', 1, 1002, N'admin/UC05/0512.aspx', NULL)
INSERT [dbo].[AUTH_MENU_FUNC] ([MenuFuncId], [MenuFuncName], [ParentId], [ListOrder], [MainPath], [Note]) VALUES (4, N'庫存類別', 1, 1003, N'admin/UC04/0411.aspx', NULL)
INSERT [dbo].[AUTH_MENU_FUNC] ([MenuFuncId], [MenuFuncName], [ParentId], [ListOrder], [MainPath], [Note]) VALUES (5, N'庫存品名', 1, 1004, N'admin/UC04/0421.aspx', NULL)
INSERT [dbo].[AUTH_MENU_FUNC] ([MenuFuncId], [MenuFuncName], [ParentId], [ListOrder], [MainPath], [Note]) VALUES (6, N'業績報表', 1, 1005, N'admin/UC07/0711.aspx', NULL)
INSERT [dbo].[AUTH_MENU_FUNC] ([MenuFuncId], [MenuFuncName], [ParentId], [ListOrder], [MainPath], [Note]) VALUES (7, N'個人設定', NULL, 99998, NULL, NULL)
INSERT [dbo].[AUTH_MENU_FUNC] ([MenuFuncId], [MenuFuncName], [ParentId], [ListOrder], [MainPath], [Note]) VALUES (8, N'登入密碼變更', 7, 1, N'admin/UC30/3001Personal.aspx', NULL)
INSERT [dbo].[AUTH_MENU_FUNC] ([MenuFuncId], [MenuFuncName], [ParentId], [ListOrder], [MainPath], [Note]) VALUES (9, N'權限管理', NULL, 99999, NULL, NULL)
INSERT [dbo].[AUTH_MENU_FUNC] ([MenuFuncId], [MenuFuncName], [ParentId], [ListOrder], [MainPath], [Note]) VALUES (10, N'帳號管理', 9, 1, N'admin/UC14/UserAdd.aspx', NULL)
INSERT [dbo].[AUTH_MENU_FUNC] ([MenuFuncId], [MenuFuncName], [ParentId], [ListOrder], [MainPath], [Note]) VALUES (11, N'群組管理', 9, 2, N'admin/UC14/RoleAdd.aspx', NULL)
INSERT [dbo].[AUTH_MENU_FUNC] ([MenuFuncId], [MenuFuncName], [ParentId], [ListOrder], [MainPath], [Note]) VALUES (12, N'帳號群組設定', 9, 3, N'admin/UC14/UserRoleSet.aspx', NULL)
INSERT [dbo].[AUTH_MENU_FUNC] ([MenuFuncId], [MenuFuncName], [ParentId], [ListOrder], [MainPath], [Note]) VALUES (13, N'群組權限設定', 9, 4, N'admin/UC14/RoleFuncSet.aspx', NULL)
INSERT [dbo].[AUTH_MENU_FUNC] ([MenuFuncId], [MenuFuncName], [ParentId], [ListOrder], [MainPath], [Note]) VALUES (14, N'使用紀錄', 9, 5, N'admin/UC14/QueryLog.aspx', NULL)
SET IDENTITY_INSERT [dbo].[AUTH_MENU_FUNC] OFF
/****** Object:  Table [dbo].[AUTH_LOGIN_USER]    Script Date: 05/09/2016 22:42:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_LOGIN_USER]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AUTH_LOGIN_USER](
	[UserId] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NOT NULL,
	[Version] [int] NOT NULL,
	[Password] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Comment] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[CreateDate] [datetime] NULL,
	[FullNameInChinese] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[FullNameInEnglish] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Sex] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[ContactAddress] [nvarchar](500) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[ContactZipCode] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[HousePhoneAreaCode] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[HousePhone] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[HouseOtherPhone] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Email] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[SSID] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Birthday] [datetime] NULL,
	[Height] [int] NULL,
	[Weight] [int] NULL,
	[Blood] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[JobTitle] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Mobile] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[ArrivedDate] [datetime] NULL,
	[LeaveDate] [datetime] NULL,
	[EmergencyContactPerson] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[EmergencyRelationship] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[EmergencyPhone] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[EmergencyAddress] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[UpdatedBy] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[UpdatedDate] [datetime] NULL,
	[IsAlive] [int] NULL,
	[ShowInSalesStatistics] [int] NULL,
	[SortNo] [int] NULL,
 CONSTRAINT [PK__AUTH_LOG__1788CC4C5AEE82B9] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[AUTH_LOGIN_USER] ([UserId], [Version], [Password], [Comment], [CreateDate], [FullNameInChinese], [FullNameInEnglish], [Sex], [ContactAddress], [ContactZipCode], [HousePhoneAreaCode], [HousePhone], [HouseOtherPhone], [Email], [SSID], [Birthday], [Height], [Weight], [Blood], [JobTitle], [Mobile], [ArrivedDate], [LeaveDate], [EmergencyContactPerson], [EmergencyRelationship], [EmergencyPhone], [EmergencyAddress], [UpdatedBy], [UpdatedDate], [IsAlive], [ShowInSalesStatistics], [SortNo]) VALUES (N'admin', 2, N'1234', NULL, CAST(0x0000A52501752B40 AS DateTime), N'系統管理者', N'Administrator', NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', NULL, 0, 0, NULL, NULL, N'123', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 0, 9999)
/****** Object:  Table [dbo].[AUTH_LOGIN_ROLE]    Script Date: 05/09/2016 22:42:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_LOGIN_ROLE]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AUTH_LOGIN_ROLE](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NOT NULL,
	[LoweredRoleName] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Description] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
 CONSTRAINT [PK__AUTH_LOG__8AFACE1A5FB337D6] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [UQ__AUTH_LOG__8A2B6160628FA481] UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[AUTH_LOGIN_ROLE] ON
INSERT [dbo].[AUTH_LOGIN_ROLE] ([RoleId], [RoleName], [LoweredRoleName], [Description]) VALUES (1, N'系統管理員', NULL, NULL)
INSERT [dbo].[AUTH_LOGIN_ROLE] ([RoleId], [RoleName], [LoweredRoleName], [Description]) VALUES (2, N'店員', NULL, NULL)
SET IDENTITY_INSERT [dbo].[AUTH_LOGIN_ROLE] OFF
/****** Object:  Table [dbo].[MEMBER]    Script Date: 05/09/2016 22:42:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MEMBER]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MEMBER](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LoginID] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Password] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Name] [nvarchar](100) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Company] [nvarchar](100) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Dept] [nvarchar](100) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[JobTitle] [nvarchar](100) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Phone] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Mobile] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Fax] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Email] [nvarchar](200) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[CreateIP] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_CI_AS NOT NULL,
	[Sex] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Status] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[UserConfirm] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Token] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[ApplyDate] [datetime] NULL,
	[DueDate] [datetime] NULL,
	[Birthday] [datetime] NULL,
	[BirthdayYear] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[BirthdayMonth] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[BirthdayDay] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Project] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Product] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[PhoneSer] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[PhonePrice] [float] NULL,
	[PhoneSellPrice] [float] NULL,
	[Commission] [float] NULL,
	[BreakMoney] [float] NULL,
	[Compensation] [float] NULL,
	[ContractMonths] [float] NULL,
	[Sales] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[WarrantySuppliers] [nvarchar](100) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[MobileWholesalers] [nvarchar](100) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Note] [nvarchar](max) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[ApplyDate2] [datetime] NULL,
	[PID] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Store] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[OnlineWholesalers] [nvarchar](100) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[SimNo] [nvarchar](100) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Project1] [nvarchar](100) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Project2] [nvarchar](100) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Project3] [nvarchar](100) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[GetCommission] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Prepayment] [float] NULL,
	[ReturnCommission] [float] NULL,
	[SelfPrepayment] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
 CONSTRAINT [PK__MEMBER__3214EC2702084FDA] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[COMMON_SERIAL]    Script Date: 05/09/2016 22:42:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[COMMON_SERIAL]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[COMMON_SERIAL](
	[SerialId] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NOT NULL,
	[Count] [int] NULL,
	[Date] [datetime] NULL,
 CONSTRAINT [PK__COMMON_S__5E5B3EE4571DF1D5] PRIMARY KEY CLUSTERED 
(
	[SerialId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[POST_NODE]    Script Date: 05/09/2016 22:42:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POST_NODE]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[POST_NODE](
	[NodeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[ParentId] [int] NULL,
	[SortNo] [int] NULL,
	[Flag] [int] NULL,
	[UType] [int] NULL,
	[PicFileName] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[HtmlContent] [nvarchar](max) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[CreatedBy] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[UpdatedBy] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[UpdatedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK__POST_NOD__6BAE226372C60C4A] PRIMARY KEY CLUSTERED 
(
	[NodeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[POST_NODE] ON
INSERT [dbo].[POST_NODE] ([NodeId], [Name], [ParentId], [SortNo], [Flag], [UType], [PicFileName], [HtmlContent], [CreatedBy], [UpdatedBy], [UpdatedDate], [CreatedDate]) VALUES (1, N'Root', NULL, 1, 1, 0, NULL, NULL, N'admin', N'admin', CAST(0x0000A52501752B40 AS DateTime), CAST(0x0000A52501752B40 AS DateTime))
INSERT [dbo].[POST_NODE] ([NodeId], [Name], [ParentId], [SortNo], [Flag], [UType], [PicFileName], [HtmlContent], [CreatedBy], [UpdatedBy], [UpdatedDate], [CreatedDate]) VALUES (2, N'庫存', 1, 1, 1, 0, NULL, NULL, N'admin', N'admin', CAST(0x0000A52501752B40 AS DateTime), CAST(0x0000A52501752B40 AS DateTime))
INSERT [dbo].[POST_NODE] ([NodeId], [Name], [ParentId], [SortNo], [Flag], [UType], [PicFileName], [HtmlContent], [CreatedBy], [UpdatedBy], [UpdatedDate], [CreatedDate]) VALUES (3, N'庫存類別', 1, 2, 1, 0, NULL, NULL, N'admin', N'admin', CAST(0x0000A52501752C6C AS DateTime), CAST(0x0000A52501752C6C AS DateTime))
INSERT [dbo].[POST_NODE] ([NodeId], [Name], [ParentId], [SortNo], [Flag], [UType], [PicFileName], [HtmlContent], [CreatedBy], [UpdatedBy], [UpdatedDate], [CreatedDate]) VALUES (6, N'庫存品名', 1, 3, 1, 0, NULL, NULL, N'admin', N'admin', CAST(0x0000A52501752C6C AS DateTime), CAST(0x0000A52501752C6C AS DateTime))
INSERT [dbo].[POST_NODE] ([NodeId], [Name], [ParentId], [SortNo], [Flag], [UType], [PicFileName], [HtmlContent], [CreatedBy], [UpdatedBy], [UpdatedDate], [CreatedDate]) VALUES (9, N'店家', 1, 4, 1, 0, NULL, NULL, N'admin', N'admin', CAST(0x0000A52501752C6C AS DateTime), CAST(0x0000A52501752C6C AS DateTime))
INSERT [dbo].[POST_NODE] ([NodeId], [Name], [ParentId], [SortNo], [Flag], [UType], [PicFileName], [HtmlContent], [CreatedBy], [UpdatedBy], [UpdatedDate], [CreatedDate]) VALUES (10, N'淡水', 9, 1, 1, 0, NULL, NULL, N'admin', N'admin', CAST(0x0000A52501752C6C AS DateTime), CAST(0x0000A52501752C6C AS DateTime))
INSERT [dbo].[POST_NODE] ([NodeId], [Name], [ParentId], [SortNo], [Flag], [UType], [PicFileName], [HtmlContent], [CreatedBy], [UpdatedBy], [UpdatedDate], [CreatedDate]) VALUES (17, N'P-手機殼', 3, 1, 1, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[POST_NODE] ([NodeId], [Name], [ParentId], [SortNo], [Flag], [UType], [PicFileName], [HtmlContent], [CreatedBy], [UpdatedBy], [UpdatedDate], [CreatedDate]) VALUES (18, N'B-保護貼', 3, 2, 1, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[POST_NODE] ([NodeId], [Name], [ParentId], [SortNo], [Flag], [UType], [PicFileName], [HtmlContent], [CreatedBy], [UpdatedBy], [UpdatedDate], [CreatedDate]) VALUES (19, N'L-充電傳輸', 3, 3, 1, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[POST_NODE] ([NodeId], [Name], [ParentId], [SortNo], [Flag], [UType], [PicFileName], [HtmlContent], [CreatedBy], [UpdatedBy], [UpdatedDate], [CreatedDate]) VALUES (20, N'PO-行動電源', 3, 4, 1, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[POST_NODE] ([NodeId], [Name], [ParentId], [SortNo], [Flag], [UType], [PicFileName], [HtmlContent], [CreatedBy], [UpdatedBy], [UpdatedDate], [CreatedDate]) VALUES (21, N'O-耳機喇叭', 3, 5, 1, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[POST_NODE] ([NodeId], [Name], [ParentId], [SortNo], [Flag], [UType], [PicFileName], [HtmlContent], [CreatedBy], [UpdatedBy], [UpdatedDate], [CreatedDate]) VALUES (22, N'PH-手機類', 3, 6, 1, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[POST_NODE] ([NodeId], [Name], [ParentId], [SortNo], [Flag], [UType], [PicFileName], [HtmlContent], [CreatedBy], [UpdatedBy], [UpdatedDate], [CreatedDate]) VALUES (23, N'OT-其他', 3, 7, 1, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[POST_NODE] ([NodeId], [Name], [ParentId], [SortNo], [Flag], [UType], [PicFileName], [HtmlContent], [CreatedBy], [UpdatedBy], [UpdatedDate], [CreatedDate]) VALUES (24, N'#特別現金收支', 1, 4, 1, 0, NULL, NULL, N'admin', N'admin', NULL, NULL)
INSERT [dbo].[POST_NODE] ([NodeId], [Name], [ParentId], [SortNo], [Flag], [UType], [PicFileName], [HtmlContent], [CreatedBy], [UpdatedBy], [UpdatedDate], [CreatedDate]) VALUES (25, N'#每日結帳', 1, 5, 1, 0, NULL, NULL, N'admin', N'admin', NULL, NULL)
SET IDENTITY_INSERT [dbo].[POST_NODE] OFF
/****** Object:  Table [dbo].[POST_MESSAGE]    Script Date: 05/09/2016 22:42:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POST_MESSAGE]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[POST_MESSAGE](
	[MessageId] [int] IDENTITY(1,1) NOT NULL,
	[CreateName] [nvarchar](100) COLLATE Chinese_Taiwan_Stroke_CI_AS NOT NULL,
	[CreateIP] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_CI_AS NOT NULL,
	[CreatedDate] [datetime] NULL,
	[Phone] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Mobile] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Fax] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[EMail] [nvarchar](100) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[LineID] [nvarchar](100) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[QType] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Content] [nvarchar](max) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[ReservationDate] [datetime] NULL,
	[ReservationPeriod] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[ReservationTime] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
 CONSTRAINT [PK__POST_MES__C87C0C9C6EF57B66] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[POST]    Script Date: 05/09/2016 22:42:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POST]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[POST](
	[PostId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Summary] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[HtmlContent] [nvarchar](max) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[KeyWord] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[NodeId] [int] NULL,
	[ParentPostId] [int] NULL,
	[SortNo] [int] NULL,
	[Flag] [int] NULL,
	[Quantity] [int] NULL,
	[PicFileName] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[PicFileName2] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[DocFileName] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[CreatedBy] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[UpdatedBy] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[UpdatedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[ShowDate] [datetime] NULL,
	[CloseDate] [datetime] NULL,
	[Type] [int] NULL,
	[LinkUrl] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[PageTitle] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[PageKeyWord] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[PageDescription] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[IsRecommend] [bit] NULL,
	[Price] [float] NULL,
	[SellPrice] [float] NULL,
	[IsTemp] [bit] NULL,
	[CustomField1] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[CustomField2] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[MemberName] [nvarchar](100) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[MemberPhone] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[ProductSer] [nvarchar](100) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[WarrantySuppliers] [nvarchar](100) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Wholesalers] [nvarchar](100) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[MemberId] [nvarchar](100) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
 CONSTRAINT [PK__POST__AA12601876969D2E] PRIMARY KEY CLUSTERED 
(
	[PostId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[AUTH_USER_IN_ROLE]    Script Date: 05/09/2016 22:42:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_USER_IN_ROLE]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AUTH_USER_IN_ROLE](
	[UserId] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NOT NULL,
	[RoleId] [int] NOT NULL
)
END
GO
INSERT [dbo].[AUTH_USER_IN_ROLE] ([UserId], [RoleId]) VALUES (N'admin', 1)
/****** Object:  Table [dbo].[AUTH_ROLE_FUNC]    Script Date: 05/09/2016 22:42:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_ROLE_FUNC]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AUTH_ROLE_FUNC](
	[RoleId] [int] NOT NULL,
	[MenuFuncId] [int] NOT NULL
)
END
GO
INSERT [dbo].[AUTH_ROLE_FUNC] ([RoleId], [MenuFuncId]) VALUES (1, 8)
INSERT [dbo].[AUTH_ROLE_FUNC] ([RoleId], [MenuFuncId]) VALUES (1, 10)
INSERT [dbo].[AUTH_ROLE_FUNC] ([RoleId], [MenuFuncId]) VALUES (1, 11)
INSERT [dbo].[AUTH_ROLE_FUNC] ([RoleId], [MenuFuncId]) VALUES (1, 12)
INSERT [dbo].[AUTH_ROLE_FUNC] ([RoleId], [MenuFuncId]) VALUES (1, 13)
INSERT [dbo].[AUTH_ROLE_FUNC] ([RoleId], [MenuFuncId]) VALUES (1, 14)
INSERT [dbo].[AUTH_ROLE_FUNC] ([RoleId], [MenuFuncId]) VALUES (1, 2)
INSERT [dbo].[AUTH_ROLE_FUNC] ([RoleId], [MenuFuncId]) VALUES (1, 3)
INSERT [dbo].[AUTH_ROLE_FUNC] ([RoleId], [MenuFuncId]) VALUES (1, 4)
INSERT [dbo].[AUTH_ROLE_FUNC] ([RoleId], [MenuFuncId]) VALUES (1, 5)
INSERT [dbo].[AUTH_ROLE_FUNC] ([RoleId], [MenuFuncId]) VALUES (1, 6)
INSERT [dbo].[AUTH_ROLE_FUNC] ([RoleId], [MenuFuncId]) VALUES (2, 8)
INSERT [dbo].[AUTH_ROLE_FUNC] ([RoleId], [MenuFuncId]) VALUES (2, 10)
INSERT [dbo].[AUTH_ROLE_FUNC] ([RoleId], [MenuFuncId]) VALUES (2, 11)
INSERT [dbo].[AUTH_ROLE_FUNC] ([RoleId], [MenuFuncId]) VALUES (2, 12)
INSERT [dbo].[AUTH_ROLE_FUNC] ([RoleId], [MenuFuncId]) VALUES (2, 13)
INSERT [dbo].[AUTH_ROLE_FUNC] ([RoleId], [MenuFuncId]) VALUES (2, 14)
INSERT [dbo].[AUTH_ROLE_FUNC] ([RoleId], [MenuFuncId]) VALUES (2, 2)
INSERT [dbo].[AUTH_ROLE_FUNC] ([RoleId], [MenuFuncId]) VALUES (2, 3)
INSERT [dbo].[AUTH_ROLE_FUNC] ([RoleId], [MenuFuncId]) VALUES (2, 4)
INSERT [dbo].[AUTH_ROLE_FUNC] ([RoleId], [MenuFuncId]) VALUES (2, 5)
INSERT [dbo].[AUTH_ROLE_FUNC] ([RoleId], [MenuFuncId]) VALUES (2, 6)
/****** Object:  Table [dbo].[AUTH_FUNCTION_PATH]    Script Date: 05/09/2016 22:42:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_FUNCTION_PATH]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AUTH_FUNCTION_PATH](
	[FunctionPathId] [int] IDENTITY(1,1) NOT NULL,
	[MenuFuncId] [int] NULL,
	[Path] [nvarchar](255) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
 CONSTRAINT [PK__AUTH_FUN__594999936B24EA82] PRIMARY KEY CLUSTERED 
(
	[FunctionPathId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[AUTH_FUNCTION_PATH] ON
INSERT [dbo].[AUTH_FUNCTION_PATH] ([FunctionPathId], [MenuFuncId], [Path]) VALUES (1, 14, N'admin/UC07/0711_2.aspx')
SET IDENTITY_INSERT [dbo].[AUTH_FUNCTION_PATH] OFF
/****** Object:  Table [dbo].[POST_POST_MESSAGE]    Script Date: 05/09/2016 22:42:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POST_POST_MESSAGE]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[POST_POST_MESSAGE](
	[PostMessageId] [int] IDENTITY(1,1) NOT NULL,
	[PostId] [int] NULL,
	[ParentPostMessageId] [int] NULL,
	[CreateName] [nvarchar](100) COLLATE Chinese_Taiwan_Stroke_CI_AS NOT NULL,
	[CreateIP] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_CI_AS NOT NULL,
	[CreatedDate] [datetime] NULL,
	[Phone] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Mobile] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Fax] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[EMail] [nvarchar](100) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
	[Content] [nvarchar](max) COLLATE Chinese_Taiwan_Stroke_CI_AS NULL,
 CONSTRAINT [PK__POST_POS__23E114367A672E12] PRIMARY KEY CLUSTERED 
(
	[PostMessageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  ForeignKey [FKFB4A43CBD604F076]    Script Date: 05/09/2016 22:42:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FKFB4A43CBD604F076]') AND parent_object_id = OBJECT_ID(N'[dbo].[AUTH_FUNCTION_PATH]'))
ALTER TABLE [dbo].[AUTH_FUNCTION_PATH]  WITH CHECK ADD  CONSTRAINT [FKFB4A43CBD604F076] FOREIGN KEY([MenuFuncId])
REFERENCES [dbo].[AUTH_MENU_FUNC] ([MenuFuncId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FKFB4A43CBD604F076]') AND parent_object_id = OBJECT_ID(N'[dbo].[AUTH_FUNCTION_PATH]'))
ALTER TABLE [dbo].[AUTH_FUNCTION_PATH] CHECK CONSTRAINT [FKFB4A43CBD604F076]
GO
/****** Object:  ForeignKey [FK2843E431E76F1629]    Script Date: 05/09/2016 22:42:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK2843E431E76F1629]') AND parent_object_id = OBJECT_ID(N'[dbo].[AUTH_MENU_FUNC]'))
ALTER TABLE [dbo].[AUTH_MENU_FUNC]  WITH CHECK ADD  CONSTRAINT [FK2843E431E76F1629] FOREIGN KEY([ParentId])
REFERENCES [dbo].[AUTH_MENU_FUNC] ([MenuFuncId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK2843E431E76F1629]') AND parent_object_id = OBJECT_ID(N'[dbo].[AUTH_MENU_FUNC]'))
ALTER TABLE [dbo].[AUTH_MENU_FUNC] CHECK CONSTRAINT [FK2843E431E76F1629]
GO
/****** Object:  ForeignKey [FK5567A438D604F076]    Script Date: 05/09/2016 22:42:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK5567A438D604F076]') AND parent_object_id = OBJECT_ID(N'[dbo].[AUTH_ROLE_FUNC]'))
ALTER TABLE [dbo].[AUTH_ROLE_FUNC]  WITH CHECK ADD  CONSTRAINT [FK5567A438D604F076] FOREIGN KEY([MenuFuncId])
REFERENCES [dbo].[AUTH_MENU_FUNC] ([MenuFuncId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK5567A438D604F076]') AND parent_object_id = OBJECT_ID(N'[dbo].[AUTH_ROLE_FUNC]'))
ALTER TABLE [dbo].[AUTH_ROLE_FUNC] CHECK CONSTRAINT [FK5567A438D604F076]
GO
/****** Object:  ForeignKey [FK5567A438FB046CB]    Script Date: 05/09/2016 22:42:38 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK5567A438FB046CB]') AND parent_object_id = OBJECT_ID(N'[dbo].[AUTH_ROLE_FUNC]'))
ALTER TABLE [dbo].[AUTH_ROLE_FUNC]  WITH CHECK ADD  CONSTRAINT [FK5567A438FB046CB] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AUTH_LOGIN_ROLE] ([RoleId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK5567A438FB046CB]') AND parent_object_id = OBJECT_ID(N'[dbo].[AUTH_ROLE_FUNC]'))
ALTER TABLE [dbo].[AUTH_ROLE_FUNC] CHECK CONSTRAINT [FK5567A438FB046CB]
GO
/****** Object:  ForeignKey [FKCF567B35D7C9EC4B]    Script Date: 05/09/2016 22:42:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FKCF567B35D7C9EC4B]') AND parent_object_id = OBJECT_ID(N'[dbo].[AUTH_USER_IN_ROLE]'))
ALTER TABLE [dbo].[AUTH_USER_IN_ROLE]  WITH CHECK ADD  CONSTRAINT [FKCF567B35D7C9EC4B] FOREIGN KEY([UserId])
REFERENCES [dbo].[AUTH_LOGIN_USER] ([UserId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FKCF567B35D7C9EC4B]') AND parent_object_id = OBJECT_ID(N'[dbo].[AUTH_USER_IN_ROLE]'))
ALTER TABLE [dbo].[AUTH_USER_IN_ROLE] CHECK CONSTRAINT [FKCF567B35D7C9EC4B]
GO
/****** Object:  ForeignKey [FKCF567B35FB046CB]    Script Date: 05/09/2016 22:42:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FKCF567B35FB046CB]') AND parent_object_id = OBJECT_ID(N'[dbo].[AUTH_USER_IN_ROLE]'))
ALTER TABLE [dbo].[AUTH_USER_IN_ROLE]  WITH CHECK ADD  CONSTRAINT [FKCF567B35FB046CB] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AUTH_LOGIN_ROLE] ([RoleId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FKCF567B35FB046CB]') AND parent_object_id = OBJECT_ID(N'[dbo].[AUTH_USER_IN_ROLE]'))
ALTER TABLE [dbo].[AUTH_USER_IN_ROLE] CHECK CONSTRAINT [FKCF567B35FB046CB]
GO
/****** Object:  ForeignKey [FK2EA96F7C33026F24]    Script Date: 05/09/2016 22:42:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK2EA96F7C33026F24]') AND parent_object_id = OBJECT_ID(N'[dbo].[POST]'))
ALTER TABLE [dbo].[POST]  WITH CHECK ADD  CONSTRAINT [FK2EA96F7C33026F24] FOREIGN KEY([ParentPostId])
REFERENCES [dbo].[POST] ([PostId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK2EA96F7C33026F24]') AND parent_object_id = OBJECT_ID(N'[dbo].[POST]'))
ALTER TABLE [dbo].[POST] CHECK CONSTRAINT [FK2EA96F7C33026F24]
GO
/****** Object:  ForeignKey [FK2EA96F7C64A6D7BA]    Script Date: 05/09/2016 22:42:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK2EA96F7C64A6D7BA]') AND parent_object_id = OBJECT_ID(N'[dbo].[POST]'))
ALTER TABLE [dbo].[POST]  WITH CHECK ADD  CONSTRAINT [FK2EA96F7C64A6D7BA] FOREIGN KEY([NodeId])
REFERENCES [dbo].[POST_NODE] ([NodeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK2EA96F7C64A6D7BA]') AND parent_object_id = OBJECT_ID(N'[dbo].[POST]'))
ALTER TABLE [dbo].[POST] CHECK CONSTRAINT [FK2EA96F7C64A6D7BA]
GO
/****** Object:  ForeignKey [FKD3EE24B717ED9912]    Script Date: 05/09/2016 22:42:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FKD3EE24B717ED9912]') AND parent_object_id = OBJECT_ID(N'[dbo].[POST_NODE]'))
ALTER TABLE [dbo].[POST_NODE]  WITH CHECK ADD  CONSTRAINT [FKD3EE24B717ED9912] FOREIGN KEY([ParentId])
REFERENCES [dbo].[POST_NODE] ([NodeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FKD3EE24B717ED9912]') AND parent_object_id = OBJECT_ID(N'[dbo].[POST_NODE]'))
ALTER TABLE [dbo].[POST_NODE] CHECK CONSTRAINT [FKD3EE24B717ED9912]
GO
/****** Object:  ForeignKey [FKE05F5297ABDB002C]    Script Date: 05/09/2016 22:42:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FKE05F5297ABDB002C]') AND parent_object_id = OBJECT_ID(N'[dbo].[POST_POST_MESSAGE]'))
ALTER TABLE [dbo].[POST_POST_MESSAGE]  WITH CHECK ADD  CONSTRAINT [FKE05F5297ABDB002C] FOREIGN KEY([ParentPostMessageId])
REFERENCES [dbo].[POST_POST_MESSAGE] ([PostMessageId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FKE05F5297ABDB002C]') AND parent_object_id = OBJECT_ID(N'[dbo].[POST_POST_MESSAGE]'))
ALTER TABLE [dbo].[POST_POST_MESSAGE] CHECK CONSTRAINT [FKE05F5297ABDB002C]
GO
/****** Object:  ForeignKey [FKE05F5297DCEF9320]    Script Date: 05/09/2016 22:42:39 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FKE05F5297DCEF9320]') AND parent_object_id = OBJECT_ID(N'[dbo].[POST_POST_MESSAGE]'))
ALTER TABLE [dbo].[POST_POST_MESSAGE]  WITH CHECK ADD  CONSTRAINT [FKE05F5297DCEF9320] FOREIGN KEY([PostId])
REFERENCES [dbo].[POST] ([PostId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FKE05F5297DCEF9320]') AND parent_object_id = OBJECT_ID(N'[dbo].[POST_POST_MESSAGE]'))
ALTER TABLE [dbo].[POST_POST_MESSAGE] CHECK CONSTRAINT [FKE05F5297DCEF9320]
GO
