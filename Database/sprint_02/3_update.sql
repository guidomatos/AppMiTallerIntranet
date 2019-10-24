use AppMiTaller
go

update OPM
set fl_ind_visible = '1'
where CSTRUCT = '130301' --Administrar Citas
go

update mae_parametros
set fl_activo = 'I'
where
co_parametro = 'PARAMETRO_SYS_11'
go

update mae_parametros
set fl_activo = 'I'
where
co_parametro = 'PARAMETRO_SYS_2'
go

update mae_parametros
set fl_activo = 'I'
where
co_parametro = 'PARAMETRO_SYS_3'
go

update mae_parametros
set fl_activo = 'I'
where
co_parametro = 'PARAMETRO_SYS_4'
go

update mae_parametros
set fl_valor3 = '1'
where co_parametro = 'PARAMETRO_SYS_12'
go