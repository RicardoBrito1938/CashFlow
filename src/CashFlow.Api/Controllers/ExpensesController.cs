using Microsoft.AspNetCore.Mvc;
using CashFlow.Api.Models;
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
      return Created();
   }
}