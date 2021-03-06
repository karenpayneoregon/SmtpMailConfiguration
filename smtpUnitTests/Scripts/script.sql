USE [master]
GO
/****** Object:  Database [EmailTesting]    Script Date: 9/8/2018 12:19:46 PM ******/
CREATE DATABASE [EmailTesting]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EmailTesting', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\EmailTesting.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'EmailTesting_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\EmailTesting_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [EmailTesting] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EmailTesting].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EmailTesting] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EmailTesting] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EmailTesting] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EmailTesting] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EmailTesting] SET ARITHABORT OFF 
GO
ALTER DATABASE [EmailTesting] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EmailTesting] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [EmailTesting] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EmailTesting] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EmailTesting] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EmailTesting] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EmailTesting] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EmailTesting] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EmailTesting] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EmailTesting] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EmailTesting] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EmailTesting] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EmailTesting] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EmailTesting] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EmailTesting] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EmailTesting] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EmailTesting] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EmailTesting] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EmailTesting] SET RECOVERY FULL 
GO
ALTER DATABASE [EmailTesting] SET  MULTI_USER 
GO
ALTER DATABASE [EmailTesting] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EmailTesting] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EmailTesting] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EmailTesting] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [EmailTesting]
GO
/****** Object:  Table [dbo].[CannedMessages]    Script Date: 9/8/2018 12:19:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CannedMessages](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[HtmlMessage] [nvarchar](max) NULL,
	[TextMessage] [nvarchar](max) NULL,
 CONSTRAINT [PK_CannedMessages] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[CannedMessages] ON 

INSERT [dbo].[CannedMessages] ([id], [Description], [HtmlMessage], [TextMessage]) VALUES (1, N'Very simple HTML', N'This email was sent from a <span style="font-weight: bold;color: brown"> unit test</span> as rich text.', N'This email was sent from a unit test as plain text.')
INSERT [dbo].[CannedMessages] ([id], [Description], [HtmlMessage], [TextMessage]) VALUES (2, N'Has unordered list', N'This email was sent from a <span style="font-weight: bold;color: brown"> unit test</span> as hrml.<ul><li>Apples</li><li>Oranges</li><li>Grapes</li></ul>', N'So sorry you missed seeing this great looking message.')
INSERT [dbo].[CannedMessages] ([id], [Description], [HtmlMessage], [TextMessage]) VALUES (3, N'With table', N'<span style="font-weight: bold;color: green">Unit test</span>.<table style="border: 1px solid black; border-collapse: collapse;" rules="groups"><thead style="border: 2px solid black;"><tr><td>First</td><td>Last</td></tr></thead><tr><td>Karen</td><td>Payne</td></tr><tr><td>Jill</td><td>Jones</td></tr></table>', N'Missing table demo')
INSERT [dbo].[CannedMessages] ([id], [Description], [HtmlMessage], [TextMessage]) VALUES (4, N'Bad host', N'Will never see this', N'Not to be seen')
INSERT [dbo].[CannedMessages] ([id], [Description], [HtmlMessage], [TextMessage]) VALUES (5, N'SQL-Server tib but', N'<h1>Microsoft SQL Server</h1><p style="font-style: italic">From Wikipedia, the free encyclopedia</p>Microsoft SQL Server is a <a href="https://en.wikipedia.org/wiki/Relational_database_management_system">relational database management system</a><a href="https://en.wikipedia.org/wiki/Relational_database_management_system">relational database management system</a> developed by Microsoft. As a database server, it is a software product with the primary function of storing and retrieving data as requested by other software applications—which may run either on the same computer or on another computer across a network (including the Internet).
', N'About SQL-Server')
SET IDENTITY_INSERT [dbo].[CannedMessages] OFF
USE [master]
GO
ALTER DATABASE [EmailTesting] SET  READ_WRITE 
GO
