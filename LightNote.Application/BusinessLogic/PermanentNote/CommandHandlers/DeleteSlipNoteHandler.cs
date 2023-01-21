using LightNote.Application.BusinessLogic.PermanentNote.Commands;
using LightNote.Application.BusinessLogic.SlipNote.Commands;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using MediatR;

namespace LightNote.Application.BusinessLogic.SlipNote.CommandHandlers
{
    public class DeletePermanentNoteHandler : IRequestHandler<DeletePermanentNote, OperationResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePermanentNoteHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<bool>> Handle(DeletePermanentNote request, CancellationToken cancellationToken)
        {

            var permanentNote = await _unitOfWork.PermanentNoteRepository.GetById(PermanentNoteId.Create(request.Id));
            if (permanentNote == null)
            {
                return OperationResult<bool>.CreateFailure(new[] { new ResourceNotFoundException(nameof(PermanentNote)) });
            }
            if (permanentNote.UserProfileId.Value != request.UserProfileId)
            {
                return OperationResult<bool>.CreateFailure(new[] { new AccessIsNotAuthorizedException() });
            }
            try
            {
                _unitOfWork.PermanentNoteRepository.Delete(permanentNote);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.CreateFailure(new[] { ex });
            }
            return OperationResult<bool>.CreateSuccess(true);
        }
    }
}