-- Database: SmartMeter

-- DROP DATABASE IF EXISTS "SmartMeter";

CREATE DATABASE "SmartMeter"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'English_India.1252'
    LC_CTYPE = 'English_India.1252'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;


-- User table
CREATE TABLE "User" (  
  UserId         BIGSERIAL PRIMARY KEY,  
  Username       VARCHAR(100) NOT NULL UNIQUE,  
  PasswordHash   BYTEA NOT NULL,  
  DisplayName    VARCHAR(150) NOT NULL,  
  Email          VARCHAR(200) NULL,  
  Phone          VARCHAR(30) NULL,  
  LastLoginUtc   TIMESTAMP(3) NULL,  
  IsActive       BOOLEAN NOT NULL DEFAULT true
);

-- Organization Unit table
CREATE TABLE OrgUnit (
  OrgUnitId SERIAL PRIMARY KEY,
  Type VARCHAR(20) NOT NULL CHECK (Type IN ('Zone','Substation','Feeder','DTR')),
  Name VARCHAR(100) NOT NULL,
  ParentId INT NULL REFERENCES OrgUnit(OrgUnitId)
);

-- Tariff table
CREATE TABLE Tariff (
  TariffId SERIAL PRIMARY KEY,
  Name VARCHAR(100) NOT NULL,
  EffectiveFrom DATE NOT NULL,
  EffectiveTo DATE NULL,
  BaseRate DECIMAL(18,4) NOT NULL,
  TaxRate DECIMAL(18,4) NOT NULL DEFAULT 0,
  
  -- Constraints
  CONSTRAINT CHK_Tariff_EffectiveDates CHECK (EffectiveTo IS NULL OR EffectiveTo > EffectiveFrom),
  CONSTRAINT CHK_Tariff_BaseRate_Positive CHECK (BaseRate > 0)
);

-- Time of Day Rule table
CREATE TABLE TodRule (
  TodRuleId SERIAL PRIMARY KEY,
  TariffId INT NOT NULL REFERENCES Tariff(TariffId),
  Name VARCHAR(50) NOT NULL,
  StartTime TIME(0) NOT NULL,
  EndTime TIME(0) NOT NULL,
  RatePerKwh DECIMAL(18,6) NOT NULL,
  
  -- Constraints
  CONSTRAINT CHK_TodRule_TimeRange CHECK (EndTime > StartTime),
  CONSTRAINT CHK_TodRule_Rate_Positive CHECK (RatePerKwh > 0)
);

-- Tariff Slab table
CREATE TABLE TariffSlab (
  TariffSlabId SERIAL PRIMARY KEY,
  TariffId INT NOT NULL REFERENCES Tariff(TariffId),
  FromKwh DECIMAL(18,6) NOT NULL,
  ToKwh DECIMAL(18,6) NOT NULL,
  RatePerKwh DECIMAL(18,6) NOT NULL,
  
  -- Constraints
  CONSTRAINT CHK_TariffSlab_Range CHECK (FromKwh >= 0 AND ToKwh > FromKwh),
  CONSTRAINT CHK_TariffSlab_Rate_Positive CHECK (RatePerKwh > 0)
);

-- Consumer table
CREATE TABLE Consumer (
  ConsumerId BIGSERIAL PRIMARY KEY,
  Name VARCHAR(200) NOT NULL,
  Address VARCHAR(500) NULL,
  Phone VARCHAR(30) NULL,
  Email VARCHAR(200) NULL,
  OrgUnitId INT NOT NULL REFERENCES OrgUnit(OrgUnitId),
  TariffId INT NOT NULL REFERENCES Tariff(TariffId),
  Status VARCHAR(20) NOT NULL DEFAULT 'Active' CHECK (Status IN ('Active','Inactive')),
  CreatedAt TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  CreatedBy VARCHAR(100) NOT NULL DEFAULT 'system',
  UpdatedAt TIMESTAMP(3) NULL,
  UpdatedBy VARCHAR(100) NULL,
  
  -- Constraints
  CONSTRAINT CHK_Consumer_UpdatedAfterCreated CHECK (UpdatedAt IS NULL OR UpdatedAt >= CreatedAt)
);

-- Meter table
CREATE TABLE Meter (
  MeterSerialNo VARCHAR(50) NOT NULL PRIMARY KEY,
  IpAddress VARCHAR(45) NOT NULL,
  ICCID VARCHAR(30) NOT NULL,
  IMSI VARCHAR(30) NOT NULL,
  Manufacturer VARCHAR(100) NOT NULL,
  Firmware VARCHAR(50) NULL,
  Category VARCHAR(50) NOT NULL,
  InstallTsUtc TIMESTAMP(3) NOT NULL,
  Status VARCHAR(20) NOT NULL DEFAULT 'Active'
           CHECK (Status IN ('Active','Inactive','Decommissioned')),
  ConsumerId BIGINT NOT NULL REFERENCES Consumer(ConsumerId)
);

-- Meter Reading table for energy consumption data
CREATE TABLE MeterReading (
    ReadingId BIGSERIAL PRIMARY KEY,
    ReadingDate DATE NOT NULL,
    EnergyConsumed DECIMAL(18,6) NOT NULL,
    MeterSerialNo VARCHAR(50) NOT NULL REFERENCES Meter(MeterSerialNo),
    Current DECIMAL(8,3) NULL,
    Voltage DECIMAL(8,3) NULL,
    
    -- Constraints
    CONSTRAINT CHK_MeterReading_EnergyConsumed_Positive CHECK (EnergyConsumed >= 0),
    CONSTRAINT CHK_MeterReading_Voltage_Range CHECK (Voltage IS NULL OR (Voltage >= 0 AND Voltage <= 500)),
    CONSTRAINT CHK_MeterReading_Current_Range CHECK (Current IS NULL OR (Current >= 0 AND Current <= 500))
);

-- Tariff Details table (junction table for tariff calculations)
CREATE TABLE TariffDetails (
  TariffDetailId BIGSERIAL PRIMARY KEY,
  TariffId INT NOT NULL REFERENCES Tariff(TariffId),
  TariffSlabId INT NULL REFERENCES TariffSlab(TariffSlabId),
  TodRuleId INT NULL REFERENCES TodRule(TodRuleId)
);

-- Bill table
CREATE TABLE Bill (
  BillId BIGSERIAL PRIMARY KEY,
  BillDate DATE NOT NULL,
  BillAmount DECIMAL(18,2) NOT NULL,
  MeterSerialNo VARCHAR(50) NOT NULL REFERENCES Meter(MeterSerialNo),
  TariffDetailId BIGINT NOT NULL REFERENCES TariffDetails(TariffDetailId),
  CreateDate TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PaymentDate TIMESTAMP(3) NULL,
  DueDate DATE NOT NULL,
  CreatedBy VARCHAR(100) NOT NULL,
  PrevReading DECIMAL(18,6) NOT NULL,
  CurrReading DECIMAL(18,6) NOT NULL,
  PowerFactor DECIMAL(5,4) NULL,
  LoadFactor DECIMAL(5,4) NULL,
  DisconnectedDate TIMESTAMP(3) NULL,
  
  -- Constraints
  CONSTRAINT CHK_Bill_Amount_Positive CHECK (BillAmount >= 0),
  CONSTRAINT CHK_Bill_Readings_Valid CHECK (CurrReading >= PrevReading)
);

-- Arrears table
CREATE TABLE Arrears (
  ArrearId BIGSERIAL PRIMARY KEY,
  ConsumerId BIGINT NOT NULL REFERENCES Consumer(ConsumerId),
  ArrearType VARCHAR(50) NOT NULL,
  PaidStatus VARCHAR(20) NOT NULL CHECK (PaidStatus IN ('Pending','Paid','Partial')),
  BillId BIGINT NOT NULL REFERENCES Bill(BillId),
  ArrearAmount DECIMAL(18,2) NOT NULL,
  CreatedAt TIMESTAMP(3) DEFAULT CURRENT_TIMESTAMP,
  
  CONSTRAINT CHK_Arrears_Amount_Positive CHECK (ArrearAmount >= 0)
);

-- Drop the voltage range constraint
ALTER TABLE MeterReading DROP CONSTRAINT CHK_MeterReading_Voltage_Range;

-- Drop the current range constraint
ALTER TABLE MeterReading DROP CONSTRAINT CHK_MeterReading_Current_Range;


SELECT * FROM "User";
SELECT * FROM OrgUnit;
SELECT * FROM Tariff;
SELECT * FROM TodRule;
SELECT * FROM TariffSlab;
SELECT * FROM Consumer;
SELECT * FROM Meter;
SELECT * FROM MeterReading;
SELECT * FROM TariffDetails;
SELECT * FROM Bill;
SELECT * FROM Arrears;




