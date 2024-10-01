using Microsoft.AspNetCore.Mvc;
    
[ApiController]
[Route("api/Temas")]
public class TemaController : ControllerBase {

  private readonly ITemaService _temaService;

  public TemaController(ITemaService temaService)
  {
    _temaService = temaService;
  }
  [HttpGet]
  public ActionResult<List<Tema>> GetAllTemas(){

    return Ok(_temaService.GetAll());
  }
    
  [HttpGet("{id}")]
  public ActionResult<Tema> GetById(int id)
  {
  Tema? a = _temaService.GetById(id);
  if(a == null)
  {
    return NotFound("Tema no Encotrado");
  }
  
  return Ok(a);

  }

  [HttpPost]
  public ActionResult<Tema> NuevoTema(TemaDTO a){
    //Asigno el id al Tema nuevo buscando el maximo id en la lista actual de Temaes y sumando 1
    //a.Id = _temaService.GetAll().Max(m => m.Id) + 1;
    // Llamo al metodo Create del servicio de Tema para dar de alta el nuevo Tema
    Tema _a = _temaService.Create(a);
    //Devuelvo el resultado de llamar al metodo GetById pasando como parametro el Id del nuevo Tema
    return CreatedAtAction(nameof(GetById), new {id = _a.Id}, _a);
  }

  [HttpDelete("{id}")]
  public ActionResult Delete(int id)
  {
    var a = _temaService.GetById(id);

    if (a == null)
    { return NotFound("Tema no encontrado!!!");}

    _temaService.Delete(id);
    return NoContent();
  }

  [HttpPut("{id}")]
  public ActionResult<Tema> UpdateTema(int id, TemaDTO updatedTema) {
    
    var tema = _temaService.Update(id, updatedTema);

    if (tema is null) {
      return NotFound(); // Si no se encontró el Tema, retorna 404 Not Found
    }
    return CreatedAtAction(nameof(GetById), new {id = tema.Id}, tema);
  }

}
