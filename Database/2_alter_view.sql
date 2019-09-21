use AppMiTaller
go

ALTER VIEW v_Listado_Tipo_Usuario_BO
/*****************************************************************************
Objetivo		: Lista los talleres, departamentos, provincias y distritos donde se encuentra cada uno
SELECT*FROM [v_Listado_Tipo_Usuario_BO]
****************************************************************************/
AS
	SELECT DISTINCT 
	a.no_valor2, a.no_valor1
	FROM mae_tabla_detalle a 
	INNER JOIN usr b ON a.no_valor2 = b.nid_cod_tipo_usuario
	where
		a.nid_tabla_gen = 10
	and a.fl_inactivo='0'
GO