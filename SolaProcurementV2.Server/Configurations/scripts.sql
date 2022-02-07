CREATE TABLE Example_SystemData
(
	Id int NOT NULL PRIMARY KEY,
	Code nvarchar(30) NOT NULL UNIQUE,
	Name nvarchar(255) NOT NULL,
	Description nvarchar(255)
);


CREATE TABLE Example_ReferenceData
(
	Id int NOT NULL IDENTITY (1,1) PRIMARY KEY,
	Code nvarchar(30) NOT NULL UNIQUE,
	Name nvarchar(255) NOT NULL,
	Description nvarchar(255),
	StatusId int NOT NULL FOREIGN KEY REFERENCES Status(Id),
	BusinessUnitId int NOT NULL FOREIGN KEY REFERENCES BusinessUnit(Id),
	CreatedOn datetime NOT NULL,
	CreatedBy int NOT NULL FOREIGN KEY REFERENCES AppUser(Id),
	UpdatedOn datetime NOT NULL,
	UpdatedBy int NOT NULL FOREIGN KEY REFERENCES AppUser(Id)
);


CREATE TABLE AppUser
(
	Id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	AspNetUserId int NOT NULL UNIQUE FOREIGN KEY REFERENCES AspNetUsers(Id),
	UserName nvarchar(256) NOT NULL
);

CREATE TABLE Status
(
	Id int NOT NULL PRIMARY KEY,
	Code nvarchar(30) NOT NULL UNIQUE,
	Name nvarchar(255) NOT NULL,
	Description nvarchar(255)
);

INSERT INTO Status (Id, Code, Name) VALUES (0, N'O', N'Open'), (1, N'C', N'Closed'), (2, N'H', N'Hidden');

CREATE TABLE Menu
(
	Id int NOT NULL PRIMARY KEY,
	Code nvarchar(30) NOT NULL UNIQUE,
	Name nvarchar(255) NOT NULL,
	Description nvarchar(255),
	Url nvarchar(255),
	Icon nvarchar(255),
	ParentId int FOREIGN KEY REFERENCES Menu(Id),
	Sequence int NOT NULL UNIQUE,
	IsFunction bit NOT NULL
);

INSERT INTO Menu (Id, Code, Name, Description, Url, Icon, ParentId, Sequence, Hidden) VALUES 
(1100, N'user_manager', N'User Manager', N'User Manager', null, N'ManageAccounts', null, 100, 0),
(1101, N'users', N'Users', N'Users', N'/users', N'AccountCircle', 1100, 101, 1),
(1111, N'user_groups', N'User Groups', N'User Groups', N'/usergroups', N'SupervisedUserCircle', 1100, 111, 1);

CREATE TABLE BusinessUnit
(
	Id int NOT NULL IDENTITY (1,1) PRIMARY KEY,
	Code nvarchar(30) NOT NULL UNIQUE,
	Name nvarchar(255) NOT NULL,
	Description nvarchar(255),
	StatusId int NOT NULL FOREIGN KEY REFERENCES Status(Id),
	CreatedOn datetime NOT NULL,
	CreatedBy int NOT NULL FOREIGN KEY REFERENCES AppUser(Id),
	UpdatedOn datetime NOT NULL,
	UpdatedBy int NOT NULL FOREIGN KEY REFERENCES AppUser(Id)
);

CREATE TABLE ApproveRole
(
	Id int NOT NULL IDENTITY (1,1) PRIMARY KEY,
	Code nvarchar(30) NOT NULL UNIQUE,
	Name nvarchar(255) NOT NULL,
	Description nvarchar(255),
	StatusId int NOT NULL FOREIGN KEY REFERENCES Status(Id),
	BusinessUnitId int NOT NULL FOREIGN KEY REFERENCES BusinessUnit(Id),
	CreatedOn datetime NOT NULL,
	CreatedBy int NOT NULL FOREIGN KEY REFERENCES AppUser(Id),
	UpdatedOn datetime NOT NULL,
	UpdatedBy int NOT NULL FOREIGN KEY REFERENCES AppUser(Id)
);

CREATE TABLE UserGroup
(
	Id int NOT NULL IDENTITY (1,1) PRIMARY KEY,
	Code nvarchar(30) NOT NULL UNIQUE,
	Name nvarchar(255) NOT NULL,
	Description nvarchar(255),
	StatusId int NOT NULL FOREIGN KEY REFERENCES Status(Id),
	CreatedOn datetime NOT NULL,
	CreatedBy int NOT NULL FOREIGN KEY REFERENCES AppUser(Id),
	UpdatedOn datetime NOT NULL,
	UpdatedBy int NOT NULL FOREIGN KEY REFERENCES AppUser(Id)
);

CREATE TABLE UserGroupAppUser
(
	Id int NOT NULL IDENTITY (1,1) PRIMARY KEY,
	UserGroupId int NOT NULL FOREIGN KEY REFERENCES UserGroup(Id),
	AppUserId int NOT NULL FOREIGN KEY REFERENCES AppUser(AspNetUserId),
	Description nvarchar(255),

	CONSTRAINT UC_UserGroupAppUser UNIQUE (UserGroupId, AppUserId)
);

CREATE TABLE UserGroupMenu
(
	Id int NOT NULL IDENTITY (1,1) PRIMARY KEY,
	UserGroupId int NOT NULL FOREIGN KEY REFERENCES UserGroup(Id),
	MenuId int NOT NULL FOREIGN KEY REFERENCES Menu(Id),
	Description nvarchar(255),
	CreateAccess int NOT NULL,
	ReadAccess int NOT NULL,
	UpdateAccess int NOT NULL,
	DeleteAccess int NOT NULL

	CONSTRAINT UC_UserGroupMenu UNIQUE (UserGroupId, MenuId)
);

CREATE TABLE UserGroupBusinessUnit
(
	Id int NOT NULL IDENTITY (1,1) PRIMARY KEY,
	UserGroupId int NOT NULL FOREIGN KEY REFERENCES UserGroup(Id),
	BusinessUnitId int NOT NULL FOREIGN KEY REFERENCES BusinessUnit(Id),
	Description nvarchar(255),

	CONSTRAINT UC_UserGroupBusinessUnit UNIQUE (UserGroupId, BusinessUnitId)
);

CREATE TABLE UserGroupApproveRole
(
	Id int NOT NULL IDENTITY (1,1) PRIMARY KEY,
	UserGroupId int NOT NULL FOREIGN KEY REFERENCES UserGroup(Id),
	ApproveRoleId int NOT NULL FOREIGN KEY REFERENCES ApproveRole(Id),
	Description nvarchar(255),

	CONSTRAINT UC_UserGroupApproveRole UNIQUE (UserGroupId, ApproveRoleId)
);

CREATE TABLE AnalysisDimension
(
	Id int NOT NULL IDENTITY (1,1) PRIMARY KEY,
	Code nvarchar(30) NOT NULL UNIQUE,
	Name nvarchar(255) NOT NULL,
	Description nvarchar(255),
	Length int NOT NULL,
	StatusId int NOT NULL FOREIGN KEY REFERENCES Status(Id),
	CreatedOn datetime NOT NULL,
	CreatedBy int NOT NULL FOREIGN KEY REFERENCES AppUser(Id),
	UpdatedOn datetime NOT NULL,
	UpdatedBy int NOT NULL FOREIGN KEY REFERENCES AppUser(Id)
);

CREATE TABLE AnalysisStructure
(
	Id int NOT NULL IDENTITY (1,1) PRIMARY KEY,
	Description nvarchar(255),
	BusinessUnitId int NOT NULL FOREIGN KEY REFERENCES BusinessUnit(Id),
	MenuId int NOT NULL FOREIGN KEY REFERENCES Menu(Id),
	CreatedOn datetime NOT NULL,
	CreatedBy int NOT NULL FOREIGN KEY REFERENCES AppUser(Id),
	UpdatedOn datetime NOT NULL,
	UpdatedBy int NOT NULL FOREIGN KEY REFERENCES AppUser(Id),
	AnalysisDimensionId01 int FOREIGN KEY REFERENCES AnalysisDimension(Id),
	AnalysisDimensionId02 int FOREIGN KEY REFERENCES AnalysisDimension(Id),
	AnalysisDimensionId03 int FOREIGN KEY REFERENCES AnalysisDimension(Id),
	AnalysisDimensionId04 int FOREIGN KEY REFERENCES AnalysisDimension(Id),
	AnalysisDimensionId05 int FOREIGN KEY REFERENCES AnalysisDimension(Id),
	AnalysisDimensionId06 int FOREIGN KEY REFERENCES AnalysisDimension(Id),
	AnalysisDimensionId07 int FOREIGN KEY REFERENCES AnalysisDimension(Id),
	AnalysisDimensionId08 int FOREIGN KEY REFERENCES AnalysisDimension(Id),
	AnalysisDimensionId09 int FOREIGN KEY REFERENCES AnalysisDimension(Id),
	AnalysisDimensionId10 int FOREIGN KEY REFERENCES AnalysisDimension(Id),

	CONSTRAINT UC_AnalysisMenuBusinessUnit UNIQUE (MenuId, BusinessUnitId)
);


CREATE TABLE Analysis
(
	Id int NOT NULL IDENTITY (1,1) PRIMARY KEY,
	Code nvarchar(30) NOT NULL UNIQUE,
	Name nvarchar(255) NOT NULL,
	Description nvarchar(255),
	StatusId int NOT NULL FOREIGN KEY REFERENCES Status(Id),
	AnalysisDimensionId int FOREIGN KEY REFERENCES AnalysisDimension(Id),
	CreatedOn datetime NOT NULL,
	CreatedBy int NOT NULL FOREIGN KEY REFERENCES AppUser(Id),
	UpdatedOn datetime NOT NULL,
	UpdatedBy int NOT NULL FOREIGN KEY REFERENCES AppUser(Id)
);