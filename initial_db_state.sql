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

create table City
(
	IDCity int primary key identity,
	Name nvarchar(50)
);

create table TravelOrder
(
	IDTravelOrder int primary key identity,
	DriverID int foreign key references Driver(IDDriver) not null,
	VehicleID int foreign key references Vehicle(IDVehicle) not null,
	TravelOrderTypeID int foreign key references TravelOrderType(IDTravelOrderType) not null,
	ExpectedNumberOfDays int not null default 1,
	ReasonForTravel nvarchar(100),
	CityStartId int foreign key references City(IDCity),
	CityEndId int foreign key references City(IDCity),
	TotalCost money,
	DocumentDate datetime
);

create table FuelInfo
(
	IDFuelInfo int primary key identity,
	Location nvarchar(50),
	Amount float,
	Price money,
	DatePurchased date,
	TravelOrderID int foreign key references TravelOrder(IDTravelOrder)
)

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
	FuelExpense float,
	TravelOrderID int foreign key references TravelOrder(IDTravelOrder)
);

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
		('2020-02-05', 2, 2100, 'Zamagljena stakla');

	insert into City(Name)
	values
		('Zagreb'),
		('Split'),
		('Rijeka'),
		('Slavonski Brod'),
		('Osijek'),
		('Zadar'),
		('Pula'),
		('Karlovac'),
		('Šibenik'),
		('Dubrovnik'),
		('Luka'),
		('Lukač'),
		('Bol'),
		('Popovac'),
		('Zrinski Topolovac'),
		('Slano'),
		('Garčin'),
		('Jakšić'),
		('Podbablje'),
		('Klakar'),
		('Biograd na Moru'),
		('Vis'),
		('Krašić'),
		('Delnice'),
		('Rovišće'),
		('Rovinj'),
		('Labin'),
		('Bilice'),
		('Mikleuš'),
		('Ogulin'),
		('Žumberak'),
		('Solin'),
		('Čepin'),
		('Kukljica'),
		('Babino Polje'),
		('Povljana'),
		('Kaštelir'),
		('Rešetari'),
		('Lasinja'),
		('Galovac'),
		('Šodolovci'),
		('Kalinovac'),
		('Pribislavec'),
		('Jagodnjak'),
		('Podgorač'),
		('Orehovica'),
		('Donji Andrijevci'),
		('Dugopolje'),
		('Prelog'),
		('Donja Stubica'),
		('Vir'),
		('Petrinja'),
		('Gračac'),
		('Pakrac'),
		('Oklaj'),
		('Vojnić'),
		('Krnjak'),
		('Hercegovac'),
		('Saborsko'),
		('Pregrada'),
		('Otok'),
		('Sveti Martin na Muri'),
		('Brestovac'),
		('Žminj'),
		('Komiža'),
		('Prgomet'),
		('Sućuraj'),
		('Nova Kapela'),
		('Vrlika'),
		('Podcrkavlje'),
		('Klana'),
		('Antunovac'),
		('Novalja'),
		('Topusko'),
		('Mali Bukovec'),
		('Brinje'),
		('Vodice'),
		('Otrić-Seoci'),
		('Ploče'),
		('Ružić'),
		('Rakovec'),
		('Tuhelj'),
		('Gornja Rijeka'),
		('Bošnjaci'),
		('Petrijanec'),
		('Orle'),
		('Biskupija'),
		('Lobor'),
		('Desinić'),
		('Šestanovac'),
		('Kistanje'),
		('Novi Golubovec'),
		('Ozalj'),
		('Peteranec'),
		('Milna'),
		('Oriovac'),
		('Dragalić'),
		('Voćin'),
		('Donji Miholjac'),
		('Lumbarda'),
		('Severin'),
		('Sveti Ivan Zelina'),
		('Buje'),
		('Brtonigla'),
		('Nerežišće'),
		('Lećevica'),
		('Dvor'),
		('Matulji'),
		('Goričan'),
		('Vlaka'),
		('Brckovljani'),
		('Donji Kukuruzari'),
		('Kloštar Podravski'),
		('Kloštar Ivanić'),
		('Darda'),
		('Daruvar'),
		('Vrhovine'),
		('Nova Rača'),
		('Tar'),
		('Zemunik Donji'),
		('Klis'),
		('Petlovac'),
		('Vrsar'),
		('Dekanovec'),
		('Ðurđenovac'),
		('Ðurđevac'),
		('Čazma'),
		('Rugvica'),
		('Buzet'),
		('Cernik'),
		('Levanjska Varoš'),
		('Gradište'),
		('Požega'),
		('Barilović'),
		('Pićan'),
		('Tompojevci'),
		('Trnovec Bartolovečki'),
		('Budinšćina'),
		('Čačinci'),
		('Fažana'),
		('Bednja'),
		('Bedenica'),
		('Imotski'),
		('Perušić'),
		('Hrvace'),
		('Gornji Stupnik'),
		('Novi Marof'),
		('Bilje'),
		('Kumrovec'),
		('Sveti Ilija'),
		('Mursko Središće'),
		('Metković'),
		('Lekenik'),
		('Županja'),
		('Podstrana'),
		('Vrbnik'),
		('Rasinja'),
		('Veliki Bukovec'),
		('Omišalj'),
		('Lovinac'),
		('Umag'),
		('Srebreno'),
		('Dežanovac'),
		('Medulin'),
		('Orahovica'),
		('Baška'),
		('Kutina'),
		('Generalski Stol'),
		('Velika Gorica'),
		('Slavonski Šamac'),
		('Dugi Rat'),
		('Mrkopalj'),
		('Vukovar'),
		('Oroslavje'),
		('Donji Vidovec'),
		('Lokve'),
		('Mali Lošinj'),
		('Babina Greda'),
		('Tribunj'),
		('Oprisavci'),
		('Oprtalj'),
		('Staro Petrovo Selo'),
		('Kostrena'),
		('Primorski Dolac'),
		('Visoko'),
		('Samobor'),
		('Jesenice'),
		('Hrvatska Dubica'),
		('Varaždinske Toplice'),
		('Varaždin'),
		('Velika Trnovitica'),
		('Petrovsko'),
		('Nin'),
		('Zagvozd'),
		('Stankovci'),
		('Zmijavci'),
		('Punitovci'),
		('Ražanac'),
		('Jasenovac'),
		('Bale'),
		('Trpanj'),
		('Ilok'),
		('Pag'),
		('Ervenik'),
		('Privlaka'),
		('Ivanska'),
		('Kolan'),
		('Lipovljani'),
		('Valpovo'),
		('Markušica'),
		('Skradin'),
		('Donji Lapac'),
		('Mala Subotica'),
		('Martijanec'),
		('Sveti Petar u Šumi'),
		('Murter'),
		('Podravske Sesvete'),
		('Gornji Mihaljevec'),
		('Crnac'),
		('Breznica'),
		('Gradec'),
		('Brela'),
		('Brdovec'),
		('Radoboj'),
		('Hrašćina'),
		('Stari Mikanovci'),
		('Rogoznica'),
		('Ivankovo'),
		('Omiš'),
		('Jelsa'),
		('Zagorska Sela'),
		('Duga Resa'),
		('Sinj'),
		('Semeljci'),
		('Viljevo'),
		('Belišće'),
		('Gundinci'),
		('Gunja'),
		('Križ'),
		('Bedekovčina'),
		('Klinča Sela'),
		('Udbina'),
		('Andrijaševci'),
		('Nedeščina'),
		('Nedelišće'),
		('Sveti Filip i Jakov'),
		('Koška'),
		('Mače'),
		('Starigrad'),
		('Sibinj'),
		('Ivanić-Grad'),
		('Otok'),
		('Pleternica'),
		('Špišić-Bukovica'),
		('Bebrina'),
		('Garešnica'),
		('Slunj'),
		('Preko'),
		('Vladislavci'),
		('Zabok'),
		('Sveti Ivan Žabno'),
		('Marina'),
		('Lepoglava'),
		('Supetar'),
		('Cista Provo'),
		('Ribnik'),
		('Vrbovsko'),
		('Mihovljan'),
		('Pirovac'),
		('Breznički Hum'),
		('Vrbanja'),
		('Donja Voća'),
		('Poreč'),
		('Trogir'),
		('Mošćenička Draga'),
		('Drniš'),
		('Koprivnički Bregi'),
		('Vratišinec'),
		('Josipdol'),
		('Donja Motičina'),
		('Posedarje'),
		('Krapina'),
		('Krapinske Toplice'),
		('Ferdinandovac'),
		('Cestica'),
		('Našice'),
		('Sutivan'),
		('Štefanje'),
		('Rakovica'),
		('Gola'),
		('Korenica'),
		('Ðulovac'),
		('Kastav'),
		('Gornji Kneginec'),
		('Klanjec'),
		('Podgora'),
		('Sveti Petar Orehovec'),
		('Orebić'),
		('Sisak'),
		('Sveti Križ Začretje'),
		('Kneževi Vinogradi'),
		('Virje'),
		('Virovitica'),
		('Donja Pušća'),
		('Čaglin'),
		('Ravna Gora'),
		('Marčana'),
		('Karlobag'),
		('Karlovac'),
		('Pitomača'),
		('Otočac'),
		('Stara Gradiška'),
		('Gradina'),
		('Ližnjan'),
		('Lanišće'),
		('Viškovci'),
		('Ljubešćica'),
		('Tučepi'),
		('Satnica Ðakovačka'),
		('Unešić'),
		('Kaštel Sućurac'),
		('Benkovac'),
		('Velika Kopanica'),
		('Molve'),
		('Končanica'),
		('Nuštar'),
		('Sukošan'),
		('Lastovo'),
		('Novigrad'),
		('Vođinci'),
		('Hvar'),
		('Civljane'),
		('Sunja'),
		('Skrad'),
		('Ludbreg'),
		('Motovun'),
		('Glina'),
		('Grubišno Polje'),
		('Tinjan'),
		('Baška Voda'),
		('Draž'),
		('Postire'),
		('Jakovlje'),
		('Klenovnik'),
		('Vrgorac'),
		('Gvozd'),
		('Zlatar Bistrica'),
		('Višnjan'),
		('Čeminac'),
		('Cetingrad'),
		('Tordinci'),
		('Berek'),
		('Beretinec'),
		('Lokvičič'),
		('Grožnjan'),
		('Hlebine'),
		('Drenovci'),
		('Barban'),
		('Dubravica'),
		('Knin'),
		('Suhopolje'),
		('Bosiljevo'),
		('Mlinište'),
		('Runović'),
		('Netretić'),
		('Petrijevci'),
		('Jalžabet'),
		('Gradac'),
		('Selca'),
		('Davor'),
		('Lupoglav'),
		('Grohote'),
		('Sokolovac'),
		('Kršan'),
		('Sikirevci'),
		('Svetvinčenat'),
		('Sračinec'),
		('Jarmina'),
		('Donji Muć'),
		('Nova Gradiška'),
		('Pazin'),
		('Maruševec'),
		('Poljanica Bistranska'),
		('Kaptol'),
		('Čabar'),
		('Brodski Stupnik'),
		('Lišane Ostrovičke'),
		('Veliko Trojstvo'),
		('Veliko Trgovišće'),
		('Okučani'),
		('Drenje'),
		('Kula Norinska'),
		('Lopar'),
		('Kotoriba'),
		('Gospić'),
		('Tisno'),
		('Zdenci'),
		('Marija Gorica'),
		('Gornji Bogičevci'),
		('Draganići'),
		('Kraljevica'),
		('Kraljevec na Sutli'),
		('Dobrinj'),
		('Ðurmanec'),
		('Sveti Ðurđ'),
		('Čađavica'),
		('Sirač'),
		('Janjina'),
		('Strizivojna'),
		('Gornja Vrba'),
		('Ðelekovec'),
		('Vižinada'),
		('Cavtat'),
		('Čavle'),
		('Gorjani'),
		('Novo Virje'),
		('Opatija'),
		('Farkaševac'),
		('Drnje'),
		('Veliki Grđevac'),
		('Dugo Selo'),
		('Ðakovo'),
		('Kali'),
		('Funtana'),
		('Sopje'),
		('Cres'),
		('Štrigova'),
		('Vrbovec'),
		('Vrsi'),
		('Lopatinec'),
		('Makarska'),
		('Kutjevo'),
		('Tounj'),
		('Belica'),
		('Nova Bukovica'),
		('Velika'),
		('Novigrad'),
		('Novigrad Podravski'),
		('Gornje Jesenje'),
		('Sveta Marija'),
		('Sveta Nedjelja'),
		('Koprivnica'),
		('Popovača'),
		('Crikvenica'),
		('Čakovec'),
		('Kijevo'),
		('Vrbje'),
		('Plaški'),
		('Smokvica'),
		('Šandrovac'),
		('Raša'),
		('Trpinja'),
		('Kapela'),
		('Borovo'),
		('Pokupsko'),
		('Lovas'),
		('Sali'),
		('Primošten'),
		('Bakar'),
		('Martinska Ves'),
		('Marjanci'),
		('Vidovec'),
		('Stubičke Toplice'),
		('Donji Proložac'),
		('Lovran'),
		('Lovreć'),
		('Sveti Lovreč Pazenatički'),
		('Kalnik'),
		('Štitar'),
		('Novi Vinodolski'),
		('Trilj'),
		('Bukovlje'),
		('Legrad'),
		('Kraj'),
		('Privlaka'),
		('Žakanje'),
		('Pašman'),
		('Brod Moravice'),
		('Domašinec'),
		('Pučišće'),
		('Tovarnik'),
		('Korčula'),
		('Ston'),
		('Slatina'),
		('Okrug Gornji'),
		('Bogdanovci'),
		('Donji Seget'),
		('Rab'),
		('Kamanje'),
		('Kanfanar'),
		('Cerovlje'),
		('Zlatar'),
		('Koprivnički Ivanec'),
		('Donji Kraljevec'),
		('Punat'),
		('Dubrava'),
		('Lipik'),
		('Beli Manastir'),
		('Obrovac'),
		('Jastrebarsko'),
		('Kravarsko'),
		('Negoslavci'),
		('Podravska Moslavina'),
		('Viškovo'),
		('Magadenovac'),
		('Feričanci'),
		('Zadvarje'),
		('Bjelovar'),
		('Stari Jankovci'),
		('Starigrad'),
		('Preseka'),
		('Erdut'),
		('Jelenje'),
		('Strahoninec'),
		('Šenkovec'),
		('Novska'),
		('Bibinje'),
		('Križevci'),
		('Fužine'),
		('Blato'),
		('Škabrnje'),
		('Bizovac'),
		('Gornja Stubica'),
		('Vela Luka'),
		('Vinkovci'),
		('Marija Bistrica'),
		('Malinska'),
		('Krk'),
		('Poličnik'),
		('Pakoštane'),
		('Zaprešić'),
		('Velika Ludina'),
		('Hum na Sutli'),
		('Senj'),
		('Nijemci'),
		('Trnava'),
		('Velika Pisanica'),
		('Karojba'),
		('Opuzen'),
		('Gračišće'),
		('Vrpolje'),
		('Konjšćina'),
		('Selnica'),
		('Vuka'),
		('Vinica'),
		('Donja Dubrava'),
		('Vodnjan'),
		('Majur'),
		('Hrvatska Kostajnica'),
		('Ernestinovo'),
		('Polača'),
		('Pisarovina'),
		('Ivanec'),
		('Bribir'),
		('Podturen'),
		('Cerna');
end
go

--vehicle crud sps
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

create procedure deleteVehicle
	@IDVehicle int
as
begin
	delete from Vehicle
	where Vehicle.IDVehicle = @IDVehicle
end
go

create proc clearDatabase
as
begin
	delete from RouteInfo;
	delete from FuelInfo;
	delete from TravelOrder;

	delete from ServiceInfo;
	delete from City;
	delete from TravelOrderType;
	delete from Driver;
	delete from Vehicle;
end
go

--travel crud order sps
create procedure getAllTravelOrders
as
begin
	select * from TravelOrder
end
go

create procedure getTravelOrder
	@IDTravelOrder int
as
begin
	select * from TravelOrder
	where IDTravelOrder = @IDTravelOrder
end
go

create procedure insertTravelOrder
	@DriverID int,
	@VehicleID int,
	@TravelOrderTypeID int,
	@ExpectedNumberOfDays int,
	@ReasonForTravel nvarchar(100),
	@CityStartId int,
	@CityEndId int,
	@TotalCost money,
	@DocumentDate datetime,
	@NewId int output
as
begin
	update Vehicle
	set IsAvailable = 0
	where IDVehicle = @VehicleID

	insert into TravelOrder
	(
		DriverID,
		VehicleID,
		TravelOrderTypeID,
		ExpectedNumberOfDays,
		ReasonForTravel,
		CityStartId,
		CityEndId,
		TotalCost,
		DocumentDate
	)
	values
	(
		@DriverID,
		@VehicleID,
		@TravelOrderTypeID,
		@ExpectedNumberOfDays,
		@ReasonForTravel,
		@CityStartId,
		@CityEndId,
		@TotalCost,
		@DocumentDate
	);

	set @NewId = SCOPE_IDENTITY();
end
go

create procedure updateTravelOrder
	@IDTravelOrder int,
	@DriverID int,
	@VehicleID int,
	@TravelOrderTypeID int,
	@ExpectedNumberOfDays int,
	@ReasonForTravel nvarchar(100),
	@CityStartId int,
	@CityEndId int,
	@TotalCost money,
	@DocumentDate datetime
as
begin
	update TravelOrder
	set
		DriverID = @DriverID,
		VehicleID = @VehicleID,
		TravelOrderTypeID = @TravelOrderTypeID,
		ExpectedNumberOfDays = @ExpectedNumberOfDays,
		ReasonForTravel = @ReasonForTravel,
		CityStartId = @CityStartId,
		CityEndId = @CityEndId,
		TotalCost = @TotalCost,
		DocumentDate = @DocumentDate
	where IDTravelOrder = @IDTravelOrder;
end
go
create procedure deleteTravelOrder
	@IDTravelOrder int
as
begin
	declare @RouteInfoId int;
	declare @FuelInfoId int;
	declare @VehicleId int;

	select @VehicleId = t.VehicleID
	from TravelOrder as t 
	where t.IDTravelOrder = @IDTravelOrder;

	delete from RouteInfo
	where TravelOrderID = @IDTravelOrder;

	delete from FuelInfo
	where TravelOrderID = @IDTravelOrder;

	update Vehicle
	set IsAvailable = 1
	where IDVehicle = @VehicleId

	delete from TravelOrder
	where IDTravelOrder = @IDTravelOrder;
end
go

create procedure getAllRouteInfoes
as
begin
	select * from RouteInfo
end
go

create procedure getRouteInfo
	@IDRouteInfo int
as
begin
	select * 
	from RouteInfo
	where IDRouteInfo = @IDRouteInfo
end
go

create procedure insertRouteInfo
	@DateTimeStart datetime,
	@DateTimeEnd datetime,
	@LatitudeStart float,
	@LongitudeStart float,
	@LatitudeEnd float,
	@LongitudeEnd float,
	@DistanceInKm float,
	@AverageSpeed float,
	@FuelExpense float,
	@TravelOrderID int
as
begin
	insert into RouteInfo
	(
		DateTimeStart,
		DateTimeEnd,
		LatitudeStart,
		LongitudeStart,
		LatitudeEnd,
		LongitudeEnd, 
		DistanceInKm,
		AverageSpeed,
		FuelExpense,
		TravelOrderID
	)
	values
	(
		@DateTimeStart,
		@DateTimeEnd,
		@LatitudeStart,
		@LongitudeStart,
		@LatitudeEnd,
		@LongitudeEnd,
		@DistanceInKm,
		@AverageSpeed,
		@FuelExpense,
		@TravelOrderID
	)
end
go
create procedure updateRouteInfo
	@IDRouteInfo int,
	@DateTimeStart datetime,
	@DateTimeEnd datetime,
	@LatitudeStart float,
	@LongitudeStart float,
	@LatitudeEnd float,
	@LongitudeEnd float,
	@DistanceInKm float,
	@AverageSpeed float,
	@FuelExpense float,
	@TravelOrderID int
as
begin
	update RouteInfo
	set
		DateTimeStart = @DateTimeStart,
		DateTimeEnd = @DateTimeEnd,
		LatitudeStart = @LatitudeStart,
		LongitudeStart = @LongitudeStart,
		LatitudeEnd = @LatitudeEnd,
		LongitudeEnd = @LongitudeEnd,
		DistanceInKm = @DistanceInKm,
		AverageSpeed = @AverageSpeed,
		FuelExpense = @FuelExpense,
		TravelOrderID = @TravelOrderID
	where IDRouteInfo = @IDRouteInfo
end
go

create procedure deleteRouteInfo
	@IDRouteInfo int
as
begin
	delete from RouteInfo
	where IDRouteInfo = @IDRouteInfo
end