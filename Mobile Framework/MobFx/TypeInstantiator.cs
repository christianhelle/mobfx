using System;

namespace ChristianHelle.Framework.WindowsMobile
{
    /// <summary>
    /// Helper class for instantiating interface implementations
    /// </summary>
    /// <typeparam name="T">Interface to use</typeparam>
    public static class TypeInstantiator<T>
    {
        /// <summary>
        /// Loads a type (Namespace.Class, Assembly) as the specified type parameter
        /// </summary>
        /// <param name="typeName">Full Type name to load (Namespace.Class, Assembly)</param>
        /// <returns>Returns an instance of the specified type parameter</returns>
        public static T LoadType(string typeName)
        {
            var type = Type.GetType(typeName);
            if (type == null)
                throw new TypeLoadException("Unable to get the Type of" + typeName);

            return LoadType(type);
        }

        /// <summary>
        /// Loads a type (Namespace.Class, Assembly) as the specified type parameter
        /// </summary>
        /// <param name="type">Type to load</param>
        /// <returns>Returns an instance of the specified type parameter</returns>
        public static T LoadType(Type type)
        {
            return (T)Activator.CreateInstance(type);
        }

        /// <summary>
        /// Loads a type (Namespace.Class, Assembly) as the specified type parameter
        /// </summary>
        /// <returns>Returns an instance of the specified type parameter</returns>
        public static T LoadType()
        {
            return Activator.CreateInstance<T>();
        }
    }
}