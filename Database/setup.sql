CREATE TABLE bookmarks(
	id TEXT primary key not null,
	bookmarkName TEXT
);

CREATE TABLE articles(
	id TEXT primary key not null,
	articleName TEXT,
	website TEXT,
	bookmarkId TEXT REFERENCES bookmarks(id)
);

INSERT INTO bookmarks VALUES ('b1', 'bookmark1');
INSERT INTO bookmarks VALUES ('b2', 'bookmark2');


INSERT INTO articles VALUES ('a1', 'article1', 'article1.com', 'b1');
INSERT INTO articles VALUES ('a2', 'article2', 'article2.com', 'b2');
INSERT INTO articles VALUES ('a3', 'article3', 'article3.com', 'b1');
INSERT INTO articles VALUES ('a4', 'article4', 'article4.com', 'b2');