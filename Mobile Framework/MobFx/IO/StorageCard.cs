using System.Collections.Generic;
using System.IO;

namespace ChristianHelle.Framework.WindowsMobile.IO
{
    /// <summary>
    /// Helper class for storage card enumeration
    /// </summary>
    public static class StorageCard
    {
        /// <summary>
        /// Enumerates the available storage cards into a string array
        /// </summary>
        /// <returns>string array of available storage cards</returns>
        public static string[] GetStorageCards()
        {
            var list = new List<string>();
            var root = new DirectoryInfo(@"\");
            foreach (DirectoryInfo di in root.GetDirectories()) {
                if (FileAttributes.Temporary == (di.Attributes & FileAttributes.Temporary))
                    list.Add(di.Name);
            }
            return list.ToArray();
        }
    }
}