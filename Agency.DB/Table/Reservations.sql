CREATE TABLE [dbo].[Reservations]
(
	[Id] INT NOT NULL IDENTITY, 
    [ClientName] NVARCHAR(80) NOT NULL , 
    [DepartureDate] DATETIME NOT NULL, 
    [BookMadeBy] UNIQUEIDENTIFIER NOT NULL,
    -- constraints
    CONSTRAINT [PK_Reservations] PRIMARY KEY (Id),
    CONSTRAINT [FK_Reservations_BookMadeBy] FOREIGN KEY (BookMadeBy) REFERENCES Employees ([EmployeeCode])


)
