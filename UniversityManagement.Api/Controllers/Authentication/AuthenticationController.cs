using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniversityManagement.Application.Features.Login.Commands.CreateRegistrations;
using UniversityManagement.Application.Features.Registration.Commands.CreateRegistrations;
using UniversityManagement.Shared.Response;

namespace UniversityManagement.Api.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] CreateRegistrationCommand request)
        {
            CommonResponse<Guid> Register = await _mediator.Send(request);
            return Ok(Register);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] CreateLoginCommand request)
        {
            AuthResponse login = await _mediator.Send(request);
            return Ok(login);
        }
    }
}
