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
                    
	set @vl_marca = (select no_marca from mae_marca where nid_marca = @vi_nid_marca)                                                          
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

ALTER PROCEDURE SRC_SPU_DATOS_CONTACTO_FO
/*****************************************************************************          
OBJETIVO  : ACTUALIZA LOS DATOS DE CLIENTE          
HISTORIAL  :           
****************************************************************************          
****************************************************************************/          
(          
@vi_id_contacto    int,
@vi_no_nombre      varchar(50),
@vi_no_ape_paterno varchar(50),
@vi_no_ape_materno varchar(50),
@vi_nu_documento   varchar(20),
@vi_no_email       varchar(255),
@vi_nu_tel_movil   varchar(20)
)          
as          
begin try          
 
	--declaración de variables          
	declare @vo_id_usuario int          
	declare @vo_resultado int          
	declare @vl_nu_error int, @vl_fl_transaccion char(1);          
          
	--inicialización de variables
	set @vl_fl_transaccion = '0';
          
	if not exists(select nid_cliente from mae_cliente where nid_cliente = @vi_id_contacto )          
	begin          
		set @vo_resultado = '0'          
		select @vo_resultado as vo_resultado          
		return
	end
          
	begin transaction

	set @vl_fl_transaccion = '1';

	update mae_cliente
	set
	co_tipo_cliente = '0001',
	nu_documento = @vi_nu_documento,
	no_cliente = @vi_no_nombre,
	no_ape_pat = @vi_no_ape_paterno,
	no_ape_mat = @vi_no_ape_materno,
	nu_celular = @vi_nu_tel_movil,
	no_correo = @vi_no_email
	where nid_cliente = @vi_id_contacto


	commit transaction
	set @vl_fl_transaccion = '0';
          
	set @vo_resultado = 1;
	select @vo_resultado as vo_resultado
          
end try          
begin catch          
	--select error_message(), error_number()          
	-- 2627 error de unique          
	-- 515 error de insertar valor null no permitido          
	-- 547 error conflicto de llave foranea          
	set     @vl_nu_error = error_number()          
	if      @vl_nu_error = 2627 set @vo_id_usuario = -2          
	else if @vl_nu_error = 515 set @vo_id_usuario  = -3          
	else if @vl_nu_error = 547 set @vo_id_usuario  = -4          
	else   set @vo_id_usuario = -1          

	if (@vl_fl_transaccion = '1') rollback transaction          
	select @vo_id_usuario as vo_id_error          
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
	and (tc.nu_placa  = @vi_nu_placa or @vi_nu_placa = '')
	and (tc.nid_contacto_src = @vi_nid_cliente or @vi_nid_cliente = 0)
	and (tc.fl_activo = 'A')
	and (tde.no_valor3 != 'EC03') --No se muestran Citas Anuladas
	order by tc.fe_programada desc,tc.ho_inicio asc                    
                    
end
go