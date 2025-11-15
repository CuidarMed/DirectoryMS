-- Script para actualizar el paciente existente con UserId 4001
-- Agregar los datos de Obra Social y Número de Afiliado

USE [DirectoryMS]; -- Reemplaza con el nombre de tu base de datos si es diferente

-- Verificar el paciente actual
SELECT 
    PatientId,
    UserId,
    Name,
    LastName,
    HealthPlan,
    MembershipNumber
FROM [dbo].[Patient]
WHERE [UserId] = 4001;

-- Si el paciente existe y los campos están vacíos, actualizarlos
-- NOTA: Esto es solo un ejemplo. Ajusta los valores según tus necesidades
-- UPDATE [dbo].[Patient]
-- SET 
--     [HealthPlan] = N'Galeno',  -- Cambia por el valor que necesites
--     [MembershipNumber] = N'12345678'  -- Cambia por el valor que necesites
-- WHERE [UserId] = 4001;

PRINT 'Verifica los datos arriba y descomenta el UPDATE si necesitas actualizar los valores.';
GO
