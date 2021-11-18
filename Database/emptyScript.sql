USE [master]
GO
/****** Object:  Database [BugDB]    Script Date: 18/11/2021 12:21:32 ******/
CREATE DATABASE [BugDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BugDB', FILENAME = N'D:\Microsoft SQL Server\MSSQL15.SQLEXPRESS01\MSSQL\DATA\BugDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BugDB_log', FILENAME = N'D:\Microsoft SQL Server\MSSQL15.SQLEXPRESS01\MSSQL\DATA\BugDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BugDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BugDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BugDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BugDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BugDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BugDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BugDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [BugDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BugDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BugDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BugDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BugDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BugDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BugDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BugDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BugDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BugDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BugDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BugDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BugDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BugDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BugDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BugDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [BugDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BugDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BugDB] SET  MULTI_USER 
GO
ALTER DATABASE [BugDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BugDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BugDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BugDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BugDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BugDB] SET QUERY_STORE = OFF
GO
USE [BugDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 18/11/2021 12:21:32 ******/
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
/****** Object:  Table [dbo].[Assignments]    Script Date: 18/11/2021 12:21:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assignments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[HourlyRate] [int] NOT NULL,
	[Duration] [float] NOT NULL,
	[ProjectId] [int] NOT NULL,
 CONSTRAINT [PK_Assignments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bugs]    Script Date: 18/11/2021 12:21:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bugs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Version] [nvarchar](max) NULL,
	[State] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[FixerId] [int] NULL,
	[FixingTime] [int] NOT NULL,
 CONSTRAINT [PK_Bugs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 18/11/2021 12:21:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectUser]    Script Date: 18/11/2021 12:21:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectUser](
	[ProjectsId] [int] NOT NULL,
	[UsersId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectUser] PRIMARY KEY CLUSTERED 
(
	[ProjectsId] ASC,
	[UsersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 18/11/2021 12:21:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[UserName] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Role] [int] NOT NULL,
	[Token] [nvarchar](max) NULL,
	[HourlyRate] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_Assignments_ProjectId]    Script Date: 18/11/2021 12:21:32 ******/
CREATE NONCLUSTERED INDEX [IX_Assignments_ProjectId] ON [dbo].[Assignments]
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Bugs_FixerId]    Script Date: 18/11/2021 12:21:32 ******/
CREATE NONCLUSTERED INDEX [IX_Bugs_FixerId] ON [dbo].[Bugs]
(
	[FixerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Bugs_ProjectId]    Script Date: 18/11/2021 12:21:32 ******/
CREATE NONCLUSTERED INDEX [IX_Bugs_ProjectId] ON [dbo].[Bugs]
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProjectUser_UsersId]    Script Date: 18/11/2021 12:21:32 ******/
CREATE NONCLUSTERED INDEX [IX_ProjectUser_UsersId] ON [dbo].[ProjectUser]
(
	[UsersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bugs] ADD  DEFAULT ((0)) FOR [ProjectId]
GO
ALTER TABLE [dbo].[Bugs] ADD  DEFAULT ((0)) FOR [FixingTime]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [HourlyRate]
GO
ALTER TABLE [dbo].[Assignments]  WITH CHECK ADD  CONSTRAINT [FK_Assignments_Projects_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Assignments] CHECK CONSTRAINT [FK_Assignments_Projects_ProjectId]
GO
ALTER TABLE [dbo].[Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Projects_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bugs] CHECK CONSTRAINT [FK_Bugs_Projects_ProjectId]
GO
ALTER TABLE [dbo].[Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Users_FixerId] FOREIGN KEY([FixerId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bugs] CHECK CONSTRAINT [FK_Bugs_Users_FixerId]
GO
ALTER TABLE [dbo].[ProjectUser]  WITH CHECK ADD  CONSTRAINT [FK_ProjectUser_Projects_ProjectsId] FOREIGN KEY([ProjectsId])
REFERENCES [dbo].[Projects] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectUser] CHECK CONSTRAINT [FK_ProjectUser_Projects_ProjectsId]
GO
ALTER TABLE [dbo].[ProjectUser]  WITH CHECK ADD  CONSTRAINT [FK_ProjectUser_Users_UsersId] FOREIGN KEY([UsersId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectUser] CHECK CONSTRAINT [FK_ProjectUser_Users_UsersId]
GO
USE [master]
GO
ALTER DATABASE [BugDB] SET  READ_WRITE 
GO
