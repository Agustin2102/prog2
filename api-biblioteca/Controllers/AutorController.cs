using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
    
[ApiController]
[Authorize]
[Route("api/autores")]
public class AutorController : ControllerBase {

  private readonly IAutorService _autorService;

  public AutorController(IAutorService autorService)
  {
    _autorService = autorService;
  }
  
  [HttpGet]
  // [AllowAnonymous]
  public ActionResult<List<Autor>> GetAllAutores(){

    return Ok(_autorService.GetAll());
  }
    
  [HttpGet("{id}/libros")]
  [Authorize(Roles = "admin")]
  public ActionResult<List<Libro>> GetLibros(int id)
  {
    var a = _autorService.GetLibros(id);
    return Ok(a);

  }

  [HttpGet("{id}")]
  public ActionResult<Autor> GetById(int id)
  {
  Autor? a = _autorService.GetById(id);
  if(a == null)
  {
    return NotFound("Autor no Encotrado");
  }
  
    return Ok(a);

  }

  [HttpPost]
  public ActionResult<Autor> NuevoAutor(AutorDTO a){
    //Asigno el id al Autor nuevo buscando el maximo id en la lista actual de autores y sumando 1
    //a.Id = _autorService.GetAll().Max(m => m.Id) + 1;
    // Llamo al metodo Create del servicio de autor para dar de alta el nuevo autor
    Autor _a = _autorService.Create(a);
    //Devuelvo el resultado de llamar al metodo GetById pasando como parametro el Id del nuevo autor
    return CreatedAtAction(nameof(GetById), new {id = _a.Id}, _a);
  }

  [HttpDelete("{id}")]
  public ActionResult Delete(int id)
  {
    var a = _autorService.GetById(id);

    if (a == null)
    { return NotFound("Autor no encontrado!!!");}

    _autorService.Delete(id);
    return NoContent();
  }

  [HttpPut("{id}")]
  public ActionResult<Autor> UpdateAutor(int id, Autor updatedAutor) {
    // Asegurarse de que el ID del autor en la solicitud coincida con el ID del parámetro
    if (id != updatedAutor.Id) {
      return BadRequest("El ID del autor en la URL no coincide con el ID del autor en el cuerpo de la solicitud.");
    }
    var autor = _autorService.Update(id, updatedAutor);

    if (autor is null) {
      return NotFound(); // Si no se encontró el autor, retorna 404 Not Found
    }
     return CreatedAtAction(nameof(GetById), new {id = autor.Id}, autor); // Retorna el recurso actualizado
  }

}

