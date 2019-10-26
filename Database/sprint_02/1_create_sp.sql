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



if exists (SELECT o.name FROM sys.objects o inner join sys.schemas s on o.schema_id = s.schema_id and o.type = 'P' and s.name = 'dbo' and o.name = 'src_sps_cliente_por_id')
    drop procedure src_sps_cliente_por_id
GO

create procedure src_sps_cliente_por_id
/****************************************************************
Nombre: src_sps_cliente_por_id
Objetivo: Obtener datos del cliente por ID
Notas:
*****************************************************************
exec dbo.src_sps_cliente_por_id 1509561
select*from mae_cliente where nu_documento = '46124933'
****************************************************************/
(
@vi_nid_cliente int
)
as
begin

	select
	top 1
	cli.nid_cliente,
	cli.co_tipo_cliente,
	cli.co_tipo_documento,
	cli.nu_documento,
	cli.no_cliente,
	cli.no_ape_pat,
	cli.no_ape_mat,
	cli.nu_celular,
	cli.no_correo,
	cli.tx_direccion,
	vehi.nid_vehiculo,
	vehi.nu_placa,
	vehi.nid_marca,
	vehi.nid_modelo
	from mae_cliente cli
	left join mae_vehiculo vehi on vehi.nid_contacto = cli.nid_cliente and vehi.fl_activo = 'A'
	where
	cli.nid_cliente = @vi_nid_cliente
	
end
GO