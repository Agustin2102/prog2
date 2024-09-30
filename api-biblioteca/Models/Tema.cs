using System.Text.Json.Serialization;

public class Tema
{
  public int Id {get; set;}
  public string Nombre {get; set;}

  [JsonIgnore]
  public List<Libro> Libros {get; set;}   
}