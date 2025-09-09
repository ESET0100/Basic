create database esyasoft

use esyasoft

create table customers(customer_id int unique, customer_name varchar(50), address varchar(100), region varchar(25))
exec sp_help customers

create table SmartMeterReadings(Meter_id int primary key, customer_id int, location varchar(25),
                                installed_date date, Reading_date_time time, energy_consumed float,
								Foreign key(customer_id) references customers(customer_id))
exec sp_help SmartMeterReadings

alter table SmartMeterReadings alter column Reading_date_time datetime

insert into customers values(10,'Lakshay','New Delhi','South West')
insert into customers values(20,'Shivansh','Jodhpur','West')
insert into customers values(30,'Piyush','Bokaro','South')
insert into customers values(40,'Mohith','Bangalore','East')
select* from customers

alter table 

insert into SmartMeterReadings values(01,10,'New Delhi','2025-02-13','2025-09-09 12:30:30',20.50)
update SmartMeterReadings set location='rooftop' where location='New Delhi'
insert into SmartMeterReadings values(02,20,'Backyard','2024-05-18','2025-01-02 08:30:30',40)
insert into SmartMeterReadings values(03,30,'Main gate','2024-12-25','2025-06-05 22:45:30',80)
insert into SmartMeterReadings values(04,40,'Lobby','2025-03-21','2025-04-10 17:30:00',8.50)
insert into SmartMeterReadings values(05,10,'Lobby','2025-03-21','2025-04-10 17:30:00',17.50)
insert into SmartMeterReadings values(06,20,'Lobby','2025-03-21','2025-04-10 17:30:00',18.50)

select* from customers
select* from SmartMeterReadings

/* TASK 1 */
/* Fetching records with energy consumed between 10kWh and 50 kWh */
select* from SmartMeterReadings where energy_consumed between 10 and 50

/* Reading date time is between given 2 dates */
select* from SmartMeterReadings where Reading_date_time between '2024-01-01' and '2024-12-31'

select* from SmartMeterReadings

/*Exclude meters installed after the given date */
select Meter_id, installed_date, Reading_date_time, energy_consumed from SmartMeterReadings where installed_date<='2024-06-30'

/* TASK 2*/

/*Calculate avg energy consumed by each customer  */
select customer_id,avg(energy_consumed) from SmartMeterReadings group by customer_id

/*Find max energy of each customer */
select customer_id,max(energy_consumed) as Max_energy_consumed from SmartMeterReadings group by customer_id

/* Include readings only from current year */
select * from SmartMeterReadings where year(Reading_date_time)=year(getdate())


