USE [master]
GO
/****** Object:  Database [QuanLyQuanCafe]    Script Date: 03/12/2020 9:40:18 CH ******/
CREATE DATABASE [QuanLyQuanCafe]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuanLyQuanCafe', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\QuanLyQuanCafe.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QuanLyQuanCafe_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\QuanLyQuanCafe_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [QuanLyQuanCafe] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuanLyQuanCafe].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuanLyQuanCafe] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuanLyQuanCafe] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuanLyQuanCafe] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QuanLyQuanCafe] SET  MULTI_USER 
GO
ALTER DATABASE [QuanLyQuanCafe] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuanLyQuanCafe] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuanLyQuanCafe] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QuanLyQuanCafe] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QuanLyQuanCafe] SET QUERY_STORE = OFF
GO
USE [QuanLyQuanCafe]
GO
/****** Object:  UserDefinedFunction [dbo].[fuConvertToUnsign1]    Script Date: 03/12/2020 9:40:18 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fuConvertToUnsign1] ( @strInput NVARCHAR(4000) ) RETURNS NVARCHAR(4000) AS BEGIN IF @strInput IS NULL RETURN @strInput IF @strInput = '' RETURN @strInput DECLARE @RT NVARCHAR(4000) DECLARE @SIGN_CHARS NCHAR(136) DECLARE @UNSIGN_CHARS NCHAR (136) SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' +NCHAR(272)+ NCHAR(208) SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' DECLARE @COUNTER int DECLARE @COUNTER1 int SET @COUNTER = 1 WHILE (@COUNTER <=LEN(@strInput)) BEGIN SET @COUNTER1 = 1 WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) BEGIN IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) BEGIN IF @COUNTER=1 SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) ELSE SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) BREAK END SET @COUNTER1 = @COUNTER1 +1 END SET @COUNTER = @COUNTER +1 END SET @strInput = replace(@strInput,' ','-') RETURN @strInput END
GO
/****** Object:  Table [dbo].[Account]    Script Date: 03/12/2020 9:40:18 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[UserName] [nvarchar](100) NOT NULL,
	[DisplayName] [nvarchar](100) NOT NULL,
	[PassWord] [nvarchar](1000) NOT NULL,
	[Type] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 03/12/2020 9:40:18 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DataCheckIn] [date] NOT NULL,
	[DataCheckOut] [date] NULL,
	[IdTable] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[discount] [int] NULL,
	[totalPrice] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BillInfo]    Script Date: 03/12/2020 9:40:18 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdBill] [int] NOT NULL,
	[IdFood] [int] NOT NULL,
	[count] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Food]    Script Date: 03/12/2020 9:40:18 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Food](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[IdCategory] [int] NOT NULL,
	[Price] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FoodCategory]    Script Date: 03/12/2020 9:40:18 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TableFood]    Script Date: 03/12/2020 9:40:18 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TableFood](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Status] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeAccount]    Script Date: 03/12/2020 9:40:18 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeAccount](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Account] ([UserName], [DisplayName], [PassWord], [Type]) VALUES (N'admin', N'admin', N'admin', 1)
INSERT [dbo].[Account] ([UserName], [DisplayName], [PassWord], [Type]) VALUES (N'nhanvien', N'nhân viên', N'0', 3)
GO
SET IDENTITY_INSERT [dbo].[Bill] ON 

INSERT [dbo].[Bill] ([Id], [DataCheckIn], [DataCheckOut], [IdTable], [Status], [discount], [totalPrice]) VALUES (49, CAST(N'2020-09-10' AS Date), CAST(N'2020-09-10' AS Date), 54, 1, 0, 20000)
INSERT [dbo].[Bill] ([Id], [DataCheckIn], [DataCheckOut], [IdTable], [Status], [discount], [totalPrice]) VALUES (50, CAST(N'2020-09-21' AS Date), CAST(N'2020-09-21' AS Date), 51, 1, 0, 20000)
INSERT [dbo].[Bill] ([Id], [DataCheckIn], [DataCheckOut], [IdTable], [Status], [discount], [totalPrice]) VALUES (51, CAST(N'2020-09-21' AS Date), NULL, 56, 0, 0, NULL)
INSERT [dbo].[Bill] ([Id], [DataCheckIn], [DataCheckOut], [IdTable], [Status], [discount], [totalPrice]) VALUES (52, CAST(N'2020-09-21' AS Date), CAST(N'2020-09-21' AS Date), 53, 1, 0, 30000)
INSERT [dbo].[Bill] ([Id], [DataCheckIn], [DataCheckOut], [IdTable], [Status], [discount], [totalPrice]) VALUES (53, CAST(N'2020-09-21' AS Date), CAST(N'2020-09-21' AS Date), 51, 1, 0, 20000)
INSERT [dbo].[Bill] ([Id], [DataCheckIn], [DataCheckOut], [IdTable], [Status], [discount], [totalPrice]) VALUES (54, CAST(N'2020-09-26' AS Date), CAST(N'2020-09-26' AS Date), 51, 1, 10, 18000)
INSERT [dbo].[Bill] ([Id], [DataCheckIn], [DataCheckOut], [IdTable], [Status], [discount], [totalPrice]) VALUES (55, CAST(N'2020-09-26' AS Date), CAST(N'2020-12-03' AS Date), 52, 1, 0, 5000)
INSERT [dbo].[Bill] ([Id], [DataCheckIn], [DataCheckOut], [IdTable], [Status], [discount], [totalPrice]) VALUES (56, CAST(N'2020-09-26' AS Date), CAST(N'2020-09-27' AS Date), 53, 1, 0, 15000)
INSERT [dbo].[Bill] ([Id], [DataCheckIn], [DataCheckOut], [IdTable], [Status], [discount], [totalPrice]) VALUES (57, CAST(N'2020-09-27' AS Date), CAST(N'2020-09-27' AS Date), 51, 1, 0, 5000)
INSERT [dbo].[Bill] ([Id], [DataCheckIn], [DataCheckOut], [IdTable], [Status], [discount], [totalPrice]) VALUES (58, CAST(N'2020-12-03' AS Date), NULL, 52, 0, 0, NULL)
INSERT [dbo].[Bill] ([Id], [DataCheckIn], [DataCheckOut], [IdTable], [Status], [discount], [totalPrice]) VALUES (59, CAST(N'2020-12-03' AS Date), CAST(N'2020-12-03' AS Date), 51, 1, 0, 15000)
SET IDENTITY_INSERT [dbo].[Bill] OFF
GO
SET IDENTITY_INSERT [dbo].[BillInfo] ON 

INSERT [dbo].[BillInfo] ([Id], [IdBill], [IdFood], [count]) VALUES (68, 52, 4, 1)
INSERT [dbo].[BillInfo] ([Id], [IdBill], [IdFood], [count]) VALUES (72, 56, 5, 3)
INSERT [dbo].[BillInfo] ([Id], [IdBill], [IdFood], [count]) VALUES (74, 55, 5, 1)
INSERT [dbo].[BillInfo] ([Id], [IdBill], [IdFood], [count]) VALUES (75, 57, 5, 1)
INSERT [dbo].[BillInfo] ([Id], [IdBill], [IdFood], [count]) VALUES (76, 59, 3, 1)
SET IDENTITY_INSERT [dbo].[BillInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[Food] ON 

INSERT [dbo].[Food] ([Id], [Name], [IdCategory], [Price]) VALUES (3, N'Trà sữa bạc hà', 2, 15000)
INSERT [dbo].[Food] ([Id], [Name], [IdCategory], [Price]) VALUES (4, N'Trà sữa chân châu', 2, 10000)
INSERT [dbo].[Food] ([Id], [Name], [IdCategory], [Price]) VALUES (5, N'Trà đá nóng', 3, 5000)
INSERT [dbo].[Food] ([Id], [Name], [IdCategory], [Price]) VALUES (6, N'Trà đá lạnh', 3, 10000)
INSERT [dbo].[Food] ([Id], [Name], [IdCategory], [Price]) VALUES (17, N'Coca Cola', 5, 15000)
INSERT [dbo].[Food] ([Id], [Name], [IdCategory], [Price]) VALUES (18, N'7 Up', 5, 15000)
SET IDENTITY_INSERT [dbo].[Food] OFF
GO
SET IDENTITY_INSERT [dbo].[FoodCategory] ON 

INSERT [dbo].[FoodCategory] ([Id], [Name]) VALUES (1, N'Cà phê')
INSERT [dbo].[FoodCategory] ([Id], [Name]) VALUES (2, N'Trà sữa')
INSERT [dbo].[FoodCategory] ([Id], [Name]) VALUES (3, N'Trà đá')
INSERT [dbo].[FoodCategory] ([Id], [Name]) VALUES (5, N'Nước')
SET IDENTITY_INSERT [dbo].[FoodCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[TableFood] ON 

INSERT [dbo].[TableFood] ([Id], [Name], [Status]) VALUES (51, N'Bàn 1', N'Trống')
INSERT [dbo].[TableFood] ([Id], [Name], [Status]) VALUES (52, N'Bàn 2', N'Trống')
INSERT [dbo].[TableFood] ([Id], [Name], [Status]) VALUES (53, N'Bàn 3', N'Trống')
INSERT [dbo].[TableFood] ([Id], [Name], [Status]) VALUES (54, N'Bàn 4', N'Trống')
INSERT [dbo].[TableFood] ([Id], [Name], [Status]) VALUES (55, N'Bàn 5', N'Trống')
INSERT [dbo].[TableFood] ([Id], [Name], [Status]) VALUES (56, N'Bàn 6', N'Hỏng')
SET IDENTITY_INSERT [dbo].[TableFood] OFF
GO
SET IDENTITY_INSERT [dbo].[TypeAccount] ON 

INSERT [dbo].[TypeAccount] ([Id], [Name]) VALUES (1, N'Admin')
INSERT [dbo].[TypeAccount] ([Id], [Name]) VALUES (3, N'Nhân viên')
SET IDENTITY_INSERT [dbo].[TypeAccount] OFF
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT (N'Kter') FOR [DisplayName]
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT ((0)) FOR [PassWord]
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT ((0)) FOR [Type]
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT (getdate()) FOR [DataCheckIn]
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[BillInfo] ADD  DEFAULT ((0)) FOR [count]
GO
ALTER TABLE [dbo].[Food] ADD  DEFAULT (N'Chưa đặt tên') FOR [Name]
GO
ALTER TABLE [dbo].[Food] ADD  DEFAULT ((0)) FOR [Price]
GO
ALTER TABLE [dbo].[FoodCategory] ADD  DEFAULT (N'Chưa đặt tên') FOR [Name]
GO
ALTER TABLE [dbo].[TableFood] ADD  DEFAULT (N'Bàn chưa đặt tên') FOR [Name]
GO
ALTER TABLE [dbo].[TableFood] ADD  DEFAULT (N'Trống') FOR [Status]
GO
ALTER TABLE [dbo].[TypeAccount] ADD  DEFAULT (N'Chưa đặt tên chức vụ') FOR [Name]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_TypeAccount] FOREIGN KEY([Type])
REFERENCES [dbo].[TypeAccount] ([Id])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_TypeAccount]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD FOREIGN KEY([IdTable])
REFERENCES [dbo].[TableFood] ([Id])
GO
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([IdBill])
REFERENCES [dbo].[Bill] ([Id])
GO
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([IdFood])
REFERENCES [dbo].[Food] ([Id])
GO
ALTER TABLE [dbo].[Food]  WITH CHECK ADD FOREIGN KEY([IdCategory])
REFERENCES [dbo].[FoodCategory] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[USP_GetAccountByUsersName]    Script Date: 03/12/2020 9:40:18 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetAccountByUsersName]
@userName nvarchar(100)
AS
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GetListBillByDate]    Script Date: 03/12/2020 9:40:18 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetListBillByDate]
@checkIn DATE, @checkOut DATE
AS
BEGIN
	SELECT t.name AS [Tên bàn], DataCheckIn AS [Ngày vào], DataCheckOut AS [Ngày ra], discount AS [Giảm giá], b.totalPrice AS [Tổng tiền]
	FROM dbo.Bill AS b, dbo.TableFood AS t
	WHERE DataCheckIn >= @checkIn AND DataCheckOut <= @checkOut AND b.Status = 1 AND t.Id = b.IdTable
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GetTableList]    Script Date: 03/12/2020 9:40:18 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetTableList]
AS SELECT Id, Name, Status FROM dbo.TableFood
GO
/****** Object:  StoredProcedure [dbo].[USP_InsertBill]    Script Date: 03/12/2020 9:40:18 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_InsertBill]
@idTable INT
AS
BEGIN
	INSERT dbo.Bill (DataCheckIn, DataCheckOut, IdTable, Status, discount)
	VALUES (GETDATE(), NULL, @idTable, 0, 0)
END
GO
/****** Object:  StoredProcedure [dbo].[USP_InsertBillInfo]    Script Date: 03/12/2020 9:40:18 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_InsertBillInfo]
@idBill INT, @idFood INT, @count INT
AS
BEGIN
	DECLARE @isExitsBillInfo INT
	DECLARE @foodCount INT = 1

	SELECT @isExitsBillInfo = id, @foodCount = b.count 
	FROM dbo.BillInfo AS b
	WHERE IdBill = @idBill AND IdFood = @idFood

	IF(@isExitsBillInfo > 0)
		BEGIN
			DECLARE @newCount INT = @foodCount + @count
			IF(@newCount > 0)
				UPDATE dbo.BillInfo SET count = @foodCount + @count WHERE IdFood = @idFood
			ELSE
				DELETE dbo.BillInfo WHERE IdBill = @idBill AND IdFood = @idFood
		END
	ELSE
		BEGIN
			INSERT dbo.BillInfo (IdBill, IdFood, count)
			VALUES (@idBill, @idFood, @count)
		END
END
GO
/****** Object:  StoredProcedure [dbo].[USP_Login]    Script Date: 03/12/2020 9:40:18 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_Login]
@userName NVARCHAR(100), @passWord NVARCHAR(100)
AS
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName and PassWord = @passWord
END
GO
/****** Object:  StoredProcedure [dbo].[USP_SwitchTable]    Script Date: 03/12/2020 9:40:18 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_SwitchTable]
@idTable1 INT, @idTable2 INT
AS
BEGIN
	DECLARE @idFirstBill INT

	DECLARE @idSeconrdBill INT

	DECLARE @isFirstTablEmty INT = 1
	DECLARE @isSecondTablEmty INT = 1

	SELECT @idFirstBill = Id FROM dbo.Bill WHERE IdTable = @idTable1 AND Status = 0

	SELECT @idSeconrdBill = Id FROM dbo.Bill WHERE IdTable = @idTable2 AND Status = 0

	IF(@idFirstBill IS NULL)
		BEGIN
			INSERT dbo.Bill (DataCheckIn, DataCheckOut, IdTable, Status)
			VALUES (GETDATE(), NULL, @idTable1, 0)

			SELECT @idFirstBill = MAX(Id) FROM dbo.Bill WHERE IdTable = @idTable1 AND Status = 0
		END
	
	SELECT @isFirstTablEmty = COUNT(*) FROM dbo.BillInfo WHERE IdBill = @idFirstBill

	IF(@idSeconrdBill IS NULL)
		BEGIN
			INSERT dbo.Bill (DataCheckIn, DataCheckOut, IdTable, Status)
			VALUES (GETDATE(), NULL, @idTable2, 0)

			SELECT @idSeconrdBill = MAX(Id) FROM dbo.Bill WHERE IdTable = @idTable2 AND Status = 0
		END
	
	SELECT @isSecondTablEmty = COUNT(*) FROM dbo.BillInfo WHERE IdBill = @idSeconrdBill

	SELECT id INTO IDBillInfoTable FROM dbo.BillInfo WHERE IdBill = @idSeconrdBill

	UPDATE dbo.BillInfo SET IdBill = @idSeconrdBill WHERE IdBill = @idFirstBill

	UPDATE dbo.BillInfo SET IdBill = @idFirstBill WHERE id IN (SELECT * FROM IDBillInfoTable)

	DROP TABLE IDBillInfoTable

	IF(@isFirstTablEmty = 0)
		UPDATE dbo.TableFood SET Status = N'Trống' WHERE Id = @idTable2
	IF(@isSecondTablEmty = 0)
		UPDATE dbo.TableFood SET Status = N'Trống' WHERE Id = @idTable1
END
GO
/****** Object:  StoredProcedure [dbo].[USP_UpdateAccount]    Script Date: 03/12/2020 9:40:18 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_UpdateAccount]
@userName NVARCHAR(100), @displayName NVARCHAR(100), @password NVARCHAR(100), @newPassword NVARCHAR(100)
AS
BEGIN
	DECLARE @isRightPass INT = 0

	SELECT @isRightPass = COUNT(*) FROM dbo.Account WHERE UserName = @userName AND PassWord = @password

	IF(@isRightPass = 1)
		BEGIN
			IF(@newPassword = NULL OR @newPassword = '')
				BEGIN
					UPDATE dbo.Account SET DisplayName = @displayName WHERE UserName = @userName
				END
			ELSE
				BEGIN
					UPDATE dbo.Account SET DisplayName = @displayName, PassWord = @newPassword WHERE UserName = @userName
				END
		END
END
GO
USE [master]
GO
ALTER DATABASE [QuanLyQuanCafe] SET  READ_WRITE 
GO
