using System.Collections.Generic;
using System.Windows.Forms;
using System;
using ChristianHelle.Framework.WindowsMobile.Configuration;
using ChristianHelle.Framework.WindowsMobile.Diagnostics;

namespace ChristianHelle.Framework.WindowsMobile.Forms
{
    /// <summary>
    /// Prompts the user through message dialogs
    /// </summary>
    public static class MessagePrompt
    {
        private static readonly bool messageBeep;
        private const string APP_SETTINGS_KEY = "MessagePromptAudioEnabled";

        static MessagePrompt()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings.AllKeys;
                if (appSettings != null && appSettings.Length > 0)
                {
                    var list = new List<string>(appSettings);
                    if (list.Contains(APP_SETTINGS_KEY))
                        messageBeep = Convert.ToBoolean(ConfigurationManager.AppSettings[APP_SETTINGS_KEY]);
                }
            }
            catch (Exception exception)
            {
                var handler = new DeviceExceptionHandler(exception);
                handler.HandleException("Unable to load MessagePrompt audio settings from <appSettings> (Key: MessagePromptAudioEnabled Values: True|False", false);
            }
        }

        /// <summary>
        /// Prompts the user with a question
        /// </summary>
        /// <param name="text">question</param>
        /// <param name="caption">caption or subject</param>
        /// <returns>Returns a <see cref="DialogResult"/> based on the users reaction</returns>
        public static DialogResult ShowQuestion(string text, string caption)
        {
            return ShowQuestion(text, caption, messageBeep);
        }

        /// <summary>
        /// Prompts the user with a question
        /// </summary>
        /// <param name="text">question</param>
        /// <param name="caption">caption or subject</param>
        /// <param name="beep">Set to <c>true</c> to play a message beep, otherwise <c>false</c></param>
        /// <returns>Returns a <see cref="DialogResult"/> based on the users reaction</returns>
        public static DialogResult ShowQuestion(string text, string caption, bool beep)
        {
            if (beep)
                Notifications.QuestionBeep();

            return FullScreenMessageBox.Show(
                text,
                caption,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// Prompts the user with a warning
        /// </summary>
        /// <param name="text">warning</param>
        /// <param name="caption">caption or subject</param>
        /// <returns>Returns a <see cref="DialogResult"/> based on the users reaction</returns>
        public static void ShowWarning(string text, string caption)
        {
            ShowWarning(text, caption, messageBeep);
        }

        /// <summary>
        /// Prompts the user with a warning
        /// </summary>
        /// <param name="text">warning</param>
        /// <param name="caption">caption or subject</param>
        /// <param name="beep">Set to <c>true</c> to play a message beep, otherwise <c>false</c></param>
        /// <returns>Returns a <see cref="DialogResult"/> based on the users reaction</returns>
        public static void ShowWarning(string text, string caption, bool beep)
        {
            if (beep)
                Notifications.WarningBeep();

            FullScreenMessageBox.Show(
                text,
                caption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// Prompts the user with an error
        /// </summary>
        /// <param name="exception">exception describing the error details</param>
        /// <param name="message">message to prompt</param>
        /// <param name="caption">caption or subject</param>
        public static void ShowErrorDialog(Exception exception, string message, string caption)
        {
            ShowErrorDialog(exception, message, caption, messageBeep);
        }

        /// <summary>
        /// Prompts the user with an error
        /// </summary>
        /// <param name="exception">exception describing the error details</param>
        /// <param name="message">message to prompt</param>
        /// <param name="caption">caption or subject</param>
        /// <param name="beep">Set to <c>true</c> to play a message beep, otherwise <c>false</c></param>
        public static void ShowErrorDialog(Exception exception, string message, string caption, bool beep)
        {
            if (beep)
                Notifications.ErrorBeep();

            FullScreenMessageBox.Show(
                ErrorMessageBuilder.BuildExceptionMessage(exception, message),
                caption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
        }
    }
}