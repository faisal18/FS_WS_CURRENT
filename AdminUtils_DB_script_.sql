USE [master]
GO
/****** Object:  Database [FS_WS_WSCTFW]    Script Date: 6/16/2016 12:11:38 PM ******/
CREATE DATABASE [FS_WS_WSCTFW] ON  PRIMARY 
( NAME = N'FS_WS_WSCTFW', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\FS_WS_WSCTFW.mdf' , SIZE = 4544KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FS_WS_WSCTFW_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\FS_WS_WSCTFW_log.ldf' , SIZE = 1088KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FS_WS_WSCTFW].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FS_WS_WSCTFW] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FS_WS_WSCTFW] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FS_WS_WSCTFW] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FS_WS_WSCTFW] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FS_WS_WSCTFW] SET ARITHABORT OFF 
GO
ALTER DATABASE [FS_WS_WSCTFW] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [FS_WS_WSCTFW] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FS_WS_WSCTFW] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FS_WS_WSCTFW] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FS_WS_WSCTFW] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FS_WS_WSCTFW] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FS_WS_WSCTFW] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FS_WS_WSCTFW] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FS_WS_WSCTFW] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FS_WS_WSCTFW] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FS_WS_WSCTFW] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FS_WS_WSCTFW] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FS_WS_WSCTFW] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FS_WS_WSCTFW] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FS_WS_WSCTFW] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FS_WS_WSCTFW] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [FS_WS_WSCTFW] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FS_WS_WSCTFW] SET RECOVERY FULL 
GO
ALTER DATABASE [FS_WS_WSCTFW] SET  MULTI_USER 
GO
ALTER DATABASE [FS_WS_WSCTFW] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FS_WS_WSCTFW] SET DB_CHAINING OFF 
GO
USE [FS_WS_WSCTFW]
GO
/****** Object:  Table [__MigrationHistory]    Script Date: 6/16/2016 12:11:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AspNetRoles]    Script Date: 6/16/2016 12:11:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AspNetUserClaims]    Script Date: 6/16/2016 12:11:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [AspNetUserLogins]    Script Date: 6/16/2016 12:11:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AspNetUserRoles]    Script Date: 6/16/2016 12:11:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AspNetUsers]    Script Date: 6/16/2016 12:11:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[Discriminator] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [MaintenanceLogs]    Script Date: 6/16/2016 12:11:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [MaintenanceLogs](
	[MLogID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [nvarchar](max) NOT NULL,
	[MaintenanceLogDetails] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[TimeStamp] [timestamp] NOT NULL,
	[AppPath] [nvarchar](max) NULL,
	[ErrorDetails] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.MaintenanceLogs] PRIMARY KEY CLUSTERED 
(
	[MLogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [WSEnvironments]    Script Date: 6/16/2016 12:11:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [WSEnvironments](
	[WSEnvID] [int] IDENTITY(1,1) NOT NULL,
	[WSURL] [nvarchar](max) NULL,
	[WSEnvName] [nvarchar](max) NOT NULL,
	[WSEnvEndPoint] [nvarchar](max) NULL,
	[WSEnvWSDL] [nvarchar](max) NOT NULL,
	[WSUsername] [nvarchar](max) NULL,
	[WSPasswd] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[TimeStamp] [timestamp] NOT NULL,
	[isActive] [bit] NOT NULL,
	[isDeleted] [bit] NOT NULL,
	[isPublic] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.WSEnvironments] PRIMARY KEY CLUSTERED 
(
	[WSEnvID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [WSMethodParameters]    Script Date: 6/16/2016 12:11:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [WSMethodParameters](
	[WSMethodParametersID] [int] IDENTITY(1,1) NOT NULL,
	[ParameterName] [nvarchar](max) NOT NULL,
	[ParameterDataType] [nvarchar](max) NULL,
	[ParameterType] [nvarchar](max) NULL,
	[ParameterDefaultValue] [nvarchar](max) NULL,
	[ParameterDescrition] [nvarchar](max) NULL,
	[WSEnvID] [int] NOT NULL,
	[WSMethodID] [int] NOT NULL,
	[isActive] [bit] NOT NULL,
	[isDeleted] [bit] NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[TimeStamp] [timestamp] NOT NULL,
	[WSMethodsModels_WSMethodsID] [int] NULL,
 CONSTRAINT [PK_dbo.WSMethodParameters] PRIMARY KEY CLUSTERED 
(
	[WSMethodParametersID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [WSMethods]    Script Date: 6/16/2016 12:11:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [WSMethods](
	[WSMethodsID] [int] IDENTITY(1,1) NOT NULL,
	[MethodName] [nvarchar](max) NOT NULL,
	[MethodDetails] [nvarchar](max) NULL,
	[WSEnvID] [int] NOT NULL,
	[isActive] [bit] NOT NULL,
	[isDeleted] [bit] NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[TimeStamp] [timestamp] NOT NULL,
	[SOAPAction] [nvarchar](max) NULL,
	[Binding] [nvarchar](max) NULL,
	[BindingAddress] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.WSMethods] PRIMARY KEY CLUSTERED 
(
	[WSMethodsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [WSRequestParameters]    Script Date: 6/16/2016 12:11:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [WSRequestParameters](
	[WSRequestParametersID] [int] IDENTITY(1,1) NOT NULL,
	[ParameterValue] [nvarchar](max) NOT NULL,
	[WSRequestID] [int] NOT NULL,
	[WSMethodParametersID] [int] NOT NULL,
	[isActive] [bit] NOT NULL,
	[isDeleted] [bit] NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[TimeStamp] [timestamp] NOT NULL,
	[WSRequestsModels_WSWebServiceRequestID] [int] NULL,
 CONSTRAINT [PK_dbo.WSRequestParameters] PRIMARY KEY CLUSTERED 
(
	[WSRequestParametersID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [WSRequests]    Script Date: 6/16/2016 12:11:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [WSRequests](
	[WSWebServiceRequestID] [int] IDENTITY(1,1) NOT NULL,
	[WSEnvID] [int] NOT NULL,
	[RequestDetails] [nvarchar](max) NOT NULL,
	[ResponseDetails] [nvarchar](max) NULL,
	[isActive] [bit] NOT NULL,
	[isDeleted] [bit] NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[TimeStamp] [timestamp] NOT NULL,
	[WSMethodID] [int] NOT NULL,
	[WSMethodsModels_WSMethodsID] [int] NULL,
	[WSRequestSize] [int] NOT NULL,
 CONSTRAINT [PK_dbo.WSRequests] PRIMARY KEY CLUSTERED 
(
	[WSWebServiceRequestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [WSResponse]    Script Date: 6/16/2016 12:11:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [WSResponse](
	[WSResponseID] [int] IDENTITY(1,1) NOT NULL,
	[RequestDetails] [nvarchar](max) NOT NULL,
	[ResponseDetails] [nvarchar](max) NULL,
	[WSResponseHeaderStatusCode] [int] NOT NULL,
	[WSResponseTime] [int] NOT NULL,
	[WSResponseSize] [int] NOT NULL,
	[WSRequestID] [int] NOT NULL,
	[isActive] [bit] NOT NULL,
	[isDeleted] [bit] NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[TimeStamp] [timestamp] NOT NULL,
	[WSRequestsModels_WSWebServiceRequestID] [int] NULL,
 CONSTRAINT [PK_dbo.WSResponse] PRIMARY KEY CLUSTERED 
(
	[WSResponseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 6/16/2016 12:11:38 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 6/16/2016 12:11:38 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 6/16/2016 12:11:38 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 6/16/2016 12:11:38 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 6/16/2016 12:11:38 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 6/16/2016 12:11:38 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_WSEnvID]    Script Date: 6/16/2016 12:11:38 PM ******/
CREATE NONCLUSTERED INDEX [IX_WSEnvID] ON [WSMethodParameters]
(
	[WSEnvID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_WSMethodsModels_WSMethodsID]    Script Date: 6/16/2016 12:11:38 PM ******/
CREATE NONCLUSTERED INDEX [IX_WSMethodsModels_WSMethodsID] ON [WSMethodParameters]
(
	[WSMethodsModels_WSMethodsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_WSEnvID]    Script Date: 6/16/2016 12:11:38 PM ******/
CREATE NONCLUSTERED INDEX [IX_WSEnvID] ON [WSMethods]
(
	[WSEnvID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_WSMethodParametersID]    Script Date: 6/16/2016 12:11:38 PM ******/
CREATE NONCLUSTERED INDEX [IX_WSMethodParametersID] ON [WSRequestParameters]
(
	[WSMethodParametersID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_WSRequestsModels_WSWebServiceRequestID]    Script Date: 6/16/2016 12:11:38 PM ******/
CREATE NONCLUSTERED INDEX [IX_WSRequestsModels_WSWebServiceRequestID] ON [WSRequestParameters]
(
	[WSRequestsModels_WSWebServiceRequestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_WSEnvID]    Script Date: 6/16/2016 12:11:38 PM ******/
CREATE NONCLUSTERED INDEX [IX_WSEnvID] ON [WSRequests]
(
	[WSEnvID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_WSMethodsModels_WSMethodsID]    Script Date: 6/16/2016 12:11:38 PM ******/
CREATE NONCLUSTERED INDEX [IX_WSMethodsModels_WSMethodsID] ON [WSRequests]
(
	[WSMethodsModels_WSMethodsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_WSRequestsModels_WSWebServiceRequestID]    Script Date: 6/16/2016 12:11:38 PM ******/
CREATE NONCLUSTERED INDEX [IX_WSRequestsModels_WSWebServiceRequestID] ON [WSResponse]
(
	[WSRequestsModels_WSWebServiceRequestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [WSEnvironments] ADD  DEFAULT ((0)) FOR [isActive]
GO
ALTER TABLE [WSEnvironments] ADD  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [WSEnvironments] ADD  DEFAULT ((0)) FOR [isPublic]
GO
ALTER TABLE [WSRequests] ADD  DEFAULT ((0)) FOR [WSMethodID]
GO
ALTER TABLE [WSRequests] ADD  DEFAULT ((0)) FOR [WSRequestSize]
GO
ALTER TABLE [AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [WSMethodParameters]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WSMethodParameters_dbo.WSEnvironments_WSEnvID] FOREIGN KEY([WSEnvID])
REFERENCES [WSEnvironments] ([WSEnvID])
ON DELETE CASCADE
GO
ALTER TABLE [WSMethodParameters] CHECK CONSTRAINT [FK_dbo.WSMethodParameters_dbo.WSEnvironments_WSEnvID]
GO
ALTER TABLE [WSMethodParameters]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WSMethodParameters_dbo.WSMethods_WSMethodsModels_WSMethodsID] FOREIGN KEY([WSMethodsModels_WSMethodsID])
REFERENCES [WSMethods] ([WSMethodsID])
GO
ALTER TABLE [WSMethodParameters] CHECK CONSTRAINT [FK_dbo.WSMethodParameters_dbo.WSMethods_WSMethodsModels_WSMethodsID]
GO
ALTER TABLE [WSMethods]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WSMethods_dbo.WSEnvironments_WSEnvID] FOREIGN KEY([WSEnvID])
REFERENCES [WSEnvironments] ([WSEnvID])
ON DELETE CASCADE
GO
ALTER TABLE [WSMethods] CHECK CONSTRAINT [FK_dbo.WSMethods_dbo.WSEnvironments_WSEnvID]
GO
ALTER TABLE [WSRequestParameters]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WSRequestParameters_dbo.WSMethodParameters_WSMethodParametersID] FOREIGN KEY([WSMethodParametersID])
REFERENCES [WSMethodParameters] ([WSMethodParametersID])
ON DELETE CASCADE
GO
ALTER TABLE [WSRequestParameters] CHECK CONSTRAINT [FK_dbo.WSRequestParameters_dbo.WSMethodParameters_WSMethodParametersID]
GO
ALTER TABLE [WSRequestParameters]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WSRequestParameters_dbo.WSRequests_WSRequestsModels_WSWebServiceRequestID] FOREIGN KEY([WSRequestsModels_WSWebServiceRequestID])
REFERENCES [WSRequests] ([WSWebServiceRequestID])
GO
ALTER TABLE [WSRequestParameters] CHECK CONSTRAINT [FK_dbo.WSRequestParameters_dbo.WSRequests_WSRequestsModels_WSWebServiceRequestID]
GO
ALTER TABLE [WSRequests]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WSRequests_dbo.WSMethods_WSMethodsModels_WSMethodsID] FOREIGN KEY([WSMethodsModels_WSMethodsID])
REFERENCES [WSMethods] ([WSMethodsID])
GO
ALTER TABLE [WSRequests] CHECK CONSTRAINT [FK_dbo.WSRequests_dbo.WSMethods_WSMethodsModels_WSMethodsID]
GO
ALTER TABLE [WSRequests]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WSWebService_dbo.WSEnvironments_WSEnvID] FOREIGN KEY([WSEnvID])
REFERENCES [WSEnvironments] ([WSEnvID])
ON DELETE CASCADE
GO
ALTER TABLE [WSRequests] CHECK CONSTRAINT [FK_dbo.WSWebService_dbo.WSEnvironments_WSEnvID]
GO
ALTER TABLE [WSResponse]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WSResponse_dbo.WSRequests_WSRequestsModels_WSWebServiceRequestID] FOREIGN KEY([WSRequestsModels_WSWebServiceRequestID])
REFERENCES [WSRequests] ([WSWebServiceRequestID])
GO
ALTER TABLE [WSResponse] CHECK CONSTRAINT [FK_dbo.WSResponse_dbo.WSRequests_WSRequestsModels_WSWebServiceRequestID]
GO
USE [master]
GO
ALTER DATABASE [FS_WS_WSCTFW] SET  READ_WRITE 
GO
