using AutoMapper;
using LightNote.Api.Contracts.Identity.Request;
using LightNote.Api.Contracts.Identity.Response;
using LightNote.Api.Filters;
using LightNote.Application.BusinessLogic.Identity.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LightNote.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    [HandleException]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public IdentityController(IMediator mediator, IMapper mapper) {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Route(ApiRoutes.Identity.Register)]
        [ValidateModel]
        public async Task<IActionResult> Register(Registration registerRequest)
        {
            var command = _mapper.Map<RegisterIdentity>(registerRequest);
            var operationResult = await _mediator.Send(command);
            var authResult = new AuthenticationResult { Token = operationResult.Value };            
            return Ok(authResult);
        }
        [HttpPost]
        [Route(ApiRoutes.Identity.Login)]
        [ValidateModel]
        public async Task<IActionResult> Loginf(Login loginRequest)
        {
            var command = _mapper.Map<LoginIdentity>(loginRequest);
            var operationResult = await _mediator.Send(command);
            var authResult = new AuthenticationResult { Token = operationResult.Value };
            return Ok(authResult);
        }
    }
}

