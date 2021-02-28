using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using XCoffee.Core.Helpers;

namespace XCoffee.Core.Domain
{
    public abstract class Enumeration<TKey> : IDomainEntity
        where TKey : struct
    {
        #region Properties

        public TKey Id { get; protected set; }
        public string Name { get; private set; }

        #endregion

        #region Constructors

        protected Enumeration(string name)
        {
            Guard.AssertArgumentNotEmpty(name, "The name is required.");
            Name = name;
        }

        #endregion

        #region Behaviours

        public override string ToString() => Name;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (ReferenceEquals(null, obj)) return false;
            if (GetType() != obj.GetType()) return false;

            var compareTo = obj as Enumeration<TKey>;
            return Id.Equals(compareTo.Id);
        }

        public override int GetHashCode() => HashCodeHelper.GetHashCode(GetType(), Id);

        #endregion

        #region Methods

        public static IEnumerable<T> GetAll<T>() where T : Enumeration<TKey>
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public static T FromValue<T>(TKey value) where T : Enumeration<TKey>
        {
            var matchingItem = Parse<T, TKey>(value, "value", item => item.Id.Equals(value));
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration<TKey>
        {
            var matchingItem = Parse<T, string>(displayName, "display name", item => item.Name == displayName);
            return matchingItem;
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration<TKey>
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);
            if (matchingItem == null)
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

            return matchingItem;
        }

        #endregion
    }
}
