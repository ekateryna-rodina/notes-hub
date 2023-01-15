using System;
using LightNote.Domain.Models.Common.BaseModels;

namespace LightNote.Domain.Models.Notebook.ValueObjects
{
    public sealed class SlipNoteId : ValueObject
    {
        public Guid Value { get; init; }
        private SlipNoteId(Guid value)
        {
            Value = value;
        }
        public static SlipNoteId Create()
        {
            return new SlipNoteId(Guid.NewGuid());
        }

        public override IEnumerable<object> GetValues()
        {
            yield return Value;
        }
    }
}

