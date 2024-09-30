public interface IAutorService
{
  public IEnumerable<Autor> GetAll();
  public Autor? GetById(int id);
  public Autor Create(AutorDTO a);

  public void Delete(int id);
  public Autor? Update(int id, Autor a);
}