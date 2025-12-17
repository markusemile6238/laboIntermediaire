CREATE TRIGGER [TR_OnDelete_Destinations]
ON [dbo].[Destinations]
INSTEAD OF DELETE
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION;

	BEGIN TRY

		-- soft delete des activité relié a la destination
		UPDATE a
		SET
			a.IsEnable = 0,
			a.UpdatedAt =  GETUTCDATE()
		FROM [dbo].[Activities] a
		INNER JOIN deleted d ON a.DestinationId = d.Id
		WHERE a.IsEnable = 1;

		-- soft delete de la destination
		UPDATE d
		SET 
			d.IsEnable = 0,
			d.UpdatedAt = GETUTCDATE()
		FROM [dbo].[Destinations] d 
		INNER JOIN deleted dd ON d.Id = dd.Id
		WHERE d.IsEnable = 1;

		-- soft delete activities relied

		COMMIT TRANSACTION;

	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		THROW;
	END CATCH
END
