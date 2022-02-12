using EFCore.BulkExtensions;
using Gss.Data.SqlServer;
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
    [HttpGet]
    [Route("DI")]
    public IActionResult Get(string branch, string academicyear)
    {
        if (_dbContext is not null)
        {
            var result = _dbContext.WeeklyMarks;
            
            // Bulk Insert entities [NOT TESTED]
            // _dbContext.BulkInsert(result!.ToList(), options => 
            // {
            //     // TODO: using a different nuget package here,
            //     // https://github.com/borisdj/EFCore.BulkExtensions,
            //     // hence the lines below don't work
            //     // options.InsertIfNotExists = true;
            //     // options.ColumnPrimaryKeyExpression = ma => new 
            //     // { 
            //     //     ma.acyear
            //     // };
            //     options.IncludeGraph = true;
            //     options.PropertiesToIncludeOnCompare = new List<string> { "subjectid" };
            // });

            return Ok(result);
        }

        return BadRequest();
    }


    /// <summary>
    /// DbContext is created on controller level
    /// </summary>
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


    /// <summary>
    /// DbContext is created on controller level
    /// </summary>
    [HttpGet(Name = "GetTestMarks")]
    [Route("TestMarks")]
    public IActionResult GetTestMarks(string academicyear)
    {
        var database = $"gssmarks{academicyear}lafto";
        var tableName = $"testmarks{academicyear}";
        var db = new MarkDbContext(database, academicyear);
        var result = db.TestMarks;// db.GetRecords<TestMark>();

        return Ok(result);
    }
}
