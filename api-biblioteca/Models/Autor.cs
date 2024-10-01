using System.Text.Json.Serialization;

public class Autor
{
  public int Id { get; set; }
  public string? Nombre { get; set; }
  public string? Apellido { get; set; }
  [JsonIgnore]
  public virtual List<Libro> Libros {get; set;}
  public Autor()
  {

  }

  public Autor(int id, string nombre, string apellido)
  {
    Id = id;
    Nombre = nombre;
    Apellido = apellido;
  }


  override public string ToString()
  {
    return $"Id:{Id}, {Nombre} {Apellido}";
  }
}