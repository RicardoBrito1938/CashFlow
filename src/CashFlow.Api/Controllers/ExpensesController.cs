using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Application.UseCases.Expenses.GetById;
using Microsoft.AspNetCore.Mvc;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Authorization;

namespace CashFlow.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize]
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
   
   [HttpGet("{id}")]
   [ProducesResponseType(typeof(ResponseExpenseJson), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> GetById(
      [FromServices] IGetExpenseByIdUseCase useCase,
      [FromRoute] long id)
   {
      var response = await useCase.Execute(id);
      return Ok(response);
   }
   
   [HttpDelete("{id}")]
   [ProducesResponseType(StatusCodes.Status204NoContent)]
   [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> Delete(
      [FromServices] IDeleteExpenseByIdUseCase useCase,
      [FromRoute] long id)
   {
      await useCase.Execute(id);
      return NoContent();
   }
   
   [HttpPut]
   [Route("{id}")]
   [ProducesResponseType(typeof(ResponseExpenseJson), StatusCodes.Status204NoContent)]
   [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
   [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> Update(
      [FromServices] IUpdateExpenseUseCase useCase,
      [FromRoute] long id,
      [FromBody] RequestExpenseJson request)
   {
     await useCase.Execute(id, request);
     return NoContent();
   }
}