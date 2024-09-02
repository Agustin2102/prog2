public class Libro
{
  public Libro(int id, string titulo, Autor autor)
  {
    this.Id = id;
    this.Titulo = titulo;
    this.Autor = autor;
  }
  public int Id { get; set; }
  public string Titulo { get; set; }

  public Autor Autor { get; set; }
}