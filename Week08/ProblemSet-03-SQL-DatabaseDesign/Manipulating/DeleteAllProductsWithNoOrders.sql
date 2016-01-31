USE HackCompany

DELETE FROM Products
WHERE ProductID NOT IN (SELECT op.ProductID
						FROM OrderProducts op)