using System;
using LightNote.Domain.Models.Common.BaseModels;

namespace LightNote.Domain.Models.NotebookAggregate.ValueObjects
{
    public sealed class ReferenceId : ValueObject
    {
        public Guid Value { get; init; }
        private ReferenceId(Guid value)
        {
            Value = value;
        }
        private ReferenceId() { }
        public static ReferenceId Create(Guid? id = null)
        {
            return new ReferenceId(id ?? Guid.NewGuid());
        }

        public override IEnumerable<object> GetValues()
        {
            yield return Value;
        }
    }
}

