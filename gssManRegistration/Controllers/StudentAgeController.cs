using System.Net;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using gssManRegistration.Models;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;

namespace gssManRegistration.Controllers
{
    public class StudentAgeController : ApiController
    {
        private readonly gssManRegistrationDbContext dbContext = new gssManRegistrationDbContext();

        // GET: api/studentage    
        public IQueryable<tbl_studentages> GetStudentAge()
        {
            return dbContext.tbl_studentages;
        }

        // GET: api/studentage/{{studentId}}         
        [ResponseType(typeof(tbl_studentages))]
        public IHttpActionResult GetStudentAge(int id)
        {
            var student = dbContext.tbl_studentages.SingleOrDefault(x => x.studentid == id.ToString());
            //tbl_studentages student = dbContext.tbl_studentages.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }


        // POST: api/studentage    
        [ResponseType(typeof(tbl_studentages))]
        public IHttpActionResult PostStudentAge(tbl_studentages student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dbContext.tbl_studentages.Add(student);
            dbContext.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { student.id }, student);
        }

        // PUT: api/studentage/5    
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudentAge(int id, tbl_studentages student)
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
                if (!StudentExists(id))
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

        // DELETE: api/studentage/5    
        [ResponseType(typeof(tbl_studentages))]
        public IHttpActionResult DeleteStudentAge(int id)
        {
            tbl_studentages student = dbContext.tbl_studentages.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            dbContext.tbl_studentages.Remove(student);
            dbContext.SaveChanges();

            return Ok();
            //return Ok(student);
            //return StatusCode(HttpStatusCode.OK);
        }
        private bool StudentExists(int id)
        {
            return dbContext.tbl_studentages.Count(e => e.id == id) > 0;
        }

        //private bool StudentIdExists(string id)
        //{
        //    return dbContext.tbl_studentages.Count(e => e.studentid == id) > 0;
        //}
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
