# Database Design Practice

1. **Design a Database for a company to Manage all its projects**.
Company has diverse offices in several countries, which manage and co-ordinate the project of that country.
Head office has a unique name, city, country, address, phone number and name of the director.
Every head office manages a set of projects with Project code, title, project starting and end date, assigned budget and name of the person in-charge. One project is formed by the set of operations that can affect to several cities.

We want to know what actions are realized in each city storing its name, country and number of inhabitants.
```sql
create table Companies(
	id int PRIMARY KEY,
	name varchar(10),
	City varchar(20),
	cityID int,
	country varchar(20),
	address varchar(50),
	[Phone Number] varchar(20),
	NameOfTheDirector varchar(20),
	FOREIGN KEY(CityID) REFERENCES Cities(CityID)
)
insert into Companies values(1,'RDC','Atlanta',1,'US',null,null,'William'),
				(2,'Diagonos', 'Boston', 2, 'US', null,null, 'Bob'),
				(3,'Operation', 'London',3,'UK',null,null,'Alice'),
				(4,'Atoi','Paris',4,'France',null,null,'Johnson')
create table Cities(
	cityID int PRIMARY KEY,
	Name varchar(20),
	Country varchar(20),
	population int
)
insert into Cities values
(1,'Atlanta','US',240261),
(2,'Boston','US',198273),
(3,'London','UK',27264),
(4,'Paris','France',74323)

create table Projects(
	projectID int PRIMARY KEY,
	title varchar(20),
	startt date,
	endd date,
	budget int,
	director varchar(10)
)
insert into Projects values(1,'a','1990-6-30','1992-6-30',13,'William'),
				(2,'zz','1997-7-13','1999-6-15',69,'Alice'),
				(3,'ghn','2003-9-30','2008-1-21',45,'Bob'),
				(4,'kng','1991-12-30','1996-6-30',13,'Johnson'),
				(5,'mk','1991-6-30','1998-6-30',13,'Johnson'),
				(6,'sae','1993-8-30','1998-3-20',13,'Alice'),
				(7,'a','1990-6-30','1992-6-30',13,'William')
create table ProjectActions(
	actionID int,
	cityID int,
	Name varchar(20),
	projectID int,
	PRIMARY KEY(actionID,projectID),
	FOREIGN KEY(projectID) REFERENCES Projects(projectID),
	FOREIGN KEY(cityID) REFERENCES Cities(cityID)
)
alter table ProjectActions
add CONSTRAINT pk_fore FOREIGN KEY(cityID) REFERENCES Cities(cityID)
/*
sales
reserach
production
advertisement
education
volunteer
*/
insert into ProjectActions values(1,2,'sales',3),
				(1,3,'sales',6),
				(1,4,'sales',2),
				(1,1,'sales',4),
				(2,3,'research',7),
				(2,1,'research',4),
				(2,4,'research',5),
				(3,1,'production',1),
				(3,4,'production',6),
				(4,3,'advertise',2),
				(4,1,'advertise',7),
				(4,2,'advertise',3),
				(4,4,'advertise',4),
				(5,1,'education',1),
				(5,4,'education',2),
				(5,2,'education',6),
				(6,3,'volunteer',4),
				(6,4,'volunteer',3)
```
**DB Diagram:**

![enter image description here](https://github.com/bravo951/full-stack-training/blob/main/Design3.png?raw=true)
## 
2. **Design a database for a lending company which manages lending among people (p2p lending)**
Lenders that lending money are registered with an Id, name and available amount of money for the financial operations.
Borrowers are identified by their id and the company registers their name and a risk value depending on their personal situation.
When borrowers apply for a loan, a new loan code, the total amount, the refund deadline, the interest rate and its purpose are stored in database.

Lenders choose the amount they want to invest in each loan. A lender can contribute with different partial amounts to several loans.
```sql
create table Lenders(
	id int PRIMARY KEY,
	name varchar(20),
	MoneyAmount float
)
create table Borrowers(
	id int PRIMARY KEY,
	name varchar(20),
	riskvalue float,
)
create table IntermidiateCompany(
	Lid int,
	Bid int,
	--PRIMARY KEY(Lid,Bid),
	loadcode int PRIMARY KEY,
	amount float,
	refundDDL date,
	[interest rate] float,
	purpose text
	FOREIGN KEY(Lid) REFERENCES Lenders(id),
	FOREIGN KEY(Bid) REFERENCES Borrowers(id),
)
```
**DB Diagram:**

![enter image description here](https://github.com/bravo951/full-stack-training/blob/main/design2.png?raw=true)

## 
3. **Design a database to maintain the menu of a restaurant.**
Each course has its name, a short description, photo and final price.
Each course has categories characterized by their names, short description, name of the employee in-charge of them.
Besides the courses some recipes are stored. They are formed by the name of their ingredients, the required amount, units of measurements and the current amount in the store.
```sql
create table Course(
	id int PRIMARY KEY,
	description text,
	photo int,
	price float
)
insert into Course values(1,null,null,27.00),
						 (2,null,null,16.53),
						 (3,null,null,31.21)
create table Recipes(
	id int PRIMARY KEY IDENTITY,
	RecipeID int,
	ingredients varchar(20),
	[required amount] float,
	[units measurement] varchar(10),
	storeAmount float
)
insert into Recipes values(1,'pork',0.5,'kg',100),
						  (1,'green pepper',30,'g',30),
						  (1,'salt',3,'g',300),
						  (2,'chicken',0.7,'kg',100),
						  (2,'onion',0.4,'kg',60),
						  (3,'sugar',3,'g',300),
						  (3,'ribe',3,'kg',300),
						  (4,'vinegar',3,'g',300),
						  (4,'potato',1,'kg',50)
create table Categories(
	id int,
	name varchar(20),
	description text,
	PersonInCharge varchar(10),
	courseID int,
	RecipeID int,
	PRIMARY KEY(courseID,RecipeID),
	FOREIGN KEY(courseID) REFERENCES Course(id),
	FOREIGN KEY(RecipeID) REFERENCES Recipes(id)
)
insert into Categories values(1,'a',null,'Bob',1,1),
							 (1,'b',null,'Alice',1,2),
							 (2,'b',null,'Jose',2,3),
							 (2,'b',null,'Alice',2,1),
							 (3,'b',null,'Jose',3,4),
							 (3,'b',null,'Alice',3,2)
```
**DB Diagram:**

![enter image description here](https://github.com/bravo951/full-stack-training/blob/main/Design3.png?raw=true)
