use AppMiTaller
go

declare @vl_nid_usuario int
declare @vl_co_usuario varchar(20)
set @vl_co_usuario = 'admintaller109'
--set @vl_co_usuario = 'asesorserv146'

/*
select*from mae_taller where no_taller like 'taller 71'
*/

select @vl_nid_usuario = nid_usuario from USR where CUSR_ID = @vl_co_usuario

select
p.nid_perfil, p.co_perfil_usuario, p.VDEPRF, u.CUSR_ID, VUSR_PASS, VPASSMD5
,u.nid_ubica, ubi.no_ubica_corto
from USR u
inner join PRFUSR pu on pu.nid_usuario = u.nid_usuario
inner join PRF p on p.nid_perfil = pu.nid_perfil
left join mae_ubicacion ubi on ubi.nid_ubica = u.nid_ubica
WHERE
	u.fl_inactivo = '0'
and u.CUSR_ID = @vl_co_usuario

select
prf.nid_perfil, prf.co_perfil_usuario, prf.VDEPRF,
usu.nid_usuario, usu.CUSR_ID, usu.VNOMUSR, usu.no_ape_paterno,
tal.nid_taller, tal.no_taller, tal.nid_ubica, ubiTal.no_ubica_corto as no_ubica_taller
from mae_taller tal
inner join mae_usr_taller utal on utal.nid_taller = tal.nid_taller and utal.fl_activo = 'A'
inner join USR usu on usu.nid_usuario = utal.nid_usuario
inner join PRFUSR pu on pu.nid_usuario = usu.nid_usuario and pu.fl_inactivo = '0'
inner join PRF prf on prf.nid_perfil = pu.nid_perfil
inner join mae_ubicacion ubiTal on ubiTal.nid_ubica = tal.nid_ubica
where 
	tal.fl_activo = 'A'
and tal.no_taller = 'TALLER 71'
--and prf.co_perfil_usuario = 'ATAL'
and utal.nid_usuario = @vl_nid_usuario


select*from USR where nid_usuario = 9352

select*from OPM where CSTRUCT like '1303__'

select*from OPM where CSTRUCT = '13'
select*from OPM where CSTRUCT = '1303'
select*from OPM where CSTRUCT = '130301'
select*from OPM where CSTRUCT like '130301__'

select*from OPP where nid_perfil = 65

select*from OPP where nid_opcion IN (
	select nid_opcion from OPM where CSTRUCT like '130301__'
)
and nid_perfil = 65

