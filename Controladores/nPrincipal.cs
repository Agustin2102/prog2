
public class nPrincipal {

  public static List<Autor> autores = new List<Autor>(); 

  public static void Inicio()
  {
    GenerarAutores();
    Menu();
  }

  public static void Menu (){
    Console.Clear();

    //Imprimir opciones:
    Console.WriteLine("Seleccione una opción:");
    // Agregar autor
    Console.WriteLine("1- Crear Autor");
    //Listar autores
    Console.WriteLine("2- Listar Autores");
    // Modificar autor
    Console.WriteLine("3- Modificar un autor");
    // Eliminar autor
    Console.WriteLine("4- Eliminar un autor");
    //Salir
    Console.WriteLine("5- Salir ");

    //Leer la opcion seleccionada
    int op = int.Parse(Console.ReadLine());

    switch (op)
    {
      case 1: autores.Add(nAutor.Crear(nAutor.MaximoId(autores) + 1));
        Menu();
        break;
      case 2: Console.Clear(); nAutor.Listar(autores); Console.ReadKey(); Menu();break;
      case 3: nAutor.Modifcar(nAutor.Seleccionar(autores)); Menu(); break;
      case 4: Console.WriteLine("Opción no disponible");break;
      case 5: break;
    }

  }

  // public static void AgregarAutores()
  // {
  //     List<Autor> autores = GenerarAutores();
  //     Console.Write("Ingrese total de autores a crear:  ");
  //     int total = int.Parse(Console.ReadLine());
      

  //     for (int i = 0; i < total; i++)
  //     {
  //       Autor a = new();
  //       a.Id = autores[autores.Count -1 ].Id + 1;
  //       Console.Write("Ingrese el nombre: ");
  //       a.Nombre = Console.ReadLine();
  //       Console.Write("Ingrese el apellido: ");
  //       a.Apellido = Console.ReadLine();
  //       autores.Add(a);
  //     }

  //     ImprimirAutores(autores);

  //     // Libro l = new();
  //     // l.Id = 1;
  //     // l.Titulo = "Aprendiendo TS";
  //     // l.Autor = autores[0];
  //     // Console.WriteLine("------Libros-------");
  //     // Console.WriteLine($"Titulo: {l.Titulo}");
  //     // Console.WriteLine($"Autor: {l.Autor}");
  // }

  static void GenerarAutores(){
    //List<Autor> autores = new List<Autor>();
    autores.Add(new Autor(1,"Juan", "Perez"));
    autores.Add(new Autor(2,"Pedro", "Lopez"));
    autores.Add(new Autor(3,"Lucia", "Fernandez"));
    autores.Add(new Autor(7,"Maria", "Frick"));
  }

  void ImprimirAutores(List<Autor> autores){
    foreach (Autor a in autores)
    {
      Console.WriteLine($" {a.Nombre} {a.Apellido}");
    }
  }

}

