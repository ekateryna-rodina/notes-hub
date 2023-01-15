﻿using System;
using LightNote.Domain.Models.Common.BaseModels;

namespace LightNote.Domain.Models.Notebook.ValueObjects
{
    public sealed class TagId : ValueObject
    {
        public Guid Value { get; init; }
        private TagId(Guid value)
        {
            Value = value;
        }
        public static TagId Create()
        {
            return new TagId(Guid.NewGuid());
        }

        public override IEnumerable<object> GetValues()
        {
            yield return Value;
        }
    }
}

