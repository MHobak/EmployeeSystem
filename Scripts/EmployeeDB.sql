/****** Object:  Database [EmployeeDB]    Script Date: 3/30/2023 3:28:48 PM ******/
CREATE DATABASE [EmployeeDB]
GO
USE [EmployeeDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 3/30/2023 3:28:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 3/30/2023 3:28:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[LastName] [nvarchar](200) NOT NULL,
	[RFC] [nvarchar](13) NOT NULL,
	[BornDate] [datetime2](7) NOT NULL,
	[Status] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230330194651_CreateEmployeeDB', N'7.0.4')
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([ID], [Name], [LastName], [RFC], [BornDate], [Status]) VALUES (1, N'Roberto', N'González', N'GOCR980618VE3', CAST(N'1989-03-09T00:00:00.0000000' AS DateTime2), N'Inactive')
INSERT [dbo].[Employee] ([ID], [Name], [LastName], [RFC], [BornDate], [Status]) VALUES (2, N'Melanie', N'Flores', N'FOTM980618PI9', CAST(N'1991-05-21T00:00:00.0000000' AS DateTime2), N'Inactive')
INSERT [dbo].[Employee] ([ID], [Name], [LastName], [RFC], [BornDate], [Status]) VALUES (3, N'Alice', N'Guerrero', N'GUPA980618CK5', CAST(N'1997-02-14T00:00:00.0000000' AS DateTime2), N'Active')
INSERT [dbo].[Employee] ([ID], [Name], [LastName], [RFC], [BornDate], [Status]) VALUES (4, N'Alberto ', N'Cruz ', N'CUGA980618N4A', CAST(N'1998-06-18T00:00:00.0000000' AS DateTime2), N'Inactive')
INSERT [dbo].[Employee] ([ID], [Name], [LastName], [RFC], [BornDate], [Status]) VALUES (5, N'María de Jesús', N'Méndez', N'MEOJ980618JT0', CAST(N'1999-12-02T00:00:00.0000000' AS DateTime2), N'Inactive')
INSERT [dbo].[Employee] ([ID], [Name], [LastName], [RFC], [BornDate], [Status]) VALUES (6, N'Juan Manuel', N'Cruz', N'CUPJ980618T9A', CAST(N'2001-11-30T00:00:00.0000000' AS DateTime2), N'Inactive')
INSERT [dbo].[Employee] ([ID], [Name], [LastName], [RFC], [BornDate], [Status]) VALUES (8, N'Alyx', N'Guerrero', N'GUPE980618CK5', CAST(N'1997-02-14T00:00:00.0000000' AS DateTime2), N'NotSet')
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
/****** Object:  Index [IX_Employee_ID]    Script Date: 3/30/2023 3:28:49 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Employee_ID] ON [dbo].[Employee]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Employee] ADD  DEFAULT (N'NotSet') FOR [Status]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Empleyee name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Empleyee last name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'LastName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Empleyee RFC code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'RFC'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Employee born date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'BornDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Employee status' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Employees table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee'
GO
USE [master]
GO
ALTER DATABASE [EmployeeDB] SET  READ_WRITE 
GO
