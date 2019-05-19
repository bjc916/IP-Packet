namespace Packet
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tstripCmboXmitRcv = new System.Windows.Forms.ToolStripComboBox();
            this.tstripCmboError = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.tstripPortStatusOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tstripPortStatusClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripShowConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.txtPortStatus = new System.Windows.Forms.ToolStripTextBox();
            this.tstripCheckConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.tstripLaunchFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tstripSendFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tstripClearDisplay = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rtxtAscii = new System.Windows.Forms.RichTextBox();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.rtxtHex = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstripCmboXmitRcv,
            this.tstripCmboError,
            this.toolStripTextBox1,
            this.tstripPortStatusOpen,
            this.tstripPortStatusClose,
            this.toolStripShowConfig,
            this.txtPortStatus,
            this.tstripCheckConnection,
            this.tstripLaunchFile,
            this.tstripSendFile,
            this.tstripClearDisplay});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1236, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tstripCmboXmitRcv
            // 
            this.tstripCmboXmitRcv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tstripCmboXmitRcv.Items.AddRange(new object[] {
            "Transmit",
            "Receive"});
            this.tstripCmboXmitRcv.Name = "tstripCmboXmitRcv";
            this.tstripCmboXmitRcv.Size = new System.Drawing.Size(160, 24);
            this.tstripCmboXmitRcv.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // tstripCmboError
            // 
            this.tstripCmboError.Items.AddRange(new object[] {
            "Error Free",
            "CRC With Recovery",
            "CRC Without Recovery",
            "SOH With Recovery"});
            this.tstripCmboError.Name = "tstripCmboError";
            this.tstripCmboError.Size = new System.Drawing.Size(160, 24);
            this.tstripCmboError.SelectedIndexChanged += new System.EventHandler(this.tstripCmboError_SelectedIndexChanged);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.ReadOnly = true;
            this.toolStripTextBox1.Size = new System.Drawing.Size(39, 24);
            this.toolStripTextBox1.Text = "Port:";
            // 
            // tstripPortStatusOpen
            // 
            this.tstripPortStatusOpen.Name = "tstripPortStatusOpen";
            this.tstripPortStatusOpen.Size = new System.Drawing.Size(48, 24);
            this.tstripPortStatusOpen.Text = "Open";
            this.tstripPortStatusOpen.ToolTipText = "Open Com Port";
            this.tstripPortStatusOpen.Click += new System.EventHandler(this.tstripPortStatusOpen_Click);
            // 
            // tstripPortStatusClose
            // 
            this.tstripPortStatusClose.Name = "tstripPortStatusClose";
            this.tstripPortStatusClose.Size = new System.Drawing.Size(48, 24);
            this.tstripPortStatusClose.Text = "Close";
            this.tstripPortStatusClose.ToolTipText = "Close Com Port";
            this.tstripPortStatusClose.Click += new System.EventHandler(this.tstripPortStatusClose_Click_1);
            // 
            // toolStripShowConfig
            // 
            this.toolStripShowConfig.Name = "toolStripShowConfig";
            this.toolStripShowConfig.Size = new System.Drawing.Size(87, 24);
            this.toolStripShowConfig.Text = "Show Config";
            this.toolStripShowConfig.ToolTipText = "Show Com Port Configuration";
            this.toolStripShowConfig.Click += new System.EventHandler(this.toolStripShowConfig_Click);
            // 
            // txtPortStatus
            // 
            this.txtPortStatus.Name = "txtPortStatus";
            this.txtPortStatus.ReadOnly = true;
            this.txtPortStatus.Size = new System.Drawing.Size(132, 24);
            this.txtPortStatus.Text = "Not Connected";
            this.txtPortStatus.ToolTipText = "Not Connected";
            // 
            // tstripCheckConnection
            // 
            this.tstripCheckConnection.Name = "tstripCheckConnection";
            this.tstripCheckConnection.Size = new System.Drawing.Size(117, 24);
            this.tstripCheckConnection.Text = "Check Connection";
            this.tstripCheckConnection.ToolTipText = "Check Connection";
            this.tstripCheckConnection.Click += new System.EventHandler(this.tstripCheckConnection_Click);
            // 
            // tstripLaunchFile
            // 
            this.tstripLaunchFile.Image = ((System.Drawing.Image)(resources.GetObject("tstripLaunchFile.Image")));
            this.tstripLaunchFile.Name = "tstripLaunchFile";
            this.tstripLaunchFile.Size = new System.Drawing.Size(32, 24);
            this.tstripLaunchFile.ToolTipText = "Launch File Explorer";
            this.tstripLaunchFile.Click += new System.EventHandler(this.tstripLaunchFile_Click);
            // 
            // tstripSendFile
            // 
            this.tstripSendFile.Image = ((System.Drawing.Image)(resources.GetObject("tstripSendFile.Image")));
            this.tstripSendFile.Name = "tstripSendFile";
            this.tstripSendFile.Size = new System.Drawing.Size(32, 24);
            this.tstripSendFile.ToolTipText = "Send ";
            this.tstripSendFile.Click += new System.EventHandler(this.tstripSendFile_Click);
            // 
            // tstripClearDisplay
            // 
            this.tstripClearDisplay.Name = "tstripClearDisplay";
            this.tstripClearDisplay.Size = new System.Drawing.Size(87, 24);
            this.tstripClearDisplay.Text = "Clear Display";
            this.tstripClearDisplay.Click += new System.EventHandler(this.tstripClearDisplay_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 28);
            this.splitter1.Margin = new System.Windows.Forms.Padding(4);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 528);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(4, 529);
            this.statusStrip1.MaximumSize = new System.Drawing.Size(1232, 27);
            this.statusStrip1.MinimumSize = new System.Drawing.Size(1232, 27);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1232, 27);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 22);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rtxtAscii);
            this.panel1.Controls.Add(this.splitter2);
            this.panel1.Controls.Add(this.rtxtHex);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 28);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1232, 501);
            this.panel1.TabIndex = 8;
            // 
            // rtxtAscii
            // 
            this.rtxtAscii.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtAscii.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtAscii.Location = new System.Drawing.Point(617, 0);
            this.rtxtAscii.Margin = new System.Windows.Forms.Padding(4);
            this.rtxtAscii.Name = "rtxtAscii";
            this.rtxtAscii.ReadOnly = true;
            this.rtxtAscii.Size = new System.Drawing.Size(615, 501);
            this.rtxtAscii.TabIndex = 2;
            this.rtxtAscii.Text = "";
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(613, 0);
            this.splitter2.Margin = new System.Windows.Forms.Padding(4);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(4, 501);
            this.splitter2.TabIndex = 1;
            this.splitter2.TabStop = false;
            // 
            // rtxtHex
            // 
            this.rtxtHex.Dock = System.Windows.Forms.DockStyle.Left;
            this.rtxtHex.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtHex.Location = new System.Drawing.Point(0, 0);
            this.rtxtHex.Margin = new System.Windows.Forms.Padding(4);
            this.rtxtHex.Name = "rtxtHex";
            this.rtxtHex.ReadOnly = true;
            this.rtxtHex.Size = new System.Drawing.Size(613, 501);
            this.rtxtHex.TabIndex = 0;
            this.rtxtHex.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1236, 556);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1251, 593);
            this.Name = "Form1";
            this.Text = "IP Packet Lab 6A";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripComboBox tstripCmboXmitRcv;
        private System.Windows.Forms.ToolStripComboBox tstripCmboError;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripMenuItem tstripPortStatusClose;
        private System.Windows.Forms.ToolStripMenuItem tstripPortStatusOpen;
        private System.Windows.Forms.ToolStripMenuItem toolStripShowConfig;
        private System.Windows.Forms.ToolStripMenuItem tstripLaunchFile;
        private System.Windows.Forms.ToolStripMenuItem tstripSendFile;
        private System.Windows.Forms.ToolStripMenuItem tstripClearDisplay;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox rtxtAscii;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.RichTextBox rtxtHex;
        public System.Windows.Forms.ToolStripTextBox txtPortStatus;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        public System.Windows.Forms.ToolStripMenuItem tstripCheckConnection;
    }
}

