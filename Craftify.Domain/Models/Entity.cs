using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Domain.Models
{
    public abstract class Entity<TId> (TId id) : IEquatable<Entity<TId>>
        where TId : notnull
    {
        public TId Id { get; protected set; } = id;

        public override bool Equals(object? obj)
        {
            return obj is Entity<TId> entity && Equals(entity.Id);
        }

        public bool Equals(Entity<TId>? other)
        {
            return Equals((object?)other);
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
    }
}
