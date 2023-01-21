using System;
using LightNote.Domain.Exceptions;
using LightNote.Domain.Models.Common.BaseModels;

namespace LightNote.Domain.Models.Common.ValueObjects
{
    public sealed class Title : ValueObject
    {
        public string Value { get; set; }
        private Title(string value)
        {
            var min = 5;
            var max = 50;
            if (value.Length < min || value.Length > max)
            {
                throw new InvalidLengthException(nameof(Title), min, max);
            }
            Value = value;
        }

        public static Title Create(string value)
        {
            return new Title(value);
        }

        public override IEnumerable<object> GetValues()
        {
            yield return Value;
        }
    }
}

