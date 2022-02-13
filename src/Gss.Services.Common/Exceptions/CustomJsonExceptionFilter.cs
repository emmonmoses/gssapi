using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Gss.Services.Common.Exceptions;
public class CustomJsonExceptionFilter : IExceptionFilter
{
    public async void OnException(ExceptionContext context)
    {
        int statusCode = 500;
        var messages = new ResponseError();
        var exceptionType = context.Exception.GetType();
        Console.WriteLine($"{exceptionType.FullName} \t {exceptionType.Name}");

        if (exceptionType.Name == "SqlException")
        {
            statusCode = 400;
            messages.Code = "db_sql_error";
            messages.Reason = context.Exception.Message;
        }
        else
        {
            messages.Code = "server_error";
            messages.Reason = "Oops! Something went wrong.";
        }


        var response = context.HttpContext.Response;
        response.StatusCode = statusCode;
        response.ContentType = "application/json";

        context.ExceptionHandled = true;
        await response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(messages));
    }
}
