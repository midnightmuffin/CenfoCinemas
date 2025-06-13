using DataAccess.CRUD;
using DataAccess.DAO;
using DTOs;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;
using System.Xml.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("===== MENÚ PRINCIPAL =====");
            Console.WriteLine("1. Crear Usuario");
            Console.WriteLine("2. Consultar Usuarios");
            Console.WriteLine("3. Actualizar Usuario");
            Console.WriteLine("4. Eliminar Usuario");
            Console.WriteLine("5. Registrar Película");
            Console.WriteLine("6. Consultar Películas");
            Console.WriteLine("7. Actualizar Película");
            Console.WriteLine("8. Eliminar Película");
            Console.WriteLine("9. Salir");
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
                    ActualizarUsuario();
                    break;
                case "4":
                    EliminarUsuario();
                    break;
                case "5":
                    RegistrarPelicula();
                    break;
                case "6":
                    ConsultarPeliculas();
                    break;
                case "7":
                    ActualizarPelicula();
                    break;
                case "8":
                    EliminarPelicula();
                    break;
                case "9":
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
        try
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
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime birthDate))
            {
                Console.WriteLine("Fecha inválida. Debe tener el formato YYYY-MM-DD.");
                return;
            }

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

            var uCrud = new UserCrudFactory();
            uCrud.Create(user);

            Console.WriteLine("Usuario creado exitosamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al crear usuario: {ex.Message}");
        }
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
        try
        {
            Console.WriteLine("=== Registrar Nueva Película ===");

            Console.Write("Título: ");
            string title = Console.ReadLine();

            Console.Write("Descripción: ");
            string description = Console.ReadLine();

            Console.Write("Fecha de Estreno (YYYY-MM-DD): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime releaseDate))
            {
                Console.WriteLine("Fecha inválida. Debe tener el formato YYYY-MM-DD.");
                return;
            }

            Console.Write("Género: ");
            string genre = Console.ReadLine();

            Console.Write("Director: ");
            string director = Console.ReadLine();

            var sqlOperation = new SqlOperation
            {
                ProcedureName = "CRE_MOVIE_PR"
            };

            sqlOperation.AddStringParameter("P_Title", title);
            sqlOperation.AddStringParameter("P_Description", description);
            sqlOperation.AddDateTimeParam("P_ReleaseDate", releaseDate);
            sqlOperation.AddStringParameter("P_Genre", genre);
            sqlOperation.AddStringParameter("P_Director", director);

            var sqlDao = SqlDao.GetInstance();
            sqlDao.ExecuteProcedure(sqlOperation);

            Console.WriteLine("Película registrada exitosamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al registrar película: {ex.Message}");
        }
    }

    public static void ConsultarPeliculas()
    {
        Console.WriteLine("Consultando películas...");
        // Lógica SELECT de películas
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
