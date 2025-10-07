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









