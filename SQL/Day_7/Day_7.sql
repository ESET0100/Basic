use lakshay

select* from student
select* from students
select* from users

/* CREATE PROCEDURE */
create procedure retrieve_data
as
begin
select* from student
end

/* DROP PROCEDURE */
drop procedure retrieve_data

/* ANOTHER WAY TO CREATE PROCEDURE */
create procedure ret_data
@user_id int
as
begin
select* from users where user_id=@user_id;
end;

drop procedure ret_data

exec ret_data @user_id=6;

/* PROCEDURE WITH IF-ELSE */
create procedure CheckUserId
@user_id int /* extrenal variable that we provide value for */
as
begin
declare @name varchar(50); /* local variable where value is feeded from the table */

select @name=name from users where user_id=@user_id;
if @name='pratul'
	print('employee is pratul')
else
	print('employee is someone else')
end

exec CheckUserId @user_id=6

/* TRIGGER ON INSERT */  /* THEY WORK ONLY ON UPDATE, DELETE, INSERT */
create trigger Trig_user on users
after insert 
as
begin
	print 'A new record has been inserted';
end;

insert into users values(8,'shreya','shreya@gmail.com')

/* TRIGGER ON UPDATE */
create trigger Trig_users on users
after update 
as
begin
	print 'A new record has been updated';
end;

update users set name='rohan' where user_id=1;




