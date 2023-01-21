using LightNote.Api.Contracts.SlipNote.Request;
using LightNote.Api.Contracts.SlipNote.Response;
using LightNote.Api.Extensions;
using LightNote.Api.Filters;
using LightNote.Application.BusinessLogic.SlipNote.Commands;
using LightNote.Application.BusinessLogic.SlipNote.Queries;
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
    public class SlipnoteController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SlipnoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route(ApiRoutes.SlipNote.Create)]
        [ValidateModel]
        public async Task<IActionResult> CreateAsync([FromBody] CreateSlipNoteRequest createRequest)
        {
            var command = createRequest.Adapt<CreateSlipNote>();
            command.UserProfileId = HttpContext.GetCurrentUserId(); ;
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            var slipNote = operationResult.Value.Adapt<SlipNoteResponse>();
            return CreatedAtAction(nameof(GetByIdAsync), new { id = slipNote.Id }, slipNote);
        }

        [HttpGet]
        [Route(ApiRoutes.SlipNote.GetById)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequestWithErrors(new List<Exception>() { new Exception("Id cannot be empty string") });
            }
            var userProfileId = HttpContext.GetCurrentUserId();
            var command = new GetSlipNoteById
            { Id = id, UserProfileId = userProfileId };
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            var slipNote = operationResult.Value!.Adapt<SlipNoteResponse>();
            return Ok(slipNote);
        }

        [HttpGet]
        [Route(ApiRoutes.SlipNote.GetAllByNotebookId)]
        public async Task<IActionResult> GetAllByNotebookIdAsync(Guid notebookId)
        {
            var userProfileId = HttpContext.GetCurrentUserId();
            var command = new GetSlipNotesByNotebookId()
            {
                NotebookId = notebookId,
                UserProfileId = userProfileId
            };
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            var slipNotes = operationResult.Value.Select(s => s.Adapt<SlipNoteResponse>());
            return Ok(slipNotes);
        }

        [HttpPut]
        [Route(ApiRoutes.SlipNote.Update)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateSlipNoteRequest updateRequest)
        {
            var userProfileId = HttpContext.GetCurrentUserId();
            var command = updateRequest.Adapt<UpdateSlipNote>();
            command.UserProfileId = userProfileId;
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            return NoContent();
        }
        [HttpDelete]
        [Route(ApiRoutes.SlipNote.Delete)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var userProfileId = HttpContext.GetCurrentUserId();
            var command = new DeleteSlipNote() { Id = id, UserProfileId = userProfileId };
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            return NoContent();
        }
    }
}
