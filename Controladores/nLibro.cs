public class nLibro
{
  public static Libro Crear(int id, List<Autor> autores)
  {
    Console.Clear();
    Console.WriteLine("----------------------");
    Console.WriteLine("\tCrear Libro");
    Console.WriteLine("----------------------");
    Console.Write("Ingrese el título: ");
    string titulo = Console.ReadLine();

    // Mostrar la lista de autores para seleccionar uno
    Console.WriteLine("Seleccione un autor:");
    Autor autor = nAutor.Seleccionar(autores);

    Libro libro = new (id, titulo, autor);
    return libro;
  }

  public static int MaximoId(List<Libro> libros)
  {
    int max = 0;
    foreach (Libro libro in libros)
    {
      if (libro.Id > max)
      {
        max = libro.Id;
      }
    }
    return max;
  }

  public static void Listar(List<Libro> libros)
  {
    Console.WriteLine("----------------------");
    Console.WriteLine("\tLibros");
    Console.WriteLine("----------------------");
    foreach (Libro libro in libros)
    {
      Console.WriteLine($"{libros.IndexOf(libro)} - {libro.Titulo} ({libro.Id}), Autor: {libro.Autor.Nombre} {libro.Autor.Apellido}");
    }
  }

  public static Libro Seleccionar(List<Libro> libros)
  {
    Listar(libros);
    Console.Write("Seleccione un libro: ");
    int i = int.Parse(Console.ReadLine());
    return libros[i];
  }

  public static void Modificar(Libro libro, List<Autor> autores)
  {
    Console.WriteLine($"----------------------------------------");
    Console.WriteLine($"Modificando: {libro.Titulo}");
    Console.WriteLine($"----------------------------------------");
    Console.Write("Ingrese el nuevo título: ");
    libro.Titulo = Console.ReadLine();

    Console.WriteLine("Seleccione un nuevo autor:");
    Autor nuevoAutor = nAutor.Seleccionar(autores);
    libro.Autor = nuevoAutor;
  }

  public static void Eliminar(List<Libro> libros)
  {
    Console.Clear();
    Libro libro = Seleccionar(libros);
    libros.Remove(libro);
  }

  //Listar libros de un autor
  public static void ListarLibrosParaUnAutor(Autor a, List<Libro> libros){
    Console.WriteLine($"Libros de : {a.Nombre} {a.Apellido}");
    foreach (Libro libro in libros)
    {
      if(libro.Autor.Id == a.Id){
        Console.WriteLine($"{libro.Titulo}");
      }
    }
    Console.ReadKey();
  }
}
