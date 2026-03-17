# Sistema Gestor de Automóviles

## 1) Objetivo
Construir una solución académica en **.NET 8** para administrar un catálogo de automóviles mediante una **API REST** + **SQLite** + **Entity Framework Core**, aplicando arquitectura por capas, principios SOLID y una vista web simple consumida con JavaScript vanilla.

## 2) Descripción general
La solución separa claramente dominio, aplicación, infraestructura, API y Web. Los controladores son delgados: solo coordinan HTTP, delegan reglas al servicio y devuelven respuestas correctas. La vista web consume la API por `fetch`, evitando lógica de negocio en controladores de UI.

## 3) Arquitectura por capas
```text
SistemaGestorAutomoviles.sln
└── src
    ├── SistemaGestorAutomoviles.Domain
    ├── SistemaGestorAutomoviles.Application
    ├── SistemaGestorAutomoviles.Infrastructure
    ├── SistemaGestorAutomoviles.API
    └── SistemaGestorAutomoviles.Web
```

## 4) Explicación de cada proyecto
- **Domain**: entidad `Vehicle` y reglas estructurales básicas.
- **Application**: DTOs, contratos (repositorios/servicios), validaciones de negocio y mapeos.
- **Infrastructure**: `AppDbContext`, repositorio EF Core, configuración de dependencias y seed.
- **API**: endpoints REST, middleware de errores global, Swagger, CORS.
- **Web**: interfaz simple (HTML/CSS) y lógica CRUD en JavaScript separado (`vehicles.js`).

## 5) Paquetes NuGet utilizados
- `Microsoft.EntityFrameworkCore`: Core ORM.
- `Microsoft.EntityFrameworkCore.Sqlite`: proveedor SQLite.
- `Microsoft.EntityFrameworkCore.Design`: diseño/migraciones (API).
- `Swashbuckle.AspNetCore`: Swagger/OpenAPI.

> Opcional para CLI de migraciones:
- `Microsoft.EntityFrameworkCore.Tools`

## 6) Principios SOLID aplicados
- **SRP**: cada capa tiene una responsabilidad clara.
- **OCP**: se extiende comportamiento con nuevas implementaciones (interfaces repositorio/servicio) sin modificar consumidores.
- **DIP**: API depende de abstracciones (`IVehicleService`, `IVehicleRepository`) e inyección de dependencias.

## 7) Patrones de diseño aplicados
- **Repository Pattern** (`VehicleRepository`).
- **Service Layer Pattern** (`VehicleService`).
- **Dependency Injection** (registro en extensión de servicios).
- **DTO Pattern** (`VehicleDto`, `VehicleCreateDto`, `VehicleUpdateDto`).

## 8) Cómo ejecutar el proyecto
### API
```bash
dotnet run --project src/SistemaGestorAutomoviles.API
```

### Web
```bash
dotnet run --project src/SistemaGestorAutomoviles.Web
```

> Ajusta `apiBaseUrl` en `wwwroot/js/vehicles.js` según el puerto real de la API.

## 9) Cómo aplicar migraciones
1. Instala la herramienta (si no está instalada):
```bash
dotnet tool install --global dotnet-ef
```
2. Crear migración inicial:
```bash
dotnet ef migrations add InitialCreate \
  --project src/SistemaGestorAutomoviles.Infrastructure \
  --startup-project src/SistemaGestorAutomoviles.API \
  --output-dir Data/Migrations
```
3. Aplicar migración:
```bash
dotnet ef database update \
  --project src/SistemaGestorAutomoviles.Infrastructure \
  --startup-project src/SistemaGestorAutomoviles.API
```

## 10) Endpoints disponibles
- `GET    /api/vehicles`
- `GET    /api/vehicles/{id}`
- `POST   /api/vehicles`
- `PUT    /api/vehicles/{id}`
- `DELETE /api/vehicles/{id}`

## 11) Cómo usar la vista web
1. Levanta API y Web.
2. En la vista principal:
   - Lista vehículos automáticamente.
   - Crea nuevos desde formulario.
   - Edita con botón **Editar**.
   - Consulta detalle con **Detalle**.
   - Elimina con confirmación.
3. Los mensajes de éxito/error se muestran sin recargar toda la página.

## 12) Integrantes del grupo
- Integrantes: [Agregar nombres]

## 13) Enlace del repositorio
- Repositorio: [Agregar enlace]

## 14) Comandos CLI para crear toda la solución
```bash
# 1) Solución y carpetas
mkdir SistemaGestorAutomoviles
cd SistemaGestorAutomoviles
mkdir src

# 2) Crear proyectos
 dotnet new classlib -n SistemaGestorAutomoviles.Domain -o src/SistemaGestorAutomoviles.Domain
 dotnet new classlib -n SistemaGestorAutomoviles.Application -o src/SistemaGestorAutomoviles.Application
 dotnet new classlib -n SistemaGestorAutomoviles.Infrastructure -o src/SistemaGestorAutomoviles.Infrastructure
 dotnet new webapi   -n SistemaGestorAutomoviles.API -o src/SistemaGestorAutomoviles.API
 dotnet new mvc      -n SistemaGestorAutomoviles.Web -o src/SistemaGestorAutomoviles.Web

# 3) Crear solución y agregar proyectos
 dotnet new sln -n SistemaGestorAutomoviles
 dotnet sln add src/SistemaGestorAutomoviles.Domain/SistemaGestorAutomoviles.Domain.csproj
 dotnet sln add src/SistemaGestorAutomoviles.Application/SistemaGestorAutomoviles.Application.csproj
 dotnet sln add src/SistemaGestorAutomoviles.Infrastructure/SistemaGestorAutomoviles.Infrastructure.csproj
 dotnet sln add src/SistemaGestorAutomoviles.API/SistemaGestorAutomoviles.API.csproj
 dotnet sln add src/SistemaGestorAutomoviles.Web/SistemaGestorAutomoviles.Web.csproj

# 4) Referencias entre capas
 dotnet add src/SistemaGestorAutomoviles.Application/SistemaGestorAutomoviles.Application.csproj reference src/SistemaGestorAutomoviles.Domain/SistemaGestorAutomoviles.Domain.csproj
 dotnet add src/SistemaGestorAutomoviles.Infrastructure/SistemaGestorAutomoviles.Infrastructure.csproj reference src/SistemaGestorAutomoviles.Application/SistemaGestorAutomoviles.Application.csproj
 dotnet add src/SistemaGestorAutomoviles.Infrastructure/SistemaGestorAutomoviles.Infrastructure.csproj reference src/SistemaGestorAutomoviles.Domain/SistemaGestorAutomoviles.Domain.csproj
 dotnet add src/SistemaGestorAutomoviles.API/SistemaGestorAutomoviles.API.csproj reference src/SistemaGestorAutomoviles.Application/SistemaGestorAutomoviles.Application.csproj
 dotnet add src/SistemaGestorAutomoviles.API/SistemaGestorAutomoviles.API.csproj reference src/SistemaGestorAutomoviles.Infrastructure/SistemaGestorAutomoviles.Infrastructure.csproj

# 5) NuGet
 dotnet add src/SistemaGestorAutomoviles.Infrastructure/SistemaGestorAutomoviles.Infrastructure.csproj package Microsoft.EntityFrameworkCore
 dotnet add src/SistemaGestorAutomoviles.Infrastructure/SistemaGestorAutomoviles.Infrastructure.csproj package Microsoft.EntityFrameworkCore.Sqlite
 dotnet add src/SistemaGestorAutomoviles.API/SistemaGestorAutomoviles.API.csproj package Microsoft.EntityFrameworkCore.Design
 dotnet add src/SistemaGestorAutomoviles.API/SistemaGestorAutomoviles.API.csproj package Swashbuckle.AspNetCore
```

## 15) Cómo este diseño evita controladores sobrecargados
- Los controladores solo validan entrada HTTP y llaman al servicio.
- La lógica de negocio (validaciones de rango, reglas de actualización, etc.) vive en Application.
- El acceso a datos está aislado en Infrastructure con repositorios + DbContext.
- La vista usa JavaScript separado para todo el CRUD vía API, evitando lógica pesada en controladores de UI.
- Resultado: mejor mantenibilidad, pruebas más simples y separación de responsabilidades defendible en exposición.
