CREATE TABLE [dbo].[Layouts]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] VARCHAR(255) NOT NULL, 
    [Description] VARCHAR(5000) NULL, 
    [PreviewUrl] VARCHAR(500) NULL, 
    [Body] VARCHAR(MAX) NOT NULL
)
