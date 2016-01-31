USE HackCompany

--DECLARE @bigBossID INT
--SET @bigBossID = (Select EmployeeID FROM Employees e WHERE e.Manager IS NULL)

SELECT *
FROM Employees
WHERE Manager = (Select EmployeeID FROM Employees WHERE Manager IS NULL)
