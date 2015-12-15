using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using practicaAngularjs.Models;

namespace practicaAngularjs.Controllers
{
    public class VehiculosController : ApiController
    {
        private ConsecionarioEntities db = new ConsecionarioEntities();

        // GET api/Vehiculos
        public IQueryable<vehiculos> Getvehiculos()
        {
            return db.vehiculos;
        }

        // GET api/Vehiculos/5
        [ResponseType(typeof(vehiculos))]
        public async Task<IHttpActionResult> Getvehiculos(long id)
        {
            vehiculos vehiculos = await db.vehiculos.FindAsync(id);
            if (vehiculos == null)
            {
                return NotFound();
            }

            return Ok(vehiculos);
        }

        // PUT api/Vehiculos/5
        public async Task<IHttpActionResult> Putvehiculos(long id, vehiculos vehiculos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehiculos.idvehiculos)
            {
                return BadRequest();
            }

            db.Entry(vehiculos).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!vehiculosExists(id))
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

        // POST api/Vehiculos
        [ResponseType(typeof(vehiculos))]
        public async Task<IHttpActionResult> Postvehiculos(vehiculos vehiculos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.vehiculos.Add(vehiculos);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = vehiculos.idvehiculos }, vehiculos);
        }

        // DELETE api/Vehiculos/5
        [ResponseType(typeof(vehiculos))]
        public async Task<IHttpActionResult> Deletevehiculos(long id)
        {
            vehiculos vehiculos = await db.vehiculos.FindAsync(id);
            if (vehiculos == null)
            {
                return NotFound();
            }

            db.vehiculos.Remove(vehiculos);
            await db.SaveChangesAsync();

            return Ok(vehiculos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool vehiculosExists(long id)
        {
            return db.vehiculos.Count(e => e.idvehiculos == id) > 0;
        }
    }
}