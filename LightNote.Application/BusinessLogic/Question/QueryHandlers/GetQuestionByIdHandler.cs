using LightNote.Application.BusinessLogic.Question.Queries;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using MediatR;

namespace LightNote.Application.BusinessLogic.Question.QueryHandlers
{
    public class GetQuestionByIdHandler : IRequestHandler<GetQuestionById, OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.Question?>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetQuestionByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.Question?>> Handle(GetQuestionById request, CancellationToken cancellationToken)
        {
            var question = await _unitOfWork.QuestionRepository.GetByID(request.Id);
            if (question == null)
            {
                return OperationResult<Domain.Models.NotebookAggregate.Entities.Question?>.CreateFailure(new List<Exception>() { new ResourceNotFoundException(nameof(Question)) });
            }
            if (question!.UserProfileId.Value != request.UserProfileId)
            {
                return OperationResult<Domain.Models.NotebookAggregate.Entities.Question?>.CreateFailure(new List<Exception>() { new AccessIsNotAuthorizedException() });
            }
            return OperationResult<Domain.Models.NotebookAggregate.Entities.Question?>.CreateSuccess(question);
        }
    }
}