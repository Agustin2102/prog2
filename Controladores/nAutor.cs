public class nAutor
{
  public static Autor Crear(int id)
  {
    Console.Clear();
    Console.WriteLine("----------------------");
    Console.WriteLine("\tCrear Autor");
    Console.WriteLine("----------------------");
    Autor a = new Autor();
    a.Id = id;
    Console.Write("Ingrese el nombre: ");
    a.Nombre = Console.ReadLine();
    Console.Write("Ingrese el apellido: ");
    a.Apellido = Console.ReadLine();
    return a;
  }

  public static int MaximoId(List<Autor> autores)
  {
    int max = 0;
    foreach(Autor a in autores)
    {
      if(a.Id > max)      
      {
        max = a.Id;
      }
    }
    return max;
  }

  public static void Listar(List<Autor> autores)
  {
    Console.WriteLine("----------------------");
    Console.WriteLine("\tAutores");
    Console.WriteLine("----------------------");
    foreach(Autor a in autores)
    {
      Console.WriteLine($"{autores.IndexOf(a)} - {a.Nombre} {a.Apellido} ({a.Id})");
    }
  }
  public static void Listar(List<Autor> autores, List<Libro> libros)
  {
    Console.WriteLine("----------------------");
    Console.WriteLine("\tAutores");
    Console.WriteLine("----------------------");
    foreach(Autor a in autores)
    {
      Console.WriteLine($"{autores.IndexOf(a)} - {a.Nombre} {a.Apellido} ({a.Id})");
      List<Libro> librosDelAutor = libros.FindAll( (el) => el.Autor.Id == a.Id);
      if (librosDelAutor.Count > 0){
        Console.WriteLine("   Libros:");
        librosDelAutor.ForEach( libro => {
          Console.WriteLine($"    - {libro.Titulo}");
        });
      }
      Console.WriteLine("----------------------");
    }
  }

  public static Autor Seleccionar(List<Autor> autores)
  {
    Listar(autores);
    Console.Write("Seleccione un autor: ");
    int i = int.Parse(Console.ReadLine());
    return autores[i];
  }

  public static void Modifcar(Autor a)
  {
    Console.WriteLine($"----------------------------------------");
    Console.WriteLine($"Modificando: {a.Nombre} {a.Apellido}");
    Console.WriteLine($"----------------------------------------");
    Console.Write("Ingrese el nuevo nombre: ");
    a.Nombre = Console.ReadLine();
    Console.Write("Ingrese el nuevo apellido: ");
    a.Apellido = Console.ReadLine();
  }

  public static void Eliminar(List<Autor> autores){
    Console.Clear();
    Autor a = Seleccionar(autores);
    autores.Remove(a);
  }
}