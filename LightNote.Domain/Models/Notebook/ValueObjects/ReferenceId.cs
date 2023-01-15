using System;
using LightNote.Domain.Models.Common.BaseModels;

namespace LightNote.Domain.Models.Notebook.ValueObjects
{
    public sealed class ReferenceId : ValueObject
    {
        public Guid Value { get; init; }
        private ReferenceId(Guid value)
        {
            Value = value;
        }
        public static ReferenceId Create()
        {
            return new ReferenceId(Guid.NewGuid());
        }

        public override IEnumerable<object> GetValues()
        {
            yield return Value;
        }
    }
}

