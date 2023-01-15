using System;
using LightNote.Application.BusinessLogic.Tags.Queries;
using LightNote.Application.Helpers;
using LightNote.Dal;
using LightNote.Dal.Contracts;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LightNote.Application.BusinessLogic.Tags.QueryHandlers
{
    public class GetAllTagsHandler : IRequestHandler<GetAllTags, OperationResult<IEnumerable<Tag>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTagsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<IEnumerable<Tag>>> Handle(GetAllTags request, CancellationToken cancellationToken)
        {
            var tags = await _unitOfWork.TagRepository.Get();
            return OperationResult<IEnumerable<Tag>>.CreateSuccess(tags);
        }
    }
}

