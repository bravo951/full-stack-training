/*
1. A result set is a set of rows from a database and metadata about the query such as the column names.
2. Union all keeps all of the records from each of the orginal datasets, while Union removes any duplicate records.
3. SQL also has operator like intersect and except.
4. Both Joins and Unions can be used to combine data from two or more tables.Joins combine data into new columns, while unions combine data into new rows.
5. Inner joins returns only the matching rows between two tables. Full join returns all rows from both the tables. 
6. Left joins return all records from the left table, and the matched records from the right table. Outer join return all records when there is a match in either left or right table.
7. Cross join produces a result set which is the number of rows in the first table multiplied by the number of rows in the second table. 
8. Where Clause is ued to filter the records from the table based on the specified condition. Having clause is used to filter record from the groups based on the specificed contion.
9. There can be multiple group by columns.
*/

use AdventureWorks2019
go

-- Q1. How many products can you find in the Production.Product table?
select count(1) as [product number] from Production.Product

-- Q2. 
select count(ProductSubcategoryID) as [product number] from Production.Product

-- Q3
select ProductSubcategoryID as ProductSubcategoryID, count(ProductSubcategoryID) as CountedProducts from Production.Product
group by ProductSubcategoryID
having ProductSubcategoryID is not null

-- Q4
select count(1) as [#products w/o SubCategory] from Production.Product
where ProductSubcategoryID is null

-- Q5
select ProductID,sum(Quantity) as TheSum from Production.ProductInventory
group by ProductID

-- Q6
select ProductID,sum(Quantity) as TheSum from Production.ProductInventory
where LocationID = 40
group by ProductID
having sum(Quantity) < 100

-- Q7
select * from Production.ProductInventory
select Shelf, ProductID, sum(Quantity) as TheSum from Production.ProductInventory
where LocationID = 40
group by ProductID,Shelf
having sum(Quantity) < 100

-- Q8
select avg(Quantity) as TheAvg from Production.ProductInventory
where LocationID = 10

-- Q9
select ProductID, Shelf, avg(Quantity) as TheAvg from Production.ProductInventory
group by ProductID, Shelf

-- Q10
select ProductID,Shelf,avg(Quantity) as TheAvg from Production.ProductInventory
where Shelf != 'N/A'
group by ProductID, Shelf

-- Q11
select Color,Class, count(ProductID) as TheCount, avg(ListPrice) as AvgPrice from Production.Product
where Color is not null and Class is not null
group by Color, Class

-- Q12
select * from Person.CountryRegion
select * from Person.StateProvince
select c.Name as Country, p.Name as Province from Person.CountryRegion c join
Person.StateProvince p
on c.CountryRegionCode = p.CountryRegionCode

-- Q13
select c.Name as Country, p.Name as Province from Person.CountryRegion c inner join
Person.StateProvince p
on c.CountryRegionCode = p.CountryRegionCode and c.Name in ('Germany','Canada')


use Northwind
go
-- Q14
select distinct d.ProductID from Orders o right join
[Order Details] d
on d.OrderID = o.OrderID

-- Q15
select top 5 temp.ShipPostalCode from
(select d.OrderID, d.Quantity, o.ShipPostalCode from [Order Details] d left join
Orders o
on o.OrderID = d.OrderID) temp
group by temp.ShipPostalCode
having temp.ShipPostalCode is not null
order by sum(temp.Quantity) desc

-- Q16
/*There is no record in last 20 years. I set 1998 as timeline here*/
select top 5 temp.ShipPostalCode from
(select d.OrderID, d.Quantity, o.ShipPostalCode from [Order Details] d left join
Orders o
on o.OrderID = d.OrderID
where year(o.OrderDate) >= 1998) temp
group by temp.ShipPostalCode
having temp.ShipPostalCode is not null
order by sum(temp.Quantity) desc

-- Q17
select count(customerID) as Count_Customers,City from customers
group by City
/*If join operation is required, please see follow*/
select o1.ShipCity, count(o2.CustomerID) as TheCount from Orders o1 left join
Orders o2
on o1.ShipCity=o2.ShipCity
group by o1.ShipCity, o2.CustomerID

-- Q18
select o1.ShipCity, count(o2.CustomerID) as TheCount from Orders o1 left join
Orders o2
on o1.ShipCity=o2.ShipCity
group by o1.ShipCity, o2.CustomerID
having count(o2.CustomerID) > 10

-- Q19
select c.ContactName, o.OrderDate from Customers c left join
Orders o
on c.CustomerID = o.CustomerID
where o.OrderDate > '1998-01-01'

-- Q20
select c.ContactName, max(o.OrderDate) as [MostRecentDate] from Customers c left join
Orders o
on c.CustomerID = o.CustomerID
group by c.ContactName

-- Q21
select c.ContactName, sum(d.Quantity) as TheSum from [Order Details] d left join
Orders o
on o.OrderID=d.OrderID
left join Customers c
on c.CustomerID = o.CustomerID
group by c.ContactName

-- Q22
select o.CustomerID from [Order Details] d left join
Orders o
on o.OrderID=d.OrderID
group by o.CustomerID
having sum(d.Quantity) > 100

-- Q23
select s.CompanyName as [Supplier Company Name], p.CompanyName as [Shipping Company Name]
from Shippers s
inner join Suppliers p
on s.Phone = p.Phone;

-- Q24
select o.OrderDate, p.ProductName from Orders o right join
[Order Details] d
on o.OrderID = d.OrderID
left join Products p
on p.ProductID = d.ProductID
order by o.OrderDate

-- Q25
select e1.EmployeeID, e2.EmployeeID from Employees e1 inner join
Employees e2
on e1.Title = e2.Title and e1.EmployeeID < e2.EmployeeID

-- Q26
select e1.EmployeeID from Employees e1 inner join
Employees e2
on e1.EmployeeID = e2.ReportsTo
group by e1.EmployeeID
having count(e2.EmployeeID) > 2

-- Q27
select c.City, c.CompanyName, c.ContactName, 'Customer' as Type
from Customers c
union
select s.City, s.CompanyName, s.ContactName, 'Supplier' as Type
from Suppliers s

Use Mydatabase
go
-- Q28
select a.ID,b.ID from T1 A inner join
T2 B
on a.ID = b.ID
/*result:
	ID	ID
1	2	2
2	3	3
*/

-- Q29
select a.ID,b.ID from T1 A left join
T2 B
on a.ID = b.ID
/*result:
	ID	ID
1	1	NULL
2	2	2
3	3	3
*/










