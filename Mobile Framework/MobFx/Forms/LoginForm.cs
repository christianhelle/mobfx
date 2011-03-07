using System;

namespace ChristianHelle.Framework.WindowsMobile.Forms
{
    /// <summary>
    /// Represents a dialog that specializes in authentication 
    /// </summary>
    public class LoginForm : MobileForm
    {
        private LabelEx labelExPassword;
        private TextBoxEx textBoxExUserId;
        private TextBoxEx textBoxExPassword;
        private ButtonEx buttonExLogin;
        private LabelEx labelExUserId;

        /// <summary>
        /// Creates and initializes the controls inside the form
        /// </summary>
        private void InitializeComponent()
        {
            this.labelExUserId = new LabelEx();
            this.labelExPassword = new LabelEx();
            this.textBoxExUserId = new TextBoxEx();
            this.textBoxExPassword = new TextBoxEx();
            this.buttonExLogin = new ButtonEx();
            this.SuspendLayout();
            // 
            // labelExUserId
            // 
            this.labelExUserId.AutoSize = true;
            this.labelExUserId.Location = new System.Drawing.Point( 20, 24 );
            this.labelExUserId.Name = "labelExUserId";
            this.labelExUserId.Size = new System.Drawing.Size( 37, 15 );
            this.labelExUserId.TabIndex = 0;
            this.labelExUserId.TabStop = false;
            this.labelExUserId.Text = "Login:";
            // 
            // labelExPassword
            // 
            this.labelExPassword.AutoSize = true;
            this.labelExPassword.Location = new System.Drawing.Point( 20, 95 );
            this.labelExPassword.Name = "labelExPassword";
            this.labelExPassword.Size = new System.Drawing.Size( 59, 15 );
            this.labelExPassword.TabIndex = 2;
            this.labelExPassword.TabStop = false;
            this.labelExPassword.Text = "Password:";
            // 
            // textBoxExUserId
            // 
            this.textBoxExUserId.Font = new System.Drawing.Font( "Tahoma", 9F, System.Drawing.FontStyle.Italic );
            this.textBoxExUserId.Location = new System.Drawing.Point( 32, 45 );
            this.textBoxExUserId.Name = "textBoxExUserId";
            this.textBoxExUserId.Size = new System.Drawing.Size( 176, 21 );
            this.textBoxExUserId.TabIndex = 1;
            this.textBoxExUserId.TextPrompt = "";
            // 
            // textBoxExPassword
            // 
            this.textBoxExPassword.Font = new System.Drawing.Font( "Tahoma", 9F, System.Drawing.FontStyle.Italic );
            this.textBoxExPassword.Location = new System.Drawing.Point( 32, 116 );
            this.textBoxExPassword.Name = "textBoxExPassword";
            this.textBoxExPassword.PasswordChar = '*';
            this.textBoxExPassword.Size = new System.Drawing.Size( 176, 21 );
            this.textBoxExPassword.TabIndex = 3;
            this.textBoxExPassword.TextPrompt = "";
            // 
            // buttonExLogin
            // 
            this.buttonExLogin.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonExLogin.Location = new System.Drawing.Point( 84, 178 );
            this.buttonExLogin.Name = "buttonExLogin";
            this.buttonExLogin.Size = new System.Drawing.Size( 124, 39 );
            this.buttonExLogin.TabIndex = 4;
            this.buttonExLogin.Text = "login";
            // 
            // LoginForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size( 240, 294 );
            this.Controls.Add( this.buttonExLogin );
            this.Controls.Add( this.textBoxExPassword );
            this.Controls.Add( this.textBoxExUserId );
            this.Controls.Add( this.labelExPassword );
            this.Controls.Add( this.labelExUserId );
            this.Name = "LoginForm";
            this.ResumeLayout( false );

        }

        /// <summary>
        /// Gets or sets the text to display for the UserId label.
        /// </summary>
        public string UserIdLabel
        {
            get
            {
                return labelExUserId.Text;
            }
            set
            {
                labelExUserId.Text = value;
            }
        }

        /// <summary>
        /// Gets ors sets the text to display for the Password label.
        /// </summary>
        public string PasswordLabel
        {
            get
            {
                return labelExPassword.Text;
            }
            set
            {
                labelExPassword.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the text to display for the Login button.
        /// </summary>
        public string LoginButtonLabel 
        {
            get
            {
                return buttonExLogin.Text;
            }
            set
            {
                buttonExLogin.Text = value;
            }
        }

        /// <summary>
        /// Raises the <see cref="System.Windows.Forms.Form.Load"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );
            InitializeComponent();
        }

        //protected void OnCredentialsSubmit()
        //{
        //}
    }
}
