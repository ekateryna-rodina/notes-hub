using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightNote.Application.BusinessLogic.Question.Commands;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using MediatR;

namespace LightNote.Application.BusinessLogic.Question.CommandHandlers
{
    public class CreateQuestionHandler : IRequestHandler<CreateQuestion, OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.Question>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateQuestionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<Domain.Models.NotebookAggregate.Entities.Question>> Handle(CreateQuestion request, CancellationToken cancellationToken)
        {
            var notebook = await _unitOfWork.NotebookRepository.GetById(NotebookId.Create(request.NotebookId));
            if (notebook == null)
            {
                return OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.Question>.CreateFailure(new[] { new ResourceNotFoundException(nameof(LightNote.Domain.Models.NotebookAggregate.Notebook)) });
            }
            if (notebook.UserProfileId.Value != request.UserProfileId)
            {
                return OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.Question>.CreateFailure(new[] { new AccessIsNotAuthorizedException() });
            }

            var question = Domain.Models.NotebookAggregate.Entities.Question
                    .Create(request.Content, request.NotebookId,
                    request.UserProfileId, request.InsightId, request.PermanentNoteId);
            notebook.AddQuestion(question);
            try
            {
                _unitOfWork.QuestionRepository.Insert(question);
                _unitOfWork.NotebookRepository.Update(notebook);
                await _unitOfWork.SaveAsync();
                return OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.Question>.CreateSuccess(question);
            }
            catch (Exception ex)
            {
                return OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.Question>.CreateFailure(new[] { ex });
            }
        }
    }
}