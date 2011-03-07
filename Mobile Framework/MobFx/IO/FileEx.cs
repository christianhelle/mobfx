using System.IO;

namespace ChristianHelle.Framework.WindowsMobile.IO
{

    ///<summary>
    /// The FileEx class implements utility methods extending on the <see cref="File"/> class in the 
    /// .NET Compact Framework
    ///</summary>
    public static class FileEx
    {

        /// <summary>
        /// Returns the file system path to the currently executing assembly.
        /// </summary>
        /// <returns>Path to executable</returns>
        public static string GetExecutablePath()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
        }
    }
}


