create database society_new

use society_new

create table apartment(Flat_no int, resident_name varchar(50), contact varchar(10))

insert into apartment values(101,'Lakshay','7905687703')
insert into apartment values(102,'Vatti','8966352413')
insert into apartment values(103,'Shivansh','9087647583')
insert into apartment values(104,'Piyush','7764890223')

select* from apartment

exec sp_help apartment

exec sp_rename 'apartment','apt'
select* from apt

exec sp_rename 'apt.flat_no','flat_number'
select* from apt

alter table apt alter column contact float
exec sp_help apt
select* from apt

alter table apt drop column contact
select* from apt

alter table apt add contact float
/*insert into apt(contact) values((777),(888),(999),(444))*/
select* from apt
