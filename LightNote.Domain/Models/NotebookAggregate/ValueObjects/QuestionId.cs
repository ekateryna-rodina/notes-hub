using System;
using LightNote.Domain.Models.Common.BaseModels;

namespace LightNote.Domain.Models.NotebookAggregate.ValueObjects
{
    public sealed class QuestionId : ValueObject
    {
        public Guid Value { get; init; }
        private QuestionId(Guid value)
        {
            Value = value;
        }
        private QuestionId() { }
        public static QuestionId Create(Guid?id = null)
        {
            return new QuestionId(id ?? Guid.NewGuid());
        }

        public override IEnumerable<object> GetValues()
        {
            yield return Value;
        }
    }
}

