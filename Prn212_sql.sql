use master
create database OnlineShop
use OnlineShop
CREATE TABLE Accounts (
    account_id INT IDENTITY(1,1) PRIMARY KEY,  -- ID tài khoản, tự động tăng
    username NVARCHAR(50) NOT NULL UNIQUE,     -- Tên đăng nhập, không trùng lặp
    password NVARCHAR(255) NOT NULL,           -- Mật khẩu đã mã hóa
	address NVARCHAR(255),                     -- Địa chỉ 
    email NVARCHAR(100) NOT NULL UNIQUE,       -- Email người dùng, không trùng lặp
    phone_number NVARCHAR(15),                 -- Số điện thoại (tùy chọn)
    role NVARCHAR(20) NOT NULL CHECK (role IN ('Customer', 'Admin', 'Manager')),  -- Vai trò của người dùng
);

CREATE TABLE Category (
    category_id INT IDENTITY(1,1) PRIMARY KEY,  -- ID loại sản phẩm, tự động tăng
    name NVARCHAR(255) NOT NULL,                -- Tên loại sản phẩm
);

CREATE TABLE Products (
    product_id INT IDENTITY(1,1) PRIMARY KEY,   -- ID sản phẩm, tự động tăng
    category_id INT,                            -- ID loại sản phẩm (liên kết với bảng Category)
    name NVARCHAR(255) NOT NULL,                -- Tên sản phẩm
    price DECIMAL(10, 2) NOT NULL,              -- Giá sản phẩm
    quantity_in_stock INT NOT NULL,             -- Số lượng sản phẩm trong kho
    FOREIGN KEY (category_id) REFERENCES Category(category_id) ON DELETE SET NULL,  -- Khóa ngoại tham chiếu đến bảng Category
);

CREATE TABLE Orders (
    order_id INT IDENTITY(1,1) PRIMARY KEY,  -- ID đơn hàng, tự động tăng
    account_id INT,                          -- ID tài khoản của khách hàng
    total_amount DECIMAL(10, 2) NOT NULL,    -- Tổng số tiền
    status NVARCHAR(20) CHECK (status IN ('Pending', 'Processing', 'Completed', 'Cancelled')) NOT NULL,  -- Trạng thái đơn hàng
	order_date DATETIME DEFAULT GETDATE(),   -- Ngày đặt hàng
    FOREIGN KEY (account_id) REFERENCES Accounts(account_id) ON DELETE SET NULL  -- Khóa ngoại tham chiếu đến bảng Accounts
);
CREATE TABLE Order_Items (
    order_item_id INT IDENTITY(1,1) PRIMARY KEY,
    order_id INT,
    product_id INT,
    prod_qty INT NOT NULL,
    total_price DECIMAL(10, 2)              
	FOREIGN KEY (order_id) REFERENCES Orders(order_id) ON DELETE CASCADE,
	FOREIGN KEY (product_id) REFERENCES Products(product_id) ON DELETE SET NULL,
);
CREATE TABLE Cart (
    cart_id INT IDENTITY(1,1) PRIMARY KEY,
    account_id INT,
	create_at DATETIME DEFAULT GETDATE(),
	FOREIGN KEY (account_id) REFERENCES Accounts(account_id) ON DELETE SET NULL,
);
CREATE TABLE Cart_Item (
    cart_item_id INT IDENTITY(1,1) PRIMARY KEY,
	cart_id INT,
	product_id INT,
	product_qty INT,
	FOREIGN KEY (cart_id) REFERENCES Cart(cart_id) ON DELETE SET NULL,
	FOREIGN KEY (product_id) REFERENCES Products(product_id) ON DELETE SET NULL,
);

-- khởi tạo dữ liệu
-- Insert categories
INSERT INTO Category (name) VALUES 
    (N'Men’s Clothing'),
    (N'Women’s Clothing'),
    (N'Children’s Clothing'),
    (N'Accessories');

-- Insert products
INSERT INTO Products (category_id, name, price, quantity_in_stock) VALUES
    (1, N'T-Shirt', 19.99, 100),            -- Men’s Clothing
    (1, N'Jeans', 49.99, 50),               -- Men’s Clothing
    (2, N'Dress', 59.99, 30),               -- Women’s Clothing
    (2, N'Skirt', 29.99, 40),               -- Women’s Clothing
    (3, N'Kids T-Shirt', 14.99, 80),        -- Children’s Clothing
    (3, N'Kids Jeans', 24.99, 60),          -- Children’s Clothing
    (4, N'Scarf', 9.99, 200),               -- Accessories
    (4, N'Hat', 15.99, 150);                -- Accessories

	INSERT INTO Accounts (username, password, address, email, phone_number, role) VALUES
    (N'customer1', 'password121', N'123 Main St, New York, NY', N'john@example.com', N'1234567890', N'Customer'),
    (N'customer2', 'password122', N'456 Elm St, Los Angeles, CA', N'jane@example.com', N'0987654321', N'Customer'),
    (N'admin', 'admin', N'789 Maple St, Chicago, IL', N'admin@example.com', NULL, N'Admin'),
    (N'manager1', 'manage123', N'101 Pine St, Houston, TX', N'manager@example.com', N'1122334455', N'Manager'),
    (N'customer3', 'password123', N'202 Oak St, Miami, FL', N'sarah@example.com', N'2233445566', N'Customer')


