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
using System.Linq;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using GetTagsByIds = LightNote.Api.Contracts.Tag.Request.GetTagsByIds;
using System.ComponentModel.DataAnnotations;

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
        [Route(ApiRoutes.Tag.CreateMany)]
        [ValidateModel]
        public async Task<IActionResult> CreateAsync([FromBody]CreateTagRequest createTagRequest)
        {
            var command = createTagRequest.Adapt<CreateTags>();
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            var tags = operationResult.Value.Select(v => new TagResponse {Id = v.Id.Value, Name = v.Name });
            return CreatedAtAction(nameof(GetByIdsAsync), new { ids = String.Join("|", tags.Select(t => t.Id).ToList()) }, null);
        }

        [HttpGet]
        [Route(ApiRoutes.Tag.GetAll)]
        public async Task<IActionResult> GetAllAsync()
        {
            var command = new GetAllTags();
            var operationResult = await _mediator.Send(command);
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            var tags = operationResult.Value.Select(s => s.Adapt<TagResponse>());
            return Ok(tags);
        }
         
        [HttpGet]
        [Route(ApiRoutes.Tag.GetByIds)]
        public async Task<IActionResult> GetByIdsAsync(string ids)
        {
            if (string.IsNullOrWhiteSpace(ids))
            {
                return this.BadRequestWithErrors(new List<Exception>() { new Exception("Ids cannot be empy") });
            }
            var _ids = new List<string>(ids.Split("|")).Select(i => Guid.Parse(i));
            var command = new LightNote.Application.BusinessLogic.Tags.Queries.GetTagsByIds { TagIds = _ids };
            var operationResult = await _mediator.Send(command);
            // TODO: Test
            if (!operationResult.IsSuccess)
            {
                return this.BadRequestWithErrors(operationResult.Exceptions);
            }
            var tags = operationResult.Value.Select(s => s.Adapt<TagResponse>());
            return Ok(tags);
        }
    }
}

