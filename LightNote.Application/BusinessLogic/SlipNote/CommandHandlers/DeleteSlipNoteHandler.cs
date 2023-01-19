using LightNote.Application.BusinessLogic.SlipNote.Commands;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using MediatR;

namespace LightNote.Application.BusinessLogic.SlipNote.CommandHandlers
{
    public class DeleteSlipNoteHandler : IRequestHandler<DeleteSlipNote, OperationResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSlipNoteHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<bool>> Handle(DeleteSlipNote request, CancellationToken cancellationToken)
        {

            var slipnote = await _unitOfWork.SlipNoteRepository.GetByID(request.Id);
            if (slipnote == null)
            {
                return OperationResult<bool>.CreateFailure(new[] { new ResourceNotFoundException(nameof(SlipNote)) });
            }
            if (slipnote.UserProfileId.Value != request.UserProfileId)
            {
                return OperationResult<bool>.CreateFailure(new[] { new AccessIsNotAuthorizedException() });
            }
            try
            {
                _unitOfWork.SlipNoteRepository.Delete(slipnote);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.CreateFailure(new[] { ex });
            }
            return OperationResult<bool>.CreateSuccess(true);
        }
    }
}