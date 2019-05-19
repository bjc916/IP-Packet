namespace Packet
{
    partial class ShowConfig
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
            this.rtxtPortConfig = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtxtPortConfig
            // 
            this.rtxtPortConfig.Location = new System.Drawing.Point(34, 44);
            this.rtxtPortConfig.Name = "rtxtPortConfig";
            this.rtxtPortConfig.ReadOnly = true;
            this.rtxtPortConfig.Size = new System.Drawing.Size(214, 175);
            this.rtxtPortConfig.TabIndex = 0;
            this.rtxtPortConfig.Text = "";
            // 
            // ShowConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.rtxtPortConfig);
            this.Name = "ShowConfig";
            this.Text = "ShowConfig";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtPortConfig;
    }
}