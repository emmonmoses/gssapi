using Gss.Data.SqlServer;
using Microsoft.AspNetCore.Mvc;

namespace Gss.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{      
    private readonly MainDbContext _dbContext = new MainDbContext();   
    
    [HttpGet]
    public IActionResult Get()
    {
        var students = _dbContext.Students;
        return Ok(students);
    }

    [HttpGet("{studentId}")]
    public IActionResult Get(string studentId)
    {        
        var student = _dbContext.Students?.SingleOrDefault(x => x.studentid == studentId);
        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }
}
