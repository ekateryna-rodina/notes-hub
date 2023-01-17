using System;
using LightNote.Domain.Models.Common.BaseModels;

namespace LightNote.Domain.Models.NotebookAggregate.ValueObjects
{
    public sealed class SlipNoteId : ValueObject
    {
        public Guid Value { get; init; }
        private SlipNoteId(Guid value)
        {
            Value = value;
        }
        private SlipNoteId() { }
        public static SlipNoteId Create(Guid? id = null)
        {
            return new SlipNoteId(id ?? Guid.NewGuid());
        }

        public override IEnumerable<object> GetValues()
        {
            yield return Value;
        }
    }
}

