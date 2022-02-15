using System.Net;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using gssManRegistration.Models;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;

namespace gssManRegistration.Controllers
{
    public class StudentExDetailController : ApiController
    {
        private readonly gssManRegistrationDbContext dbContext = new gssManRegistrationDbContext();

        // GET: api/studentexdetail    
        public IQueryable<tbl_studentexdetail> GetStudentExDetail()
        {
            return dbContext.tbl_studentexdetail;
        }

        // GET: api/studentexdetail/5    
        [ResponseType(typeof(tbl_studentexdetail))]
        public IHttpActionResult GetStudentExDetail(int id)
        {
            tbl_studentexdetail student = dbContext.tbl_studentexdetail.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }


        // POST: api/studentexdetail    
        [ResponseType(typeof(tbl_studentexdetail))]
        public IHttpActionResult PostStudentExDetail(tbl_studentexdetail student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dbContext.tbl_studentexdetail.Add(student);
            dbContext.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { student.id }, student);
        }

        // PUT: api/studentexdetail/5    
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudentExDetail(int id, tbl_studentexdetail student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.id)
            {
                return BadRequest();
            }

            dbContext.Entry(student).State = EntityState.Modified;

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // return CreatedAtRoute("DefaultApi", new { student.id }, student);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/studentexdetail/5    
        [ResponseType(typeof(tbl_studentexdetail))]
        public IHttpActionResult DeleteStudentExDetail(int id)
        {
            tbl_studentexdetail student = dbContext.tbl_studentexdetail.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            dbContext.tbl_studentexdetail.Remove(student);
            dbContext.SaveChanges();

            return Ok();
            //return Ok(student);
            //return StatusCode(HttpStatusCode.OK);
        }
        private bool StudentExDetailExists(int id)
        {
            return dbContext.tbl_studentexdetail.Count(e => e.id == id) > 0;
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
