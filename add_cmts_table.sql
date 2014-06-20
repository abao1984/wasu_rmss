--alter table MachineRoom add primary key (ID);
--alter table ip_bussiness add primary key(ID);
--drop table CMTS;
create table CMTS
(
id int identity(1,1) not null primary key,
device_code varchar(100) not null default '',
belong_to varchar(100) default '',
room_id int foreign key references MachineRoom(id),
bussiness_id bigint foreign key references IP_bussiness(ID)
);

insert into cmts (device_code,belong_to,room_id)values('abc-001','test','885');
insert into cmts (device_code,belong_to,room_id)values('abc-002','test','886');
insert into cmts (device_code,belong_to,room_id)values('abc-003','test','887');
insert into cmts (device_code,belong_to,room_id)values('abc-004','test','888');

select top 10 * from cmts;
