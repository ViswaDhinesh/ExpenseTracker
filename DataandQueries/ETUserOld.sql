USE [ExpenseTracker]
GO

/****** Object:  Table [dbo].[ETUser]    Script Date: 22-07-2020 12:22:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ETUser](
	[UserUniqueID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [bigint] NOT NULL,
	[Title] [nvarchar](20) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[MiddleName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Gender] [nvarchar](1) NOT NULL,
	[MaritalStatus] [nvarchar](1) NOT NULL,
	[DOB] [datetime] NOT NULL,
	[Address] [nvarchar](250) NOT NULL,
	[RoleID] [bigint] NOT NULL,
	[LoginName] [nvarchar](10) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[IsTwoFactor] [bit] NOT NULL,
	[IsOwner] [bit] NOT NULL,
	[IsAdmin] [bit] NOT NULL,
	[IsManager] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedUserID] [bigint] NULL,
	[Otp] [nvarchar](10) NULL,
	[DeviceID] [nvarchar](max) NULL,
	[UserField1] [nvarchar](max) NULL,
	[UserField2] [nvarchar](max) NULL,
	[UserField3] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
 CONSTRAINT [PK_ETUsers] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


