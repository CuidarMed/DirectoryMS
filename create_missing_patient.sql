-- Script para crear el paciente faltante para el usuario con UserId 4001
-- Basado en los datos de la tabla Users: Hernan Parma, DNI 32896920

USE [DirectoryMS]; -- Cambia esto por el nombre de tu base de datos

-- Verificar si el paciente ya existe
IF EXISTS (SELECT 1 FROM [dbo].[Patient] WHERE [UserId] = 4001)
BEGIN
    PRINT 'El paciente con UserId 4001 ya existe.';
    SELECT * FROM [dbo].[Patient] WHERE [UserId] = 4001;
END
ELSE
BEGIN
    -- Crear el paciente
    INSERT INTO [dbo].[Patient] 
    (
        [Dni], 
        [Name], 
        [LastName], 
        [Adress], 
        [DateOfBirth], 
        [HealthPlan], 
        [MembershipNumber], 
        [UserId]
    )
    VALUES 
    (
        32896920,                    -- DNI
        N'Hernan',                   -- Name (FirstName)
        N'Parma',                    -- LastName
        N'',                         -- Adress (vacío por ahora)
        CAST(DATEADD(YEAR, -30, GETDATE()) AS DATE), -- DateOfBirth (30 años atrás)
        N'',                         -- HealthPlan (vacío por ahora)
        N'',                         -- MembershipNumber (vacío por ahora)
        4001                         -- UserId
    );
    
    PRINT 'Paciente creado exitosamente para UserId 4001.';
    SELECT * FROM [dbo].[Patient] WHERE [UserId] = 4001;
END
GO
