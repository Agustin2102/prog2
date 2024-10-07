using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Agregamos los controladores
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlServer<BibliotecaContext>(builder.Configuration.GetConnectionString("cnBiblioteca"));
builder.Services.AddScoped<IFileStorageService, FileStorageService>();
builder.Services.AddScoped<IAutorService, AutorDbService>();
builder.Services.AddScoped<ILibroService, LibroDbService>();
builder.Services.AddScoped<ITemaService, TemaDbService>();

// Configurar el contexto para Identity (autenticación y autorización)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("cnBiblioteca")));

// Configurar Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configurar JWT para autenticación
// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
// })
// .AddJwtBearer(options =>
// {
//     options.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuer = true, // Valida que el emisor del token sea el esperado
//         ValidateAudience = true, // Valida que la audiencia del token sea la esperada
//         ValidateLifetime = true, // Valida que el token no haya expirado
//         ValidateIssuerSigningKey = true, // Verifica que el token esté firmado con la clave correcta
//         ValidIssuer = builder.Configuration["Jwt:Issuer"], // Especifica el emisor esperado del token
//         ValidAudience = builder.Configuration["Jwt:Audience"], // Especifica la audiencia esperada del token
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // Clave secreta para firmar el token
//     };
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
