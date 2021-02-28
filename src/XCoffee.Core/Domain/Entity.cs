using System;
using System.Linq;
using System.Collections.Generic;
using XCoffee.Core.Helpers;

namespace XCoffee.Core.Domain
{
    public abstract class Entity : IEntity
    {
        #region Constructors

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region IEntity Members

        public Guid Id { get; set; }

        #endregion

        #region Abstract Methods

        protected abstract IEnumerable<object> GetIdentityComponents();

        #endregion

        #region Behaviours

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (ReferenceEquals(null, obj)) return false;
            if (GetType() != obj.GetType()) return false;

            var compareTo = obj as Entity;
            return GetIdentityComponents().SequenceEqual(compareTo.GetIdentityComponents());
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
            => !(a == b);

        public override int GetHashCode()
            => HashCodeHelper.GetHashCode(GetType(), GetIdentityComponents());

        public override string ToString()
            => $"{GetType().Name} [Id={Id}]";

        #endregion
    }
}
