USE HackCompany
GO

CREATE PROCEDURE SwitchBossWithEmployee
@EmployeeID INT
AS
	DECLARE @EmployeeFirstName NVARCHAR(50) = (SELECT e.FirstName FROM Employees e WHERE e.EmployeeID = @EmployeeID);
	DECLARE @EmployeeLastName NVARCHAR(50) = (SELECT e.LastName FROM Employees e WHERE e.EmployeeID = @EmployeeID);
	DECLARE @EmployeeEmail NVARCHAR(50) = (SELECT e.Email FROM Employees e WHERE e.EmployeeID = @EmployeeID);
	DECLARE @EmployeeBirthDate DATE = (SELECT e.BirthDate FROM Employees e WHERE e.EmployeeID = @EmployeeID);
	DECLARE @BossFirstName NVARCHAR(50) = (SELECT e.FirstName FROM Employees e WHERE e.Manager IS NULL);
	DECLARE @BossLastName NVARCHAR(50) = (SELECT e.LastName FROM Employees e WHERE e.Manager IS NULL);
	DECLARE @BossEmail NVARCHAR(50) = (SELECT e.Email FROM Employees e WHERE e.Manager IS NULL);
	DECLARE @BossBirthDate DATE = (SELECT e.BirthDate FROM Employees e WHERE e.Manager IS NULL);

	UPDATE Employees
	SET FirstName = @EmployeeFirstName,
		LastName = @EmployeeLastName,
		Email = @EmployeeEmail,
		BirthDate = @EmployeeBirthDate
	WHERE Manager IS NULL

	UPDATE Employees
	SET FirstName = @BossFirstName,
		LastName = @BossLastName,
		Email = @BossEmail,
		BirthDate = @BossBirthDate
	WHERE EmployeeID = @EmployeeID
GO