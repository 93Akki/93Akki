USE [master]
GO
/****** Object:  Database [SBS]    Script Date: 10/25/2021 8:58:43 AM ******/
CREATE DATABASE [SBS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SBS', FILENAME = N'D:\SystemDB\MSSQL12.MSSQLSERVER\MSSQL\DATA\SBS.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SBS_log', FILENAME = N'D:\SystemDB\MSSQL12.MSSQLSERVER\MSSQL\DATA\SBS_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SBS] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SBS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SBS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SBS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SBS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SBS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SBS] SET ARITHABORT OFF 
GO
ALTER DATABASE [SBS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SBS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SBS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SBS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SBS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SBS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SBS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SBS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SBS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SBS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SBS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SBS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SBS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SBS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SBS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SBS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SBS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SBS] SET RECOVERY FULL 
GO
ALTER DATABASE [SBS] SET  MULTI_USER 
GO
ALTER DATABASE [SBS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SBS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SBS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SBS] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [SBS] SET DELAYED_DURABILITY = DISABLED 
GO
USE [SBS]
GO
/****** Object:  User [SBS]    Script Date: 10/25/2021 8:58:43 AM ******/
CREATE USER [SBS] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [SBS]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [SBS]
GO
ALTER ROLE [db_datareader] ADD MEMBER [SBS]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [SBS]
GO
/****** Object:  Schema [SBS]    Script Date: 10/25/2021 8:58:44 AM ******/
CREATE SCHEMA [SBS]
GO
/****** Object:  UserDefinedFunction [dbo].[GenPass]    Script Date: 10/25/2021 8:58:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GenPass]()
RETURNS VARCHAR(8)
AS
BEGIN
   -- Declare the variables here
   DECLARE @Result VARCHAR(8)
   DECLARE @BinaryData VARBINARY(8)
   DECLARE @CharacterData VARCHAR(8)
 
   SELECT @BinaryData = randval
   FROM vRandom
 
   Set @CharacterData=cast ('' as xml).value ('xs:base64Binary(sql:variable("@BinaryData"))',
                   'varchar (max)')
   
   SET @Result = @CharacterData
   
   -- Return the result of the function
   RETURN @Result
END

GO
/****** Object:  Table [dbo].[CityMaster]    Script Date: 10/25/2021 8:58:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CityMaster](
	[CityID] [bigint] IDENTITY(1,1) NOT NULL,
	[CityName] [varchar](50) NULL,
	[StateID] [bigint] NULL,
	[CreationDate] [datetime] NULL,
	[ModifiyDate] [datetime] NULL,
 CONSTRAINT [PK_CityMaster] PRIMARY KEY CLUSTERED 
(
	[CityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CountryMaster]    Script Date: 10/25/2021 8:58:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CountryMaster](
	[CountryID] [bigint] IDENTITY(1,1) NOT NULL,
	[CountryName] [varchar](50) NULL,
	[CreationDate] [datetime] NULL,
	[ModifiyDate] [datetime] NULL,
 CONSTRAINT [PK_CountryMaster] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SBS_CategoryMaster]    Script Date: 10/25/2021 8:58:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SBS_CategoryMaster](
	[catID] [bigint] IDENTITY(1,1) NOT NULL,
	[CatName] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
	[CreationDate] [datetime] NULL,
	[ModifiyDate] [datetime] NULL,
	[CatSection] [varchar](5) NULL,
PRIMARY KEY CLUSTERED 
(
	[catID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SBS_Employee_EducationCertificates]    Script Date: 10/25/2021 8:58:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SBS_Employee_EducationCertificates](
	[DocID] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [bigint] NULL,
	[EducationCertificates] [nvarchar](max) NULL,
	[CreationDate] [datetime] NULL,
	[ModifiyDate] [datetime] NULL,
 CONSTRAINT [PK_SBS_Employee_EducationCertificates] PRIMARY KEY CLUSTERED 
(
	[DocID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SBS_Employee_IDProofs]    Script Date: 10/25/2021 8:58:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SBS_Employee_IDProofs](
	[DocID] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [bigint] NULL,
	[IDProofs] [nvarchar](max) NULL,
	[CreationDate] [datetime] NULL,
	[ModifiyDate] [datetime] NULL,
 CONSTRAINT [PK_SBS_Employee_IDProofs] PRIMARY KEY CLUSTERED 
(
	[DocID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SBS_EmployeeMaster]    Script Date: 10/25/2021 8:58:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SBS_EmployeeMaster](
	[EmployeeID] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Mobile] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Gender] [int] NULL,
	[DOB] [varchar](50) NULL,
	[City] [int] NULL,
	[State] [int] NULL,
	[Country] [int] NULL,
	[Status] [varchar](50) NULL,
	[CreationDate] [datetime] NULL,
	[ModifiyDate] [datetime] NULL,
	[Department] [varchar](50) NULL,
	[PossportPhoto] [nvarchar](max) NULL,
 CONSTRAINT [PK_SBS_EmployeeMaster] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SBS_Login]    Script Date: 10/25/2021 8:58:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SBS_Login](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[IsActive] [bit] NULL,
	[LastLogin] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SBS_MemberKYCDetails]    Script Date: 10/25/2021 8:58:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SBS_MemberKYCDetails](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[MemberID] [nvarchar](50) NULL,
	[KYC_AadharCard] [nvarchar](max) NULL,
	[KYC_PanCard] [nvarchar](max) NULL,
	[CreationDate] [datetime] NULL,
	[ModifiyDate] [datetime] NULL,
	[Verified] [bit] NULL,
	[VerifiedBy] [varchar](100) NULL,
	[PassportPhoto] [nvarchar](max) NULL,
	[KYC_AadharCard_back] [nvarchar](max) NULL,
	[KYC_PanCard_back] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SBS_ProductDetail]    Script Date: 10/25/2021 8:58:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SBS_ProductDetail](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[PID] [bigint] NULL,
	[SubimgPath] [nvarchar](max) NULL,
	[CreationDate] [datetime] NULL,
	[MoifiyDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SBS_ProductMaster]    Script Date: 10/25/2021 8:58:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SBS_ProductMaster](
	[PID] [bigint] IDENTITY(1,1) NOT NULL,
	[PName] [nvarchar](500) NULL,
	[marketRate] [decimal](18, 2) NULL,
	[sbsRate] [decimal](18, 2) NULL,
	[imgPath] [nvarchar](max) NULL,
	[catID] [bigint] NULL,
	[IsActive] [bit] NULL,
	[IsAddToCart] [bit] NULL,
	[CreationDate] [datetime] NULL,
	[ModifiyDate] [datetime] NULL,
	[ProductDescription] [nvarchar](max) NULL,
	[DocPath] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[PID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SBS_RegistrationMaster]    Script Date: 10/25/2021 8:58:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SBS_RegistrationMaster](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Mobile] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[Gender] [bit] NULL,
	[City] [nvarchar](10) NULL,
	[Country] [nvarchar](10) NULL,
	[SBSMemberID] [nvarchar](50) NULL,
	[SBSPassword] [nvarchar](10) NULL,
	[IsActive] [bit] NULL,
	[CreationDate] [datetime] NULL,
	[ModifiyDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StateMaster]    Script Date: 10/25/2021 8:58:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StateMaster](
	[StateID] [bigint] IDENTITY(1,1) NOT NULL,
	[StateName] [varchar](50) NULL,
	[CountryID] [bigint] NULL,
	[CreationDate] [datetime] NULL,
	[ModifiyDate] [datetime] NULL,
 CONSTRAINT [PK_StateMaster] PRIMARY KEY CLUSTERED 
(
	[StateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[CityMaster] ON 

INSERT [dbo].[CityMaster] ([CityID], [CityName], [StateID], [CreationDate], [ModifiyDate]) VALUES (1, N'Jodhpur', 1, CAST(N'2021-04-03 16:19:23.777' AS DateTime), CAST(N'2021-04-03 16:19:23.777' AS DateTime))
SET IDENTITY_INSERT [dbo].[CityMaster] OFF
SET IDENTITY_INSERT [dbo].[CountryMaster] ON 

INSERT [dbo].[CountryMaster] ([CountryID], [CountryName], [CreationDate], [ModifiyDate]) VALUES (1, N'India', CAST(N'2021-04-03 16:14:48.647' AS DateTime), CAST(N'2021-04-03 16:14:48.647' AS DateTime))
SET IDENTITY_INSERT [dbo].[CountryMaster] OFF
SET IDENTITY_INSERT [dbo].[SBS_CategoryMaster] ON 

INSERT [dbo].[SBS_CategoryMaster] ([catID], [CatName], [IsActive], [CreationDate], [ModifiyDate], [CatSection]) VALUES (1, N'Grocery', 1, CAST(N'2020-09-29 02:48:43.570' AS DateTime), CAST(N'2020-09-29 02:48:43.570' AS DateTime), N'A')
INSERT [dbo].[SBS_CategoryMaster] ([catID], [CatName], [IsActive], [CreationDate], [ModifiyDate], [CatSection]) VALUES (2, N'Electric Scooter', 1, CAST(N'2020-09-29 02:48:52.883' AS DateTime), CAST(N'2020-09-29 02:48:52.883' AS DateTime), N'B')
INSERT [dbo].[SBS_CategoryMaster] ([catID], [CatName], [IsActive], [CreationDate], [ModifiyDate], [CatSection]) VALUES (5, N'Mens Shirts', 1, CAST(N'2021-04-18 02:03:11.300' AS DateTime), CAST(N'2021-04-18 02:03:11.300' AS DateTime), N'A')
SET IDENTITY_INSERT [dbo].[SBS_CategoryMaster] OFF
SET IDENTITY_INSERT [dbo].[SBS_Employee_EducationCertificates] ON 

INSERT [dbo].[SBS_Employee_EducationCertificates] ([DocID], [EmployeeID], [EducationCertificates], [CreationDate], [ModifiyDate]) VALUES (1, 1, N'~/SBSEmployee_Documents/EmpID_1_Documents/photo4.jpg', CAST(N'2021-04-17 19:47:29.537' AS DateTime), CAST(N'2021-04-17 19:47:30.017' AS DateTime))
INSERT [dbo].[SBS_Employee_EducationCertificates] ([DocID], [EmployeeID], [EducationCertificates], [CreationDate], [ModifiyDate]) VALUES (2, 1, N'~/SBSEmployee_Documents/EmpID_1_Documents/user1-128x128.jpg', CAST(N'2021-04-17 19:47:44.097' AS DateTime), CAST(N'2021-04-17 19:47:44.410' AS DateTime))
INSERT [dbo].[SBS_Employee_EducationCertificates] ([DocID], [EmployeeID], [EducationCertificates], [CreationDate], [ModifiyDate]) VALUES (3, 1, N'~/SBSEmployee_Documents/EmpID_1_Documents/user2-160x160.jpg', CAST(N'2021-04-17 19:47:53.767' AS DateTime), CAST(N'2021-04-17 19:47:54.007' AS DateTime))
SET IDENTITY_INSERT [dbo].[SBS_Employee_EducationCertificates] OFF
SET IDENTITY_INSERT [dbo].[SBS_Employee_IDProofs] ON 

INSERT [dbo].[SBS_Employee_IDProofs] ([DocID], [EmployeeID], [IDProofs], [CreationDate], [ModifiyDate]) VALUES (1, 1, N'~/SBSEmployee_Documents/EmpID_1_Documents/user3-128x128.jpg', CAST(N'2021-04-17 19:47:59.837' AS DateTime), CAST(N'2021-04-17 19:48:00.067' AS DateTime))
INSERT [dbo].[SBS_Employee_IDProofs] ([DocID], [EmployeeID], [IDProofs], [CreationDate], [ModifiyDate]) VALUES (2, 1, N'~/SBSEmployee_Documents/EmpID_1_Documents/user4-128x128.jpg', CAST(N'2021-04-17 19:48:03.933' AS DateTime), CAST(N'2021-04-17 19:48:04.177' AS DateTime))
INSERT [dbo].[SBS_Employee_IDProofs] ([DocID], [EmployeeID], [IDProofs], [CreationDate], [ModifiyDate]) VALUES (3, 1, N'~/SBSEmployee_Documents/EmpID_1_Documents/user5-128x128.jpg', CAST(N'2021-04-17 19:48:08.053' AS DateTime), CAST(N'2021-04-17 19:48:08.353' AS DateTime))
SET IDENTITY_INSERT [dbo].[SBS_Employee_IDProofs] OFF
SET IDENTITY_INSERT [dbo].[SBS_EmployeeMaster] ON 

INSERT [dbo].[SBS_EmployeeMaster] ([EmployeeID], [FirstName], [LastName], [Mobile], [Email], [Gender], [DOB], [City], [State], [Country], [Status], [CreationDate], [ModifiyDate], [Department], [PossportPhoto]) VALUES (1, N'Akshay', N'Joshi', N'8562867092', N'joshaiakshjs@gmci.com', 1, N'04/17/2021', 1, 1, 1, N'Active', CAST(N'2021-04-17 19:47:20.437' AS DateTime), CAST(N'2021-04-17 19:47:20.740' AS DateTime), N'Admin', N'~/SBSEmployee_Documents/EmpID_1_Documents/photo4.jpg')
SET IDENTITY_INSERT [dbo].[SBS_EmployeeMaster] OFF
SET IDENTITY_INSERT [dbo].[SBS_Login] ON 

INSERT [dbo].[SBS_Login] ([UserId], [UserName], [Password], [IsActive], [LastLogin]) VALUES (1, N'Admin', N'Admin@123', 1, CAST(N'2021-04-18 00:31:34.717' AS DateTime))
SET IDENTITY_INSERT [dbo].[SBS_Login] OFF
SET IDENTITY_INSERT [dbo].[SBS_MemberKYCDetails] ON 

INSERT [dbo].[SBS_MemberKYCDetails] ([ID], [MemberID], [KYC_AadharCard], [KYC_PanCard], [CreationDate], [ModifiyDate], [Verified], [VerifiedBy], [PassportPhoto], [KYC_AadharCard_back], [KYC_PanCard_back]) VALUES (1, N'00000001', N'~/KYCDocument/00000001/Screenshot (2).png', N'~/KYCDocument/00000001/Screenshot (4).png', CAST(N'2021-03-28 14:07:18.037' AS DateTime), CAST(N'2021-03-28 14:07:18.037' AS DateTime), 1, NULL, N'~/KYCDocument/00000001/Screenshot (1).png', N'~/KYCDocument/00000001/Screenshot (3).png', N'~/KYCDocument/00000001/Screenshot (10).png')
INSERT [dbo].[SBS_MemberKYCDetails] ([ID], [MemberID], [KYC_AadharCard], [KYC_PanCard], [CreationDate], [ModifiyDate], [Verified], [VerifiedBy], [PassportPhoto], [KYC_AadharCard_back], [KYC_PanCard_back]) VALUES (2, N'00000002', N'~/KYCDocument/00000002/Screenshot (11).png', N'~/KYCDocument/00000002/Screenshot (11).png', CAST(N'2021-03-28 14:51:23.710' AS DateTime), CAST(N'2021-03-28 14:51:23.710' AS DateTime), NULL, NULL, N'~/KYCDocument/00000002/Screenshot (9).png', N'~/KYCDocument/00000002/Screenshot (2).png', N'~/KYCDocument/00000002/Screenshot (10).png')
SET IDENTITY_INSERT [dbo].[SBS_MemberKYCDetails] OFF
SET IDENTITY_INSERT [dbo].[SBS_ProductDetail] ON 

INSERT [dbo].[SBS_ProductDetail] ([ID], [PID], [SubimgPath], [CreationDate], [MoifiyDate]) VALUES (1, 1, N'/Content/images/products/download (8).jpg', CAST(N'2020-11-21 03:57:10.027' AS DateTime), CAST(N'2020-11-21 03:57:10.027' AS DateTime))
INSERT [dbo].[SBS_ProductDetail] ([ID], [PID], [SubimgPath], [CreationDate], [MoifiyDate]) VALUES (2, 1, N'/Content/images/products/download (9).jpg', CAST(N'2020-11-21 03:57:14.887' AS DateTime), CAST(N'2020-11-21 03:57:14.887' AS DateTime))
INSERT [dbo].[SBS_ProductDetail] ([ID], [PID], [SubimgPath], [CreationDate], [MoifiyDate]) VALUES (3, 1, N'/Content/images/products/download (10).jpg', CAST(N'2020-11-21 03:57:19.670' AS DateTime), CAST(N'2020-11-21 03:57:19.670' AS DateTime))
INSERT [dbo].[SBS_ProductDetail] ([ID], [PID], [SubimgPath], [CreationDate], [MoifiyDate]) VALUES (4, 1, N'/Content/images/products/download (11).jpg', CAST(N'2020-11-21 03:57:24.093' AS DateTime), CAST(N'2020-11-21 03:57:24.093' AS DateTime))
INSERT [dbo].[SBS_ProductDetail] ([ID], [PID], [SubimgPath], [CreationDate], [MoifiyDate]) VALUES (5, 1, N'/Content/images/products/download (12).jpg', CAST(N'2020-11-21 03:57:27.937' AS DateTime), CAST(N'2020-11-21 03:57:27.937' AS DateTime))
SET IDENTITY_INSERT [dbo].[SBS_ProductDetail] OFF
SET IDENTITY_INSERT [dbo].[SBS_ProductMaster] ON 

INSERT [dbo].[SBS_ProductMaster] ([PID], [PName], [marketRate], [sbsRate], [imgPath], [catID], [IsActive], [IsAddToCart], [CreationDate], [ModifiyDate], [ProductDescription], [DocPath]) VALUES (1, N'Gopi Poha', CAST(80.00 AS Decimal(18, 2)), CAST(45.00 AS Decimal(18, 2)), N'/Content/images/products/8908007828042_f.jpg', 1, 1, 0, CAST(N'2020-09-29 05:05:24.433' AS DateTime), CAST(N'2020-09-29 05:05:24.433' AS DateTime), NULL, NULL)
INSERT [dbo].[SBS_ProductMaster] ([PID], [PName], [marketRate], [sbsRate], [imgPath], [catID], [IsActive], [IsAddToCart], [CreationDate], [ModifiyDate], [ProductDescription], [DocPath]) VALUES (2, N'Swastik Poha', CAST(75.00 AS Decimal(18, 2)), CAST(45.00 AS Decimal(18, 2)), N'/Content/images/products/8906092170060_f.jpg', 1, 1, 0, CAST(N'2020-09-29 05:50:25.693' AS DateTime), CAST(N'2020-09-29 05:50:25.693' AS DateTime), NULL, NULL)
INSERT [dbo].[SBS_ProductMaster] ([PID], [PName], [marketRate], [sbsRate], [imgPath], [catID], [IsActive], [IsAddToCart], [CreationDate], [ModifiyDate], [ProductDescription], [DocPath]) VALUES (6, N'Techo Electra', CAST(0.00 AS Decimal(18, 2)), CAST(73000.00 AS Decimal(18, 2)), N'/Content/images/products/download (6).jpg', 2, 1, 0, CAST(N'2020-11-21 01:27:37.380' AS DateTime), CAST(N'2020-11-21 01:27:37.380' AS DateTime), N'', N'')
INSERT [dbo].[SBS_ProductMaster] ([PID], [PName], [marketRate], [sbsRate], [imgPath], [catID], [IsActive], [IsAddToCart], [CreationDate], [ModifiyDate], [ProductDescription], [DocPath]) VALUES (7, N'Techo Electra', CAST(0.00 AS Decimal(18, 2)), CAST(73000.00 AS Decimal(18, 2)), N'/Content/images/products/download (7).jpg', 2, 1, 0, CAST(N'2020-11-21 01:27:49.147' AS DateTime), CAST(N'2020-11-21 01:27:49.147' AS DateTime), N'', N'')
SET IDENTITY_INSERT [dbo].[SBS_ProductMaster] OFF
SET IDENTITY_INSERT [dbo].[SBS_RegistrationMaster] ON 

INSERT [dbo].[SBS_RegistrationMaster] ([ID], [FirstName], [LastName], [Mobile], [Email], [Gender], [City], [Country], [SBSMemberID], [SBSPassword], [IsActive], [CreationDate], [ModifiyDate]) VALUES (1, N'Akshay', N'Joshi', N'8562867092', N'Joshiakshayjoshi421@gmail.com', 0, N'Jodhpur', N'India', N'00000001', N'h-_wN*F^%m', 1, CAST(N'2021-03-28 13:45:09.107' AS DateTime), CAST(N'2021-03-28 13:45:09.107' AS DateTime))
INSERT [dbo].[SBS_RegistrationMaster] ([ID], [FirstName], [LastName], [Mobile], [Email], [Gender], [City], [Country], [SBSMemberID], [SBSPassword], [IsActive], [CreationDate], [ModifiyDate]) VALUES (2, N'Akki', N'joshi', N'9024167455', N'joshiakshayjojkjshi421@gmail.com', 0, N'jodhpur', N'India', N'00000002', N'Ay2"u$S(_N', 0, CAST(N'2021-03-28 14:49:29.350' AS DateTime), CAST(N'2021-03-28 14:49:29.350' AS DateTime))
SET IDENTITY_INSERT [dbo].[SBS_RegistrationMaster] OFF
SET IDENTITY_INSERT [dbo].[StateMaster] ON 

INSERT [dbo].[StateMaster] ([StateID], [StateName], [CountryID], [CreationDate], [ModifiyDate]) VALUES (1, N'Rajasthan', 1, CAST(N'2021-04-03 16:18:23.280' AS DateTime), CAST(N'2021-04-03 16:18:23.280' AS DateTime))
SET IDENTITY_INSERT [dbo].[StateMaster] OFF
/****** Object:  StoredProcedure [dbo].[SBS_GetDashboardData]    Script Date: 10/25/2021 8:58:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SBS_GetDashboardData]
@Criteria NVARCHAR(200) =''
AS
BEGIN
     
	 DECLARE @NewMember varchar(100) =''
	 DECLARE @SBSMember varchar(100) =''
	 DECLARE @SBSEmployee varchar(100) =''

	 CREATE TABLE #tmpTilesData
	 ( 
	    NewMember varchar(100),
	    SBSMember varchar(100),   
	    SBSEmployee varchar(100)   
	 )

	 SELECT @NewMember = COUNT(*) FROM SBS_RegistrationMaster WHERE ISNULL(isActive,0) = 0
	 SELECT @SBSMember = COUNT(*) FROM SBS_RegistrationMaster WHERE ISNULL(isActive,0) = 1
	 SELECT @SBSEmployee = COUNT(*) FROM SBS_EmployeeMaster WHERE Status = 'Active'

	 INSERT INTO #tmpTilesData (NewMember, SBSMember,SBSEmployee) VALUES (@NewMember,@SBSMember,@SBSEmployee)


	 SELECT * FROM #tmpTilesData
    
END



GO
/****** Object:  StoredProcedure [dbo].[SBS_GetEmployeeDetails]    Script Date: 10/25/2021 8:58:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SBS_GetEmployeeDetails] 
@Criteria NVARCHAR(200) =''
AS
BEGIN
      DECLARE @Sql NVARCHAR(MAX) = ''

	  IF @Criteria ='Detail'
	  BEGIN
	  	    SET @Sql ='SELECT ROW_NUMBER() OVER (ORDER BY EmployeeID) As sNo 
						  ,[EmployeeID]
						  ,[FirstName]
						  ,[LastName]
						  ,[Mobile]
						  ,[Email]
						  ,[Gender]
						  ,[DOB]
						  ,Department
						  ,CityID
						  ,CityName
						  ,StateMaster.StateID
						  ,StateName
						  ,CountryMaster.CountryID
						  ,CountryName
						  ,[Status]
						  ,SBS_EmployeeMaster.[CreationDate]
						  ,SBS_EmployeeMaster.[ModifiyDate]
					  FROM [dbo].[SBS_EmployeeMaster]
					  INNER JOIN CountryMaster ON Country = CountryID
					  INNER JOIN StateMaster ON State = StateID
					  INNER JOIN CityMaster ON City = CityID'
	  END
	  ELSE IF @Criteria ='SBSMembers'
	  BEGIN
	  	    SET @Sql ='SELECT 
			            ROW_NUMBER() OVER (ORDER BY ID) As sNo,
			            ID,
						FirstName,
						LastName,
						Mobile,	
						Email,	
						CASE WHEN Gender = 0 THEN ''Male'' ELSE ''Female'' END Gender,	
						City,	
						Country,	
						SBSMemberID,	
						SBSPassword,	
						CASE WHEN IsActive = 0 THEN ''In-Active'' ELSE ''Active'' END IsActive,	
						CreationDate,	
						ModifiyDate
						FROM SBS_RegistrationMaster WHERE IsActive = 1'
	  END
	  ELSE 
	  BEGIN
	  	    SET @Sql =''
	  END

	  
	  EXEC (@Sql)
	
END




GO
/****** Object:  StoredProcedure [dbo].[SBS_GetMemberDetails]    Script Date: 10/25/2021 8:58:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SBS_GetMemberDetails] 
@Criteria NVARCHAR(200) =''
AS
BEGIN
      DECLARE @Sql NVARCHAR(MAX) = ''

	  IF @Criteria ='NewMembers'
	  BEGIN
	  	    SET @Sql ='SELECT 
			            ROW_NUMBER() OVER (ORDER BY ID) As sNo,
			            ID,
						FirstName,
						LastName,
						Mobile,	
						Email,	
						CASE WHEN Gender = 0 THEN ''Male'' ELSE ''Female'' END Gender,	
						City,	
						Country,	
						SBSMemberID,	
						SBSPassword,	
						CASE WHEN IsActive = 0 THEN ''In-Active'' ELSE ''Active'' END IsActive,	
						CreationDate,	
						ModifiyDate,
						(select CASE WHEN COUNT(*) > 0 THEN ''True'' ELSE ''False'' END from SBS_MemberKYCDetails WHERE MemberID =SBSMemberID)	AS IsKYCFilled
						FROM SBS_RegistrationMaster WHERE IsActive = 0'
	  END
	  ELSE IF @Criteria ='SBSMembers'
	  BEGIN
	  	    SET @Sql ='SELECT 
			            ROW_NUMBER() OVER (ORDER BY ID) As sNo,
			            ID,
						FirstName,
						LastName,
						Mobile,	
						Email,	
						CASE WHEN Gender = 0 THEN ''Male'' ELSE ''Female'' END Gender,	
						City,	
						Country,	
						SBSMemberID,	
						SBSPassword,	
						CASE WHEN IsActive = 0 THEN ''In-Active'' ELSE ''Active'' END IsActive,	
						CreationDate,	
						ModifiyDate
						FROM SBS_RegistrationMaster WHERE IsActive = 1'
	  END
	  ELSE 
	  BEGIN
	  	    SET @Sql ='SELECT Retbl.ID,
						Retbl.FirstName,
						Retbl.LastName,
						Retbl.Mobile,	
						Retbl.Email,	
						CASE WHEN Retbl.Gender = 0 THEN ''Male'' ELSE ''Female'' END Gender,	
						Retbl.City,	
						Retbl.Country,	
						Retbl.SBSMemberID,	
						Retbl.SBSPassword,	
						CASE WHEN Retbl.IsActive = 0 THEN ''In-Active'' ELSE ''Active'' END IsActive,	
						Retbl.CreationDate,	
						Retbl.ModifiyDate,

						KYC.ID AS KYC_ID,
						KYC.MemberID AS KYC_MemberID,
						KYC.KYC_AadharCard,	
						KYC.KYC_PanCard,	
						KYC.CreationDate AS KYC_CreationDate,	
						KYC.ModifiyDate AS KYC_ModifiyDate,	
						ISNULL(KYC.Verified,0) AS Verified,	
						ISNULL(KYC.VerifiedBy,'''') AS VerifiedBy,	
						KYC.PassportPhoto,	
						KYC.KYC_AadharCard_back,	
						KYC.KYC_PanCard_back
						FROM SBS_RegistrationMaster AS Retbl
						LEFT JOIN SBS_MemberKYCDetails AS KYC ON Retbl.SBSMemberID =KYC.MemberID  WHERE Retbl.SBSMemberID ='+@Criteria+''
	  END

	  
	  EXEC (@Sql)
	
END




GO
/****** Object:  StoredProcedure [dbo].[SBS_GetMemberKYCDetail]    Script Date: 10/25/2021 8:58:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SBS_GetMemberKYCDetail]
@Criteria NVARCHAR(50) =''
AS
BEGIN
	  SELECT  FirstName ,
	          LastName ,
	          Mobile ,
	          Email ,
	          Gender ,
	          City ,
	          Country ,
	          SBSMemberID ,	          
	          IsActive FROM SBS_RegistrationMaster WHERE SBSMemberID = @Criteria
END

GO
/****** Object:  StoredProcedure [dbo].[SBS_GetProductCategory]    Script Date: 10/25/2021 8:58:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SBS_GetProductCategory]
@Criteria NVARCHAR(MAX)=''
AS
BEGIN	
        DECLARE @sSql NVARCHAR(MAX) =''
		SET @sSql = N'SELECT catID,CatName,CatSection FROM SBS_CategoryMaster WHERE IsActive = 1 '+@Criteria
		EXEC(@sSql)

END

GO
/****** Object:  StoredProcedure [dbo].[SBS_GetProducts]    Script Date: 10/25/2021 8:58:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SBS_GetProducts]
@Criteria NVARCHAR(MAX)='',
@CallBy NVARCHAR(50) =''
AS
BEGIN	
      -- EXEC SBS_GetProducts @Criteria = ' AND PID = ''1''',@CallBy='ProductDetail'
        DECLARE @sSql NVARCHAR(MAX) =''

		IF @CallBy ='Product'
		BEGIN
			SET @sSql = N' SELECT   PID ,
						PName ,
						marketRate ,
						sbsRate ,
						imgPath ,
						SBS_ProductMaster.catID ,
						CatName,                
						IsAddToCart,
						ProductDescription
					FROM SBS_ProductMaster
					INNER JOIN SBS_CategoryMaster ON SBS_CategoryMaster.catID = SBS_ProductMaster.catID WHERE SBS_ProductMaster.IsActive = 1 '+@Criteria
		END
		ELSE
		BEGIN
			SET @sSql = N' SELECT   PID ,
						PName ,
						marketRate ,
						sbsRate ,
						imgPath ,
						SBS_ProductMaster.catID ,
						CatName,                
						IsAddToCart,
						ProductDescription
					FROM SBS_ProductMaster
					INNER JOIN SBS_CategoryMaster ON SBS_CategoryMaster.catID = SBS_ProductMaster.catID WHERE SBS_ProductMaster.IsActive = 1 '+@Criteria+' 
					  SELECT  ID , PID ,SubimgPath  FROM SBS_ProductDetail WHERE 0=0 '+@Criteria+' '
		END

		
		EXEC(@sSql)

END

GO
/****** Object:  StoredProcedure [dbo].[SBS_GetProductsDetails]    Script Date: 10/25/2021 8:58:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SBS_GetProductsDetails]
@Criteria NVARCHAR(MAX)=''
AS
BEGIN	
      
        DECLARE @sSql NVARCHAR(MAX) =''
		SET @sSql = N' SELECT PID ,
						PName ,
						marketRate ,
						sbsRate ,
						imgPath ,
						SBS_ProductMaster.catID ,
						CatName,                
						IsAddToCart,
						ProductDescription,
						DocPath
					FROM SBS_ProductMaster
					INNER JOIN SBS_CategoryMaster ON SBS_CategoryMaster.catID = SBS_ProductMaster.catID WHERE SBS_ProductMaster.IsActive = 1 ' + @Criteria +' 

					 SELECT SubimgPath FROM dbo.SBS_ProductDetail WHERE PID IN(SELECT PID FROM SBS_ProductMaster WHERE IsActive = 1  ' + @Criteria +')'
		EXEC(@sSql)

END

GO
/****** Object:  StoredProcedure [dbo].[SBS_saveMemberKYCDetails]    Script Date: 10/25/2021 8:58:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SBS_saveMemberKYCDetails]
@MemberID NVARCHAR(50) ='',
@AadharCard NVARCHAR(MAX) ='',
@PanCard NVARCHAR(MAX) ='',
@PassportPhoto NVARCHAR(MAX) ='',
@AadharCardBack NVARCHAR(MAX) ='',
@PanCardBack NVARCHAR(MAX) =''
AS
BEGIN
	   
	   INSERT INTO SBS_MemberKYCDetails
	           ( MemberID ,
	             KYC_AadharCard ,
	             KYC_PanCard ,
				 PassportPhoto,
	             CreationDate ,
	             ModifiyDate,
				 KYC_AadharCard_back,
				 KYC_PanCard_back
	           )
	   VALUES  ( @MemberID , -- MemberID - nvarchar(50)
	             @AadharCard , -- KYC_AadharCard - nvarchar(max)
	             @PanCard , -- KYC_PanCard - nvarchar(max),
				 @PassportPhoto, -- PassportPhoto - nvarchar(max),
	            GETDATE() , -- CreationDate - datetime
	            GETDATE(),  -- ModifiyDate - datetime
				@AadharCardBack,
				@PanCardBack
	           )


		SELECT 'Record insert sucessfully!!!'

END

GO
/****** Object:  StoredProcedure [dbo].[SBS_Verifiy_KYC_Documents]    Script Date: 10/25/2021 8:58:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SBS_Verifiy_KYC_Documents]
@MemberID varchar(200) =''
AS
BEGIN
       UPDATE SBS_RegistrationMaster SET IsActive = 1 WHERE SBSMemberID = @MemberID
	   UPDATE  SBS_MemberKYCDetails SET Verified  = 1 WHERE MemberID = @MemberID   

	   
END

GO
/****** Object:  StoredProcedure [dbo].[sp_MemberRegistration]    Script Date: 10/25/2021 8:58:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_MemberRegistration] 
@FirstName NVARCHAR(50) ='',
@LastName NVARCHAR(50) ='',
@Mobile NVARCHAR(20) ='',
@Email NVARCHAR(50) ='',
@Gender BIT,
@City NVARCHAR(10) ='',
@Country NVARCHAR(10) =''
AS 
BEGIN
       DECLARE @check INT =0
	 
	 --  TRUNCATE TABLE SBS_RegistrationMaster

	   --- SELECT * FROM  SBS_RegistrationMaster

	   IF EXISTS(SELECT * FROM SBS_RegistrationMaster WHERE Mobile = @Mobile)
	   BEGIN
	         SELECT 'E|Mobile number already registred with SBS.' as Msg;
	   END

	   ELSE IF EXISTS(SELECT * FROM SBS_RegistrationMaster WHERE Email = @Email)
	   BEGIN
	         SELECT 'E|Email already registred with SBS.' AS Msg;
	   END
	   ELSE
	   BEGIN   	
	  
	   	        DECLARE @new_password varchar(50)
				EXEC usp_RandomPassword @pass_len = 10, @password = @new_password OUT
        
				DECLARE @NewCount BIGINT = 0;
				DECLARE @CurrentCount BIGINT = 0;
				DECLARE @MemberID NVARCHAR(50) ='';

				SET @CurrentCount = (SELECT COUNT(1) FROM SBS_RegistrationMaster);
				SET @NewCount = (1 + @CurrentCount)
				SET @MemberID = REPLACE(STR(@NewCount,8),' ', '0')

				
			   INSERT INTO SBS_RegistrationMaster
					   ( FirstName ,
						 LastName ,
						 Mobile ,
						 Email ,
						 Gender ,
						 City ,
						 Country ,
						 SBSMemberID ,
						 SBSPassword ,
						 IsActive ,
						 CreationDate ,
						 ModifiyDate
					   )
			   VALUES  ( LTRIM(RTRIM(@FirstName)) , -- FirstName - nvarchar(50)
						 LTRIM(RTRIM(@LastName)) , -- LastName - nvarchar(50)
						 LTRIM(RTRIM(@Mobile)) , -- Mobile - nvarchar(20)
						 LTRIM(RTRIM(@Email)), -- Email - nvarchar(50)
						 @Gender , -- Gender - bit
						 LTRIM(RTRIM(@City)), -- City - nvarchar(10)
						 @Country, -- Country - nvarchar(10)
						 @MemberID , -- SBSMemberID - bigint
						 @new_password , -- SBSPassword - nvarchar(10)
						 0 , -- IsActive - bit
						 GETDATE() , -- CreationDate - datetime
						 GETDATE()  -- ModifiyDate - datetime
					   )

				SET @check  = @@ROWCOUNT

				IF @check > 0
				BEGIN
					  SELECT 'S|Record insert successfully.|'+@MemberID+'|'+@new_password AS Msg
				END
				ELSE
				BEGIN	
					 SELECT 'E|Some thing went worng.' AS Msg
				END

		END
		  
	   
END



GO
/****** Object:  StoredProcedure [dbo].[usp_RandomPassword]    Script Date: 10/25/2021 8:58:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_RandomPassword]
 (
      @pass_len AS INT ,
      @password NVARCHAR(400) OUTPUT
    )
AS
    DECLARE @ValidChar AS NVARCHAR(400)
    SET @ValidChar = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890~!@#$%^&*()_-+={}[]\|:;"<>,.?/'
    DECLARE @counter INT
    SET @counter = 0
    SET @password = ''
    WHILE @counter < @pass_len
        BEGIN
            SELECT  @password = @password + SUBSTRING(@ValidChar, ( CONVERT(INT, ( LEN(@ValidChar) * RAND() + 1 )) ),  1)
            SET @counter += 1
        END

GO
USE [master]
GO
ALTER DATABASE [SBS] SET  READ_WRITE 
GO
