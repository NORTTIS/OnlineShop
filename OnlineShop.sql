USE [master]
GO
/****** Object:  Database [OnlineShop]    Script Date: 10/11/2024 19:44:22 ******/
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'OnlineShop') DROP DATABASE OnlineShop;
CREATE DATABASE [OnlineShop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OnlineShop', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\OnlineShop.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OnlineShop_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\OnlineShop_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [OnlineShop] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OnlineShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OnlineShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OnlineShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OnlineShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OnlineShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OnlineShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [OnlineShop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OnlineShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OnlineShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OnlineShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OnlineShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OnlineShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OnlineShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OnlineShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OnlineShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OnlineShop] SET  ENABLE_BROKER 
GO
ALTER DATABASE [OnlineShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OnlineShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OnlineShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OnlineShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OnlineShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OnlineShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OnlineShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OnlineShop] SET RECOVERY FULL 
GO
ALTER DATABASE [OnlineShop] SET  MULTI_USER 
GO
ALTER DATABASE [OnlineShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OnlineShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OnlineShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OnlineShop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OnlineShop] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OnlineShop] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'OnlineShop', N'ON'
GO
ALTER DATABASE [OnlineShop] SET QUERY_STORE = OFF
GO
USE [OnlineShop]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 10/11/2024 19:44:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[account_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](255) NOT NULL,
	[address] [nvarchar](255) NULL,
	[email] [nvarchar](100) NOT NULL,
	[phone_number] [nvarchar](15) NULL,
	[role] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 10/11/2024 19:44:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[category_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 10/11/2024 19:44:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[order_id] [int] IDENTITY(1,1) NOT NULL,
	[account_id] [int] NULL,
	[total_amount] [decimal](10, 2) NOT NULL,
	[status] [nvarchar](20) NOT NULL,
	[order_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_Item]    Script Date: 10/11/2024 19:44:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Item](
	[order_item_id] [int] IDENTITY(1,1) NOT NULL,
	[order_id] [int] NULL,
	[product_id] [int] NULL,
	[prod_qty] [int] NOT NULL,
	[total_price] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[order_item_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 10/11/2024 19:44:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[product_id] [int] IDENTITY(1,1) NOT NULL,
	[category_id] [int] NULL,
	[name] [nvarchar](255) NOT NULL,
	[price] [decimal](10, 2) NOT NULL,
	[quantity_in_stock] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([account_id], [username], [password], [address], [email], [phone_number], [role]) VALUES (1, N'admin', N'1234', N'Hanoi', N'admin@onlineshop.com', N'', N'Admin')
INSERT [dbo].[Account] ([account_id], [username], [password], [address], [email], [phone_number], [role]) VALUES (2, N'manager', N'123', N'Hanoi', N'manager@onlineshop.com', NULL, N'Manager')
INSERT [dbo].[Account] ([account_id], [username], [password], [address], [email], [phone_number], [role]) VALUES (3, N'customer', N'123', N'Hanoi', N'customer@onlineshop.com', NULL, N'Customer')
INSERT [dbo].[Account] ([account_id], [username], [password], [address], [email], [phone_number], [role]) VALUES (6, N'test', N'1234', N'Saigon', N'test@onlineshop.com', N'', N'Manager')
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([category_id], [name]) VALUES (1, N'Food')
INSERT [dbo].[Category] ([category_id], [name]) VALUES (2, N'Beverage')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([order_id], [account_id], [total_amount], [status], [order_date]) VALUES (3, NULL, CAST(90000.00 AS Decimal(10, 2)), N'Pending', CAST(N'2024-11-10T18:25:37.500' AS DateTime))
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[Order_Item] ON 

INSERT [dbo].[Order_Item] ([order_item_id], [order_id], [product_id], [prod_qty], [total_price]) VALUES (1, 3, 4, 1, CAST(8000.00 AS Decimal(10, 2)))
INSERT [dbo].[Order_Item] ([order_item_id], [order_id], [product_id], [prod_qty], [total_price]) VALUES (2, 3, 7, 2, CAST(36000.00 AS Decimal(10, 2)))
INSERT [dbo].[Order_Item] ([order_item_id], [order_id], [product_id], [prod_qty], [total_price]) VALUES (3, 3, 1, 1, CAST(10000.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Order_Item] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([product_id], [category_id], [name], [price], [quantity_in_stock]) VALUES (1, 1, N'Bread', CAST(10000.00 AS Decimal(10, 2)), 36)
INSERT [dbo].[Product] ([product_id], [category_id], [name], [price], [quantity_in_stock]) VALUES (2, 1, N'Mix snack', CAST(12000.00 AS Decimal(10, 2)), 50)
INSERT [dbo].[Product] ([product_id], [category_id], [name], [price], [quantity_in_stock]) VALUES (3, 1, N'Spicy noodles', CAST(10000.00 AS Decimal(10, 2)), 87)
INSERT [dbo].[Product] ([product_id], [category_id], [name], [price], [quantity_in_stock]) VALUES (4, 1, N'Nabati', CAST(8000.00 AS Decimal(10, 2)), 44)
INSERT [dbo].[Product] ([product_id], [category_id], [name], [price], [quantity_in_stock]) VALUES (5, 2, N'Coca Cola', CAST(15000.00 AS Decimal(10, 2)), 32)
INSERT [dbo].[Product] ([product_id], [category_id], [name], [price], [quantity_in_stock]) VALUES (6, 2, N'Fanta', CAST(18000.00 AS Decimal(10, 2)), 64)
INSERT [dbo].[Product] ([product_id], [category_id], [name], [price], [quantity_in_stock]) VALUES (7, 2, N'Pepsi', CAST(18000.00 AS Decimal(10, 2)), 37)
INSERT [dbo].[Product] ([product_id], [category_id], [name], [price], [quantity_in_stock]) VALUES (8, 2, N'C2', CAST(10000.00 AS Decimal(10, 2)), 100)
INSERT [dbo].[Product] ([product_id], [category_id], [name], [price], [quantity_in_stock]) VALUES (11, NULL, N'Test', CAST(10000.00 AS Decimal(10, 2)), 12)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Accounts__AB6E61647E489405]    Script Date: 10/11/2024 19:44:23 ******/
ALTER TABLE [dbo].[Account] ADD UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Accounts__F3DBC5726BB0C20B]    Script Date: 10/11/2024 19:44:23 ******/
ALTER TABLE [dbo].[Account] ADD UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Order] ADD  DEFAULT (getdate()) FOR [order_date]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([account_id])
REFERENCES [dbo].[Account] ([account_id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Order_Item]  WITH CHECK ADD FOREIGN KEY([product_id])
REFERENCES [dbo].[Product] ([product_id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Order_Item]  WITH CHECK ADD FOREIGN KEY([order_id])
REFERENCES [dbo].[Order] ([order_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([category_id])
REFERENCES [dbo].[Category] ([category_id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD CHECK  (([role]='Manager' OR [role]='Admin' OR [role]='Customer'))
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD CHECK  (([status]='Cancelled' OR [status]='Completed' OR [status]='Processing' OR [status]='Pending'))
GO
USE [master]
GO
ALTER DATABASE [OnlineShop] SET  READ_WRITE 
GO
