-- Script completo para verificar, agregar y corregir la columna Specialty
USE [DirectoryMS];
GO

PRINT '=== VERIFICACIÓN DE COLUMNA SPECIALTY ===';
GO

-- 1. Mostrar TODAS las columnas de la tabla Doctor actuales
PRINT 'Columnas actuales de la tabla Doctor:';
SELECT 
    COLUMN_NAME AS 'Nombre Columna',
    DATA_TYPE AS 'Tipo',
    CHARACTER_MAXIMUM_LENGTH AS 'Longitud',
    IS_NULLABLE AS 'Nullable'
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA = 'dbo'
AND TABLE_NAME = 'Doctor'
ORDER BY ORDINAL_POSITION;
GO

-- 2. Verificar si Specialty existe (con diferentes variaciones)
DECLARE @SpecialtyExists BIT = 0;
DECLARE @ColumnName NVARCHAR(255);

SELECT @ColumnName = COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA = 'dbo'
AND TABLE_NAME = 'Doctor'
AND COLUMN_NAME LIKE '%pecialty%';  -- Buscar cualquier variación

IF @ColumnName IS NOT NULL
BEGIN
    PRINT 'Se encontró una columna similar a Specialty: ' + @ColumnName;
    SET @SpecialtyExists = 1;
END
ELSE
BEGIN
    SELECT @SpecialtyExists = CASE 
        WHEN EXISTS (
            SELECT 1 
            FROM INFORMATION_SCHEMA.COLUMNS 
            WHERE TABLE_SCHEMA = 'dbo' 
            AND TABLE_NAME = 'Doctor' 
            AND COLUMN_NAME = 'Specialty'
        ) THEN 1 ELSE 0
    END;
END

IF @SpecialtyExists = 1
BEGIN
    PRINT 'La columna Specialty (o similar) EXISTE';
END
ELSE
BEGIN
    PRINT 'La columna Specialty NO EXISTE - Agregándola...';
    
    BEGIN TRY
        ALTER TABLE [dbo].[Doctor]
        ADD [Specialty] NVARCHAR(100) NULL;
        
        PRINT '✓ Columna Specialty agregada exitosamente.';
    END TRY
    BEGIN CATCH
        PRINT '✗ Error al agregar la columna Specialty:';
        PRINT ERROR_MESSAGE();
    END CATCH
END
GO

-- 3. Verificar nuevamente después del intento
PRINT '=== VERIFICACIÓN FINAL ===';
IF EXISTS (
    SELECT 1 
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_SCHEMA = 'dbo' 
    AND TABLE_NAME = 'Doctor' 
    AND COLUMN_NAME = 'Specialty'
)
BEGIN
    PRINT '✓ La columna Specialty EXISTE y está configurada correctamente.';
    
    -- Mostrar detalles
    SELECT 
        'Detalles de la columna Specialty:' AS Mensaje,
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
    PRINT '✗ ERROR: La columna Specialty NO EXISTE después del intento de agregarla.';
    PRINT 'Por favor, verifica los permisos y la conexión a la base de datos.';
END
GO

-- 4. Consulta de prueba usando la columna Specialty
PRINT '=== PRUEBA DE CONSULTA ===';
BEGIN TRY
    DECLARE @TestQuery NVARCHAR(MAX) = '
    SELECT TOP 1
        DoctorId,
        FirstName,
        LastName,
        [Specialty]
    FROM [dbo].[Doctor];';
    
    EXEC sp_executesql @TestQuery;
    
    PRINT '✓ Consulta ejecutada exitosamente - La columna Specialty funciona.';
END TRY
BEGIN CATCH
    PRINT '✗ Error al ejecutar consulta de prueba:';
    PRINT ERROR_MESSAGE();
END CATCH
GO

-- 5. Instrucciones para refrescar IntelliSense en SSMS
PRINT '=== INSTRUCCIONES PARA SSMS ===';
PRINT 'Si SSMS sigue mostrando "Invalid column name", intenta:';
PRINT '1. Cerrar y reabrir SSMS completamente';
PRINT '2. En el Query Editor, presiona Ctrl+Shift+R (Refresh IntelliSense)';
PRINT '3. O ve a: Edit > IntelliSense > Refresh Local Cache';
PRINT '4. O ejecuta manualmente:';
PRINT '   USE [DirectoryMS];'
PRINT '   EXEC sp_refreshsqlmodule ''[dbo].[Doctor]'';'
GO

PRINT '=== SCRIPT COMPLETADO ===';
GO
