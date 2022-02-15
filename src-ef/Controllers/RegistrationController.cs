using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using gssManRegistration.Models;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;

namespace gssManRegistration.Controllers
{
    public class RegistrationController : ApiController
    {
        private readonly gssManRegistrationDbContext dbContext = new gssManRegistrationDbContext();

        // GET: api/registration    
        public IQueryable<registrations> GetRegistration()
        {
            return dbContext.registrations;
        }

        // GET: api/registration/5    
        [ResponseType(typeof(registrations))]
        public IHttpActionResult GetRegistration(int id)
        {
            registrations reg = dbContext.registrations.Find(id);
            if (reg == null)
            {
                return NotFound();
            }

            return Ok(reg);
        }


        // POST: api/registration    
        [ResponseType(typeof(registrations))]
        public IHttpActionResult PostRegistration(registrations reg)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dbContext.registrations.Add(reg);
            dbContext.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { reg.id }, reg);
        }

        // PUT: api/registration/5    
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRegistration(int id, registrations reg)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reg.id)
            {
                return BadRequest();
            }

            dbContext.Entry(reg).State = EntityState.Modified;

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentRegistrationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { reg.id }, reg);
        }

        // DELETE: api/student/5    
        [ResponseType(typeof(registrations))]
        public IHttpActionResult DeleteRegistration(int id)
        {
            registrations reg = dbContext.registrations.Find(id);
            if (reg == null)
            {
                return NotFound();
            }

            dbContext.registrations.Remove(reg);
            dbContext.SaveChanges();

            return Ok(reg);
        }
        private bool StudentRegistrationExists(int id)
        {
            return dbContext.registrations.Count(e => e.id == id) > 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
