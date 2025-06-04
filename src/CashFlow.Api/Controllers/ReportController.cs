using System.Net.Mime;
using CashFlow.Application.UseCases.Expenses.Reports.Excel;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status204NoContent)]
public class ReportController: ControllerBase
{
    [HttpGet("excel")]
    public async Task<IActionResult> GetExcel([FromHeader] DateOnly month, [FromServices]IGeneratedExpensesReportExcelUseCase useCase)
    {
        byte[] fileContents = await useCase.Execute(month);
        if(fileContents.Length == 0)return NoContent();
        return File(fileContents, MediaTypeNames.Application.Octet, "Report.xlsx");
    }
}