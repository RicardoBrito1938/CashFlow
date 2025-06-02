using CashFlow.Application.UseCases.Expenses.GetAll;
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
   [ProducesResponseType(typeof(ResponseRegisteredExpenseJson), StatusCodes.Status201Created)]
   [ProducesResponseType(typeof(ResponseErrorJson),StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> Register(
      [FromServices] IRegisterExpenseUseCase useCase,
      [FromBody] RequestExpenseJson request)
   {
      var response = await useCase.Execute(request);
   
      return Created(string.Empty, response);
   }
   
   [HttpGet]
   [ProducesResponseType(typeof(ResponseExpensesJson), StatusCodes.Status200OK)]
   [ProducesResponseType( StatusCodes.Status204NoContent)]
   public async Task<IActionResult> GetAll([FromServices] IGetAllExpensesUseCase useCase)
   {
      var response = await useCase.Execute();
      
      if(response.Expenses.Count != 0) return Ok(response);
     
      return NoContent();
   }
}