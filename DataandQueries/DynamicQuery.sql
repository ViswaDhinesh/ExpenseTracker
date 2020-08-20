--alter procedure dbo.[sp_Users]
--(
--@Query nvarchar(max) = ''
--)
--as
--begin
--	Declare @ranQuery nvarchar(max)
--	set @ranQuery = 'Select * from ETUser'
--	if (@Query <> '')
--		Set @ranQuery += @Query

--	Exec (@ranQuery)
--end


--Exec [sp_Users] ' where UserUniqueID > 3 and RoleID = 3'


Select * from ETUser