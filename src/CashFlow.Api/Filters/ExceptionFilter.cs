using CashFlow.Communication.Responses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.Api.Filters;

public class ExceptionFilter: IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is CashFlowException e)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnknownException(context);
        }
    }
    
    private void HandleProjectException(ExceptionContext context)
    {
        var cashFlowException = (CashFlowException)context.Exception;
        var errorResponse = new ResponseErrorJson(cashFlowException.GetErrors());
        
        context.HttpContext.Response.StatusCode = cashFlowException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
        
        // if (context.Exception is ErrorOnValidationException)
        // {
        //     var e = (ErrorOnValidationException)context.Exception;
        //     var errorResponse = new ResponseErrorJson(e.Errors);
        //     context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        //     context.Result = new BadRequestObjectResult(errorResponse);
        // } else if (context.Exception is NotFoundException notFoundException)
        // {
        //     var errorResponse = new ResponseErrorJson(notFoundException.Message);
        //     context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        //     context.Result = new NotFoundObjectResult(errorResponse);
        // }
        // else
        // {
        //     var errorResponse = new ResponseErrorJson(context.Exception.Message);
        //     context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        //     context.Result = new ObjectResult(errorResponse);
        // }
    }
    
    private void ThrowUnknownException(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOW_ERROR);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}