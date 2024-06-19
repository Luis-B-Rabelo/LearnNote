CREATE DATABASE LearnNote 
DEFAULT CHARACTER SET utf8
DEFAULT COLLATE utf8_general_ci;
 
USE LearnNote;
 
CREATE TABLE UserTable(  
	userId INT UNSIGNED NOT NULL AUTO_INCREMENT, 
    userEmail VARCHAR (320) NOT NULL,
    userName VARCHAR (255) NOT NULL,
    userPassword VARCHAR (255) NOT NULL,
    
    PRIMARY KEY (userId)
 
) DEFAULT CHARSET = utf8;
 
CREATE TABLE NotebookTable(
 
	notebookId INT UNSIGNED NOT NULL AUTO_INCREMENT,
    notebookTitle VARCHAR (255) NOT NULL, 
    notebookQntNotes TINYINT UNSIGNED NOT NULL,
    
    userIdFk INT UNSIGNED,
    
    PRIMARY KEY (notebookId),
    FOREIGN KEY (userIdFk) REFERENCES userTable (userId)
 
) DEFAULT CHARSET = utf8;
 
CREATE TABLE ActivityTable(
 
	activityId INT UNSIGNED NOT NULL AUTO_INCREMENT,
    activityName VARCHAR (255) NOT NULL,
    activityDescription VARCHAR (1023), 
    activityDayWeek TINYINT UNSIGNED NOT NULL, 
    activityStartTime TIME NOT NULL,
    activityEndTime TIME NOT NULL,
    
    userIdFk INT UNSIGNED,
    
    PRIMARY KEY (activityId),
    FOREIGN KEY (userIdFk) REFERENCES userTable (userId)
 
) DEFAULT CHARSET = utf8;
 
CREATE TABLE EventTable(
 
	eventId INT UNSIGNED NOT NULL AUTO_INCREMENT,
    eventName VARCHAR (255) NOT NULL,
    eventDescription VARCHAR (1023),
    eventDate DATE NOT NULL,
    eventTime TIME,
    
    notebookIdFk INT UNSIGNED,
    
    PRIMARY KEY (eventId),
    FOREIGN KEY (notebookIdFk) REFERENCES notebookTable (notebookId)
 
) DEFAULT CHARSET = utf8;
 
CREATE TABLE NoteTable(
 
	noteId INT UNSIGNED NOT NULL AUTO_INCREMENT, 
    noteTitle VARCHAR (255) NOT NULL,
    noteCreationDate DATE NOT NULL,
    noteLastEditDateTime DATETIME NOT NULL,
    
	userIdFk INT UNSIGNED,
	notebookIdFk INT UNSIGNED,
    
    PRIMARY KEY (noteId),
	FOREIGN KEY (userIdFk) REFERENCES usertable (userId),
    CONSTRAINT notebookIdFk FOREIGN KEY (notebookIdFk)
	REFERENCES notebooktable(notebookId)
    ON DELETE CASCADE
) DEFAULT CHARSET = utf8;

CREATE TABLE TestTable(

	testId TINYINT UNSIGNED NOT NULL AUTO_INCREMENT, 
    
    PRIMARY KEY (testId)
) DEFAULT CHARSET = utf8;

INSERT INTO testtable() VALUE ();