public interface ITemaService
{
  public IEnumerable<Tema> GetAll();
  public Tema? GetById(int id);
  public Tema Create(TemaDTO a);

  public void Delete(int id);
  public Tema? Update(int id, TemaDTO a);
}