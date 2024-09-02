
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
    Console.WriteLine("11- Salir ");

    //Leer la opción seleccionada
    int op = int.Parse(Console.ReadLine());

    switch (op)
    {
      // 1 - Agregar autor
      case 1: autores.Add(nAutor.Crear(nAutor.MaximoId(autores) + 1)); Menu(); break;
      // 2 - Listar autores
      case 2: Console.Clear(); nAutor.Listar(autores); Console.ReadKey(); Menu();break;
      // 3 - Modificar autor
      case 3: nAutor.Modifcar(nAutor.Seleccionar(autores)); Menu(); break;
      // 4 - Eliminar autor
      case 4: nAutor.Eliminar(autores); Menu(); break;
      // 5 - Crear libro
      case 5: libros.Add(nLibro.Crear(nLibro.MaximoId(libros) + 1, autores)); Menu(); break;
      // 6 - Listar libros
      case 6: nLibro.Listar(libros); Console.ReadKey(); Menu(); break;
      // 7 - Modificar un libro
      case 7: nLibro.Modificar(nLibro.Seleccionar(libros) , autores); Menu(); break;
      // 8 - Eliminar un libro
      case 8: nLibro.Eliminar(libros); Menu(); break;
      // 9 - Listar libros por autor
      case 9: nLibro.ListarLibrosParaUnAutor(nAutor.Seleccionar(autores), libros); Menu(); break;
      // 10 - Listar Autores y sus libros
      case 10: nAutor.Listar(autores, libros); Console.ReadKey(); Menu(); break;
      // 11 - Salir
      case 11: break;
    }

  }

  static void GenerarAutores(){
    autores.Add(new Autor(1,"Juan", "Perez"));
    autores.Add(new Autor(2,"Pedro", "Lopez"));
    autores.Add(new Autor(3,"Lucia", "Fernandez"));
    autores.Add(new Autor(7,"Maria", "Frick"));
  }

  static void GenerarLibros(){
    libros.Add(new Libro(1,"Don quijote", autores[1]));
    libros.Add(new Libro(2,"Harry y el caliz", autores[1]));
    libros.Add(new Libro(3,"El principito", autores[2]));
    libros.Add(new Libro(4,"El lobo", autores[2]));
  }

}

