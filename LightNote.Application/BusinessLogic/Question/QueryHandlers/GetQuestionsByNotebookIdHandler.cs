using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightNote.Application.BusinessLogic.Question.Queries;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using MediatR;

namespace LightNote.Application.BusinessLogic.Question.QueryHandlers
{
    public class GetQuestionsByNotebookIdHandler : IRequestHandler<GetQuestionsByNotebookId, OperationResult<IEnumerable<LightNote.Domain.Models.NotebookAggregate.Entities.Question>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetQuestionsByNotebookIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<IEnumerable<Domain.Models.NotebookAggregate.Entities.Question>>> Handle(GetQuestionsByNotebookId request, CancellationToken cancellationToken)
        {
            var notebook = await _unitOfWork.NotebookRepository.GetByID(request.NotebookId);
            if (notebook == null)
            {
                return OperationResult<IEnumerable<Domain.Models.NotebookAggregate.Entities.Question>>.CreateFailure(new[] { new ResourceNotFoundException(nameof(Notebook)) });
            }
            if (notebook.UserProfileId.Value != request.UserProfileId)
            {
                return OperationResult<IEnumerable<LightNote.Domain.Models.NotebookAggregate.Entities.Question>>.CreateFailure(new[] { new AccessIsNotAuthorizedException() });
            }
            var questions = await _unitOfWork.QuestionRepository.Get(q => q.NotebookId == notebook.Id);
            return OperationResult<IEnumerable<LightNote.Domain.Models.NotebookAggregate.Entities.Question>>.CreateSuccess(questions);
        }
    }
}