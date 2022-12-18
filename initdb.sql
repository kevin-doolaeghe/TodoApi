CREATE TABLE IF NOT EXISTS TodoItems
(
    Id int PRIMARY KEY NOT NULL AUTO_INCREMENT,
    Name varchar(100),
    IsComplete boolean NOT NULL DEFAULT false
)