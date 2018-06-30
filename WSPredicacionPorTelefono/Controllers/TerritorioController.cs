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
    public class TerritorioController : ApiController
    {
        private DB_9EB27B_TJEntities db = new DB_9EB27B_TJEntities();

        // GET: api/Territorios
        public IQueryable<Territorio> GetTerritorios()
        {
            return db.Territorios;
        }

        // GET: api/Territorios/5
        [ResponseType(typeof(Territorio))]
        public IHttpActionResult GetTerritorio(int id)
        {
            Territorio territorio = db.Territorios.Find(id);
            if (territorio == null)
            {
                return NotFound();
            }

            return Ok(territorio);
        }

        // PUT: api/Territorios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTerritorio(int id, Territorio territorio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != territorio.Id)
            {
                return BadRequest();
            }

            db.Entry(territorio).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TerritorioExists(id))
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

        // POST: api/Territorios
        [ResponseType(typeof(Territorio))]
        public IHttpActionResult PostTerritorio(Territorio territorio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Territorios.Add(territorio);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = territorio.Id }, territorio);
        }

        // DELETE: api/Territorios/5
        [ResponseType(typeof(Territorio))]
        public IHttpActionResult DeleteTerritorio(int id)
        {
            Territorio territorio = db.Territorios.Find(id);
            if (territorio == null)
            {
                return NotFound();
            }

            db.Territorios.Remove(territorio);
            db.SaveChanges();

            return Ok(territorio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TerritorioExists(int id)
        {
            return db.Territorios.Count(e => e.Id == id) > 0;
        }
    }
}