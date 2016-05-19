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

namespace LinqToSql.Controllers
{
    public class HomeController : Controller
    {
        private readonly AlcaldiaContextDataContext _db = new AlcaldiaContextDataContext();
        protected readonly ISubAlcaldiasAppServices SubAlcaldiasApp;
        public HomeController(ISubAlcaldiasAppServices pSubAlcaldiasApp)
        {

            if(pSubAlcaldiasApp == null)
                throw new ArgumentNullException(nameof(pSubAlcaldiasApp));
            SubAlcaldiasApp = pSubAlcaldiasApp;

        }

        //public void LinqToSqlSamples()
        //{

        //    // LISTADO
        //    var listado = from su in _db.SubAlcaldias
        //                  select su;
        //    var subalcaldias = listado.ToList();

        //    // NUEVO
        //    var subAlcaldia = new SubAlcaldia()
        //    {
        //        Nombre = "Sub Alcaldia DM-05",
        //        Direccion = "Av. Beni",
        //        Telefono = "33557618",
        //        Zona = "Norte"
        //    };
        //    _db.SubAlcaldias.InsertOnSubmit(subAlcaldia);
        //    _db.SubmitChanges();

        //    // MODIFICAR
        //    var id = 1;
        //    var newDireccion = "Av. Mutualista";
        //    var newTelefono = "73112828";
        //    var subAlcaldiaToUpdate = _db.SubAlcaldias
        //                                 .SingleOrDefault(ele => ele.IdSubAlcaldia == id);
        //    if (subAlcaldiaToUpdate == null) { /* Mensaje */ }
        //    subAlcaldiaToUpdate.Direccion = newDireccion;
        //    subAlcaldiaToUpdate.Telefono = newTelefono;
        //    _db.SubmitChanges();
                    

        //    // ELIMINAR
        //    var dto = new SubAlcaldia()
        //    {
        //        IdSubAlcaldia = 1
        //    };
        //    var entitie = _db.SubAlcaldias
        //                     .SingleOrDefault(ele => 
        //                     ele.IdSubAlcaldia == dto.IdSubAlcaldia);
        //    if (entitie != null)
        //    {
        //        _db.SubAlcaldias.DeleteOnSubmit(entitie);
        //        _db.SubmitChanges();
        //    }

        //}

        // GET: Home
        public ActionResult Index()
        {

            //var subAlcaldias = from su in _db.SubAlcaldias
            //                   select su;
            //var lista = subAlcaldias.ToList();

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
            {
                _db.Dispose();               
            }
           
            base.Dispose(disposing);
        }
    }
}