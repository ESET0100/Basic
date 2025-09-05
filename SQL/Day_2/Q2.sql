use Lakshay

create table users(user_id int primary key, name varchar(50),email varchar(50))
create table books(product_id int primary key, title varchar(50), price int)

insert into users values(1,'Ramu','Ramu@gmail.com')
insert into users values(2,'Gopal','Gopal@gmail.com')
insert into users values(3,'Ganesh','Ganesh@gmail.com')
insert into users values(4,'Jay','Jay@gmail.com')

insert into books values(10,'Ramayan',1500)
insert into books values(20,'Tintin',300)
insert into books values(30,'2 States',500)
insert into books values(40,'Kalyug',400)

create table orders(order_no int primary key, user_id int, product_id int,
					foreign key(user_id) references users(user_id),
					foreign key(product_id) references books(product_id))

select* from users;
select* from books;

insert into orders values(101,1,10)
insert into orders values(102,1,40)


select* from orders



