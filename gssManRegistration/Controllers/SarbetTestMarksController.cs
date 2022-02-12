using System.Net;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using gssManRegistration.Models;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;

namespace gssManRegistration.Controllers
{
    public class SarbetTestMarksController : ApiController
    {
        private readonly sarbetMarkDbContext dbContext = new sarbetMarkDbContext();

        // GET: api/sarbettestmarks
        public IQueryable<testmarks20212022> GetTestMarks()
        {
            return dbContext.testmarks20212022;
        }

        // GET: api/sarbettestmarks/5    
        [ResponseType(typeof(testmarks20212022))]
        public IHttpActionResult GetTestMark(int id)
        {
            testmarks20212022 marks = dbContext.testmarks20212022.Find(id);
            if (marks == null)
            {
                return NotFound();
            }

            return Ok(marks);
        }


        // POST: api/sarbettestmarks    
        [ResponseType(typeof(testmarks20212022))]
        public IHttpActionResult PostTestMark(testmarks20212022 marks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dbContext.testmarks20212022.Add(marks);
            dbContext.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { marks.id }, marks);
        }

        // PUT: api/sarbettestmarks/5    
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTestMark(int id, testmarks20212022 marks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != marks.id)
            {
                return BadRequest();
            }

            dbContext.Entry(marks).State = EntityState.Modified;

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarksExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // return CreatedAtRoute("DefaultApi", new { marks.id }, marks);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/sarbettestmarks/5    
        [ResponseType(typeof(testmarks20212022))]
        public IHttpActionResult DeleteTestMark(int id)
        {
            testmarks20212022 marks = dbContext.testmarks20212022.Find(id);
            if (marks == null)
            {
                return NotFound();
            }

            dbContext.testmarks20212022.Remove(marks);
            dbContext.SaveChanges();

            return Ok();
            //return Ok(marks);
            //return StatusCode(HttpStatusCode.OK);
        }
        private bool MarksExists(int id)
        {
            return dbContext.testmarks20212022.Count(e => e.id == id) > 0;
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
