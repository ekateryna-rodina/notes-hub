using System;
using LightNote.Api.Contracts.Identity.Request;
using LightNote.Api.Contracts.Tag.Request;
using LightNote.Api.Contracts.Tag.Response;
using LightNote.Api.Filters;
using LightNote.Application.BusinessLogic.Identity.Commands;
using LightNote.Application.BusinessLogic.Tags.Commands;
using LightNote.Application.BusinessLogic.Tags.Queries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LightNote.Api.Contracts.Common;
using LightNote.Api.Contracts.Identity.Response;
using LightNote.Api.Extensions;

namespace LightNote.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    [HandleException]
    [Authorize]
    public class TagController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TagController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateAsync(CreateTagRequest createTagRequest)
        {
            var command = createTagRequest.Adapt<CreateTag>();
            var operationResult = await _mediator.Send(command);
                var tag = new CreateTagResponse {Id = operationResult.Value.Id.Value, Name = operationResult.Value.Name };
            
            return CreatedAtAction(nameof(CreateAsync), new { id = tag.Id }, tag);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var command = new GetAllTags();
            var operationResult = await _mediator.Send(command);
            var tags = operationResult.Value.Adapt<IEnumerable<CreateTagResponse>>();

            return Ok(tags);
        }
    }
}

