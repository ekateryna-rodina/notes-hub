using LightNote.Application.BusinessLogic.SlipNote.Commands;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using MediatR;

namespace LightNote.Application.BusinessLogic.SlipNote.CommandHandlers
{
    public class UpdateSlipNoteHandler : IRequestHandler<UpdateSlipNoteContent, OperationResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSlipNoteHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<bool>> Handle(UpdateSlipNoteContent request, CancellationToken cancellationToken)
        {
            var slipNote = await _unitOfWork.SlipNoteRepository.GetByID(request.Id);
            if (slipNote == null)
            {
                return OperationResult<bool>.CreateFailure(new[] { new ResourceNotFoundException(nameof(SlipNote)) });
            }
            if (slipNote.UserProfileId.Value != request.UserProfileId)
            {
                return OperationResult<bool>.CreateFailure(new[] { new AccessIsNotAuthorizedException() });
            }
            slipNote.UpdateContent(request.Content);
            try
            {
                _unitOfWork.SlipNoteRepository.Update(slipNote);
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