IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241022220348_Created_EmployeeTable_and_DesignationTable'
)
BEGIN
    CREATE TABLE [Designations] (
        [DesId] int NOT NULL IDENTITY,
        [Title] nvarchar(100) NOT NULL,
        [Description] nvarchar(500) NOT NULL,
        [Department] nvarchar(100) NOT NULL,
        [Salary] float NULL,
        [IsActive] bit NULL,
        [CreatedDate] datetime2 NULL,
        [UpdatedDate] datetime2 NULL,
        CONSTRAINT [PK_Designations] PRIMARY KEY ([DesId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241022220348_Created_EmployeeTable_and_DesignationTable'
)
BEGIN
    CREATE TABLE [Employees] (
        [EmpId] int NOT NULL IDENTITY,
        [FirstName] nvarchar(50) NOT NULL,
        [LastName] nvarchar(50) NOT NULL,
        [Phone] nvarchar(max) NULL,
        [Email] nvarchar(max) NOT NULL,
        [EmpAge] int NOT NULL,
        [DateOfJoining] datetime2 NOT NULL,
        [Gender] int NOT NULL,
        [IsMarried] bit NOT NULL,
        [IsActive] bit NOT NULL,
        [DesId] int NULL,
        CONSTRAINT [PK_Employees] PRIMARY KEY ([EmpId]),
        CONSTRAINT [FK_Employees_Designations_DesId] FOREIGN KEY ([DesId]) REFERENCES [Designations] ([DesId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241022220348_Created_EmployeeTable_and_DesignationTable'
)
BEGIN
    CREATE INDEX [IX_Employees_DesId] ON [Employees] ([DesId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241022220348_Created_EmployeeTable_and_DesignationTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241022220348_Created_EmployeeTable_and_DesignationTable', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241022223913_Added_optional_employee_to_Designation'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241022223913_Added_optional_employee_to_Designation', N'8.0.0');
END;
GO

COMMIT;
GO

