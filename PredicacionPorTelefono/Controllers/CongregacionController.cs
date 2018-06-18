using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PredicacionPorTelefono.Models;

namespace PredicacionPorTelefono.Controllers
{
    public class CongregacionController : Controller
    {
        private DB_9EB27B_TJEntities db = new DB_9EB27B_TJEntities();

        // GET: Congragacions
        public async Task<ActionResult> Index()
        {
            return View(await db.Congragacions.ToListAsync());
        }

        // GET: Congragacions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Congragacion congragacion = await db.Congragacions.FindAsync(id);
            if (congragacion == null)
            {
                return HttpNotFound();
            }
            return View(congragacion);
        }

        // GET: Congragacions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Congragacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nombre,Usuario,Password,Direccion,Circuito")] Congragacion congragacion)
        {
            if (ModelState.IsValid)
            {
                db.Congragacions.Add(congragacion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(congragacion);
        }

        // GET: Congragacions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Congragacion congragacion = await db.Congragacions.FindAsync(id);
            if (congragacion == null)
            {
                return HttpNotFound();
            }
            return View(congragacion);
        }

        // POST: Congragacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nombre,Usuario,Password,Direccion,Circuito")] Congragacion congragacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(congragacion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(congragacion);
        }

        // GET: Congragacions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Congragacion congragacion = await db.Congragacions.FindAsync(id);
            if (congragacion == null)
            {
                return HttpNotFound();
            }
            return View(congragacion);
        }

        // POST: Congragacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Congragacion congragacion = await db.Congragacions.FindAsync(id);
            db.Congragacions.Remove(congragacion);
            await db.SaveChangesAsync();
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
