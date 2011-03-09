using Microsoft.WindowsCE.Forms;

namespace ChristianHelle.Framework.WindowsMobile.Forms
{
    /// <summary>
    /// Used for displaying a notification bubble
    /// </summary>
    public static class NotificationBubble
    {
        /// <summary>
        /// Displays a notification bubble for 2 seconds
        /// </summary>
        /// <param name="text">Body</param>
        public static void Show(string text)
        {
            Show(2, "Notification", text);
        }

        /// <summary>
        /// Displays a notification bubble for 2 seconds
        /// </summary>
        /// <param name="caption">Caption</param>
        /// <param name="text">Body</param>
        public static void Show(string caption, string text)
        {
            Show(2, caption, text);
        }

        /// <summary>
        /// Displays a notification bubble
        /// </summary>
        /// <param name="duration">Duration in which the notification bubble is shown (in seconds)</param>
        /// <param name="caption">Caption</param>
        /// <param name="text">Body</param>
        public static void Show(int duration, string caption, string text)
        {
            var bubble = new Notification
            {
                InitialDuration = duration,
                Caption = caption,
                Text = text
            };

            bubble.BalloonChanged += OnBalloonChanged;
            bubble.Visible = true;
        }

        private static void OnBalloonChanged(object sender, BalloonChangedEventArgs e)
        {
            if (!e.Visible)
                ((Notification)sender).Dispose();
        }
    }
}
