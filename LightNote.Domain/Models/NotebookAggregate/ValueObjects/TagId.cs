using System;
using LightNote.Domain.Models.Common.BaseModels;

namespace LightNote.Domain.Models.NotebookAggregate.ValueObjects
{
    public sealed class TagId : ValueObject
    {
        public Guid Value { get; init; }
        private TagId(Guid value)
        {
            Value = value;
        }
        private TagId() { }
        public static TagId Create(Guid? id = null)
        {
            return new TagId(id ?? Guid.NewGuid());
        }

        public override IEnumerable<object> GetValues()
        {
            yield return Value;
        }
    }
}

