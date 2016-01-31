USE HackCompany

SELECT e.EmployeeID, e.FirstName, e.LastName, e.Email, e.BirthDate, d.Name AS DepartmentName
FROM Employees e
JOIN Departments d ON e.Department = d.DepartmentID
WHERE D.Name = 'Sales'
ORDER BY e.FirstName, e.LastName