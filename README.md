# Agenda Electrónica

Aplicación de escritorio para gestionar contactos mediante Windows Forms y SQL Server. Permite registrar, consultar, buscar, modificar y eliminar contactos, aplicando validaciones básicas sobre los datos introducidos.

## Características

- Alta de contactos.
- Consulta de contactos almacenados.
- Búsqueda por nombre.
- Carga de contactos para modificación.
- Modificación y eliminación de contactos.
- Validación de nombre, correo electrónico, fecha de nacimiento, género y teléfonos.
- Resumen estadístico reutilizable para listas de contactos.

## Tecnologías

- C#
- .NET Framework 4.8
- Windows Forms
- SQL Server / SQL Server Express
- ADO.NET (`System.Data.SqlClient`)
- Visual Studio

## Estructura de la solución

```text
AgendaElectronica.UI
├── AgendaElectronica.UI             # Interfaz Windows Forms
├── AgendaElectronica.Datos          # Acceso a datos y consultas SQL
├── AgendaElectronica.Entidades      # Entidad Contacto
└── AgendaElectronica.Logica         # Validaciones y servicios de negocio
```

### Proyectos principales

- **AgendaElectronica.UI**: contiene `Form1`, los controles de la interfaz y los eventos de usuario.
- **AgendaElectronica.Datos**: contiene `ContactoDal`, responsable de las operaciones CRUD sobre SQL Server.
- **AgendaElectronica.Entidades**: contiene la clase `Contacto`.
- **AgendaElectronica.Logica**: contiene `ContactoValidator` y `EstadisticasService`.

## Requisitos

- Windows.
- Visual Studio con soporte para .NET Framework 4.8.
- SQL Server o SQL Server Express.
- Una base de datos llamada `AgendaBD`.
- Una tabla `Contactos` con, al menos, las siguientes columnas:

| Columna | Uso esperado |
|---|---|
| `IdContacto` | Identificador único, preferiblemente clave primaria e identidad |
| `Nombre` | Nombre del contacto |
| `Apellido` | Apellido del contacto |
| `FechaNacimiento` | Fecha de nacimiento |
| `Direccion` | Dirección |
| `Genero` | Género seleccionado |
| `EstadoCivil` | Estado civil seleccionado |
| `Movil` | Número de móvil |
| `Telefono` | Número de teléfono |
| `CorreoElectronico` | Correo electrónico |

## Configuración de la base de datos

La capa de datos utiliza actualmente esta conexión:

```text
Data Source=DESKTOP-J3TK5SM\\SQLEXPRESS;Database=AgendaBD;Trusted_Connection=True;
```

Si la instancia de SQL Server es diferente, actualiza la cadena de conexión en:

```text
AgendaElectronica.Datos/Class1.cs
```

Se recomienda trasladar esta configuración a `App.config` antes de distribuir la aplicación.

## Ejecución

1. Abre `AgendaElectronica.UI.sln` con Visual Studio.
2. Comprueba la cadena de conexión y la disponibilidad de SQL Server.
3. Verifica que la base de datos `AgendaBD` y la tabla `Contactos` existan.
4. Establece `AgendaElectronica.UI` como proyecto de inicio.
5. Compila la solución.
6. Ejecuta el proyecto con `F5`.

## Validaciones actuales

- Nombre y apellido obligatorios.
- Dirección, género, estado civil y correo obligatorios.
- Correo electrónico con formato básico válido.
- Nombre entre 2 y 50 caracteres.
- Fecha de nacimiento no posterior al día actual.
- Al menos un teléfono válido entre móvil y teléfono.

## Estado actual y aspectos pendientes

- La solución compila correctamente en .NET Framework 4.8.
- No hay proyectos de pruebas automatizadas actualmente.
- La conexión a SQL Server está definida directamente en el código.
- La opción de género mostrada por la interfaz debe mantenerse sincronizada con los valores aceptados por el validador.
- Conviene revisar la visibilidad y selección de la tabla de contactos.
- Las operaciones de modificación y eliminación deberían reutilizar la misma validación y controlar errores de conexión.
- La búsqueda debería admitir nombres y apellidos compuestos y evitar consultar la base de datos en cada pulsación.

## Contribución

1. Crea una rama para el cambio.
2. Mantén la compatibilidad con .NET Framework 4.8.
3. No incluyas credenciales ni cadenas de conexión específicas de una máquina en cambios distribuidos.
4. Compila la solución y prueba los flujos afectados.
5. Documenta cualquier cambio en el modelo de datos.

## Licencia

No se ha definido una licencia para este repositorio.
