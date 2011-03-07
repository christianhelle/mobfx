namespace ChristianHelle.Framework.WindowsMobile.Forms.SmartMenu
{
    partial class SmartMenuFormView
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
            this.SuspendLayout();
            // 
            // SmartMenuFormView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "SmartMenuFormView";
            this.Text = "Mobile Application";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SmartMenuForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SmartMenuFormView_Paint);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SmartMenuFormView_Closing);
            this.Resize += new System.EventHandler(this.SmartMenuFormView_Resize);
            this.ResumeLayout(false);

        }

        #endregion

    }
}