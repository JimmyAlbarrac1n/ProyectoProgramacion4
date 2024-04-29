using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

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

        #region Llamadas a la API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Pelicula.GetAll(includeProperties: "Cartelera") });
        }
        #endregion
    }
}
