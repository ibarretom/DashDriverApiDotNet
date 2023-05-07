using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared;
using Shared.ValueObject.Exceptions;
using Shared.ValueObject.Response;
using System.Net;

namespace Api.Filters;

public class ExceptionsFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is DashDriverException)
        {
            HandleDashDriverException(context);
        }else
        {
            HandleGenericException(context);
        }
    }

    private void HandleDashDriverException(ExceptionContext context)
    {
        if(context.Exception is ValidationErrorException)
        {
            HandleValidationErrorException(context);
        }else if(context.Exception is UserAlreadyRegisteredException)
        {
            HandleUserAlreadyRegisteredException(context);
        }
    }

    private void HandleValidationErrorException(ExceptionContext context)
    {
        var exception = context.Exception as ValidationErrorException;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ExceptionResponse(exception?.ErrorMessages.ToList()));
    }

    private void HandleUserAlreadyRegisteredException(ExceptionContext context)
    {
        var exception = context.Exception as UserAlreadyRegisteredException;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
        context.Result = new ObjectResult(new ExceptionResponse(exception?.Message));
    }

    private void HandleGenericException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ExceptionResponse(ErrorMessagesResource.INTERNAL_SERVER_ERROR));
    }
}
