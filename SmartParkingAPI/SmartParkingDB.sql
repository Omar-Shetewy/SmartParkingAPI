CREATE DATABASE SmartParkingDB;

USE SmartParkingDB;


CREATE TABLE Persons (
    PersonID INT PRIMARY KEY IDENTITY(1,1),
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
    UserID INT PRIMARY KEY IDENTITY(1,1),
    PersonID INT,
    FOREIGN KEY (PersonID) REFERENCES Persons(PersonID)
);

CREATE TABLE Owners (
    OwnerID INT PRIMARY KEY IDENTITY(1,1),
    PersonID INT,
    GarageContract VARBINARY(MAX),
    FOREIGN KEY (PersonID) REFERENCES Persons(PersonID)
);

CREATE TABLE Positions (
    SpecializationID INT PRIMARY KEY IDENTITY(1,1),
    PositionName NVARCHAR(255)
);

CREATE TABLE Garages (
    GarageID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    OwnerID INT,
    TotalSpots INT,
    AvailableSpots INT,
    Location NVARCHAR(255),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (OwnerID) REFERENCES Owners(OwnerID)
);

CREATE TABLE Employees (
    EmpID INT PRIMARY KEY IDENTITY(1,1),
    PersonID INT,
    SpecializationID INT,
    GarageID INT,
    Salary MONEY,
    FOREIGN KEY (PersonID) REFERENCES Persons(PersonID),
    FOREIGN KEY (SpecializationID) REFERENCES Positions(SpecializationID),
    FOREIGN KEY (GarageID) REFERENCES Garages(GarageID)
);

CREATE TABLE Sensors (
    SensorID INT PRIMARY KEY IDENTITY(1,1),
    IsOccupied BIT      -- Spot occupancy status
);

CREATE TABLE Spots (
    SpotID INT PRIMARY KEY IDENTITY(1,1),
    GarageID INT,
    SensorID INT,
    SpotNumber INT, -- ????
    FOREIGN KEY (GarageID) REFERENCES Garages(GarageID),
    FOREIGN KEY (SensorID) REFERENCES Sensors(SensorID)
);

CREATE TABLE Cars (
    CarID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    SpotID INT,
    PlateNumber NVARCHAR(50),
    EntryDateTime DATETIME,
    ExitDateTime DATETIME,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (SpotID) REFERENCES Spots(SpotID)
);

CREATE TABLE Tickets (
    TicketID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    ReservationID INT,
    QRCode NVARCHAR(255),
    IssueDateTime DATETIME,
    ExpirationDateTime DATETIME,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (ReservationID) REFERENCES Reservations(ReservationID)
);

CREATE TABLE PaymentMethods (
    PaymentMethodID INT PRIMARY KEY IDENTITY(1,1),
    MethodName NVARCHAR(255)
);

CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    PaymentMethodID INT,
    ReservationID INT,
    Amount MONEY,
    PaymentStatus INT, -- Use an enum or lookup table (0 = Pending, 1 = Completed, 2 = Failed)
    FOREIGN KEY (PaymentMethodID) REFERENCES PaymentMethods(PaymentMethodID),
    FOREIGN KEY (ReservationID) REFERENCES Reservations(ReservationID)
);

CREATE TABLE Reservations (
    ReservationID INT PRIMARY KEY IDENTITY(1,1),
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
    GuestID INT PRIMARY KEY IDENTITY(1,1),
    PaymentID INT,
    GarageID INT,
    PlateNumber NVARCHAR(50),
    StartDateTime DATETIME,
    EndDateTime DATETIME,
    FOREIGN KEY (PaymentID) REFERENCES Payments(PaymentID),
    FOREIGN KEY (GarageID) REFERENCES Garages(GarageID)
);

CREATE TABLE Gate (
    GateID INT PRIMARY KEY IDENTITY(1,1),
    GarageID INT,
    GateType NVARCHAR(255),
    FOREIGN KEY (GarageID) REFERENCES Garages(GarageID)
);

CREATE TABLE Cameras (
    CameraID INT PRIMARY KEY IDENTITY(1,1),
    GateID INT,
    GarageID INT,
    Location NVARCHAR(255),
    FOREIGN KEY (GateID) REFERENCES Gate(GateID),
    FOREIGN KEY (GarageID) REFERENCES Garages(GarageID)
);

-- new table
CREATE TABLE Pricing (
    PricingID INT PRIMARY KEY IDENTITY(1,1),
    GarageID INT,
    RatePerHour MONEY,
    FlatRate MONEY,
    FOREIGN KEY (GarageID) REFERENCES Garages(GarageID)
);


