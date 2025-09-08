use lakshay;

select* from books
select* from orders
select* from users

/* INNER JOIN */
select* from (books b inner join orders o on b.product_id=o.product_id) inner join users u on u.user_id=o.user_id where b.price=1500

/* LEFT JOIN */
select* from books b left join orders o on b.product_id=o.product_id	

/* RIGHT JOIN */
select* from books b right join orders o on b.product_id=o.product_id

/* FULL OUTER JOIN */
select* from books b full outer join orders o on b.product_id=o.product_id

insert into users values(5,'manu','manu@gmail.com')
insert into users values(6,'pratul','pratul@gmail.com')
insert into users values(7,'shreyansh','shreyansh@gmail.com')

insert into books values(50,'manusmriti',50)
insert into books values(60,'Smoking 101',500)
insert into books values(70,'Quran',350)

insert into orders values(103,6,60)
insert into orders values(104,5,50)
insert into orders values(105,7,70)

/* Find all books, users, and orders */
select* from (books b full outer join orders o on b.product_id=o.product_id)
full outer join users u on u.user_id=o.user_id

/*Find all orders */
select* from orders

/*Find users with a particular book */
select b.price,u.name,b.title from (books b inner join orders o on b.product_id=o.product_id)
inner join users u on u.user_id=o.user_id where b.title='ramayan' or b.title='kalyug' or b.title='quran'

/*find all the orders of books that have been done */
select o.order_no,b.title,u.name from (books b inner join orders o on b.product_id=o.product_id)
inner join users u on u.user_id=o.user_id 

/* find total price of books purchased by each user */
select u.name,sum(b.price) as Total_Price from (books b inner join orders o on b.product_id=o.product_id)
inner join users u on u.user_id=o.user_id group by u.name





















