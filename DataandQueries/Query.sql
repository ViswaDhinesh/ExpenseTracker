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