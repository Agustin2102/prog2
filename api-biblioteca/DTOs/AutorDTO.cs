using System.ComponentModel.DataAnnotations;

public class AutorDTO {
  [Required( ErrorMessage = "El campo Nombre es requerido.")]
  public string? Nombre { get; set; }
  [Required( ErrorMessage = "El campo Apellido es requerido.")]
  public string? Apellido { get; set; }
}