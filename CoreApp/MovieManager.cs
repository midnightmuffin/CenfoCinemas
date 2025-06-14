using DataAccess.CRUD;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class MovieManager : BaseManager
    {

        /*
         * Metodo para la creacion de una pelicula
         * Valida que el título de la película no exista en la base de datos
         */
        public void Create(Movie movie)
        {
            try
            {
                var mCrud = new MovieCrudFactory();
                var mExist = mCrud.RetrieveByTitle<Movie>(movie);

                if (mExist == null)
                {
                    mCrud.Create(movie);

                    var uCrud = new UserCrudFactory();
                    var users = uCrud.RetrieveAll<User>();

                    var emailManager = new EmailManager();
                    emailManager.SendNewMovie(movie.Title, users).GetAwaiter().GetResult();
                }
                else
                {
                    throw new Exception("El título de la película ya existe. Por favor, ingrese otro título.");
                }
            }
            catch (Exception ex)
            {
                ManageException(ex);
            }
        }
    }
}
