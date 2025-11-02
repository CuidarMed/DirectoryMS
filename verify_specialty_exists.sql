-- Script completo para verificar y corregir la columna Specialty
USE [DirectoryMS];
GO

-- 1. Verificar si la tabla Doctor existe
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Doctor')
BEGIN
    PRINT 'La tabla Doctor EXISTE';
END
ELSE
BEGIN
    PRINT 'ERROR: La tabla Doctor NO EXISTE';
    RETURN;
END
GO

-- 2. Mostrar todas las columnas de la tabla Doctor
PRINT 'Columnas actuales de la tabla Doctor:';
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

-- 3. Verificar específicamente si Specialty existe
IF EXISTS (
    SELECT 1 
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_SCHEMA = 'dbo' 
    AND TABLE_NAME = 'Doctor' 
    AND COLUMN_NAME = 'Specialty'
)
BEGIN
    PRINT 'La columna Specialty EXISTE en la tabla Doctor';
    
    -- Mostrar detalles de la columna
    SELECT 
        'Columna Specialty encontrada:' AS Mensaje,
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
    PRINT 'La columna Specialty NO EXISTE - Intentando agregarla...';
    
    -- Intentar agregar la columna
    BEGIN TRY
        ALTER TABLE [dbo].[Doctor]
        ADD [Specialty] NVARCHAR(100) NULL;
        
        PRINT 'Columna Specialty agregada exitosamente.';
    END TRY
    BEGIN CATCH
        PRINT 'Error al agregar la columna Specialty:';
        PRINT ERROR_MESSAGE();
    END CATCH
END
GO

-- 4. Intentar una consulta de prueba para verificar que funciona
PRINT 'Ejecutando consulta de prueba...';
BEGIN TRY
    SELECT TOP 1
        DoctorId,
        FirstName,
        LastName,
        [Specialty]  -- Usando corchetes por si acaso
    FROM [dbo].[Doctor];
    
    PRINT 'Consulta ejecutada exitosamente - La columna Specialty funciona correctamente.';
END TRY
BEGIN CATCH
    PRINT 'Error al ejecutar consulta:';
    PRINT ERROR_MESSAGE();
END CATCH
GO

-- 5. Si SSMS sigue mostrando error de IntelliSense, intentar refrescar
PRINT 'Si SSMS sigue mostrando error, intenta:';
PRINT '1. Cerrar y reabrir SSMS';
PRINT '2. Presionar Ctrl+Shift+R para refrescar IntelliSense';
PRINT '3. Ejecutar: EXEC sp_refreshsqlmodule ''[dbo].[Doctor]''';
GO

-- Intentar refrescar el caché
EXEC sp_refreshsqlmodule '[dbo].[Doctor]';
GO
