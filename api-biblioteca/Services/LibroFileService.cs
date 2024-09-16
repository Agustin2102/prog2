using System.Text.Json;

public class LibroFileService : ILibroService
{
    private readonly IFileStorageService _fileStorageService;
    private readonly IAutorService _autorService;
    private readonly string _filePath = "Data/Libros.json";

    public LibroFileService(IFileStorageService fileStorageService, IAutorService autorService)
    {
      _fileStorageService = fileStorageService;
      _autorService = autorService;
    }
    public Libro Create(Libro l)
    {
      List<Libro> libros = (List<Libro>)GetAll();

      //Encontramos el maximo id existente
      int lastId = libros.Max( l => l.Id);
      l.Id = lastId + 1;

      libros.Add(l);
      var json = JsonSerializer.Serialize(libros);
      _fileStorageService.Write(_filePath , json);
      return l;
    }

    public bool Delete(int id)
    {
      List<Libro> libros = (List<Libro>)GetAll();
      Libro? libroParaEliminar =  libros.Find( l => l.Id == id);
      
      if( libroParaEliminar is null ) return false;
      
      bool deleted = libros.Remove(libroParaEliminar) ;
      if ( deleted ) 
      {
        var json = JsonSerializer.Serialize(libros);
        _fileStorageService.Write(_filePath , json);
      }
      return deleted;
    }

    public IEnumerable<Libro> GetAll()
    {
      var json = _fileStorageService.Read(_filePath);
      return JsonSerializer.Deserialize<List<Libro>>(json) ?? new();
    }

    public Libro? GetById(int id)
    {
      List<Libro> libros = (List<Libro>)GetAll();
      return libros.Find( l => l.Id == id);
    }

    public Boolean Update(int id, Libro l)
    {
      List<Libro> libros = (List<Libro>)GetAll();
      int index = libros.FindIndex( li => li.Id == id);
      //No se encontró el id que se quiere actualizar
      if ( index == -1 ) return false;

      // El autor que se quiere cargar existe?
      // bool autorValido = _autorService.ElAutorExiste(l.Autor);
      // if ( autorValido == false ) throw new HttpStatusCodeException("El autor recibido no es válido", 400);
      libros[index] = l;
      _fileStorageService.Write(_filePath, JsonSerializer.Serialize(libros));
      return true;
    }
}