/*
1.	What is View? What are the benefits of using views?
	View is a virtual table whose contents are defined by a query.
	
2.	Can data be modified through views?
	No. Only select statement can be used inside a view. 
	After view is created, any operation on view are on base table

3.	What is stored procedure and what are the benefits of using it?
	- To prevent sql injection;
	- Stored procedures generally result in improved performance because the database can optimize the data access plan used by the procedure and cache it for subsequent reuse
	- Stored procedures help centralize your Transact-SQL code in the data tier. 
	- Stored procedures encourage code reusability. 

4.	What is the difference between view and stored procedure?
	Only select statement can be used in view. But other DML statements can be used in stored procedure.
	Operations on views are on base table. Operation on result set of a SP will not effect original table.

5.	What is the difference between stored procedure and functions?
	Store procedure is more about table manipulation. Functions are mostly used when we need to realized
	some scalar calculations. Functions ask for return values, stored procedure can have no return value.
	Stored procedures use out parameter to indicate return value, whereas functions don't need that.

6.	Can stored procedure return multiple result sets?
	Yes. By using multiple select statement inside the stored procedure.

7.	Can stored procedure be executed as part of SELECT Statement? Why?
	Yes. A stored procedure can be excuted implicitly from within a SELECT statement, 
	provided that the stored procedure returns a result set. Also, when SP returns a value, it can
	be used after Where clause.

8.	What is Trigger? What types of Triggers are there?
	Trigers are a special type of stored procedure that get excuted when specific event happens.
	2 types procedures are there. DML and DDL triggers.

9.	What are the scenarios to use Triggers?
	If we need some sql statements to be excuted automatically after a event(like insert, delete 
	or update), triggers can be used. 

10.	What is the difference between Trigger and Stored Procedure?
	Triggers cannot be explicitly excuted. Whereas Store procedures are called manually.
*/
use Mydatabase
go

/*alter proc sp1
as
begin
	select * from t1
	select * from t2
end

exec sp1
drop procedure sp1

create table temp(
	c1 int,
	c2 int,
	primary key(c1,c2)
)
drop table temp

create index index_fore on temp(c1,c2)
select * from temp
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

/* Q4 Create a view named “view_product_order_[your_last_name]”, list all 
products and total ordered quantity for that product.*/
create view view_product_order_Ma
as
	select p.ProductName, temp. OrderedQuantity from 
	(select od.ProductID, sum(od.Quantity) as OrderedQuantity from [Order Details] od
	group by od.ProductID) temp
	inner join Products p
	on p.ProductID = temp.ProductID
select * from view_product_order_Ma

/* Q5. Create a stored procedure “sp_product_order_quantity_[your_last_name]” that accept product id as an input 
	and total quantities of order as output parameter. @totalQ =*/
alter proc sp_product_order_quantity_Ma
@pid int, @totalQ int out
as
begin
	select @totalQ = OrderedQuantity from 
	(select od.ProductID, sum(od.Quantity) as OrderedQuantity from [Order Details] od
	group by od.ProductID) temp where ProductID = @pid
end
declare @Q int
exec sp_product_order_quantity_Ma 1, @Q out
print(@Q)

/* Q6. Create a stored procedure “sp_product_order_city_[your_last_name]” that accept product name as an input and top 5 
cities that ordered most that product combined with the total quantity of that product ordered from that city as output. */
create proc sp_product_order_city_Ma
@pname varchar(20)
as
begin
	select top 5 ShipCity, TotalQ from
	(select od.ProductID,o.ShipCity,count(o.OrderID) as orderCNT, sum(od.Quantity) TotalQ from Orders o 
	inner join [Order Details] od on o.OrderID = od.OrderID
	group by o.ShipCity,od.ProductID) temp
	where ProductID = (select ProductID from Products where ProductName = @pname)
	order by orderCNT desc
end
exec sp_product_order_city_Ma 'Chai'
select * from Region
-- Q7
create proc sp_move_employees_Ma
as
begin
	declare @temp table(id int)
	insert into @temp 
	select et.EmployeeID from EmployeeTerritories et 
	where et.TerritoryID = (select TerritoryID from Territories where TerritoryDescription = 'Tory')
	if not exists(select * from @temp)
		return
	else
		insert into Territories values(98105,'Stevens Point',3)
		update EmployeeTerritories
		set TerritoryID = 98105 where EmployeeID in (select * from @temp)
end

-- Q8
SELECT * FROM Territories
SELECT * FROM Employees

DROP TRIGGER IF TerritoryDescription IS NOT 'Stevens Point'
GO
CREATE TRIGGER Stevens_Point ON Territories INSTEAD OF INSERT
AS BEGIN
INSERT INTO Territories (Employees.City) VALUES ('Troy')
END

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
create sp_birthday_employees_Ma
as
begin
create table birthday_employees_Ma(L varchar(20),F varchar(20))
insert into birthday_employees_Ma
	select LastName, FirstName from Employees
	where MONTH(BirthDate) = 2
end
-- Q11
create proc sp_your_Ma_1
as
begin 
	with cte as
	(select count(od.ProductID) ProductCNT,c.CustomerID,c.City from Orders o 
	inner join [Order Details] od on o.OrderID = od.OrderID
	right join Customers c on c.CustomerID = o.CustomerID
	group by c.City,c.CustomerID
	having count(od.ProductID) <= 1)
	select COUNT(1) cnt,City from cte
	group by City having COUNT(1) >= 2
end

create proc sp_your_Ma_2
as
begin
	with cte as
	(select count(od.ProductID) ProductCNT,c.CustomerID,c.City from Orders o 
	inner join [Order Details] od on o.OrderID = od.OrderID
	right join Customers c on c.CustomerID = o.CustomerID
	group by c.City,c.CustomerID
	having count(od.ProductID) <= 1)
	select COUNT(1) cnt,City from cte
	group by City having COUNT(1) >= 2
end

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
