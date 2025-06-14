﻿using DataAccess.DAO;
using DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class MovieCrudFactory : CrudFactory
    {
        public MovieCrudFactory()
        {
            _sqlDao = SqlDao.GetInstance();
        }

        public override void Create(BaseDTO baseDTO)
        {
            var movie = baseDTO as Movie;

            var sqlOperation = new SqlOperation() { ProcedureName = "CRE_MOVIE_PR" };

            sqlOperation.AddStringParameter("P_Title", movie.Title);
            sqlOperation.AddStringParameter("P_Description", movie.Description);
            sqlOperation.AddDateTimeParam("P_ReleaseDate", movie.ReleaseDate);
            sqlOperation.AddStringParameter("P_Genre", movie.Genre);
            sqlOperation.AddStringParameter("P_Director", movie.Director);

            _sqlDao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseDTO baseDTO)
        {
            throw new NotImplementedException();
        }

        public override T Retrieve<T>()
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstMovies = new List<T>();

            var sqlOperation = new SqlOperation() { ProcedureName = "RET_ALL_MOVIE_PR" };

            var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                foreach (var row in lstResults)
                {
                    var movie = BuildMovie(row);
                    lstMovies.Add((T)Convert.ChangeType(movie, typeof(T)));
                }
            }
            return lstMovies;
        }

        public override T RetrieveById<T>(int id)
        {
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_MOVIE_BY_ID_PR" };

            sqlOperation.AddIntParam("P_Id", id);

            var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                var row = lstResults[0];
                var movie = BuildMovie(row);
                return (T)Convert.ChangeType(movie, typeof(T));
            }

            return default(T);
        }

        public T RetrieveByTitle<T>(Movie movie)
        {
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_MOVIE_BY_TITLE_PR" };

            sqlOperation.AddStringParameter("P_Title", movie.Title);

            var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                var row = lstResults[0];
                movie = BuildMovie(row);
                return (T)Convert.ChangeType(movie, typeof(T));
            }

            return default(T);
        }

        public override void Update(BaseDTO baseDTO)
        {
            throw new NotImplementedException();
        }

        // Metodo que convierte el diccionario en una película

        private Movie BuildMovie(Dictionary<string, object> row)
        {
            var movie = new Movie()
            {
                Id = (int)row["Id"],
                Created = (DateTime)row["Created"],
                //Updated = (DateTime)row["Updated"],
                Title = (string)row["Title"],
                Description = (string)row["Description"],
                ReleaseDate = (DateTime)row["ReleaseDate"],
                Genre = (string)row["Genre"],
                Director = (string)row["Director"]
            };
            return movie;
        }
    }
}
