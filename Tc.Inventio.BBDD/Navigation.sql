CREATE TABLE [dbo].[Navigation]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ParentId] INT NULL, 
    [Path] NVARCHAR(2000) NOT NULL, 
    [NodeType] INT NOT NULL, 
    [Title] NVARCHAR(255) NOT NULL, 
    [MetaData] NVARCHAR(MAX) NULL, 
    [ObjectId] INT NULL
)
