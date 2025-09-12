use esyasoft


CREATE TABLE Students2024 (ID INT, Name VARCHAR(50));
CREATE TABLE Students2025 (ID INT, Name VARCHAR(50));

INSERT INTO Students2024 VALUES
(1,'Ravi'),(2,'Asha'),(3,'John'),(4,'Meera'),(5,'Kiran'),
(6,'Divya'),(7,'Lokesh'),(8,'Anita'),(9,'Rahul'),(10,'Sneha');

INSERT INTO Students2025 VALUES
(2,'Asha'),(4,'Meera'),(5,'Kiran'),(11,'Prakash'),(12,'Vidya'),
(13,'Neha'),(14,'Manoj'),(15,'Ramesh'),(16,'Lata'),(17,'Akash');

CREATE TABLE Employees (EmpID INT, Name VARCHAR(50), Department VARCHAR(20));

INSERT INTO Employees VALUES 
(1,'Ananya','HR'),(2,'Ravi','IT'),(3,'Meera','Finance'),
(4,'John','IT'),(5,'Divya','Marketing'),(6,'Sneha','Finance'),
(7,'Lokesh','HR'),(8,'Asha','IT'),(9,'Kiran','Finance'),(10,'Rahul','Sales');


CREATE TABLE Projects (ProjectID INT, Name VARCHAR(50), StartDate DATE, EndDate DATE);
INSERT INTO Projects VALUES 
(1,'Bank App','2025-01-01','2025-03-15'),
(2,'E-Commerce','2025-02-10','2025-05-20');


CREATE TABLE Contacts (ID INT, Name VARCHAR(50), Email VARCHAR(50), Phone VARCHAR(20));
INSERT INTO Contacts VALUES
(1,'Ravi','ravi@gmail.com',NULL),
(2,'Asha',NULL,'9876543210'),
(3,'John',NULL,NULL);

select* from Students2024
select* from Students2025
select* from Employees
select* from Projects
select* from Contacts

/* Task 1- UNION and UNION ALL*/
SELECT Name FROM Students2024 UNION
SELECT Name FROM Students2025 ORDER BY Name

SELECT Name FROM Students2024 UNION ALL
SELECT Name FROM Students2025 ORDER BY Name

/*Task 2- UPPER, LOWER, CONCAT, LEN, SUBSTRING*/
SELECT
    Name,
    UPPER(Name) AS UppercaseName,
    LOWER(Name) AS LowercaseName
FROM Employees;

SELECT Name, LEN(Name) AS NameLength from Employees

select Name, SUBSTRING(name,1,3) as Substring_name from employees

SELECT EmpID,Name,Department,REPLACE(Department, 'Finance', 'Accounts') AS UpdatedDepartment FROM Employees;

SELECT EmpID,CONCAT(Name, ' - ', Department) AS NameAndDept from employees

/* Task 3 - DATE FUNCTIONS*/
SELECT GETDATE() AS TodayDate;
 
 
SELECT
    ProjectID,
    Name,
    StartDate,
    EndDate,
    DATEDIFF(DAY, StartDate, EndDate) AS DurationInDays
FROM Projects;
 
 
 
SELECT
    ProjectID,
    Name,
    EndDate,
    DATEADD(DAY, 10, EndDate) AS NewEndDate
FROM Projects;
 
 
SELECT
    ProjectID,
    Name,
    EndDate,
    DATEDIFF(DAY, GETDATE(), EndDate) AS DaysLeft
FROM Projects;

/* Task 4 */

SELECT CONVERT(VARCHAR, GETDATE(), 103) AS FormattedDate;
 
 
SELECT CAST(123.456 AS INT) AS IntegerValue;
