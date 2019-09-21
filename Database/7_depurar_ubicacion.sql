use AppMiTaller
go

select
ROW_NUMBER() over(order by GETDATE()) as nu_item,
nid_ubica
into #tmpUbica
from mae_ubicacion

update ubi
set 
no_ubica = 'UBICACION ' + convert(varchar(20), tmp.nu_item),
no_ubica_corto = 'UBICACION ' + convert(varchar(20), tmp.nu_item)
from mae_ubicacion ubi
inner join #tmpUbica tmp on tmp.nid_ubica = ubi.nid_ubica

drop table #tmpUbica

go