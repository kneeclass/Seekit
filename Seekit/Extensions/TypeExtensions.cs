using System;

namespace Seekit.Extensions {
    public static class TypeExtensions {
        public static string JsonNetFormat(this Type type)
        {
            if (!string.IsNullOrEmpty(type.AssemblyQualifiedName)) {
                var endOfAssemblyIndex = type.AssemblyQualifiedName.IndexOf(", Version");
                return type.AssemblyQualifiedName.Substring(0, endOfAssemblyIndex);
            }
                return type.FullName;
        }
    }
}
