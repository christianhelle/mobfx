using System;

namespace ChristianHelle.Framework.WindowsMobile.Patterns
{
    /// <summary>
    /// Guard Pattern implementation for abstracting over trvial sanity checking
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Checks if an object is NULL
        /// </summary>
        /// <param name="argument">Object to check</param>
        /// <param name="name">Name of the argument to use when throwing an <see cref="ArgumentNullException"/></param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the argument is NULL
        /// </exception>
        public static void ArgumentNull(object argument, string name)
        {
            if (argument == null)
                throw new ArgumentNullException("name");
        }

        /// <summary>
        /// Checks if a String is NULL or Empty
        /// </summary>
        /// <param name="argument">Object to check</param>
        /// <param name="name">Name of the argument to use when throwing an <see cref="ArgumentNullException"/></param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the argument is NULL
        /// </exception>
        public static void ArgumentNullOrEmpty(string argument, string name)
        {
            if (string.IsNullOrEmpty(argument))
                throw new ArgumentNullException("name");
        }
    }
}
