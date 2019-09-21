use AppMiTaller
go

update USR
set
CESTBLQ = '0',
fe_inicio_acceso = GETDATE(),
fe_fin_acceso = GETDATE() + 360,
VUSR_PASS = '80-/BD', --123456
VPASSMD5 = 'CE-0B-FD-15-05-9B-68-D6-76-88-88-4D-7A-3D-3E-8C', --123456
fl_reset = '0' --1: Debe cambiar contraseña en el 1er login
go