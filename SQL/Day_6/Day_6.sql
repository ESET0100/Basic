use Lakshay

select* from books
select* from users

/* UNION */
select * from books where price<400 union select* from books where price<800

/* UNION ALL */
select * from books where price<400 union all select* from books where price<800

/* UPPER */
select UPPER(title) as UPPER_TITLE from books

/* LOWER */
select LOWER(title) as LOWER_TITLE from books

/* LENGTH */
select len('hello') as LENGTH
select title,len(title) as LENGTH from books

/* SUBSTRING */
select SUBSTRING('Lakshay',3,2) as substr 
select SUBSTRING(title,3,5) as Substr from books

/* REVERSE */
select reverse(title) as Reverse_Title from books
select reverse('Lakshay') as Reverse_name

/* REPLACE */
select REPLACE('Lakshay likes tea','tea','çoffee') as coffee
select REPLACE(title,'a','A') as New_title from books

/* CONCAT */
Select CONCAT('Hi',' how are you') as greeting
select CONCAT(title,price) as Title_price from books

/* CAST */
Select cast('5000' as int) as New_type
select cast(price as float) as NEW_TYPE from books

/* CONVERT */
select convert(varchar(10),GETDATE(),103) as Date_ddmmyyyy4

/* CASE */
