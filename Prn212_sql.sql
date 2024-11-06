use master
create database OnlineShop

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