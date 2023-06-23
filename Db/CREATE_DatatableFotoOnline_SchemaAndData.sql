USE [master]
GO
/****** Object:  Database [FotoOnline]    Script Date: 17/6/2023 17:46:01 ******/
CREATE DATABASE [FotoOnline]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FotoOnline', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\FotoOnline.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FotoOnline_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\FotoOnline_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [FotoOnline] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FotoOnline].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FotoOnline] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FotoOnline] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FotoOnline] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FotoOnline] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FotoOnline] SET ARITHABORT OFF 
GO
ALTER DATABASE [FotoOnline] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FotoOnline] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FotoOnline] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FotoOnline] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FotoOnline] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FotoOnline] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FotoOnline] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FotoOnline] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FotoOnline] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FotoOnline] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FotoOnline] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FotoOnline] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FotoOnline] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FotoOnline] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FotoOnline] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FotoOnline] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FotoOnline] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FotoOnline] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FotoOnline] SET  MULTI_USER 
GO
ALTER DATABASE [FotoOnline] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FotoOnline] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FotoOnline] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FotoOnline] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FotoOnline] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FotoOnline] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [FotoOnline] SET QUERY_STORE = OFF
GO
USE [FotoOnline]
GO
/****** Object:  Table [dbo].[RefreshTokens]    Script Date: 17/6/2023 17:46:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshTokens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Token] [varchar](500) NOT NULL,
	[TokenRefresh] [varchar](500) NOT NULL,
	[ExpiratedDate] [datetime] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UserCreatedId] [int] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[UserModifiedId] [int] NOT NULL,
 CONSTRAINT [PK_RefreshTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 17/6/2023 17:46:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](150) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[FullName] [varchar](200) NOT NULL,
	[Active] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[UserCreatedId] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[UserModifiedId] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[RefreshTokens] ON 

INSERT [dbo].[RefreshTokens] ([Id], [UserId], [Token], [TokenRefresh], [ExpiratedDate], [CreatedDate], [UserCreatedId], [ModifiedDate], [UserModifiedId]) VALUES (1, 1, N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VybmFtZSI6ImFkbWluIiwibmJmIjoxNjg3MDM0MDI0LCJleHAiOjE2ODcwMzQwODQsImlhdCI6MTY4NzAzNDAyNH0.4JrW-zAzqK3e0B-QVdxVaJg92nIFl5Wwa9abPkudtiY', N'kegJRfPdjORwzlMCGB+ThktqhBVbz46nax5dfqbb5YwTOMQPCOzf8plwVL4t+CqIwFeXsN6Q0KUnf86XKnlstw==', CAST(N'2023-06-17T00:00:00.000' AS DateTime), CAST(N'2023-06-17T00:00:00.000' AS DateTime), 9, CAST(N'2023-06-17T00:00:00.000' AS DateTime), 9)
INSERT [dbo].[RefreshTokens] ([Id], [UserId], [Token], [TokenRefresh], [ExpiratedDate], [CreatedDate], [UserCreatedId], [ModifiedDate], [UserModifiedId]) VALUES (2, 1, N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VybmFtZSI6ImFkbWluIiwibmJmIjoxNjg3MDM0MjI3LCJleHAiOjE2ODcwMzQyODcsImlhdCI6MTY4NzAzNDIyN30.9j6v7UBU4JZ6XtJV4c-E0H4XRIZeaFo1Wm1AF43sBWE', N'8ezHrMYHjurz+o7FzVEvxk8jXqhpV/IHN8YPiL2enCc0zRPssHx/AY9WCIAGCUrvrcjB0ndwiLWMqWlfmV02Wg==', CAST(N'2023-06-17T00:00:00.000' AS DateTime), CAST(N'2023-06-17T00:00:00.000' AS DateTime), 9, CAST(N'2023-06-17T00:00:00.000' AS DateTime), 9)
INSERT [dbo].[RefreshTokens] ([Id], [UserId], [Token], [TokenRefresh], [ExpiratedDate], [CreatedDate], [UserCreatedId], [ModifiedDate], [UserModifiedId]) VALUES (3, 1, N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VybmFtZSI6ImFkbWluIiwibmJmIjoxNjg3MDM0NDI5LCJleHAiOjE2ODcwMzQ0ODksImlhdCI6MTY4NzAzNDQyOX0.gsL7e_ERkWKsAyiVbIBU4xu3opAmd3c-wqNwSlaFj34', N'RXU97B5VKPPhV+etYwhmFzc8b4geAzu1GGRwxBz05Zik6JUNzfToQgvmfhHlx6dHmdNMhKktKbjVhmER3zhmGg==', CAST(N'2023-06-17T20:42:30.083' AS DateTime), CAST(N'2023-06-17T17:40:30.083' AS DateTime), 9, CAST(N'2023-06-17T17:40:30.083' AS DateTime), 9)
INSERT [dbo].[RefreshTokens] ([Id], [UserId], [Token], [TokenRefresh], [ExpiratedDate], [CreatedDate], [UserCreatedId], [ModifiedDate], [UserModifiedId]) VALUES (4, 1, N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VybmFtZSI6InN0cmluZyIsIm5iZiI6MTY4NzAzNDU4MCwiZXhwIjoxNjg3MDM0NjQwLCJpYXQiOjE2ODcwMzQ1ODB9.jb3kz96csygrY_KIrQhJElrjtHDuxQpkQkB11WwTYUU', N'rX/cphWzYFdFW1C+Dc//tU/BxQZUAs7JEhNM+Cg24muiKF3tfBGAtDvfsq8nW34KxNafxBW1ZEkbdv1Qobzaog==', CAST(N'2023-06-17T20:45:00.280' AS DateTime), CAST(N'2023-06-17T17:43:00.280' AS DateTime), 9, CAST(N'2023-06-17T17:43:00.280' AS DateTime), 9)
SET IDENTITY_INSERT [dbo].[RefreshTokens] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Username], [Password], [FullName], [Active], [CreatedDate], [UserCreatedId], [ModifiedDate], [UserModifiedId]) VALUES (1, N'admin', N'admin', N'admin', 1, CAST(N'2023-05-22T00:00:00.000' AS DateTime), 9, CAST(N'2023-05-22T00:00:00.000' AS DateTime), 9)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Active]  DEFAULT ((1)) FOR [Active]
GO
USE [master]
GO
ALTER DATABASE [FotoOnline] SET  READ_WRITE 
GO
