using Gss.Data.SqlServer;
using Microsoft.AspNetCore.Mvc;

namespace Gss.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BranchController : ControllerBase
{      
    private readonly MainDbContext _dbContext = new();   
    
    [HttpGet(Name = "GetBranches")]
    public IActionResult Get()
    {
        var branches = _dbContext.Branches;
        return Ok(branches);
    }

    [HttpGet("{id}", Name = "GetBranch")]
    public IActionResult Get(int id)
    {        
        var branch = _dbContext.Branches?.SingleOrDefault(x => x.id == id);
        if (branch == null)
        {
            return NotFound();
        }

        return Ok(branch);
    }
}
