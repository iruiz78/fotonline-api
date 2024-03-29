USE [master]
GO
/****** Object:  Database [Fotonline]    Script Date: 23/10/2023 22:31:43 ******/
CREATE DATABASE [Fotonline]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Fotonline', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Fotonline.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Fotonline_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Fotonline_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Fotonline] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Fotonline].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Fotonline] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Fotonline] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Fotonline] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Fotonline] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Fotonline] SET ARITHABORT OFF 
GO
ALTER DATABASE [Fotonline] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Fotonline] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Fotonline] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Fotonline] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Fotonline] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Fotonline] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Fotonline] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Fotonline] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Fotonline] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Fotonline] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Fotonline] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Fotonline] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Fotonline] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Fotonline] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Fotonline] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Fotonline] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Fotonline] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Fotonline] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Fotonline] SET  MULTI_USER 
GO
ALTER DATABASE [Fotonline] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Fotonline] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Fotonline] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Fotonline] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Fotonline] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Fotonline] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Fotonline] SET QUERY_STORE = OFF
GO
USE [Fotonline]
GO
/****** Object:  Table [dbo].[CodeAccounts]    Script Date: 23/10/2023 22:31:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CodeAccounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](150) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[CodeExpiration] [datetime] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UserCreatedId] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[UserModifiedId] [int] NOT NULL,
 CONSTRAINT [PK_CodeAccounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Events]    Script Date: 23/10/2023 22:31:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Events](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Description] [varchar](500) NULL,
	[BannerUrl] [varchar](300) NULL,
	[Active] [bit] NOT NULL,
	[PublicationDate] [datetime] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UserCreatedId] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[UserModifiedId] [int] NOT NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Photos]    Script Date: 23/10/2023 22:31:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Photos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Label] [varchar](500) NULL,
	[UserId] [int] NOT NULL,
	[Url] [varchar](300) NOT NULL,
	[EventId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UserCreatedId] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[UserModifiedId] [int] NOT NULL,
 CONSTRAINT [PK_Photos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefreshTokens]    Script Date: 23/10/2023 22:31:43 ******/
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
	[ModifiedDate] [datetime] NOT NULL,
	[UserModifiedId] [int] NOT NULL,
 CONSTRAINT [PK_RefreshTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleDetails]    Script Date: 23/10/2023 22:31:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SaleId] [int] NOT NULL,
	[PhotoId] [int] NOT NULL,
	[UnitPrice] [decimal](10, 2) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UserCreatedId] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[UserModifiedId] [int] NOT NULL,
 CONSTRAINT [PK_SaleDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 23/10/2023 22:31:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[UserId] [int] NOT NULL,
	[StateId] [int] NOT NULL,
	[Description] [varchar](500) NULL,
	[PaymentType] [int] NOT NULL,
	[PaymentDate] [datetime] NOT NULL,
	[UrlPaymentVoucher] [varchar](300) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UserCreatedId] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[UserModifiedId] [int] NOT NULL,
 CONSTRAINT [PK_Sales] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 23/10/2023 22:31:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](150) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[FullName] [varchar](200) NOT NULL,
	[Active] [bit] NOT NULL,
	[RolId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UserCreatedId] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[UserModifiedId] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersModules]    Script Date: 23/10/2023 22:31:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersModules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Module] [varchar](50) NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UserCreatedId] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[UserModifiedId] [int] NOT NULL,
 CONSTRAINT [PK_UsersModules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Email], [Password], [FullName], [Active], [RolId], [CreatedDate], [UserCreatedId], [ModifiedDate], [UserModifiedId]) VALUES (1, N'admin', N'admin', N'Admin User', 1, 1, CAST(N'2023-10-23T22:26:56.277' AS DateTime), 1, CAST(N'2023-10-23T22:26:56.277' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [NonClusteredIndex-20231023-221003]    Script Date: 23/10/2023 22:31:43 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20231023-221003] ON [dbo].[Users]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[CodeAccounts]  WITH CHECK ADD  CONSTRAINT [FK_CodeAccounts_Users] FOREIGN KEY([UserCreatedId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[CodeAccounts] CHECK CONSTRAINT [FK_CodeAccounts_Users]
GO
ALTER TABLE [dbo].[CodeAccounts]  WITH CHECK ADD  CONSTRAINT [FK_CodeAccounts_Users1] FOREIGN KEY([UserModifiedId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[CodeAccounts] CHECK CONSTRAINT [FK_CodeAccounts_Users1]
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_Users] FOREIGN KEY([UserCreatedId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_Users]
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_Users1] FOREIGN KEY([UserModifiedId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_Users1]
GO
ALTER TABLE [dbo].[Photos]  WITH CHECK ADD  CONSTRAINT [FK_Photos_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Photos] CHECK CONSTRAINT [FK_Photos_Users]
GO
ALTER TABLE [dbo].[Photos]  WITH CHECK ADD  CONSTRAINT [FK_Photos_Users1] FOREIGN KEY([UserCreatedId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Photos] CHECK CONSTRAINT [FK_Photos_Users1]
GO
ALTER TABLE [dbo].[Photos]  WITH CHECK ADD  CONSTRAINT [FK_Photos_Users2] FOREIGN KEY([UserModifiedId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Photos] CHECK CONSTRAINT [FK_Photos_Users2]
GO
ALTER TABLE [dbo].[RefreshTokens]  WITH CHECK ADD  CONSTRAINT [FK_RefreshTokens_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[RefreshTokens] CHECK CONSTRAINT [FK_RefreshTokens_Users]
GO
ALTER TABLE [dbo].[RefreshTokens]  WITH CHECK ADD  CONSTRAINT [FK_RefreshTokens_Users1] FOREIGN KEY([UserCreatedId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[RefreshTokens] CHECK CONSTRAINT [FK_RefreshTokens_Users1]
GO
ALTER TABLE [dbo].[RefreshTokens]  WITH CHECK ADD  CONSTRAINT [FK_RefreshTokens_Users2] FOREIGN KEY([UserModifiedId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[RefreshTokens] CHECK CONSTRAINT [FK_RefreshTokens_Users2]
GO
ALTER TABLE [dbo].[SaleDetails]  WITH CHECK ADD  CONSTRAINT [FK_SaleDetails_Photos] FOREIGN KEY([PhotoId])
REFERENCES [dbo].[Photos] ([Id])
GO
ALTER TABLE [dbo].[SaleDetails] CHECK CONSTRAINT [FK_SaleDetails_Photos]
GO
ALTER TABLE [dbo].[SaleDetails]  WITH CHECK ADD  CONSTRAINT [FK_SaleDetails_Sales] FOREIGN KEY([SaleId])
REFERENCES [dbo].[Sales] ([Id])
GO
ALTER TABLE [dbo].[SaleDetails] CHECK CONSTRAINT [FK_SaleDetails_Sales]
GO
ALTER TABLE [dbo].[SaleDetails]  WITH CHECK ADD  CONSTRAINT [FK_SaleDetails_Users] FOREIGN KEY([UserCreatedId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[SaleDetails] CHECK CONSTRAINT [FK_SaleDetails_Users]
GO
ALTER TABLE [dbo].[SaleDetails]  WITH CHECK ADD  CONSTRAINT [FK_SaleDetails_Users1] FOREIGN KEY([UserModifiedId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[SaleDetails] CHECK CONSTRAINT [FK_SaleDetails_Users1]
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_Sales_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sales_Users]
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_Sales_Users1] FOREIGN KEY([UserCreatedId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sales_Users1]
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_Sales_Users2] FOREIGN KEY([UserModifiedId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sales_Users2]
GO
ALTER TABLE [dbo].[UsersModules]  WITH CHECK ADD  CONSTRAINT [FK_UsersModules_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UsersModules] CHECK CONSTRAINT [FK_UsersModules_Users]
GO
ALTER TABLE [dbo].[UsersModules]  WITH CHECK ADD  CONSTRAINT [FK_UsersModules_Users1] FOREIGN KEY([UserCreatedId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UsersModules] CHECK CONSTRAINT [FK_UsersModules_Users1]
GO
ALTER TABLE [dbo].[UsersModules]  WITH CHECK ADD  CONSTRAINT [FK_UsersModules_Users2] FOREIGN KEY([UserModifiedId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UsersModules] CHECK CONSTRAINT [FK_UsersModules_Users2]
GO
USE [master]
GO
ALTER DATABASE [Fotonline] SET  READ_WRITE 
GO
