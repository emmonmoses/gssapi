using System.Net;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using gssManRegistration.Models;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;

namespace gssManRegistration.Controllers
{
    public class UserDetailController : ApiController
    {
        private readonly gssManRegistrationDbContext dbContext = new gssManRegistrationDbContext();

        // GET: api/userdetail    
        public IQueryable<userdetail> GetUserDetail()
        {
            return dbContext.userdetail;
        }

        // GET: api/userdetail/5    
        [ResponseType(typeof(userdetail))]
        public IHttpActionResult GetUserDetail(int id)
        {
            userdetail userdetail = dbContext.userdetail.Find(id);
            if (userdetail == null)
            {
                return NotFound();
            }

            return Ok(userdetail);
        }


        // POST: api/userdetail    
        [ResponseType(typeof(userdetail))]
        public IHttpActionResult PostUserDetail(userdetail userdetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dbContext.userdetail.Add(userdetail);
            dbContext.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { userdetail.id }, userdetail);
        }

        // PUT: api/userdetail/5    
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserDetail(int id, userdetail userdetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userdetail.id)
            {
                return BadRequest();
            }

            dbContext.Entry(userdetail).State = EntityState.Modified;

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // return CreatedAtRoute("DefaultApi", new { userdetail.id }, userdetail);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/userdetail/5    
        [ResponseType(typeof(userdetail))]
        public IHttpActionResult DeleteUserDetail(int id)
        {
            userdetail userdetail = dbContext.userdetail.Find(id);
            if (userdetail == null)
            {
                return NotFound();
            }

            dbContext.userdetail.Remove(userdetail);
            dbContext.SaveChanges();

            return Ok();
            //return Ok(userdetail);
            //return StatusCode(HttpStatusCode.OK);
        }
        private bool UserDetailExists(int id)
        {
            return dbContext.userdetail.Count(e => e.id == id) > 0;
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
