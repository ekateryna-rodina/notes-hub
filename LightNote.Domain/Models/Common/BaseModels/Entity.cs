using System;
namespace LightNote.Domain.Models.Common.BaseModels
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>> where TId : notnull
    {
        private Guid id;

        public TId Id { get; protected set; }
        protected Entity(TId id)
        {
            Id = id;
        }

        protected Entity()
        {
        }

        public override bool Equals(object obj)
        {
            return obj is Entity<TId> entity && Id.Equals(entity.Id);
        }

        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            return Equals(left, right);
        }
        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !Equals(left, right);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public bool Equals(Entity<TId> other)
        {
            return Equals((object) other);
        }
    }
}

