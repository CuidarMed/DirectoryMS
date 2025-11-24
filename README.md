# DirectoryMS

DirectoryMS es el microservicio encargado de **gestionar la información estructural del sistema CuidarMed**, incluyendo datos de médicos, pacientes, especialidades, centros de salud y cualquier otro recurso que describe "quién es quién" dentro de la plataforma.

Su propósito principal es centralizar toda la **información de directorio**, garantizar su consistencia y ponerla a disposición de otros microservicios como `SchedulingMS`, `AuthMS` y `ClinicalMS`.

---

## 🧾 ¿Qué gestiona DirectoryMS?

DirectoryMS se encarga de almacenar y exponer datos como:

- **Médicos** (perfil profesional, matrículas, especialidades)
- **Pacientes** (información demográfica y de contacto)

---

## 🏗 Funcionalidades clave

- Crear, leer, actualizar y eliminar información de médicos y pacientes  
- Exposición de datos estructurados para uso en otros MS  
- Validación de datos con FluentValidation  
- Endpoints REST estandarizados con respuesta en JSON  
- Documentación automática con Swagger  

---

## ⚙️ Tecnologías utilizadas

- **.NET 9 / ASP.NET Core**
- **Entity Framework Core**
- **SQL Server**
- **FluentValidation**
- **Swagger / OpenAPI**
- **Docker** para BD y despliegue
- **Localización** en español (`es-US`)

---

## 💾 Base de Datos

DirectoryMS utiliza SQL Server como base de datos.  
- Tablas principales:
| Tabla | Descripción |
|-------|-------------|
| `Doctors` | Datos de los médicos registrados (nombre, matrícula, especialidad principal, contacto, estado activo) |
| `Patients` | Información básica de los pacientes (nombre, DNI, teléfono, email, fecha de nacimiento, especialidad) 

---
## 🚀 Instalación

1. Clonar el repositorio:

```bash
git clone https://github.com/tu-usuario/DirectoryMS.git
cd DirectoryMS
```
2. Levantar el servicio con Docker Compose:
```bash
dotnet docker compose up --build
```
3. Si no usas Docker -> Configurar la cadena de conexión en appsettings.json:
```bash
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1434;Database=DirectoryDB;User Id=sa;Password=TuPassword123!;"
  }
}

```
4. Aplicar migraciones
```bash
dotnet ef database update
```
5. Ejecutar la aplicación
```bash
dotnet run
```
6. Acceder a Swagger para explorar la API:
- Si usas Docker
```bash
http://localhost:8081/swagger/index.html
```
- Si usas appsettings.json. El puerto (5001) va a variar según los que tengas disponibles
```bash
https://localhost:5001/swagger
```

## 🔗 Integración con otros microservicios
👉 SchedulingMS

Para:

- Verificar qué médicos existen

- Obtener sus especialidades

👉 AuthMS

Para:

- Complementar información de usuario con perfil médico o paciente

👉 ClinicalMS

Para:

- Obtener datos demográficos del paciente

- Datos profesionales del médico
