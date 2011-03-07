using ChristianHelle.Framework.WindowsMobile.Patterns;

namespace ChristianHelle.Framework.WindowsMobile.Forms
{
    /// <summary>
    /// Represents the splash screen as a façade.
    /// </summary>
    /// <remarks>Anyone can access the splash screen through this class from anywhere</remarks>
    /// <example>
    /// This is an example of how you might use this class:
    /// 
    /// <code>
    /// public SomeFormConstructor()
    /// {
    ///		SplashScreen.Show()
    ///		
    ///		InitializeComponent();
    ///		
    ///		SplashScreen.Hide();
    /// }
    /// </code>
    /// </example>
    public static class SplashScreen
    {
        /// <summary>
        /// Gets the visibility state of the splash screen
        /// </summary>
        public static bool Visible { get; private set; }

        /// <summary>
        /// Displays the splash screen
        /// </summary>
        public static void Show()
        {
            if (Visible)
                return;

            var instance = Singleton<SplashScreenForm>.GetInstance();
            instance.Show();
            Visible = true;
        }

        /// <summary>
        /// Hides the splash screen
        /// </summary>
        public static void Hide()
        {
            if (!Visible) 
                return;

            var instance = Singleton<SplashScreenForm>.GetInstance();
            instance.Dispose();
            instance = null;
            Visible = false;
        }
    }
}