namespace Gss.Data.SqlServer
{
    public class DatabaseOptions
    {
        public string? Server { get; set; }
        public string? UserId { get; set; }
        public string? Password { get; set; }
        public bool IntegratedSecurity { get; internal set; }
        public bool MultipleActiveResultSets { get; internal set; }
    }
}