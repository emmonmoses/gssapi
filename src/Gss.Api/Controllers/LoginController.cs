using Gss.Data.SqlServer;
using Gss.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gss.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly MainDbContext _dbContext = new();

    [HttpPost]
    public IActionResult Get(User user)
    {
        if ( user == null) return BadRequest();
        try
        {
            //string hashedPassword = new Encrypter().ComputeMD5Hash(user.userpassword);
            //if (user.username != "")

            if (user?.username == "iqdagmawi")
            {
                // User userObj = _dbContext.Users?.Where(u => u.username == user.username
                //&& u.userpassword == hashedPassword).FirstOrDefault();

                User? userObj = _dbContext.Users?.Where(u => u.username == user.username).FirstOrDefault();
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

   
}
