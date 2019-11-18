if exists(
	select 1 from mae_marca where co_marca = 'SN'
)
begin
	delete from mae_marca where co_marca = 'SN'
end
go

if exists(
	select 1 from mae_marca where co_marca = 'SM1601'
)
begin
	delete from mae_marca where co_marca = 'SM1601'
end
go

update mae_empresa
set no_empresa = 'Gildemeister'
where nid_empresa = 1

update mae_empresa
set no_empresa = 'Manasa'
where nid_empresa = 2

update mae_empresa
set no_empresa = 'Motormundo'
where nid_empresa = 3
go

declare @sqlForeignKey varchar(max) = ''
SELECT @sqlForeignKey =
    'ALTER TABLE [' +  OBJECT_SCHEMA_NAME(parent_object_id) +
    '].[' + OBJECT_NAME(parent_object_id) + 
    '] DROP CONSTRAINT [' + name + ']'
FROM sys.foreign_keys
WHERE referenced_object_id = object_id('mae_importador')

exec (@sqlForeignKey)

go

if exists(
	select 1 from sys.tables where name = 'mae_importador'
)
begin
	drop table mae_importador
end
go


IF EXISTS(
        SELECT 1
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE 
			TABLE_SCHEMA = 'dbo'
		AND TABLE_NAME = 'mae_marca' 
		AND COLUMN_NAME = 'nid_importador'
    )
    BEGIN
        alter table mae_marca
		drop column nid_importador
    END
GO

IF EXISTS(
        SELECT 1
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE 
			TABLE_SCHEMA = 'dbo'
		AND TABLE_NAME = 'mae_marca' 
		AND COLUMN_NAME = 'logo'
    )
    BEGIN
        alter table mae_marca
		drop column logo
    END
GO

IF EXISTS(
        SELECT 1
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE 
			TABLE_SCHEMA = 'dbo'
		AND TABLE_NAME = 'mae_marca' 
		AND COLUMN_NAME = 'imagen_logo'
    )
    BEGIN
        alter table mae_marca
		drop column imagen_logo
    END
GO

IF EXISTS(
        SELECT 1
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE 
			TABLE_SCHEMA = 'dbo'
		AND TABLE_NAME = 'mae_marca' 
		AND COLUMN_NAME = 'qt_tamanio'
    )
    BEGIN
        alter table mae_marca
		drop column qt_tamanio
    END
GO