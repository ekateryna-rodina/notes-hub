using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightNote.Application.BusinessLogic.Question.Commands;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using MediatR;

namespace LightNote.Application.BusinessLogic.Question.CommandHandlers
{
    public class UpdateQuestionHandler : IRequestHandler<UpdateQuestion, OperationResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateQuestionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<bool>> Handle(UpdateQuestion request, CancellationToken cancellationToken)
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
            question.UpdateContent(request.Content);
            try
            {
                _unitOfWork.QuestionRepository.Update(question);
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