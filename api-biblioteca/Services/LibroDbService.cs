
using Microsoft.EntityFrameworkCore;

public class LibroDbService : ILibroService
{
    private readonly BibliotecaContext _context;

    public LibroDbService(BibliotecaContext context)
    {
        _context = context;
    }
    public Libro Create(LibroDTO l)
    {
        var nuevoLibro = new Libro
        {
            Titulo = l.Titulo,
            AutorId = l.AutorId,
            Paginas = l.Paginas,
            Ano = l.Ano,
            Url_Portada = l.Url_Portada,
            Temas = new List<Tema>()
        };

        foreach(int idTema in l.TemasIds)
        {
            nuevoLibro.Temas.Add(_context.Temas.Find(idTema));
        }
        _context.Add(nuevoLibro);
        _context.SaveChanges();
        return nuevoLibro;
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

    public Libro Update(int id, LibroDTO l)
    {
        var libroUpdate = _context.Libros.Include(l => l.Temas).FirstOrDefault(l => l.Id == id);
        Console.WriteLine(libroUpdate.Id);
        libroUpdate.Titulo = l.Titulo;
        libroUpdate.Ano = l.Ano;
        libroUpdate.Paginas = l.Paginas;
        libroUpdate.AutorId = l.AutorId;
        libroUpdate.Url_Portada = l.Url_Portada;
        libroUpdate.Temas.Clear();

        
        foreach(int idTema in l.TemasIds)
        {
            libroUpdate.Temas.Add(_context.Temas.Find(idTema));
        }

        _context.Entry(libroUpdate).State = EntityState.Modified;
        _context.SaveChanges();
        return libroUpdate;

    }
}