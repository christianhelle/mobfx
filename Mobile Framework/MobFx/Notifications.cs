using System;
using System.Collections.Generic;
using System.IO;
using ChristianHelle.Framework.WindowsMobile.Configuration;
using ChristianHelle.Framework.WindowsMobile.Diagnostics;
using ChristianHelle.Framework.WindowsMobile.IO;
using ChristianHelle.Framework.WindowsMobile.Media;

namespace ChristianHelle.Framework.WindowsMobile
{
    /// <summary>
    /// Sound notifications
    /// </summary>
    public static class Notifications
    {
        private const string APP_SETTINGS_KEY = "SoundRepository";
        private static readonly string errorFile;
        private static readonly string warningFile;
        private static readonly string questionFile;
        private static readonly string soundRepository;

        static Notifications()
        {
            var appSettings = ConfigurationManager.AppSettings.AllKeys;
            if (appSettings != null && appSettings.Length > 0)
            {
                var list = new List<string>(appSettings);
                if (list.Contains(APP_SETTINGS_KEY))
                    soundRepository = Path.Combine(DirectoryEx.GetCurrentDirectory(),
                                                   ConfigurationManager.AppSettings[APP_SETTINGS_KEY]);
            }

            errorFile = Path.Combine(soundRepository, "Error.wav");
            warningFile = Path.Combine(soundRepository, "Warning.wav");
            questionFile = Path.Combine(soundRepository, "Question.wav");
        }

        /// <summary>
        /// Plays an annoying error notification beep
        /// </summary>
        public static void ErrorBeep()
        {
            try
            {
                if (File.Exists(errorFile))
                    SoundPlayerFacade.Instance.Play(errorFile);
                else 
                    SystemSounds.Exclamation.Play();
            }
            catch (Exception exception)
            {
                var handler = new DeviceExceptionHandler(exception);
                handler.HandleException("Error Beep failed", false);

                SystemSounds.Exclamation.Play();
            }
        }

        /// <summary>
        /// Plays a warning notification beep
        /// </summary>
        public static void WarningBeep()
        {
            try
            {
                if (File.Exists(errorFile))
                    SoundPlayerFacade.Instance.Play(warningFile);
                else 
                    SystemSounds.Asterisk.Play();
            }
            catch (Exception exception)
            {
                var handler = new DeviceExceptionHandler(exception);
                handler.HandleException("Warning Beep failed", false);

                SystemSounds.Exclamation.Play();
            }
        }

        /// <summary>
        /// Plays a question notification beep
        /// </summary>
        public static void QuestionBeep()
        {
            try
            {
                if (File.Exists(errorFile))
                    SoundPlayerFacade.Instance.Play(questionFile);
                else 
                    SystemSounds.Question.Play();
            }
            catch (Exception exception)
            {
                var handler = new DeviceExceptionHandler(exception);
                handler.HandleException("Question Beep failed", false);

                SystemSounds.Question.Play();
            }
        }
    }
}