USE [Fotonline]
GO

/****** Object:  Table [dbo].[RecoveryCode]    Script Date: 25/11/2023 22:42:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RecoveryCode](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](150) NULL,
	[Code] [varchar](200) NULL,
	[DateExpiration] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[UserCreatedId] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[UserModifiedId] [int] NULL,
 CONSTRAINT [PK_RecoveryCode] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
