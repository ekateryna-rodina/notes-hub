using System;
using LightNote.Application.BusinessLogic.Tags.Queries;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using MediatR;

namespace LightNote.Application.BusinessLogic.Tags.QueryHandlers
{
	public class GetTagsByIdsHandler : IRequestHandler<GetTagsByIds, OperationResult<IEnumerable<Tag>>>
	{
        private readonly IUnitOfWork _unitOfWork;

        public GetTagsByIdsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<IEnumerable<Tag>>> Handle(GetTagsByIds request, CancellationToken cancellationToken)
        {
            var tags = new List<Tag>();
            foreach (var id in request.TagIds) {
                var searchTagId = TagId.Create(id);
                var tag = await _unitOfWork.TagRepository.Get(t => searchTagId == t.Id);
                tags.AddRange(tag);
            }
            return OperationResult<IEnumerable<Tag>>.CreateSuccess(tags);
        }
    }
}

