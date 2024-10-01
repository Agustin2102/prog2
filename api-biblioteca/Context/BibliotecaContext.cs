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

        modelBuilder.Entity<Libro>(entity =>
        {
          entity.Property(l => l.Titulo).IsRequired();
          entity.Property(l => l.Paginas).IsRequired();
          entity.Property(l => l.Ano).IsRequired();
          entity.Property(l => l.Url_Portada).IsRequired(false);

           entity.HasOne(l => l.Autor)
           .WithMany(a => a.Libros)
           .HasForeignKey(l => l.AutorId).IsRequired();

           entity.HasMany(l => l.Temas)
           .WithMany(t => t.Libros)
           .UsingEntity(j => j.ToTable("LibroTema") );
           
        }       
        );

        modelBuilder.Entity<Tema>(entity => 
        {
          entity.Property(t => t.Nombre).IsRequired().HasMaxLength(50);
        }
        );

      

    }

} 