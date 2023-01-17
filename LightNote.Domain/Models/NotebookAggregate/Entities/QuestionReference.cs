using System;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;

namespace LightNote.Domain.Models.NotebookAggregate.Entities
{
	public class QuestionReference
	{
		public QuestionId QuestionId { get; set; }
		public Question Question { get; set; }

		public ReferenceId ReferenceId{get;set;}
        public Reference Reference { get; set; }
    }
}

