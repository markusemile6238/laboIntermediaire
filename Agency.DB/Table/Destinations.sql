CREATE TABLE [dbo].[Destinations]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Country] NVARCHAR(80) NOT NULL, 
    [City] NVARCHAR(80) NOT NULL, 
    [Description]  NVARCHAR(max) NOT NULL,
        -- management
    [IsEnable] BIT  DEFAULT 1 NOT NULL,
    [CreatedAt] DATETIME DEFAULT GETDATE() NOT NULL,
    [UpdatedAt] DATETIME NULL
    -- constraints ,
    CONSTRAINT [PU_Destinations] UNIQUE (Country,City)

)
