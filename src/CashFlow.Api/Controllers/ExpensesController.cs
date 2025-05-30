using Microsoft.AspNetCore.Mvc;
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
   public IActionResult Register(
      [FromServices] IRegisterExpenseUseCase useCase,
      [FromBody] RequestExpenseJson request)
   {
      var response = useCase.Execute(request);
   
      return Created(string.Empty, response);
   }
}