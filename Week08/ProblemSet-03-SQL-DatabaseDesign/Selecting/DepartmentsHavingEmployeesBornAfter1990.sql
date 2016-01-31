USE HackCompany

SELECT d.DepartmentID , d.Name
FROM Departments d
INNER JOIN Employees e ON e.Department = d.DepartmentID
WHERE e.BirthDate >= '1991-01-01'
GROUP BY d.DepartmentID, d.Name