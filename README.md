Backend de la Aplicación Reservas
Este es el backend de la aplicación desarrollado utilizando la arquitectura limpia y microservicios. A continuación, se explica cómo correr las migraciones, cómo funciona el endpoint de generación de tokens con JWT y detalles generales sobre la estructura de la aplicación.

Requisitos
.NET Core SDK (preferiblemente 6 o superior)
SQL Server (u otro sistema de base de datos compatible, si se ha configurado)
Node.js (si se requiere para algún servicio adicional)
Docker (opcional, si deseas contenedores para los microservicios)
Postman o alguna herramienta similar para probar los endpoints
Instalación
Clona el repositorio:

bash
Copiar código
git clone (https://github.com/Alonsor1999/AppBackendReservations.git)
cd <nombre_del_directorio>
Restaura las dependencias del proyecto:

bash
Copiar código
dotnet restore
Compila el proyecto:

bash
Copiar código
dotnet build
Migraciones de la Base de Datos
Para correr las migraciones de la base de datos y aplicar cambios, sigue estos pasos:
Abre la terminal y navega a la carpeta del proyecto donde está tu archivo .csproj.

Ejecuta el siguiente comando para aplicar las migraciones:

bash
Copiar código
dotnet ef database update
Este comando se encargará de aplicar las migraciones pendientes y actualizar la base de datos.

Si deseas crear una nueva migración:
Crea la migración con el siguiente comando:

bash
Copiar código
actualiza la base de datos con para que corra la migraciones en la capa infradata: Update-Database

bash
Copiar código
dotnet ef database update
Endpoints de Autenticación y JWT
Endpoint para obtener un token JWT
Se ha implementado un endpoint de autenticación que utiliza JWT (JSON Web Tokens) para la autenticación de usuarios.

Endpoint
POST /api/auth/login
ademas de los Endpoint requeridos:
POST/ api/Reservations
Delete/ api/Reservations
Get/ api/Reservations

Post/ api/Space
Get/ api/Space

Get/ api/User
Post/ api/User
Put/ api/User
Delete/ api/User