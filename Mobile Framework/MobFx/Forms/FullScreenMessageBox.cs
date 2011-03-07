using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ChristianHelle.Framework.WindowsMobile.Media;

namespace ChristianHelle.Framework.WindowsMobile.Forms
{
    /// <summary>
    /// Full Screen MessageBox for Pocket PC
    /// </summary>
    public class FullScreenMessageBox
    {
        /// <summary>
        /// Displays a message box with specified text
        /// </summary>
        /// <param name="text">The text to display in the message box</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values</returns>
        public static DialogResult Show(string text)
        {
            return Show(text, string.Empty, MessageBoxButtons.OK,
                        MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// Displays a message box with specified text
        /// </summary>
        /// <param name="text">The text to display in the message box</param>
        /// <param name="caption">The text to display in the message box</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values</returns>
        public static DialogResult Show(string text, string caption)
        {
            return Show(text, caption, MessageBoxButtons.OK,
                        MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// Displays a message box with specified text
        /// </summary>
        /// <param name="text">The text to display in the message box</param>
        /// <param name="caption">The text to display in the message box</param>
        /// <param name="buttons">
        /// One of the System.Windows.Forms.MessageBoxButtons values that specifies which
        /// buttons to display in the message box</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
        {
            return Show(text, caption, buttons,
                        MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// Displays a message box with specified text
        /// </summary>
        /// <param name="text">The text to display in the message box</param>
        /// <param name="caption">The text to display in the message box</param>
        /// <param name="buttons">
        /// One of the System.Windows.Forms.MessageBoxButtons values that specifies which
        /// buttons to display in the message box</param>
        /// <param name="icon">
        /// One of the System.Windows.Forms.MessageBoxIcon values that specifies which
        /// icon to display in the message box</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values</returns>
        public static DialogResult Show(string text, string caption,
                                        MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return Show(text, caption, buttons, icon,
                        MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// Displays a message box with specified text
        /// </summary>
        /// <param name="text">The text to display in the message box</param>
        /// <param name="caption">The text to display in the message box</param>
        /// <param name="buttons">
        /// One of the System.Windows.Forms.MessageBoxButtons values that specifies which
        /// buttons to display in the message box</param>
        /// <param name="icon">
        /// One of the System.Windows.Forms.MessageBoxIcon values that specifies which
        /// icon to display in the message box</param>
        /// <param name="defaultButton">
        /// One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies
        /// the default button for the message box</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values</returns>
        public static DialogResult Show(string text, string caption,
                                        MessageBoxButtons buttons, MessageBoxIcon icon,
                                        MessageBoxDefaultButton defaultButton)
        {
            using (var form = new MessageBoxDlg(text, caption, buttons, icon))
            {
                if (Environment.OSVersion.Platform != PlatformID.WinCE)
                    form.Show();
                else
                    return form.ShowDialog();
                    //MessageBox.Show(text, caption, buttons, icon, defaultButton);
            }

            return DialogResult.OK;
        }

        #region Nested type: MessageBoxDlg

        private sealed class MessageBoxDlg : MobileForm
        {
            private const string NAME = "ChristianHelle.Framework.WindowsMobile.Images.TopBar.bmp";
            private static readonly bool HiDpi;
            private static readonly bool IsWm5;
            private RectangleF bodyRect;
            private int buttonHeight;
            private string header;
            private Button left;
            private MainMenu mainMenu1;
            private string message;
            private Button right;
            private SystemSound sound;
            private string watermark;

            static MessageBoxDlg()
            {
                IsWm5 = Environment.OSVersion.Version >= new Version(5, 1);
                HiDpi = (Screen.PrimaryScreen.WorkingArea.Width + Screen.PrimaryScreen.WorkingArea.Height) > 560;
            }

            public MessageBoxDlg(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
            {
                if (Environment.OSVersion.Platform == PlatformID.WinCE)
                    WindowState = FormWindowState.Maximized;

                BackColor = Color.White;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                ControlBox = false;
                MaximizeBox = false;
                MinimizeBox = false;
                Text = string.IsNullOrEmpty(caption) ? "  " : caption;

                buttonHeight = 0;
                if ((IsWm5 || (buttons == MessageBoxButtons.AbortRetryIgnore)) ||
                    (buttons == MessageBoxButtons.YesNoCancel))
                {
                    mainMenu1 = new MainMenu();
                    Menu = mainMenu1;
                }
                else
                {
                    buttonHeight = HiDpi ? 0x34 : 0x1a;

                    left = new Button { Bounds = new Rectangle(0, Height - buttonHeight, Width / 2, buttonHeight) };
                    Controls.Add(left);

                    right = new Button { Bounds = new Rectangle(Width / 2, Height - buttonHeight, Width / 2, buttonHeight) };
                    Controls.Add(right);
                }

                message = text;
                switch (buttons)
                {
                    case MessageBoxButtons.OK:
                        if (!IsWm5)
                        {
                            left.Text = "OK";
                            left.KeyDown += (sender, e) => OnOk(sender, EventArgs.Empty);
                            left.Click += OnOk;
                            right.Enabled = false;
                            break;
                        }
                        var item1 = new MenuItem();
                        item1.Text = "OK";
                        mainMenu1.MenuItems.Add(item1);
                        item1.Click += OnOk;
                        break;

                    case MessageBoxButtons.OKCancel:
                        if (!IsWm5)
                        {
                            right.Text = "Cancel";
                            left.KeyDown += (sender, e) => OnOk(sender, EventArgs.Empty);
                            left.Click += OnOk;
                            right.KeyDown += (sender, e) => OnCancel(sender, EventArgs.Empty);
                            right.Click += OnCancel;
                            break;
                        }
                        var item2 = new MenuItem();
                        var item3 = new MenuItem();
                        item2.Text = "OK";
                        item3.Text = "Cancel";
                        mainMenu1.MenuItems.Add(item2);
                        mainMenu1.MenuItems.Add(item3);
                        item2.Click += OnOk;
                        item3.Click += OnCancel;
                        break;

                    case MessageBoxButtons.AbortRetryIgnore:
                        var item4 = new MenuItem();
                        var item5 = new MenuItem();
                        var item6 = new MenuItem();
                        item4.Text = "Abort";
                        item5.Text = "Retry";
                        item6.Text = "Ignore";
                        mainMenu1.MenuItems.Add(item4);
                        mainMenu1.MenuItems.Add(item5);
                        mainMenu1.MenuItems.Add(item6);
                        item4.Click += OnAbort;
                        item5.Click += OnRetry;
                        item6.Click += OnIgnore;
                        break;

                    case MessageBoxButtons.YesNoCancel:
                        var item7 = new MenuItem();
                        var item8 = new MenuItem();
                        var item9 = new MenuItem();
                        item7.Text = "Yes";
                        item8.Text = "No";
                        mainMenu1.MenuItems.Add(item7);
                        mainMenu1.MenuItems.Add(item8);
                        mainMenu1.MenuItems.Add(item9);
                        item7.Click += OnYes;
                        item8.Click += OnNo;
                        item9.Click += OnNo;
                        break;

                    case MessageBoxButtons.YesNo:
                        if (!IsWm5)
                        {
                            left.Text = "Yes";
                            right.Text = "No";
                            left.KeyDown += (sender, e) => OnYes(sender, EventArgs.Empty);
                            left.Click += OnYes;
                            right.KeyDown += (sender, e) => OnNo(sender, EventArgs.Empty);
                            right.Click += OnNo;
                            break;
                        }
                        var item10 = new MenuItem();
                        var item11 = new MenuItem();
                        item10.Text = "Yes";
                        item11.Text = "No";
                        mainMenu1.MenuItems.Add(item10);
                        mainMenu1.MenuItems.Add(item11);
                        item10.Click += OnYes;
                        item11.Click += OnNo;
                        break;

                    case MessageBoxButtons.RetryCancel:
                        if (!IsWm5)
                        {
                            left.Text = "Retry";
                            right.Text = "Cancel";
                            left.KeyDown += (sender, e) => OnRetry(sender, EventArgs.Empty);
                            left.Click += OnRetry;
                            right.KeyDown += (sender, e) => OnCancel(sender, EventArgs.Empty);
                            right.Click += OnCancel;
                            break;
                        }
                        var item12 = new MenuItem();
                        var item13 = new MenuItem();
                        item12.Text = "Retry";
                        item13.Text = "Cancel";
                        mainMenu1.MenuItems.Add(item12);
                        mainMenu1.MenuItems.Add(item13);
                        item12.Click += OnRetry;
                        item13.Click += OnCancel;
                        break;
                }

                if (icon <= MessageBoxIcon.Question)
                {
                    if (icon != MessageBoxIcon.Hand)
                    {
                        if (icon == MessageBoxIcon.Question)
                        {
                            header = "Confirm";
                            watermark = "?";
                            sound = SystemSounds.Question;
                        }
                        return;
                    }
                }
                else
                {
                    if (icon != MessageBoxIcon.Exclamation)
                    {
                        if (icon == MessageBoxIcon.Asterisk)
                        {
                            header = "Info";
                            watermark = "i";
                            sound = SystemSounds.Asterisk;
                        }
                        return;
                    }
                    header = "Alert";
                    watermark = "!";
                    sound = SystemSounds.Exclamation;
                    return;
                }

                header = "Notification";
                watermark = "!";
                sound = SystemSounds.Hand;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                if (watermark != null)
                {
                    var width = Width - (HiDpi ? 0x60 : 0x40);
                    var height = (Height - buttonHeight) - (HiDpi ? 0x80 : 0x60);

                    using (var brush = new SolidBrush(SystemColors.Control))
                    using (var font = new Font(FontFamily.GenericSansSerif, 64f, FontStyle.Bold))
                        e.Graphics.DrawString(watermark, font, brush, width, height);
                }

                if (header != null)
                {
                    using (var font2 = new Font(FontFamily.GenericSansSerif, 16f, FontStyle.Bold))
                    {
                        using (var brush = new SolidBrush(SystemColors.Highlight))
                            e.Graphics.DrawString(header, font2, brush, HiDpi ? (0x10) : (8), 8f);

                        using (var pen = new Pen(Color.Black))
                            e.Graphics.DrawLine(pen, 0, HiDpi ? 0x48 : 0x2a, Width, HiDpi ? 0x48 : 0x2a);
                    }
                }

                if (message != null)
                {
                    using (var brush2 = new SolidBrush(Color.Black))
                        if (Screen.PrimaryScreen.Bounds.Height == Screen.PrimaryScreen.Bounds.Width)
                            e.Graphics.DrawString(message, Font, brush2, bodyRect);
                        else
                            using (var font = new Font(Font.Name, Font.Size + 2f, FontStyle.Regular))
                                e.Graphics.DrawString(message, font, brush2, bodyRect);
                }

                try
                {
                    BringToFront();
                    SHFullScreen(Handle, 40);
                }
                catch (MissingMethodException) { }

                base.OnPaint(e);
            }

            [DllImport("coredll.dll", SetLastError = true)]
            private static extern IntPtr GetCapture();

            [DllImport("aygshell.dll", SetLastError = true)]
            private static extern bool SHFullScreen(IntPtr hwnd, int state);

            private void OnAbort(object sender, EventArgs e)
            {
                DialogResult = DialogResult.Abort;
            }

            private void OnCancel(object sender, EventArgs e)
            {
                DialogResult = DialogResult.Cancel;
            }

            private void OnIgnore(object sender, EventArgs e)
            {
                DialogResult = DialogResult.Ignore;
            }

            protected override void OnLoad(EventArgs e)
            {
                if (sound != null)
                    sound.Play();
            }

            private void OnNo(object sender, EventArgs e)
            {
                DialogResult = DialogResult.No;
            }

            private void OnOk(object sender, EventArgs e)
            {
                DialogResult = DialogResult.OK;
            }

            protected override void OnResize(EventArgs e)
            {
                var x = HiDpi ? 0x10 : 8;
                if (header == null)
                {
                    bodyRect = new RectangleF(
                        x,
                        x,
                        (Width - (2 * x)),
                        ((Height - buttonHeight) - (2 * x)));
                }
                else
                {
                    bodyRect = new RectangleF(
                        x,
                        (6 * x),
                        (Width - (2 * x)),
                        ((Height - buttonHeight) - (8 * x)));
                }
                //bodyRect = Dpi.ScaleRectangleF(bodyRect);
                base.OnResize(e);
            }

            private void OnRetry(object sender, EventArgs e)
            {
                DialogResult = DialogResult.Retry;
            }

            private void OnYes(object sender, EventArgs e)
            {
                DialogResult = DialogResult.Yes;
            }
        }

        #endregion
    }
}