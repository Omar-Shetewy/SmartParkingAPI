CREATE DATABASE SmartParkingDB;

USE SmartParkingDB;


CREATE TABLE Persons (
    PersonID INT PRIMARY KEY,
    FirstName NVARCHAR(255),
    LastName NVARCHAR(255),
    DateOfBirth DATETIME,
    Email NVARCHAR(255),
    Password NVARCHAR(255),
    Phone NVARCHAR(20),
    Address NVARCHAR(255),
    Gender BIT
);

CREATE TABLE Users (
    UserID INT PRIMARY KEY,
    PersonID INT,
    FOREIGN KEY (PersonID) REFERENCES Persons(PersonID)
);

CREATE TABLE Owners (
    OwnerID INT PRIMARY KEY,
    PersonID INT,
    GarageContract VARBINARY(MAX),
    FOREIGN KEY (PersonID) REFERENCES Persons(PersonID)
);

CREATE TABLE Positions (
    SpecializationID INT PRIMARY KEY,
    PositionName NVARCHAR(255)
);

CREATE TABLE Garages (
    GarageID INT PRIMARY KEY,
    UserID INT,
    OwnerID INT,
    TotalSpots INT,
    AvailableSpots INT,
    Location NVARCHAR(255),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (OwnerID) REFERENCES Owners(OwnerID)
);

CREATE TABLE Employees (
    EmpID INT PRIMARY KEY,
    PersonID INT,
    SpecializationID INT,
    GarageID INT,
    Salary MONEY,
    FOREIGN KEY (PersonID) REFERENCES Persons(PersonID),
    FOREIGN KEY (SpecializationID) REFERENCES Positions(SpecializationID),
    FOREIGN KEY (GarageID) REFERENCES Garages(GarageID)
);

CREATE TABLE Sensors (
    SensorID INT PRIMARY KEY,
    Status BIT
);
CREATE TABLE Spots (
    SpotID INT PRIMARY KEY,
    GarageID INT,
    SensorID INT,
    SpotNumber INT,
    FOREIGN KEY (GarageID) REFERENCES Garages(GarageID),
    FOREIGN KEY (SensorID) REFERENCES Sensors(SensorID)
);


CREATE TABLE Cars (
    CarID INT PRIMARY KEY,
    UserID INT,
    SpotID INT,
    CarLicense NVARCHAR(50),
    PlateNumber NVARCHAR(50),
    Model NVARCHAR(50),
    Type NVARCHAR(50),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (SpotID) REFERENCES Spots(SpotID)
);

CREATE TABLE Tickets (
    TicketID INT PRIMARY KEY,
    UserID INT,
    QRCode INT,
    IssueDateTime DATETIME,
    ExpirationDateTime DATETIME,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);


CREATE TABLE PaymentMethods (
    PaymentMethodID INT PRIMARY KEY,
    MethodName NVARCHAR(255)
);

CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY,
    PaymentMethodID INT,
    Amount MONEY,
    PaymentStatus NVARCHAR(255),
    FOREIGN KEY (PaymentMethodID) REFERENCES PaymentMethods(PaymentMethodID)
);

CREATE TABLE Reservations (
    ReservationID INT PRIMARY KEY,
    UserID INT,
    PaymentID INT,
    GarageID INT,
    StartDateTime DATETIME,
    EndDateTime DATETIME,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (PaymentID) REFERENCES Payments(PaymentID),
    FOREIGN KEY (GarageID) REFERENCES Garages(GarageID)
);


CREATE TABLE GuestUsers (
    GuestID INT PRIMARY KEY,
    PaymentID INT,
    PlateNumber NVARCHAR(50),
    StartDateTime DATETIME,
    EndDateTime DATETIME,
    FOREIGN KEY (PaymentID) REFERENCES Payments(PaymentID)
);

CREATE TABLE Gate (
    GateID INT PRIMARY KEY,
    GarageID INT,
    GateType NVARCHAR(255),
    FOREIGN KEY (GarageID) REFERENCES Garages(GarageID)
);

CREATE TABLE Cameras (
    CameraID INT PRIMARY KEY,
    GateID INT,
    GarageID INT,
    Location NVARCHAR(255),
    FOREIGN KEY (GateID) REFERENCES Gate(GateID),
    FOREIGN KEY (GarageID) REFERENCES Garages(GarageID)
);
