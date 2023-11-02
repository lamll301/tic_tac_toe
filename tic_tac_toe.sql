create database tic_tac_toe
go

use tic_tac_toe
go

create table Player (
	Id int primary key not null,
	Ten nvarchar(30) not null,
	CacODaDanh nvarchar(100)
)

insert into Player(Id,Ten,CacODaDanh) values('1','O','1, 2, 4')
insert into Player(Id,Ten,CacODaDanh) values('2','X','7, 8')

select * from Player
update Player set CacODaDanh = 'button1, button2' where Id = '1'
update Player set CacODaDanh = 'button3, button4' where Id = '2'