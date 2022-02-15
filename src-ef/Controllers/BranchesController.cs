using System.Linq;
using System.Web.Http;
using gssManRegistration.Models;

namespace gssManRegistration.Controllers
{
    public class BranchesController : ApiController
    {
        private readonly gssManRegistrationDbContext dbContext = new gssManRegistrationDbContext();
        
        // GET: api/branches
        public IQueryable<branches> GetBranches()
        {
            return dbContext.branches;
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
