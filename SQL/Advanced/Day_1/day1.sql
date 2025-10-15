create database advanced;

use advanced

create table student(student_id int primary key, name varchar(50), marks1 float, marks2 float, marks3 float, average float, total float) 

insert into student values(101,'Lakshay',98,99,97,98,294)
insert into student values(102,'Shivansh',90,80,70,80,240)
insert into student values(103,'Piyush',80,80,60,73.3,220)
insert into student values(104,'Mohith',50,40,30,40,120)
insert into student values(105,'Shruti',60,70,80,70,210)

select* from student

alter table student add DOB date
update student set DOB='2003-02-13' where name='Lakshay'
update student set DOB='2002-11-06' where name='Shivansh'
update student set DOB='2003-06-23' where name='Piyush'
update student set DOB='2003-08-07' where name='Mohith'
update student set DOB='2003-12-13' where name='Shruti'

/* STUDENTS WITH MARKS>35 IN ALL SUBJECTS */
select* from student where (marks1>=35 and marks2>=35 and marks3>=35)

/* DOB BETWEEN CERTAIN DATES */
select* from student where dob between '2003-01-01' and '2003-12-31'

/* ANY ONE SUBJECTS MARKS < 35 */
select* from student where(marks1<35 or marks2<35 or marks3<35)

/* SEARCH FOR A PARTICULAR VALUE */
select* from student where name='Piyush'

/* SEARCH FOR VALUES */
select* from student where name='Shivansh' or name='Lakshay'

/* IN OPERATOR */
select* from student where name in('Shivansh','Lakshay')

/* FIND DOB WITH CERTAIN MONTHS */
select* from student where month(dob) in (2,3,4,5,6,7)

/* FIND DOB BETWEEN CERTAIN MONTHS */
select* from student where month(dob) between 2 and 8

/* DATEDIFF */
select DATEDIFF(DAY, '2003-02-01','2003-02-12') as Date_difference
select DATEDIFF(MONTH, '2003-02-01','2003-10-12') as Date_difference
select DATEDIFF(YEAR, '2007-02-01','2025-09-12') as Date_difference

/* DATEADD */
SELECT DATEADD(DAY, 7, GETDATE()) AS NextWeek;
SELECT DATEADD(MONTH, 2, GETDATE()) AS NextMonth;
SELECT DATEADD(YEAR, 3, GETDATE()) AS NextYear;

/* DATEPART */
SELECT DATEPART(year, GETDATE()) AS CurrentYear;
SELECT DATEPART(month, GETDATE()) AS CurrentMonth;
SELECT DATEPART(day, GETDATE()) AS CurrentDay;

/* ISDATE */
SELECT ISDATE('2025-10-07') AS IsValidDate;  -- Returns 1
SELECT ISDATE('Invalid Date') AS IsValidDate;  -- Returns 0

/* UPPER AND LOWER */
select upper(name) as UpperName from student
select lower(name) as LowerName from student

/* LENGTH */
select len(name) as Length from student;

/* CONCAT */
select concat(name,' has scored ',total,' marks') as concatted from student

/* SUBSTRING */
select substring(name,2,4) as Substr from student;

/* REPLACE */
select replace(name,'a','@') as New_name from student

/* PATTERN */
select* from student where name like '%sh'

/* LTRIM AND RTRIM */
SELECT RTRIM('   Hello, World!   ') AS TrimmedRight;  -- Returns '   Hello, World!'
SELECT LTRIM('   Hello, World!   ') AS TrimmedLeft;   -- Returns 'Hello, World!   '

/* FORMAT FUNCTION */
SELECT FORMAT(GETDATE(), 'yyyy-MM-dd') AS FormattedDate;  
SELECT FORMAT(GETDATE(), 'MMMM dd, yyyy') AS FormattedDate;  
SELECT FORMAT(GETDATE(), 'MM/dd/yyyy') AS FormattedDate;  

/* VALUES EXCLUDING DIGITS */
select name from student where total like '%[^0-9]%'

/* NUMERIC FUNCTIONS */
SELECT ABS(-10) AS AbsoluteValue;  -- Returns 10
SELECT ROUND(123.4567, 2) AS RoundedValue;  -- Returns 123.46
SELECT CEILING(10.1) AS CeilingValue;  -- Returns 11
SELECT FLOOR(10.9) AS FloorValue;  -- Returns 10
SELECT POWER(2, 3) AS PowerValue;  -- Returns 8 (2^3)
SELECT SQRT(16) AS SquareRoot;  -- Returns 4
SELECT CAST(123.45 AS INT) AS IntegerValue;  -- Returns 123
SELECT CONVERT(VARCHAR, 12345) AS StringValue;  -- Returns '12345'

/* CREATE ANOTHER TABLE AND INSERT DATA FROM STUDENT TABLE */
create table student_temp(student_id int primary key, name varchar(50), marks1 float, marks2 float, marks3 float, average float, total float)
alter table student_temp add DOB date
insert into student_temp select* from student where student_id!=105
select* from student_temp

/* Make a table from another table directly */
select* into student2 from student
select* from student2

/* ADDING RESULT COLUMN AND UPDATING IT */
alter table student add result varchar(20)
update student set result='Pass' where marks1>30 and marks2>30 and marks3>30
update student set result='Fail' where marks1<=30 or marks2<=30 or marks3<=30
select* from student

/* ORDER BY AND LIMIT */
select* from student order by name desc
select TOP(2)* from student order by name

/* GROUP BY */
select name,result from student group by result,name

/* CONSTRAINTS */
CREATE TABLE teacher (
    teacher_id INT CONSTRAINT uq_teacher_id UNIQUE,  
    name VARCHAR(30) NOT NULL,
    age INT,
    salary INT DEFAULT 10000,
    CONSTRAINT chk_age CHECK (age > 20 AND age < 40) 
);

insert into teacher values(1,'Shivam',28,12000)
insert into teacher(teacher_id,name,age) values(2,'Ankit',31)
select* from teacher

/* FOREIGN KEY */
create table courses(course_id varchar(20) primary key, course_name  varchar(30))
alter table teacher add course_id varchar(20) references courses(course_id)
insert into courses values('CSE2051','DSA')
insert into courses values('ECE3051','VLSI')
update teacher set course_id='CSE2051' where teacher_id=1
update teacher set course_id='ECE3051' where teacher_id=2
select* from teacher

-- INDEX 
CREATE INDEX idx_name ON student(name);
select* from student
EXEC sp_helpindex 'student'; -- Viewing the indexes on a table
ALTER INDEX idx_name ON student REBUILD;
DROP INDEX idx_name ON student;

/* To show stats of time taken to execute */
Set Statistics Time ON

/* SCALAR SUBQUERY -> Returns a single value */
SELECT name, (SELECT COUNT(*) FROM student) AS total_students FROM student;

--SINGLE ROW SUBQUERY
SELECT name FROM student WHERE student_id = (SELECT student_id FROM student WHERE result = 'fail');

--MULTIPLE ROW SUBQUERY
SELECT name FROM student WHERE student_id in (SELECT student_id FROM student WHERE result = 'pass');

--CORELATED SUBQUERY
SELECT name from student where marks1 < (select avg(marks1) from student)

select * from student where exists(select name from student)

select* from student
select* from teacher
select* from courses
insert into courses values('CSE2032','DBMS')

/* INNER JOIN */
select t.name,c.course_id from teacher t INNER JOIN courses c on t.course_id=c.course_id

/* LEFT JOIN */
select t.name,c.course_id from courses c LEFT JOIN teacher t on c.course_id=t.course_id

/* RIGHT JOIN */
select t.name,c.course_id from teacher t Right JOIN courses c on c.course_id=t.course_id

/* FULL OUTER JOIN */
select t.name,c.course_id from teacher t FULL OUTER JOIN courses c on c.course_id=t.course_id

/* PROCEDURE */ /***************************************************************************************************/
create procedure ret_data
@student_id int
as
begin
select* from student where student_id=@student_id;
end;

exec ret_data @student_id=101;

/* PROCEDURE WITH IF-ELSE */
create procedure CheckStudentId
@student_id int 
as
begin
declare @name varchar(50); /* local variable where value is feeded from the table */

select @name=name from student where student_id=@student_id;
if @name='Lakshay'
	print('employee is Lakshay')
else
	print('employee is someone else')
end

exec CheckStudentId @student_id=101


CREATE PROCEDURE InsertStudentsWithWhile
    @NumberOfStudents INT
AS
BEGIN
    DECLARE @i INT = 1;
    
    WHILE @i <= @NumberOfStudents
    BEGIN
        INSERT INTO student (student_id, name, marks1, marks2, marks3, total, average)
        VALUES (
            1000 + @i,
            'Student_' + CAST(@i AS VARCHAR(10)),
            ROUND(RAND() * 100, 2),
            ROUND(RAND() * 100, 2),
            ROUND(RAND() * 100, 2),
            0,
            0
        );
        
        SET @i = @i + 1;
    END
END

exec InsertStudentsWithWhile @NumberOfStudents=1

/* FUNCTIONS */  /******************************************************************************************************/
CREATE FUNCTION CalculateGrade(@average FLOAT)
RETURNS VARCHAR(2)
AS
BEGIN
    DECLARE @grade VARCHAR(2);
    
    IF @average >= 90 SET @grade = 'A+'
    ELSE IF @average >= 80 SET @grade = 'A'
    ELSE IF @average >= 70 SET @grade = 'B'
    ELSE IF @average >= 60 SET @grade = 'C'
    ELSE IF @average >= 50 SET @grade = 'D'
    ELSE SET @grade = 'F'
    
    RETURN @grade;
END

-- Using scalar functions in SELECT
SELECT 
    student_id,
    name,
    average,
    dbo.CalculateGrade(average) as grade
FROM student;

/* TVP */ /*****************************************************************************************************************************************/
-- Step 1: Create type and procedure
-- Check if type and procedure exist

-- Batch 1: Create type and procedure
CREATE TYPE StudentTableType AS TABLE
(
    student_id INT,
    name VARCHAR(50),
    marks1 FLOAT,
    marks2 FLOAT,
    marks3 FLOAT
);

CREATE PROCEDURE DisplayStudentTVP
    @StudentData StudentTableType READONLY
AS
BEGIN
    SELECT 
        student_id,
        name,
        marks1,
        marks2,
        marks3,
        marks1 + marks2 + marks3 as total_marks,
        ROUND((marks1 + marks2 + marks3) / 3.0, 2) as average_marks
    FROM @StudentData
    ORDER BY student_id;
END;

IF EXISTS (SELECT * FROM sys.types WHERE name = 'StudentTableType')
    AND EXISTS (SELECT * FROM sys.objects WHERE name = 'DisplayStudentTVP' AND type = 'P')
BEGIN
    DECLARE @StudentData StudentTableType;

    INSERT INTO @StudentData (student_id, name, marks1, marks2, marks3)
    VALUES 
    (1, 'John Doe', 85.5, 90.0, 78.5),
    (2, 'Jane Smith', 92.0, 88.5, 95.0),
    (3, 'Mike Johnson', 76.0, 82.5, 79.0);

    EXEC DisplayStudentTVP @StudentData;
END
ELSE
BEGIN
    PRINT 'Type or Procedure does not exist. Please create them first.';
END

/* JSON */ /********************************************************************************************************************************************/
-- Convert student table to JSON format
SELECT student_id, name, marks1, marks2, marks3 FROM student FOR JSON PATH

-- Convert JSON back to table format
DECLARE @json NVARCHAR(MAX) = '
[
    {"student_id":101, "name":"Alice", "marks1":85, "marks2":90, "marks3":78},
    {"student_id":102, "name":"Bob", "marks1":92, "marks2":88, "marks3":95}
]';

SELECT * FROM OPENJSON(@json)
WITH (
    student_id INT '$.student_id',
    name VARCHAR(50) '$.name',
    marks1 FLOAT '$.marks1',
    marks2 FLOAT '$.marks2',
    marks3 FLOAT '$.marks3'
);

-- Get just one value from JSON
DECLARE @studentData NVARCHAR(MAX) = '{"name":"John", "age":20, "grade":"A"}';
SELECT JSON_VALUE(@studentData, '$.name') as StudentName;  -- Returns: John

-- Get entire object or array
DECLARE @studentData NVARCHAR(MAX) = '{"name":"John", "subjects":["Math","Science"]}';
SELECT JSON_QUERY(@studentData, '$.subjects') as Subjects;  -- Returns: ["Math","Science"]

-- Change values in JSON
DECLARE @studentData NVARCHAR(MAX) = '{"name":"John", "marks":85}';
SET @studentData = JSON_MODIFY(@studentData, '$.marks', 90);
SELECT JSON_VALUE(@studentData, '$.marks') as Subjects;  

/* APPLY WITH JSON */  /************************************************************************************************************************/

-- Add a JSON column for student hobbies
ALTER TABLE student ADD hobbies_json NVARCHAR(MAX);

-- Update with sample JSON data
UPDATE student SET hobbies_json = '
[
    {"hobby": "Reading", "level": "Advanced", "years": 5},
    {"hobby": "Swimming", "level": "Intermediate", "years": 3}
]'
WHERE student_id = 101;

UPDATE student SET hobbies_json = '
[
    {"hobby": "Chess", "level": "Expert", "years": 8}
]'
WHERE student_id = 102;

UPDATE student SET hobbies_json = NULL WHERE student_id = 103; -- No hobbies

select* from student

-- Get each student with their hobbies (only students with hobbies)  [APPLY]
SELECT 
    s.student_id,
    s.name,
    h.hobby,
    h.level,
    h.years
FROM student s
CROSS APPLY OPENJSON(s.hobbies_json) 
WITH (
    hobby VARCHAR(50) '$.hobby',
    level VARCHAR(20) '$.level',
    years INT '$.years'
) AS h;

-- Get ALL students, even if they have no hobbies [OUTER APPLY]
SELECT 
    s.student_id,
    s.name,
    h.hobby,
    h.level,
    h.years
FROM student s
OUTER APPLY OPENJSON(s.hobbies_json) 
WITH (
    hobby VARCHAR(50) '$.hobby',
    level VARCHAR(20) '$.level',
    years INT '$.years'
) AS h;













