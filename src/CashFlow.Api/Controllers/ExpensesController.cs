using Microsoft.AspNetCore.Mvc;
using CashFlow.Api.Models;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ExpensesController : ControllerBase
{

   [HttpPost]
   [ProducesResponseType(StatusCodes.Status201Created)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public IActionResult Register([FromBody] RequestExpenseJson request)
   {
      try
      {
         var useCase = new RegisterExpenseUseCase();
         var response = useCase.Execute(request);

         return Created(string.Empty, response);
      }
      catch (ErrorOnValidationException e)
      {
         var errorResponse = new ResponseErrorJson(e.Errors);
         
         return BadRequest(errorResponse);
      }
      catch 
      {
         var errorResponse = new ResponseErrorJson("An unexpected error occurred.");
        
         return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
      }
   }
}