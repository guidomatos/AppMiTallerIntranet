use AppMiTaller
go

select
p.nid_perfil, p.VDEPRF, u.CUSR_ID, VUSR_PASS, VPASSMD5
from USR u
inner join PRFUSR pu on pu.nid_usuario = u.nid_usuario
inner join PRF p on p.nid_perfil = pu.nid_perfil
WHERE
u.fl_inactivo = '0'