use AdventureWorks2019
go
-- Q1
select ProductID, Name, Color, ListPrice from Production.Product
-- Q2
select ProductID, Name, Color, ListPrice from Production.Product
where ListPrice = 0
-- Q3
select ProductID, Name, Color, ListPrice from Production.Product
where Color is NULL
-- Q4
select ProductID, Name, Color, ListPrice from Production.Product
where Color is not NULL
-- Q5
select ProductID, Name, Color, ListPrice from Production.Product
where Color is not NULL and ListPrice > 0
-- Q6
select Name, Color from Production.Product as report
where Color is not NULL
-- Q7
select Name, Color from Production.Product
where Color in ('Silver','Black')
-- Q8
select ProductID, Name from Production.Product
where ProductID between 400 and 500
-- Q9
select ProductID, Name, Color from Production.Product
where Color in ('Black','Blue')
-- Q10
select ProductID, Name, Color from Production.Product
where Color in ('Black','Blue')
-- Q11
select Name, ListPrice from Production.Product
where Name like 's%'
order by Name
-- Q12
select Name, ListPrice from Production.Product
where Name like '[A,S]%'
order by Name
-- Q13
select Name, ListPrice from Production.Product
where Name like 'spo[^k]%'
order by Name
-- Q14
select distinct Color from Production.Product
order by Color desc
-- Q15
select ProductSubcategoryID, Color from Production.Product
where ProductSubcategoryID is not null and Color is not null
group by ProductSubcategoryID, Color
-- Q16
select ProductSubCategoryID
      , LEFT([Name],35) as [Name]
      , Color, ListPrice 
from Production.Product
where Color not in ('Red','Black') 
      or ListPrice between 1000 and 2000 
      or ProductSubCategoryID = 1
order by ProductID

-- Q17
select ProductSubCategoryID, Name, Color, ListPrice from Production.Product
where ProductSubCategoryID<15 and ListPrice>500 and 
(name='HL Road Frame - Black, 58' or 
name like 'HL Road Frame - Red%' or 
name like 'HL Mo%' or
name like 'Road-350-W Yellow%' or
name like 'Mountain-500 Black%')
order by ProductSubCategoryID desc





