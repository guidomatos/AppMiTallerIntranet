use AppMiTaller
go

IF NOT EXISTS(
        SELECT 1
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE 
			TABLE_SCHEMA = 'dbo'
		AND TABLE_NAME = 'mae_cliente' 
		AND COLUMN_NAME = 'tx_direccion'
    )
    BEGIN
        ALTER TABLE dbo.mae_cliente
		ADD tx_direccion varchar(255) NULL
    END
GO

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
	ADD no_clave_web varchar(100) NULL
END
ELSE
BEGIN
	ALTER TABLE dbo.mae_cliente
	ALTER COLUMN no_clave_web varchar(100) NULL
END
GO