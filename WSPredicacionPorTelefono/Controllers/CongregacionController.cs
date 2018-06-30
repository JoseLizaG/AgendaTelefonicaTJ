using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace WSPredicacionPorTelefono.Controllers
{
    public class CongregacionController : ApiController
    {
        private DB_9EB27B_TJEntities db = new DB_9EB27B_TJEntities();

        // GET: api/Congragacions
        public IQueryable<Congragacion> GetCongragacions()
        {
            List<Congragacion> lista = db.Congragacions.ToList();
            return lista.AsQueryable();
        }

        // GET: api/Congragacions/5
        [ResponseType(typeof(Congragacion))]
        public IHttpActionResult GetCongragacion(int id)
        {
            Congragacion congragacion = db.Congragacions.Find(id);
            if (congragacion == null)
            {
                return NotFound();
            }

            return Ok(congragacion);
        }

        // PUT: api/Congragacions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCongragacion(int id, Congragacion congragacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != congragacion.Id)
            {
                return BadRequest();
            }

            db.Entry(congragacion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CongragacionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Congragacions
        [ResponseType(typeof(Congragacion))]
        public IHttpActionResult PostCongragacion(Congragacion congragacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Congragacions.Add(congragacion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = congragacion.Id }, congragacion);
        }

        // DELETE: api/Congragacions/5
        [ResponseType(typeof(Congragacion))]
        public IHttpActionResult DeleteCongragacion(int id)
        {
            Congragacion congragacion = db.Congragacions.Find(id);
            if (congragacion == null)
            {
                return NotFound();
            }

            db.Congragacions.Remove(congragacion);
            db.SaveChanges();

            return Ok(congragacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CongragacionExists(int id)
        {
            return db.Congragacions.Count(e => e.Id == id) > 0;
        }
    }
}