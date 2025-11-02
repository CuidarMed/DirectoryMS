-- Script para verificar si la columna Specialty existe en la tabla Doctor
USE [DirectoryMS];

-- Verificar si la columna existe
IF EXISTS (
    SELECT 1 
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_SCHEMA = 'dbo' 
    AND TABLE_NAME = 'Doctor' 
    AND COLUMN_NAME = 'Specialty'
)
BEGIN
    PRINT 'La columna Specialty EXISTE en la tabla Doctor';
    
    -- Mostrar información de la columna
    SELECT 
        COLUMN_NAME,
        DATA_TYPE,
        CHARACTER_MAXIMUM_LENGTH,
        IS_NULLABLE
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_SCHEMA = 'dbo'
    AND TABLE_NAME = 'Doctor'
    AND COLUMN_NAME = 'Specialty';
END
ELSE
BEGIN
    PRINT 'La columna Specialty NO EXISTE en la tabla Doctor';
    PRINT 'Necesitas aplicar la migración para agregar la columna.';
END
GO

-- Si la columna no existe, este script la crea
IF NOT EXISTS (
    SELECT 1 
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_SCHEMA = 'dbo' 
    AND TABLE_NAME = 'Doctor' 
    AND COLUMN_NAME = 'Specialty'
)
BEGIN
    PRINT 'Agregando columna Specialty a la tabla Doctor...';
    
    ALTER TABLE [dbo].[Doctor]
    ADD [Specialty] NVARCHAR(100) NULL;
    
    PRINT 'Columna Specialty agregada exitosamente.';
END
GO

-- Verificar todas las columnas de la tabla Doctor
SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    CHARACTER_MAXIMUM_LENGTH,
    IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA = 'dbo'
AND TABLE_NAME = 'Doctor'
ORDER BY ORDINAL_POSITION;
GO
