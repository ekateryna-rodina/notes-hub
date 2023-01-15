using System;
using LightNote.Domain.Models.Common.BaseModels;

namespace LightNote.Domain.Models.Notebook.ValueObjects
{
    public sealed class NotebookId : ValueObject
    {
        public Guid Value { get; }
        private NotebookId(Guid value)
        {
            Value = value;
        }
        public static NotebookId Create()
        {
            return new NotebookId(Guid.NewGuid());
        }
        public override IEnumerable<object> GetValues()
        {
            yield return Value;
        }
    }
}

