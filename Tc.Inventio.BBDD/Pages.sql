CREATE TABLE [dbo].[Pages]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [GUID] VARCHAR(100) NULL, 
    [Path] VARCHAR(2000) NOT NULL, 
    [Title] VARCHAR(500) NOT NULL, 
    [LayoutId] INT NOT NULL, 
    [State] INT NOT NULL, 
    [PubVersion] VARCHAR(10) NULL, 
    [Metadata] VARCHAR(MAX) NULL, 
    [MetadataVersion] VARCHAR(10) NULL, 
    [Action] INT NULL, 
    [IdUser] INT NULL
)
