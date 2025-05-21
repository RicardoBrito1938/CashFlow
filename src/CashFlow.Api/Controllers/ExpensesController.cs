using Microsoft.AspNetCore.Mvc;
using CashFlow.Api.Models;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;

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
      catch (ArgumentException e)
      {
         return BadRequest(e.Message);
      }
      catch (Exception e)
      {
         return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
      }
   }
}