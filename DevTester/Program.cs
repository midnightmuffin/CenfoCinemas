using CoreApp;
using DataAccess.CRUD;
using DataAccess.DAO;
using DTOs;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;

public class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("===== MENÚ PRINCIPAL =====");
            Console.WriteLine("1. Crear Usuario");
            Console.WriteLine("2. Consultar Usuarios");
            Console.WriteLine("3. Consultar Usuario por Id");
            Console.WriteLine("4. Consultar Usuario por Código de Usuario");
            Console.WriteLine("5. Actualizar Usuario");
            Console.WriteLine("6. Eliminar Usuario");
            Console.WriteLine("7. Registrar Película");
            Console.WriteLine("8. Consultar Películas");
            Console.WriteLine("9. Consultar Película por Id");
            Console.WriteLine("10. Actualizar Película");
            Console.WriteLine("11. Eliminar Película");
            Console.WriteLine("12. Salir");
            Console.Write("Seleccione una opción: ");

            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    CrearUsuario();
                    break;
                case "2":
                    ConsultarUsuarios();
                    break;
                case "3":
                    ConsultarUsuarioPorId();
                    break;
                case "4":
                    ConsultarUsuarioPorCodigo();
                    break;
                case "5":
                    ActualizarUsuario();
                    break;
                case "6":
                    EliminarUsuario();
                    break;
                case "7":
                    RegistrarPelicula();
                    break;
                case "8":
                    ConsultarPeliculas();
                    break;
                case "9":
                    ConsultarPeliculaPorId();
                    break;
                case "10":
                    ActualizarPelicula();
                    break;
                case "11":
                    EliminarPelicula();
                    break;
                case "12":
                    Console.WriteLine("¡Gracias por usar el menú!");
                    return;
                default:
                    Console.WriteLine("Opción inválida. Intente de nuevo.");
                    break;
            }

            Console.WriteLine();
        }
    }

    public static void CrearUsuario()
    {

        Console.WriteLine("=== Crear Nuevo Usuario ===");

        Console.Write("Código de Usuario: ");
        string userCode = Console.ReadLine();

        Console.Write("Nombre: ");
        string name = Console.ReadLine();

        Console.Write("Correo Electrónico: ");
        string email = Console.ReadLine();

        Console.Write("Contraseña: ");
        string password = Console.ReadLine();

        Console.Write("Fecha de nacimiento (YYYY-MM-DD): ");
        var birthDate = DateTime.Parse(Console.ReadLine());

        Console.Write("Estado (ej: AC): ");
        string status = Console.ReadLine();

        //Creamos el objeto del usuario a partir de los datos ingresados

        var user = new User()
        {
            UserCode = userCode,
            Name = name,
            Email = email,
            Password = password,
            BirthDate = birthDate,
            Status = status
        };

        var uManager = new UserManager();
        uManager.Create(user);

        Console.WriteLine("Usuario creado exitosamente.");

    }

    public static void ConsultarUsuarios()
    {
        Console.WriteLine("Consultando usuarios...");

        var uCrud = new UserCrudFactory();
        var listUsers = uCrud.RetrieveAll<User>();
        foreach (var u in listUsers)
        {
            Console.WriteLine(JsonConvert.SerializeObject(u));
        }

    }

    public static void ConsultarUsuarioPorId()
    {
        Console.WriteLine("Consultando usuario por Id...");

        try
        {
            Console.Write("Ingrese el ID del usuario: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var uCrud = new UserCrudFactory();
            var u = uCrud.RetrieveById<User>(id);

            if (u == null)
            {
                Console.WriteLine("Usuario no encontrado.");
                return;
            }

            Console.WriteLine("Usuario encontrado:\n" + JsonConvert.SerializeObject(u));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al consultar usuario: {ex.Message}");
        }
    }

    public static void ConsultarUsuarioPorCodigo()
    {
        Console.WriteLine("Consultando usuario por código de usuario...");

    }

    public static void ActualizarUsuario()
    {
        Console.WriteLine("Actualizando usuario...");
        // Lógica UPDATE usuario
    }

    public static void EliminarUsuario()
    {
        Console.WriteLine("Eliminando usuario...");
        // Lógica DELETE usuario
    }

    public static void RegistrarPelicula()
    {

        Console.WriteLine("=== Registrar Nueva Película ===");

        Console.Write("Título: ");
        string title = Console.ReadLine();

        Console.Write("Descripción: ");
        string description = Console.ReadLine();

        Console.Write("Fecha de Estreno (YYYY-MM-DD): ");
        var releaseDate = DateTime.Parse(Console.ReadLine());

        Console.Write("Género: ");
        string genre = Console.ReadLine();

        Console.Write("Director: ");
        string director = Console.ReadLine();

        //Creamos el objeto de la pelicula a partir de los datos ingresados

        var movie = new Movie()
        {
            Title = title,
            Description = description,
            ReleaseDate = releaseDate,
            Genre = genre,
            Director = director
        };

        var mManager = new MovieManager();
        mManager.Create(movie);

        Console.WriteLine("Película registrada exitosamente.");

    }

    public static void ConsultarPeliculas()
    {
        Console.WriteLine("Consultando peliculas...");

        var uCrud = new MovieCrudFactory();
        var listMovies = uCrud.RetrieveAll<Movie>();
        foreach (var m in listMovies)
        {
            Console.WriteLine(JsonConvert.SerializeObject(m));
        }
    }

    public static void ConsultarPeliculaPorId()
    {
        Console.WriteLine("Consultando película por Id...");

        try
        {
            Console.Write("Ingrese el ID de la película: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var uCrud = new MovieCrudFactory();
            var m = uCrud.RetrieveById<Movie>(id);

            if (m == null)
            {
                Console.WriteLine("Película no encontrada.");
                return;
            }

            Console.WriteLine("Película encontrada:\n" + JsonConvert.SerializeObject(m));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al consultar película: {ex.Message}");
        }
    }

    public static void ActualizarPelicula()
    {
        Console.WriteLine("Actualizando película...");
        // Lógica UPDATE película
    }

    public static void EliminarPelicula()
    {
        Console.WriteLine("Eliminando película...");
        // Lógica DELETE película
    }
}
