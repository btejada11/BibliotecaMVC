using Microsoft.AspNetCore.Mvc;
using EjercicioPractica_S1.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace EjercicioPractica_S1.Controllers
{
    public class AutorController : Controller
    {
        
        private static List<Autor> autores = new List<Autor>()
        {
            new Autor { ID = 1, Nombre = "Gabriel García Márquez", Nacionalidad = "Colombiano", FechaNacimiento = new DateTime(1927, 3, 6), Activo = false },
            new Autor { ID = 2, Nombre = "Antoine de Saint-Exupéry", Nacionalidad = "Francés", FechaNacimiento = new DateTime(1900, 6, 29), Activo = false },
            new Autor { ID = 3, Nombre = "George Orwell", Nacionalidad = "Británico", FechaNacimiento = new DateTime(1903, 6, 25), Activo = false },
            new Autor { ID = 4, Nombre = "Miguel de Cervantes", Nacionalidad = "Español", FechaNacimiento = new DateTime(1547, 9, 29), Activo = false },
            new Autor { ID = 5, Nombre = "J.K. Rowling", Nacionalidad = "Británica", FechaNacimiento = new DateTime(1965, 7, 31), Activo = true },
            new Autor { ID = 6, Nombre = "Haruki Murakami", Nacionalidad = "Japonés", FechaNacimiento = new DateTime(1949, 1, 12), Activo = true }
        };

        // ACCIÓN MANDATORIA QUE FALTABA: Carga la vista principal de Autores
        public IActionResult Index()
        {
            // Enviamos la lista a la vista mediante ViewBag o directamente como modelo
            ViewBag.Autores = autores;
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

            autor.ID = autores.Any() ? autores.Max(a => a.ID) + 1 : 1;
            autores.Add(autor);

            return RedirectToAction(nameof(Index));
        }

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

        [HttpGet]
        public IActionResult Delete(int id)
        {
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
            var autor = autores.FirstOrDefault(a => a.ID == id);
            if (autor != null)
            {
                autores.Remove(autor);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult ProcesarAccionId(int id, string accion)
        {
      
            bool existeAutor = autores.Any(a => a.ID == id);
            if (!existeAutor)
            {
                TempData["ErrorId"] = "El ID del autor ingresado no existe.";
                return RedirectToAction("Index");
            }

         
            if (accion == "Edit")
            {
                return RedirectToAction("Edit", new { id = id });
            }
            else if (accion == "Details")
            {
                return RedirectToAction("Details", new { id = id });
            }
            else if (accion == "Delete")
            {
                return RedirectToAction("Delete", new { id = id });
            }

            return RedirectToAction("Index");
        }
    }
}
