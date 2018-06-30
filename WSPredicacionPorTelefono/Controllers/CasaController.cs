using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WSPredicacionPorTelefono;

namespace WSPredicacionPorTelefono.Controllers
{
    public class CasaController : ApiController
    {
        private DB_9EB27B_TJEntities db = new DB_9EB27B_TJEntities();

        // GET: api/Casa
        public IQueryable<Casa> GetCasas()
        {
            return db.Casas;
        }

        // GET: api/Casa/5
        [ResponseType(typeof(Casa))]
        public IHttpActionResult GetCasa(int id)
        {
            Casa casa = db.Casas.Find(id);
            if (casa == null)
            {
                return NotFound();
            }

            return Ok(casa);
        }

        // PUT: api/Casa/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCasa(int id, Casa casa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != casa.Id)
            {
                return BadRequest();
            }

            db.Entry(casa).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CasaExists(id))
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

        // POST: api/Casa
        [ResponseType(typeof(Casa))]
        public IHttpActionResult PostCasa(Casa casa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Casas.Add(casa);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = casa.Id }, casa);
        }

        // DELETE: api/Casa/5
        [ResponseType(typeof(Casa))]
        public IHttpActionResult DeleteCasa(int id)
        {
            Casa casa = db.Casas.Find(id);
            if (casa == null)
            {
                return NotFound();
            }

            db.Casas.Remove(casa);
            db.SaveChanges();

            return Ok(casa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CasaExists(int id)
        {
            return db.Casas.Count(e => e.Id == id) > 0;
        }
    }
}