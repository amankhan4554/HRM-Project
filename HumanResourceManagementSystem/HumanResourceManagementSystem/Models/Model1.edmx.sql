
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/11/2022 06:59:53
-- Generated from EDMX file: G:\FYP\HumanResourceManagementSystem\HumanResourceManagementSystem\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [HumanResourceManagementSystem];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Attendance_Employee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Attendance] DROP CONSTRAINT [FK_Attendance_Employee];
GO
IF OBJECT_ID(N'[dbo].[FK_DepartmentEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employee] DROP CONSTRAINT [FK_DepartmentEmployee];
GO
IF OBJECT_ID(N'[dbo].[FK_Leave_Employee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Leave] DROP CONSTRAINT [FK_Leave_Employee];
GO
IF OBJECT_ID(N'[dbo].[FK_Salary_Employee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Salary] DROP CONSTRAINT [FK_Salary_Employee];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Admin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Admin];
GO
IF OBJECT_ID(N'[dbo].[Attendance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Attendance];
GO
IF OBJECT_ID(N'[dbo].[Department]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Department];
GO
IF OBJECT_ID(N'[dbo].[Employee]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employee];
GO
IF OBJECT_ID(N'[dbo].[Event]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Event];
GO
IF OBJECT_ID(N'[dbo].[Leave]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Leave];
GO
IF OBJECT_ID(N'[dbo].[Notice]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notice];
GO
IF OBJECT_ID(N'[dbo].[Salary]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Salary];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Admin'
CREATE TABLE [dbo].[Admin] (
    [AdminId] int IDENTITY(1,1) NOT NULL,
    [Email] nvarchar(100)  NULL,
    [Password] nvarchar(100)  NULL
);
GO

-- Creating table 'Attendance'
CREATE TABLE [dbo].[Attendance] (
    [AttendanceId] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NULL,
    [Status] nvarchar(50)  NULL,
    [WorkDetail] nvarchar(250)  NULL,
    [EmployeeId] bigint  NOT NULL
);
GO

-- Creating table 'Department'
CREATE TABLE [dbo].[Department] (
    [DepartmentId] bigint IDENTITY(1,1) NOT NULL,
    [DepartmentName] nvarchar(100)  NULL,
    [DepartmentDescription] varchar(255)  NULL
);
GO

-- Creating table 'Employee'
CREATE TABLE [dbo].[Employee] (
    [EmployeeId] bigint IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(100)  NULL,
    [LastName] nvarchar(100)  NULL,
    [Password] nvarchar(100)  NOT NULL,
    [DateOFBirth] datetime  NULL,
    [Gender] nvarchar(30)  NULL,
    [Address] nvarchar(100)  NULL,
    [Mobile] nvarchar(15)  NULL,
    [DepartmentId] bigint  NOT NULL,
    [username] nvarchar(500)  NULL,
    [EmpSalary] nvarchar(50)  NULL
);
GO

-- Creating table 'Event'
CREATE TABLE [dbo].[Event] (
    [EventId] bigint IDENTITY(1,1) NOT NULL,
    [Subject] nvarchar(75)  NOT NULL,
    [StartTime] datetime  NULL,
    [EndTime] datetime  NULL,
    [Description] varchar(max)  NULL
);
GO

-- Creating table 'Leave'
CREATE TABLE [dbo].[Leave] (
    [LeaveId] int IDENTITY(1,1) NOT NULL,
    [Subject] varchar(50)  NULL,
    [Date] datetime  NULL,
    [Description] varchar(max)  NULL,
    [EmployeeId] bigint  NOT NULL,
    [Status] nvarchar(50)  NULL
);
GO

-- Creating table 'Notice'
CREATE TABLE [dbo].[Notice] (
    [NoticeId] bigint IDENTITY(1,1) NOT NULL,
    [Subject] nvarchar(75)  NOT NULL,
    [Date] datetime  NULL,
    [Time] time  NULL,
    [Description] varchar(max)  NULL
);
GO

-- Creating table 'Salary'
CREATE TABLE [dbo].[Salary] (
    [SalaryId] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NULL,
    [RemaningSalary] nvarchar(250)  NULL,
    [EmployeeId] bigint  NOT NULL,
    [TotalSalary] nvarchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [AdminId] in table 'Admin'
ALTER TABLE [dbo].[Admin]
ADD CONSTRAINT [PK_Admin]
    PRIMARY KEY CLUSTERED ([AdminId] ASC);
GO

-- Creating primary key on [AttendanceId] in table 'Attendance'
ALTER TABLE [dbo].[Attendance]
ADD CONSTRAINT [PK_Attendance]
    PRIMARY KEY CLUSTERED ([AttendanceId] ASC);
GO

-- Creating primary key on [DepartmentId] in table 'Department'
ALTER TABLE [dbo].[Department]
ADD CONSTRAINT [PK_Department]
    PRIMARY KEY CLUSTERED ([DepartmentId] ASC);
GO

-- Creating primary key on [EmployeeId] in table 'Employee'
ALTER TABLE [dbo].[Employee]
ADD CONSTRAINT [PK_Employee]
    PRIMARY KEY CLUSTERED ([EmployeeId] ASC);
GO

-- Creating primary key on [EventId] in table 'Event'
ALTER TABLE [dbo].[Event]
ADD CONSTRAINT [PK_Event]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
GO

-- Creating primary key on [LeaveId] in table 'Leave'
ALTER TABLE [dbo].[Leave]
ADD CONSTRAINT [PK_Leave]
    PRIMARY KEY CLUSTERED ([LeaveId] ASC);
GO

-- Creating primary key on [NoticeId] in table 'Notice'
ALTER TABLE [dbo].[Notice]
ADD CONSTRAINT [PK_Notice]
    PRIMARY KEY CLUSTERED ([NoticeId] ASC);
GO

-- Creating primary key on [SalaryId] in table 'Salary'
ALTER TABLE [dbo].[Salary]
ADD CONSTRAINT [PK_Salary]
    PRIMARY KEY CLUSTERED ([SalaryId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [EmployeeId] in table 'Attendance'
ALTER TABLE [dbo].[Attendance]
ADD CONSTRAINT [FK_Attendance_Employee]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[Employee]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Attendance_Employee'
CREATE INDEX [IX_FK_Attendance_Employee]
ON [dbo].[Attendance]
    ([EmployeeId]);
GO

-- Creating foreign key on [DepartmentId] in table 'Employee'
ALTER TABLE [dbo].[Employee]
ADD CONSTRAINT [FK_DepartmentEmployee]
    FOREIGN KEY ([DepartmentId])
    REFERENCES [dbo].[Department]
        ([DepartmentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DepartmentEmployee'
CREATE INDEX [IX_FK_DepartmentEmployee]
ON [dbo].[Employee]
    ([DepartmentId]);
GO

-- Creating foreign key on [EmployeeId] in table 'Leave'
ALTER TABLE [dbo].[Leave]
ADD CONSTRAINT [FK_Leave_Employee]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[Employee]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Leave_Employee'
CREATE INDEX [IX_FK_Leave_Employee]
ON [dbo].[Leave]
    ([EmployeeId]);
GO

-- Creating foreign key on [EmployeeId] in table 'Salary'
ALTER TABLE [dbo].[Salary]
ADD CONSTRAINT [FK_Salary_Employee]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[Employee]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Salary_Employee'
CREATE INDEX [IX_FK_Salary_Employee]
ON [dbo].[Salary]
    ([EmployeeId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------