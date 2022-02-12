using Gss.Data.SqlServer;
using Microsoft.AspNetCore.Mvc;

namespace Gss.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DbController : ControllerBase
{
    private readonly ILogger<DbController> _logger;
    private readonly MarkDbContext? _dbContext;

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
        if (!string.IsNullOrWhiteSpace(branch) && !string.IsNullOrWhiteSpace(academicyear))
        {
            _dbContext = new MarkDbContext($"gssmarks{academicyear}{branch}", academicyear);
        }
    }

    [HttpGet]
    [Route("DI")]
    public IActionResult Get(string branch, string academicyear)
    {
        if (_dbContext is not null)
        {
            var result = _dbContext.WeeklyMarks;
            return Ok(result);
        }

        return BadRequest();
    }

    [HttpGet(Name = "GetWeeklyMarks")]
    [Route("WeeklyMarks")]
    public IActionResult GetWeeklyMarks(string academicyear)
    {
        var database = $"gssmarks{academicyear}lafto";
        var tableName = $"weeklymarks{academicyear}";
        var db = new MarkDbContext(database, academicyear);
        var result = db.WeeklyMarks;// db.GetRecords<WeeklyMark>();

        return Ok(result);
    }

    [HttpGet(Name = "GetTestMarks")]
    [Route("TestMarks")]
    public IActionResult GetTestMarks(string academicyear)
    {
        var database = $"gssmarks{academicyear}lafto";
        var tableName = $"testmarks{academicyear}";
        var db = new MarkDbContext(database, academicyear);
        var result = db.WeeklyMarks;// db.GetRecords<TestMark>();

        return Ok(result);
    }
}
