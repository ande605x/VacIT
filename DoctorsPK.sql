alter table Doctors
alter column Ydertype int not null;
go
alter table Doctors
add constraint PK_Ydertype primary key clustered (Ydertype);
go