#region Imported Namespaces

using System;
using System.Diagnostics;
using System.IO;
using System.Text;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Diagnostics
{
    /// <summary>
    /// Base class for all file logging classes
    /// </summary>
    public class LogFile : EventLogBase
    {
        private readonly object instanceLock = new object();
        private string filename;

        /// <summary>
        /// Gets or Sets the file (full path) to use for error logging
        /// </summary>
        public virtual string Filename
        {
            get
            {
                if (string.IsNullOrEmpty(filename))
                    filename = GetDefaultFile();
                return filename;
            }
            set { filename = value; }
        }

        /// <summary>
        /// Retrieves the default log file to use
        /// </summary>
        /// <returns></returns>
        protected virtual string GetDefaultFile()
        {
            return LogFileRepository.GetLogFile("Log");
        }

        /// <summary>
        /// Prepares the Log file for writting
        /// </summary>
        /// <remarks>
        /// If the log file is larger than 10MB, 
        /// it will be deleted and a new log file with the same name is created
        /// </remarks>
        /// <returns>
        /// Returns an instance of <see cref="StreamWriter"/> to be used for writting to the log file
        /// </returns>
        protected StreamWriter PrepareLogFile()
        {
            if (File.Exists(Filename))
            {
                var fi = new FileInfo(Filename);
                if (fi.Length > 1000*1024)
                {
                    Debug.WriteLine("Log file is too large, renaming to [Log file]-[Date and time].txt");
                    var newFilename = Filename.Replace(".txt", DateTime.Now.ToString("yyyy-MM-dd-HH.mm.ss") + ".txt");
                    fi.MoveTo(newFilename);
                }
            }

            var sw = new StreamWriter(Filename, true, Encoding.UTF8);
            return sw;
        }

        /// <summary>
        /// Writes a line to the log file
        /// </summary>
        /// <param name="message">Text to log</param>
        public override void WriteLine(string message)
        {
            Debug.WriteLine(message);

            try
            {
                lock (instanceLock)
                {
                    using (var sw = PrepareLogFile())
                    {
                        sw.WriteLine(message);
                        sw.Close();
                    }
                }

                base.WriteLine(message);
            }
            catch (Exception e)
            {
                TraceException(e);
            }
        }

        /// <summary>
        /// Writes a line to the log file
        /// </summary>
        /// <param name="message">Text to log</param>
        public override void WriteLine(StringBuilder message)
        {
            WriteLine(message.ToString());
        }

        /// <summary>
        /// Displays the error to the attached trace listeners
        /// </summary>
        /// <param name="e"></param>
        protected void TraceException(Exception e)
        {
            Debug.WriteLine("Error saving error information to " + Filename);
            Debug.WriteLine(e.Message);
        }
    }
}