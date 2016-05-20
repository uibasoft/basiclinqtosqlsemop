using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LinqToSql.Framework.Pepemosca.Data;
using Semop.Aplicacion.Modulos.Core.SubAlcaldias;
using Semop.Aplicacion.Modulos.Core.SubAlcaldias.Dtos;

namespace LinqToSql.Controllers
{
    public class HomeController : Controller
    {
        protected readonly ISubAlcaldiasAppServices SubAlcaldiasApp;
        public HomeController(ISubAlcaldiasAppServices pSubAlcaldiasApp)
        {
            if(pSubAlcaldiasApp == null)
                throw new ArgumentNullException(nameof(pSubAlcaldiasApp));
            SubAlcaldiasApp = pSubAlcaldiasApp;

        }

        // GET: Home
        public ActionResult Index()
        {
            var lista = SubAlcaldiasApp.Listar(string.Empty, string.Empty, 1, 10);
            return View(lista);
        }

        // GET: Home/Crear
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Home/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear([Bind(Include = "Id,Nombre,Direccion,Zona,Telefono, NombreSubAlcalde")] SubAlcaldiaDto subAlcaldia)
        {
            if (!ModelState.IsValid) return View(subAlcaldia);
            try
            {
                var result = SubAlcaldiasApp.GuardarAsignarResponsable(subAlcaldia);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Debe ingresar todos los datos requeridos. Inténtelo de nuevo, y si el problema persiste, consulte con el administrador del sistema.");
                return View(subAlcaldia);
            }
            return RedirectToAction("Index");
        }

        // GET: Home/Eliminar/5
        public ActionResult Eliminar(int? id, bool? exceptionError)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var subAlcaldia = SubAlcaldiasApp.Obtener(id.Value);
            if (subAlcaldia == null)
            {
                if (exceptionError.GetValueOrDefault())
                {
                    return RedirectToAction("Index");
                }
                return HttpNotFound();
            }

            if (exceptionError.GetValueOrDefault())
            {
                ViewBag.ExceptionErrorMessage = "Se ha producido una excepcion controlada. " +
                                                "Pulse nuevamente el botón eliminar o cancele la operacion.";
            }

            return View(subAlcaldia);
        }

        // POST: Home/Eliminar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(SubAlcaldiaDto salcaldia)
        {
            try
            {
                var result = SubAlcaldiasApp.Eliminar(new[] {salcaldia.Id});             
                return RedirectToAction("Index");
            }
            catch (DataException ex)
            {
                return RedirectToAction("Eliminar", new { exceptionError = true, id = salcaldia.Id });
            }
        }

        // GET: Home/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }          
            var subAlcaldiaDto = SubAlcaldiasApp.Obtener(id.Value);
            if (subAlcaldiaDto == null)
            {
                return HttpNotFound();
            }
            return View(subAlcaldiaDto);
        }

        // POST: Home/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int? id, byte[] rowVersion)
        {
            var fieldsToBind = new[] { "Nombre", "Direccion", "Zona", "Telefono", "NombreSubAlcalde" };

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var subAlcaldiaToUpdateDto = SubAlcaldiasApp.Obtener(id.Value);
            if (subAlcaldiaToUpdateDto == null)
            {
                var deletedSubAlcaldia = new SubAlcaldiaDto();
                TryUpdateModel(deletedSubAlcaldia, fieldsToBind);
                ModelState.AddModelError(string.Empty, "No se puede guardar los cambios. El elemento fué eliminado por otro usuario.");
                return View(deletedSubAlcaldia);
            }

            if (!TryUpdateModel(subAlcaldiaToUpdateDto, fieldsToBind)) return View(subAlcaldiaToUpdateDto);

            try
            {
                var result = SubAlcaldiasApp.Editar(subAlcaldiaToUpdateDto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "No se puede guardar los cambios. Inténtelo de nuevo, y si el problema persiste, consulte con el administrador del sistema.");
            }
            return View(subAlcaldiaToUpdateDto);
        }
    }
}