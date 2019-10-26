use AppMiTaller
go

create function dbo.fn_EncriptarClave
(
	@ClaveSecreta varchar(50)
)
returns varchar(100)
as
/*
select dbo.fn_EncriptarClave('1234')
*/
begin

	declare @resultado varchar(100) = ''

	set @resultado = UPPER(master.dbo.fn_varbintohexsubstring(0, HashBytes('SHA1', @ClaveSecreta), 1, 0))

	return @resultado

end
go