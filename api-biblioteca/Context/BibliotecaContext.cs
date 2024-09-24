using Microsoft.EntityFrameworkCore;

public class BibliotecaContext:DbContext
{
  public DbSet<Autor> Autores {get; set;}
  public DbSet<Libro> Libros {get; set;}
  public DbSet<Tema> Temas {get; set;}

    public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
          entity.Property(a => a.Nombre).IsRequired().HasMaxLength(100);
          entity.Property(a => a.Apellido).IsRequired().HasMaxLength(100);
        }
        );
    }

} 