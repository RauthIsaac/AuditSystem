using AuditSystem.Application.Features.Auth.Commands;
using AuditSystem.Application.Features.Auth.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuditSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /*------------------------------------------------------------------*/
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /*------------------------------------------------------------------*/
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await _mediator.Send(new LoginCommand(request.Email, request.Password));

            if (result == null)
                return Unauthorized("Invalid email or password.");

            return Ok(result);
        }
        /*------------------------------------------------------------------*/
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var result = await _mediator.Send(
                new RegisterCommand(request.Name, request.Email, request.Password));

            if (result == null)
                return BadRequest("Email already exists.");

            return Ok(result);
        }
        /*------------------------------------------------------------------*/
    }
}