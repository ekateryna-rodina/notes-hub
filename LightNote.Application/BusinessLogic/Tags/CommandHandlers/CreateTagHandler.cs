using System;
using LightNote.Application.BusinessLogic.Tags.Commands;
using LightNote.Application.Helpers;
using LightNote.Dal;
using LightNote.Dal.Contracts;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using MediatR;

namespace LightNote.Application.BusinessLogic.Tags.CommandHandlers
{
    public class CreateTagHandler : IRequestHandler<CreateTag, OperationResult<Tag>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTagHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<Tag>> Handle(CreateTag request, CancellationToken cancellationToken)
        {
            var tag = Tag.Create(request.Name);
            try
            {
                _unitOfWork.TagRepository.Insert(tag);
                await _unitOfWork.SaveAsync();
                return OperationResult<Tag>.CreateSuccess(tag);
            }
            catch (Exception ex)
            {
                return OperationResult<Tag>.CreateFailure(new[] { ex });
            }
        }
    }
}

