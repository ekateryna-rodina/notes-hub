using System;
using LightNote.Domain.Models.Common.BaseModels;
using LightNote.Domain.Models.Notebook.Entities;

namespace LightNote.Domain.Models.Notebook.ValueObjects
{
    public sealed class PermanentNoteId : ValueObject
    {
        public Guid Value { get; init; }
        private PermanentNoteId(Guid value)
        {
            Value = value;
        }
        public static PermanentNoteId Create()
        {
            return new PermanentNoteId(Guid.NewGuid());
        }

        public override IEnumerable<object> GetValues()
        {
            yield return Value;
        }
    }
}