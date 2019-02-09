create database newdata
go
use newdata
go
create table Student
(
Id int primary key identity (1,1),
Name varchar(50),
Email varchar(50)
)
select * from Student