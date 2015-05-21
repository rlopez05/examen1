using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using examenDesing;

namespace examenDesing.Controllers
{
    public class equiposController : Controller
    {
        private db7c1be70e4bbf4b92a5aaa49d0069f4c0Entities db = new db7c1be70e4bbf4b92a5aaa49d0069f4c0Entities();

        // GET: equipos
        public ActionResult Index()
        {
            return View(db.equipoes.ToList());
        }

        public JsonResult CargarEquipos()
        {
            return Json(db.equipoes.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: equipos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipo equipo = db.equipoes.Find(id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            return View(equipo);
        }

        // GET: equipos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: equipos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public ActionResult Create([Bind(Include = "idEquipo,nombre,fechaCreacion,activo")] equipo equipo)
        {
            if (ModelState.IsValid)
            {
                db.equipoes.Add(equipo);
                db.SaveChanges();
                return Json(equipo, JsonRequestBehavior.AllowGet);
            }

            return Json(equipo,JsonRequestBehavior.AllowGet);
        }

        // GET: equipos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipo equipo = db.equipoes.Find(id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            return View(equipo);
        }

        // POST: equipos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEquipo,nombre,fechaCreacion,activo")] equipo equipo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(equipo);
        }
    [HttpPost]

        public JsonResult update(equipo equipo)
        {
            if(ModelState.IsValid)
            {
                db.Entry(equipo).State = EntityState.Modified;
                db.SaveChanges();
                return Json(equipo, JsonRequestBehavior.AllowGet);
            }


            return Json(equipo, JsonRequestBehavior.AllowGet);
        }

        // GET: equipos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipo equipo = db.equipoes.Find(id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            return View(equipo);
        }

        // POST: equipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            equipo equipo = db.equipoes.Find(id);
            db.equipoes.Remove(equipo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Remove(string id )
        {
            try
            {
                if (id == null)
                    return Json("El registro no es valido.", JsonRequestBehavior.AllowGet);

                var nId = int.Parse(id);
                var equipo = db.equipoes.FirstOrDefault(x => x.idEquipo == nId);

                if (equipo == null)
                    return Json("No se encontro ningun registro.", JsonRequestBehavior.AllowGet);

                db.equipoes.Remove(equipo);
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
