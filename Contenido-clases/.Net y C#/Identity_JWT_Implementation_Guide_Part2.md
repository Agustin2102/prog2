
# Implementación de Identity y Autenticación con JWT en ASP.NET Core - Parte 2

## 6. Creación de Controladores para Registro y Login de Usuarios

### **AccountController**:

Vamos a crear el controlador **AccountController**, que contendrá los endpoints para registro, login y asignación de roles a usuarios.

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

    // Registro de usuarios
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var userExists = await _userManager.FindByNameAsync(model.Username);
        if (userExists != null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "El usuario ya existe" });
        }

        var user = new ApplicationUser
        {
            UserName = model.Username,
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error al crear usuario" });
        }

        return Ok(new { Message = "Usuario creado satisfactoriamente" });
    }

    // Login de usuarios y generación de JWT
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Identificador único para el token
            };

            // Añadir los roles del usuario como claims
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
        return Unauthorized();
    }

    // Asignar un rol a un usuario
    [HttpPost("asignar-rol")]
    public async Task<IActionResult> AsignarRol([FromBody] RoleAssignmentModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user == null)
        {
            return NotFound(new { Message = "Usuario no encontrado" });
        }

        var roleExists = await _userManager.IsInRoleAsync(user, model.Role);
        if (roleExists)
        {
            return BadRequest(new { Message = "El usuario ya tiene este rol" });
        }

        var result = await _userManager.AddToRoleAsync(user, model.Role);
        if (result.Succeeded)
        {
            return Ok(new { Message = "Rol asignado correctamente" });
        }

        return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error al asignar el rol" });
    }
}
```

### Modelos para el Registro y Asignación de Roles:

```csharp
public class RegisterModel
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class RoleAssignmentModel
{
    public string Username { get; set; }
    public string Role { get; set; }
}
```

### Explicación del Controlador:

1. **`Register`**: Permite registrar un nuevo usuario en la base de datos si no existe.
2. **`Login`**: Autentica al usuario, genera un **JWT**, y lo envía como respuesta. Los roles del usuario son agregados como **claims** dentro del token.
3. **`AsignarRol`**: Permite asignar un rol a un usuario existente.

---

## 7. Autorización de Endpoints utilizando Roles

Finalmente, vamos a proteger los endpoints y limitar el acceso según el rol de usuario. Se utiliza el atributo `[Authorize]` para definir qué roles pueden acceder a un endpoint en particular.

### Ejemplo de Endpoint Protegido por Roles:

```csharp
[Authorize(Roles = "Administrador,Usuario")]
[HttpGet("{id}")]
public ActionResult<Autor> GetById(int id)
{
    Autor? a = _autorService.GetById(id);
    if (a == null)
    {
        return NotFound("Autor no encontrado");
    }

    return Ok(a);
}
```

### Explicación:

- **[Authorize(Roles = "Administrador,Usuario")]**: Este atributo asegura que solo los usuarios autenticados con los roles de **Administrador** o **Usuario** puedan acceder a este recurso.
- Si un usuario no tiene el rol adecuado o no está autenticado, se devolverá un estado **403 Forbidden** o **401 Unauthorized**.

---

### Resumen

1. **Estructura de Identity**: Usamos **Identity** para gestionar la autenticación y autorización de usuarios.
2. **JWT para autenticación**: Configuramos **JWT** para autenticar a los usuarios de forma segura y sin estado (stateless).
3. **Roles para autorización**: Los roles son utilizados para gestionar quién puede acceder a qué recursos en la API, protegiendo los endpoints con `[Authorize(Roles = "rol")]`.
4. **Controlador Account**: Se implementaron los métodos de registro, login, y asignación de roles, permitiendo una gestión completa de usuarios y permisos.

Con esta guía tienes un proyecto de **ASP.NET Core** con **Identity y JWT** completamente funcional. ¡Listo para implementar!
