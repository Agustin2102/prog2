public class LibroDTO {
  public string? Titulo { get; set; }

  public int? AutorId { get; set; }

  public int? Paginas {get; set;}

  public int? Ano {get; set;}
  public string? Url_Portada {get; set;}

  public List<int>? TemasIds {get; set;}
}