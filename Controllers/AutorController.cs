using Microsoft.AspNetCore.Mvc;
using EjercicioPractica_S1.Models;

namespace EjercicioPractica_S1.Controllers
{
    public class AutorController : Controller
    {
        public IActionResult Index()
        {
            List<Autor> autores = new List<Autor>()
            {
                new Autor
                {
                    ID = 1,
                    Nombre = "Gabriel García Márquez",
                    Nacionalidad = "Colombiano",
                    FechaNacimiento = new DateTime(1927, 3, 6),
                    Activo = false
                },
                new Autor
                {
                    ID = 2,
                    Nombre = "Antoine de Saint-Exupéry",
                    Nacionalidad = "Francés",
                    FechaNacimiento = new DateTime(1900, 6, 29),
                    Activo = false
                },
                new  Autor       
                {
                    ID = 3,
                    Nombre = "George Orwell",
                    Nacionalidad = "Británico",
                    FechaNacimiento = new DateTime(1903, 6, 25),
                    Activo = false
                },
                new Autor
                {
                    ID = 4,
                    Nombre = "Miguel de Cervantes",
                    Nacionalidad = "Español",
                    FechaNacimiento = new DateTime(1547, 9, 29),
                    Activo = false
                },
                new Models.Autor
                {
                    ID = 5,
                    Nombre = "J.K. Rowling",
                    Nacionalidad = "Británica",
                    FechaNacimiento = new DateTime(1965, 7, 31),
                    Activo = true
                },
                new Models.Autor
                {
                    ID = 6,
                    Nombre = "Haruki Murakami",
                    Nacionalidad = "Japonés",
                    FechaNacimiento = new DateTime(1949, 1, 12),
                    Activo = true
                }
            };

            ViewBag.Autor = autores;

            return View();
        }
    }
}
