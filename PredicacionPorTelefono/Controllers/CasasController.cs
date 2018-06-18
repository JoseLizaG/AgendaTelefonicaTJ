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
    public class CasasController : Controller
    {
        private DB_9EB27B_TJEntities db = new DB_9EB27B_TJEntities();

        // GET: Casas
        public async Task<ActionResult> Index()
        {
            var casas = db.Casas.Include(c => c.Territorio);
            return View(await casas.ToListAsync());
        }

        // GET: Casas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Casa casa = await db.Casas.FindAsync(id);
            //Actualización: Jose Liza - 2018/06/17
            var casas = db.Casas.Where(c => c.TerritorioId==id);
            //
            if (casas == null)
            {
                return HttpNotFound();
            }
            return View("Index", await casas.ToListAsync());
        }

        // GET: Casas/Create
        public ActionResult Create()
        {
            ViewBag.TerritorioId = new SelectList(db.Territorios, "Id", "Territorio1");
            return View();
        }

        // POST: Casas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,TerritorioId,Tipo,Calle,Numero,DepInt,Telefono1,Telefono2,Telefono3,Abonado,Distrito,Observaciones")] Casa casa)
        {
            if (ModelState.IsValid)
            {
                db.Casas.Add(casa);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TerritorioId = new SelectList(db.Territorios, "Id", "Territorio1", casa.TerritorioId);
            return View(casa);
        }

        // GET: Casas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Casa casa = await db.Casas.FindAsync(id);
            if (casa == null)
            {
                return HttpNotFound();
            }
            ViewBag.TerritorioId = new SelectList(db.Territorios, "Id", "Territorio1", casa.TerritorioId);
            return View(casa);
        }

        // POST: Casas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,TerritorioId,Tipo,Calle,Numero,DepInt,Telefono1,Telefono2,Telefono3,Abonado,Distrito,Observaciones")] Casa casa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(casa).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TerritorioId = new SelectList(db.Territorios, "Id", "Territorio1", casa.TerritorioId);
            return View(casa);
        }

        // GET: Casas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Casa casa = await db.Casas.FindAsync(id);
            if (casa == null)
            {
                return HttpNotFound();
            }
            return View(casa);
        }

        // POST: Casas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Casa casa = await db.Casas.FindAsync(id);
            db.Casas.Remove(casa);
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
