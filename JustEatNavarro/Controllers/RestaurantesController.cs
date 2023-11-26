using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JustEatNavarro.Models;

namespace JustEatNavarro.Controllers
{
    public class RestaurantesController : Controller
    {
        private JustEatNavarroEntities db = new JustEatNavarroEntities();

        // GET: Restaurantes
        public ActionResult Index()
        {
            var restaurante = db.Restaurante.Include(r => r.Categoria);
            return View(restaurante.ToList());
        }

        // GET: Restaurantes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurante restaurante = db.Restaurante.Find(id);
            if (restaurante == null)
            {
                return HttpNotFound();
            }
            return View(restaurante);
        }

        // GET: Restaurantes/Create
        public ActionResult Create()
        {
            ViewBag.IdCategoria = new SelectList(db.Categoria, "Id", "Descripcion");
            return View();
        }

        // POST: Restaurantes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CIF,Nombre,IdCategoria,Calle,Numero,CodigoPostal,Localidad,Telefono")] Restaurante restaurante)
        {
            if (ModelState.IsValid)
            {
                db.Restaurante.Add(restaurante);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCategoria = new SelectList(db.Categoria, "Id", "Descripcion", restaurante.IdCategoria);
            return View(restaurante);
        }

        // GET: Restaurantes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurante restaurante = db.Restaurante.Find(id);
            if (restaurante == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCategoria = new SelectList(db.Categoria, "Id", "Descripcion", restaurante.IdCategoria);
            return View(restaurante);
        }

        // POST: Restaurantes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CIF,Nombre,IdCategoria,Calle,Numero,CodigoPostal,Localidad,Telefono")] Restaurante restaurante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCategoria = new SelectList(db.Categoria, "Id", "Descripcion", restaurante.IdCategoria);
            return View(restaurante);
        }

        // GET: Restaurantes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurante restaurante = db.Restaurante.Find(id);
            if (restaurante == null)
            {
                return HttpNotFound();
            }
            return View(restaurante);
        }

        // POST: Restaurantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurante restaurante = db.Restaurante.Find(id);
            db.Restaurante.Remove(restaurante);
            db.SaveChanges();
            return RedirectToAction("Index");
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
