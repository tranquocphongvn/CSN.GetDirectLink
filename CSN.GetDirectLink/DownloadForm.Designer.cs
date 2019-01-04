namespace CSN
{
    partial class DownloadForm
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
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDownloadFolder = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtProxies = new System.Windows.Forms.TextBox();
            this.chkUseProxy = new System.Windows.Forms.CheckBox();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.prb1 = new System.Windows.Forms.ProgressBar();
            this.lblSpeed1 = new System.Windows.Forms.Label();
            this.lblTimeLeft1 = new System.Windows.Forms.Label();
            this.lblDownload1 = new System.Windows.Forms.Label();
            this.lblDownload2 = new System.Windows.Forms.Label();
            this.lblTimeLeft2 = new System.Windows.Forms.Label();
            this.lblSpeed2 = new System.Windows.Forms.Label();
            this.prb2 = new System.Windows.Forms.ProgressBar();
            this.lblFile1 = new System.Windows.Forms.Label();
            this.lblFile2 = new System.Windows.Forms.Label();
            this.lblFile3 = new System.Windows.Forms.Label();
            this.lblDownload3 = new System.Windows.Forms.Label();
            this.lblTimeLeft3 = new System.Windows.Forms.Label();
            this.lblSpeed3 = new System.Windows.Forms.Label();
            this.prb3 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(519, 583);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(68, 22);
            this.btnDownload.TabIndex = 0;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(592, 582);
            this.btnStop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(60, 22);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Download Folder";
            // 
            // txtDownloadFolder
            // 
            this.txtDownloadFolder.Location = new System.Drawing.Point(98, 7);
            this.txtDownloadFolder.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDownloadFolder.Name = "txtDownloadFolder";
            this.txtDownloadFolder.Size = new System.Drawing.Size(490, 20);
            this.txtDownloadFolder.TabIndex = 3;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(592, 5);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(60, 22);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // txtProxies
            // 
            this.txtProxies.Location = new System.Drawing.Point(9, 60);
            this.txtProxies.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtProxies.Multiline = true;
            this.txtProxies.Name = "txtProxies";
            this.txtProxies.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtProxies.Size = new System.Drawing.Size(644, 147);
            this.txtProxies.TabIndex = 7;
            // 
            // chkUseProxy
            // 
            this.chkUseProxy.AutoSize = true;
            this.chkUseProxy.Checked = true;
            this.chkUseProxy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseProxy.Location = new System.Drawing.Point(9, 36);
            this.chkUseProxy.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkUseProxy.Name = "chkUseProxy";
            this.chkUseProxy.Size = new System.Drawing.Size(82, 17);
            this.chkUseProxy.TabIndex = 5;
            this.chkUseProxy.Text = "&Use Proxies";
            this.chkUseProxy.UseVisualStyleBackColor = true;
            // 
            // txtConsole
            // 
            this.txtConsole.Location = new System.Drawing.Point(9, 221);
            this.txtConsole.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtConsole.Size = new System.Drawing.Size(644, 147);
            this.txtConsole.TabIndex = 8;
            // 
            // prb1
            // 
            this.prb1.Location = new System.Drawing.Point(9, 398);
            this.prb1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.prb1.Name = "prb1";
            this.prb1.Size = new System.Drawing.Size(343, 19);
            this.prb1.TabIndex = 9;
            // 
            // lblSpeed1
            // 
            this.lblSpeed1.AutoSize = true;
            this.lblSpeed1.Location = new System.Drawing.Point(482, 398);
            this.lblSpeed1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSpeed1.Name = "lblSpeed1";
            this.lblSpeed1.Size = new System.Drawing.Size(55, 13);
            this.lblSpeed1.TabIndex = 10;
            this.lblSpeed1.Text = "S: 0 MB/s";
            // 
            // lblTimeLeft1
            // 
            this.lblTimeLeft1.AutoSize = true;
            this.lblTimeLeft1.Location = new System.Drawing.Point(564, 398);
            this.lblTimeLeft1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTimeLeft1.Name = "lblTimeLeft1";
            this.lblTimeLeft1.Size = new System.Drawing.Size(88, 13);
            this.lblTimeLeft1.TabIndex = 11;
            this.lblTimeLeft1.Text = "L: 00:00:00.0000";
            // 
            // lblDownload1
            // 
            this.lblDownload1.AutoSize = true;
            this.lblDownload1.Location = new System.Drawing.Point(373, 398);
            this.lblDownload1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDownload1.Name = "lblDownload1";
            this.lblDownload1.Size = new System.Drawing.Size(76, 13);
            this.lblDownload1.TabIndex = 12;
            this.lblDownload1.Text = "D: 0 MB/0 MB";
            // 
            // lblDownload2
            // 
            this.lblDownload2.AutoSize = true;
            this.lblDownload2.Location = new System.Drawing.Point(374, 446);
            this.lblDownload2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDownload2.Name = "lblDownload2";
            this.lblDownload2.Size = new System.Drawing.Size(76, 13);
            this.lblDownload2.TabIndex = 16;
            this.lblDownload2.Text = "D: 0 MB/0 MB";
            // 
            // lblTimeLeft2
            // 
            this.lblTimeLeft2.AutoSize = true;
            this.lblTimeLeft2.Location = new System.Drawing.Point(565, 446);
            this.lblTimeLeft2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTimeLeft2.Name = "lblTimeLeft2";
            this.lblTimeLeft2.Size = new System.Drawing.Size(88, 13);
            this.lblTimeLeft2.TabIndex = 15;
            this.lblTimeLeft2.Text = "L: 00:00:00.0000";
            // 
            // lblSpeed2
            // 
            this.lblSpeed2.AutoSize = true;
            this.lblSpeed2.Location = new System.Drawing.Point(483, 446);
            this.lblSpeed2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSpeed2.Name = "lblSpeed2";
            this.lblSpeed2.Size = new System.Drawing.Size(55, 13);
            this.lblSpeed2.TabIndex = 14;
            this.lblSpeed2.Text = "S: 0 MB/s";
            // 
            // prb2
            // 
            this.prb2.Location = new System.Drawing.Point(9, 446);
            this.prb2.Margin = new System.Windows.Forms.Padding(2);
            this.prb2.Name = "prb2";
            this.prb2.Size = new System.Drawing.Size(343, 19);
            this.prb2.TabIndex = 13;
            // 
            // lblFile1
            // 
            this.lblFile1.AutoSize = true;
            this.lblFile1.Location = new System.Drawing.Point(9, 383);
            this.lblFile1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFile1.Name = "lblFile1";
            this.lblFile1.Size = new System.Drawing.Size(16, 13);
            this.lblFile1.TabIndex = 17;
            this.lblFile1.Text = "F:";
            // 
            // lblFile2
            // 
            this.lblFile2.AutoSize = true;
            this.lblFile2.Location = new System.Drawing.Point(9, 431);
            this.lblFile2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFile2.Name = "lblFile2";
            this.lblFile2.Size = new System.Drawing.Size(16, 13);
            this.lblFile2.TabIndex = 18;
            this.lblFile2.Text = "F:";
            // 
            // lblFile3
            // 
            this.lblFile3.AutoSize = true;
            this.lblFile3.Location = new System.Drawing.Point(9, 479);
            this.lblFile3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFile3.Name = "lblFile3";
            this.lblFile3.Size = new System.Drawing.Size(16, 13);
            this.lblFile3.TabIndex = 23;
            this.lblFile3.Text = "F:";
            // 
            // lblDownload3
            // 
            this.lblDownload3.AutoSize = true;
            this.lblDownload3.Location = new System.Drawing.Point(374, 494);
            this.lblDownload3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDownload3.Name = "lblDownload3";
            this.lblDownload3.Size = new System.Drawing.Size(76, 13);
            this.lblDownload3.TabIndex = 22;
            this.lblDownload3.Text = "D: 0 MB/0 MB";
            // 
            // lblTimeLeft3
            // 
            this.lblTimeLeft3.AutoSize = true;
            this.lblTimeLeft3.Location = new System.Drawing.Point(565, 494);
            this.lblTimeLeft3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTimeLeft3.Name = "lblTimeLeft3";
            this.lblTimeLeft3.Size = new System.Drawing.Size(88, 13);
            this.lblTimeLeft3.TabIndex = 21;
            this.lblTimeLeft3.Text = "L: 00:00:00.0000";
            // 
            // lblSpeed3
            // 
            this.lblSpeed3.AutoSize = true;
            this.lblSpeed3.Location = new System.Drawing.Point(483, 494);
            this.lblSpeed3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSpeed3.Name = "lblSpeed3";
            this.lblSpeed3.Size = new System.Drawing.Size(55, 13);
            this.lblSpeed3.TabIndex = 20;
            this.lblSpeed3.Text = "S: 0 MB/s";
            // 
            // prb3
            // 
            this.prb3.Location = new System.Drawing.Point(9, 494);
            this.prb3.Margin = new System.Windows.Forms.Padding(2);
            this.prb3.Name = "prb3";
            this.prb3.Size = new System.Drawing.Size(343, 19);
            this.prb3.TabIndex = 19;
            // 
            // DownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 612);
            this.Controls.Add(this.lblFile3);
            this.Controls.Add(this.lblDownload3);
            this.Controls.Add(this.lblTimeLeft3);
            this.Controls.Add(this.lblSpeed3);
            this.Controls.Add(this.prb3);
            this.Controls.Add(this.lblFile2);
            this.Controls.Add(this.lblFile1);
            this.Controls.Add(this.lblDownload2);
            this.Controls.Add(this.lblTimeLeft2);
            this.Controls.Add(this.lblSpeed2);
            this.Controls.Add(this.prb2);
            this.Controls.Add(this.lblDownload1);
            this.Controls.Add(this.lblTimeLeft1);
            this.Controls.Add(this.lblSpeed1);
            this.Controls.Add(this.prb1);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.txtProxies);
            this.Controls.Add(this.chkUseProxy);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtDownloadFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnDownload);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DownloadForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Download";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DownloadForm_FormClosing);
            this.Load += new System.EventHandler(this.DownloadForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDownloadFolder;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtProxies;
        private System.Windows.Forms.CheckBox chkUseProxy;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.ProgressBar prb1;
        private System.Windows.Forms.Label lblSpeed1;
        private System.Windows.Forms.Label lblTimeLeft1;
        private System.Windows.Forms.Label lblDownload1;
        private System.Windows.Forms.Label lblDownload2;
        private System.Windows.Forms.Label lblTimeLeft2;
        private System.Windows.Forms.Label lblSpeed2;
        private System.Windows.Forms.ProgressBar prb2;
        private System.Windows.Forms.Label lblFile1;
        private System.Windows.Forms.Label lblFile2;
        private System.Windows.Forms.Label lblFile3;
        private System.Windows.Forms.Label lblDownload3;
        private System.Windows.Forms.Label lblTimeLeft3;
        private System.Windows.Forms.Label lblSpeed3;
        private System.Windows.Forms.ProgressBar prb3;
    }
}