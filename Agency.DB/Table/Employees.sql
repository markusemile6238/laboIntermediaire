CREATE TABLE [dbo].[Employees]
(
	[Id] INT NOT NULL IDENTITY, 
    [Firstname] NVARCHAR(50) NOT NULL, 
    [Lastname] NVARCHAR(50) NOT NULL,
    [Email] NVARCHAR(150) NOT NULL,
    [Password] NVARCHAR(150) NOT NULL, 
    [EmployeeCode] UNIQUEIDENTIFIER NOT NULL, 
    --constraint
    CONSTRAINT [PK_Employees] PRIMARY KEY (Id),
    CONSTRAINT [UK_Employees_Email] UNIQUE (Email),
    CONSTRAINT [UK_Employees_EmployeeCode] UNIQUE (EmployeeCode)
)
