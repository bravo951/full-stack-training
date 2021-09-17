/*
1. In most cases, join has better performance than subqueries. However, correlated subquery is used, it may be better than join. We shoud check.
2. CTE stands for Common Table Expression. It is a temporary result set and typically it may be a result of complex sub-query.
   CTE can be used to a)create a recursive query;
					  b)substitute for a view when the general use of a view is not required;
					  c)improve readability and ease in the maintenance of complex queries.
3. Table variable is a special type of the local variable that helps to store data temporarily, similar to the temp table in SQL Server.
   Table variable acts like a variable and exists for a particular batch of query excution. It is created in Tempdb database.
4. DELETE is a Data Manipulation Language and TRUNCATE is a Data Definition Language
   TRUNCATE reseeds identity values, whereas DELETE doesn't.
   DELETE can specify certain rows that users want to delete, whereas TRUNCATE deletes all rows in a table
   DELETE command activates the trigger applied on the table and causes them to fire, whereas TRUNCATE doesn't
   DELETE eliminates rows one by one, whereas TRUNCATE deletes the entire data page
   Truncate is not possible when a table is referenced by a Foreign Key or tables are used in replication or with indexed views.
   Truncate is faster compared to delete as it makes less use of the transaction log.
5. Identity column is a column whose values are automatically generated when you add a new row to the table. TRUNCATE reseeds identity values, whereas DELETE doesn't.
6. DELETE is operated row-by-row, and records transaction log for each row. TRUNCATE is operated on the enrire data page, it only records transaction log for entire data page.
*/

use Northwind
go

-- Q1
select * from Employees
select * from Customers
/* join version */
select distinct c.City from Customers c inner join
Employees e
on c.City = e.City
/* subquery version */
select distinct City from Customers
where City in (select City from Employees)

-- Q2
/* a. use subquery */
select distinct City from Customers
where City not in (select City from Employees)
/* b. Do not use subquery */
select distinct c.City from Customers c left join
Employees e
on c.City = e.City
where e.City is null

-- Q3
select p.ProductName, count(d.OrderID) as OrderCount from Products p right join
[Order Details] d
on p.ProductID = d.ProductID
group by ProductName
select * from Products
select * from Orders
select * from [Order Details]

-- Q4
select City, count(OrderID) from
(select c.City,o.OrderID from Customers c left join
Orders o
on c.City = o.ShipCity) temp
group by City

select ShipCity from Orders where ShipCity not in (select City from Customers)

-- Q5
/* a. use union */
select City from Customers
group by City
having count(CustomerID) >= 2
union
select ShipCity from Orders
group by ShipCity
/* b. use sub-query and no union */
select distinct City from Customers
where City in (select ShipCity from Orders group by ShipCity having count(CustomerID) >= 2)

-- Q6
select o.ShipCity, count(d.ProductID) as ProductCnt from Orders o right join
[Order Details] d
on o.orderID = d.OrderID
group by ShipCity
having count(d.ProductID) >= 2

-- Q7
select * from Customers
select * from Orders
select * from Customers c
where c.City not in
(select o.ShipCity from Orders o inner join Customers c on o.ShipCity = c.City)

-- Q8
with cte1
as(
select top 5 * from
(select d.ProductID, sum(d.Quantity) as TotalQuantity, avg(d.UnitPrice) as AvgPrice from [Order Details] d
group by d.ProductID) temp
order by TotalQuantity desc
), cte2
as(
select d.ProductID,o.ShipCity, sum(d.Quantity) as #productPerCity, ROW_NUMBER() over(partition by d.ProductID order by sum(d.Quantity) desc) as rank from [Order Details] d left join
Orders o
on o.OrderID = d.OrderID
group by d.ProductID, o.ShipCity
)
select cte1.*, cte2.ShipCity as bestSellCity from cte1 join
cte2 on cte2.ProductID = cte1.ProductID and cte2.rank = 1

-- Q9
/* a. use subquery */
select City from Employees 
where City not in (select ShipCity from Orders)
/* b. do not use subquery */
select e.City from Employees e left join
Orders o on e.City = o.ShipCity
where ShipCity is null

-- Q10
select * from
(select EmployeeID, ShipCity,count(OrderID) cnt,ROW_NUMBER() over(partition by EmployeeID order by count(OrderID) desc) rank from Orders
group by EmployeeID, ShipCity) temp1 where rank =1 and ShipCity = 
(
select top 1 ShipCity from(
select o.ShipCity, sum(d.Quantity) as TotalQuatityPerCity from Orders o inner join
[Order Details] d
on o.OrderID = d.OrderID
group by ShipCity) temp
order by TotalQuatityPerCity desc
)

-- Q11
/* Union the table with itself*/

-- Q12
/* sample table is like below 
Employee:
empid	mgrid	deptid	salary
1		2		1		100
2		NULL	1		200
3		2		1		100
4		NULL	2		200
5		4		2		110
6		1		2		200
7		4		2		110
8		10		3		150
9		8		3		100
10		NULL	3		250
*/
select empid from Employee where empid!=mgrid

-- Q13
/*Dept:
deptid	deptname
1		test
2		tech
3		sales
*/
select e.deptname, temp.cnt from Employee e
(select deptid, count(empid) as cnt from Employees
group by deptid) temp
where temp.cnt = max(temp.cnt) and temp.deptid = e.deptid
order by e.deptname

-- Q14
select d.deptname, temp.empid, temp.salary from(
select *,ROW_NUMBER() over (partition by deptid order by salary desc) rank
from Employee e1) temp, Dept d
where d.deptid = temp.deptid and temp.rank <= 3
order by d.deptname, temp.salary desc
