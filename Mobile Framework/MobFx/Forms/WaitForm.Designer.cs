namespace ChristianHelle.Framework.WindowsMobile.Forms
{
    partial class WaitForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaitForm));
            this.Header = new System.Windows.Forms.Label();
            this.Message = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Header
            // 
            this.Header.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Header.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.Header.Location = new System.Drawing.Point(3, 43);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(314, 60);
            this.Header.Text = "Loading...";
            this.Header.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Message
            // 
            this.Message.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Message.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.Message.Location = new System.Drawing.Point(3, 103);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(314, 137);
            this.Message.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // WaitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(320, 240);
            this.ControlBox = false;
            this.Controls.Add(this.Message);
            this.Controls.Add(this.Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "WaitForm";
            this.Text = "WaitForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label Header;
        public System.Windows.Forms.Label Message;

    }
}