using LightNote.Application.BusinessLogic.Insight.Commands;
using LightNote.Application.BusinessLogic.Question.Commands;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using MediatR;

namespace LightNote.Application.BusinessLogic.Question.CommandHandlers
{
    public class DeleteQuestionHandler : IRequestHandler<DeleteQuestion, OperationResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteQuestionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<bool>> Handle(DeleteQuestion request, CancellationToken cancellationToken)
        {

            var question = await _unitOfWork.QuestionRepository.GetByID(request.Id);
            if (question == null)
            {
                return OperationResult<bool>.CreateFailure(new[] { new ResourceNotFoundException(nameof(Question)) });
            }
            if (question.UserProfileId.Value != request.UserProfileId)
            {
                return OperationResult<bool>.CreateFailure(new[] { new AccessIsNotAuthorizedException() });
            }
            try
            {
                _unitOfWork.QuestionRepository.Delete(question);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.CreateFailure(new[] { ex });
            }
            return OperationResult<bool>.CreateSuccess(true);
        }
    }
}