using System.Net;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using gssManRegistration.Models;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;

namespace gssManRegistration.Controllers
{
    public class StudentController : ApiController
    {
        private readonly gssManRegistrationDbContext dbContext = new gssManRegistrationDbContext();
       // private readonly laftoTestMarkDbContext laftoMarkDbContext = new laftoTestMarkDbContext();
        
        // GET: api/student
        public IQueryable<tbl_student> GetStudent()
        {
            return dbContext.tbl_student;
        }

        // GET: api/student/{{studentID}}    
        [ResponseType(typeof(tbl_student))]
        public IHttpActionResult GetStudent(int id)
        {
            // tbl_student student = dbContext.tbl_student.Find(id);
            var student = dbContext.tbl_student.SingleOrDefault(x => x.studentid == id.ToString());
            if (student == null)
            {
                return NotFound();
            }
           
            return Ok(student);
            
        }


        // POST: api/student    
        [ResponseType(typeof(tbl_student))]
        public IHttpActionResult PostStudent(tbl_student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dbContext.tbl_student.Add(student);
            dbContext.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { student.id }, student);
        }

        // PUT: api/student/5    
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudent(int id, tbl_student student)
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

        // DELETE: api/student/5    
        [ResponseType(typeof(tbl_student))]
        public IHttpActionResult DeleteStudent(int id)
        {
            tbl_student student = dbContext.tbl_student.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            dbContext.tbl_student.Remove(student);
            dbContext.SaveChanges();

            return Ok();
            //return Ok(student);
            //return StatusCode(HttpStatusCode.OK);
        }
        private bool StudentExists(int id)
        {
            return dbContext.tbl_student.Count(e => e.id == id) > 0;
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
