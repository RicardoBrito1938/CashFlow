using Microsoft.AspNetCore.Mvc;
using CashFlow.Api.Models;

namespace CashFlow.Api.Controllers;

/// <summary>
/// Manages expense operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ExpensesController : ControllerBase
{
   /// <summary>
   /// Registers a new expense
   /// </summary>
   /// <param name="expenseDto">The expense details</param>
   /// <returns>The created expense</returns>
   /// <response code="201">Returns the newly created expense</response>
   /// <response code="400">If the expense is invalid</response>
   [HttpPost]
   [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ExpenseResponse))]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public IActionResult Register([FromBody] ExpenseDto expenseDto)
   {
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      // In a real app, you would save this to a database
      var createdExpense = new ExpenseResponse
      {
         Id = Guid.NewGuid(),
         Description = expenseDto.Description,
         Amount = expenseDto.Amount,
         Date = expenseDto.Date,
         CreatedAt = DateTime.UtcNow
      };
      
      return Created($"/api/expenses/{createdExpense.Id}", createdExpense);
   }
}