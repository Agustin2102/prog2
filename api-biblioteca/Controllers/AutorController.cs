using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/autores")]
public class AutorController : ControllerBase {

  [HttpGet("listado")]
  public ActionResult<List<Autor>> GetAllAutores(){
    List<Autor> autores =
    [
      new Autor{Id = 1, Nombre = "Guille", Apellido = "Cortez"},
      new Autor{Id = 2, Nombre = "Juan", Apellido = "Gomez"},
    ];
    return autores;
  }

  // public ActionResult<Autor> GetById(){

  // }
}

