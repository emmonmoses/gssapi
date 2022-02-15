using System.Net;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using gssManRegistration.Models;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;

namespace gssManRegistration.Controllers
{
    public class MegWeeklyMarksController : ApiController
    {
        private readonly megMarkDbContext dbContext = new megMarkDbContext();

        // GET: api/megweeklymarks
        public IQueryable<weeklymarks20212022> GetWeeklyMarks()
        {
            return dbContext.weeklymarks20212022;
        }

        // GET: api/megweeklymarks/5    
        [ResponseType(typeof(weeklymarks20212022))]
        public IHttpActionResult GetWeeklyMark(int id)
        {
            weeklymarks20212022 marks = dbContext.weeklymarks20212022.Find(id);
            if (marks == null)
            {
                return NotFound();
            }

            return Ok(marks);
        }


        // POST: api/megweeklymarks    
        [ResponseType(typeof(weeklymarks20212022))]
        public IHttpActionResult PostWeeklyMark(weeklymarks20212022 marks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dbContext.weeklymarks20212022.Add(marks);
            dbContext.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { marks.id }, marks);
        }

        // PUT: api/megweeklymarks/5    
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWeeklyMark(int id, weeklymarks20212022 marks)
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

        // DELETE: api/megweeklymarks/5    
        [ResponseType(typeof(weeklymarks20212022))]
        public IHttpActionResult DeleteWeeklyMark(int id)
        {
            weeklymarks20212022 marks = dbContext.weeklymarks20212022.Find(id);
            if (marks == null)
            {
                return NotFound();
            }

            dbContext.weeklymarks20212022.Remove(marks);
            dbContext.SaveChanges();

            return Ok();
            //return Ok(marks);
            //return StatusCode(HttpStatusCode.OK);
        }
        private bool MarksExists(int id)
        {
            return dbContext.weeklymarks20212022.Count(e => e.id == id) > 0;
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

