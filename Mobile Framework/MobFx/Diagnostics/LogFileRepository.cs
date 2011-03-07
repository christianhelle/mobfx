#region Imported Namespaces

using System;
using System.IO;
using ChristianHelle.Framework.WindowsMobile.IO;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Diagnostics
{
    /// <summary>
    /// Helper class for retrieving the Log file directory
    /// </summary>
    public static class LogFileRepository
    {
        /// <summary>
        /// Default Timestamp format for log files (YEAR-MONTH-DAY_HOURS.MINUTES.SECONDS)
        /// </summary>
        public const string TIMESTAMP_FORMAT = "yyyy-MM-dd_HH.mm.ss";

        /// <summary>
        /// Gets the location of the default log file repository
        /// </summary>
        /// <returns>Returns the path to the log file repository</returns>
        public static string GetLocation()
        {
            var directory = Path.Combine(DirectoryEx.GetCurrentDirectory(), "Log");
            if (!Directory.Exists(directory))
                DirectoryEx.CreateDirectory(directory, true, false);
            return directory;
        }

        /// <summary>
        /// Gets the location of the default log file repository
        /// </summary>
        /// <param name="name">Name of the file</param>
        /// <returns>Returns the path to the log file repository</returns>
        public static string GetLogFile(string name)
        {
            var logFile = string.Format("{0}\\{1}-{2}.txt",
                                        GetLocation(),
                                        name,
                                        DateTime.Now.ToString(TIMESTAMP_FORMAT));
            return logFile;
        }
    }
}