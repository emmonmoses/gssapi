namespace Gss.Models;

public class WeeklyMark
{
    public long id { get; set; }
    public string? markid { get; set; }
    public string? studentid { get; set; }
    public string? weeknumber { get; set; }
    public string? quarternumber { get; set; }
    public string? acyear { get; set; }
    public string? subjectid { get; set; }
    public string? subjectname { get; set; }
    public string? dateadded { get; set; }
    public string? dateupdated { get; set; }
    public string? description { get; set; }
    public string? markvalue { get; set; }
    public string? addedbyuserid { get; set; }
    public string? updatedbyuserid { get; set; }
    public string? flagged { get; set; }
    public string? outofpoints { get; set; }
    public string? quizorhomework { get; set; }
}