use AppMiTaller
go

if exists (SELECT o.name FROM sys.objects o inner join sys.schemas s on o.schema_id = s.schema_id and o.type = 'P' and s.name = 'dbo' and o.name = 'src_sps_login_cliente_web')
    drop procedure src_sps_login_cliente_web
GO

create procedure src_sps_login_cliente_web
/****************************************************************
Nombre: src_sps_login_cliente_web
Objetivo: Login Web
Notas:
*****************************************************************
exec dbo.src_sps_login_cliente_web '46124933', '1234567'
exec dbo.src_sps_login_cliente_web '46124933', '12345678'

select*from mae_cliente where nu_documento = '46124933'
update mae_cliente 
set no_clave_web = '12345678'
where nu_documento = '46124933'
****************************************************************/
(
@vi_usuario varchar(20),
@vi_clave varchar(20)
)
as
begin

	select
	nid_cliente
	from mae_cliente
	where
		nu_documento = @vi_usuario
	and no_clave_web = @vi_clave
	
end
GO