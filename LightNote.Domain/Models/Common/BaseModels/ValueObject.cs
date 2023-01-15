using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightNote.Domain.Models.Common.BaseModels
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        public abstract IEnumerable<object> GetValues();
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }
            var valueObject = (ValueObject)obj;
            return GetValues().SequenceEqual(valueObject.GetValues());
        }
        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return Equals(left, right);
        }
        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !Equals(left, right);
        }
        public override int GetHashCode()
        {
            return GetValues().Select(x => x?.GetHashCode() ?? 0)
                .Aggregate((x, y) => x ^ y);
        }

        public bool Equals(ValueObject other)
        {
            return Equals((object)other);
        }
    }
}