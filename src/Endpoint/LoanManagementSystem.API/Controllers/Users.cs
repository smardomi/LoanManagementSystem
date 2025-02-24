using LoanManagementSystem.Application.Features.Identity.User.Commands.LoginUser;
using LoanManagementSystem.Application.Features.Identity.User.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagementSystem.Web.Endpoints;

[ApiController]
[Route("[controller]/[action]")]
public class Users(IMediator mediator) : ControllerBase
{  

    [HttpPost]
    public async Task<IActionResult> RegisterUser(RegisterUserCommand command)
    {
        await mediator.Send(command);
        return Created();
    }

    [HttpPost]
    public async Task<IActionResult> LoginUser(LoginUserCommand command)
    {
       var result = await mediator.Send(command);
       return Ok(result);
    }
}
