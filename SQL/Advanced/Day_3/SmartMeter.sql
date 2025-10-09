create database SmartMeter

use SmartMeter

CREATE TABLE [User](  
  UserId         BIGINT IDENTITY PRIMARY KEY,  
  Username       NVARCHAR(100) NOT NULL UNIQUE,  
  PasswordHash   VARBINARY(256) NOT NULL,  
  DisplayName    NVARCHAR(150) NOT NULL,  
  Email          NVARCHAR(200) NULL,  
  Phone          NVARCHAR(30) NULL,  
  LastLoginUtc   DATETIME2(3) NULL,  
  IsActive       BIT NOT NULL DEFAULT 1);
 
CREATE TABLE OrgUnit (
  OrgUnitId INT IDENTITY PRIMARY KEY,
  Type VARCHAR(20) NOT NULL CHECK (Type IN ('Zone','Substation','Feeder','DTR')),
  Name NVARCHAR(100) NOT NULL,
  ParentId INT NULL REFERENCES OrgUnit(OrgUnitId)
);
 
CREATE TABLE Tariff (
  TariffId INT IDENTITY PRIMARY KEY,
  Name NVARCHAR(100) NOT NULL,
  EffectiveFrom DATE NOT NULL,
  EffectiveTo DATE NULL,
  BaseRate DECIMAL(18,4) NOT NULL,
  TaxRate DECIMAL(18,4) NOT NULL DEFAULT 0
);
 
CREATE TABLE TodRule (
  TodRuleId      INT IDENTITY PRIMARY KEY,
  TariffId       INT NOT NULL REFERENCES Tariff(TariffId),
  Name           NVARCHAR(50) NOT NULL,
  StartTime      TIME(0) NOT NULL,
  EndTime        TIME(0) NOT NULL,
  RatePerKwh     DECIMAL(18,6) NOT NULL
);
 
 
CREATE TABLE TariffSlab (
  TariffSlabId   INT IDENTITY PRIMARY KEY,
  TariffId       INT NOT NULL REFERENCES Tariff(TariffId),
  FromKwh        DECIMAL(18,6) NOT NULL,
  ToKwh          DECIMAL(18,6) NOT NULL,
  RatePerKwh     DECIMAL(18,6) NOT NULL,
  CONSTRAINT CK_TariffSlab_Range CHECK (FromKwh >= 0 AND ToKwh > FromKwh)
);
 
CREATE TABLE Consumer (
  ConsumerId BIGINT IDENTITY PRIMARY KEY,
  Name NVARCHAR(200) NOT NULL,
  Address NVARCHAR(500) NULL,
  Phone NVARCHAR(30) NULL,
  Email NVARCHAR(200) NULL,
  OrgUnitId INT NOT NULL REFERENCES OrgUnit(OrgUnitId),
  TariffId INT NOT NULL REFERENCES Tariff(TariffId),
  Status VARCHAR(20) NOT NULL DEFAULT 'Active' CHECK (Status IN ('Active','Inactive')),
  CreatedAt DATETIME2(3) NOT NULL DEFAULT SYSUTCDATETIME(),
  CreatedBy NVARCHAR(100) NOT NULL DEFAULT 'system',
  UpdatedAt DATETIME2(3) NULL,
  UpdatedBy NVARCHAR(100) NULL
);
 
CREATE TABLE Meter (
  MeterSerialNo NVARCHAR(50) NOT NULL PRIMARY KEY,
  IpAddress NVARCHAR(45) NOT NULL,
  ICCID NVARCHAR(30) NOT NULL,
  IMSI NVARCHAR(30) NOT NULL,
  Manufacturer NVARCHAR(100) NOT NULL,
  Firmware NVARCHAR(50) NULL,
  Category NVARCHAR(50) NOT NULL,
  InstallTsUtc DATETIME2(3) NOT NULL,
  Status VARCHAR(20) NOT NULL DEFAULT 'Active'
           CHECK (Status IN ('Active','Inactive','Decommissioned')),
  ConsumerId BIGINT NULL REFERENCES Consumer(ConsumerId)
);

-- Insert into User table
INSERT INTO [User] (Username, PasswordHash, DisplayName, Email, Phone, LastLoginUtc, IsActive)
VALUES 
('raj.sharma', 0x48656C6C6F576F726C64, 'Raj Sharma', 'raj.sharma@utility.com', '+91-9876543210', '2024-01-15 08:30:00', 1),
('priya.patel', 0x48656C6C6F576F726C64, 'Priya Patel', 'priya.patel@utility.com', '+91-9876543211', '2024-01-14 14:20:00', 1),
('amit.kumar', 0x48656C6C6F576F726C64, 'Amit Kumar', 'amit.kumar@utility.com', '+91-9876543212', '2024-01-13 11:15:00', 1),
('neha.gupta', 0x48656C6C6F576F726C64, 'Neha Gupta', 'neha.gupta@utility.com', '+91-9876543213', '2024-01-12 16:45:00', 1),
('vikram.singh', 0x48656C6C6F576F726C64, 'Vikram Singh', 'vikram.singh@utility.com', '+91-9876543214', '2024-01-11 09:10:00', 1);

-- Insert into OrgUnit (Hierarchical structure: Zone -> Substation -> Feeder -> DTR)
INSERT INTO OrgUnit (Type, Name, ParentId)
VALUES 
-- Zones
('Zone', 'North Delhi Zone', NULL),
('Zone', 'South Delhi Zone', NULL),
-- Substations (under Zones)
('Substation', 'Rohini Substation', 1),
('Substation', 'Pitampura Substation', 1),
('Substation', 'Saket Substation', 2),
('Substation', 'Hauz Khas Substation', 2),
-- Feeders (under Substations)
('Feeder', 'Rohini Feeder-1', 3),
('Feeder', 'Rohini Feeder-2', 3),
('Feeder', 'Pitampura Feeder-1', 4),
('Feeder', 'Saket Feeder-1', 5),
-- DTRs (under Feeders)
('DTR', 'DTR-RH-001', 7),
('DTR', 'DTR-RH-002', 7),
('DTR', 'DTR-PT-001', 9),
('DTR', 'DTR-SK-001', 10);

-- Insert into Tariff (Realistic Indian electricity tariffs)
INSERT INTO Tariff (Name, EffectiveFrom, EffectiveTo, BaseRate, TaxRate)
VALUES 
('Domestic LT-A (0-200 units)', '2024-01-01', '2024-12-31', 3.5000, 0.0500),
('Domestic LT-B (201-500 units)', '2024-01-01', '2024-12-31', 5.2000, 0.0500),
('Domestic LT-C (Above 500 units)', '2024-01-01', '2024-12-31', 6.8000, 0.0500),
('Commercial LT', '2024-01-01', '2024-12-31', 8.0000, 0.0800),
('Industrial HT', '2024-01-01', '2024-12-31', 7.2000, 0.0800);

-- Insert into TodRule (Time of Day pricing - realistic for Indian utilities)
INSERT INTO TodRule (TariffId, Name, StartTime, EndTime, RatePerKwh)
VALUES 
-- Peak hours pricing for Commercial
(4, 'Commercial Peak', '18:00:00', '22:00:00', 10.500000),
(4, 'Commercial Normal', '06:00:00', '18:00:00', 8.000000),
(4, 'Commercial Off-Peak', '22:00:00', '06:00:00', 6.000000),
-- Industrial TOD rates
(5, 'Industrial Peak', '18:00:00', '22:00:00', 9.000000),
(5, 'Industrial Normal', '06:00:00', '18:00:00', 7.200000),
(5, 'Industrial Off-Peak', '22:00:00', '06:00:00', 5.500000);

-- Insert into TariffSlab (Progressive slab system common in India)
INSERT INTO TariffSlab (TariffId, FromKwh, ToKwh, RatePerKwh)
VALUES 
-- Domestic LT-A slabs
(1, 0.000000, 100.000000, 3.000000),
(1, 100.000000, 200.000000, 4.000000),
-- Domestic LT-B slabs
(2, 200.000000, 300.000000, 5.000000),
(2, 300.000000, 500.000000, 6.000000),
-- Domestic LT-C slabs
(3, 500.000000, 1000.000000, 7.000000),
(3, 1000.000000, 999999.000000, 8.000000);

-- Insert into Consumer (Realistic Indian consumer data)
INSERT INTO Consumer (Name, Address, Phone, Email, OrgUnitId, TariffId, Status, CreatedBy)
VALUES 
('Anil Kumar Sharma', 'H.No. 123, Sector-7, Rohini, Delhi - 110085', '+91-9812345678', 'anil.sharma@email.com', 11, 1, 'Active', 'raj.sharma'),
('Sunita Devi', 'A-45, Pitampura, Near TV Tower, Delhi - 110034', '+91-9812345679', 'sunita.devi@email.com', 13, 1, 'Active', 'priya.patel'),
('Rahul Malhotra', 'Shop No. 25, Main Market, Saket, Delhi - 110017', '+91-9812345680', 'rahul.malhotra@email.com', 14, 4, 'Active', 'amit.kumar'),
('Meera Enterprises', 'Plot No. 45, Industrial Area, Rohini, Delhi - 110085', '+91-9812345681', 'accounts@meeraent.com', 11, 5, 'Active', 'neha.gupta'),
('Kiran Textiles', 'G-12, Commercial Complex, Saket, Delhi - 110017', '+91-9812345682', 'info@kirantextiles.com', 14, 4, 'Active', 'vikram.singh'),
('Dr. Sanjay Verma', 'B-25, Doctors Colony, Hauz Khas, Delhi - 110016', '+91-9812345683', 'sanjay.verma@email.com', 12, 2, 'Active', 'raj.sharma');

-- Insert into Meter (Smart meter data with Indian manufacturers)
INSERT INTO Meter (MeterSerialNo, IpAddress, ICCID, IMSI, Manufacturer, Firmware, Category, InstallTsUtc, Status, ConsumerId)
VALUES 
('DL-MTR-0012345', '192.168.1.101', '8941100000001234567', '404010000012345', 'HPL Electric & Power', 'V2.1.5', 'Single Phase', '2023-12-01 10:00:00', 'Active', 1),
('DL-MTR-0012346', '192.168.1.102', '8941100000001234568', '404010000012346', 'HPL Electric & Power', 'V2.1.5', 'Single Phase', '2023-12-02 11:30:00', 'Active', 2),
('DL-MTR-0012347', '192.168.1.103', '8941100000001234569', '404010000012347', 'L&T Electrical & Automation', 'V3.0.1', 'Three Phase', '2023-12-03 09:15:00', 'Active', 3),
('DL-MTR-0012348', '192.168.1.104', '8941100000001234570', '404010000012348', 'L&T Electrical & Automation', 'V3.0.1', 'Three Phase', '2023-12-04 14:20:00', 'Active', 4),
('DL-MTR-0012349', '192.168.1.105', '8941100000001234571', '404010000012349', 'HPL Electric & Power', 'V2.1.5', 'Three Phase', '2023-12-05 16:45:00', 'Active', 5),
('DL-MTR-0012350', '192.168.1.106', '8941100000001234572', '404010000012350', 'Secure Meters', 'V2.5.2', 'Single Phase', '2023-12-06 13:10:00', 'Active', 6);

select* from [User]
select* from OrgUnit
select* from Tariff
select* from TodRule
select* from TariffSlab
select* from Consumer
select* from Meter

-- 1. List all active users
SELECT * FROM [User] WHERE IsActive = 1 ORDER BY DisplayName;

-- 2. Show all consumers with their tariff names
SELECT c.ConsumerId, c.Name AS ConsumerName, c.Address, c.Phone, c.Email, c.Status AS ConsumerStatus,
t.Name AS TariffName, t.BaseRate, t.TaxRate FROM Consumer c
INNER JOIN Tariff t ON c.TariffId = t.TariffId ORDER BY c.Name;

-- 3. Count total meters by status
SELECT Status, COUNT(*) AS MeterCount FROM Meter
GROUP BY Status ORDER BY MeterCount DESC;

-- 4. Show all ToD rules for a given Tariff name
SELECT t.Name AS TariffName, tr.Name AS TodRuleName FROM TodRule tr
INNER JOIN Tariff t ON tr.TariffId = t.TariffId WHERE t.Name = 'Commercial LT' ORDER BY tr.StartTime;

-- 5. Get top 5 most recently installed meters
SELECT TOP 5 m.MeterSerialNo, m.IpAddress, m.Manufacturer, m.Category, m.InstallTsUtc, m.Status,
c.Name AS ConsumerName, c.Phone AS ConsumerPhone FROM Meter m LEFT JOIN Consumer c ON m.ConsumerId = c.ConsumerId
ORDER BY m.InstallTsUtc DESC;

-- 6. List consumers without an assigned meter
SELECT c.ConsumerId, c.Name AS ConsumerName, c.Address, c.Phone, c.Email, t.Name AS TariffName, c.Status
FROM Consumer c INNER JOIN Tariff t ON c.TariffId = t.TariffId LEFT JOIN Meter m ON c.ConsumerId = m.ConsumerId
WHERE m.ConsumerId IS NULL ORDER BY c.Name;

-- 7. Find tariffs that have expired (EffectiveTo date is in the past)
SELECT TariffId,Name AS TariffName, EffectiveFrom, EffectiveTo, BaseRate, TaxRate,
DATEDIFF(DAY, EffectiveTo, GETDATE()) AS DaysSinceExpired FROM Tariff
WHERE EffectiveTo < CAST(GETDATE() AS DATE) ORDER BY EffectiveTo DESC;