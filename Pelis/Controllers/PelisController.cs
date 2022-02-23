using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pelis.Models;
using Pelis.Models.Request;
using Pelis.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pelis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PelisController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();


            try
            {
                using (pelisContext db = new pelisContext())
                {
                    var lst = db.Movies.ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = lst;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;

            }
            return Ok(oRespuesta);
        }


        [HttpPost]
        public IActionResult Add(MoviesRequest Models)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (pelisContext db = new pelisContext())
                {
                    Movie Movie = new Movie();
                    Movie.Titulo = Models.Titulo;
                    Movie.Director = Models.Director;
                    Movie.Genero = Models.Genero;
                    Movie.Puntuacion = Models.Puntuacion;
                    Movie.Rating = Models.Rating;
                    NewMethod(Models, Movie);
                    db.Movies.Add(Movie);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        private static void NewMethod(MoviesRequest Models, Movie Movie)
        {
            Movie.FechaDePublicacion = Models.FechaDePublicacion;
        }

        //METODO PARA EDITAR
        [HttpPut]
        public IActionResult Editar(MoviesRequest Models)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (pelisContext db = new pelisContext())
                {

                    //ID para modificar los datos
                    Movie Movie = db.Movies.Find(Models.Id);
                    Movie.Titulo = Models.Titulo;
                    Movie.Director = Models.Director;
                    Movie.Genero = Models.Genero;
                    Movie.Puntuacion = Models.Puntuacion;
                    Movie.Rating = Models.Rating;
                    Movie.FechaDePublicacion = Models.FechaDePublicacion;
                    //Indica que se modifico
                    db.Entry(Movie).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        //METODO PARA ELIMINAR EL ID
        [HttpDelete("{Id}")]
        public IActionResult Eliminar(int Id)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (pelisContext db = new pelisContext())
                {

                    //para eliminar una pelicula con el ID
                    Movie Opeli = db.Movies.Find(Id);

                    //
                    //elimina los datos en el Registro
                    db.Remove(Opeli);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
    }
}
