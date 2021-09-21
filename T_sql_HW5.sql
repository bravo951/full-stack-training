/*
1.	What is an object in SQL?
	 An object is any SQL Server resource, such as a SQL Server lock or Windows process. 
	 Each object contains one or more counters that determine various aspects of the objects to monitor. 
	 For example, the SQL Server Locks object contains counters called Number of Deadlocks/sec and Lock Timeouts/sec.

2.	What is Index? What are the advantages and disadvantages of using Indexes?
	Indexes are database objects based on table column for faster retrieval of data. 
	pros:
	- fast retrival of data
	cons:
	- Additional disc space
	- slower speed of Insert, Update and delete statements

3.	What are the types of Indexes?
	2 types. One is clustered. The other is non-clustered.

4.	Does SQL Server automatically create indexes when a table is created? If yes, under which constraints?
	Yes. When a table is created, a primary key is also created and a cluster index is put on this primary key by default.

5.	Can a table have multiple clustered index? Why?
	No. Clustered index is set on the primary key by default. Even it is changed to another column, it still has 
	to be unique. Only one cluster index is allowed for a table.

6.	Can an index be created on multiple columns? Is yes, is the order of columns matter?
	Yes. An index can be created on a combination of multiple columns, and after creating, rows are automatically sorted.

7.	Can indexes be created on views?
	Yes.Creating a unique clustered index on a view improves query performance because the view is stored in the database
	in the same way a table with a clustered index is stored. 
	
8.	What is normalization? What are the steps (normal forms) to achieve normalization?
	Database Normalization is a process of organizing data to minimize redundancy (data duplication), which in turn 
	ensures data consistency.
	1NF:
	- Data in each column should be atomic, no multiples values separated by comma.
	- The table does not contain any repeating column group
	- Identify each record using primary key.
	2NF:
	- The table must meet all the conditions of 1NF
	- Move redundant data to separate table
	- Create relationships between these tables using foreign keys
	3NF:
	- Table must meet all the conditions of 2NF.
	- Does not contain columns that are not fully dependent on primary key.

9.	What is denormalization and under which scenarios can it be preferable?
	Denormalization is a strategy used on a normalized database to increase read performance.
	It generates redundant data or groups data and is preferable when a large numbers of 
	read operations is needed in database software
	
10.	How do you achieve Data Integrity in SQL Server?
	Data integrity can be achieved by constraints and triggers
	Constraints are divided into 3 catagories:
	- Domain integrity
	- Entity integrity
	- Referential integrity
	
11.	What are the different kinds of constraint do SQL Server have?
	- NOT NULL 
	- Unique
    - PRIMARY KEY 
	- Foriegn Key
	- Check Constraints

12.	What is the difference between Primary Key and Unique Key?
	Unique + NOT NULL = Primary Key. That is to say, primary key doesn't allow NULL value.
	Unique key can allow one NULL value.
	
13.	What is foreign key?
	A Foreign Key is a constraint that is used to link two tables together.
	The FOREIGN KEY constraint identifies the relationships between the database tables by 
	referencing a column, or set of columns.
	
14.	Can a table have multiple foreign keys?
	Yes.

15.	Does a foreign key have to be unique? Can it be null?
	A foreign key doesn't need to be unique and it can be null.

16.	Can we create indexes on Table Variables or Temporary Tables?
	Can create indexes on Temporary Tables but not on Table Variables.

17.	What is Transaction? What types of transaction levels are there in SQL Server?
	Transaction is a logical unit of work is a SQL operation or a set of SQL statements executed against a database. 
	It usually includes at least one statement and changes the database from one consistent state to another
	Isolation levels for transactions:
	-   Read Uncommitted (Lowest level)
	-	Read Committed
	-	Repeatable Read
	-	Serializable (Highest Level)
	-	Snapshot Isolation
*/
use Mydatabase
go

/* Q1. Write an sql statement that will display the name of each customer 
and the sum of order totals placed by that customer during the year 2002 
Create table customer(cust_id int,  iname varchar (50))
create table [order](order_id int,cust_id int,amount money,order_date smalldatetime)*/

select c.iname, sum(o.amount) as [OrderTotals2002] from customer c right join
[order] o
on c.cust_id = o.cust_id
where YEAR(o.order_date) = 2002
group by c.iname

/* Q2. The following table is used to store information about company’s personnel:
Create table person (id int, firstname varchar(100), lastname varchar(100)) write a query that returns 
all employees whose last names  start with “A”.*/
insert into person
select top 200 BusinessEntityID, FirstName,LastName from AdventureWorks2019.Person.Person
select * from person
where LastName like 'A%'

/* Q3. The information about company’s personnel is stored in the following table:
Create table person(person_id int primary key, manager_id int null, name varchar(100)not null) 
The filed managed_id contains the person_id of the employee’s manager.
Please write a query that would return the names of all top managers(an employee who does not have a manger, 
and the number of people that report directly to this manager. */
insert into person
select top 8 EmployeeID,ReportsTo, FirstName+LastName from Northwind.dbo.Employees
update person
set manager_id = null where person_id = 5
/*select * from person
with cte_recursive
as
(
	select person_id, manager_id, name, 1 as lvl from person where manager_id is null
	union all
	select p.person_id, p.manager_id, p.name, c.lvl+1 from person p
	inner join cte_recursive c
	on c.person_id = p.manager_id
)
select * from cte_recursive*/

select pp.person_id,pp.name, 
(select count(1) from person p where pp.person_id = p.manager_id) as pplUnderMg
from person pp where manager_id is null

-- Q4. List all events that can cause a trigger to be executed.


/* Q5. Generate a destination schema in 3rd Normal Form.  Include all necessary fact, join, and dictionary 
tables, and all Primary and Foreign Key relationships.  The following assumptions can be made:
a. Each Company can have one or more Divisions.
b. Each record in the Company table represents a unique combination 
c. Physical locations are associated with Divisions.
d. Some Company Divisions are collocated at the same physical of Company Name and Division Name.
e. Contacts can be associated with one or more divisions and the address, but are differentiated by 
suite/mail drop records.status of each association should be separately maintained and audited.*/

create table locations
(
	id int PRIMARY KEY,
	address text,
)
create table Divisions
(
	id int PRIMARY KEY,
	name varchar(20),
)
create table Company
(
	id int,
	name varchar(20),
	DIV_id int FOREIGN KEY REFERENCES Divisions(id),
	PRIMARY KEY(id,DIV_id),
	LOC_id int FOREIGN KEY REFERENCES locations(id)
)
create table Contacts
(
	id int,
	email varchar(20) PRIMARY KEY,
	name varchar(20),
	DIV_id int FOREIGN KEY REFERENCES Divisions(id),
	LOC_id int FOREIGN KEY REFERENCES locations(id)
)