    CREATE TABLE [dbo].[Reservations]
    (
	    [Id] INT NOT NULL IDENTITY, 
        [ClientName] NVARCHAR(80) NOT NULL , 
        [ActivityId] int NOT NULL,
        [DepartureDate] DATETIME NOT NULL, 
        [BookMadeBy] UNIQUEIDENTIFIER NOT NULL,
            -- management
        [IsEnable] BIT  DEFAULT 1 NOT NULL,
        [CreatedAt] DATETIME DEFAULT GETDATE() NOT NULL,
        [UpdatedAt] DATETIME NULL
        -- constraints
        CONSTRAINT [PK_Reservations] PRIMARY KEY (Id),
        CONSTRAINT [FK_Reservations_ActivityId] FOREIGN KEY (ActivityId) REFERENCES Activities ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Reservations_BookMadeBy] FOREIGN KEY (BookMadeBy) REFERENCES Employees ([EmployeeCode]) ON DELETE NO ACTION


)
