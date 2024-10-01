
using Microsoft.EntityFrameworkCore;

public class AutorDbService : IAutorService
{
  private readonly BibliotecaContext _context;

  public AutorDbService(BibliotecaContext context)
  {
    _context = context;
  }
    public Autor Create(AutorDTO a)
    {
        Autor autor = new()
        {
            Nombre = a.Nombre,
            Apellido = a.Apellido
        };
        _context.Autores.Add(autor);
       _context.SaveChanges();
       return autor;
    }

    public void Delete(int id)
    {
        var a = _context.Autores.Find(id);
        _context.Autores.Remove(a);
        _context.SaveChanges();
    }

    public IEnumerable<Autor> GetAll()
    {
        return _context.Autores; //.Where(a => a.Nombre.Contains("j")) ;
    }

    public Autor? GetById(int id)
    {
        return _context.Autores.Find(id);
    }

    public Autor? Update(int id, Autor a)
    {
        _context.Entry(a).State = EntityState.Modified;
        _context.SaveChanges();
        return a;
    }

    public IEnumerable<Libro> GetLibros(int id)
    {
        Autor a = _context.Autores.Include(a => a.Libros).ThenInclude(l => l.Temas).FirstOrDefault(x => x.Id == id);
        return a.Libros;
    }
}
