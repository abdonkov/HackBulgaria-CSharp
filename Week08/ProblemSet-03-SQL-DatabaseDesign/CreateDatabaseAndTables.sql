IF DB_ID('HackCompany') IS NUll
CREATE DATABASE HackCompany

GO

USE HackCompany

IF OBJECT_ID('dbo.Departments') IS NULL
CREATE TABLE dbo.Departments
( 
	DepartmentID INT IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(50) NOT NULL
);

IF OBJECT_ID('dbo.Employees') IS NULL
CREATE TABLE dbo.Employees
(
	EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	Email NVARCHAR(50) NULL,
	BirthDate DATE NOT NULL,
	Manager INT NULL FOREIGN KEY REFERENCES Employees(EmployeeID),
	Department INT NULL FOREIGN KEY REFERENCES Departments(DepartmentID)
);

IF OBJECT_ID('dbo.Categories') IS NULL
CREATE TABLE dbo.Categories
(
	CategoryID NCHAR(3) PRIMARY KEY,
	Name NVARCHAR(50) NOT NULL

);

IF OBJECT_ID('dbo.Products') IS NULL
CREATE TABLE dbo.Products
(
	ProductID INT IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(50) NOT NULL,
	Price MONEY NOT NULL,
	Category NCHAR(3) FOREIGN KEY REFERENCES Categories(CategoryID)
);

IF OBJECT_ID('dbo.Customers') IS NULL
CREATE TABLE dbo.Customers
(
	CustomerID INT IDENTITY(1,1) PRIMARY KEY,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	Email NVARCHAR(50) NULL,
	[Address] NVARCHAR(50) NOT NULL,
	Discount FLOAT NULL
);

IF OBJECT_ID('dbo.Orders') IS NULL
CREATE TABLE dbo.Orders
(
	OrderID INT IDENTITY(1,1) PRIMARY KEY,
	DateOfOrder DATETIME NOT NULL,
	Customer INT NOT NULL FOREIGN KEY REFERENCES Customers(CustomerID),
	TotalPrice FLOAT NOT NULL,
);

IF OBJECT_ID('dbo.OrderProducts') IS NULL
CREATE TABLE dbo.OrderProducts
(
	OrderID INT NOT NULL FOREIGN KEY REFERENCES Orders(OrderID),
	ProductID INT NOT NULL FOREIGN KEY REFERENCES Products(ProductID),
	Quantity INT NOT NULL,
	CONSTRAINT OrderProductID PRIMARY KEY (OrderID, ProductID)
);

GO