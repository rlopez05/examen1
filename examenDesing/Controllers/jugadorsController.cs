using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using examenDesing;

 
namespace examenDesing.Controllers
{
    
    public class jugadorsController : Controller
    {
        [ScriptIgnore]
        private db7c1be70e4bbf4b92a5aaa49d0069f4c0Entities db = new db7c1be70e4bbf4b92a5aaa49d0069f4c0Entities();
        
        // GET: jugadors
        public ActionResult Index()
        {
            var jugadors = db.jugadors.Include(j => j.equipo);
            return View(jugadors.ToList());
        }
        [HttpGet]
        
        public JsonResult Cargar()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            var jugadors = db.jugadors.Include(j => j.equipo.nombre).Include(j => j.equipo.nombre);
            return Json(db.jugadors.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: jugadors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jugador jugador = db.jugadors.Find(id);
            if (jugador == null)
            {
                return HttpNotFound();
            }
            return View(jugador);
        }

        // GET: jugadors/Create
        public ActionResult Create()
        {
            ViewBag.idEquipo = new SelectList(db.equipoes, "idEquipo", "nombre");
            return View();
        }

        // POST: jugadors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
    
        public ActionResult Create([Bind(Include = "idJugador,nombre,activo,idEquipo")] jugador jugador)
        {
            if (ModelState.IsValid)
            {
                db.jugadors.Add(jugador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idEquipo = new SelectList(db.equipoes, "idEquipo", "nombre", jugador.idEquipo);
            return View(jugador);
        }

        // GET: jugadors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jugador jugador = db.jugadors.Find(id);
            if (jugador == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEquipo = new SelectList(db.equipoes, "idEquipo", "nombre", jugador.idEquipo);
            return View(jugador);
        }

        // POST: jugadors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idJugador,nombre,activo,idEquipo")] jugador jugador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jugador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEquipo = new SelectList(db.equipoes, "idEquipo", "nombre", jugador.idEquipo);
            return View(jugador);
        }

        [HttpPost]

        public JsonResult update(jugador jugador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jugador).State = EntityState.Modified;
                db.SaveChanges();
                return Json(jugador, JsonRequestBehavior.AllowGet);
            }


            return Json(jugador, JsonRequestBehavior.AllowGet);
        }

        // GET: jugadors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jugador jugador = db.jugadors.Find(id);
            if (jugador == null)
            {
                return HttpNotFound();
            }
            return View(jugador);
        }

        // POST: jugadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            jugador jugador = db.jugadors.Find(id);
            db.jugadors.Remove(jugador);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult Remove(string id)
        {
            try
            {
                if (id == null)
                    return Json("El registro no es valido.", JsonRequestBehavior.AllowGet);

                var nId = int.Parse(id);
                var jugador = db.jugadors.FirstOrDefault(x => x.idJugador == nId);

                if (jugador == null)
                    return Json("No se encontro ningun registro.", JsonRequestBehavior.AllowGet);

                db.jugadors.Remove(jugador);
                db.SaveChanges();

                return Json("Eliminado correctarmente", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Error:" + ex.Message, JsonRequestBehavior.AllowGet);
            }


        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
