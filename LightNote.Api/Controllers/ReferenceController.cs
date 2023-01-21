using LightNote.Api.Contracts.Reference.Request;
using LightNote.Api.Contracts.Reference.Response;
using LightNote.Api.Extensions;
using LightNote.Api.Filters;
using LightNote.Application.BusinessLogic.References.Commands;
using LightNote.Application.BusinessLogic.References.Queries;
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
    [Authorize]
    public class ReferenceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReferenceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route(ApiRoutes.Reference.Create)]
        [ValidateModel]
        public async Task<IActionResult> CreateAsync([FromBody] CreateReferenceRequest createRequest)
        {
            var command = createRequest.Adapt<CreateReference>();
            command.UserProfileId = HttpContext.GetCurrentUserId();
            var operationResult = await _mediator.Send(command);
            var reference = operationResult.Value.Adapt<ReferenceResponse>();
            return CreatedAtAction(nameof(GetByIdAsync), new { id = reference.Id }, reference);
        }

        [HttpGet]
        [Route(ApiRoutes.Reference.GetById)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequestWithErrors(new List<Exception>() { new Exception("Id cannot be empty string") });
            }
            var userProfileId = HttpContext.GetCurrentUserId();
            var command = new LightNote.Application.BusinessLogic.References.Queries
                .GetReferenceById
            { Id = id, UserProfileId = userProfileId };
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            var reference = operationResult.Value!.Adapt<ReferenceResponse>();
            return Ok(reference);
        }

        [HttpGet]
        [Route(ApiRoutes.Reference.GetAllByNotebookId)]
        public async Task<IActionResult> GetAllByNotebookIdAsync(Guid notebookId)
        {
            var userProfileId = HttpContext.GetCurrentUserId();
            var command = new GetReferencesByNotebookId()
            {
                NotebookId = notebookId,
                UserProfileId = userProfileId
            };
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            var references = operationResult.Value.Select(s => s.Adapt<ReferenceResponse>());
            return Ok(references);
        }
        [HttpPut]
        [Route(ApiRoutes.Reference.Update)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateReferenceRequest updateRequest)
        {
            var userProfileId = HttpContext.GetCurrentUserId();
            var command = updateRequest.Adapt<UpdateReference>();
            command.UserProfileId = userProfileId;
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            return NoContent();
        }
        [HttpDelete]
        [Route(ApiRoutes.Reference.Delete)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var userProfileId = HttpContext.GetCurrentUserId();
            var command = new DeleteReference() { Id = id, UserProfileId = userProfileId };
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            return NoContent();
        }
    }
}


