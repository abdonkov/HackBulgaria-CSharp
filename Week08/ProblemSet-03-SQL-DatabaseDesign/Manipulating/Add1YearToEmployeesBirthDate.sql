USE HackCompany

UPDATE Employees
SET BirthDate = DATEADD(year, 1, BirthDate)