DECLARE @a nvarchar(MAX) = '
CREATE TABLE tblCity (
Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
Name nvarchar(MAX) NOT NULL
)'

DECLARE @b nvarchar(MAX) = '
CREATE TABLE [tblUser] (
Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
Name nvarchar(MAX) NOT NULL CHECK (LEN(Name) > 0),
telNummer nvarchar(MAX) NOT NULL,
CityId INT NOT NULL FOREIGN KEY (CityId) REFERENCES tblCity(Id)
)'

IF NOT EXISTS (SELECT * FROM sys.tables WHERE OBJECT_ID = OBJECT_ID('[dbo].[tblCity]'))
EXECUTE(@a)

IF NOT EXISTS (SELECT * FROM sys.tables WHERE OBJECT_ID = OBJECT_ID('[dbo].[tblUser]'))
EXECUTE(@b)

