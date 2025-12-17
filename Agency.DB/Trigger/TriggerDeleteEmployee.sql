CREATE TRIGGER [TR_OnDelete_Employee]
ON [dbo].[Employees]
INSTEAD OF DELETE
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE e
	SET 
		e.IsEnable = 0,
		e.UpdatedAt = GETUTCDATE()
	FROM [dbo].[Employees] e 
	INNER JOIN deleted d ON e.Id = d.Id
	WHERE e.IsEnable = 1;

END
