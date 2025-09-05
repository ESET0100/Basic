use office

create table employee(id int primary key, name varchar(50), branch varchar(50), salary float)

insert into employee values(1,'aakash','mech',300000)
insert into employee values(2,'Lakshay','IT',800000)
insert into employee values(3,'Piyush','IT',700000)
insert into employee values(4,'Vatti','AIML',300000)
insert into employee values(5,'Shivansh','SDE',500000)

select* from employee

select* from employee where id=1
select* from employee where id!=2

insert into employee(id,name,branch) values(6,'shruti','IT')
select* from employee where salary is NULL
select* from employee where salary is NOT NULL
select* from employee where name like 's%'
select* from employee where name like '%h'

select max(salary) from employee;
select* from employee where salary=(select max(salary) from employee)
select* from employee where salary in(select salary from employee where salary between 400000 and 900000)

select salary from employee order by salary desc top 1
