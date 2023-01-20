using LightNote.Api.Contracts.Insight.Request;
using LightNote.Api.Contracts.Insight.Response;
using LightNote.Api.Extensions;
using LightNote.Api.Filters;
using LightNote.Application.BusinessLogic.Insight.Commands;
using LightNote.Application.BusinessLogic.Insight.Queries;
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
    public class InsightController : ControllerBase
    {

        private readonly IMediator _mediator;
        public InsightController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route(ApiRoutes.Insight.Create)]
        [ValidateModel]
        public async Task<IActionResult> CreateAsync([FromBody] CreateInsightRequest createRequest)
        {
            var command = createRequest.Adapt<CreateInsight>();
            command.UserProfileId = HttpContext.GetCurrentUserId(); ;
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            var insight = operationResult.Value.Adapt<InsightResponse>();
            return CreatedAtAction(nameof(GetByIdAsync), new { id = insight.Id }, insight);
        }

        [HttpGet]
        [Route(ApiRoutes.Insight.GetById)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequestWithErrors(new List<Exception>() { new Exception("Id cannot be empty string") });
            }
            var userProfileId = HttpContext.GetCurrentUserId();
            var command = new GetInsightById { Id = id, UserProfileId = userProfileId };
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            var insight = operationResult.Value!.Adapt<InsightResponse>();
            return Ok(insight);
        }

        [HttpGet]
        [Route(ApiRoutes.Insight.GetAllByNotebookId)]
        public async Task<IActionResult> GetAllByNotebookIdAsync(Guid notebookId)
        {
            var userProfileId = HttpContext.GetCurrentUserId();
            var command = new GetInsightsByNotebookId()
            {
                NotebookId = notebookId,
                UserProfileId = userProfileId
            };
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            var insight = operationResult.Value.Adapt<InsightResponse>();
            return Ok(insight);
        }

        [HttpPut]
        [Route(ApiRoutes.Insight.Update)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateInsightRequest updateRequest)
        {
            var userProfileId = HttpContext.GetCurrentUserId();
            var command = updateRequest.Adapt<UpdateInsight>();
            command.UserProfileId = userProfileId;
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            return NoContent();
        }
        [HttpDelete]
        [Route(ApiRoutes.Insight.Delete)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var userProfileId = HttpContext.GetCurrentUserId();
            var command = new DeleteInsight() { Id = id, UserProfileId = userProfileId };
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            return NoContent();
        }
    }
}