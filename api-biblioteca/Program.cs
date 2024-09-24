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
