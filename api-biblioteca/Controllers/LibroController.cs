
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/libros")]
public class LibroController : ControllerBase
{
    private readonly ILibroService _libroService;
    private readonly IAutorService _autorService;

    public LibroController(ILibroService libroService, IAutorService autorService)
    {
      _libroService = libroService;
      _autorService = autorService;
    }

    [HttpGet]
    public ActionResult<List<Libro>> GetAll()
    {
      try
      {
        return Ok(_libroService.GetAll());
      }
      catch (System.Exception e)
      {
        Console.WriteLine(e.Message);
        return Problem(detail: e.Message, statusCode: 500);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Libro> GetById(int id)
    {
      Libro? libro = _libroService.GetById(id);
      if ( libro is null ) return NotFound();
      return Ok(libro);
    }

    [HttpPost]
    public ActionResult<Libro> NuevoLibro(Libro l)
    {
      // a.Id = _autorService.GetAll().Max(m => m.Id) + 1;
      AutorDTO autorDTO = new()
      {
        Nombre = l.Autor.Nombre,
        Apellido = l.Autor.Apellido
      };
      Autor a = _autorService.Create(autorDTO);
      l.Autor = a;
      
      Libro libro = _libroService.Create(l);
      return CreatedAtAction(nameof(GetById), new { id = libro.Id}, libro);
    }

    [HttpPut("{id}")]
    public ActionResult<Libro> Update(int id, Libro l)
    {
      try
      {
        bool libro = _libroService.Update(id, l);
        if ( libro is false ) return NotFound(new {Message = $"No se encontro el libro con id: {id}"});
        return NoContent();
      }
      catch (System.Exception e)
      {
        Console.WriteLine(e.Message);
        return Problem(detail: e.Message, statusCode: 500);
      }
    }

    [HttpDelete]
    public ActionResult Delete(int id)
    {
      bool deleted = _libroService.Delete(id);
      if (deleted) return NoContent();
      return NotFound();
    }
}