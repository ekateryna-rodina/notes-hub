using System;
using LightNote.Domain.Models.Common.BaseModels;

namespace LightNote.Domain.Models.Notebook.ValueObjects
{
    public sealed class InsightId : ValueObject
    {
        public Guid Value { get; init; }
        private InsightId(Guid value)
        {
            Value = value;
        }
        public static InsightId Create(Guid? id = null)
        {
            return new InsightId(id ?? Guid.NewGuid());
        }

        public override IEnumerable<object> GetValues()
        {
            yield return Value;
        }
    }
}

