using LightNote.Application.BusinessLogic.References.Commands;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using MediatR;

namespace LightNote.Application.BusinessLogic.References.CommandHandlers
{
    public class SetTagsToReferenceHandler : IRequestHandler<SetTagsToReference, OperationResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SetTagsToReferenceHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<bool>> Handle(SetTagsToReference request, CancellationToken cancellationToken)
        {
            var tags = await _unitOfWork.TagRepository.Get(t => request.TagIds.Contains(t.Id.Value));
            var reference = await _unitOfWork.ReferenceRepository.GetByID(request.ReferenceId);
            if (reference == null)
            {
                return OperationResult<bool>.CreateFailure(new[] { new ResourceNotFoundException(nameof(Reference)) });
            }
            if (reference.UserProfileId.Value != request.UserProfileId)
            {
                return OperationResult<bool>.CreateFailure(new[] { new AccessIsNotAuthorizedException() });
            }
            reference.SetTags(tags);
            try
            {
                _unitOfWork.ReferenceRepository.Update(reference);
                await _unitOfWork.SaveAsync();
                return OperationResult<bool>.CreateSuccess(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.CreateFailure(new[] { ex });
            }

        }
    }
}