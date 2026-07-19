using Microsoft.AspNetCore.Mvc;
using EjercicioPractica_S1.Models;

namespace EjercicioPractica_S1.Controllers
{
    public class LibrosController : Controller
    {
        public IActionResult Index()
        {
            List<Libro> libros = new List<Libro>()
            {
                new Libro
                {
                    ID = 1,
                    Titulo = "Cien Años de Soledad",
                    Autor = "Gabriel García Márquez",
                    Categoria = "Novela",
                    Precio = 19.99m,
                    Disponible = true
                },

                new Libro
                {
                    ID = 2,
                    Titulo = "El Principito",
                    Autor = "Antoine de Saint-Exupéry",
                    Categoria = "Fábula",
                    Precio = 9.99m,
                    Disponible = true
                },

                new Libro
                {
                    ID = 3,
                    Titulo = "1984",
                    Autor = "George Orwell",
                    Categoria = "Distopía",
                    Precio = 14.99m,
                    Disponible = false
                }, 

                new Libro
                {
                    ID = 4,
                    Titulo = "Don Quijote de la Mancha",
                    Autor = "Miguel de Cervantes",
                    Categoria = "Novela",
                    Precio = 24.99m,
                    Disponible = true
                }


            };

            ViewBag.Libros = libros;

            return View();
        }
    }
}
