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
    public class TerritoriosController : Controller
    {
        private DB_9EB27B_TJEntities db = new DB_9EB27B_TJEntities();

        // GET: Territorios
        public async Task<ActionResult> Index()
        {
            var territorios = db.Territorios.Include(t => t.Congragacion);
            return View(await territorios.ToListAsync());
        }

        // GET: Territorios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Territorio territorio = await db.Territorios.FindAsync(id);
            //Actualización: Jose Liza - 2018/06/17
            var territorios = db.Territorios.Where(c => c.CongregacionId == id);
            //
            if (territorios == null)
            {
                return HttpNotFound();
            }
            return View("Index", await territorios.ToListAsync());
        }

        // GET: Territorios/Create
        public ActionResult Create()
        {
            ViewBag.CongregacionId = new SelectList(db.Congragacions, "Id", "Nombre");
            return View();
        }

        // POST: Territorios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CongregacionId,Territorio1,Mapa,CoordenadaX,CoordenadaY")] Territorio territorio)
        {
            if (ModelState.IsValid)
            {
                db.Territorios.Add(territorio);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CongregacionId = new SelectList(db.Congragacions, "Id", "Nombre", territorio.CongregacionId);
            return View(territorio);
        }

        // GET: Territorios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Territorio territorio = await db.Territorios.FindAsync(id);
            if (territorio == null)
            {
                return HttpNotFound();
            }
            ViewBag.CongregacionId = new SelectList(db.Congragacions, "Id", "Nombre", territorio.CongregacionId);
            return View(territorio);
        }

        // POST: Territorios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CongregacionId,Territorio1,Mapa,CoordenadaX,CoordenadaY")] Territorio territorio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(territorio).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CongregacionId = new SelectList(db.Congragacions, "Id", "Nombre", territorio.CongregacionId);
            return View(territorio);
        }

        // GET: Territorios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Territorio territorio = await db.Territorios.FindAsync(id);
            if (territorio == null)
            {
                return HttpNotFound();
            }
            return View(territorio);
        }

        // POST: Territorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Territorio territorio = await db.Territorios.FindAsync(id);
            db.Territorios.Remove(territorio);
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
