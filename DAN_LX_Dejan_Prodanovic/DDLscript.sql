--we create database  
CREATE DATABASE EmployeeDb;

GO

use EmployeeDb;

GO

 --we delete tables in case they exist
DROP TABLE IF EXISTS tblEmployee;
 DROP TABLE IF EXISTS tblSector;

--we create table tblSector
 CREATE TABLE tblSector (
    SectorID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    SectorName varchar(50)
 
);
 
--we create table tblSector
 CREATE TABLE tblLocation (
    LocationID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Street varchar(50),
	Number varchar(50),
	City varchar(50),
	Country varchar(50)
);

--we create table tblUser
 CREATE TABLE tblEmployee (
    EmployeeID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    FirstName varchar(50),
	LastName varchar(50),
	Gender varchar(50),
	DateOfBirth date,
    JMBG varchar(50),
	IDNumber varchar(50),
	PhoneNumber varchar(50),
	ManagerId int FOREIGN KEY REFERENCES tblEmployee(EmployeeID),
	SectorID int FOREIGN KEY REFERENCES tblSector(SectorID),
	LocationID int FOREIGN KEY REFERENCES tblLocation(LocationID)    
 
);
 