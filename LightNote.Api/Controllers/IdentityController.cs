using LightNote.Api.Contracts.Common;
using LightNote.Api.Contracts.Identity.Request;
using LightNote.Api.Contracts.Identity.Response;
using LightNote.Api.Filters;
using LightNote.Application.BusinessLogic.Identity.Commands;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        public IdentityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route(ApiRoutes.Identity.Register)]
        [ValidateModel]
        public async Task<IActionResult> RegisterAsync(Registration registerRequest)
        {
            var command = registerRequest.Adapt<RegisterIdentity>();
            var operationResult = await _mediator.Send(command);
            var authResult = new AuthenticationResult { AccessToken = operationResult.Value.AccessToken, RefreshToken = operationResult.Value.RefreshToken };
            return Ok(authResult);
        }
        [HttpPost]
        [Route(ApiRoutes.Identity.Login)]
        [ValidateModel]
        public async Task<IActionResult> LoginAsync(Login loginRequest)
        {
            var command = loginRequest.Adapt<LoginIdentity>();
            var operationResult = await _mediator.Send(command);
            var authResult = new AuthenticationResult
            {
                AccessToken = operationResult.Value.AccessToken,
                RefreshToken = operationResult.Value.RefreshToken
            };
            return Ok(authResult);
        }
        [HttpPost]
        [Route(ApiRoutes.Identity.Refresh)]
        [ValidateModel]
        public async Task<IActionResult> RefreshAsync([FromBody] Refresh refreshRequest)
        {
            var command = refreshRequest.Adapt<RefreshIdentity>();
            var result = await _mediator.Send(command);
            if (result.IsSuccess == false)
            {
                return BadRequest(new ErrorResponse(403, "Invalid token"));
            }
            return Ok(result.Value);
        }
        [HttpDelete]
        [Route(ApiRoutes.Identity.Logout)]
        public async Task<IActionResult> LogoutAsync()
        {
            //var authorizationHeader = HttpContext.Request.Headers["authorization"];
            //var command = _mapper.Map<LogoutIdentity>(authorizationHeader);
            //await _mediator.Send(command);
            return Ok();
        }
    }
}

