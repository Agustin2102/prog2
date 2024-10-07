
# Migraciones en Entity Framework: Conceptos y Comandos

## ¿Qué son las migraciones?
Las migraciones en Entity Framework (EF) permiten gestionar los cambios en el esquema de la base de datos de manera automática conforme los modelos de tu aplicación cambian. En lugar de escribir SQL manual, las migraciones generan scripts que actualizan la base de datos a la última versión de tu modelo.

## ¿Cómo funcionan las migraciones?
1. **Definición de Modelos**: Los cambios en tus modelos C# representan modificaciones en el esquema de la base de datos.
2. **Creación de Migraciones**: EF compara el estado actual de los modelos con el estado anterior y genera una migración que contiene los scripts necesarios para actualizar la base de datos.
3. **Aplicación de Migraciones**: Los cambios se aplican a la base de datos, manteniéndola sincronizada con los modelos de la aplicación.

## Comandos principales para trabajar con migraciones en Bash

### Agregar una nueva migración:
```bash
dotnet ef migrations add NombreMigracion
```

### Aplicar las migraciones a la base de datos:
```bash
dotnet ef database update
```

### Eliminar la última migración (si no ha sido aplicada aún):
```bash
dotnet ef migrations remove
```

### Crear una migración para ApplicationDbContext (Identity):
```bash
dotnet ef migrations add InitialIdentityMigration --context ApplicationDbContext
```

### Aplicar la migración a la base de datos usando ApplicationDbContext:
```bash
dotnet ef database update --context ApplicationDbContext
```

## Resumen
Las migraciones de EF permiten gestionar los cambios en el esquema de la base de datos de manera eficiente. Con los comandos descritos, puedes crear nuevas migraciones, aplicarlas a la base de datos y mantener tus modelos y esquema sincronizados.
