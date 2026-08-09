using Microsoft.AspNetCore.Mvc;
using EjercicioPractica_S1.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace EjercicioPractica_S1.Controllers
{
    public class LibrosController : Controller
    {
        // Se declara la interfaz para manejar las rutas del servidor
        private readonly IWebHostEnvironment _environment;

        // Lista estática simulando la base de datos con su nombre correcto 'libros'
        private static List<Libro> libros = new List<Libro>
        {
            new Libro { ID = 1, Titulo = "Cien Años de Soledad", Autor = "Gabriel García Márquez", Categoria = "Novela", Precio = 19.99m, Disponible = true, ImagenUrl = "/images/cien_anios.jpg" },
            new Libro { ID = 2, Titulo = "El Principito", Autor = "Antoine de Saint-Exupéry", Categoria = "Fábula", Precio = 9.99m, Disponible = true, ImagenUrl = "/images/principito.jpg" },
            new Libro { ID = 3, Titulo = "1984", Autor = "George Orwell", Categoria = "Distopía", Precio = 14.99m, Disponible = true, ImagenUrl = "/images/1984.jpg" },
            new Libro { ID = 4, Titulo = "Don Quijote de la Mancha", Autor = "Miguel de Cervantes", Categoria = "Novela", Precio = 24.99m, Disponible = true, ImagenUrl = "/images/quijote.jpg" }
        };

        // Constructor para inyectar IWebHostEnvironment requerido en la carga de imágenes
        public LibrosController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult Index()
        {
            ViewBag.Libros = libros;
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Libro libro, IFormFile? archivoImagen)
        {
            if (!ModelState.IsValid)
            {
                return View(libro);
            }

            if (archivoImagen != null && archivoImagen.Length > 0)
            {
                string carpetaUploads = Path.Combine(_environment.WebRootPath, "images");
                if (!Directory.Exists(carpetaUploads))
                {
                    Directory.CreateDirectory(carpetaUploads);
                }

                string nombreArchivo = System.Guid.NewGuid().ToString() + Path.GetExtension(archivoImagen.FileName);
                string rutaCompleta = Path.Combine(carpetaUploads, nombreArchivo);

                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    archivoImagen.CopyTo(stream);
                }

                libro.ImagenUrl = "/images/" + nombreArchivo;
            }

            int nuevoId = libros.Any() ? libros.Max(l => l.ID) + 1 : 1;
            libro.ID = nuevoId;
            libros.Add(libro);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var libro = libros.FirstOrDefault(l => l.ID == id);
            if (libro == null)
            {
                return NotFound();
            }
            return View(libro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Libro libro)
        {
            if (!ModelState.IsValid)
            {
                return View(libro);
            }

            var libroExistente = libros.FirstOrDefault(l => l.ID == libro.ID);
            if (libroExistente == null)
            {
                return NotFound();
            }

            libroExistente.Titulo = libro.Titulo;
            libroExistente.Autor = libro.Autor;
            libroExistente.Categoria = libro.Categoria;
            libroExistente.Precio = libro.Precio;
            libroExistente.Disponible = libro.Disponible;

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var libro = libros.FirstOrDefault(l => l.ID == id);
            if (libro == null)
            {
                return NotFound();
            }
            return View(libro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var libro = libros.FirstOrDefault(l => l.ID == id);
            if (libro == null)
            {
                return NotFound();
            }

            libros.Remove(libro);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult ProcesarAccionId(int id, string accion)
        {
            var libro = libros.FirstOrDefault(l => l.ID == id);
            if (libro == null)
            {
                return NotFound();
            }

            if (accion == "Edit")
            {
                return RedirectToAction("Edit", new { id = id });
            }
            else if (accion == "Delete")
            {
                return RedirectToAction("Delete", new { id = id });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

