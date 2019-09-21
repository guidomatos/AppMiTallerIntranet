use AppMiTaller
go

if exists(select 1 from sysobjects where name = 'sgsnet_spi_mae_servicio_especifico_modelo' and type = 'P')
begin
	drop proc sgsnet_spi_mae_servicio_especifico_modelo
end
go

create procedure sgsnet_spi_mae_servicio_especifico_modelo
/*********************************************          
Nombre Procedure  : sgsnet_spi_mae_servicio_especifico_modelo      
Objetivo          : asignar servicios a un modelo          
*********************************************/ 
@xmlServicios xml,
@co_usuario_crea varchar(20),
@no_estacion_red varchar(50),
@no_usuario_red varchar(50)
as
begin
 declare @hdoc int = 0    
 declare @v_in_TotalRegistros int = 0   
 declare @table table (id int primary key identity(1,1),nid_servicio int,nid_modelo  int)  
 set @v_in_TotalRegistros = IsNull(@xmlServicios.value('count(/ROOT/doc)', 'int'),0)   
if (@v_in_TotalRegistros > 0 )     
	begin  
	 exec sp_xml_preparedocument @hDoc output, @xmlServicios                       
		  insert into @table(nid_servicio,nid_modelo ) 
		  select nid_servicio,nid_modelo  from openxml(@hDoc, N'/ROOT/doc') with(nid_servicio int,nid_modelo  int)                                 
		  exec  sp_xml_removedocument @hDoc    
--------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------
        
          update mae_servicio_especifico_modelo set fl_inactivo=
          case when m.fl_inactivo = 'A' then 'I' else 'A' end,
          co_usuario_cambio = @co_usuario_crea,
          no_estacion_red=@no_estacion_red,
          no_usuario_red=@no_usuario_red, 
          fe_cambio = getdate()
		  from mae_servicio_especifico_modelo m inner join @table t
		  on m.nid_servicio = t.nid_servicio and m.nid_modelo = t.nid_modelo
		  
	
		  	insert into mae_servicio_especifico_modelo(
			nid_servicio,
			nid_modelo,
			fe_crea,
			co_usuario_crea,
			no_estacion_red,
			no_usuario_red)
			select nid_servicio,nid_modelo,getdate(),@co_usuario_crea,
			@no_estacion_red,
			@no_usuario_red from @table t where not exists
			(select 1   from mae_servicio_especifico_modelo m where m.nid_servicio = t.nid_servicio and
			 m.nid_modelo = t.nid_modelo)
			
		
    end

end
go