using Newtonsoft.Json;

namespace Gss.Services.Common.Exceptions;

public class ResponseError
{
    [JsonProperty("code")]
    public string? Code { get; set; }
    [JsonProperty("reason")]
    public string? Reason { get; set; }
}
