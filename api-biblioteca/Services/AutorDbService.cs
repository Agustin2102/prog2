
using Microsoft.EntityFrameworkCore;

public class AutorDbService : IAutorService
{
  private readonly BibliotecaContext _context;

  public AutorDbService(BibliotecaContext context)
  {
    _context = context;
  }
    public Autor Create(Autor a)
    {
       _context.Autores.Add(a);
       _context.SaveChanges();
       return a;
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
}
