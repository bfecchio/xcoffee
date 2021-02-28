using System;
using System.Collections.Generic;

namespace XCoffee.Core.Helpers
{
    public static class HashCodeHelper
    {
        #region Public Methods

        public static int GetHashCode(Type obj, IEnumerable<object> properties)
        {
            unchecked
            {
                var hash = obj.GetHashCode();
                foreach (var prop in properties)
                    hash = (hash ^ 93) + (prop != null ? prop.GetHashCode() : 0);

                return hash;
            }
        }

        public static int GetHashCode(Type obj, object property)
            => (obj.GetHashCode() ^ 93) + property.GetHashCode();

        #endregion
    }
}
