using Microsoft.AspNetCore.Mvc;
using EjercicioPractica_S1.Models;

namespace EjercicioPractica_S1.Controllers
{
    public class AutorController : Controller
    {
        private static List<Autor> autores = new List<Autor>()
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
            new Autor
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
            new Autor
            {
                ID = 5,
                Nombre = "J.K. Rowling",
                Nacionalidad = "Británica",
                FechaNacimiento = new DateTime(1965, 7, 31),
                Activo = true
            },
            new Autor
            {
                ID = 6,
                Nombre = "Haruki Murakami",
                Nacionalidad = "Japonés",
                FechaNacimiento = new DateTime(1949, 1, 12),
                Activo = true
            }
        };

        public IActionResult Index()
        {
            return View(autores);
        }

        public IActionResult Details(int id)
        {
            var autor = autores.FirstOrDefault(a => a.ID == id);
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);


        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Autor autor)
        {
            if (!ModelState.IsValid)
            {
                return View(autor);
            }

            if (autores.Any())
            {
                autor.ID = autores.Max(a => a.ID) + 1;
            }
            else
            {
                autor.ID = 1;
            }

            autores.Add(autor);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Muestra el formulario de edición para un autor específico.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(int id)
        {
            var autor = autores.FirstOrDefault(a => a.ID == id);

            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Autor autorEditado)
        {
            if (!ModelState.IsValid)
            {
                return View(autorEditado);
            }

            var autor = autores.FirstOrDefault(a => a.ID == autorEditado.ID);

            if (autor == null)
            {
                return NotFound();
            }

            autor.Nombre = autorEditado.Nombre;
            autor.Nacionalidad = autorEditado.Nacionalidad;
            autor.FechaNacimiento = autorEditado.FechaNacimiento;
            autor.Activo = autorEditado.Activo;

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Muestra el formulario de confirmación de eliminación para un autor específico.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Delete(int id)
        {
            // Buscamos el autor en la lista estática por su ID
            var autor = autores.FirstOrDefault(a => a.ID == id);

            if (autor == null)
            {
                return NotFound(); 
            }

            return View(autor);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Buscamos el registro en la lista
            var autor = autores.FirstOrDefault(a => a.ID == id);

            if (autor != null)
            {
                // Eliminamos el objeto de la lista estática
                autores.Remove(autor);
            }

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public IActionResult ProcesarAccionId(int id, string accion)
        {
            // Evaluamos el botón que presionó el usuario
            if (accion == "Edit")
            {
                return RedirectToAction("Edit", new { id = id });
            }
            else if (accion == "Delete")
            {
                return RedirectToAction("Delete", new { id = id });
            }

            // Si por alguna razón no coincide, regresa al listado
            return RedirectToAction("Index");
        }


    }
}