public interface ILibroService
{
  public IEnumerable<Libro> GetAll();
  public Libro? GetById(int id);
  public Libro Create(Libro l);

  public bool Delete(int id);
  public bool Update(int id, Libro l);
}