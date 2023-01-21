using System;
using LightNote.Application.BusinessLogic.References.Commands;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Dal;
using LightNote.Dal.Contracts;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using MediatR;

namespace LightNote.Application.BusinessLogic.References.CommandHandlers
{
    public class CreateReferenceHandler : IRequestHandler<CreateReference, OperationResult<Reference>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateReferenceHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<Reference>> Handle(CreateReference request, CancellationToken cancellationToken)
        {
            var notebook = await _unitOfWork.NotebookRepository.GetById(NotebookId.Create(request.NotebookId));
            if (notebook == null)
            {
                return OperationResult<Reference>.CreateFailure(new[] { new ResourceNotFoundException("Noteboook is not found") });
            }
            if (notebook.UserProfileId.Value != request.UserProfileId)
            {
                return OperationResult<Reference>.CreateFailure(new[] { new AccessIsNotAuthorizedException() });
            }
            var tags = await _unitOfWork.TagRepository.Get(t => request.TagIds.Contains(t.Id.Value));
            if (!tags.Any())
            {
                return OperationResult<Reference>.CreateFailure(new[] { new EntityIsRequiredException(nameof(Tag)) });
            }

            var reference = Reference.Create(request.Name, request.IsLink, request.UserProfileId, request.NotebookId, tags);
            notebook.AddReference(reference);
            try
            {
                _unitOfWork.ReferenceRepository.Insert(reference);
                _unitOfWork.NotebookRepository.Update(notebook);
                await _unitOfWork.SaveAsync();
                return OperationResult<Reference>.CreateSuccess(reference);
            }
            catch (Exception ex)
            {
                return OperationResult<Reference>.CreateFailure(new[] { ex });
            }
        }
    }
}

