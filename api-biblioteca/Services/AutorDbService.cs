
public class AutorDbService : IAutorService
{
  private readonly BibliotecaContext _context;

  public AutorDbService(BibliotecaContext context)
  {
    _context = context;
  }
    public Autor Create(Autor a)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Autor> GetAll()
    {
        return _context.Autores;
    }

    public Autor? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Autor? Update(int id, Autor a)
    {
        throw new NotImplementedException();
    }
}
