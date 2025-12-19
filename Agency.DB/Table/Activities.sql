CREATE TABLE [dbo].[Activities]
(
	[Id] INT NOT NULL IDENTITY, 
    [Name] NVARCHAR(150) NOT NULL, 
    [Description] NVARCHAR(max) NOT NULL, 
    [Price] FLOAT NOT NULL, 
    [DestinationId] INT NULL,
    [ImageUrl] NVARCHAR(max) null,
    -- management
    [IsEnable] BIT  DEFAULT 1 NOT NULL,
    [CreatedAt] DATETIME DEFAULT GETDATE() NOT NULL,
    [UpdatedAt] DATETIME NULL
    -- constraints
    CONSTRAINT [PK_Activities] PRIMARY KEY (Id)
    CONSTRAINT [UK_Activities] UNIQUE (Name),
    CONSTRAINT [CK_Activities_Price] CHECK (Price > 0),
    CONSTRAINT [FK_Activities_destination] FOREIGN KEY (DestinationId) REFERENCES Destinations(Id) ON DELETE SET NULL

)
