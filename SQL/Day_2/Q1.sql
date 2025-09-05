use office;

create table student(id int primary key, name varchar(50), contact float unique)

alter table student add subject_code int

insert into student values(1,'Lakshay',7905687703)
insert into student values(2,'Piyush',8765443234)
insert into student values(3,'Vatti',7657895501)
insert into student values(4,'Shivansh',6789453321)
insert into student(id,name) values(5,'shivam')

update student set subject_code=101 where id=1
update student set subject_code=102 where id=2
update student set subject_code=103 where id=3
update student set subject_code=102 where id=4
update student set subject_code=101 where id=5


select* from student

create table teachers(name varchar(50), code int, id int, Foreign key(id) references student(id))

insert into teachers values('Mohit',101,1)
insert into teachers values('Rohan',102,2)
insert into teachers values('Rohit',103,3)

select* from teachers

