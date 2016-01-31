USE HackCompany

UPDATE Customers
SET Discount = Discount * 2
WHERE CustomerID = (SELECT TOP 1 c.CustomerID
					FROM Customers c
					JOIN Orders o ON c.CustomerID = o.Customer
					GROUP BY c.CustomerID
					ORDER BY COUNT(*) DESC)