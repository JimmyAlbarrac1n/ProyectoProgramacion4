﻿using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PeliculasController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PeliculasController(IContenedorTrabajo contenedorTrabajo, 
            IWebHostEnvironment hostingEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            PeliculaVM peliVM = new PeliculaVM()
            {
                Pelicula = new BlogCore.Models.Pelicula(),
                ListaCarteleras = _contenedorTrabajo.Cartelera.GetListaCarteleras()
            };
            return View(peliVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PeliculaVM peliVM)
        {
            if(ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                if(peliVM.Pelicula.Id == 0 && archivos.Count()>0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\peliculas");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    peliVM.Pelicula.UrlImagen= @"\imagenes\peliculas\"+ nombreArchivo+extension;
                    

                    _contenedorTrabajo.Pelicula.Add(peliVM.Pelicula);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Imagen", "Debes seleccionar una imágen");
                }
            }
            peliVM.ListaCarteleras = _contenedorTrabajo.Cartelera.GetListaCarteleras();
            return View(peliVM);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            PeliculaVM peliVM = new PeliculaVM()
            {
                Pelicula = new BlogCore.Models.Pelicula(),
                ListaCarteleras = _contenedorTrabajo.Cartelera.GetListaCarteleras()
            };
            if (id != null)
            {
                peliVM.Pelicula = _contenedorTrabajo.Pelicula.Get(id.GetValueOrDefault());
            }
            return View(peliVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PeliculaVM peliVM)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var peliculaDesdeBd = _contenedorTrabajo.Pelicula.Get(peliVM.Pelicula.Id);
                if ( archivos.Count() > 0)
                {
                    //Nueva imagen para la pelicula
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\peliculas");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var nuevaExtension = Path.GetExtension(archivos[0].FileName);
                    var rutaImagen = Path.Combine(rutaPrincipal, peliculaDesdeBd.UrlImagen.TrimStart('\\'));

                    if(System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }
                    //Nuevamente subimos el archivo
                    
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    peliVM.Pelicula.UrlImagen = @"\imagenes\peliculas\" + nombreArchivo + extension;


                    _contenedorTrabajo.Pelicula.Update(peliVM.Pelicula);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    peliVM.Pelicula.UrlImagen = peliculaDesdeBd.UrlImagen;
                }
                _contenedorTrabajo.Pelicula.Update(peliVM.Pelicula);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            peliVM.ListaCarteleras = _contenedorTrabajo.Cartelera.GetListaCarteleras();
            return View(peliVM);
        }




        #region Llamadas a la API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Pelicula.GetAll(includeProperties: "Cartelera") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var peliculaDesdeBd = _contenedorTrabajo.Pelicula.Get(id);
            string rutaDirectorioPrincipal = _hostingEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, peliculaDesdeBd.UrlImagen.TrimStart('\\'));

            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }

            if (peliculaDesdeBd == null)
            {
                return Json(new { success = false, message = "Error al borrar pelicula" });
            }
            _contenedorTrabajo.Pelicula.Remove(peliculaDesdeBd);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Pelicula borrada correctamente" });
        }
        #endregion
    }
}
