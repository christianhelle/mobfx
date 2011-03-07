using System;
namespace ChristianHelle.Framework.WindowsMobile.Patterns
{
    /// <summary>
    /// Factory class for retrieving instance of singleton objects
    /// </summary>
    /// <typeparam name="T">T of the singleton object</typeparam>
    public static class Singleton<T> where T : class, new()
    {
        private static readonly object staticLock = new object();
        private static T instance;

        /// <summary>
        /// Retrieves the static instance of the specified type parameter
        /// </summary>
        /// <returns>Returns the static of the specified type parameter</returns>
        public static T GetInstance()
        {
            lock (staticLock)
            {
                if (instance == null)
                    instance = Activator.CreateInstance<T>();
                return instance;
            }
        }
    }
}