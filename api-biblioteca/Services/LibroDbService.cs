
using Microsoft.EntityFrameworkCore;

public class LibroDbService : ILibroService
{
    private readonly BibliotecaContext _context;

    public LibroDbService(BibliotecaContext context)
    {
        _context = context;
    }
    public Libro Create(Libro l)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        Libro? l = _context.Libros.Find(id);
        if (l is null) return false;

        _context.Libros.Remove(l);
        _context.SaveChanges();
        return true;
    }

    public IEnumerable<Libro> GetAll()
    {
        return _context.Libros.Include(el => el.Autor).Include(l => l.Temas);
    }

    public Libro? GetById(int id)
    {
        return _context.Libros.Find(id);
    }

    public bool Update(int id, Libro l)
    {
        throw new NotImplementedException();
    }
}