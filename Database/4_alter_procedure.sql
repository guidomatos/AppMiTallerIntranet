use AppMiTaller
go

alter procedure sgsnet_spu_usuario_password
/*****************************************************************************
Objetivo		: Permite cambiar la contraseña
****************************************************************************
exec sgsnet_spu_usuario_password 
@vi_va_login=N'admintaller1',
@vi_va_password=N'pvm738rKL',
@vi_va_password_MD5=N'31-6D-60-7B-03-92-DB-20-CF-A3-23-89-C9-45-DF-C2',
@vi_va_password_des=N'gmatosc88',
@vi_va_cod_usuario=N'admintaller1',
@vi_va_nom_usuario_red=N'',
@vi_va_nom_estacion_red=N'DESKTOP-KN7714R',
@vi_va_ind_act_reset=N'0'
****************************************************************************/
(
@vi_va_login varchar(20),
@vi_va_password varchar(255),
@vi_va_password_MD5 varchar(255),
@vi_va_password_des varchar(255),
@vi_va_cod_usuario varchar(20),
@vi_va_nom_usuario_red varchar(20),
@vi_va_nom_estacion_red varchar(20),
@vi_va_ind_act_reset char(1)
)
as
set nocount on

declare @v_in_retorno int
declare @v_nro_error int

set @vi_va_login = ltrim(rtrim(@vi_va_login));
set @vi_va_cod_usuario = isnull(ltrim(rtrim(@vi_va_cod_usuario)),'')
set @vi_va_nom_estacion_red = ltrim(rtrim(@vi_va_nom_estacion_red))
set @vi_va_nom_usuario_red = ltrim(rtrim(@vi_va_nom_usuario_red))

--Nro. Dias de Caducidad de contraseña
declare @vl_int_dias_caducidad int
select  @vl_int_dias_caducidad = no_valor from mae_politica_contraseha where nid_politica_contraseha = 7

BEGIN TRY

	if (select count(nid_usuario) from usr where [CUSR_ID] = @vi_va_login) > 0
	begin

		update USR
		set 
		VPASSMD5 = @vi_va_password_MD5,
		fe_inicio_acceso = getdate(),
		fe_fin_acceso = DATEADD(day,@vl_int_dias_caducidad,getdate()),
		fl_reset = @vi_va_ind_act_reset,
		CESTBLQ = '0',
		fe_cambio = getdate(),
		co_usuario_cambio = @vi_va_cod_usuario,
		no_estacion_red = @vi_va_nom_estacion_red,
		no_usuario_red = @vi_va_nom_usuario_red
		where CUSR_ID = @vi_va_login;

		SELECT 1 AS vo_va_retval;
	end
	else SELECT -5 AS vo_va_retval;
	

END TRY
BEGIN CATCH
begin
		-- select ERROR_MESSAGE(), ERROR_NUMBER()
		-- 2627 ERROR DE UNIQUE
		-- 515 ERROR DE INSERTAR VALOR NULL NO PERMITIDO
		-- 547 ERROR CONFLICTO DE LLAVE FORANEA
		set @v_nro_error = ERROR_NUMBER()
		if @v_nro_error = 2627 set @v_in_retorno = -2
		else if @v_nro_error = 515 set @v_in_retorno = -3
		else if @v_nro_error = 547 set @v_in_retorno = -4
		else set @v_in_retorno = -1
		
		SELECT @v_in_retorno AS vo_va_retval
		
end	
END CATCH
go

alter procedure sgsnet_getall_combo
/*********************************************          
Nombre Procedure  : sgsnet_getall_combo      
Objetivo          : Obtener el listado de los combos

select distinct nid_marca as [nid_padre],nid_modelo as [nid_hijo],no_modelo as [no_nombre] from mae_modelo
where fl_inactivo = '0'
and no_modelo like '%accen%'

*********************************************/ 
@nid_tabla int
as
begin

	select * from (
		select 2 as nid_tabla, nid_empresa as [nid_padre],nid_marca as [nid_hijo],no_marca as [no_nombre] from mae_marca
		where fl_inactivo = '0'
		union all
		select 3 as nid_tabla,nid_marca as [nid_padre],nid_modelo as [nid_hijo],no_modelo as [no_nombre] from mae_modelo
		where fl_inactivo = '0'
	) tmp where nid_tabla = @nid_tabla
	order by no_nombre
end
go

ALTER PROCEDURE SRC_SPS_TIPO_USUARIO_BO
/*****************************************************************************
OBJETIVO		: LISTA LOS TIPOS DE USUARIO
****************************************************************************
EXEC [DBO].[SRC_SPS_MAE_TALLERES_MAE_DISTRITO_BO]
****************************************************************************/
AS
BEGIN
	SELECT NO_VALOR2 AS 'ID', NO_VALOR1 AS 'DES' FROM V_LISTADO_TIPO_USUARIO_BO
END
GO

alter procedure sgsnet_sps_destino_x_id
/*****************************************************************************  
Objetivo  : Permite visualizar los campos   
exec sgsnet_sps_destino_x_id 22
****************************************************************************/  
(  
@vi_in_id_ubica int  
)  
as  
begin  
  
	select 
	u.nid_ubica AS id_ubicacion  
	,LTRIM(RTRIM(u.tip_ubica)) AS tipo_ubicacion  
	,LTRIM(RTRIM(u.di_ubica)) AS direccion  
	,u.no_ubica AS nom_ubicacion  
	,u.no_ubica_corto AS nom_corto_ubicacion  
	,u.nu_ruc AS nro_ruc  
	,udpto.nombre AS nom_dpto  
	,u.coddpto AS cod_dpto  
	,uprov.nombre AS nom_provincia  
	,u.codprov AS cod_provincia  
	,u.coddist AS cod_distrito  
	,udist.nombre AS nom_distrito  
	,u.fe_crea as fec_creacion  
	,rtrim(rtrim(u.co_usuario_crea)) as cod_usu_crea  
	,(CASE WHEN u.fl_inactivo = '0' THEN 'Activo' ELSE 'Inactivo' END ) as estado  
	,u.fl_inactivo as cod_estado  
	from mae_ubicacion u  
	INNER JOIN mae_tipo_ubicacion tu ON u.tip_ubica = tu.tip_ubica 
	left JOIN mae_ubigeo udpto ON u.coddpto = udpto.coddpto and udpto.codprov = '00' and udpto.coddist = '00'  
	LEFT JOIN mae_ubigeo uprov ON u.codprov = uprov.codprov and uprov.coddpto = u.coddpto AND uprov.coddist = '00'  
	LEFT JOIN mae_ubigeo udist ON u.coddist = udist.coddist and udist.coddpto = u.coddpto and udist.codprov = u.codprov  
	where u.nid_ubica = @vi_in_id_ubica  
  
end;  
go

ALTER procedure sgsnet_sps_modelo_x_id
/*****************************************
EXEC  sgsnet_sps_modelo_x_id 41
*****************************************/
(        
@vi_in_id_modelo INT
)        
as        
begin        
        
 set nocount ON        
        
 select mo.nid_modelo as id_modelo        
  ,LTRIM(RTRIM(mo.co_modelo)) AS cod_modelo        
  ,LTRIM(RTRIM(mo.no_modelo)) AS nom_modelo        
  ,m.nid_marca AS nid_marca        
  ,LTRIM(RTRIM(m.no_marca)) AS nom_marca        
  ,mo.nid_negocio_linea as cod_linea_importacion        
  ,mo.nid_negocio_subfamilia as cod_linea_comercial
  ,mnl.co_negocio as cod_negocio        
  ,mnl.co_negocio as no_familia
  ,mo.fl_inactivo as cod_estado
 from mae_modelo mo      
  INNER JOIN mae_marca m ON m.nid_marca = mo.nid_marca      
  left join mae_negocio_linea mnl on mo.nid_negocio_linea = mnl.nid_negocio_linea      
  left join mae_familia_vehiculo fam on fam.co_familia = mnl.co_familia      
  left join mae_familia_vehiculo neg on neg.co_familia=fam.co_familia and neg.fl_inactivo='0'
 where mo.nid_modelo = @vi_in_id_modelo      
        
end
go

alter procedure sgsnet_sps_bandeja_modelo
/*****************************************************************************      
Objetivo  : Bandeja de Modelo
****************************************************************************
exec sgsnet_sps_bandeja_modelo 12,'','',''
****************************************************************************/
(      
@vi_in_id_marca INT,      
@vi_ch_cod_modelo VARCHAR(20),      
@vi_va_nom_modelo varchar(50),       
@vi_ch_cod_estado CHAR(1)
)      
as      
begin
      
set @vi_ch_cod_modelo = isnull(ltrim(rtrim(@vi_ch_cod_modelo)),'')      
set @vi_va_nom_modelo = isnull(ltrim(rtrim(@vi_va_nom_modelo)),'')      
set @vi_ch_cod_estado = isnull(@vi_ch_cod_estado,'')      
      
      
 select 
 mo.nid_modelo as id_modelo      
  ,LTRIM(RTRIM(mo.co_modelo)) AS cod_modelo      
  ,LTRIM(RTRIM(mo.no_modelo)) AS nom_modelo       
  ,LTRIM(RTRIM(ma.no_marca)) AS nom_marca
  ,LTRIM(RTRIM(ng.no_negocio)) AS no_familia
  ,fv1.no_familia as nom_linea_importacion      
  ,fv2.no_familia as nom_linea_comercial      
  ,(CASE       
    WHEN mo.fl_inactivo = '0' THEN 'Activo'       
    WHEN mo.fl_inactivo = '1' THEN 'Inactivo' END ) as estado      
  ,mo.fl_inactivo as cod_estado
	from mae_modelo mo      
	inner join mae_marca ma ON ma.nid_marca = mo.nid_marca      
	left join mae_negocio_linea mnl       
	left join mae_familia_vehiculo fv1 on mnl.co_familia = fv1.co_familia on mo.nid_negocio_linea = mnl.nid_negocio_linea      
	left join mae_negocio_subfamilia sub on sub.nid_negocio_subfamilia = mo.nid_negocio_subfamilia      
	left join mae_familia_vehiculo fv2 on fv2.co_familia = sub.co_familia      
	left join mae_negocio ng ON ng.co_negocio = mnl.co_negocio
	where 
		mo.nid_marca =  @vi_in_id_marca       
	and ( @vi_ch_cod_modelo = '' or rtrim(ltrim(mo.co_modelo)) like '%' + @vi_ch_cod_modelo + '%' )      
	and ( @vi_va_nom_modelo = '' or rtrim(ltrim(mo.no_modelo)) like '%' + @vi_va_nom_modelo + '%' )      
	and ( @vi_ch_cod_estado = '' or mo.fl_inactivo =  @vi_ch_cod_estado )      

end 
go