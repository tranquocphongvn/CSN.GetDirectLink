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
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDownloadFolder = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtProxies = new System.Windows.Forms.TextBox();
            this.chkUseProxy = new System.Windows.Forms.CheckBox();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.prb1 = new System.Windows.Forms.ProgressBar();
            this.lblSpeed1 = new System.Windows.Forms.Label();
            this.lblTimeLeft1 = new System.Windows.Forms.Label();
            this.lblMB1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(692, 717);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(90, 27);
            this.btnDownload.TabIndex = 0;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(790, 716);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 27);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Download Folder";
            // 
            // txtDownloadFolder
            // 
            this.txtDownloadFolder.Location = new System.Drawing.Point(130, 9);
            this.txtDownloadFolder.Name = "txtDownloadFolder";
            this.txtDownloadFolder.Size = new System.Drawing.Size(652, 22);
            this.txtDownloadFolder.TabIndex = 3;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(790, 6);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(80, 27);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // txtProxies
            // 
            this.txtProxies.Location = new System.Drawing.Point(12, 74);
            this.txtProxies.Multiline = true;
            this.txtProxies.Name = "txtProxies";
            this.txtProxies.Size = new System.Drawing.Size(858, 210);
            this.txtProxies.TabIndex = 7;
            // 
            // chkUseProxy
            // 
            this.chkUseProxy.AutoSize = true;
            this.chkUseProxy.Checked = true;
            this.chkUseProxy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseProxy.Location = new System.Drawing.Point(12, 44);
            this.chkUseProxy.Name = "chkUseProxy";
            this.chkUseProxy.Size = new System.Drawing.Size(105, 21);
            this.chkUseProxy.TabIndex = 5;
            this.chkUseProxy.Text = "&Use Proxies";
            this.chkUseProxy.UseVisualStyleBackColor = true;
            // 
            // txtConsole
            // 
            this.txtConsole.Location = new System.Drawing.Point(12, 293);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.Size = new System.Drawing.Size(858, 215);
            this.txtConsole.TabIndex = 8;
            // 
            // prb1
            // 
            this.prb1.Location = new System.Drawing.Point(12, 525);
            this.prb1.Name = "prb1";
            this.prb1.Size = new System.Drawing.Size(478, 23);
            this.prb1.TabIndex = 9;
            // 
            // lblSpeed1
            // 
            this.lblSpeed1.AutoSize = true;
            this.lblSpeed1.Location = new System.Drawing.Point(641, 525);
            this.lblSpeed1.Name = "lblSpeed1";
            this.lblSpeed1.Size = new System.Drawing.Size(51, 17);
            this.lblSpeed1.TabIndex = 10;
            this.lblSpeed1.Text = "0 MB/s";
            // 
            // lblTimeLeft1
            // 
            this.lblTimeLeft1.AutoSize = true;
            this.lblTimeLeft1.Location = new System.Drawing.Point(769, 525);
            this.lblTimeLeft1.Name = "lblTimeLeft1";
            this.lblTimeLeft1.Size = new System.Drawing.Size(100, 17);
            this.lblTimeLeft1.TabIndex = 11;
            this.lblTimeLeft1.Text = "00:00:00.0000";
            // 
            // lblMB1
            // 
            this.lblMB1.AutoSize = true;
            this.lblMB1.Location = new System.Drawing.Point(512, 525);
            this.lblMB1.Name = "lblMB1";
            this.lblMB1.Size = new System.Drawing.Size(52, 17);
            this.lblMB1.TabIndex = 12;
            this.lblMB1.Text = "MB/MB";
            // 
            // DownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 753);
            this.Controls.Add(this.lblMB1);
            this.Controls.Add(this.lblTimeLeft1);
            this.Controls.Add(this.lblSpeed1);
            this.Controls.Add(this.prb1);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.txtProxies);
            this.Controls.Add(this.chkUseProxy);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtDownloadFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDownload);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DownloadForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Download";
            this.Load += new System.EventHandler(this.DownloadForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDownloadFolder;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtProxies;
        private System.Windows.Forms.CheckBox chkUseProxy;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.ProgressBar prb1;
        private System.Windows.Forms.Label lblSpeed1;
        private System.Windows.Forms.Label lblTimeLeft1;
        private System.Windows.Forms.Label lblMB1;
    }
}