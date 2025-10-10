create database hotel

use hotel

-- 1. Guests Table
CREATE TABLE Guests (
    GuestID INT PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Email VARCHAR(100),
    Phone VARCHAR(20),
    Addr VARCHAR(255)
);

-- 2. RoomTypes Table
CREATE TABLE RoomTypes (
    RoomTypeID INT PRIMARY KEY,
    TypeName VARCHAR(50) NOT NULL,
    Capacity INT,
    BasePrice DECIMAL(10,2)
);

-- 3. Rooms Table
CREATE TABLE Rooms (
    RoomID INT PRIMARY KEY,
    RoomNumber VARCHAR(10) NOT NULL,
    RoomTypeID INT FOREIGN KEY REFERENCES RoomTypes(RoomTypeID),
    Floor INT,
    Status VARCHAR(20) DEFAULT 'Available',
    PricePerNight DECIMAL(10,2)
);

-- 4. Bookings Table
CREATE TABLE Bookings (
    BookingID INT PRIMARY KEY,
    GuestID INT FOREIGN KEY REFERENCES Guests(GuestID),
    RoomID INT FOREIGN KEY REFERENCES Rooms(RoomID),
    CheckInDate DATE NOT NULL,
    CheckOutDate DATE NOT NULL,
    TotalAmount DECIMAL(10,2),
    Status VARCHAR(20) DEFAULT 'Confirmed'
);

-- 5. Payments Table
CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY,
    BookingID INT FOREIGN KEY REFERENCES Bookings(BookingID),
    Amount DECIMAL(10,2),
    PaymentMethod VARCHAR(20),
    PaymentDate DATE,
    Status VARCHAR(20)
);

Create table Login(username varchar(50) primary key, staff_name varchar(25), passwd varchar(30))

INSERT INTO Guests (GuestID, FirstName, LastName, Email, Phone, Addr) VALUES
(1, 'Raj', 'Sharma', 'raj.sharma@email.com', '9876543210', '123 MG Road, Mumbai, Maharashtra'),
(2, 'Priya', 'Patel', 'priya.patel@email.com', '9876543211', '456 Brigade Road, Bangalore, Karnataka'),
(3, 'Amit', 'Kumar', NULL, '9876543212', '789 Connaught Place, Delhi, Delhi'),
(4, 'Anjali', 'Singh', 'anjali.singh@email.com', '9876543213', '321 Park Street, Kolkata, West Bengal'),
(5, 'Sanjay', 'Gupta', 'sanjay.gupta@email.com', '9876543214', '654 FC Road, Pune, Maharashtra'),
(6, 'Neha', 'Reddy', 'neha.reddy@email.com', '9876543215', '987 Banjara Hills, Hyderabad, Telangana'),
(7, 'Vikram', 'Malhotra', 'vikram.m@email.com', '9876543216', '159 Sabarmati, Ahmedabad, Gujarat'),
(8, 'Deepika', 'Iyer', 'deepika.iyer@email.com', '9876543217', '753 Marine Drive, Chennai, Tamil Nadu');

/* CHECK CONSTRAINTS ADDED TO GUEST TABLE */
alter table guests add constraint Chk_num check(len(Phone)=10)
alter table guests add constraint chk_email check(email like '%@email.com' or email like'%@gmail.com')

/* INSERTING VALUES INTO ALL TABLES */

INSERT INTO RoomTypes (RoomTypeID, TypeName, Capacity, BasePrice) VALUES
(1, 'Standard Single', 1, 1500.00),
(2, 'Standard Double', 2, 2500.00),
(3, 'Deluxe Room', 3, 4000.00),
(4, 'Executive Suite', 2, 6000.00),
(5, 'Family Suite', 4, 8000.00),
(6, 'Presidential Suite', 3, 12000.00),
(7, 'Budget Room', 1, 1000.00),
(8, 'Luxury Villa', 6, 15000.00);

INSERT INTO Rooms (RoomID, RoomNumber, RoomTypeID, Floor, Status, PricePerNight) VALUES
(1, '101', 1, 1, 'Available', 1500.00),     
(2, '102', 1, 1, 'Occupied', 1500.00),      
(3, '201', 2, 2, 'Available', 2500.00),      
(4, '202', 2, 2, 'Maintenance', 2500.00),    
(5, '301', 3, 3, 'Occupied', 4000.00),       
(6, '302', 3, 3, 'Maintenance', 4000.00),    
(7, '401', 4, 4, 'Available', 6000.00),      
(8, '501', 5, 5, 'Occupied', 8000.00);       

INSERT INTO Bookings (BookingID, GuestID, RoomID, CheckInDate, CheckOutDate, TotalAmount, Status) VALUES
(1, 1, 2, '2024-01-15', '2024-01-18', 4500.00, 'Confirmed'),      
(2, 2, 8, '2024-01-16', '2024-01-20', 32000.00, 'Checked-In'),    
(3, 3, 4, '2024-01-17', '2024-01-19', 5000.00, 'Cancelled'),      
(4, 4, 5, '2024-01-18', '2024-01-22', 16000.00, 'Confirmed'),     
(5, 5, 1, '2024-01-19', '2024-01-21', 3000.00, 'Confirmed'),      
(6, 6, 3, '2024-01-20', '2024-01-25', 12500.00, 'Confirmed'),     
(7, 7, 7, '2024-01-21', '2024-01-23', 12000.00, 'No-Show'),       
(8, 8, 6, '2024-01-22', '2024-01-24', 8000.00, 'Checked-Out');    

INSERT INTO Payments (PaymentID, BookingID, Amount, PaymentMethod, PaymentDate, Status) VALUES
(1, 1, 4500.00, 'Credit Card', '2024-01-14', 'Completed'),    
(2, 2, 16000.00, 'Debit Card', '2024-01-15', 'Completed'),    
(3, 3, 0.00, 'Cash', '2024-01-16', 'Refunded'),               
(4, 4, 8000.00, 'Online Transfer', '2024-01-17', 'Completed'),
(5, 5, 3000.00, 'Cash', '2024-01-18', 'Completed'),          
(6, 6, 6250.00, 'Credit Card', '2024-01-19', 'Pending'),    
(7, 7, 0.00, 'Debit Card', '2024-01-20', 'Failed'),           
(8, 8, 8000.00, 'Online Transfer', '2024-01-21', 'Completed');

INSERT INTO Login (username, staff_name, passwd) VALUES 
('arjun.verma', 'Arjun Verma', 'Arjun@2024'),
('sneha.reddy', 'Sneha Reddy', 'Sneha#5678'),
('rahul.joshi', 'Rahul Joshi', 'Rahul$Secure1'),
('pooja.mishra', 'Pooja Mishra', 'Pooja!Pass123'),
('deepak.iyer', 'Deepak Iyer', 'Deepak*9876'),
('kavita.choudhary', 'Kavita Choudhary', 'Kavita@2024');

select* from Login
select* from Guests
select* from RoomTypes
select* from Rooms
select* from Bookings
select* from Payments


