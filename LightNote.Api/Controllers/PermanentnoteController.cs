using LightNote.Api.Contracts.PermanentNote.Request;
using LightNote.Api.Contracts.PermanentNote.Response;
using LightNote.Api.Extensions;
using LightNote.Api.Filters;
using LightNote.Application.BusinessLogic.PermanentNote.Commands;
using LightNote.Application.BusinessLogic.PermanentNote.Queries;
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
    public class PermanentnoteController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PermanentnoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route(ApiRoutes.PermanentNote.Create)]
        [ValidateModel]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePermanentNoteRequest createRequest)
        {
            var command = createRequest.Adapt<CreatePermanentNote>();
            command.UserProfileId = HttpContext.GetCurrentUserId(); ;
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            var permanentNote = operationResult.Value.Adapt<PermanentNoteResponse>();
            return CreatedAtAction(nameof(GetByIdAsync), new { id = permanentNote.Id }, permanentNote);
        }

        [HttpGet]
        [Route(ApiRoutes.PermanentNote.GetById)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequestWithErrors(new List<Exception>() { new Exception("Id cannot be empty string") });
            }
            var userProfileId = HttpContext.GetCurrentUserId();
            var command = new GetPermanentNoteById
            { Id = id, UserProfileId = userProfileId };
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            var permanentNote = operationResult.Value!.Adapt<PermanentNoteResponse>();
            return Ok(permanentNote);
        }

        [HttpGet]
        [Route(ApiRoutes.PermanentNote.GetAllByNotebookId)]
        public async Task<IActionResult> GetAllByNotebookIdAsync(Guid notebookId)
        {
            var userProfileId = HttpContext.GetCurrentUserId();
            var command = new GetPermanentNotesByNotebookId()
            {
                NotebookId = notebookId,
                UserProfileId = userProfileId
            };
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            var permanentNotes = operationResult.Value.Select(s => s.Adapt<PermanentNoteResponse>());
            return Ok(permanentNotes);
        }

        [HttpPut]
        [Route(ApiRoutes.PermanentNote.Update)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdatePermanentNoteRequest updateRequest)
        {
            var userProfileId = HttpContext.GetCurrentUserId();
            var command = updateRequest.Adapt<UpdatePermanentNote>();
            command.UserProfileId = userProfileId;
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            return NoContent();
        }
        [HttpDelete]
        [Route(ApiRoutes.PermanentNote.Delete)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var userProfileId = HttpContext.GetCurrentUserId();
            var command = new DeletePermanentNote() { Id = id, UserProfileId = userProfileId };
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            return NoContent();
        }
    }
}