#region Imported Namespaces

using System;
using System.Drawing;
using System.Windows.Forms;
using ChristianHelle.Framework.WindowsMobile.Core;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Forms
{
    /// <summary>
    /// Creates an instance of the splash screen
    /// </summary>
    public partial class SplashScreenForm : MobileForm
    {
        /// <summary>
        /// Creates an instance of the <see cref="SplashScreenForm"/>
        /// </summary>
        public SplashScreenForm()
        {
            InitializeComponent();
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            CenterForm();
        }

        private void CenterForm()
        {
            var height = Screen.PrimaryScreen.Bounds.Height;
            var width = Screen.PrimaryScreen.Bounds.Width;

            Location = new Point(
                (width - Size.Width) / 2,
                (height - Size.Height) / 2);
        }

        /// <summary>
        /// Displays the splash screen
        /// </summary>
        public new void Show()
        {
            SystemWindow.SetForegroundWindow(Handle);
            base.Show();
            Update();
        }
    }
}