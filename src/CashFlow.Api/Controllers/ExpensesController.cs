using Microsoft.AspNetCore.Mvc;

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
   /// <returns>The created expense</returns>
   [HttpPost]
   [ProducesResponseType(StatusCodes.Status201Created)]
   public IActionResult Register()
   {
      return Created(string.Empty, null);
   }
}