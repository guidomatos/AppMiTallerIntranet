use AppMiTaller
go

if exists(select 1 from sysobjects where name = 'src_sps_reporte_citas_atendidas_por_marca' and type = 'P')
begin
	drop proc src_sps_reporte_citas_atendidas_por_marca
end
go

create procedure src_sps_reporte_citas_atendidas_por_marca
/*
--Estados de Cita
select*from mae_tabla_detalle where nid_tabla_gen = 154

--Citas Confirmadas
select
cita.nid_cita,
mar.no_marca
from tbl_cita cita
inner join tbl_estado_cita ec on ec.nid_cita = cita.nid_cita and ec.fl_activo = 'A'
inner join mae_marca mar on mar.nid_marca = cita.nid_marca
where
	cita.fl_activo = 'A'
and ec.co_estado_cita = 994 --Confirmada

--Actualizar a Atendida para pruebas

update ec
set ec.co_estado_cita = 996
from tbl_cita cita
inner join tbl_estado_cita ec on ec.nid_cita = cita.nid_cita and ec.fl_activo = 'A'
where
ec.co_estado_cita = 994 --Confirmada

Test:
exec src_sps_reporte_citas_atendidas_por_marca @vi_nid_usuario=6725
exec src_sps_reporte_citas_atendidas_por_marca @vi_nid_usuario=8175
*/
(
@vi_nid_usuario int
)
as
begin

	--Taller(es) gestionados por Jefe de Taller
	declare @tmpTallerUsuario as table
	(
		nid_taller int
	)

	insert @tmpTallerUsuario
	(nid_taller)
	select
	tal.nid_taller
	from mae_taller tal
	inner join mae_usr_taller utal on utal.nid_taller = tal.nid_taller and utal.fl_activo = 'A'
	where 
		tal.fl_activo = 'A'
	and utal.nid_usuario = @vi_nid_usuario

 
	select
	mar.no_marca,
	COUNT(cita.nid_cita) as qt_cita
	from tbl_cita cita
	inner join tbl_estado_cita ec on ec.nid_cita = cita.nid_cita and ec.fl_activo = 'A'
	inner join mae_marca mar on mar.nid_marca = cita.nid_marca
	where
		cita.fl_activo = 'A'
	and ec.co_estado_cita = 996 --Atendida
	and cita.nid_taller IN (select nid_taller from @tmpTallerUsuario)
	group by mar.no_marca

end
go

if exists(select 1 from sysobjects where name = 'src_sps_reporte_citas_atendidas_por_asesor' and type = 'P')
begin
	drop proc src_sps_reporte_citas_atendidas_por_asesor
end
go

create procedure src_sps_reporte_citas_atendidas_por_asesor
/*
Test:
exec src_sps_reporte_citas_atendidas_por_asesor @vi_nid_usuario=6725
exec src_sps_reporte_citas_atendidas_por_asesor @vi_nid_usuario=8175
*/
(
@vi_nid_usuario int
)
as
begin

	--Taller(es) gestionados por Jefe de Taller
	declare @tmpTallerUsuario as table
	(
		nid_taller int
	)

	insert @tmpTallerUsuario
	(nid_taller)
	select
	tal.nid_taller
	from mae_taller tal
	inner join mae_usr_taller utal on utal.nid_taller = tal.nid_taller and utal.fl_activo = 'A'
	where 
		tal.fl_activo = 'A'
	and utal.nid_usuario = @vi_nid_usuario
 
	select
	no_asesor,
	COUNT(nid_cita) as qt_cita
	from
	(
		select
		asesor.VNOMUSR + ' ' + asesor.no_ape_paterno + ' ' + ISNULL(asesor.no_ape_materno,'') as no_asesor,
		cita.nid_cita
		from tbl_cita cita
		inner join tbl_estado_cita ec on ec.nid_cita = cita.nid_cita and ec.fl_activo = 'A'
		inner join USR asesor on asesor.nid_usuario = ec.nid_usuario
		where
			cita.fl_activo = 'A'
		and ec.co_estado_cita = 996 --Atendida
		and cita.nid_taller IN (select nid_taller from @tmpTallerUsuario)
	) as tbl
	group by no_asesor

end
go