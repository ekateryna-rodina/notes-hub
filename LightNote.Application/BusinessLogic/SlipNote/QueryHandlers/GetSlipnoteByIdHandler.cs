using LightNote.Application.BusinessLogic.SlipNote.Queries;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using MediatR;

namespace LightNote.Application.BusinessLogic.SlipNote.QueryHandlers
{
    public class GetSlipnoteByIdHandler : IRequestHandler<GetSlipnoteById, OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.SlipNote?>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSlipnoteByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.SlipNote?>> Handle(GetSlipnoteById request, CancellationToken cancellationToken)
        {
            var slipNote = await _unitOfWork.SlipNoteRepository.GetByID(request.Id);
            if (slipNote == null)
            {
                return OperationResult<Domain.Models.NotebookAggregate.Entities.SlipNote?>.CreateFailure(new List<Exception>() { new ResourceNotFoundException(nameof(SlipNote)) });
            }
            if (slipNote!.UserProfileId.Value != request.UserProfileId)
            {
                return OperationResult<Domain.Models.NotebookAggregate.Entities.SlipNote?>.CreateFailure(new List<Exception>() { new AccessIsNotAuthorizedException() });
            }
            return OperationResult<Domain.Models.NotebookAggregate.Entities.SlipNote?>.CreateSuccess(slipNote);
        }
    }
}