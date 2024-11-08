---

# Proyecto de Backend con C# y .NET

Este proyecto consiste en desarrollar una API de backend para la gestión de usuarios, utilizando C# con .NET Core. Durante todo el desarrollo, integre nuevas tecnologías y herramientas como Docker, Swagger, y Entity Framework Core, para crear una api lo mas robusta, escalable y fácil de mantener.

## Tecnologías Utilizadas

- **C# 12**: Lenguaje de programación principal para la lógica de la aplicación.
- **.NET 8**: Framework para el desarrollo de aplicaciones web y API RESTful.
- **Entity Framework Core**: ORM para la gestión de bases de datos, facilitando la interacción con SQL Server.
- **SQL Server**: Base de datos para almacenar información de los usuarios y operaciones.
- **Docker**: Para contenerizar la base de datos SQL Server y la aplicación, facilitando el despliegue y desarrollo en diferentes entornos.
- **Swagger**: Para documentar automáticamente la API y facilitar la interacción con los endpoints.
- **Visual Studio Code**: Herramientas de desarrollo para la escritura de código y pruebas de la API.
- **Postman**: Utilice esta herramienta para testear todos los endpoints de la API.

## Objetivos del Proyecto

1. **Gestión de usuarios**: Crear una backend con CRUD para gestionar usuarios, incluyendo la creación, autenticación, obtención, actualización y eliminación suave (desactivar) de usuarios.
2. **Uso de Docker**: Implementar contenedores de Docker para facilitar el despliegue y la gestión de servicios como SQL Server.
3. **Documentación automática**: Integrar Swagger para documentar y probar los endpoints de manera sencilla, teniendo logs mucho mas extensos que permitieron entender cada error.
4. **Desarrollo multiplataforma**: Usar un entorno macOS para el desarrollo, aprovechando herramientas Visual Studio Code y tambien utilizar entornos linux mediante contenedores de Docker.

## Proceso de Desarrollo

### Paso 1: Preparación del Entorno

- Se configuró un entorno de desarrollo utilizando Visual Studio Code para el desarrollo de la API en C#.
- Se optó por usar **.NET 8**, ya que es el marco solicitado en el que debiamos hacer este proyecto.
- Para la base de datos, se utilizó **SQL Server**, que se ejecutó en un contenedor Docker, lo que me permitio configurar y desplegar con facilidad sin tener que instalar el servidor directamente en el sistema.

### Paso 2: Creación de la API de Usuarios

Se implementó una API RESTful para la gestión de usuarios. Las funcionalidades principales son:

1. **Crear un usuario**: Permite agregar un nuevo usuario al sistema.
2. **Autenticación de usuario**: Valida las credenciales y devuelve un token JWT para autenticar futuras solicitudes.
3. **Obtener lista de usuarios**: Devuelve todos los usuarios registrados.
4. **Obtener un usuario por ID**: Permite obtener información detallada de un usuario en particular.
5. **Actualizar usuario**: Permite modificar los detalles de un usuario.
6. **Eliminar (soft delete) usuario**: Modifica el campo `IsActive` para marcar al usuario como inactivo, en lugar de eliminarlo de la base de datos.

### Paso 3: Configuración de Docker

Para facilitar el proceso de despliegue y la consistencia del entorno de desarrollo, también asi para que, quien quiera, pueda clonar y levantar la API y Base de Datos desde su lado, se configuró **Docker** para ejecutar el contenedor de **SQL Server**. Esto permite que la base de datos se ejecute en un entorno aislado, sin interferir con el sistema operativo.

El archivo `docker-compose.yml` incluye la configuración de la base de datos SQL Server, lo que hace que sea sencillo ejecutar tanto la API como la base de datos desde Docker. Aquí un ejemplo del archivo:

```yaml
version: '3.4'

services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2019-latest
    container_name: sqlserver
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourStrongPassword123
    ports:
      - "1433:1433"
    networks:
      - willinn-project
    volumes:
      - sqlserver-data:/var/opt/mssql

networks:
  willinn-project:
    driver: bridge

volumes:
  sqlserver-data:
```

### Paso 4: Documentación y Pruebas con Swagger y Postman

Para documentar la API y facilitar las pruebas, se utilizó **Swagger** a través de la configuración de **Swashbuckle** en .NET. Esto genera una interfaz interactiva que permite probar todos los endpoints de la API directamente desde el navegador, sin necesidad de usar herramientas externas como Postman, herramienta que tambien utilizamos dada la facilidad y versatilidad que da a la hora de testear apis.

En el archivo `Program.cs`, se configuró Swagger de la siguiente manera:

```csharp
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

Esto habilita la documentación de la API en la ruta `/swagger` de la aplicación.

### Paso 5: Implementación y Pruebas

1. **Pruebas de Funcionalidad**: Se probaron todas las rutas utilizando **Swagger** y **Postman** para asegurarse de que las operaciones de la API fueran correctas.

### Paso 6: Despliegue

El despliegue de la aplicación se realizó utilizando contenedores Docker. Lo cual facilita tanto el desarrollo local como el despliegue en cualquier servidor compatible con Docker. La API y la base de datos SQL Server se ejecutan de forma aislada, pero creando una conexion ellas, lo que permite un despliegue sencillo y confiable.

## Estructura del Proyecto

```
/Backend
    /Controllers
        OperationController.cs
        UsersController.cs
    /Models
        User.cs
    /Data
        AppDbContext.cs
    /Services
        UserService.cs
    /UnitTests
        UserControllerTests.cs
    Program.cs
    docker-compose.yml
```

- **Controllers**: Contiene los controladores de la API.
- **Models**: Contiene los modelos de datos, como `User`.
- **Data**: Contiene la configuración de Entity Framework Core y `DbContext`.
- **Services**: Contiene la lógica de negocios para la gestión de usuarios.
- **UnitTests**: Contiene las pruebas unitarias para la API.

## Conclusión

Este proyecto es un ejemplo de cómo se puede desarrollar una API RESTful utilizando **C# y .NET**. Se integraron varias tecnologías como **Docker** para la base de datos, **Swagger** para la documentación de la API y **Entity Framework Core** para el acceso a la base de datos, todo dentro de un flujo de trabajo eficiente y multiplataforma.

El principal desafío que enfrente fue el hecho de haber tenido que aprender un lenguaje y un entorno totalmente nuevo, el cual no habia utilizado anteriormente al mismo tiempo que desarrollaba el backend. Un reto muy grande pero que demuestra que C# tiene una comunidad en la cual se encuentra todo tipo de documentación, lo cual explica la robustez y popularidad de sus soluciones.

---
