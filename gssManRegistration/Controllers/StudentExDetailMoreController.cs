using System.Net;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using gssManRegistration.Models;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;

namespace gssManRegistration.Controllers
{
    public class StudentExDetailMoreController : ApiController
    {
        private readonly gssManRegistrationDbContext dbContext = new gssManRegistrationDbContext();

        // GET: api/studentexdetailmore   
        public IQueryable<tbl_studentexdetailmore> GetStudentExDetailMore()
        {
            return dbContext.tbl_studentexdetailmore;
        }

        // GET: api/studentexdetailmore/5    
        [ResponseType(typeof(tbl_studentexdetailmore))]
        public IHttpActionResult GetStudentExDetailMore(int id)
        {
            tbl_studentexdetailmore student = dbContext.tbl_studentexdetailmore.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }


        // POST: api/studentexdetailmore    
        [ResponseType(typeof(tbl_studentexdetailmore))]
        public IHttpActionResult PostStudentExDetailMore(tbl_studentexdetailmore student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dbContext.tbl_studentexdetailmore.Add(student);
            dbContext.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { student.id }, student);
        }

        // PUT: api/studentexdetailmore/5    
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudentExDetailMore(int id, tbl_studentexdetailmore student)
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
                if (!StudentExDetailMoreExists(id))
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

        // DELETE: api/studentexdetailmore/5    
        [ResponseType(typeof(tbl_studentexdetailmore))]
        public IHttpActionResult DeleteStudentExDetailMore(int id)
        {
            tbl_studentexdetailmore student = dbContext.tbl_studentexdetailmore.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            dbContext.tbl_studentexdetailmore.Remove(student);
            dbContext.SaveChanges();

            return Ok();
            //return Ok(student);
            //return StatusCode(HttpStatusCode.OK);
        }
        private bool StudentExDetailMoreExists(int id)
        {
            return dbContext.tbl_studentexdetailmore.Count(e => e.id == id) > 0;
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