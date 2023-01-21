using LightNote.Domain.Models.Common.BaseModels;

namespace LightNote.Domain.Models.UserProfileAggregate.ValueObjects
{
    public sealed class UserProfileId : ValueObject
    {
        public Guid Value { get; init; }
        private UserProfileId(Guid value)
        {
            Value = value;
        }
        private UserProfileId() { }
        public static UserProfileId Create(Guid? userProfileId = null)
        {
            return new UserProfileId(userProfileId ?? Guid.NewGuid());
        }

        public override IEnumerable<object> GetValues()
        {
            yield return Value;
        }
    }
}

