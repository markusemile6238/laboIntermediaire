CREATE TRIGGER [TR_OnDelete_Reservations]
ON [dbo].[Reservations]
INSTEAD OF DELETE
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE r
	SET 
		r.IsEnable = 0,
		r.UpdatedAt = GETUTCDATE()
	FROM [dbo].[Reservations] r 
	INNER JOIN deleted dd ON r.Id = dd.Id
	WHERE r.IsEnable = 1;

END
