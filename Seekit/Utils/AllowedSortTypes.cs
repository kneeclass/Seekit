using System;
using System.Linq;

namespace Seekit.Utils {
    public class AllowedSortTypes<T> {
        private static readonly Type[] AllowedTypes = { typeof(Int32), typeof(string), typeof(DateTime) };
        public static void ThrowIfNotSortableType(string propertyName) {
            var type = typeof (T);
            var property = type.GetProperty(propertyName);

            if (!AllowedTypes.Any(x => x == property.PropertyType)) {
                throw new NotSupportedException(string.Format("The property: '{0}' of type: '{1}' can not be used to sort the result",propertyName,property.PropertyType.FullName));
            }
                

        }

    }
}
