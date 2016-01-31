USE HackCompany
GO

CREATE PROCEDURE OrderedTimes
@ProductID INT
AS
	SELECT COUNT(*) AS OrderedTimes
	FROM Products p
	JOIN OrderProducts op on p.ProductID = op.ProductID
	WHERE p.ProductID = @ProductID
	GROUP BY p.ProductID
GO