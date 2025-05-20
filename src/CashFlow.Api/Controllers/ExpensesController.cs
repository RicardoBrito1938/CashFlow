using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
   [HttpPost]
   [ProducesResponseType(StatusCodes.Status201Created)]
   public IActionResult Register()
   {
      return Created(string.Empty, null);
   }
}