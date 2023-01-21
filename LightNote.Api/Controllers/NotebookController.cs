using LightNote.Api.Contracts.Notebook.Response;
using LightNote.Api.Extensions;
using LightNote.Api.Filters;
using LightNote.Application.BusinessLogic.Notebook.Commands;
using LightNote.Application.BusinessLogic.Notebook.Queries;
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
    public class NotebookController : ControllerBase
    {
        private readonly IMediator _mediator;
        public NotebookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route(ApiRoutes.Notebook.Create)]
        [ValidateModel]
        public async Task<IActionResult> CreateAsync([FromBody] Contracts.Notebook.Request.CreateNotebookRequest createRequest)
        {
            var command = createRequest.Adapt<CreateNotebook>();
            command.UserProfileId = HttpContext.GetCurrentUserId(); ;
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            var notebook = operationResult.Value.Adapt<NotebookResponse>();
            return CreatedAtAction(nameof(GetByIdAsync), new { id = notebook.Id }, notebook);
        }

        [HttpGet]
        [Route(ApiRoutes.Notebook.GetById)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequestWithErrors(new List<Exception>() { new Exception("Id cannot be empty") });
            }
            var userProfileId = HttpContext.GetCurrentUserId();
            var command = new GetNotebookById
            { Id = id, UserProfileId = userProfileId };
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            var notebook = operationResult.Value!.Adapt<NotebookResponse>();
            return Ok(notebook);
        }

        [HttpGet]
        [Route(ApiRoutes.Notebook.GetAll)]
        public async Task<IActionResult> GetAllByNotebookIdAsync()
        {
            var userProfileId = HttpContext.GetCurrentUserId();
            var command = new GetAllNotebooks()
            {
                UserProfileId = userProfileId
            };
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            var notebooks = operationResult.Value.Select(s => s.Adapt<NotebookResponse>());
            return Ok(notebooks);
        }
        [HttpPut]
        [Route(ApiRoutes.Notebook.Update)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] Contracts.Notebook.Request.UpdateNotebookRequest updateRequest)
        {
            var userProfileId = HttpContext.GetCurrentUserId();
            var command = updateRequest.Adapt<UpdateNotebook>();
            command.UserProfileId = userProfileId;
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            return NoContent();
        }
        [HttpDelete]
        [Route(ApiRoutes.Notebook.Delete)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var userProfileId = HttpContext.GetCurrentUserId();
            var command = new DeleteNotebook() { Id = id, UserProfileId = userProfileId };
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            return NoContent();
        }
    }
}
