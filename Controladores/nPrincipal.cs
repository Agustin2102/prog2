
public class nPrincipal {

  public static List<Autor> autores = new List<Autor>();
  public static List<Libro> libros = new List<Libro>();

  public static void Inicio()
  {
    GenerarAutores();
    GenerarLibros();
    Menu();
  }

  public static void Menu (){
    Console.Clear();

    //Imprimir opciones:
    Console.WriteLine("Seleccione una opción:");
    Console.WriteLine("1- Crear Autor");
    Console.WriteLine("2- Listar Autores");
    Console.WriteLine("3- Modificar un autor");   
    Console.WriteLine("4- Eliminar un autor");
    Console.WriteLine("5- Crear Libro");
    Console.WriteLine("6- Listar Libros");
    Console.WriteLine("7- Modificar un libro");
    Console.WriteLine("8- Eliminar un libro");
    Console.WriteLine("9- Listar libros por Autor");
    Console.WriteLine("10- Listar Autores y sus libros");
    Console.WriteLine("11- Probar opcions LINQ");
    Console.WriteLine("12- Salir ");

    //Leer la opción seleccionada
    int op = int.Parse(Console.ReadLine());

    switch (op)
    {
      // 1 - Agregar autor
      case 1: autores.Add(nAutor.Crear(nAutor.MaximoId(autores) + 1)); Menu(); break;
      // 2 - Listar autores
      case 2: autores =  autores.OrderByDescending(x => libros.Where(l => l.Autor == x).ToList().Count).ToList(); Console.Clear(); nAutor.Listar(autores); Console.ReadKey(); Menu();break;
      // 3 - Modificar autor
      case 3: nAutor.Modifcar(nAutor.Seleccionar(autores)); Menu(); break;
      // 4 - Eliminar autor
      case 4: nAutor.Eliminar(autores); Menu(); break;
      // 5 - Crear libro
      case 5: libros.Add(nLibro.Crear(nLibro.MaximoId(libros) + 1, autores)); Menu(); break;
      // 6 - Listar libros
      case 6: //libros = libros.Where(l => l.Autor.Id == 2 ).ToList(); 
      nLibro.Listar(libros);
       Console.ReadKey(); Menu(); break;
      // 7 - Modificar un libro
      case 7: nLibro.Modificar(nLibro.Seleccionar(libros) , autores); Menu(); break;
      // 8 - Eliminar un libro
      case 8: nLibro.Eliminar(libros); Menu(); break;
      // 9 - Listar libros por autor
      case 9: nLibro.ListarLibrosParaUnAutor(nAutor.Seleccionar(autores), libros); Menu(); break;
      // 10 - Listar Autores y sus libros
      case 10: nAutor.Listar(autores, libros); Console.WriteLine("---------------"); nAutor.Listar(libros); Console.ReadKey(); Menu(); break;
      // 11 - Opciones Linq
      case 11: OpcionesLinq(); Menu();break;
      // 12 - Salir
      case 12:Console.Clear(); break;
    }

  }

  static void GenerarAutores(){
    autores.Add(new Autor(1,"Juan", "Perez"));
    autores.Add(new Autor(2,"Pedro", "Lopez"));
    autores.Add(new Autor(3,"Lucia", "Fernandez"));
    autores.Add(new Autor(7,"Maria", "Frick"));
  }

  static void GenerarLibros(){
    libros.Add(new Libro(1,"Don quijote", autores[1], 345));
    libros.Add(new Libro(2,"Harry y el caliz", autores[1], 540));
    libros.Add(new Libro(3,"El principito", autores[2], 540));
    libros.Add(new Libro(4,"El lobo", autores[2], 209));
    libros.Add(new Libro(5,"Analisis Matematico 1", autores[0], 750));
    libros.Add(new Libro(6,"El programador", autores[3], 109));
    libros.Add(new Libro(7,"APIRestfull", autores[3], 809));


  }

  static void OpcionesLinq()
  {
    Console.Clear();
    Console.WriteLine("Ejemplos LINQ");
    Console.WriteLine("1 - Usando Where (Libros de un autor especifico)");
    Console.WriteLine("2 - Usando OrderBy (ordeno autores por Nombre)");
    Console.WriteLine("3 - Usando OrderByDescending (ordeno autores por cantidad de libros)");
    Console.WriteLine("4 - Usando OrderByDescending and ThenBy (ordeno libros por cantidad de paginas y titulo)");
    Console.WriteLine("5 - Usando GroupBy (Libros agrupados por Autor)");
    Console.WriteLine("6 - Usando Max (Maxima cantidad de paginas)");
    Console.WriteLine("7 - Usando Average (Promedio de paginas)");
    Console.WriteLine("8 - Usando Select (Obteniendo los nombres de los autores)");
    Console.WriteLine("9 - Salir");
    Console.Write("Seleccione una opcion: ");
    int o = int.Parse(Console.ReadLine());

    switch(o)
    {
      case 1: Console.Clear();LibrosWhereAutor(nAutor.Seleccionar(autores));Console.ReadKey(); OpcionesLinq(); break; 
      case 2: OrderByAutorNombre(); Console.ReadKey(); OpcionesLinq(); break;
      case 3: OrderByDescendingAutorLibros(); Console.ReadKey(); OpcionesLinq(); break;
      case 4: OrderByThenByLibros(); Console.ReadKey(); OpcionesLinq(); break;
      case 5: GroupByLibrosAutor(); Console.ReadKey(); OpcionesLinq(); break;
      case 6: MaxPaginas(); Console.ReadKey(); OpcionesLinq(); break;
      case 7: AveragePaginas(); Console.ReadKey(); OpcionesLinq(); break;
      case 8: SelectNombres(); Console.ReadKey(); OpcionesLinq(); break;
      case 9: break;
    }

  }

  static void LibrosWhereAutor(Autor a)
  {
    Console.Clear();
    Console.WriteLine("**************************");
    Console.WriteLine(a);
    Console.WriteLine("**************************");
    foreach(Libro l in libros.Where(li => li.Autor == a).ToList())
    {
      Console.WriteLine($"\t{l.Titulo}");
    }
    Console.WriteLine("**************************");
  }

  static void OrderByAutorNombre()
  {
    
    Console.Clear();
    Console.WriteLine("**************************");
    Console.WriteLine("          Autores  ");
    Console.WriteLine("**************************");
    foreach(Autor a in autores.OrderBy(x => x.Nombre))
    {
      Console.WriteLine(a);
    }    
    Console.WriteLine("**************************");
  }

  static void OrderByDescendingAutorLibros()
  {
    Console.Clear();
    Console.WriteLine("**************************");
    Console.WriteLine("          Autores  ");
    Console.WriteLine("**************************");
    //Uso libros.Where para obtener solo los libro del autor x y poder usar el Count para saber cuantos libros hay y ordenar por este dato
    foreach(Autor a in autores.OrderByDescending(x => libros.Where(l => l.Autor == x).ToList().Count).ToList())
    {
      Console.WriteLine($"{a} - Libros:{libros.Where(l => l.Autor == a).ToList().Count} ");
    }    
    Console.WriteLine("**************************");

  }

  static void OrderByThenByLibros()
  {
    Console.Clear();
    Console.WriteLine("**************************");
    Console.WriteLine("          Libros  ");
    Console.WriteLine("**************************");
    //Uso libros.Where para obtener solo los libro del autor x y poder usar el Count para saber cuantos libros hay y ordenar por este dato
    foreach(Libro l in libros.OrderByDescending(x => x.Paginas).ThenBy(p => p.Titulo) )
    {
      Console.WriteLine($"{l.Titulo} - {l.Paginas}");
    }    
    Console.WriteLine("**************************");
  }

  static void GroupByLibrosAutor()
  {
    //Agrupo libros por autor
    //El resultado del groupBy es una colecion de datos con Clave(KEY) y valor
    var librosAgrupados = libros.GroupBy(l => l.Autor);
    //Recorro la coleccion obtenida la cual seran las diferentes claves de agrupacion, en el ejemplo los autores con libros
    foreach(var l in librosAgrupados)
    {
      //Imprimo los datos de Key que en este caso es de tipo Autor
      Console.WriteLine("**************************");
      Console.WriteLine($"{l.Key.Nombre} {l.Key.Apellido}");
      Console.WriteLine("**************************");
      //Recorro la collecion de datos asociadas a la clave en este caso un listado de libros
      foreach(var x in l)
      {
        Console.WriteLine($"    {x.Id} -  {x.Titulo} ({x.Paginas} pag.)");
      }
    }
    Console.WriteLine("**************************");
  }

  static void MaxPaginas()
  {
    //Obtengo el valor maximko de paginas de todos los libros
    var max = libros.Max(l => l.Paginas);
    Console.Clear(); 
    Console.WriteLine($"Numero maximo de paginas de un libro: {max}");
    Console.WriteLine($"Libros con {max} paginas");
    //Recorro los libro donde (where) la cantidad de paginas coincida con el Max obtenido anteriormente
    foreach(Libro libro in libros.Where(l => l.Paginas == max))
    {
      Console.WriteLine($"{libro.Titulo} ({libro.Autor.Nombre} {libro.Autor.Apellido})");
    }
  }

  static void AveragePaginas()
  {
    var avg = libros.Average(l => l.Paginas);
    Console.Clear(); 
    Console.WriteLine($"Promedio de paginas de los libros: {avg}");
  }

  static void SelectNombres()
  {
    Console.Clear();
    //Utilizo select para obtener de la lista de autores solo los nombres, el resultado es una lista de valores del tipo de dato que estoy
    //seleccionado en este caso como el dato es el nombre obtengo una lista de string.
    var nombres = autores.Select(a => a.Nombre);
    Console.Clear();
    Console.WriteLine("**************************");
    Console.WriteLine("          Nombres  ");
    Console.WriteLine("**************************");
    foreach(string n in nombres)
    {
      Console.WriteLine(n);
    }
  }
}

