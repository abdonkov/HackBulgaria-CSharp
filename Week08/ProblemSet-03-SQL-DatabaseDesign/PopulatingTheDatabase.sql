USE HackCompany

SET IDENTITY_INSERT Departments ON --Enables insertion on primary key

INSERT INTO [Departments]([DepartmentID], [Name])
VALUES	(1, 'Sales'),
		(2, 'Production'),
		(3, 'Financial');

SET IDENTITY_INSERT Departments OFF --Disables insertion on primary key

SET IDENTITY_INSERT Employees ON

--Inserting the big boss
INSERT INTO [Employees]([EmployeeID], [FirstName], [LastName], [Email], [BirthDate])
VALUES	(1, 'Bogdan', 'Yanev', 'b.yanev@gmail.com', '1971-08-27');

--Inserting departments managers
INSERT INTO [Employees]([EmployeeID], [FirstName], [LastName], [Email], [BirthDate], [Manager], [Department])
VALUES	(2, 'Ivana', 'Danailova', 'i.danailova@yahoo.com', '1975-01-11', 1, 1),
		(3, 'Radoslav', 'Zahariev', 'rado.zahariev@abv.bg', '1980-02-29', 1, 2),
		(4, 'Filip', 'Yankov', 'filip.yankov@gmail.com', '1976-08-19', 1, 3);

SET IDENTITY_INSERT Employees OFF

INSERT INTO [Employees]([FirstName], [LastName], [Email], [BirthDate], [Manager], [Department])
VALUES	('Ioan', 'Simeonov', 'isimeonov@abv.bg', '1983-03-22', 2, 1),
		('Tereza', 'Vancheva', 'tery.vancheva@abv.bg', '1985-07-22', 2, 1),
		('Milen', 'Danailov', 'm.danailov@gmail.com', '1992-12-05', 2, 1),
		('Tihomir', 'Furnadjiev', 'tihominrf@abv.bg', '1987-10-05', 2, 1),
		('Mira', 'Markova', 'mira.markova@gmail.com', '1978-04-13', 3, 2),
		('Tatiana', 'Boyadjieva', 't.boyadjieva@abv.bg', '1986-08-16', 3, 2),
		('Desislava', 'Filipova', 'desi.f@yahoo.com', '1988-01-16', 3, 2),
		('Kamen', 'Grigorov', 'kamen.grigorov@abv.bg', '1991-02-28', 3, 2),
		('Sofiya', 'Vodenicharova', 'sofia.v@gmail.com', '1981-11-12', 4, 3),
		('Mladen', 'Valeriev', 'mladen.valeriev@abv.bg', '1976-03-28', 4, 3),
		('Yasen', 'Ivov', 'yasen.ivov@yahoo.com', '1979-05-29', 4, 3),
		('Boris', 'Dobrev', 'boris.dobrev@gmail.com', '1980-12-20', 4, 3),
		('Ivan', 'Petrov', 'ivan.petrov@abv.bg', '1978-11-10', 4, 3);

INSERT INTO [Categories]([CategoryID], [Name])
VALUES	('BKS', 'Books'),
		('MSC', 'Music'),
		('HRW', 'Hardware'),
		('SFW', 'Software');

INSERT INTO [Products]([Name], [Price], [Category])
VALUES	('The Lord Of The Rings', 19.99, 'BKS'),
		('Harry Potter', 15.99, 'BKS'),
		('The Hunger Games', 17.99, 'BKS'),
		('AC/DC - High Voltage', 19.99, 'MSC'),
		('The Best Of Wolfgang Amadeus Mozart', 35.99, 'MSC'),
		('B.T.R - The Best Of', 25.99, 'MSC'),
		('Logitech G502 Mouse', 49.99, 'HRW'),
		('Radeon R9 390 GPU', 299.99, 'HRW'),
		('ASUS VN248H 24" IPS Monitor', 149.99, 'HRW'),
		('Windows 10 Pro', 199.99, 'SFW'),
		('Wolfram Mathematica 10', 149.99, 'SFW'),
		('Rise Of The Tomb Raider', 59.99, 'SFW');

SET IDENTITY_INSERT Customers ON

INSERT INTO [Customers]([CustomerID], [FirstName], [LastName], [Email], [Address], [Discount])
VALUES	(1, 'Stanislav', 'Genadiev', NULL, 'Sofia Zh.K. Mladost 5 bl. 432, et. 3, ap. 13', NULL),
		(2, 'Krastio', 'Apostolov', NULL, 'Sofia Zh.K. Lyulin bl. 122, et. 4, ap. 11', NULL),
		(3, 'Tsvetko', 'Mateev', NULL, 'Sofia Zh.K. Studenstki Grad bl. 33, et. 3, ap. 223', 18.5),
		(4, 'Yan', 'Konstantinov', NULL, 'Sofia Ul. Narodno Horo N. 13', NULL),
		(5, 'Zhaklina', 'Draganova', NULL, 'Sofia Ul. Pirotska N. 24', 23);

SET IDENTITY_INSERT Customers OFF

INSERT INTO [Orders]([DateOfOrder], [Customer], [TotalPrice])
VALUES	('2016-01-06', 3, 0),
		('2016-01-24', 4, 0),
		('2016-01-02', 1, 0),
		('2016-01-26', 5, 0),
		('2016-01-19', 2, 0);

INSERT INTO [OrderProducts]([OrderID], [ProductID], [Quantity])
VALUES	(1, 2, 2),
		(1, 7, 3),
		(3, 10, 1),
		(5, 8, 2),
		(2, 3, 2),
		(4, 9, 1),
		(5, 11, 3),
		(5, 4, 1),
		(2, 12, 1),
		(3, 7, 1),
		(4, 3, 3),
		(3, 5, 4);

GO