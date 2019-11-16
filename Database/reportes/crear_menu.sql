use AppMiTaller
go


declare @id_opcion_1 int; --Reporte por Marca
declare @id_opcion_2 int; --Reporte por Asesor
declare @co_opcion varchar(20);
set @co_opcion = '130401'

select @id_opcion_1 = nid_opcion from OPM where CSTRUCT = @co_opcion and fl_inactivo = '0'

if (@id_opcion_1 IS NULL)
begin

	insert OPM
	(
		CCOAPL, CSTRUCT, VDEMEN, no_pagina_html, fl_inactivo, fl_ind_visible
	)
	values
	(
		'003', @co_opcion, 'Citas Atendidas por Marca', 'SRC_Reportes/Citas_Atendidas_Por_Marca.aspx', '0', '1'
	)

	set @id_opcion_1 = @@IDENTITY

end

set @co_opcion = '130402'

select @id_opcion_2 = nid_opcion from OPM where CSTRUCT = @co_opcion and fl_inactivo = '0'

if (@id_opcion_2 IS NULL)
begin

	insert OPM
	(
		CCOAPL, CSTRUCT, VDEMEN, no_pagina_html, fl_inactivo, fl_ind_visible
	)
	values
	(
		'003', @co_opcion, 'Citas Atendidas por Asesor', 'SRC_Reportes/Citas_Atendidas_Por_Asesor.aspx', '0', '1'
	)

	set @id_opcion_2 = @@IDENTITY

end

declare @vl_nid_perfil int;
select
@vl_nid_perfil = nid_perfil
from PRF where co_perfil_usuario = 'ATAL'

if (@vl_nid_perfil IS NOT NULL)
begin

	insert OPP
	(
	CCOAPL,
	nid_opcion,
	nid_perfil,
	fl_inactivo,
	CTIACCESO
	)
	select
	'003',
	o.nid_opcion,
	@vl_nid_perfil,
	'0',
	'A'
	from OPM o
	LEFT JOIN OPP opp on 
			o.nid_opcion = opp.nid_opcion 
		and ISNULL(opp.CTIACCESO,'C') = 'A'
		and opp.nid_perfil = @vl_nid_perfil
		and opp.fl_inactivo = '0' 
	where 
		o.fl_inactivo = '0'
	and 
	(
		o.CSTRUCT IN ( '13', '1304')
		or
		o.nid_opcion IN ( @id_opcion_1, @id_opcion_2 )
	)
	and opp.nid_opcion_perfil IS NULL

	insert mae_usr_opm
	(
	nid_usuario,
	nid_opcion,
	fl_perfil,
	fl_inactivo,
	fl_tipo_acceso
	)
	select
	u.nid_usuario,
	o.nid_opcion,
	'1',
	'0',
	'A'
	from OPM o
	INNER JOIN PRFUSR pu on pu.nid_perfil = @vl_nid_perfil and pu.fl_inactivo = '0'
	INNER JOIN USR u on u.nid_usuario = pu.nid_usuario
	LEFT JOIN mae_usr_opm uo on 
			uo.nid_opcion = o.nid_opcion
		and uo.nid_usuario = u.nid_usuario
		and ISNULL(uo.fl_tipo_acceso,'C') = 'A'
		and uo.fl_inactivo = '0'
	where
		o.fl_inactivo = '0'
	and 
	(
		o.CSTRUCT IN ( '13', '1304')
		or
		o.nid_opcion IN ( @id_opcion_1, @id_opcion_2 )
	)
	and uo.nid_usr_opm IS NULL
	
end
go