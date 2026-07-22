--Database  and tables create script--
---------------------------------------------------------------------------------------------------------------------

CREATE DATABASE P6Referential;

---------------------------------------------------------------------------------------------------------------------

CREATE TABLE Products
(
	ProductId int IDENTITY PRIMARY KEY,
	ProductName varchar(255) NOT NULL,
); 

---------------------------------------------------------------------------------------------------------------------

CREATE TABLE Versions
(
	VersionId int IDENTITY PRIMARY KEY,
	VersionName varchar(255) NOT NULL,
); 

---------------------------------------------------------------------------------------------------------------------

CREATE TABLE ProductsVersions
(
	ProductVersionId int IDENTITY PRIMARY KEY,

	ProductId int NOT NULL,
	CONSTRAINT fk_Product
	FOREIGN KEY (ProductId)
	REFERENCES Products(ProductId),

	VersionId int NOT NULL,
	CONSTRAINT fk_Version
	FOREIGN KEY (VersionId)
	REFERENCES Versions(VersionId)
); 

---------------------------------------------------------------------------------------------------------------------

CREATE TABLE OperatingSystems
(
	OperatingSystemId int IDENTITY PRIMARY KEY,
	OperatingSystemName varchar(255) NOT NULL,
);

---------------------------------------------------------------------------------------------------------------------

CREATE TABLE RunningSolutions
(
	RunningSolutionId int IDENTITY PRIMARY KEY,

	OperatingSystemId int NOT NULL,
	CONSTRAINT fk_OperatingSystem
	FOREIGN KEY (OperatingSystemId)
	REFERENCES OperatingSystems(OperatingSystemId),

	ProductVersionId int NOT NULL,
	CONSTRAINT fk_ProductVersion
	FOREIGN KEY (ProductVersionId)
	REFERENCES ProductsVersions(ProductVersionId)
);

---------------------------------------------------------------------------------------------------------------------

CREATE TABLE TicketStatuses
(
	TicketStatusId int IDENTITY PRIMARY KEY,
	TicketStatus bit NOT NULL,
);

---------------------------------------------------------------------------------------------------------------------

CREATE TABLE Tickets
(
	TicketId int IDENTITY PRIMARY KEY,

	RunningSolutionId int NOT NULL,
	CONSTRAINT fk_RunningSolution
	FOREIGN KEY (RunningSolutionId)
	REFERENCES RunningSolutions(RunningSolutionId),

	TicketStatusId int NOT NULL,
	CONSTRAINT fk_TicketStatus
	FOREIGN KEY (TicketStatusId)
	REFERENCES TicketStatuses(TicketStatusId),

	TicketDateStart date NOT NULL,
	TicketDateEnd date,
	TicketDescription text NOT NULL,
	TicketFixDescription text,
);

