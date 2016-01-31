USE HackCompany

SELECT TOP 1 d.DepartmentID , d.Name
FROM Departments d
JOIN Employees e ON e.Department = d.DepartmentID
GROUP BY d.DepartmentID, d.Name
ORDER BY COUNT(*) DESC
