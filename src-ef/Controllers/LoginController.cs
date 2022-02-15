using System;
using System.Data;
using System.Linq;
using System.Web.Http;
using gssManRegistration.Models;

namespace gssManRegistration.Controllers
{
    public class LoginController : ApiController
    {
        private readonly gssManRegistrationDbContext dbContext = new gssManRegistrationDbContext();
        

        // Post: api/login 
        public IHttpActionResult Login(tbl_user user)
        {
            try
            {
                //string hashedPassword = new Encrypter().ComputeMD5Hash(user.userpassword);
                //if (user.username != "")
                if (user.username == "iqdagmawi")
                {
                    // tbl_user userObj = dbContext.tbl_user.Where(u => u.username == user.username
                    //&& u.userpassword == hashedPassword).FirstOrDefault();
                    tbl_user userObj = dbContext.tbl_user.Where(u => u.username == user.username && u.usertype.Equals("IQ")).FirstOrDefault();
                    if (userObj != null)
                    {
                        return Ok(userObj);
                    }
                }
                return BadRequest("Invalid Credentials");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
