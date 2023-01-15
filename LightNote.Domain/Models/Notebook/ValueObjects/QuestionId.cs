using System;
using LightNote.Domain.Models.Common.BaseModels;

namespace LightNote.Domain.Models.Notebook.ValueObjects
{
    public sealed class QuestionId : ValueObject
    {
        public Guid Value { get; init; }
        private QuestionId(Guid value)
        {
            Value = value;
        }
        public static QuestionId Create()
        {
            return new QuestionId(Guid.NewGuid());
        }

        public override IEnumerable<object> GetValues()
        {
            yield return Value;
        }
    }
}

