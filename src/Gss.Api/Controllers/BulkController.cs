using EFCore.BulkExtensions;
using Gss.Data.SqlServer;
using Gss.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gss.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BulkController : ControllerBase
{
    private readonly ILogger<BulkController> _logger;
    private readonly MainDbContext _dbContext;
    private readonly MarkDbContext _srcContext = default!;
    private readonly MarkDbContext _destContext = default!;

    public BulkController(ILogger<BulkController> logger, IHttpContextAccessor context)
    {
        _logger = logger;

        string srcbranch = context.HttpContext!.Request.Query["srcbranch"];
        string destbranch = context.HttpContext!.Request.Query["destbranch"];
        string academicyear = context.HttpContext!.Request.Query["academicyear"];

        if (!string.IsNullOrWhiteSpace(academicyear) && !string.IsNullOrWhiteSpace(srcbranch) 
            && !string.IsNullOrWhiteSpace(destbranch))
        {
            _srcContext = new MarkDbContext($"gssmarks{academicyear}{srcbranch}", academicyear);
            _destContext = new MarkDbContext($"gssmarks{academicyear}{destbranch}", academicyear);
        }

        _dbContext = new MainDbContext();
    }


    /// <summary>
    /// DbContext created with Dependency Injection
    /// </summary>
    [HttpGet]
    [Route("Transfer")]
    public IActionResult Get(string srcbranch, string destbranch, string academicyear, string studentid)
    {
        if (_srcContext is not null)
        {
            var weeklyMarks = _srcContext.WeeklyMarks?.Where(x => x.studentid == studentid);
            var testMarks = _srcContext.TestMarks?.Where(x => x.studentid == studentid);

            if (_destContext is not null)
            {
                if (testMarks is not null)
                {
                    _destContext.BulkInsertOrUpdate(testMarks.ToList(), options =>
                    {
                        // options.PropertiesToIncludeOnCompare = new List<string>
                        // {
                        //     nameof(TestMark.academicyear),
                        //     nameof(TestMark.testtype),
                        //     nameof(TestMark.quarternumber),
                        //     nameof(TestMark.weeknumber),
                        //     nameof(TestMark.description),
                        //     nameof(TestMark.studentid),
                        //     nameof(TestMark.markid),
                        // };
                        options.UpdateByProperties = new List<string>
                        {
                            nameof(TestMark.academicyear),
                            nameof(TestMark.testtype),
                            nameof(TestMark.quarternumber),
                            nameof(TestMark.weeknumber),
                            nameof(TestMark.description),
                            nameof(TestMark.studentid),
                            nameof(TestMark.markid),
                        };
                    });
                }

                if (weeklyMarks is not null)
                {
                    _destContext.BulkInsertOrUpdate(weeklyMarks.ToList(), options =>
                    {
                        options.UpdateByProperties = new List<string>
                        {
                            nameof(WeeklyMark.subjectid),
                            nameof(WeeklyMark.acyear),
                            nameof(WeeklyMark.quarternumber),
                            nameof(WeeklyMark.weeknumber),
                            nameof(WeeklyMark.description),
                            nameof(WeeklyMark.studentid),
                            nameof(WeeklyMark.markid),
                        };
                    });
                }
            }
        }

        return Ok("Yay!");
    }

    [HttpGet]
    [Route("Students")]
    public IActionResult Get()
    {
        var result = _dbContext.Students?.Take(50);
        return Ok(result);
    }
}
