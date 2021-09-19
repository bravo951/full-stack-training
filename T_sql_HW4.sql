/*


*/



use Northwind
go
select * from Region
select * from EmployeeTerritories
select * from Employees
select * from Territories
-- Q1
/* a. A new region called “Middle Earth” */
insert into Region values(5,'Middle Earth')
exec sp_help name
/* b. A new territory called “Gondor”, belongs to region “Middle Earth” */
insert into Territories(TerritoryID, TerritoryDescription, RegionID) values(98220,'Gondor',5)
/* c. A new employee “Aragorn King” who's territory is “Gondor” */
-- DBCC CHECKIDENT (Employees, RESEED, 9)
insert into Employees(LastName,FirstName,PostalCode) values('King','Aragorn',98220)
-- delete from Employees where EmployeeID = 11
insert into EmployeeTerritories values(10,98220)
-- delete from EmployeeTerritories where EmployeeID = 10

-- Q2
update Territories
set TerritoryDescription = 'Arnor'
where RegionID = 5

-- Q3
delete from EmployeeTerritories where EmployeeID = 10
delete from Territories where RegionID = 5
delete from Region where RegionID = 5
delete from Employees where EmployeeID = 10
DBCC CHECKIDENT (Employees, RESEED, 9)

-- Q4
-- Q5 
-- Q6
-- Q7
-- Q8

use Mydatabase
go
-- Q9
create table people_Ma(
	id int PRIMARY KEY IDENTITY,
	Name varchar(20),
	City int
)
insert into people_Ma values('Aaron Rodgers',2),('Russell Wilson',1),('Jody Nelson',2)
select * from city_Ma

create table city_Ma(
	id int PRIMARY KEY,
	City varchar(20) 
)
insert into city_Ma values(1,'Seatle'),(2,'Green Bay')

set XACT_ABORT ON
begin tran
delete from city_Ma where City = 'Seatle'
insert into city_Ma values(3,'Madison')
update people_Ma
set City = 3 where City = 1
go
create view Packers_Ma
as
	select Name from people_Ma p inner join
	city_Ma c on p.City = c.City and c.City = 'Madison'
go
print 'err'+cast(@@ERROR as nvarchar(10))
if(@@ERROR <>0)
rollback tran
else
commit tran
go

drop table city_Ma
drop table people_Ma
drop view Packers_Ma

-- Q10
-- Q11
-- Q12
/*
drop table t1
create table t1(id int)
insert into t1 values(1),(1)
drop table t2
create table t2 (id int)
insert into t2 values(1),(2)
select * from t1 except (select * from t2)*/
/* If A and B have no duplicate rows:
	Record numbers of rows from 2 tables A and B. Union A with B and see whether the new table has more number of rows.
*/

-- Q14
create table name(
	[First Name] varchar(20),
	[Last Name] varchar(20),
	[Middle Name] varchar(20)
)
insert into name([First Name],[Last Name]) values('John','Green')
insert into name values('Mike','White','M')
select * from name
--Select Name FROM SysObjects Where XType='U' orDER BY NameISNULL([Middle Name],''), if(ISNULL([Middle Name],'') != '','. ','')
select concat([First Name],' ', 
			  [Middle Name],
			  case when ISNULL([Middle Name],'') != '' 
				   then '. ' 
				   end, 
			  [Last Name]) as [Full Name] 
from name


-- Q15
create table Student(
	Student varchar(5),
	Marks int,
	Sex char
)

insert into Student values('Ci',70,'F'),('Bob',80,'M'),('Li',90,'F'),('Mi',95,'M')

select top 1 Marks from Student
where Sex = 'F'
order by Marks desc

-- Q16
select * from Student
order by Sex,Marks desc

go
create view tempview
as
	select * from Student where Sex = 'F'
