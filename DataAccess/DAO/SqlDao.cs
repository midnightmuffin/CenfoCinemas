using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{

    /*
     * Clase u objeto que se encarga de la comunicación con la base de datos SQL Server.
     * Solo ejecuta stored procedures.
     * Esta clase implementa el patron de Singleton para asegurar la existencia de una única instancia.
     */
    public class SqlDao
    {
        //Paso 1: Crear una instacia privada estática de la clase SqlDao.

        private static SqlDao _instance;

        private string _connectionString;

        //Paso 2: Redefinir el constructor default como privado para evitar que se pueda crear una instancia de la clase desde fuera.

        private SqlDao()
        {
            _connectionString = string.Empty; 
        }

        //Paso 3: Definir el metodo que expondrá la instancia de la clase SqlDao.

        public static SqlDao GetInstance()
        {
            //Verificar si la instancia es nula, si lo es, crear una nueva instancia de la clase SqlDao.
            if (_instance == null)
            {
                _instance = new SqlDao();
            }
            //Retornar la instancia de la clase SqlDao.
            return _instance;
        }

        //Metodo para la ejecución de stored procedures sin retorno de datos.

        public void ExecuteProcedure(SqlOperation operation)
        {
            // Conectarse a la base de datos y ejecutar el stored procedure.
        }

        //Metodo para la ejecución de stored procedures con retorno de datos.

        public List<Dictionary<string, object>> ExecuteQueryProcedure(SqlOperation operation)
        {
            // Conectarse a la base de datos y ejecutar el stored procedure, capturar el resultado y convertirlo en DTOs
            var list = new List<Dictionary<string, object>>();
            return list;
        }
    }
}
