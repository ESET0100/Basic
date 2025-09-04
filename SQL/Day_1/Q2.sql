create database office
use office

create table employee(emp_id int, emp_name varchar(50), department varchar(50), salary float)

insert into employee values(1,'Sarath','HR',200000)
insert into employee values(2,'Aakash','IT',100000)
insert into employee values(3,'Abhishek','Senior HR',250000)
insert into employee values(4,'Kunal','Marketing',400000)
insert into employee values(5,'Sowmya','IT',100000)

select* from employee

update employee set salary=salary+salary*0.40 where emp_name='sowmya'
select* from employee

delete from employee where department='senior hr'
select* from employee

exec sp_rename 'employee','friends'
select* from friends

alter table friends add address varchar(50)
select* from friends

alter table friends alter column salary varchar(50)
alter table friends alter column contact float
exec sp_help friends
select* from friends

update friends set contact=7905687703 where emp_name='sarath'
select* from friends

alter table friends drop column address
select* from friends

exec sp_rename 'friends.contact', 'phone_number'
select* from friends