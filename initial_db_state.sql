use master;
go
create database pppk
go
use pppk
go

create table Driver
(
	IDDriver int primary key identity,
	FirstName nvarchar(30) not null,
	LastName nvarchar(30) not null,
	DriversLicence nvarchar(50) not null unique
);

create table Vehicle
(
	IDVehicle int primary key identity,
	Registration nvarchar(30) not null unique,
	Type nvarchar(50) not null,
	Model nvarchar(50) not null,
	YearManufactured smallint not null,
	InitialKilometres float not null,
	IsAvailable bit not null default 1
);

create table RouteInfo
(
	IDRouteInfo int primary key identity,
	DateTimeStart datetime,
	DateTimeEnd datetime,
	LatitudeStart float,
	LongitudeStart float,
	LatitudeEnd float,
	LongitudeEnd float,
	DistanceInKm float,
	AverageSpeed float,
	FuelExpense float
);

create table ServiceInfo
(
	IDService int primary key identity,
	VehicleID int foreign key references Vehicle(IDVehicle) not null,
	DatetimeService datetime,
	Cost money,
	Description nvarchar(200)
);

create table TravelOrderType(
	IDTravelOrderType int primary key identity,
	Type nvarchar(30) not null
);

create table FuelInfo
(
	IDFuelInfo int primary key identity,
	Location nvarchar(50),
	Amount float,
	Price money,
	DatePurchased date
)

create table TravelOrder
(
	IDTravelOrder int primary key identity,
	DriverID int foreign key references Driver(IDDriver) not null,
	VehicleID int foreign key references Vehicle(IDVehicle) not null,
	TravelOrderTypeID int foreign key references TravelOrderType(IDTravelOrderType) not null,
	RouteInfoID int foreign key references RouteInfo(IDRouteInfo),
	FuelInfoID int foreign key references FuelInfo(IDFuelInfo),
	ExpectedNumberOfDays int not null default 1,
	ReasonForTravel nvarchar(100),
	TotalCost money,
	DocumentDate datetime
)
go
create procedure InitializeData
as
begin
	insert into TravelOrderType
	values ('Active'),('Closed'), ('Future');

	insert into Driver(FirstName, LastName, DriversLicence)
	values 
		('Donald', 'Duck', 'ab123456'),
		('Piedro', 'Dog', 'bg878458'),
		('Ante', 'Gust', 'rt897547'),
		('Maja', 'Bee', 'vf654287');

	insert into Vehicle(Model, Type, Registration, YearManufactured, InitialKilometres)
		values
		('Volkswagen', 'Polo', 'zg1234sd', 2000, 500000),
		('Volkswagen', 'Golf', 'zg8798sd', 2005, 10000),
		('Audi', 'R8', 'zg3254bv', 2004, 25000),
		('Škoda', 'Octavia', 'ši666sd', 2012, 32000),
		('Chevrolet', 'Spark', 'ši234pp', 2015, 35000),
		('Bugatti', 'Chiron', 'im8008uz', 2016, 10000)

	insert into ServiceInfo(DatetimeService, VehicleID, Cost, Description)
	values
		(GETDATE(), 1, 1000, 'Promijenjena zadnja felga'),
		('2020-02-01', 2, 5000, 'Zamjena cinculatora'),
		('2020-01-25', 3, 3500, 'Curenje iz gefufne'),
		('2020-02-05', 2, 2100, 'Zamagljena stakla')
end
go
create procedure insertVehicle
	@Model nvarchar(50),
	@Type nvarchar(50),
	@Registration nvarchar(30),
	@YearManufactured smallint,
	@InitialKilometres float,
	@NewId int output
as
begin
	insert into Vehicle(Model, Type, Registration, YearManufactured, InitialKilometres)
	values (@Model, @Type,@Registration, @YearManufactured, @InitialKilometres);

	set @NewId = SCOPE_IDENTITY();
end
go

create procedure updateVehicle
	@IDVehicle int,
	@Model nvarchar(50),
	@Type nvarchar(50),
	@Registration nvarchar(30),
	@YearManufactured smallint,
	@InitialKilometres float
as
begin
	update Vehicle
	set 
		Model = @Model,
		Type = @Type,
		Registration = @Registration,
		YearManufactured = @YearManufactured,
		InitialKilometres = @InitialKilometres
	where IDVehicle = @IDVehicle
end
go

create procedure getAllVehicles
as
begin
	select *
	from Vehicle
end
go

create procedure getVehicle
	@IDVehicle int
as
begin
	select *
	from Vehicle
	where Vehicle.IDVehicle = @IDVehicle
end
go

create proc clearDatabase
as
begin
	delete from TravelOrder;
	delete from ServiceInfo;

	delete from Driver;
	delete from Vehicle;
	delete from RouteInfo;
	delete from FuelInfo;
end