use AppMiTaller
go

delete from mae_usr_modelo
where nid_modelo IN (
	select nid_modelo from mae_modelo
	where nid_negocio_subfamilia IN (
		select nid_negocio_subfamilia from mae_negocio_subfamilia
		where nid_negocio_linea IN (
			select nid_negocio_linea from mae_negocio_linea
			where co_negocio IN ('ACC','MAQ','SN')
			or co_familia IN ('SN', 'TR', 'TRA', 'TRI', 'TRN', 'UTI','PRO','MTC', 'MON', 'MNV', 'MI' )
			or co_familia IN ('EXC', 'GNV','GRU','IND','LAB','AGR','ALI','API','CO','CON','COS','CT','CTR','001','002','003','004','AG ','LI ')
		)
	)
)

delete from mae_usr_modelo
where nid_modelo IN (
	select nid_modelo from mae_modelo
	where nid_negocio_subfamilia IN (
		select nid_negocio_subfamilia from mae_negocio_subfamilia
		where co_familia IN (
			select co_familia from mae_familia_vehiculo
			where co_familia IN ('SN', 'TR', 'TRA', 'TRI', 'TRN', 'UTI','PRO','MTC', 'MON', 'MNV', 'MI' )
			or co_familia IN ('EXC', 'GNV','GRU','IND','LAB','AGR','ALI','API','CO','CON','COS','CT','CTR','001','002','003','004','AG ','LI ')
		)
	)
)

delete from mae_servicio_especifico_modelo
where nid_modelo IN (
	select nid_modelo from mae_modelo
	where nid_negocio_subfamilia IN (
		select nid_negocio_subfamilia from mae_negocio_subfamilia
		where nid_negocio_linea IN (
			select nid_negocio_linea from mae_negocio_linea
			where co_negocio IN ('ACC','MAQ','SN')
			or co_familia IN ('SN', 'TR', 'TRA', 'TRI', 'TRN', 'UTI','PRO','MTC', 'MON', 'MNV', 'MI' )
			or co_familia IN ('EXC', 'GNV','GRU','IND','LAB','AGR','ALI','API','CO','CON','COS','CT','CTR','001','002','003','004','AG ','LI ')
		)
	)
)

delete from mae_servicio_especifico_modelo
where nid_modelo IN (
	select nid_modelo from mae_modelo
	where nid_negocio_subfamilia IN (
		select nid_negocio_subfamilia from mae_negocio_subfamilia
		where co_familia IN (
			select co_familia from mae_familia_vehiculo
			where co_familia IN ('SN', 'TR', 'TRA', 'TRI', 'TRN', 'UTI','PRO','MTC', 'MON', 'MNV', 'MI' )
			or co_familia IN ('EXC', 'GNV','GRU','IND','LAB','AGR','ALI','API','CO','CON','COS','CT','CTR','001','002','003','004','AG ','LI ')
		)
	)
)


delete from mae_taller_modelo
where nid_modelo IN (
	select
	mo.nid_modelo
	from mae_modelo mo
	inner join mae_negocio_linea lin on lin.nid_negocio_linea = mo.nid_negocio_linea
	where co_negocio IN ('ACC','MAQ','SN')
	or lin.co_familia IN ('SN', 'TR', 'TRA', 'TRI', 'TRN', 'UTI','PRO','MTC', 'MON', 'MNV', 'MI' )
	or lin.co_familia IN ('EXC', 'GNV','GRU','IND','LAB','AGR','ALI','API','CO','CON','COS','CT','CTR','001','002','003','004','AG ','LI ')
)

delete from mae_taller_modelo
where nid_modelo IN (
	select nid_modelo from mae_modelo
	where nid_negocio_subfamilia IN (
		select nid_negocio_subfamilia from mae_negocio_subfamilia
		where co_familia IN (
			select co_familia from mae_familia_vehiculo
			where co_familia IN ('SN', 'TR', 'TRA', 'TRI', 'TRN', 'UTI','PRO','MTC', 'MON', 'MNV', 'MI' )
			or co_familia IN ('EXC', 'GNV','GRU','IND','LAB','AGR','ALI','API','CO','CON','COS','CT','CTR','001','002','003','004','AG ','LI ')
		)
	)
)


delete from mae_modelo
where nid_modelo IN (
	select
	mo.nid_modelo
	from mae_modelo mo
	inner join mae_negocio_linea lin on lin.nid_negocio_linea = mo.nid_negocio_linea
	where lin.co_negocio IN ('ACC','MAQ','SN')
	or lin.co_familia IN ('SN', 'TR', 'TRA', 'TRI', 'TRN', 'UTI','PRO','MTC', 'MON', 'MNV', 'MI' )
	or lin.co_familia IN ('EXC', 'GNV','GRU','IND','LAB','AGR','ALI','API','CO','CON','COS','CT','CTR','001','002','003','004','AG ','LI ')
)

delete from mae_modelo
where nid_negocio_subfamilia IN (
	select nid_negocio_subfamilia from mae_negocio_subfamilia
	where nid_negocio_linea IN (
		select nid_negocio_linea from mae_negocio_linea
		where co_negocio IN ('ACC','MAQ','SN')
		or co_familia IN ('SN', 'TR', 'TRA', 'TRI', 'TRN', 'UTI','PRO','MTC', 'MON', 'MNV', 'MI' )
		or co_familia IN ('EXC', 'GNV','GRU','IND','LAB','AGR','ALI','API','CO','CON','COS','CT','CTR','001','002','003','004','AG ','LI ')
	)
)

delete from mae_negocio_subfamilia
where nid_negocio_linea IN (
	select nid_negocio_linea from mae_negocio_linea
	where co_negocio IN ('ACC','MAQ','SN')
	or co_familia IN ('SN', 'TR', 'TRA', 'TRI', 'TRN', 'UTI','PRO','MTC', 'MON', 'MNV', 'MI' )
	or co_familia IN ('EXC', 'GNV','GRU','IND','LAB','AGR','ALI','API','CO','CON','COS','CT','CTR','001','002','003','004','AG ','LI ')
)

delete from mae_modelo
where nid_negocio_subfamilia IN (
	select nid_negocio_subfamilia from mae_negocio_subfamilia
	where co_familia IN (
		select co_familia from mae_familia_vehiculo
		where co_familia IN ('SN', 'TR', 'TRA', 'TRI', 'TRN', 'UTI','PRO','MTC', 'MON', 'MNV', 'MI' )
		or co_familia IN ('EXC', 'GNV','GRU','IND','LAB','AGR','ALI','API','CO','CON','COS','CT','CTR','001','002','003','004','AG ','LI ')
	)
)

delete from mae_negocio_subfamilia
where co_familia IN (
	select co_familia from mae_familia_vehiculo
	where co_familia IN ('SN', 'TR', 'TRA', 'TRI', 'TRN', 'UTI','PRO','MTC', 'MON', 'MNV', 'MI' )
	or co_familia IN ('EXC', 'GNV','GRU','IND','LAB','AGR','ALI','API','CO','CON','COS','CT','CTR','001','002','003','004','AG ','LI ')
)

delete from mae_negocio_linea
where co_negocio IN ('ACC','MAQ','SN')
or co_familia IN ('SN', 'TR', 'TRA', 'TRI', 'TRN', 'UTI','PRO','MTC', 'MON', 'MNV', 'MI' )
or co_familia IN ('EXC', 'GNV','GRU','IND','LAB','AGR','ALI','API','CO','CON','COS','CT','CTR','001','002','003','004','AG ','LI ')

delete from mae_negocio where co_negocio IN ('ACC','MAQ','SN')

delete from mae_familia_vehiculo
where co_familia IN ('SN', 'TR', 'TRA', 'TRI', 'TRN', 'UTI','PRO','MTC', 'MON', 'MNV', 'MI' )
or co_familia IN ('EXC', 'GNV','GRU','IND','LAB','AGR','ALI','API','CO','CON','COS','CT','CTR','001','002','003','004','AG ','LI ')