using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightNote.Api.Contracts.Common;
using LightNote.Api.Contracts.Reference.Request;
using LightNote.Api.Contracts.Reference.Response;
using LightNote.Api.Contracts.Tag.Request;
using LightNote.Api.Contracts.Tag.Response;
using LightNote.Api.Extensions;
using LightNote.Api.Filters;
using LightNote.Application.BusinessLogic.References.Commands;
using LightNote.Application.BusinessLogic.References.Queries;
using LightNote.Application.BusinessLogic.Tags.Commands;
using LightNote.Application.BusinessLogic.Tags.Queries;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LightNote.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> CreateAsync([FromBody] CreateReferenceRequest createReferenceRequest)
        {
            var command = createReferenceRequest.Adapt<CreateReference>();
            command.UserProfileId = HttpContext.GetCurrentUserId();
            var operationResult = await _mediator.Send(command);
            var reference = operationResult.Value.Adapt<ReferenceResponse>();
            return CreatedAtAction(nameof(GetByIdAsync), new { id = reference.Id }, reference);
        }

        [HttpGet]
        [Route(ApiRoutes.Reference.GetById)]
        public async Task<IActionResult> GetByIdAsync(Guid id) {
            if (id == Guid.Empty)
            {
                return this.BadRequestWithErrors(new List<Exception>() {new Exception("Id cannot be empty string") });
            }
            var userProfileId = HttpContext.GetCurrentUserId();
            var command = new LightNote.Application.BusinessLogic.References.Queries
                .GetReferenceById { Id = id, UserProfileId = userProfileId };
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess) {
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
            var command = new GetReferencesByNotebookId() {
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
            
        }
    }
}


