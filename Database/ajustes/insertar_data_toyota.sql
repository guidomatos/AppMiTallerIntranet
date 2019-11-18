declare @vl_nid_empresa int
select
@vl_nid_empresa = nid_empresa
from mae_empresa where nu_ruc = '20100132592' and fl_inactivo = '0'

if (@vl_nid_empresa IS NULL)
begin

	insert mae_empresa
	(no_empresa, nu_ruc, fl_inactivo, no_empresa_corto)
	values
	('Toyota', '20100132592', '0', 'Toyota')

end

insert mae_marca
(
nid_empresa, co_marca, no_marca, fl_inactivo
)
values
( @vl_nid_empresa, 'Scion', 'Scion', '0' ),
( @vl_nid_empresa, 'Daihatsu', 'Daihatsu', '0' ),
( @vl_nid_empresa, 'Toyota', 'Toyota', '0' )