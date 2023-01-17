using System;
using LightNote.Domain.Models.Common.BaseModels;

namespace LightNote.Domain.Models.NotebookAggregate.ValueObjects
{
    public sealed class NotebookId : ValueObject
    {
        public Guid Value { get; }
        private NotebookId(Guid value)
        {
            Value = value;
        }
        private NotebookId() { }
        public static NotebookId Create(Guid? id = null)
        {
            return new NotebookId(id ?? Guid.NewGuid());
        }
        public override IEnumerable<object> GetValues()
        {
            yield return Value;
        }
    }
}

