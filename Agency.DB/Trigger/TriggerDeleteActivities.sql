CREATE TRIGGER [TR_OnDelete_Activities]
ON [dbo].[Activities]
INSTEAD OF DELETE
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE a
	SET 
		a.IsEnable = 0,
		a.UpdatedAt = GETUTCDATE()
	FROM [dbo].[Activities] a 
	INNER JOIN deleted d ON a.Id = d.Id
	WHERE a.IsEnable = 1;



END
