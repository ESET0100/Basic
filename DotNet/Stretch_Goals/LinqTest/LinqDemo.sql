create database LinqDemo
use LinqDemo

CREATE TABLE Categories (
    CategoryId INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255)
);

CREATE TABLE Products (
    ProductId INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(100) NOT NULL,
    CategoryId INT FOREIGN KEY REFERENCES Categories(CategoryId),
    Price DECIMAL(10,2),
    StockQuantity INT,
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE Orders (
    OrderId INT PRIMARY KEY IDENTITY(1,1),
    ProductId INT FOREIGN KEY REFERENCES Products(ProductId),
    Quantity INT,
    OrderDate DATETIME DEFAULT GETDATE(),
    CustomerName NVARCHAR(100),
    TotalAmount DECIMAL(10,2)
);


-- Insert Sample Data
INSERT INTO Categories (CategoryName, Description) VALUES
('Electronics', 'Electronic devices and accessories'),
('Clothing', 'Apparel and fashion items'),
('Books', 'Educational and entertainment books'),
('Home & Garden', 'Home improvement and garden supplies');

INSERT INTO Products (ProductName, CategoryId, Price, StockQuantity, IsActive) VALUES
('Laptop', 1, 999.99, 10, 1),
('Smartphone', 1, 699.99, 15, 1),
('Wireless Mouse', 1, 29.99, 50, 1),
('T-Shirt', 2, 19.99, 100, 1),
('Jeans', 2, 49.99, 75, 1),
('Programming Book', 3, 39.99, 30, 1),
('Gardening Tools', 4, 79.99, 20, 0), -- Inactive product
('Desk Lamp', 4, 24.99, 40, 1);

INSERT INTO Orders (ProductId, Quantity, OrderDate, CustomerName, TotalAmount) VALUES
(1, 1, '2024-01-15', 'John Doe', 999.99),
(2, 2, '2024-01-16', 'Jane Smith', 1399.98),
(3, 5, '2024-01-17', 'Mike Johnson', 149.95),
(4, 3, '2024-01-18', 'Sarah Wilson', 59.97),
(5, 2, '2024-01-19', 'Tom Brown', 99.98),
(2, 1, '2024-01-20', 'John Doe', 699.99),
(6, 4, '2024-01-21', 'Alice Green', 159.96),
(8, 2, '2024-01-22', 'Bob White', 49.98);

select* from Categories
select* from Products
select* from Orders