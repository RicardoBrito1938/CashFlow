using System.Net.Mime;
using CashFlow.Application.UseCases.Expenses.Reports.Excel;
using CashFlow.Application.UseCases.Expenses.Reports.Pdf;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportController: ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel([FromHeader] DateOnly month, [FromServices]IGeneratedExpensesReportExcelUseCase useCase)
    {
        byte[] fileContents = await useCase.Execute(month);
        if(fileContents.Length == 0)return NoContent();
        return File(fileContents, MediaTypeNames.Application.Octet, "Report.xlsx");
    }
    
    [HttpGet("pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetPdf([FromHeader] DateOnly month, [FromServices] IGenerateExpensesReportPdfUseCase useCase)
    {
        byte[] fileContents = await useCase.Execute(month);
        if(fileContents.Length == 0)return NoContent();
        return File(fileContents, MediaTypeNames.Application.Pdf, "Report.pdf");
    }
}