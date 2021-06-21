create database HoDucThangDBWeb;

create table UserAccount(
Id int primary key not null IDENTITY,
UserName nvarchar(50),
Password nvarchar(20),
Status bit
);

create table Category(
Id int primary key not null IDENTITY,
Name nvarchar(50),
Desciption nvarchar(max)
);

create table Product(
Id int primary key not null IDENTITY,
CategoryId int ,
	CONSTRAINT fk_category
  FOREIGN KEY (CategoryId)
  REFERENCES Category(Id),
Name nvarchar(50),
UniCost decimal(18,0),
Quantity int,
Image nvarchar(max),
Description nvarchar(max),
Status nvarchar(50) ,
);

insert into UserAccount(UserName,Password,Status)
values('admin','123456','True'),
	  ('ducthang','123456','True'),
	  ('ronaldo','123456','True'),
	  ('vantoan','123456','True'),
	  ('congphuong','123456','True'),
	  ('tantruong','123456','True')

insert into Category(Name,Desciption)
values ('Laptop văn phòng',N'Đang cập nhật'),
		('Laptop gaming',N'Đang cập nhật');

insert into Product(CategoryId,Name,UniCost,Status,Quantity,Image,Description)
values (1,'Laptop Asus NoteBook ',20000000,N'Còn hàng',7,'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRtaKPb7mspJ0UHyXVDq96879hWVzLqXPbppA&usqp=CAU',N'Nhỏ gọn'),
		(2,'Laptop Dell Vostro',30000000,N'Còn hàng',20,'https://www.sieuthimaychu.vn/datafiles/setone/14939493869743.jpg',N'Hiệu năng mạnh mẽ'),
		(2,'Acer Nitro 5',30000000,N'Còn hàng',20,'https://xgear.vn/wp-content/uploads/2021/04/an515-56-05.jpg',N'Hiệu năng mạnh mẽ'),
		(1,'Laptop HP',30000000,N'Còn hàng',10,'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRtaKPb7mspJ0UHyXVDq96879hWVzLqXPbppA&usqp=CAU',N'Đẳng cấp thương hiệu'),
		(1,'Laptop MSI 5',20000000,N'Còn hàng',10,'https://cdn.ankhang.vn/media/product/18991_laptop_msi_gp76_leopard_10ue_604vn_1.jpg',N'Đẳng cấp thương hiệu')
	