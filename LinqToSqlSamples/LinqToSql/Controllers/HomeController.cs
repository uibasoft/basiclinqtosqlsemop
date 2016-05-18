using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LinqToSql.Framework.Pepemosca.Data;

namespace LinqToSql.Controllers
{
    public class HomeController : Controller
    {
        private readonly AlcaldiaContextDataContext _db = new AlcaldiaContextDataContext();

        public HomeController()
        {

        }
             
        // GET: Home
        public ActionResult Index()
        {

            var subAlcaldias = from su in _db.SubAlcaldias
                               where su.Telefono.Contains("3")
                               select su;
            var lista = subAlcaldias.ToList();

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
        public ActionResult Crear([Bind(Include = "IdSubAlcaldia,Nombre,Direccion,Zona,Telefono, NombreSubAlcalde")] SubAlcaldia subAlcaldia)
        {
            if (!ModelState.IsValid) return View(subAlcaldia);
            try
            {
                _db.SubAlcaldias.InsertOnSubmit(subAlcaldia);
                _db.SubmitChanges();
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

            var subAlcaldia = _db.SubAlcaldias.SingleOrDefault(ele => ele.IdSubAlcaldia == id);
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
        public ActionResult Eliminar(SubAlcaldia salcaldia)
        {
            try
            {
                var entitie = _db.SubAlcaldias.SingleOrDefault(ele => ele.IdSubAlcaldia == salcaldia.IdSubAlcaldia);
                if (entitie != null)
                {
                    _db.SubAlcaldias.DeleteOnSubmit(entitie);
                    _db.SubmitChanges();
                }                
                return RedirectToAction("Index");
            }
            catch (DataException ex)
            {
                return RedirectToAction("Eliminar", new { exceptionError = true, id = salcaldia.IdSubAlcaldia });
            }
        }

        // GET: Home/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var subAlcaldia = _db.SubAlcaldias.SingleOrDefault(ele => ele.IdSubAlcaldia == id);
            if (subAlcaldia == null)
            {
                return HttpNotFound();
            }
            return View(subAlcaldia);
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

            var subAlcaldiaToUpdate = _db.SubAlcaldias.SingleOrDefault(ele => ele.IdSubAlcaldia == id);
            if (subAlcaldiaToUpdate == null)
            {
                var deletedSubAlcaldia = new SubAlcaldia();
                TryUpdateModel(deletedSubAlcaldia, fieldsToBind);
                ModelState.AddModelError(string.Empty, "No se puede guardar los cambios. El elemento fué eliminado por otro usuario.");
                return View(deletedSubAlcaldia);
            }

            if (!TryUpdateModel(subAlcaldiaToUpdate, fieldsToBind)) return View(subAlcaldiaToUpdate);

            try
            {
                _db.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "No se puede guardar los cambios. Inténtelo de nuevo, y si el problema persiste, consulte con el administrador del sistema.");
            }
            return View(subAlcaldiaToUpdate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}