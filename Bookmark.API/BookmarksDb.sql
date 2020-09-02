CREATE DATABASE [Bookmarks]
GO

USE [Bookmarks]
GO

DROP TABLE Article
DROP TABLE Bookmark
GO

CREATE LOGIN webapp WITH PASSWORD=N'P@ssw0rd!', DEFAULT_DATABASE=Bookmarks

GO

ALTER LOGIN webapp ENABLE

GO

CREATE USER webapp FOR LOGIN webapp
EXEC sp_addrolemember 'db_owner', 'webapp'

CREATE TABLE [dbo].[Bookmark](
	[Id] [varchar](255) PRIMARY KEY NOT NULL,
	[Name] [varchar](255)
)
GO

CREATE TABLE [dbo].[Article](
	[Id] [varchar](255) PRIMARY KEY NOT NULL,
	[Name] [varchar](255),
	[Website] [varchar](255),
	[BookmarkId] [varchar](255) FOREIGN KEY REFERENCES Bookmark(Id)
)
GO

INSERT INTO Bookmark VALUES('b1', 'Bookmark1')
GO

INSERT INTO Bookmark VALUES('b2', 'Bookmark2')
GO


INSERT INTO Article VALUES('a1', 'Article1', 'test.com/1', 'b1')
GO

INSERT INTO Article VALUES('a2', 'Article2', 'test.com/2', 'b1')
GO

INSERT INTO Article VALUES('a3', 'Article3', 'test.com/3', 'b2')
GO

INSERT INTO Article VALUES('a4', 'Article4', 'test.com/4', 'b2')
GO