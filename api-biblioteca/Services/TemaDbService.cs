
public class TemaDbService : ITemaService
{
  private readonly BibliotecaContext _context;

    public TemaDbService(BibliotecaContext context)
    {
        _context = context;
    }
    public Tema Create(TemaDTO a)
    {
        var NuevoTema = new Tema
        {
          Nombre = a.Nombre
        };

        _context.Temas.Add(NuevoTema);
        _context.SaveChanges();
        return NuevoTema;
    }

    public void Delete(int id)
    {
        var tema = _context.Temas.Find(id);
        _context.Temas.Remove(tema);
        _context.SaveChanges();
        
    }

    public IEnumerable<Tema> GetAll()
    {
      return _context.Temas;
    }

    public Tema? GetById(int id)
    {
        Tema t = _context.Temas.Find(id);
        return t;
    }

    public Tema? Update(int id, TemaDTO a)
    {
        throw new NotImplementedException();
    }
}