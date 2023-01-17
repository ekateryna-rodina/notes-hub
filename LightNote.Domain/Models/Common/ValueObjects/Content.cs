using System;
using LightNote.Domain.Exceptions;
using LightNote.Domain.Models.Common.BaseModels;

namespace LightNote.Domain.Models.Common.ValueObjects
{
    public sealed class Content : ValueObject
    {
        public string Value { get; set; }
        private Content(string value)
        {
            var min = 5;
            var max = 150;
            if (value.Length < min || value.Length > max)
            {
                throw new InvalidLengthException(nameof(Content), min, max);
            }
            Value = value;
        }

        public static Content Create(string value)
        {
            return new Content(value);
        }

        public override IEnumerable<object> GetValues()
        {
            yield return Value;
        }
    }
}

