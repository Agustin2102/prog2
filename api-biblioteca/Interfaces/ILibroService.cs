public interface ILibroService
{
  public IEnumerable<Libro> GetAll();
  public Libro? GetById(int id);
  public Libro Create(LibroDTO l);

  public bool Delete(int id);
  public Libro Update(int id, LibroDTO l);
}