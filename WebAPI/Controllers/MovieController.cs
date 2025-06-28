﻿using CoreApp;
using DataAccess.CRUD;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace WebAPI.Controllers
{
    // Indicamos que la direccion de este controlador será https://servidor:puerto/api/User
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(Movie movie)
        {
            try
            {
                var mm = new MovieManager();
                mm.Create(movie);
                return Ok(movie);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetrieveAll")]
        public ActionResult RetrieveAll()
        {
            try
            {
                var mm = new MovieManager();
                var lstResult = mm.RetrieveAll();
                return Ok(lstResult);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetrieveById")]
        public ActionResult RetrieveById(int id)
        {
            try
            {
                var mm = new MovieManager();
                var result = mm.RetrieveById(id);
                if (result == null)
                    return NotFound($"Película con ID {id} no encontrada.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetrieveByTitle")]
        public ActionResult RetrieveByTitle(string title)
        {
            try
            {
                var mm = new MovieManager();
                var movie = mm.RetrieveByTitle(title);
                if (movie == null)
                    return NotFound($"No se encontró la película con título '{title}'.");
                return Ok(movie);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(Movie movie)
        {
            try
            {
                var mm = new MovieManager();
                var updated = mm.Update(movie);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(Movie movie)
        {
            try
            {
                var mm = new MovieManager();
                var result = mm.RetrieveById(movie.Id);
                mm.Delete(movie.Id);
                return Ok(new { Message = $"Película con ID {movie.Id} eliminada con éxito." });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
