USE HackCompany

SELECT TOP 1 o.OrderID, o.DateOfOrder, o.Customer, o.TotalPrice
FROM Orders o
JOIN OrderProducts op ON o.OrderID = op.OrderID
GROUP BY o.OrderID, o.DateOfOrder, o.Customer, o.TotalPrice
ORDER BY (COUNT(*) * SUM(op.Quantity)) DESC