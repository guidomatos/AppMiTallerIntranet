use AppMiTaller
go

update OPM
set fl_ind_visible = '1'
where CSTRUCT = '130301' --Administrar Citas
and fl_ind_visible = '0'
go

update mae_parametros
set fl_activo = 'I'
where
co_parametro = 'PARAMETRO_SYS_11'
and fl_activo = 'A'
go

update mae_parametros
set fl_activo = 'I'
where
co_parametro = 'PARAMETRO_SYS_2'
and fl_activo = 'A'
go

update mae_parametros
set fl_activo = 'I'
where
co_parametro = 'PARAMETRO_SYS_3'
and fl_activo = 'A'
go

update mae_parametros
set fl_activo = 'I'
where
co_parametro = 'PARAMETRO_SYS_4'
and fl_activo = 'A'
go

update mae_parametros
set fl_valor3 = '1'
where co_parametro = 'PARAMETRO_SYS_12'
and fl_valor3 = '0'
go

update mae_tabla_detalle
set no_valor1 = 'Pasaporte'
where no_valor2 = '04'
and nid_tabla_gen = 40
and no_valor1 != 'Pasaporte'
go

----------------------------------------------------

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
		o.CSTRUCT = '1303'
		or
		o.CSTRUCT = '130301'
		or
		o.CSTRUCT like '130301__'
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
		o.CSTRUCT = '1303'
		or
		o.CSTRUCT = '130301'
		or
		o.CSTRUCT like '130301__'
	)
	and uo.nid_usr_opm IS NULL
	
end
go