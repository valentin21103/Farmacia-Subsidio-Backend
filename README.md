# ⚙️ Farmacia Subsidio - Backend API (.NET Core)

Este repositorio contiene la **API RESTful** del sistema de gestión de subsidios farmacéuticos. Está construido sobre **.NET Core** utilizando una arquitectura en capas limpia y desacoplada para garantizar escalabilidad y mantenimiento.

> **Nota:** Este es el Backend. Para ver el Frontend (Angular), visita: [Enlace a tu repositorio Frontend]

## 🏗️ Arquitectura del Proyecto

El proyecto sigue un diseño de **Arquitectura Limpia (Clean Architecture)** dividido en 4 capas principales, visibles en la solución:

### 1. 🌐 subsidio.API (Capa de Presentación)
Es el punto de entrada de la aplicación.
- Contiene los **Controllers** que exponen los endpoints REST.
- Configura la inyección de dependencias y el `Program.cs`.
- Maneja la autenticación y la documentación con **Swagger**.

### 2. 🧠 subsidio.Business (Capa de Lógica de Negocio)
Aquí reside la inteligencia del sistema.
- **Services:** Implementan la lógica de validación (ej: calcular descuentos, validar stocks, generar lógica de tickets).
- Se comunica con la capa de infraestructura pero no depende de la base de datos directamente.

### 3. 📦 subsidio.Dominio (Capa de Entidades/Core)
Es el núcleo del proyecto y no tiene dependencias externas.
- **Entities:** Modelos que representan las tablas de la base de datos (Medicamentos, Usuarios, Solicitudes).
- **DTOs:** Objetos de transferencia de datos para limpiar lo que enviamos al frontend.
- **Enums:** Definiciones estáticas (ej: Estado de solicitud).

### 4. 🗄️ subsidio.Infraestructura (Capa de Datos)
Maneja la persistencia y comunicación con la Base de Datos.
- **Data / DbContext:** Configuración de Entity Framework Core.
- **Migrations:** Historial de cambios en la estructura de la base de datos (Code First).
- **Repositories:** Abstracción del acceso a datos.

---

## 🚀 Tecnologías Clave

- **Framework:** .NET 6 / 7 / 8 (Core)
- **ORM:** Entity Framework Core (Code First Approach)
- **Base de Datos:** SQL Server
- **Documentación:** Swagger UI (Swashbuckle)
- **Seguridad:** JWT (JSON Web Tokens) para manejo de roles.

---
## 🗄️ Configuración de la Base de Datos (Migrations)

El proyecto utiliza **Entity Framework Core (Code First)**. No necesitas scripts SQL manuales, el código creará la base de datos por ti.

### Paso 1: Configurar conexión
Abre el archivo `appsettings.json` en la carpeta `subsidio.API`. Cambia el valor de `Server=` por el nombre de tu servidor SQL local.

### Paso 2: Ejecutar la migración
Abre una terminal en la **carpeta raíz de la solución** (al mismo nivel que el archivo `.sln`) y ejecuta el siguiente comando para impactar los cambios:

```bash
dotnet ef database update --project subsidio.Infraestructura --startup-project subsidio.API
