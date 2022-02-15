using System.Net;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using gssManRegistration.Models;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;

namespace gssManRegistration.Controllers
{
    public class StudentDetailController : ApiController
    {
        private readonly gssManRegistrationDbContext dbContext = new gssManRegistrationDbContext();

        // GET: api/studentdetail    
        public IQueryable<tbl_studentdetail> GetStudentDetail()
        {
            return dbContext.tbl_studentdetail;
        }

        // GET: api/studentdetail/5    
        [ResponseType(typeof(tbl_studentdetail))]
        public IHttpActionResult GetStudentDetail(int id)
        {
            tbl_studentdetail student = dbContext.tbl_studentdetail.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }


        // POST: api/studentdetail    
        [ResponseType(typeof(tbl_studentdetail))]
        public IHttpActionResult PostStudentDetail(tbl_studentdetail student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dbContext.tbl_studentdetail.Add(student);
            dbContext.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { student.id }, student);
        }

        // PUT: api/studentdetail/5    
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudentDetail(int id, tbl_studentdetail student)
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
                if (!StudentDetailExists(id))
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

        // DELETE: api/studentdetail/5    
        [ResponseType(typeof(tbl_studentdetail))]
        public IHttpActionResult DeleteStudentDetail(int id)
        {
            tbl_studentdetail student = dbContext.tbl_studentdetail.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            dbContext.tbl_studentdetail.Remove(student);
            dbContext.SaveChanges();

            return Ok();
            //return Ok(student);
            //return StatusCode(HttpStatusCode.OK);
        }
        private bool StudentDetailExists(int id)
        {
            return dbContext.tbl_studentdetail.Count(e => e.id == id) > 0;
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
