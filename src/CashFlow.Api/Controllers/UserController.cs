using CashFlow.Application.UseCases.Users.ChangePassword;
using CashFlow.Application.UseCases.Users.Delete;
using CashFlow.Application.UseCases.Users.Porfile;
using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Application.UseCases.Users.Update;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController: ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RequestRegisterUserJson request)
    {
        var response = await useCase.Execute(request);
        
        return Created(string.Empty, response);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseProfileJson), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> GetProfile(
        [FromServices] IGetUserProfileUseCase useCase)
    {
        var response = await useCase.Execute();
        
        return Ok(response);
    }
    
    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProfile(
        [FromServices] IUpdateUserProfileUseCase useCase,
        [FromBody] RequestUpdateUserProfileJson request)
    {
        await useCase.Execute(request);
        
        return NoContent();
    }
    
    [HttpPut]
    [Route("change-password")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePassword(
        [FromServices] IUpdateUserPasswordUseCase useCase,
        [FromBody] RequestUpdateUserPasswordJson request)
    {
        await useCase.Execute(request);
        
        return NoContent();
    }
    
    [HttpDelete]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteUser(
        [FromServices] IDeleteUserUseCase useCase)
    {
        await useCase.Execute();
        
        return NoContent();
    }
}