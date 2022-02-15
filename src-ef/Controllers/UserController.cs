using System.Net;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using gssManRegistration.Models;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;

namespace gssManRegistration.Controllers
{
    public class UserController : ApiController
    {
        private readonly gssManRegistrationDbContext dbContext = new gssManRegistrationDbContext();

        // GET: api/user    
        public IQueryable<tbl_user> GetUser()
        {
            return dbContext.tbl_user;
        }

        // GET: api/user/5    
        [ResponseType(typeof(tbl_user))]
        public IHttpActionResult GetUser(int id)
        {
            tbl_user user = dbContext.tbl_user.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        //// POST: api/user    
        [ResponseType(typeof(tbl_user))]
        public IHttpActionResult PostUser(tbl_user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dbContext.tbl_user.Add(user);
            dbContext.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { user.id }, user);
        }

        // PUT: api/user/5    
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, tbl_user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.id)
            {
                return BadRequest();
            }

            dbContext.Entry(user).State = EntityState.Modified;

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // return CreatedAtRoute("DefaultApi", new { user.id }, user);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/user/5    
        [ResponseType(typeof(tbl_user))]
        public IHttpActionResult DeleteUser(int id)
        {
            tbl_user user = dbContext.tbl_user.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            dbContext.tbl_user.Remove(user);
            dbContext.SaveChanges();

            return Ok();
            //return Ok(user);
            //return StatusCode(HttpStatusCode.OK);
        }
        
        private bool UserExists(int id)
        {
            return dbContext.tbl_user.Count(e => e.id == id) > 0;
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
