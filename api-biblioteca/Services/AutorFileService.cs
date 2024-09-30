
using System.Text.Json;

public class AutorFileService
{
    private readonly string _filePath = "Data/Autores.json";
    private readonly IFileStorageService _fileStorageService;

    public AutorFileService(IFileStorageService fileStorageService)
    {
      _fileStorageService = fileStorageService;
    }
    public Autor Create(Autor a)
    {
        // Leer el contenido del archivo JSON
        var json = _fileStorageService.Read(_filePath);
        // Deserializar el JSON en una lista de autores
        var autores = JsonSerializer.Deserialize<List<Autor>>(json) ?? new List<Autor>();
        // Agregar el nuevo autor a la lista
        autores.Add(a);
        // Serializar la lista actualizada de vuelta a JSON
        json = JsonSerializer.Serialize(autores);
        // Escribir el JSON actualizado en el archivo
        _fileStorageService.Write(_filePath, json);
        return a;
    }

    public void Delete(int id)
    {
        // Leer el contenido del archivo JSON
        var json = _fileStorageService.Read(_filePath);
        // Deserializar el JSON en una lista de autores
        var autores = JsonSerializer.Deserialize<List<Autor>>(json) ?? new List<Autor>();
        // Buscar el autor por id
        var autor = autores.Find(autor => autor.Id == id);

        // Si el autor existe, eliminarlo de la lista
        if (autor is not null) 
        {
            autores.Remove(autor);
            // Serializar la lista actualizada de vuelta a JSON
            json = JsonSerializer.Serialize(autores);
            // Escribir el JSON actualizado en el archivo
            _fileStorageService.Write(_filePath, json);
        }
    }

    public IEnumerable<Autor> GetAll()
    {
        //Leo el contenido del archivo
        var json = _fileStorageService.Read(_filePath);
        //Deserializo el Json en una lista de Autores si es nulo retorna una lista vacia
        return JsonSerializer.Deserialize<List<Autor>>(json) ?? new List<Autor>();
    }

    public Autor? GetById(int id)
    {
        //Leo el contenido del archivo
        var json = _fileStorageService.Read(_filePath);
        //Deserializo el Json en una lista de Autores
        List<Autor>? autores = JsonSerializer.Deserialize<List<Autor>>(json);
        if(autores is null) return null;
        //Busco el autor por Id y devuelvo el autor encontrado
        return autores.Find(a => a.Id == id);  

    }

    public Autor? Update(int id, Autor autor)
    {
         // Leer el contenido del archivo JSON
        var json = _fileStorageService.Read(_filePath);
        // Deserializar el JSON en una lista de autores
        var autores = JsonSerializer.Deserialize<List<Autor>>(json) ?? new List<Autor>();
        // Buscar el índice del autor por id
        var autorIndex = autores.FindIndex(a => a.Id == id);

        // Si el autor existe, reemplazarlo en la lista
        if (autorIndex >= 0) 
        {
            //reeplazo el autor de la lista por el autor recibido por parametro con los nuevos datos
            autores[autorIndex] = autor;
            // Serializar la lista actualizada de vuelta a JSON
            json = JsonSerializer.Serialize(autores);
            // Escribir el JSON actualizado en el archivo
            _fileStorageService.Write(_filePath, json);
            return autor;
        }

        // Retornar null si el autor no fue encontrado
        return null;
    }
    
}