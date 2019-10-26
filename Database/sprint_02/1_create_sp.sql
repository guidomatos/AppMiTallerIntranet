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
declare @vl_no_clave_web_encriptada varchar(100)
select @vl_no_clave_web_encriptada = dbo.fn_EncriptarClave('12345678')
update mae_cliente
set no_clave_web = @vl_no_clave_web_encriptada
where nu_documento ='46124933'

exec dbo.src_sps_login_cliente_web '46124933', '12345678'
****************************************************************/
(
@vi_usuario varchar(20),
@vi_clave varchar(50)
)
as
begin

	declare @vl_no_clave_web_encriptada varchar(100) = ''
	select @vl_no_clave_web_encriptada = dbo.fn_EncriptarClave(@vi_clave)

	select
	nid_cliente
	from mae_cliente
	where
		nu_documento = @vi_usuario
	and no_clave_web = @vl_no_clave_web_encriptada
	
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

if exists (SELECT o.name FROM sys.objects o inner join sys.schemas s on o.schema_id = s.schema_id and o.type = 'P' and s.name = 'dbo' and o.name = 'src_spu_cliente_web')
    drop procedure src_spu_cliente_web
GO

CREATE PROCEDURE src_spu_cliente_web
/*****************************************************************************          
OBJETIVO  : ACTUALIZA LOS DATOS DE CLIENTE WEB
NOTA  :           
****************************************************************************          
select no_clave_web,*from mae_cliente where nu_documento = '46124933'

exec src_spu_cliente_web @vi_nid_cliente=0,@vi_co_tipo_documento=N'0',@vi_nu_documento=N'42997694',@vi_no_nombre=N'Rebeca',@vi_no_ape_paterno=N'diaz',@vi_no_ape_materno=N'huamani',@vi_nu_tel_movil=N'982255012',@vi_no_email=N'rebeca.diaz.huamani@gmail.com',@vi_tx_direccion=N'',@vi_nu_placa=N'cix644',@vi_nid_marca=12,@vi_nid_modelo=1086,@vi_no_clave_web=N''
****************************************************************************/          
(          
@vi_nid_cliente			int,
@vi_co_tipo_documento	char(2),
@vi_nu_documento		varchar(20),
@vi_no_nombre			varchar(50),
@vi_no_ape_paterno		varchar(50),
@vi_no_ape_materno		varchar(50),
@vi_nu_tel_movil		varchar(20),
@vi_no_email			varchar(255),
@vi_tx_direccion		varchar(255),
@vi_nu_placa			varchar(50),
@vi_nid_marca			int,
@vi_nid_modelo			int,
@vi_no_clave_web		varchar(20)=NULL
)
as
begin try
 
	--declaración de variables          
	declare @vo_id_usuario int          
	declare @vo_resultado int          
	declare @vl_nu_error int, @vl_fl_transaccion char(1);
	declare @val_retorno int, @val_msg_retorno varchar(max)

	set @vi_nu_documento = LTRIM(RTRIM(ISNULL(@vi_nu_documento,'')));
	set @vi_no_nombre = LTRIM(RTRIM(ISNULL(@vi_no_nombre,'')));
	set @vi_no_ape_paterno = LTRIM(RTRIM(ISNULL(@vi_no_ape_paterno,'')));
	set @vi_no_ape_materno = LTRIM(RTRIM(ISNULL(@vi_no_ape_materno,'')));
	set @vi_nu_tel_movil = LTRIM(RTRIM(ISNULL(@vi_nu_tel_movil,'')));
	set @vi_no_email = LTRIM(RTRIM(ISNULL(@vi_no_email,'')));
	set @vi_tx_direccion = LTRIM(RTRIM(ISNULL(@vi_tx_direccion,'')));
	set @vi_nu_placa = LTRIM(RTRIM(ISNULL(@vi_nu_placa,'')));
	set @vi_nid_marca = ISNULL(@vi_nid_marca,0);
	set @vi_nid_modelo = ISNULL(@vi_nid_modelo,0);

	--Validaciones
	declare @vl_nid_cliente int;
	select @vl_nid_cliente = nid_cliente from mae_cliente where nu_documento = @vi_nu_documento and fl_inactivo = '0'

	--Validacion 1
	declare @vl_nu_documento_actual varchar(20)
	if (ISNULL(@vl_nid_cliente,0) != 0)
	begin
		select @vl_nu_documento_actual = nu_documento from mae_cliente where nid_cliente = @vl_nid_cliente

		if (@vl_nu_documento_actual != @vi_nu_documento)
		begin
			set @vo_resultado = -5; --No se puede actualizar. Nro. Documento ya existe
			select @vo_resultado
			return
		end
	end

	--Validacion 2
	declare @vl_nid_vehiculo int

	select @vl_nid_vehiculo = nid_vehiculo from mae_vehiculo where nu_placa = @vi_nu_placa and fl_activo = 'A'

	if (ISNULL(@vl_nid_vehiculo,0) != 0)
	begin
		
		declare @vl_nu_documento_vehiculo_actual varchar(20);

		select
		@vl_nu_documento_vehiculo_actual = cli.nu_documento
		from mae_vehiculo veh
		inner join mae_cliente cli on cli.nid_cliente = veh.nid_contacto
		where
			veh.fl_activo = 'A'
		and veh.nu_placa = @vi_nu_placa

		if (@vl_nu_documento_vehiculo_actual != @vi_nu_documento)
		begin
			set @vo_resultado = -6; --No se puede actualizar. Cliente no es propietario del vehículo
			select @vo_resultado
			return
		end

	end

          
	--inicialización de variables
	set @vl_fl_transaccion = '0';
          
	begin transaction

	set @vl_fl_transaccion = '1';

	--ACTUALIZACION DE CLIENTE

	if (@vl_nid_cliente IS NULL) --Nuevo Cliente
	begin

		insert mae_cliente
		(
			co_tipo_cliente,
			co_tipo_documento,
			nu_documento,
			no_cliente,
			no_ape_pat,
			no_ape_mat,
			nu_celular,
			no_correo,
			tx_direccion,
			fe_crea,
			co_usuario_crea,
			fl_inactivo
		)
		values
		(
			'0001', --Persona Natural
			@vi_co_tipo_documento,
			@vi_nu_documento,
			@vi_no_nombre,
			@vi_no_ape_paterno,
			@vi_no_ape_materno,
			@vi_nu_tel_movil,
			@vi_no_email,
			@vi_tx_direccion,
			GETDATE(),
			'WEB',
			'0'
		)

		set @vl_nid_cliente = @@IDENTITY;
		
	end
	else
	begin

		update mae_cliente
		set
		co_tipo_cliente = '0001',
		co_tipo_documento = @vi_co_tipo_documento,
		nu_documento = @vi_nu_documento,
		no_cliente = @vi_no_nombre,
		no_ape_pat = @vi_no_ape_paterno,
		no_ape_mat = @vi_no_ape_materno,
		nu_celular = @vi_nu_tel_movil,
		no_correo = @vi_no_email,
		tx_direccion = @vi_tx_direccion
		where nid_cliente = @vl_nid_cliente
		
	end

	--ACTUALIZACION DE PLACA
	
	if (ISNULL(@vl_nid_vehiculo,0) = 0)
	begin

		insert mae_vehiculo
		(
		nid_propietario,
		nid_cliente,
		nid_contacto,
		nu_placa,
		nid_marca,
		nid_modelo,
		fl_activo
		)
		values
		(
		@vl_nid_cliente,
		@vl_nid_cliente,
		@vl_nid_cliente,
		@vi_nu_placa,
		@vi_nid_marca,
		@vi_nid_modelo,
		'A'
		);

	end
	else
	begin

		update mae_vehiculo
		set 
		nid_propietario = @vl_nid_cliente,
		nid_cliente = @vl_nid_cliente,
		nid_contacto = @vl_nid_cliente,
		nid_marca = @vi_nid_marca,
		nid_modelo = @vi_nid_modelo
		where nid_vehiculo = @vl_nid_vehiculo

	end

	--ACTUALIZACION DE ACCESO
	if exists
	(
		select 1 from mae_cliente
		where 
			nid_cliente = @vl_nid_cliente
		and ISNULL(no_clave_web,'') = ''
		and fl_inactivo = '0'
	)
	begin

		declare @vl_no_clave_web_encriptada varchar(100);

		if (ISNULL(@vi_no_clave_web,'') = '') set @vi_no_clave_web = '12345678';
		select @vl_no_clave_web_encriptada = dbo.fn_EncriptarClave(@vi_no_clave_web);

		update mae_cliente
		set no_clave_web = @vl_no_clave_web_encriptada
		where
			nid_cliente = @vl_nid_cliente
		and ISNULL(no_clave_web,'') = ''

	end

	commit transaction

	set @vl_fl_transaccion = '0';
          
	set @vo_resultado = @vl_nid_cliente;

	select @vo_resultado as vo_resultado
          
end try          
begin catch          
	--select error_message(), error_number()          
	-- 2627 error de unique          
	-- 515 error de insertar valor null no permitido          
	-- 547 error conflicto de llave foranea          

	print error_message()
	print convert(varchar,error_number())

	set     @vl_nu_error = error_number()          
	if      @vl_nu_error = 2627 set @vo_id_usuario = -2          
	else if @vl_nu_error = 515 set @vo_id_usuario  = -3          
	else if @vl_nu_error = 547 set @vo_id_usuario  = -4          
	else   set @vo_id_usuario = -1          

	if (@vl_fl_transaccion = '1') rollback transaction          
	select @vo_id_usuario as vo_id_error          
end catch 
go

if exists (SELECT o.name FROM sys.objects o inner join sys.schemas s on o.schema_id = s.schema_id and o.type = 'P' and s.name = 'dbo' and o.name = 'src_sps_vehiculo_por_cliente_web')
    drop procedure src_sps_vehiculo_por_cliente_web
GO

CREATE PROCEDURE src_sps_vehiculo_por_cliente_web
/*****************************************************************************
Nombre: src_sps_vehiculo_por_cliente_web
Objetivo: Listar Datos de Vehiculo del Cliente Web
Nota:
****************************************************************************
exec dbo.src_sps_vehiculo_por_cliente_web 1509561
exec dbo.src_sps_vehiculo_por_cliente_web 1509570
select*from mae_cliente where nu_documento = '46124933'
select*from mae_cliente where nu_documento = '42997694'
****************************************************************************/
(        
@vi_nid_cliente int
)        
AS
BEGIN

	SELECT
	veh.nid_vehiculo,
	veh.nid_marca,
	veh.nid_modelo,
	veh.nu_placa
	FROM mae_vehiculo veh
	INNER JOIN mae_cliente cli on cli.nid_cliente = veh.nid_contacto
	WHERE
		veh.fl_activo = 'A'
	and veh.nid_contacto = @vi_nid_cliente

END
go