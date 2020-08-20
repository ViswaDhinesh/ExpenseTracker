--update ETMenuAccess set RoleID = 1


--update ETSubMenu set SubMenuUrl = '../' + SubMenuUrl

--update ETMenu set MenuUrl = '../' + MenuUrl

Select * from ETSubMenu

Update ETMenuAccess set CreatedBy = NULL
Update ETMenuAccess set ModifiedBy = NULL

alter table ETUser alter column SourceOfCreation nvarchar(10) NULL

alter table ETCategory alter column CreatedBy bigint NULL

alter table ETCategory alter column ModifiedBy bigint NULL

alter table ETMenu alter column CreatedBy bigint NULL

alter table ETMenu alter column ModifiedBy bigint NULL

alter table ETSubMenu alter column CreatedBy bigint NULL

alter table ETSubMenu alter column ModifiedBy bigint NULL

alter table ETRole alter column CreatedBy bigint NULL

alter table ETRole alter column ModifiedBy bigint NULL

alter table ETValue alter column CreatedBy bigint NULL

alter table ETValue alter column ModifiedBy bigint NULL

alter table ETMenuAccess alter column CreatedBy bigint NULL

alter table ETMenuAccess alter column ModifiedBy bigint NULL

alter table ETUser add DeviceType nvarchar(10) NULL


alter table ETUser add OtpReceivedDate DateTime NULL

alter table ETUser add OtpReceivedDevice nvarchar(1) NULL

--alter table ETUser add OtpDevice nvarchar(1) NULL

alter table ETLandDetails alter column AcresSize float NOT NULL
alter table ETLandDetails alter column AresSize float NOT NULL
alter table ETLandDetails alter column HectareSize float NOT NULL

alter table ETLandDetailsLog alter column AcresSize float NOT NULL
alter table ETLandDetailsLog alter column AresSize float NOT NULL
alter table ETLandDetailsLog alter column HectareSize float NOT NULL

alter table ETValue add OrderNo bigint NULL
alter table ETValue alter column OrderNo bigint NOT NULL

update ETValue set OrderNo = 0