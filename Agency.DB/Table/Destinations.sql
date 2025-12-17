CREATE TABLE [dbo].[Destinations]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Country] NVARCHAR(80) NOT NULL, 
    [City] NVARCHAR(80) NOT NULL, 
    [Description]  NVARCHAR(max) NOT NULL,
    -- constraints ,
    CONSTRAINT [PU_Destinations] UNIQUE (City)

)
