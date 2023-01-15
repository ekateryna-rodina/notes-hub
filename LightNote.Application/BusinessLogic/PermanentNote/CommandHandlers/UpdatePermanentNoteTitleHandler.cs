using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightNote.Application.BusinessLogic.PermanentNote.Commands;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using MediatR;

namespace LightNote.Application.BusinessLogic.PermanentNote.CommandHandlers
{
    public class UpdatePermanentNoteTitleHandler : IRequestHandler<UpdatePermanentNoteTitle, OperationResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePermanentNoteTitleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<bool>> Handle(UpdatePermanentNoteTitle request, CancellationToken cancellationToken)
        {
            var permanentNote = await _unitOfWork.PermanentNoteRepository.GetByID(request.Id);
            if (permanentNote == null)
            {
                return OperationResult<bool>.CreateFailure(new[] { new ResourceNotFoundException(nameof(PermanentNote)) });
            }
            if (permanentNote.UserProfileId.Value != request.UserProfileId)
            {
                return OperationResult<bool>.CreateFailure(new[] { new AccessIsNotAuthorizedException() });
            }
            permanentNote.UpdateTitle(request.Title);
            try
            {
                _unitOfWork.PermanentNoteRepository.Update(permanentNote);
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