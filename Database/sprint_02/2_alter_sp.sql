use AppMiTaller
go

ALTER PROCEDURE SRC_SPS_CITAS
/*****************************************************************************                                      
OBJETIVO  : LISTADO GENERAL DE CITAS                         
*****************************************************************************                                                                                                                                    

*****************************************************************************                           
*/                                                                
(                                                                       
@cod_reserva_cita varchar(50),                                                                                  
@coddpto char(2),                                                                                  
@codprov char(2),                                                                                  
@coddist char(2),                                                                                  
@nid_ubica int,                                                                                  
@nid_taller int,                                                                                  
@asesorservicio varchar(60),                                                                                  
@estadoreserva varchar(10),                                                                                  
@indpendiente char(1),                                                                                  
@fecreg1 varchar(20),                                                
@fecreg2 varchar(20),                                                                                  
@feccita1 varchar(20),                                                                                  
@feccita2 varchar(20),                                                                                  
@horacita1 varchar(5),                                                                                  
@horacita2 varchar(5),                                                                                  
@nu_placa varchar(50),                                                                                  
@nid_marca int,                                                                                  
@nid_modelo int,                                                                                  
@co_tipo_documento char(4),                                                                                  
@nu_documento varchar(20),                                                                                  
@no_cliente varchar(50),                                                                                  
@no_apellidos varchar(100),                                                    
@nid_usuario int                                               
)                                                  

AS                                                                            

set dateformat dmy                                                  

                                                       

declare @v_id_estado varchar(10)                                        

        

--seguridad        

declare @v_co_perfil_usuario varchar(4)            

set @v_co_perfil_usuario=(select co_perfil_usuario from prf  tp inner join prfusr tpu on tpu.nid_perfil = tp.nid_perfil       

and tpu.nid_usuario = @nid_usuario and tpu.fl_inactivo = '0')      

--fin seguridad                                         

                                      

declare @v_fl_valor3 varchar(1)                                    

                                      

select @v_fl_valor3=fl_valor3 from mae_parametros where nid_parametro=10                                      

                                       

if(@estadoreserva='0')                              

 set @v_id_estado='0'                                                                

else                                                                

 set @v_id_estado=(select nid_tabla_gen_det  from mae_tabla_detalle where no_valor3 =@estadoreserva)        

        

        

print @v_id_estado                                                                

                                                

if(len(@fecreg2)>0)   set @fecreg2  = @fecreg2  + ' 23:59'        

if(len(@feccita2)>0)  set @feccita2 = @feccita2 + ' 23:59'        
                                                                                

if(@v_co_perfil_usuario='AGEN')                                                    

begin          

                                                  

select --distinct        

	isnull(a.nid_cita,'') nid_cita        

    ,isnull(f.nid_estado,'') nid_estado        

    ,isnull(a.cod_reserva_cita,'') cod_reserva_cita                                                        

    ,(convert(varchar(10),a.fe_crea,103)+ ' - '+ convert(varchar(5),a.fe_crea,108))  fe_hora_reg    

    ,convert(varchar(10),a.fe_programada,103) fecha_cita         

    ,isnull(a.ho_inicio,'') hora_cita           

    ,h.no_valor1 estado_cita        

    ,isnull(i.nombre,'') departamento        

    ,isnull(j.nombre,'') provincia        

    ,isnull(k.nombre,'') distrito        

    ,isnull(c.no_ubica_corto,'') punto_red        

    ,isnull(b.no_taller,'') taller        

    ,isnull(g.vnomusr,'')+' '+isnull(g.no_ape_paterno,'')+' '+isnull(g.no_ape_materno,'') asesorservicio        

    ,isnull(a.nu_placa,'') placapatente                       

    ,e.nu_documento        

    ,isnull(e.no_cliente,'') as  nomcliente        

    ,isnull(e.no_ape_pat,'')+' '+isnull(e.no_ape_mat,'') as apecliente               

    ,case when a.fl_datos_pendientes='0' then 'NO' else 'SI' end indpendiente         

    ,isnull(e.nu_telefono,'') telefonocliente          

    ,isnull(e.no_correo,'') emailcliente          

    ,isnull(a.nid_taller,'') nid_tallercita          

    ,isnull(a.nid_servicio,'') nid_serviciocita          

    ,isnull(s.no_dias_validos,'') no_dias_validos        

    ,isnull(y.no_valor1,'') intervalotaller        

    ,isnull(g.nid_usuario,'') id_asesor        

    ,isnull(a.ho_fin,'') hora_cita_fin        

    ,h.no_valor1 id_estado        

    ,f.fl_activo fl_estactivo                                                             

    ,a.nid_modelo                

    ,(case 

	   when (isnull(co_origen ,'')<>'') then cod_origen.no_valor1

	   when a.fl_origen  = 'B' then isnull(a.co_usuario_crea,'') else 'WEB' 

	end) co_usuario_crea        

    ,isnull(a.co_usuario_modi,'') co_usuario_modi  

    ,s.no_servicio  

    ,mr.no_marca  

    ,md.no_modelo  
from tbl_cita as a

left join mae_tabla_detalle as cod_origen on cod_origen.fl_inactivo=0 and cod_origen.nid_tabla_gen =231 and cod_origen.no_valor2=a.co_origen --I/F@012

inner join tbl_estado_cita f on(a.nid_cita=f.nid_cita and f.fl_activo='A')                                                    

left outer join mae_tabla_detalle h on (f.co_estado_cita =  h.nid_tabla_gen_det and h.fl_inactivo = '0')                                                                        

inner join mae_taller b        on (a.nid_taller = b.nid_taller)                                  

inner join mae_marca mr on (mr.nid_marca = a.nid_marca )  

inner join mae_modelo md on (md.nid_modelo = a.nid_modelo )  

inner join mae_ubicacion c     on (b.nid_ubica  = c.nid_ubica)   

inner join mae_ubigeo i     on (c.coddpto=i.coddpto and c.coddpto=i.coddpto and i.codprov='00' and i.coddist='00')                                  

inner join mae_ubigeo j     on (c.coddpto=j.coddpto and c.codprov=j.codprov and j.coddpto=i.coddpto and j.coddist='00')                                  

inner join mae_ubigeo k     on (c.coddpto=k.coddpto and c.codprov=k.codprov and c.coddist=k.coddist and k.coddpto=j.coddpto and k.codprov=j.codprov)                                  

inner join mae_servicio_especifico s on(a.nid_servicio = s.nid_servicio and s.fl_activo ='A')           

left outer join mae_vehiculo d on(a.nid_vehiculo=d.nid_vehiculo)           

inner join mae_cliente e on (a.nid_contacto_src = e.nid_cliente)                              

inner join usr g               on (f.nid_usuario=g.nid_usuario)        

inner join mae_tabla_detalle y on (b.co_intervalo_atenc =  y.nid_tabla_gen_det and y.fl_inactivo = '0')        

        

WHERE                      

                                          

  a.fl_activo='A' and                             

 (@coddpto='0' or c.coddpto=@coddpto) and                                                                                  

 (@codprov='0' or c.codprov=@codprov) and                                                                

 (@coddist='0' or c.coddist=@coddist) and                                                                        

 (@nid_ubica=0 or c.nid_ubica=@nid_ubica) and                                                                                  

 (@nid_taller=0 or b.nid_taller=@nid_taller) and                                                                             

 (isnull(g.vnomusr,'')+' '+isnull(g.no_ape_paterno,'')+' '+isnull(g.no_ape_materno,'') like('%'+@asesorservicio+'%')) and                                                       

 (@indpendiente='2' or a.fl_datos_pendientes=@indpendiente) and                                                                         

 (len(@fecreg1)=0 or a.fe_crea>=@fecreg1) and                                                

 (len(@fecreg2)=0 or a.fe_crea<=@fecreg2) and                                                 

 (len(@feccita1)=0 or a.fe_programada>=convert(datetime,@feccita1)) and                                                

 (len(@feccita2)=0 or a.fe_programada<=convert(datetime,@feccita2)) and                                                                         

 (len(@horacita1)=0 or a.ho_inicio>=@horacita1) and                                                

 (len(@horacita2)=0 or a.ho_inicio<=@horacita2) and                                                

 (a.nu_placa like('%'+rtrim(ltrim(@nu_placa))+'%')) and                                                                     

 (@nid_marca=0 or d.nid_marca=@nid_marca) and                                                                     

 (@nid_modelo=0 or d.nid_modelo=@nid_modelo) and    

 (@co_tipo_documento='0' or e.co_tipo_documento=@co_tipo_documento) and                                                                     

 (case when e.nu_documento is null then '' else e.nu_documento end  like('%'+rtrim(ltrim(@nu_documento+'%')))) and                                                                     

 (isnull(e.no_cliente,'') like('%'+@no_cliente+'%')) and                                                               

 (isnull(e.no_ape_pat,'')+' '+isnull(e.no_ape_mat,'') like('%'+@no_apellidos+'%')) and        

 (@estadoreserva='0' or @estadoreserva=h.no_valor3) and                                                   

  isnull(cod_reserva_cita,'') like('%'+ltrim(rtrim(@cod_reserva_cita))+'%')        

                  

ORDER BY a.fe_programada DESC ,   isnull(a.ho_inicio,'') DESC

END                                                    

ELSE                                                    

BEGIN       

                            

IF(@V_CO_PERFIL_USUARIO='ASRV')                            

BEGIN                                   

SELECT DISTINCT                                                                           
 isnull(a.nid_cita,'') nid_cita        
,isnull(f.nid_estado,'') nid_estado        
,isnull(a.cod_reserva_cita,'') cod_reserva_cita                                                        
,convert(varchar(10),a.fe_crea,103) fe_hora_reg  
,a.fe_programada                                                 
,convert(varchar(10),a.fe_programada,103) fecha_cita         
,isnull(a.ho_inicio,'') hora_cita           
,h.no_valor1 estado_cita        
,isnull(i.nombre,'') departamento        
,isnull(j.nombre,'') provincia        
,isnull(k.nombre,'') distrito        
,isnull(c.no_ubica_corto,'') punto_red        
,isnull(b.no_taller,'') taller        
,isnull(g.vnomusr,'')+' '+isnull(g.no_ape_paterno,'')+' '+isnull(g.no_ape_materno,'') asesorservicio        
,isnull(a.nu_placa,'') placapatente                       
,e.nu_documento        
,isnull(e.no_cliente,'') as  nomcliente        
,isnull(e.no_ape_pat,'')+' '+isnull(e.no_ape_mat,'') as apecliente               
,case when a.fl_datos_pendientes='0' then 'NO' else 'SI' end indpendiente         
,isnull(e.nu_telefono,'') telefonocliente          
,isnull(e.no_correo,'') emailcliente          
,isnull(a.nid_taller,'') nid_tallercita          
,isnull(a.nid_servicio,'') nid_serviciocita          
,isnull(s.no_dias_validos,'') no_dias_validos        
,isnull(y.no_valor1,'') intervalotaller        
,isnull(g.nid_usuario,'') id_asesor        
,isnull(a.ho_fin,'') hora_cita_fin        
,h.no_valor1 id_estado        
,f.fl_activo fl_estactivo                                                             
,a.nid_modelo                
,(case 
    when (isnull(co_origen ,'')<>'') then cod_origen.no_valor1
    when a.fl_origen  = 'B' then isnull(a.co_usuario_crea,'') else 'WEB' 
end) co_usuario_crea        
,isnull(a.co_usuario_modi,'') co_usuario_modi    
,s.no_servicio
,mr.no_marca
,md.no_modelo
from tbl_cita as a
left join mae_tabla_detalle as cod_origen on cod_origen.fl_inactivo=0 and cod_origen.nid_tabla_gen =231 and cod_origen.no_valor2=a.co_origen --I/F@012
inner join tbl_estado_cita f   on(a.nid_cita=f.nid_cita and f.fl_activo='A')        
inner join mae_tabla_detalle h on (f.co_estado_cita =  h.nid_tabla_gen_det and h.fl_inactivo = '0')        
inner join mae_taller b        on (a.nid_taller = b.nid_taller)        
inner join mae_marca mr on (mr.nid_marca = a.nid_marca )  
inner join mae_modelo md on (md.nid_modelo = a.nid_modelo )  
inner join mae_usr_taller ut   on (ut.nid_taller = b.nid_taller and ut.nid_usuario = @nid_usuario and ut.fl_activo = 'A')        
inner join mae_ubicacion c    on (b.nid_ubica = c.nid_ubica)        
inner join mae_ubigeo i     on (c.coddpto=i.coddpto and c.coddpto=i.coddpto and i.codprov='00' and i.coddist='00')                                  
inner join mae_ubigeo j     on (c.coddpto=j.coddpto and c.codprov=j.codprov and j.coddpto=i.coddpto and j.coddist='00')                                  
inner join mae_ubigeo k     on (c.coddpto=k.coddpto and c.codprov=k.codprov and c.coddist=k.coddist and k.coddpto=j.coddpto and k.codprov=j.codprov)                                  
inner join mae_servicio_especifico s on(a.nid_servicio = s.nid_servicio and s.fl_activo ='A')           
left outer join mae_vehiculo d on(a.nid_vehiculo=d.nid_vehiculo)           
inner join mae_cliente e on (a.nid_contacto_src = e.nid_cliente)                              
inner join usr g               on (f.nid_usuario=g.nid_usuario and g.nid_usuario = @nid_usuario)        
inner join mae_tabla_detalle y on (b.co_intervalo_atenc =  y.nid_tabla_gen_det and y.fl_inactivo = '0')        
WHERE                                       
  a.fl_activo='A' and                             
 (@coddpto='0' or c.coddpto=@coddpto) and                                                                                  
 (@codprov='0' or c.codprov=@codprov) and                                                                
 (@coddist='0' or c.coddist=@coddist) and                                                                        
 (@nid_ubica=0 or c.nid_ubica=@nid_ubica) and                                                                                  
 (@nid_taller=0 or b.nid_taller=@nid_taller) and                                                                             
 (isnull(g.vnomusr,'')+' '+isnull(g.no_ape_paterno,'')+' '+isnull(g.no_ape_materno,'') like('%'+@asesorservicio+'%')) and                                                       
 (@indpendiente='2' or a.fl_datos_pendientes=@indpendiente) and            
 (len(@fecreg1)=0 or a.fe_crea>=@fecreg1) and                                                
 (len(@fecreg2)=0 or a.fe_crea<=@fecreg2) and                                                 
 (len(@feccita1)=0 or a.fe_programada>=convert(datetime,@feccita1)) and                                                
 (len(@feccita2)=0 or a.fe_programada<=convert(datetime,@feccita2)) and                                                                         
 (len(@horacita1)=0 or a.ho_inicio>=@horacita1) and                                                
 (len(@horacita2)=0 or a.ho_inicio<=@horacita2) and                                                
 (a.nu_placa like('%'+rtrim(ltrim(@nu_placa))+'%')) and                                                                     
 (@nid_marca=0 or d.nid_marca=@nid_marca) and                                                                     
 (@nid_modelo=0 or d.nid_modelo=@nid_modelo) and          
 (@co_tipo_documento='0' or e.co_tipo_documento=@co_tipo_documento) and                                                                     
 (case when e.nu_documento is null then '' else e.nu_documento end  like('%'+rtrim(ltrim(@nu_documento+'%')))) and                                                                     
 (isnull(e.no_cliente,'') like('%'+@no_cliente+'%')) and                                                               
 (isnull(e.no_ape_pat,'')+' '+isnull(e.no_ape_mat,'') like('%'+@no_apellidos+'%')) and        
(@estadoreserva='0' or @estadoreserva=h.no_valor3) and                                                   
 isnull(cod_reserva_cita,'') like('%'+ltrim(rtrim(@cod_reserva_cita))+'%')        
ORDER BY a.fe_programada DESC, isnull(a.ho_inicio,'') DESC 

END                            

ELSE                            

BEGIN                            

                            

IF(@V_CO_PERFIL_USUARIO='ATIE')                                                    

BEGIN         

select --distinct        

 isnull(a.nid_cita,'') nid_cita        

,isnull(f.nid_estado,'') nid_estado        

,isnull(a.cod_reserva_cita,'') cod_reserva_cita                                                        

,convert(varchar(10),a.fe_crea,103) fe_hora_reg                                                                               

,convert(varchar(10),a.fe_programada,103) fecha_cita         

,isnull(a.ho_inicio,'') hora_cita           

,h.no_valor1 estado_cita        

,isnull(i.nombre,'') departamento        

,isnull(j.nombre,'') provincia        

,isnull(k.nombre,'') distrito        

,isnull(c.no_ubica_corto,'') punto_red        

,isnull(b.no_taller,'') taller        

,isnull(g.vnomusr,'')+' '+isnull(g.no_ape_paterno,'')+' '+isnull(g.no_ape_materno,'') asesorservicio        

,isnull(a.nu_placa,'') placapatente                       

,e.nu_documento        

,isnull(e.no_cliente,'') as  nomcliente        

,isnull(e.no_ape_pat,'')+' '+isnull(e.no_ape_mat,'') as apecliente               

,case when a.fl_datos_pendientes='0' then 'NO' else 'SI' end indpendiente         

,isnull(e.nu_telefono,'') telefonocliente          

,isnull(e.no_correo,'') emailcliente          

,isnull(a.nid_taller,'') nid_tallercita          

,isnull(a.nid_servicio,'') nid_serviciocita          

,isnull(s.no_dias_validos,'') no_dias_validos        

,isnull(y.no_valor1,'') intervalotaller        

,isnull(g.nid_usuario,'') id_asesor        

,isnull(a.ho_fin,'') hora_cita_fin        

,h.no_valor1 id_estado        

,f.fl_activo fl_estactivo                                                             

,a.nid_modelo                

,(case
    when (isnull(co_origen ,'')<>'') then cod_origen.no_valor1
    when a.fl_origen  = 'B' then isnull(a.co_usuario_crea,'') else 'WEB' 
end) co_usuario_crea        
,isnull(a.co_usuario_modi,'') co_usuario_modi         
,s.no_servicio       
,mr.no_marca  
,md.no_modelo  
from tbl_cita as a
left join mae_tabla_detalle as cod_origen on cod_origen.fl_inactivo=0 and cod_origen.nid_tabla_gen =231 and cod_origen.no_valor2=a.co_origen --I/F@012
inner join tbl_estado_cita f   on(a.nid_cita=f.nid_cita and f.fl_activo='A')        
inner join mae_tabla_detalle h on (f.co_estado_cita =  h.nid_tabla_gen_det and h.fl_inactivo = '0')        
inner join mae_taller b        on (a.nid_taller = b.nid_taller)        
inner join mae_marca mr  on (mr.nid_marca = a.nid_marca )  
inner join mae_modelo md on (md.nid_modelo = a.nid_modelo )                                   
INNER JOIN (                                                    
  SELECT * FROM MAE_TALLER                                                    
  WHERE FL_ACTIVO='A' AND NID_UBICA IN(                                                    
    SELECT NID_UBICA FROM MAE_USR_UBICACION                                  
 WHERE NID_USUARIO=@NID_USUARIO AND FL_ACTIVO='A'                                     
  )                                                    
) TTALLER ON(B.NID_TALLER=TTALLER.NID_TALLER)                                                    
INNER JOIN MAE_UBICACION C ON(B.NID_UBICA=C.NID_UBICA)                                                     
INNER JOIN (                                                    
  SELECT * FROM MAE_UBICACION                                                    
  WHERE FL_INACTIVO='0' AND NID_UBICA IN(                                                    
   SELECT NID_UBICA FROM MAE_USR_UBICACION                                  
 WHERE NID_USUARIO=@NID_USUARIO AND FL_ACTIVO='A'                                         
  )  
) TUBICACION ON(C.NID_UBICA=TUBICACION.NID_UBICA)                               
INNER JOIN MAE_UBIGEO I ON(C.CODDPTO=I.CODDPTO AND C.CODDPTO=I.CODDPTO AND I.CODPROV='00' AND I.CODDIST='00')                                                     
INNER JOIN (                             
  SELECT * FROM MAE_UBIGEO                                   
  WHERE CODDPTO IN                                                    
  (                                                    
    SELECT CODDPTO FROM MAE_UBICACION                                                    
    WHERE NID_UBICA IN(                                    
    SELECT NID_UBICA FROM MAE_USR_UBICACION                                  
 WHERE NID_USUARIO=@NID_USUARIO AND FL_ACTIVO='A'                                                    
    )                                                    
  ) AND CODPROV='00' AND CODDIST='00'                                                    
) T1UBIGEO ON(I.CODDPTO=T1UBIGEO.CODDPTO AND I.CODPROV=T1UBIGEO.CODPROV AND I.CODDIST=T1UBIGEO.CODDIST)                                                    
INNER JOIN MAE_UBIGEO J ON(C.CODDPTO=J.CODDPTO AND C.CODPROV=J.CODPROV AND J.CODDPTO=T1UBIGEO.CODDPTO AND J.CODDIST='00')                                                     
INNER JOIN (                                                    
  SELECT * FROM MAE_UBIGEO                                                    
  WHERE CODDPTO+CODPROV IN                                                    
  (                                                    
    SELECT CODDPTO+CODPROV FROM MAE_UBICACION                                                    
    WHERE NID_UBICA IN(                                                    
    SELECT NID_UBICA FROM MAE_USR_UBICACION                                  
 WHERE NID_USUARIO=@NID_USUARIO AND FL_ACTIVO='A'                                                    
    )                                                   

  ) AND CODDIST='00'                                                    

) T2UBIGEO ON(J.CODDPTO=T2UBIGEO.CODDPTO AND J.CODPROV=T2UBIGEO.CODPROV AND J.CODDIST=T2UBIGEO.CODDIST)                                                                              

INNER JOIN MAE_UBIGEO K ON(C.CODDPTO=K.CODDPTO AND C.CODPROV=K.CODPROV AND C.CODDIST=K.CODDIST AND K.CODDPTO=T2UBIGEO.CODDPTO AND K.CODPROV=T2UBIGEO.CODPROV)                      

INNER JOIN (                                                    

  SELECT * FROM MAE_UBIGEO                                                    

  WHERE CODDPTO+CODPROV+CODDIST IN                                               

  (                                                    

    SELECT CODDPTO+CODPROV+CODDIST FROM MAE_UBICACION                                                    

    WHERE NID_UBICA IN(                                                    

    SELECT NID_UBICA FROM MAE_USR_UBICACION                                  

 WHERE NID_USUARIO=@NID_USUARIO AND FL_ACTIVO='A'                                          
    )                                                 
  )                      
) T3UBIGEO ON(K.CODDPTO=T3UBIGEO.CODDPTO AND K.codprov=T3UBIGEO.codprov AND K.CODDIST=T3UBIGEO.CODDIST) 
inner join mae_servicio_especifico s on(a.nid_servicio = s.nid_servicio and s.fl_activo ='A')           
left outer join mae_vehiculo d on(a.nid_vehiculo=d.nid_vehiculo)           
inner join mae_cliente e on (a.nid_contacto_src = e.nid_cliente)                              
inner join usr g               on (f.nid_usuario=g.nid_usuario)        
inner join mae_tabla_detalle y on (b.co_intervalo_atenc =  y.nid_tabla_gen_det and y.fl_inactivo = '0')        
WHERE                                                                                  

  a.fl_activo='A' and                             

 (@coddpto='0' or c.coddpto=@coddpto) and                                                   

 (@codprov='0' or c.codprov=@codprov) and                                                                

 (@coddist='0' or c.coddist=@coddist) and                                                                        

 (@nid_ubica=0 or c.nid_ubica=@nid_ubica) and                                                                                  

 (@nid_taller=0 or b.nid_taller=@nid_taller) and                                                                                    

 (ISNULL(G.VNOMUSR,'')+' '+ISNULL(G.NO_APE_PATERNO,'')+' '+ISNULL(G.NO_APE_MATERNO,'') LIKE('%'+@ASESORSERVICIO+'%')) AND                                                                    

 (@INDPENDIENTE='2' OR A.FL_DATOS_PENDIENTES=@INDPENDIENTE) AND                                       

 (LEN(@FECREG1)=0 OR A.FE_CREA>=@FECREG1) AND                                                

 (LEN(@FECREG2)=0 OR A.FE_CREA<=@FECREG2) AND                                                 

(LEN(@FECCITA1)=0 OR A.FE_PROGRAMADA>=CONVERT(DATETIME,@FECCITA1)) AND                                                

 (LEN(@FECCITA2)=0 OR A.FE_PROGRAMADA<=CONVERT(DATETIME,@FECCITA2)) AND                                                                          

 (LEN(@HORACITA1)=0 OR A.HO_INICIO>=@HORACITA1) AND                                                

 (LEN(@HORACITA2)=0 OR A.HO_INICIO<=@HORACITA2) AND                                                

 (A.NU_PLACA LIKE('%'+RTRIM(LTRIM(@NU_PLACA))+'%')) AND                                                                     

 (@NID_MARCA=0 OR D.NID_MARCA=@NID_MARCA) AND                                                     

 (D.NID_MARCA IN(                                        

    SELECT DISTINCT NID_MARCA FROM MAE_MARCA                                                    

    WHERE /*FL_INACTIVO='0' AND*/ NID_MARCA IN(                                                    

    SELECT DISTINCT NID_MARCA FROM MAE_MODELO                                                    

    WHERE /*FL_INACTIVO='0' AND*/ NID_MODELO IN(                            

    SELECT NID_MODELO FROM MAE_USR_MODELO  

    WHERE NID_USUARIO=@NID_USUARIO AND FL_ACTIVO='A'                                                    

  )                                                    

    )                                                    

  )) AND                                                    

 (@NID_MODELO=0 OR D.NID_MODELO=@NID_MODELO) AND                                                     

 (D.NID_MODELO IN(SELECT NID_MODELO FROM MAE_MODELO                                                    

  WHERE /*FL_INACTIVO='0' AND*/ NID_MODELO IN(                                                    

  SELECT NID_MODELO FROM MAE_USR_MODELO                                                    

  WHERE NID_USUARIO=@NID_USUARIO AND FL_ACTIVO='A'                

  ))) AND                                                    

 (@co_tipo_documento='0' or e.co_tipo_documento=@co_tipo_documento) and                                                                     

 (case when e.nu_documento is null then '' else e.nu_documento end  like('%'+rtrim(ltrim(@nu_documento+'%')))) and         

 (isnull(e.no_cliente,'') like('%'+@no_cliente+'%')) and                                                               

 (isnull(e.no_ape_pat,'')+' '+isnull(e.no_ape_mat,'') like('%'+@no_apellidos+'%')) and        

(@estadoreserva='0' or @estadoreserva=h.no_valor3) and                                                   

 ISNULL(cod_reserva_cita,'') like('%'+ltrim(rtrim(@cod_reserva_cita))+'%')        

                                  

END                                             

ELSE                                  

BEGIN          

       

select --distinct        

 isnull(a.nid_cita,'') nid_cita        

,isnull(f.nid_estado,'') nid_estado   

,isnull(a.cod_reserva_cita,'') cod_reserva_cita                                                        

,convert(varchar(10),a.fe_crea,103) fe_hora_reg                                                                               

,convert(varchar(10),a.fe_programada,103) fecha_cita         

,isnull(a.ho_inicio,'') hora_cita           

,h.no_valor1 estado_cita        

,isnull(i.nombre,'') departamento        

,isnull(j.nombre,'') provincia        

,isnull(k.nombre,'') distrito        

,isnull(c.no_ubica_corto,'') punto_red        

,isnull(b.no_taller,'') taller        

,isnull(g.vnomusr,'')+' '+isnull(g.no_ape_paterno,'')+' '+isnull(g.no_ape_materno,'') asesorservicio        

,isnull(a.nu_placa,'') placapatente                       

,e.nu_documento        

,isnull(e.no_cliente,'') as  nomcliente        

,isnull(e.no_ape_pat,'')+' '+isnull(e.no_ape_mat,'') as apecliente               

,case when a.fl_datos_pendientes='0' then 'NO' else 'SI' end indpendiente         

,isnull(e.nu_telefono,'') telefonocliente          

,isnull(e.no_correo,'') emailcliente          

,isnull(a.nid_taller,'') nid_tallercita          

,isnull(a.nid_servicio,'') nid_serviciocita          

,isnull(s.no_dias_validos,'') no_dias_validos        

,isnull(y.no_valor1,'') intervalotaller        

,isnull(g.nid_usuario,'') id_asesor        

,isnull(a.ho_fin,'') hora_cita_fin        

,h.no_valor1 id_estado        

,f.fl_activo fl_estactivo                                                             
,a.nid_modelo                
,(case 
    when (isnull(co_origen ,'')<>'') then cod_origen.no_valor1
    when a.fl_origen  = 'B' then isnull(a.co_usuario_crea,'') else 'WEB' 
 end) co_usuario_crea        
,isnull(a.co_usuario_modi,'') co_usuario_modi           
,s.no_servicio       
,mr.no_marca  
,md.no_modelo  
from tbl_cita as a
left join mae_tabla_detalle as cod_origen on cod_origen.fl_inactivo=0 and cod_origen.nid_tabla_gen =231 and cod_origen.no_valor2=a.co_origen
inner join tbl_estado_cita f   on(a.nid_cita=f.nid_cita and f.fl_activo='A')   
inner join mae_tabla_detalle h on (f.co_estado_cita =  h.nid_tabla_gen_det and h.fl_inactivo = '0')        
inner join mae_taller b        on (a.nid_taller = b.nid_taller)              
inner join mae_marca mr  on (mr.nid_marca = a.nid_marca )  
inner join mae_modelo md on (md.nid_modelo = a.nid_modelo )      
INNER JOIN (                                                    
  SELECT * FROM MAE_TALLER                                                    
  WHERE FL_ACTIVO='A' AND NID_TALLER IN(                                                    
    SELECT NID_TALLER FROM MAE_USR_TALLER                                                    
    WHERE NID_USUARIO=@NID_USUARIO AND FL_ACTIVO='A'                                                    
  )                                                    
) TTALLER ON(B.NID_TALLER=TTALLER.NID_TALLER)                                                    
INNER JOIN MAE_UBICACION C ON(B.NID_UBICA=C.NID_UBICA)                                                     
INNER JOIN (                                      
  SELECT * FROM MAE_UBICACION                
  WHERE FL_INACTIVO='0' AND NID_UBICA IN(                                                    
    SELECT NID_UBICA FROM MAE_TALLER                                                    
    WHERE NID_TALLER IN(                                                    
    SELECT NID_TALLER FROM MAE_USR_TALLER                                                    
    WHERE NID_USUARIO=@NID_USUARIO AND FL_ACTIVO='A'                                                    
  )        
  )        
) TUBICACION ON(C.NID_UBICA=TUBICACION.NID_UBICA)                                                                              

INNER JOIN MAE_UBIGEO I ON(C.CODDPTO=I.CODDPTO AND C.CODDPTO=I.CODDPTO AND I.CODPROV='00' AND I.CODDIST='00')                 

INNER JOIN (                                                    
 SELECT * FROM MAE_UBIGEO                                                    
  WHERE CODDPTO IN                                                    
  (                                                    
    SELECT CODDPTO FROM MAE_UBICACION                                                    
    WHERE NID_UBICA IN(                                                    
    SELECT NID_UBICA FROM MAE_TALLER                                                    
    WHERE NID_TALLER IN(                                                    
    SELECT NID_TALLER FROM MAE_USR_TALLER                                                    
    WHERE NID_USUARIO=@NID_USUARIO AND FL_ACTIVO='A'                                                    
    )                                                    
    )                                            
  ) AND CODPROV='00' AND CODDIST='00'                                                    

) T1UBIGEO ON(I.CODDPTO=T1UBIGEO.CODDPTO AND I.CODPROV=T1UBIGEO.CODPROV AND I.CODDIST=T1UBIGEO.CODDIST)                                                    

INNER JOIN MAE_UBIGEO J ON(C.CODDPTO=J.CODDPTO AND C.CODPROV=J.CODPROV AND J.CODDPTO=T1UBIGEO.CODDPTO AND J.CODDIST='00')                                                     

INNER JOIN (                                                    

  SELECT * FROM MAE_UBIGEO                                                    

  WHERE CODDPTO+CODPROV IN                                                    

  (                                                    

    SELECT CODDPTO+CODPROV FROM MAE_UBICACION                                                    

    WHERE NID_UBICA IN(                                           

    SELECT NID_UBICA FROM MAE_TALLER                                                    

    WHERE NID_TALLER IN(                                                    

    SELECT NID_TALLER FROM MAE_USR_TALLER                       

    WHERE NID_USUARIO=@NID_USUARIO AND FL_ACTIVO='A'                                                    

    )                                                    

    )                                                    

  ) AND CODDIST='00'                                                    

) T2UBIGEO ON(J.CODDPTO=T2UBIGEO.CODDPTO AND J.CODPROV=T2UBIGEO.CODPROV AND J.CODDIST=T2UBIGEO.CODDIST)                                                                              

INNER JOIN MAE_UBIGEO K ON(C.CODDPTO=K.CODDPTO AND C.CODPROV=K.CODPROV AND C.CODDIST=K.CODDIST AND K.CODDPTO=T2UBIGEO.CODDPTO AND K.CODPROV=T2UBIGEO.CODPROV)                                                      

INNER JOIN (                                                    

  SELECT * FROM MAE_UBIGEO                                                    

  WHERE CODDPTO+CODPROV+CODDIST IN                                               

  (                                               

    SELECT CODDPTO+CODPROV+CODDIST FROM MAE_UBICACION                                                    

    WHERE NID_UBICA IN(                                                    

    SELECT NID_UBICA FROM MAE_TALLER                                         

    WHERE NID_TALLER IN(                                                    

    SELECT NID_TALLER FROM MAE_USR_TALLER                          

    WHERE NID_USUARIO=@NID_USUARIO AND FL_ACTIVO='A'                                                    

    )                                                    

    )                                                    

  )                                                    

) T3UBIGEO ON(K.CODDPTO=T3UBIGEO.CODDPTO AND K.CODPROV=T3UBIGEO.CODPROV AND K.CODDIST=T3UBIGEO.CODDIST)          

                                                                             

inner join mae_servicio_especifico s on(a.nid_servicio = s.nid_servicio and s.fl_activo ='A')           

left outer join mae_vehiculo d on(a.nid_vehiculo=d.nid_vehiculo)           

inner join mae_cliente e on (a.nid_contacto_src = e.nid_cliente)                              

inner join usr g               on (f.nid_usuario=g.nid_usuario)        

inner join mae_tabla_detalle y on (b.co_intervalo_atenc =  y.nid_tabla_gen_det and y.fl_inactivo = '0')        

        

WHERE        

        

  a.fl_activo='A' and                             

 (@coddpto='0' or c.coddpto=@coddpto) and                                                                                  

 (@codprov='0' or c.codprov=@codprov) and                                                                

 (@coddist='0' or c.coddist=@coddist) and                                                                        

 (@nid_ubica=0 or c.nid_ubica=@nid_ubica) and                                                                                  

 (@nid_taller=0 or b.nid_taller=@nid_taller) and    

 (isnull(g.vnomusr,'')+' '+isnull(g.no_ape_paterno,'')+' '+isnull(g.no_ape_materno,'') like('%'+@asesorservicio+'%')) and                                                       

 (@indpendiente='2' or a.fl_datos_pendientes=@indpendiente) and                                                                         

 (len(@fecreg1)=0 or a.fe_crea>=@fecreg1) and                                                

 (len(@fecreg2)=0 or a.fe_crea<=@fecreg2) and                                                 

 (len(@feccita1)=0 or a.fe_programada>=convert(datetime,@feccita1)) and                                                

 (len(@feccita2)=0 or a.fe_programada<=convert(datetime,@feccita2)) and                                                                         

 (len(@horacita1)=0 or a.ho_inicio>=@horacita1) and                                                

 (len(@horacita2)=0 or a.ho_inicio<=@horacita2) and                                                

 (a.nu_placa like('%'+rtrim(ltrim(@nu_placa))+'%')) and                                                                     

 (@nid_marca=0 or d.nid_marca=@nid_marca) and                                                                     

 (@nid_modelo=0 or d.nid_modelo=@nid_modelo) and                                                

 (D.NID_MARCA IN(                                                    

    SELECT DISTINCT NID_MARCA FROM MAE_MARCA                                                    

    WHERE /*FL_INACTIVO='0' AND*/ NID_MARCA IN(                                                    

    SELECT DISTINCT NID_MARCA FROM MAE_MODELO                                                    

    WHERE /*FL_INACTIVO='0' AND*/ NID_MODELO IN(                                                    

    SELECT NID_MODELO FROM MAE_USR_MODELO                                                    

    WHERE NID_USUARIO=@NID_USUARIO AND FL_ACTIVO='A'                                                    

  )                                                    

    )                                                    

  )) AND                                                    

 (@nid_modelo=0 or d.nid_modelo=@nid_modelo) and          

 (D.NID_MODELO IN(SELECT NID_MODELO FROM MAE_MODELO                                                    

  WHERE /*FL_INACTIVO='0' AND*/ NID_MODELO IN(                                                    

  SELECT NID_MODELO FROM MAE_USR_MODELO                                                    

  WHERE NID_USUARIO=@NID_USUARIO AND FL_ACTIVO='A'                                               

  ))) AND                                                    

         

 (@co_tipo_documento='0' or e.co_tipo_documento=@co_tipo_documento) and                                                                     

 (case when e.nu_documento is null then '' else e.nu_documento end  like('%'+rtrim(ltrim(@nu_documento+'%')))) and                                                                     

 (isnull(e.no_cliente,'') like('%'+@no_cliente+'%')) and                                                               

 (isnull(e.no_ape_pat,'')+' '+isnull(e.no_ape_mat,'') like('%'+@no_apellidos+'%')) and        

 (@estadoreserva='0' or @estadoreserva=h.no_valor3) and                                                   

  ISNULL(cod_reserva_cita,'') like('%'+ltrim(rtrim(@cod_reserva_cita))+'%')        

    

ORDER BY a.fe_programada desc,   isnull(a.ho_inicio,'') DESC        
            

END                         

END                                              

END           
GO



ALTER PROCEDURE [dbo].[src_sps_combo_FO]
/***************************************************************************      
OBJETIVO: LISTA COMBO DE UN MAESTRO
****************************************************************************/      
(      
	@vi_co_maestro varchar(50),
	@vi_co_padre varchar(20)
)      
AS      
BEGIN
	if(@vi_co_maestro = 'MARCAS')  
	begin
		select ltrim(nid_marca) as [value], no_marca as [nombre] from mae_marca where fl_inactivo = '0'
		order by [nombre]
	end  
	else if(@vi_co_maestro = 'MODELOS')
	begin
		select ltrim(nid_modelo) as [value], no_modelo as [nombre] from mae_modelo 
			where fl_inactivo = '0'
			and nid_marca = @vi_co_padre
			order by [nombre]
	end  
	else if(@vi_co_maestro = 'TIPO_SERVICIOS')  
	begin
		select distinct ltrim(ts.nid_tipo_servicio) as [value], ts.no_tipo_servicio as [nombre] from mae_tipo_servicio ts
		inner join mae_servicio_especifico ser on ser.nid_tipo_servicio = ts.nid_tipo_servicio and ser.fl_activo = 'A'
			where ts.fl_activo = 'A'
			and ts.nid_tipo_servicio <> 8
			and (isnull(@vi_co_padre,'')='' or ser.nid_servicio in (
					select distinct nid_servicio from mae_servicio_especifico_modelo a
					where nid_modelo = @vi_co_padre and a.fl_inactivo = 'A'  
				)  
			)
			order by [nombre]
	end  
	else if(@vi_co_maestro = 'SERVICIOS')
	begin
		select ltrim(nid_servicio) as [value], no_servicio as [nombre] from mae_servicio_especifico
			where fl_activo = 'A'
			and nid_tipo_servicio = @vi_co_padre
			order by [nombre]
	end 
	else if(@vi_co_maestro='UBIGEO')
	begin
		declare @dep varchar(2)
		declare @pro varchar(2)
		declare @dis varchar(2)

		exec @dep = [dbo].[sgsnet_sps_split] @vi_co_padre, '|', 1  
		exec @pro = [dbo].[sgsnet_sps_split] @vi_co_padre, '|', 2  
		exec @dis = [dbo].[sgsnet_sps_split] @vi_co_padre, '|', 3  

		if(@dep='00')
		begin
			SELECT  ltrim(coddpto) as [value], nombre as [nombre] FROM mae_ubigeo 
			where codprov='00' and coddist='00' 
			order by [nombre]
		end
		else if(@pro='00')
		begin
			SELECT  ltrim(codprov) as [value], nombre as [nombre] FROM mae_ubigeo 
			where coddpto=@dep and codprov!='00' and coddist='00'
			order by [nombre]
		end
		else
		begin
			SELECT  ltrim(coddist) as [value], nombre as [nombre] FROM mae_ubigeo 
			where coddpto=@dep and codprov=@pro and coddist!='00'
			order by [nombre]
		end
	end
	else if(@vi_co_maestro='TIPO_PERSONA')
	begin
		SELECT NO_VALOR2 as [value], NO_VALOR1 as [nombre] FROM MAE_TABLA_DETALLE    
		WHERE NID_TABLA_GEN=54
	end
END
go


ALTER PROCEDURE SRC_SPS_MARCAS_FO
/*****************************************************************************    
OBJETIVO: LISTAR LAS MARCAS
****************************************************************************/
as
begin
	select nid_marca, co_marca, no_marca
	from mae_marca
	where fl_inactivo = '0'
end
go

