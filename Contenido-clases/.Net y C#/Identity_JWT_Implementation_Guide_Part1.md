
# Implementación de Identity y Autenticación con JWT en ASP.NET Core

Este documento cubre de manera completa cómo estructurar e implementar **ASP.NET Core Identity** en una API utilizando **JWT (JSON Web Tokens)** para la autenticación y autorización. Aquí aprenderás desde la creación de los contextos de base de datos, configuración de Identity, hasta la implementación de login y registro de usuarios con generación de tokens JWT.

## 1. Estructuración de Identity en un Proyecto

### ¿Qué es Identity?

**ASP.NET Core Identity** es un sistema que gestiona la autenticación (login) y autorización (permisos) de usuarios. Permite registrar usuarios, iniciar sesión, gestionar roles, y más. Para integrar Identity en una API, lo ideal es usar **JWT** para autenticar usuarios de manera segura.

### ¿Por qué usar Identity en una API?

1. **Autenticación y Autorización integradas**: Identity se integra fácilmente con el sistema de autorización de ASP.NET Core.
2. **Manejo de usuarios y roles**: Identity gestiona usuarios, contraseñas, roles, y la seguridad en general.
3. **Seguridad**: Identity se encarga de la encriptación de contraseñas, recuperación de cuentas, y otras características de seguridad.

## # Instalación de paquetes necesarios:

- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Design

    Comando:
    ```
    dotnet add package package.name
    ```

## 2. Creación de un Contexto de Base de Datos Diferente para Autenticación

### ¿Por qué separar el contexto de base de datos para la autenticación?

Separar los contextos de base de datos para la autenticación y la lógica de negocio tiene varias ventajas:

- **Organización**: Mantienes el código de autenticación separado de la lógica del negocio.
- **Escalabilidad**: Si en algún momento deseas cambiar o extender la lógica de autenticación, es más fácil hacerlo sin afectar otras áreas de la aplicación.
- **Seguridad**: Mantener un contexto separado ayuda a tener una mejor gestión y control sobre la autenticación.

### Código para crear el `ApplicationDbContext` para Identity:

```csharp
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}
```

Este contexto maneja la autenticación y los usuarios de Identity. Se extiende de `IdentityDbContext<ApplicationUser>` para incorporar la lógica de autenticación.

## 3. Creación del ModelUser para Personalizar la Autenticación

**ApplicationUser** es el modelo de usuario personalizado que hereda de `IdentityUser`. Si necesitas agregar más propiedades a los usuarios, puedes hacerlo en esta clase.

### Código para `ApplicationUser`:

```csharp
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    // Puedes agregar propiedades adicionales si lo necesitas
    public string NombreCompleto { get; set; }
}
```

- `IdentityUser` ya contiene propiedades como `UserName`, `Email`, `PasswordHash`, entre otras.
- `ApplicationUser` puede incluir propiedades adicionales como `NombreCompleto`.

## 4. Configuración en `Program.cs`

Aquí vamos a configurar Identity, la autenticación con JWT, y los contextos de base de datos en **Program.cs**.

### Código completo en `Program.cs`:

```csharp
var builder = WebApplication.CreateBuilder(args);

// Configurar el contexto para la base de datos de la biblioteca (lógica de negocio)
builder.Services.AddDbContext<BibliotecaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BibliotecaConnection")));

// Configurar el contexto para Identity (autenticación y autorización)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

// Configurar Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configurar JWT para autenticación
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // Valida que el emisor del token sea el esperado
        ValidateAudience = true, // Valida que la audiencia del token sea la esperada
        ValidateLifetime = true, // Valida que el token no haya expirado
        ValidateIssuerSigningKey = true, // Verifica que el token esté firmado con la clave correcta
        ValidIssuer = builder.Configuration["Jwt:Issuer"], // Especifica el emisor esperado del token
        ValidAudience = builder.Configuration["Jwt:Audience"], // Especifica la audiencia esperada del token
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // Clave secreta para firmar el token
    };
});

// Agregar controladores
builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roleNames = { "Admin", "User", "Manager" };
    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware de autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
```

### Explicación de la Configuración JWT:

- **ValidateIssuer**: Verifica que el emisor del token (Issuer) coincida con el que esperamos.
- **ValidateAudience**: Asegura que el token está destinado a la audiencia correcta (API).
- **ValidateLifetime**: Garantiza que el token no esté expirado.
- **IssuerSigningKey**: Clave secreta utilizada para firmar y validar el token JWT.

Los valores `Jwt:Issuer`, `Jwt:Audience` y `Jwt:Key` se configuran en el archivo **appsettings.json**:

```json
"Jwt": {
  "Key": "ClaveSuperSegura",
  "Issuer": "tuEmisor",
  "Audience": "tuAudiencia",
  "ExpiresInMinutes": 60
}
```

## 5. Explicación de `UserManager`, `SignInManager` e `IConfiguration`

### ¿Qué son y cómo se inyectan?

#### **1. `UserManager<ApplicationUser>`**

El `UserManager<TUser>` es un servicio proporcionado por **ASP.NET Core Identity** que se utiliza para realizar operaciones relacionadas con la **gestión de usuarios**. Esto incluye:

- Crear usuarios.
- Actualizar usuarios.
- Cambiar contraseñas.
- Consultar roles asociados a un usuario.
- Validar credenciales de usuarios.

#### **2. `SignInManager<ApplicationUser>`**

El `SignInManager<TUser>` también es un servicio proporcionado por Identity y está enfocado específicamente en las operaciones de **autenticación** de usuarios. Su funcionalidad incluye:

- Iniciar sesión (login).
- Cerrar sesión (logout).
- Autenticar a un usuario usando credenciales.

#### **3. `IConfiguration`**

El servicio `IConfiguration` es una interfaz de **ASP.NET Core** que se usa para acceder a las configuraciones de la aplicación, como las claves de **JWT** almacenadas en el archivo **appsettings.json**.

#### **Inyección de Dependencias en el Controlador**

Estos servicios son inyectados en el controlador mediante **Dependency Injection**:

```csharp
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AccountController(
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager, 
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    // Métodos de registro y login...
}
```

### ¿De dónde provienen estos servicios?

Estos servicios son registrados y gestionados automáticamente cuando configuras **Identity** en el archivo `Program.cs`. La línea clave es la siguiente:

```csharp
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
```

Esto le indica a ASP.NET Core que debe gestionar `UserManager`, `SignInManager`, y otros servicios de Identity.

