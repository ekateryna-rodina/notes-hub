using System;
using System.Collections.ObjectModel;
using LightNote.Application.BusinessLogic.Tags.Commands;
using LightNote.Application.Helpers;
using LightNote.Dal;
using LightNote.Dal.Contracts;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using MediatR;

namespace LightNote.Application.BusinessLogic.Tags.CommandHandlers
{
    public class CreateTagsHandler : IRequestHandler<CreateTags, OperationResult<IEnumerable<Tag>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTagsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<IEnumerable<Tag>>> Handle(CreateTags request, CancellationToken cancellationToken)
        {
            var tags = request.Names.Select(r => Tag.Create(r)).ToList();
            try
            {
                _unitOfWork.TagRepository.InsertMany(tags);
                await _unitOfWork.SaveAsync();
                return OperationResult<IEnumerable<Tag>>.CreateSuccess(tags);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<Tag>>.CreateFailure(new[] { ex });
            }
        }
    }
}

