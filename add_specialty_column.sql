-- Script para agregar la columna Specialty a la tabla Doctor
-- Ejecuta este script en SQL Server Management Studio

USE [DirectoryMS];
GO

-- Verificar si la columna ya existe
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
ELSE
BEGIN
    PRINT 'La columna Specialty ya existe en la tabla Doctor.';
END
GO

-- Verificar que la columna se agregó correctamente
SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    CHARACTER_MAXIMUM_LENGTH,
    IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA = 'dbo'
AND TABLE_NAME = 'Doctor'
AND COLUMN_NAME = 'Specialty';
GO