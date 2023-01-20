using LightNote.Api.Contracts.PermanentNote.Request;
using LightNote.Api.Contracts.Question.Request;
using LightNote.Api.Contracts.Question.Response;
using LightNote.Api.Extensions;
using LightNote.Api.Filters;
using LightNote.Application.BusinessLogic.PermanentNote.Commands;
using LightNote.Application.BusinessLogic.Question.Commands;
using LightNote.Application.BusinessLogic.Question.Queries;
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
    public class QuestionContoller : ControllerBase
    {
        private readonly IMediator _mediator;
        public QuestionContoller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route(ApiRoutes.Question.Create)]
        [ValidateModel]
        public async Task<IActionResult> CreateAsync([FromBody] CreateQuestionRequest createRequest)
        {
            var command = createRequest.Adapt<CreateQuestion>();
            command.UserProfileId = HttpContext.GetCurrentUserId();
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            var question = operationResult.Value.Adapt<QuestionResponse>();
            return CreatedAtAction(nameof(GetByIdAsync), new { id = question.Id }, question);
        }

        [HttpGet]
        [Route(ApiRoutes.Question.GetById)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequestWithErrors(new List<Exception>() { new Exception("Id cannot be empty string") });
            }
            var userProfileId = HttpContext.GetCurrentUserId();
            var command = new GetQuestionById { Id = id, UserProfileId = userProfileId };
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            var question = operationResult.Value!.Adapt<QuestionResponse>();
            return Ok(question);
        }

        [HttpGet]
        [Route(ApiRoutes.Question.GetAllByNotebookId)]
        public async Task<IActionResult> GetAllByNotebookIdAsync(Guid notebookId)
        {
            var userProfileId = HttpContext.GetCurrentUserId();
            var command = new GetQuestionsByNotebookId()
            {
                NotebookId = notebookId,
                UserProfileId = userProfileId
            };
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            var questions = operationResult.Value.Select(s => s.Adapt<QuestionResponse>());
            return Ok(questions);
        }

        [HttpPut]
        [Route(ApiRoutes.Question.Update)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateQuestionRequest updateRequest)
        {
            var userProfileId = HttpContext.GetCurrentUserId();
            var command = updateRequest.Adapt<UpdateQuestion>();
            command.UserProfileId = userProfileId;
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            return NoContent();
        }
        [HttpDelete]
        [Route(ApiRoutes.Question.Delete)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var userProfileId = HttpContext.GetCurrentUserId();
            var command = new DeleteQuestion() { Id = id, UserProfileId = userProfileId };
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            return NoContent();
        }
    }
}