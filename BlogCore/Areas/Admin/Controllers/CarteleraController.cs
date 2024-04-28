using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    //Usamos el contenedor de trabajo para quedar más limpio los controladores
    public class CarteleraController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public CarteleraController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Carteleras cartelera)
        {
            if(ModelState.IsValid)
            {
                _contenedorTrabajo.Cartelera.Add(cartelera);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(cartelera);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Carteleras cartelera = new Carteleras();
            cartelera = _contenedorTrabajo.Cartelera.Get(id);
            if(cartelera == null)
            {
                return NotFound();
            }
            return View(cartelera);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Carteleras cartelera)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Cartelera.Update(cartelera);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(cartelera);
        }

        //Delimitamos
        #region Llamadas a la API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Cartelera.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _contenedorTrabajo.Cartelera.Get(id);
            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Error al borrar categoría" });
            }
            _contenedorTrabajo.Cartelera.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Clasificación borrada correctamente" });
        }


        #endregion
    }
}
