using System;
using System.IO;
using System.Reflection;

namespace ChristianHelle.Framework.WindowsMobile.IO
{
    /// <summary>
    /// Provides directory features not supported by the <see cref="System.IO.Directory"/>
    /// </summary>
    /// <remarks>
    /// The methods of this class can be accessed from <see cref="DirectoryEx"/> or as
    /// extension methods to <see cref="Directory"/>
    /// </remarks>
    public static class DirectoryEx
    {
        /// <summary>
        /// Gets the current working directory of the application.
        /// </summary>
        /// <returns>
        /// A string containing the path of the applications working directory.
        /// </returns>
        public static string GetCurrentDirectory()
        {
            if (Environment.OSVersion.Platform == PlatformID.WinCE)
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);

            //return Directory.GetCurrentDirectory();
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
        }

        /// <summary>
        /// Creates all directories and subdirectories as specified by path.
        /// </summary>
        /// <param name="path">The directory path to create</param>
        /// <param name="hidden">Set to <c>true</c> to set the directory attributes to hidden</param>
        /// <param name="readOnly">Set to <c>true</c> to set the directory permission to read-only</param>
        /// <returns>The directory path to create</returns>
        /// <exception cref="System.IO.IOException">
        /// The directory specified by path is read-only
        /// </exception>
        /// <exception cref="System.UnauthorizedAccessException">
        /// The caller does not have the required permission
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// path is a zero-length string, contains only white space, or contains one
        /// or more invalid characters as defined by System.IO.Path.InvalidPathChars.
        /// -or- path is prefixed with, or contains only a colon character (:).
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// path is null
        /// </exception>
        /// <exception cref="System.IO.PathTooLongException">
        /// The specified path, file name, or both exceed the system-defined maximum
        /// length. For example, on Windows-based platforms, paths must be less than
        /// 248 characters and file names must be less than 260 characters.
        /// </exception>'
        /// <exception cref="System.IO.DirectoryNotFoundException">
        /// The specified path is invalid (for example, it is on an unmapped drive).
        /// </exception>
        /// <exception cref="System.NotSupportedException">
        /// Path contains a colon character (:) that is not part of a drive label ("C:\")
        /// </exception>
        public static DirectoryInfo CreateDirectory(string path, bool hidden, bool readOnly)
        {
            DirectoryInfo info = Directory.CreateDirectory(path);
            if (hidden) info.Attributes = FileAttributes.Hidden;
            if (readOnly) info.Attributes = info.Attributes | FileAttributes.ReadOnly;

            return info;
        }
    }
}