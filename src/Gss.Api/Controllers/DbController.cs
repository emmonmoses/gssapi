using Gss.Data.SqlServer;
using Gss.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gss.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DbController : ControllerBase
{
    private readonly ILogger<DbController> _logger;
    private readonly MarkDbContext _dbContext = default!;

    public DbController(ILogger<DbController> logger, IHttpContextAccessor context)
    {
        _logger = logger;
        
        foreach (var r in context.HttpContext?.Request?.Query!)
        {
            // you could use this to dynamically create the context you want
            Console.WriteLine($"{r.Key} == {r.Value} \r\n");
        }

        string branch = context.HttpContext.Request.Query["branch"];
        string academicyear = context.HttpContext.Request.Query["academicyear"];

        _dbContext = new MarkDbContext($"gssmarks{academicyear}{branch}", academicyear);
    }


    /// <summary>
    /// DbContext created with Dependency Injection
    /// </summary>
    //[HttpGet("DI")]
    //public IActionResult Get(string branch, string academicyear)
    //{
    //    if (_dbContext is not null)
    //    {
    //        var result = _dbContext.WeeklyMarks;
    //        return Ok(result);
    //    }
    //    return BadRequest();
    //}


    /// <summary>
    /// DbContext is created on controller action level
    /// </summary>
    [HttpGet("WeeklyMarks", Name = "GetWeeklyMarks")]
    public IActionResult GetWeeklyMarks(string academicyear, string branch)
    {
        var database = $"gssmarks{academicyear}{branch}";
        var tableName = $"weeklymarks{academicyear}";
        var db = new MarkDbContext(database, academicyear);
        var result = db.GetRecords<WeeklyMark>();

        return Ok(result);
    }


    /// <summary>
    /// DbContext is created on controller action level
    /// </summary>
    [HttpGet("TestMarks", Name = "GetTestMarks")]
    public IActionResult GetTestMarks(string academicyear, string branch)
    {
        var database = $"gssmarks{academicyear}{branch}";
        var tableName = $"testmarks{academicyear}";
        var db = new MarkDbContext(database, academicyear);
        var result = db.TestMarks;// db.GetRecords<TestMark>();

        return Ok(result);
    }
}
