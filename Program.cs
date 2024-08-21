// 1- Ok- Crear función que genere un listado de autores.
// 2- Ok- Crear una función que imprima un listado de autores.
// 3- Modificar el código anterior para que se agreguen autores al listado ya existente. No permitir ingresar id.
// 4- Crear menú (función) con opciones para agregar, modificar o eliminar un autor.
// 6- Agregar la opción de poder listar los libros de un autor.
// 5- Agregar la opción de poder cargar un libro nuevo a un autor.

void Iniciar()
{
    List<Autor> autores = GenerarAutores();
    Console.Write("Ingrese total de autores a crear: ");
    int total = int.Parse(Console.ReadLine());
    

    for (int i = 0; i < total; i++)
    {
      Autor a = new();
      // Console.Write("Ingrese el ID: ");
      // a.Id = int.Parse(Console.ReadLine());
      a.Id = autores.Count + 1;
      Console.Write("Ingrese el nombre: ");
      a.Nombre = Console.ReadLine();
      Console.Write("Ingrese el apellido: ");
      a.Apellido = Console.ReadLine();
      autores.Add(a);
    }

    ImprimirAutores(autores);

    Libro l = new();
    l.Id = 1;
    l.Titulo = "Aprendiendo TS";
    l.Autor = autores[0];
    Console.WriteLine("------Libros-------");
    Console.WriteLine($"Titulo: {l.Titulo}");
    Console.WriteLine($"Autor: {l.Autor}");
}


List<Autor> GenerarAutores(){
  List<Autor> autores = new List<Autor>();
  autores.Add(new Autor(1,"Juan", "Perez"));
  autores.Add(new Autor(2,"Pedro", "Lopez"));
  autores.Add(new Autor(3,"Lucia", "Fernandez"));
  autores.Add(new Autor(7,"Maria", "Frick"));
  return autores;
}

void ImprimirAutores(List<Autor> autores){
  foreach (Autor a in autores)
  {
    Console.WriteLine($"Id: {a.Id} - {a.Nombre} {a.Apellido}");
  }
}

Iniciar();

