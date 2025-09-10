use office

select* from employee
INSERT INTO employee values(17,121,'IT',900)
exec sp_help employee

/* ROW_NUM */
select id,name,branch,salary, row_number()over(order by salary desc) as row_num from employee

/* RANK and Dense_Rank */
select * ,row_number()over(order by salary) as popularity, rank()over(order by salary) as pop_rank,
dense_rank()over(order by salary) as pop_dense from employee

/* PARTITION BY */
select id,name,branch,salary, row_number()over(partition by branch order by salary) as row_num from employee

use Lakshay

select* from books
select* from orders
select* from users

select u.name,b.title,b.price, row_number()over(partition by u.user_id order by price desc),rank()over(partition by u.user_id 
order by price desc),dense_rank()over(partition by u.user_id order by price desc)
from (books b inner join orders o on b.product_id=o.product_id) inner join users u on u.user_id=o.user_id

select u.name, sum(b.price) as Book_sum,rank()over(partition by u.user_id 
order by price desc),dense_rank()over(partition by u.user_id order by price desc)
from (books b inner join orders o on b.product_id=o.product_id) inner join users u on u.user_id=o.user_id
group by u.name,sum(b.price)

