USE HackCompany

SELECT (SUM(c.Discount) / COUNT(*)) AS AverageDiscount
FROM Customers c
