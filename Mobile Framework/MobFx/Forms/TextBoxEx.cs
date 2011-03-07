using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChristianHelle.Framework.WindowsMobile.Forms
{
    /// <summary>
    /// Extends the windows <see cref="TextBox"/> control
    /// </summary>
    public class TextBoxEx : TextBox
    {
        private string textPrompt = string.Empty;
        private bool usePrompt;

        /// <summary>
        /// Creates a new instance of <see cref="TextBoxEx"/>
        /// </summary>
        public TextBoxEx()
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="TextBoxEx"/>
        /// </summary>
        /// <param name="helpTextPrompt">Help text prompt to display if the <see cref="TextBoxEx.Text"/> is empty</param>
        public TextBoxEx(string helpTextPrompt)
        {
            textPrompt = helpTextPrompt;
        }

        /// <summary>
        /// Gets or Sets the text prompt to be displayed if the <see cref="TextBoxEx.Text"/> is empty
        /// </summary>
        public string TextPrompt
        {
            get { return textPrompt; }
            set
            {
                textPrompt = value;
                if (UsePrompt && !string.IsNullOrEmpty(textPrompt))
                    Text = value;
            }
        }

        private bool UsePrompt
        {
            get { return usePrompt; }
            set
            {
                usePrompt = value;
                if (usePrompt)
                {
                    Font = new Font(Font.Name, Font.Size, FontStyle.Italic);
                    ForeColor = Color.Gray;
                }
                else
                {
                    Font = new Font(Font.Name, Font.Size, FontStyle.Regular);
                    ForeColor = Color.Black;
                }
            }
        }

        /// <summary>
        /// Gets or sets the text associated with this control
        /// </summary>
        public override string Text
        {
            get
            {
                if (UsePrompt)
                    return string.Empty;
                return base.Text;
            }
            set
            {
                if (UsePrompt && (!string.IsNullOrEmpty(value) && value != TextPrompt))
                    UsePrompt = false;

                if (string.IsNullOrEmpty(value) && !Focused && !string.IsNullOrEmpty(textPrompt))
                {
                    UsePrompt = true;
                    Text = TextPrompt;
                    return;
                }

                base.Text = value;
            }
        }

        /// <summary>
        /// Checks whether to display the text prompt or not
        /// </summary>
        /// <param name="e">Event Args</param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            if (UsePrompt)
            {
                UsePrompt = false;
                Text = string.Empty;
            }
        }

        /// <summary>
        /// Checks whether to display the text prompt or not
        /// </summary>
        /// <param name="e">Event Args</param>
        protected override void OnLostFocus(EventArgs e)
        {
            if (TextLength == 0 || Text == TextPrompt)
            {
                UsePrompt = true;
                Text = TextPrompt;
            }
            base.OnLostFocus(e);
        }

        /// <summary>
        /// Checks whether to display the text prompt or not
        /// </summary>
        /// <param name="e">Event Args</param>
        protected override void OnParentChanged(EventArgs e)
        {
            if (string.IsNullOrEmpty(Text))
            {
                UsePrompt = true;
                Text = TextPrompt;
            }
            base.OnParentChanged(e);
        }
    }
}