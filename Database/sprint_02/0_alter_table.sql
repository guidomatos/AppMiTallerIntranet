use AppMiTaller
go

IF NOT EXISTS(
        SELECT 1
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE 
			TABLE_SCHEMA = 'dbo'
		AND TABLE_NAME = 'mae_cliente' 
		AND COLUMN_NAME = 'no_clave_web'
    )
    BEGIN
        ALTER TABLE dbo.mae_cliente
		ADD no_clave_web varchar(20) NULL
    END
GO