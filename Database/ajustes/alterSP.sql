ALTER PROCEDURE [DBO].[SRC_SPS_DETALLEPUNTORED_BO]
/******************************************************************************
OBJETIVO		: DATOS DE PUNTO DE RED POR CODIGO
*******************************************************************************
DEMO:
EXEC [DBO].[SRC_SPS_DETALLEPUNTORED_BO] 31
******************************************************************************/
(
@VI_NID_UBICA INT
)
AS
BEGIN
	SELECT DI_UBICA
	FROM MAE_UBICACION
	WHERE NID_UBICA = @VI_NID_UBICA
END
go
 
ALTER PROCEDURE [SRC_SPI_MAE_TALLER_BO]
/******************************************************************************      
OBJETIVO  : GRABAR DATOS DE TALLER      
AUTOR   : GUIDO MATOS CAMONES - GMATOS      
FECHA CREACIÓN : 01/12/2010      
HISTORIAL  :       
*******************************************************************************  
declare @p13 int
set @p13=NULL
exec [src_spi_mae_taller_BO] @vi_co_taller=N'TD1',@vi_no_taller=N'TALLER DEMO 1',@vi_descripcion=N'DEMO 1',@vi_co_intervalo_atenc=998,@vi_nid_ubica=0,@vi_no_direccion=N'',@vi_tx_mapa_taller=N'',@vi_tx_url_taller=N'',@vi_fl_activo=N'',@vi_co_usuario_crea=N'admincitas',@vi_co_usuario_red=N'',@vi_no_estacion_red=N'MW-DESKTOP-KN7714R',@vo_nid_taller=@p13 output
select @p13

select*from mae_taller
******************************************************************************/      
(      
    @VI_CO_TALLER   VARCHAR(20)      
   ,@VI_NO_TALLER   VARCHAR(50)
   ,@vi_descripcion VARCHAR(500)
   ,@VI_CO_INTERVALO_ATENC INT      
   ,@VI_NID_UBICA   INT      
   ,@VI_NO_DIRECCION  VARCHAR(150)      
   ,@VI_TX_URL_TALLER  VARCHAR(255)=NULL
   ,@VI_TX_MAPA_TALLER  VARCHAR(255)            
   ,@VI_FL_ACTIVO   CHAR(1)
   ,@VI_CO_USUARIO_CREA  VARCHAR(20)=NULL      
   ,@VI_CO_USUARIO_RED     VARCHAR(20)=NULL
   ,@VI_NO_ESTACION_RED  VARCHAR(20)=NULL
   ,@VO_NID_TALLER   INT OUTPUT
)      
AS      
BEGIN TRY      

 DECLARE @VL_NU_ERROR INT

	INSERT INTO MAE_TALLER
	(      
	 CO_TALLER      
    ,NO_TALLER      
	,descripcion
    ,CO_INTERVALO_ATENC      
    ,NID_UBICA      
    ,NO_DIRECCION 
    ,TX_MAPA_TALLER    
    ,tx_url_taller    
    ,FE_CREA      
    ,CO_USUARIO_CREA      
    ,CO_USUARIO_RED      
    ,NO_ESTACION_RED      
    ,FL_ACTIVO
    )      
	VALUES
	(  
	@VI_CO_TALLER      
    ,@VI_NO_TALLER      
	,@vi_descripcion
    ,@VI_CO_INTERVALO_ATENC      
    ,@VI_NID_UBICA      
    ,@VI_NO_DIRECCION
    ,@VI_TX_MAPA_TALLER     
    ,@VI_TX_URL_TALLER    
    ,GETDATE()      
    ,@VI_CO_USUARIO_CREA      
    ,@VI_CO_USUARIO_RED      
    ,@VI_NO_ESTACION_RED      
    ,@VI_FL_ACTIVO
    )
   
   SET @VO_NID_TALLER = SCOPE_IDENTITY();
         
END TRY      
BEGIN CATCH      
    
 SET @VL_NU_ERROR = ERROR_NUMBER()      
       
END CATCH   
go


ALTER PROCEDURE [DBO].[SRC_SPS_LISTAR_UBIGEOS_BO]              
/***************************************************************************                
OBJETIVO  : LISTAR LOS UBIGEOS (DPTO, PROV, DIST) DISPONIBLES
TEST:
****************************************************************************                
exec SRC_SPS_LISTAR_UBIGEOS_BO @vi_nid_Servicio=137,@vi_nid_modelo=413,@vi_Nid_usuario=2438
exec [SRC_SPS_LISTAR_DATOS_SERVICIOS_BO] @VI_NID_SERVICIO=N'138'
select*from usr where nid_usuario = 2438
select*from mae_servicio_especifico where nid_servicio = 137
select*from mae_marca where nid_marca = 31
select*from mae_modelo where nid_modelo = 413

select nid_usuario,nid_ubica,*from usr where CUSR_ID IN ('asesorserv122','asesorserv124')
select coddpto, codprov, coddist,*from mae_ubicacion where nid_ubica = 1826
select*from mae_ubigeo where coddpto = '15' and codprov = '01' and coddist = '55'
select *from mae_taller where nid_ubica = 1826

select*from mae_usr_taller where nid_taller IN (93, 95)
select*from usr where nid_usuario = 9356
select*from usr where nid_usuario = 9418

select
p.co_perfil_usuario
from usr u
inner join prfusr pu on pu.nid_usuario = u.nid_usuario
inner join prf p on p.nid_perfil = pu.nid_perfil
where
u.cusr_id = 'admincitas'
****************************************************************************                
****************************************************************************/                
(                
 @vi_nid_servicio int,                
 @vi_nid_modelo   int,            
 @vi_nid_usuario  int        
)                
as                
begin                
        
	declare @vl_nid_perfil int;
	declare @vl_co_perfil varchar(4);

	select top 1 @vl_nid_perfil = nid_perfil from prfusr where nid_usuario = @vi_nid_usuario and fl_inactivo= '0'
	select @vl_co_perfil = co_perfil_usuario from prf where nid_perfil = @vl_nid_perfil

	select
	distinct
	ub.coddpto,ub.codprov,ub.coddist,
	udpto.nombre as nomdpto,
	uprov.nombre as nomprov,
	udist.nombre as nomdist
	from             
	mae_ubicacion ub             
	inner join mae_ubigeo u              on (ub.coddpto = u.coddpto and ub.codprov = u.codprov and ub.coddist = u.coddist)                
	inner join mae_taller t              on (t.nid_ubica  = ub.nid_ubica  and t.fl_activo   = 'A')                
	inner join mae_taller_modelo tm      on (t.nid_taller = tm.nid_taller and tm.nid_modelo =  @vi_nid_modelo and tm.fl_activo = 'A')                
	inner join mae_taller_servicio ts    on (t.nid_taller = ts.nid_taller and ts.fl_activo  = 'A')                
	inner join mae_servicio_especifico s on (ts.nid_servicio  = s.nid_servicio and s.nid_servicio = @vi_nid_servicio  and s.fl_activo = 'A')       
	inner join mae_usr_taller tsu        on (t.nid_taller     = tsu.nid_taller and tsu.nid_usuario = (case @vl_co_perfil when 'AGEN' then tsu.nid_usuario else @vi_nid_usuario end ) and tsu.fl_activo = 'A')                  
	inner join mae_usr_taller tu         on (tu.nid_taller    = t.nid_taller and tu.fl_activo = 'A')      
	inner join USR usu                   on (usu.nid_usuario  = tu.nid_usuario   and usu.fl_inactivo = '0')

	inner join mae_usr_modelo tum        on (tum.nid_modelo   = @vi_nid_modelo   and tum.nid_usuario = usu.nid_usuario  and tm.fl_activo  = 'A')                 
	inner join mae_usr_servicio tus      on (tus.nid_servicio = @vi_nid_servicio and tus.nid_usuario = usu.nid_usuario  and tus.fl_activo = 'A')                   
	inner join mae_ubigeo udpto          on (udpto.coddpto = ub.coddpto and udpto.codprov = '00')        
	inner join mae_ubigeo uprov          on (uprov.coddpto = ub.coddpto and uprov.codprov = ub.codprov and uprov.coddist = '00')              
	inner join mae_ubigeo udist          on (udist.coddpto = ub.coddpto and udist.codprov = ub.codprov and udist.coddist = ub.coddist)          
	where           
	ub.fl_inactivo = '0'
     
end    
go

alter procedure [dbo].[sgsnet_spi_marca]  
/*****************************************************************************  
Objetivo  : Registro de Marca
****************************************************************************/  
(  
@vi_va_cod_marca varchar(20),
@vi_va_nom_marca varchar(50),
@vi_in_id_empresa int,
@vi_ch_cod_estado char(1),  
@vi_va_cod_usuario varchar(20),  
@vi_va_nom_estacion varchar(20),  
@vi_va_nom_usuario_red varchar(20)
)  
as  
begin  
 set nocount on  
  
 declare @v_in_retorno int  
 declare @v_nro_error int  
 declare @v_in_id int  
  
 set @vi_va_cod_marca = UPPER(ltrim(rtrim(@vi_va_cod_marca)))  
 set @vi_va_nom_marca = ltrim(rtrim(@vi_va_nom_marca))  
 set @vi_va_cod_usuario = ltrim(rtrim(@vi_va_cod_usuario))  
 set @vi_va_nom_estacion = ltrim(rtrim(@vi_va_nom_estacion))  
 set @vi_va_nom_usuario_red = ltrim(rtrim(@vi_va_nom_usuario_red))  
  
  
 BEGIN TRY  
  
  insert into mae_marca
  (   
        co_marca  
        ,no_marca  
        ,nid_empresa  
        ,fe_crea  
        ,co_usuario_crea  
        ,fl_inactivo  
        ,no_estacion_red  
        ,no_usuario_red  
        )
      values 
	  (  
        @vi_va_cod_marca  
        ,@vi_va_nom_marca
        ,@vi_in_id_empresa  
        ,getdate()  
        ,@vi_va_cod_usuario  
        ,@vi_ch_cod_estado   
        ,@vi_va_nom_estacion  
        ,@vi_va_nom_usuario_red  
        )  
  set @v_in_id = @@identity   
  
  SELECT @v_in_id AS vo_va_retval   
      
 END TRY  
  
 BEGIN CATCH  
 begin  
   --select ERROR_MESSAGE(), ERROR_NUMBER()  
   -- 2627 ERROR DE UNIQUE  
   -- 515 ERROR DE INSERTAR VALOR NULL NO PERMITIDO  
   -- 547 ERROR CONFLICTO DE LLAVE FORANEA  
   set @v_nro_error = ERROR_NUMBER()  
   if @v_nro_error = 2627   
    set @v_in_retorno = -2
   else   
    if @v_nro_error = 515   
    set @v_in_retorno = -3
   else   
    if @v_nro_error = 547   
    set @v_in_retorno = -4
  
   else  
    set @v_in_retorno = -1
     
   SELECT @v_in_retorno AS vo_va_retval  
   
 end   
 END CATCH  
end;  
go

alter procedure [dbo].[sgsnet_spu_marca]
/*****************************************************************************
Objetivo		: Actualizar marca
****************************************************************************/
(
@vi_int_id_marca INT,
@vi_va_cod_marca varchar(20),
@vi_va_nom_marca varchar(50),
@vi_in_id_empresa int,
@vi_int_cod_estado CHAR(1),
@vi_va_cod_usuario varchar(20),
@vi_va_nom_estacion varchar(20),
@vi_va_nom_usuario_red varchar(20)
)
as
begin
	set nocount on

	declare @v_in_retorno int
	declare @v_nro_error int
	declare @v_in_existe int

	set @vi_va_cod_marca = UPPER(ltrim(rtrim(@vi_va_cod_marca)))
	set @vi_va_nom_marca = ltrim(rtrim(@vi_va_nom_marca))
	set @vi_int_cod_estado = @vi_int_cod_estado
	set @vi_va_nom_estacion = ltrim(rtrim(@vi_va_nom_estacion))
	set @vi_va_nom_usuario_red = ltrim(rtrim(@vi_va_nom_usuario_red))

	BEGIN TRY

	update mae_marca
	set 
	 co_marca = @vi_va_cod_marca
	,no_marca = @vi_va_nom_marca
	,nid_empresa = @vi_in_id_empresa
	,fe_cambio = GETDATE()
	,co_usuario_cambio = @vi_va_cod_usuario
	,fl_inactivo = @vi_int_cod_estado
	,no_estacion_red = @vi_va_nom_estacion
	,no_usuario_red = @vi_va_nom_usuario_red
	where nid_marca = @vi_int_id_marca

	SELECT @vi_int_id_marca AS vo_va_retval 
		
	END TRY
	BEGIN CATCH
	begin
		--select ERROR_MESSAGE(), ERROR_NUMBER()
		-- 2627 ERROR DE UNIQUE
		-- 515 ERROR DE INSERTAR VALOR NULL NO PERMITIDO
		-- 547 ERROR CONFLICTO DE LLAVE FORANEA
		set @v_nro_error = ERROR_NUMBER()
		if @v_nro_error = 2627 
			set @v_in_retorno = -2
		else 
			if @v_nro_error = 515 
			set @v_in_retorno = -3
		else 
			if @v_nro_error = 547 
			set @v_in_retorno = -4

		else
			set @v_in_retorno = -1
			
		SELECT @v_in_retorno AS vo_va_retval
	end	
	END CATCH
end;
go


alter procedure [dbo].[sgsnet_sps_marca_x_id]
/*****************************************************************************
Nombre			: sgsnet_sps_marca_x_id
Objetivo		: Obtener Marca
****************************************************************************/
(
@vi_in_id_marca int
)
as

begin
	set nocount on

	select 
	m.nid_marca as id_marca
	,ltrim(rtrim(m.co_marca)) as cod_marca
	,ltrim(rtrim(m.no_marca)) as nom_marca
	,emp.nid_empresa as id_empresa
	,isnull(emp.nu_ruc,'0') as nu_ruc
	,m.fe_crea as fec_creacion
	,rtrim(rtrim(m.co_usuario_crea)) as cod_usu_crea
	,m.fl_inactivo as cod_estado
	,m.fe_cambio as fec_modi
	,m.co_usuario_cambio as cod_usu_modi
	,ltrim(rtrim(m.no_estacion_red)) as nom_estacion
	,ltrim(rtrim(m.no_usuario_red)) as nom_usuario_red
	from mae_marca m
	inner join mae_empresa emp on m.nid_empresa = emp.nid_empresa
	where m.nid_marca = @vi_in_id_marca
end;
go


ALTER PROCEDURE src_sps_vehiculo_por_cliente_web
/*****************************************************************************
Nombre: src_sps_vehiculo_por_cliente_web
Objetivo: Listar Datos de Vehiculo del Cliente Web
Nota:
****************************************************************************
exec dbo.src_sps_vehiculo_por_cliente_web 1509561
select*from mae_cliente where nu_documento = '46124933'
select*from mae_vehiculo where nu_placa = 'YAR789'
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
	INNER JOIN mae_cliente cli on cli.nid_cliente = veh.nid_propietario
	WHERE
		veh.fl_activo = 'A'
	and (veh.nid_propietario = @vi_nid_cliente)

END
go

ALTER PROCEDURE SRC_SPI_RESERVAR_CITA_WEB_FO
/*****************************************************************************                                                                  
OBJETIVO: RESERVAR UNA CITA
******************************************************************************/
(                                                             
------------------------------------                                                            
-- mae_cliente                                                          
------------------------------------                                                            
 @vi_nid_contacto int,
 @vi_nid_vehiculo int,
-----------------------------------
-- tbl_cita              
-----------------------------------                                                            
 @vi_nid_taller int,                                                                  
 @vi_nid_usuario int,                                                                  
 @vi_nid_servicio int,                                                                  
 @vi_nu_placa varchar(50),
 @vi_nid_marca int,                
 @vi_nid_modelo int,                
 @vi_fe_programada datetime,
 @vi_ho_inicio varchar(5),
 @vi_ho_fin char(5),
 @vi_tx_observacion text,
-----------------------------------                                                            
-- mae_horario_excepcional                                      
-----------------------------------                                       
@vi_dd_atencion smallint
)                                                                  
as                                                                  
begin try                                                   
                                                                  
    set dateformat dmy                                        
    set language spanish  
  
	DECLARE @val_retorno INT, @val_msg_retorno VARCHAR(MAX)
	set @vi_nu_placa = LTRIM(RTRIM(ISNULL(@vi_nu_placa,'')));  
	set @vi_ho_inicio = LTRIM(RTRIM(ISNULL(@vi_ho_inicio,'')));  
	set @vi_ho_fin = LTRIM(RTRIM(ISNULL(@vi_ho_fin,'')));  
       
	 --declaración de variables                                                                  
	 declare @vl_id_cita int                                                      
	 declare @vl_nid_estado int                                                                  
	 declare @vl_cod_estado varchar(5)                                                                  
	 declare @vo_id_usuario int                                                                  
	 declare @vl_nu_error int, @vl_fl_transaccion char(1);                                                              
	----------------------------------------------------------------------------                                                                
 
	 declare @vl_cod_norm    varchar(20) --> perxxx                                                                  
	 declare @vl_cod_fecha   varchar(20) --> yyyymmddhhmm                                                                  
	 declare @vl_cod_corel   varchar(20) --> 000001 (correlativo)   =>                                                               
	 declare @vl_cod_reserva varchar(50) --> @vl_cod_norm + @vl_cod_fecha + @vl_cod_corel  =>                      
                                                                  
	 declare @vl_cod_max varchar(50)                                                                  
	 declare @vl_marca   varchar(100)                                       
                                    
	--------------------------------------                                    
	-- mae_capacidad_atencion                                    
	-------------------------------                                    
	declare @vl_capacidad_t int            
	declare @vl_capacidad_u int                             
	declare @vl_capacidad_m int
            
	select @vl_capacidad_t = (case tca.fl_control  when 'T' then tca.qt_capacidad else qt_capacidad_fo end)  
   from mae_capacidad_atencion tca where nid_propietario = @vi_nid_taller  
   and fl_tipo = 'T' and dd_atencion = @vi_dd_atencion and fl_activo = 'A'  
                                    
	select @vl_capacidad_u = qt_capacidad_fo from mae_capacidad_atencion  
      where nid_propietario = @vi_nid_usuario  
      and fl_tipo = 'A' and dd_atencion = @vi_dd_atencion and fl_activo = 'A'  
          
	select @vl_capacidad_m = qt_capacidad from mae_taller_modelo_capacidad ttmc  where ttmc.nid_taller = @vi_nid_taller  
      and ttmc.nid_modelo = @vi_nid_modelo and ttmc.dd_atencion = @vi_dd_atencion and ttmc.fl_activo = 'A'  

	--------------------------------                                                          
	-- verificamos si existe capacidad de atención                                        
	-----------------------------------                                                          
	declare @vl_res varchar(5)                         
	set @vl_res = 'OK'                                          
	----------------------------------                                     
          
	if  ((select fl_valor3 from mae_parametros where nid_parametro = 8)= '0') and (          
	select  COUNT(tc.nid_cita)          
	 from tbl_cita tc                                    
	 inner join mae_cliente tct            on(tc.nid_contacto_src = tct.nid_cliente  and tct.fl_inactivo='0')              
	 inner join tbl_estado_cita te         on(tc.nid_cita         = te.nid_cita      and te.fl_activo   ='A')                           
	 inner join mae_tabla_detalle td       on(td.nid_tabla_gen_det= te.co_estado_cita  and td.fl_inactivo ='0')           
	 where LTRIM(RTRIM(REPLACE(tc.nu_placa,'-',''))) = @vi_nu_placa                  
	 and tc.fl_activo = 'A'                    
	 and td.no_valor3 in ('EC01','EC04','EC07') ) > 0   
	begin       -- placa con cita pendientes                          
	  set @vl_res = 'C0'           
	end            
	-------------------------------------------          
	else if @vl_capacidad_t is not null and (                              
	select  @vl_capacidad_t - count(tc.nid_cita)                                    
	from tbl_cita tc                                    
	inner join tbl_estado_cita   te on ( te.nid_cita = tc.nid_cita     and te.fl_activo   = 'A')                                    
	inner join mae_tabla_detalle td on ( te.co_estado_cita = td.nid_tabla_gen_det and td.fl_inactivo = '0'                                    
	   and td.no_valor3 in ('EC01','EC04') )                                    
	where tc.nid_taller  = @vi_nid_taller                                    
	and   tc.fe_programada = @vi_fe_programada                                    
	and   tc.fl_origen  = 'F'                                    
	and   tc.fl_activo  = 'A'                                    
	) <= 0                                    
	begin       -- el taller ya alcanzo su limite de capacidad                                    
	  set @vl_res = 'C4'                                               
	end           
	-------------    /*013*/          
	else if @vl_capacidad_m is not null and  (            
	select  @vl_capacidad_m - count(tc.nid_cita)          
	from tbl_cita tc           
	inner join tbl_estado_cita   te  on te.nid_cita = tc.nid_cita  and  te.fl_activo   = 'A'             
	inner join mae_tabla_detalle tdc on tdc.nid_tabla_gen_det=te.co_estado_cita  and tdc.fl_inactivo = '0'           
	   and tdc.no_valor3 in ('EC01','EC04','EC06')          
	where tc.nid_taller    = @vi_nid_taller             
	and   tc.nid_modelo    = @vi_nid_modelo                                   
	and   tc.fe_programada = @vi_fe_programada          
	/*and   tc.fl_origen  = 'F'*/          
	and   tc.fl_activo  = 'A'             
	) <= 0                                 
	begin       -- capacidad del taller respecto al modelo alcanzo su limite                       
	  set @vl_res = 'C6'                                               
	end           
	-------------                                    
	else if @vl_capacidad_u is not null and  (                                    
	select  @vl_capacidad_u - count(tc.nid_cita)                                    
	from tbl_cita tc                                    
	inner join tbl_estado_cita   te on ( te.nid_cita = tc.nid_cita     and te.fl_activo   = 'A')                                    
	inner join mae_tabla_detalle td on ( te.co_estado_cita = td.nid_tabla_gen_det and td.fl_inactivo = '0'                                    
	   and td.no_valor3 in ('EC01','EC04') )                                    
	where te.nid_usuario = @vi_nid_usuario                                    
	and   tc.fe_programada = @vi_fe_programada                            
	and   tc.fl_origen  = 'F'                                    
	and   tc.fl_activo  = 'A'       
	) <= 0                                    
	begin      -- el asesor ya alcanzo su limite de capacidad                                    
	  set @vl_res = 'C5'                                               
	end                                                           
	-------------------------------------------                                                                      
	else if  (                                                                                          
	select                                                 
	  count(tc.nid_cita)                                                                  
	from tbl_cita tc                                
	inner join tbl_estado_cita te   on (tc.nid_cita  = te.nid_cita  and te.fl_activo = 'A'  and te.nid_usuario = @vi_nid_usuario)                                
	inner join mae_tabla_detalle td on (te.co_estado_cita =  td.nid_tabla_gen_det and td.fl_inactivo = '0' )                                
	where td.no_valor3  in ('EC01','EC04','EC07')                                
	and tc.nid_taller = @vi_nid_taller                                  
	and tc.nu_placa  = @vi_nu_placa                                   
	and tc.nid_servicio = @vi_nid_servicio                                   
	and tc.fe_programada= @vi_fe_programada                                  
	and tc.ho_inicio = @vi_ho_inicio                                 
	and tc.fl_activo = 'A'                                                         
	) > 1                                                                              
	begin       -- ya se ha registrado este vehiculo con estos mismo datos                                                
	  set @vl_res = 'C1'                                               
	end                                                               
	else if (                                                                                                                          
	select                                                             
	  count(tc.nid_cita)                                                                  
	from tbl_cita tc                                
	inner join tbl_estado_cita te   on (tc.nid_cita  = te.nid_cita  and te.fl_activo = 'A'  and te.nid_usuario = @vi_nid_usuario)                                
	inner join mae_tabla_detalle td on (te.co_estado_cita =  td.nid_tabla_gen_det and td.fl_inactivo = '0' )                                
	where td.no_valor3  in ('EC01','EC04','EC07')                                
	and tc.nid_taller = @vi_nid_taller                                  
	and tc.fe_programada= @vi_fe_programada                           
	and tc.ho_inicio = @vi_ho_inicio                                 
	and tc.fl_activo = 'A'        
	) >1                                                                              
	begin  -- ya se ha registrado un vehiculo en este horario                      
	  set @vl_res = 'C2'                                                
	end                                              
	else if (      
	select                                 
	  count(tc.nid_cita)                                  
	from tbl_cita tc                                
	inner join tbl_estado_cita te   on (tc.nid_cita  = te.nid_cita  and te.fl_activo = 'A' )                                
	inner join mae_tabla_detalle td on (te.co_estado_cita =  td.nid_tabla_gen_det and td.fl_inactivo = '0' )                                
	where td.no_valor3 in ('EC01','EC04','EC07')                                
	and tc.nu_placa     = @vi_nu_placa                                  
	and tc.fe_programada= @vi_fe_programada                                  
	and tc.ho_inicio    = @vi_ho_inicio                                 
	and tc.fl_activo    = 'A'                                                      
	)>1           
	begin  --  este vehiculo ya ha reservado una cita en esta misma hora                                              
	 set @vl_res = 'C3'                                                
	end                                              
	else                                                                    
	  begin                                 
	 set @vl_res = 'OK' 
	  end                                              
	---------------------                                                           
	if @vl_res <> 'OK'                                                              
	begin                                                              
	 select @vl_res as vo_resultado                                               
	 return                                                            
	end

	---------------------------------------------                                                            
	-- insertar registro de cita                       
	---------------------------------------------                                                   
	-- GENERAMOS UN CODIGO DE RESERVA
	----------------------------------------------------------                                  
                    
	set @vl_marca = (select UPPER(no_marca) from mae_marca where nid_marca = @vi_nid_marca)                                                          
	set @vl_marca = left(@vl_marca,3)
	set @vl_cod_norm  = 'PE' + @vl_marca                                                           
                                                        
	set @vl_cod_fecha = datename(yyyy,getdate()) +                                        
     right('00'+ cast(month(getdate()) as varchar(2)),2) +                                                          
     right('00'+ cast(day(getdate()) as varchar(2)),2)   +                                                           
     right('00'+ datename(hh,getdate()),2) +                                                           
     right('00'+ datename(minute,getdate()),2)                                                          
                                                                                                
	set @vl_cod_max = (select max(right(cod_reserva_cita,6)) from tbl_cita              
	where  substring(cod_reserva_cita,6,8) = left(@vl_cod_fecha,8))                          
                                                          
	set @vl_cod_corel = right( '000000' + cast(isnull(@vl_cod_max,'000000') + 1  as varchar(6)),6 )                 
                                                      
	---------------------------------------------------------------------                                         
	set @vl_cod_reserva = @vl_cod_norm + @vl_cod_fecha + @vl_cod_corel              
	---------------------------------------------------------------------                                                                  
                                                                  
	--Verificamos si está activado el parámetro de Confirmación Automática

	if (select fl_valor3 from mae_parametros where nid_parametro = 12) = '1'                                            
	begin                                                              
		set @vl_cod_estado = (select nid_tabla_gen_det from mae_tabla_detalle where no_valor3 = 'EC04')
	end
	else
	begin
		set dateformat dmy                          
		if( datediff(day,getdate(),@vi_fe_programada) <=1 )                          
			set @vl_cod_estado =  (select nid_tabla_gen_det from mae_tabla_detalle where no_valor3 = 'EC04')                          
		else                          
		set @vl_cod_estado =  (select nid_tabla_gen_det from mae_tabla_detalle where no_valor3 = 'EC01')                          
	end
                                                 
	------------------------------------------------------                                                

	begin transaction

	set @vl_fl_transaccion = '1';
                                                                                          
	insert into tbl_cita
      (
	  [nid_contacto_src]                                                                  
      ,[nid_taller]                                                                  
      ,[nid_servicio]                                   
      ,[nid_vehiculo]                                                                  
      ,[cod_reserva_cita]                                                                  
      ,[nu_placa]
      ,[nid_marca]                                                                  
      ,[nid_modelo]                                      
      ,[fe_programada]                                                                  
      ,[fl_origen]                                                                  
      ,[ho_inicio]                                                          
      ,[ho_fin]                                                                  
      ,[fl_datos_pendientes]      
      ,[tx_observacion]                                                    
      ,[fl_reprog]                                                                  
      ,[fe_crea]                                                                  
      ,[fl_activo]        
      ,[fl_taxi]
      ,[co_origencita]
	  ,[co_tipo_cita]
   )   
   values                                                                  
   (   @vi_nid_contacto                                                              
      ,@vi_nid_taller                                                                  
      ,@vi_nid_servicio                                                                  
      ,@vi_nid_vehiculo                                                                    
      ,@vl_cod_reserva                                                                  
      ,@vi_nu_placa
      ,@vi_nid_marca                                         
      ,@vi_nid_modelo                                                                  
      ,@vi_fe_programada                                                                  
      ,'F'                                                                  
      ,@vi_ho_inicio                                        
      ,@vi_ho_fin                                                                  
      ,'0'                        
      ,@vi_tx_observacion                                            
      ,'0'                                        
      ,getdate()                                                                  
      ,'A'           
      ,NULL
	  ,'0'
	  ,'001'
  )                                                          
                                   
	set @vl_id_cita = @@identity
                                                                   
	insert into tbl_estado_cita
	(
	[nid_cita]                                                                  
    ,[nid_usuario]                                     
    ,[co_estado_cita]                                                     
    ,[fe_crea]                                                                  
    ,[fl_activo]
	)
	values                                                                  
    (                                                                  
     @vl_id_cita                                                    
    ,@vi_nid_usuario                                     
    ,@vl_cod_estado                                                                  
    ,getdate()                                                                  
    ,'A'                                         
	)
                                                           
	set @vl_nid_estado = @@identity
                                                           
	insert into tbl_reprog_cita
	(
	[nid_cita]                                                                  
    ,[fe_reprogramacion]                                    
    ,[ho_inicio]      
    ,[ho_fin]                                                                  
    ,[tx_observacion]                                                              
    ,[fe_crea]                                                              
    ,[fl_activo]
	)
	values                                                              
	(
	@vl_id_cita                                                                  
    ,@vi_fe_programada                                                                  
    ,@vi_ho_inicio                                                                  
    ,@vi_ho_fin                                                       
    ,@vi_tx_observacion                                                              
    ,getdate()                                              
    ,'A'                                                                  
	)
                                                       
	set @vl_nid_estado = @@identity                                                        
                                                         
	---------------------------------------                                                            
	---- verificacion de capacidad de atencion
	--------------------------------------                                      
                                    
	set @vl_capacidad_t = (select qt_capacidad_fo from mae_capacidad_atencion                                     
      where nid_propietario = @vi_nid_taller                    
      and fl_tipo = 'T' and dd_atencion = @vi_dd_atencion and fl_activo = 'A')                                   
                                    
	set @vl_capacidad_u = (select qt_capacidad_fo from mae_capacidad_atencion                                     
      where nid_propietario = @vi_nid_usuario                                     
      and fl_tipo = 'A' and dd_atencion = @vi_dd_atencion and fl_activo = 'A')                                  
                                                                  
	----------------------------------                                         
 
	if @vl_capacidad_t is not null and  (                                     
	select  @vl_capacidad_t - count(tc.nid_cita)                                    
	from tbl_cita tc                                    
	inner join tbl_estado_cita   te on ( te.nid_cita = tc.nid_cita     and te.fl_activo   = 'A')                                    
	inner join mae_tabla_detalle td on ( te.co_estado_cita = td.nid_tabla_gen_det and td.fl_inactivo = '0'                                    
			  and td.no_valor3 in ('EC01','EC04') )                                    
	where tc.nid_taller  = @vi_nid_taller                                    
	and   tc.fe_programada = @vi_fe_programada                                 
	and   tc.fl_origen  = 'F'                                    
	and   tc.fl_activo  = 'A'                                    
	) < 0                                    
	begin       -- el taller ya alcanzo su limite de capacidad                                    
	  set @vl_res = 'C4'                                               
	end              
	-------------   /*013*/          
	else if @vl_capacidad_m is not null and  (            
	select  @vl_capacidad_m - count(tc.nid_cita)          
	from tbl_cita tc           
	inner join tbl_estado_cita   te  on te.nid_cita = tc.nid_cita  and  te.fl_activo   = 'A'             
	inner join mae_tabla_detalle tdc on tdc.nid_tabla_gen_det=te.co_estado_cita  and tdc.fl_inactivo = '0'           
	   and tdc.no_valor3 in ('EC01','EC04','EC06')          
	where tc.nid_taller    = @vi_nid_taller  
	and   tc.nid_modelo    = @vi_nid_modelo                                   
	and   tc.fe_programada = @vi_fe_programada              
	/*and   tc.fl_origen  = 'F'*/          
	and   tc.fl_activo  = 'A'         
	) < 0                                    
	begin       -- capacidad del taller respecto al modelo alcanzo su limite                       
	  set @vl_res = 'C6'                                               
	end                                  
	-------------                                    
	else if @vl_capacidad_u is not null and  (                                     
	select  @vl_capacidad_u - count(tc.nid_cita)                                    
	from tbl_cita tc                                    
	inner join tbl_estado_cita   te on ( te.nid_cita = tc.nid_cita     and te.fl_activo   = 'A')                                    
	inner join mae_tabla_detalle td on ( te.co_estado_cita = td.nid_tabla_gen_det and td.fl_inactivo = '0'                                    
			  and td.no_valor3 in ('EC01','EC04') )                                    
	where te.nid_usuario = @vi_nid_usuario                                    
	and   tc.fe_programada = @vi_fe_programada                                    
	and   tc.fl_origen  = 'F'                                    
	and   tc.fl_activo  = 'A'                    ) < 0                                    
	begin       -- el asesor ya alcanzo su limite de capacidad                                    
	  set @vl_res = 'C5'                                               
	end                                       
	--------------------------------------------                                                        
	else if (                                                                                 
	select                                                                             
	  count(tc.nid_cita)                         
	from tbl_cita tc                                
	inner join tbl_estado_cita te   on (tc.nid_cita  = te.nid_cita  and te.fl_activo = 'A' and te.nid_usuario = @vi_nid_usuario   )                                
	inner join mae_tabla_detalle td on (te.co_estado_cita =  td.nid_tabla_gen_det and td.fl_inactivo = '0' )                                
	where td.no_valor3  in ('EC01','EC04','EC07')                                
	and tc.nu_placa     = @vi_nu_placa  
	and tc.nid_taller   = @vi_nid_taller                                    
	and tc.nid_servicio = @vi_nid_servicio                                    
	and tc.fe_programada= @vi_fe_programada                                  
	and tc.ho_inicio    = @vi_ho_inicio                                 
	and tc.fl_activo    = 'A'                                                          
	) > 1                                                                              
	begin  -- ya se ha registrado este vehiculo con estos mismo datos                
                                         
	 set @vl_res = 'C1'                                              
	end                                                               
	else if (                                                                                                                        
	select                                                                             
	  count(tc.nid_cita)                                                                  
	from tbl_cita tc                                
	inner join tbl_estado_cita te   on (tc.nid_cita  = te.nid_cita  and te.fl_activo = 'A' and te.nid_usuario = @vi_nid_usuario   )                                
	inner join mae_tabla_detalle td on (te.co_estado_cita =  td.nid_tabla_gen_det and td.fl_inactivo = '0' )                                
	where td.no_valor3  in ('EC01','EC04','EC07')                                
	and tc.nid_taller   = @vi_nid_taller                                    
	and tc.fe_programada= @vi_fe_programada                                  
	and tc.ho_inicio    = @vi_ho_inicio                                 
	and tc.fl_activo    = 'A'                                                         
	) >1                                                                  
	begin                                                      
	  set @vl_res = 'C2'                                  
	end                                              
	else if (                            
	select                                                                             
	  count(tc.nid_cita)                                                                  
	from tbl_cita tc                                
	inner join tbl_estado_cita te   on (tc.nid_cita  = te.nid_cita  and te.fl_activo = 'A' and te.nid_usuario = @vi_nid_usuario   )                                
	inner join mae_tabla_detalle td on (te.co_estado_cita =  td.nid_tabla_gen_det and td.fl_inactivo = '0' )                                
	where td.no_valor3  in ('EC01','EC04','EC07')                                
	and tc.nu_placa     = @vi_nu_placa                                    
	and tc.fe_programada= @vi_fe_programada                                  
	and tc.ho_inicio    = @vi_ho_inicio                                 
	and tc.fl_activo    = 'A'                                                      
	)>1                                                                          
	begin  --  este vehiculo ya ha reservado una cita en esta misma hora                                              
	 set @vl_res = 'C3'                                                
	end                                          
	else                                   
	  begin                                                                  
	 set @vl_res = 'C0'
	  end                                              
                                                               
	-------------------------------------------------                                                
	if @vl_res <> 'C0'                                                            
                                                          
	begin                                                                                        
		set @vl_fl_transaccion = '2'                          
		select @vl_res as vo_resultado                    
		rollback transaction                              
	end                                                            
	else                                                           
	begin                                                    
		select @vl_cod_reserva as vo_cod_reserva_cita                                                          
	end                                                            
	  commit transaction     
	  set @vl_fl_transaccion = '0'                                                       
                                                           
	end try                                                                  
	begin catch          
                                                                                                 
	 set @vl_nu_error = error_number();
	 if @vl_nu_error = 2627 set @vo_id_usuario = -2;
	 else if @vl_nu_error = 515 set @vo_id_usuario = -3;
	 else if @vl_nu_error = 547 set @vo_id_usuario = -4;
	 else set @vo_id_usuario = -1;
                                                         
	 if (@vl_fl_transaccion = '1')                                                       
	 begin                                                      
	 rollback transaction                                                          
	 select @vl_nu_error as vo_error                                           
	 end                                                      
	 else if (@vl_fl_transaccion = '2')       
	 begin                                                          
	 rollback transaction                                                            
		select @vl_res as vo_resultado                                                           
	 end                                                              
end catch
go

ALTER PROCEDURE SRC_SPS_LISTAR_CITA_POR_DATOS_FO
/*****************************************************************************                                  
OBJETIVO       : Consultar Cita
Nota:
exec SRC_SPS_LISTAR_CITA_POR_DATOS_FO @vi_nu_placa = 'CIX629'
****************************************************************************                                  
****************************************************************************/                       
(                    
	@vi_nid_cita int = 0,
	@vi_cod_resverva_cita varchar(30) = '',
	@vi_nu_placa varchar(20) = '',
	@vi_nid_cliente int = 0
)
as
begin

	set datefirst  1
                 
	select
	tc.nid_cita, tc.cod_reserva_cita,
	convert(varchar(10),tc.fe_programada,103) as	fe_programada,
	tc.ho_inicio ho_inicio_c,tc.ho_fin ho_fin_c,tc.fl_origen,      
	tc.fl_datos_pendientes,tc.tx_observacion,tc.qt_km_inicial,convert(varchar(10),tc.fe_atencion,103) fe_atencion,tc.tx_glosa_atencion,          
	te.nid_estado,
	tde.no_valor3 as co_estado, tde.no_valor1 as no_estado, cast(tde.no_valor2 as int) as nu_estado,
	tct.nid_cliente,tct.no_cliente,tct.no_ape_pat, tct.no_ape_mat,ltrim(rtrim(tct.co_tipo_documento)) co_tipo_documento, tct.nu_documento,      
	tct.no_correo, tct.no_correo_empresa no_correo_trabajo,tct.no_correo_contacto no_correo_alter,      
	tct.nu_telefono nu_telefono_c, tct.nu_celular nu_celular_c,      
	tc.nid_servicio,ts.no_servicio,ts.fl_quick_service,ts.nid_tipo_servicio,tts.no_tipo_servicio,              
	tt.nid_taller,tt.no_taller,tdt.no_valor3 co_intervalo,cast(tdt.no_valor1 as int) nu_intervalo,      
	tt.no_direccion no_direccion_t,tt.nu_telefono nu_telefono_t,      
	tt.tx_mapa_taller,tt.tx_url_taller, cast(th.dd_atencion as int) dd_atencion_t,th.ho_inicio ho_inicio_t,th.ho_fin ho_fin_t,              
	tu.nid_ubica,tu.no_ubica_corto,tu.coddpto,tu.codprov,tu.coddist,tubg.nombre no_distrito,      
	tc.nid_vehiculo,tc.nu_placa,tc.nid_modelo,tmd.no_modelo,tc.nid_marca,tmr.no_marca,      
	te.nid_usuario nid_asesor,(isnull(u.vnomusr,'')+' '+isnull(u.no_ape_paterno,'')+' '+isnull(u.no_ape_materno,'')) as no_asesor,      
	u.VTELEFONO as nu_telefono_a,u.VCORREO  no_correo_asesor,      
	ISNULL(tte.nid_taller_empresa,0) nid_taller_empresa,tte.no_banco,tte.nu_cuenta ,tte.no_correo_callcenter,tte.nu_callcenter    
	,tt.fl_nota       
	,tc.no_nombreqr
	from tbl_cita tc
	inner join mae_cliente tct             on tc.nid_contacto_src = tct.nid_cliente       and tct.fl_inactivo='0'      
	inner join mae_marca tmr               on tc.nid_marca        = tmr.nid_marca         and tmr.fl_inactivo='0'      
	inner join mae_modelo tmd              on tc.nid_modelo     = tmd.nid_modelo
	inner join mae_servicio_especifico ts  on tc.nid_servicio     = ts.nid_servicio       and ts.fl_activo   ='A'      
	inner join mae_taller tt               on tc.nid_taller       = tt.nid_taller         and tt.fl_activo   ='A'      
	inner join mae_tabla_detalle tdt       on tdt.nid_tabla_gen_det= tt.co_intervalo_atenc and tdt.fl_inactivo ='0'      
	inner join mae_horario th              on th.nid_propietario  = tt.nid_taller         and th.fl_tipo     ='T' and th.dd_atencion = datepart(weekday,tc.fe_programada) and th.fl_activo ='A'    
	inner join mae_tipo_servicio tts       on ts.nid_tipo_servicio= tts.nid_tipo_servicio and tts.fl_activo  ='A'      
	inner join mae_ubicacion tu            on tu.nid_ubica        = tt.nid_ubica          and tu.fl_inactivo ='0'      
	inner join mae_ubigeo  tubg            on tubg.coddpto = tu.coddpto and tubg.codprov = tu.codprov and tubg.coddist= tu.coddist      
	inner join tbl_estado_cita te          on tc.nid_cita         = te.nid_cita           and te.fl_activo   ='A'      
	inner join mae_tabla_detalle tde       on tde.nid_tabla_gen_det = te.co_estado_cita     and tde.fl_inactivo ='0'    
	inner join usr u                       on te.nid_usuario      = u.nid_usuario         and u.fl_inactivo  ='0'      
	left  join mae_taller_empresa tte      on tte.nid_taller = tt.nid_taller and tte.nid_empresa = tmr.nid_empresa and tte.fl_activo = 'A'      
	inner join mae_vehiculo v on v.nid_vehiculo = tc.nid_vehiculo
	where      
		(tc.nid_cita  = @vi_nid_cita or @vi_nid_cita = 0)  
	and (tc.cod_reserva_cita  = @vi_cod_resverva_cita or @vi_cod_resverva_cita = '')
	and (tc.nu_placa  = @vi_nu_placa or @vi_nu_placa = '')
	and (tc.nid_contacto_src = @vi_nid_cliente or @vi_nid_cliente = 0)
	and (tc.fl_activo = 'A')
	and (tde.no_valor3 != 'EC03') --No se muestran Citas Anuladas
	order by tc.fe_programada desc,tc.ho_inicio asc                    
                    
end
go