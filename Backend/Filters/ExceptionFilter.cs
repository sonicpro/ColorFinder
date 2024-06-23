namespace Backend.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Backend;

public class ExceptionFilter : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Result != null)
            return;

        context.Result = CreateResult(context.Exception switch {
            (InvalidCredentialsException invalidCredentials) => (StatusCodes.Status401Unauthorized, new Error("Invalid username or password.", invalidCredentials.ToString())),
                
            (ArgumentException argumentException) => (StatusCodes.Status400BadRequest, 
                new Error($"{argumentException.ParamName} is not valid {argumentException.Message}", argumentException.ToString())),

            (HttpResponseException httpResponseException) => (httpResponseException.StatusCode, httpResponseException.Value),

            { } e => (StatusCodes.Status500InternalServerError, new Error(e.Message, e.ToString()))
        });
    }

    private IActionResult CreateResult((int statusCode, object error) args)
    {
        return new ObjectResult(args.error) { StatusCode = args.statusCode };
    }
}