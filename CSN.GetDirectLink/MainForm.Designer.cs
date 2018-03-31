namespace CSN.GetDirectLink
{
    partial class MainForm
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
            this.txtLink = new System.Windows.Forms.TextBox();
            this.txtResponse = new System.Windows.Forms.TextBox();
            this.btnGetLinks = new System.Windows.Forms.Button();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rbLossless = new System.Windows.Forms.RadioButton();
            this.rb500kbps = new System.Windows.Forms.RadioButton();
            this.rb320kbps = new System.Windows.Forms.RadioButton();
            this.rb128kbps = new System.Windows.Forms.RadioButton();
            this.lvSongs = new System.Windows.Forms.ListView();
            this.chChecked = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSong = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLink = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chQuality = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLossless = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMaxQuality = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAllLinks = new System.Windows.Forms.Button();
            this.btnQualifiedLinks = new System.Windows.Forms.Button();
            this.btnCopyLinks = new System.Windows.Forms.Button();
            this.pbSpectrum = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbSpectrum)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLink
            // 
            this.txtLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLink.Location = new System.Drawing.Point(146, 10);
            this.txtLink.Margin = new System.Windows.Forms.Padding(2);
            this.txtLink.Name = "txtLink";
            this.txtLink.Size = new System.Drawing.Size(1097, 21);
            this.txtLink.TabIndex = 0;
            this.txtLink.Text = "http://chiasenhac.vn/nhac-hot-week/~tsvc5d63qvhmkw.html";
            this.txtLink.TextChanged += new System.EventHandler(this.txtLink_TextChanged);
            // 
            // txtResponse
            // 
            this.txtResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResponse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResponse.Location = new System.Drawing.Point(9, 58);
            this.txtResponse.Margin = new System.Windows.Forms.Padding(2);
            this.txtResponse.Multiline = true;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResponse.Size = new System.Drawing.Size(1333, 237);
            this.txtResponse.TabIndex = 6;
            // 
            // btnGetLinks
            // 
            this.btnGetLinks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetLinks.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetLinks.Location = new System.Drawing.Point(1257, 10);
            this.btnGetLinks.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetLinks.Name = "btnGetLinks";
            this.btnGetLinks.Size = new System.Drawing.Size(84, 22);
            this.btnGetLinks.TabIndex = 1;
            this.btnGetLinks.Text = "Get Links";
            this.btnGetLinks.UseVisualStyleBackColor = true;
            this.btnGetLinks.Click += new System.EventHandler(this.btnGetLinks_Click);
            // 
            // txtConsole
            // 
            this.txtConsole.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConsole.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsole.Location = new System.Drawing.Point(9, 303);
            this.txtConsole.Margin = new System.Windows.Forms.Padding(2);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtConsole.Size = new System.Drawing.Size(1333, 191);
            this.txtConsole.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "CSN Album/Playlist link";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 35);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Maximum quality";
            // 
            // rbLossless
            // 
            this.rbLossless.AutoSize = true;
            this.rbLossless.Location = new System.Drawing.Point(146, 35);
            this.rbLossless.Margin = new System.Windows.Forms.Padding(2);
            this.rbLossless.Name = "rbLossless";
            this.rbLossless.Size = new System.Drawing.Size(100, 17);
            this.rbLossless.TabIndex = 2;
            this.rbLossless.Tag = "Lossless";
            this.rbLossless.Text = "Lossless (FLAC)";
            this.rbLossless.UseVisualStyleBackColor = true;
            // 
            // rb500kbps
            // 
            this.rb500kbps.AutoSize = true;
            this.rb500kbps.Checked = true;
            this.rb500kbps.Location = new System.Drawing.Point(292, 35);
            this.rb500kbps.Margin = new System.Windows.Forms.Padding(2);
            this.rb500kbps.Name = "rb500kbps";
            this.rb500kbps.Size = new System.Drawing.Size(89, 17);
            this.rb500kbps.TabIndex = 3;
            this.rb500kbps.TabStop = true;
            this.rb500kbps.Tag = "500kbps";
            this.rb500kbps.Text = "m4a 500kbps";
            this.rb500kbps.UseVisualStyleBackColor = true;
            // 
            // rb320kbps
            // 
            this.rb320kbps.AutoSize = true;
            this.rb320kbps.Location = new System.Drawing.Point(426, 35);
            this.rb320kbps.Margin = new System.Windows.Forms.Padding(2);
            this.rb320kbps.Name = "rb320kbps";
            this.rb320kbps.Size = new System.Drawing.Size(89, 17);
            this.rb320kbps.TabIndex = 4;
            this.rb320kbps.Tag = "320kbps";
            this.rb320kbps.Text = "mp3 320kbps";
            this.rb320kbps.UseVisualStyleBackColor = true;
            // 
            // rb128kbps
            // 
            this.rb128kbps.AutoSize = true;
            this.rb128kbps.Location = new System.Drawing.Point(560, 35);
            this.rb128kbps.Margin = new System.Windows.Forms.Padding(2);
            this.rb128kbps.Name = "rb128kbps";
            this.rb128kbps.Size = new System.Drawing.Size(89, 17);
            this.rb128kbps.TabIndex = 5;
            this.rb128kbps.Tag = "128kbps";
            this.rb128kbps.Text = "mp3 128kbps";
            this.rb128kbps.UseVisualStyleBackColor = true;
            // 
            // lvSongs
            // 
            this.lvSongs.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvSongs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSongs.CheckBoxes = true;
            this.lvSongs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chChecked,
            this.chSong,
            this.chLink,
            this.chQuality,
            this.chLossless,
            this.chMaxQuality});
            this.lvSongs.FullRowSelect = true;
            this.lvSongs.GridLines = true;
            this.lvSongs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvSongs.Location = new System.Drawing.Point(9, 528);
            this.lvSongs.Margin = new System.Windows.Forms.Padding(2);
            this.lvSongs.Name = "lvSongs";
            this.lvSongs.ShowItemToolTips = true;
            this.lvSongs.Size = new System.Drawing.Size(1333, 191);
            this.lvSongs.TabIndex = 11;
            this.lvSongs.UseCompatibleStateImageBehavior = false;
            this.lvSongs.View = System.Windows.Forms.View.Details;
            this.lvSongs.ItemMouseHover += new System.Windows.Forms.ListViewItemMouseHoverEventHandler(this.lvSongs_ItemMouseHover);
            this.lvSongs.Leave += new System.EventHandler(this.lvSongs_Leave);
            this.lvSongs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvSongs_MouseClick);
            this.lvSongs.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lvSongs_MouseMove);
            // 
            // chChecked
            // 
            this.chChecked.Text = "";
            this.chChecked.Width = 24;
            // 
            // chSong
            // 
            this.chSong.Text = "Song Name";
            this.chSong.Width = 480;
            // 
            // chLink
            // 
            this.chLink.Text = "Link";
            this.chLink.Width = 840;
            // 
            // chQuality
            // 
            this.chQuality.Text = "Quality";
            // 
            // chLossless
            // 
            this.chLossless.Text = "Lossless?";
            this.chLossless.Width = 70;
            // 
            // chMaxQuality
            // 
            this.chMaxQuality.Text = "Max. Quality";
            this.chMaxQuality.Width = 80;
            // 
            // btnAllLinks
            // 
            this.btnAllLinks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAllLinks.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAllLinks.Location = new System.Drawing.Point(9, 500);
            this.btnAllLinks.Margin = new System.Windows.Forms.Padding(2);
            this.btnAllLinks.Name = "btnAllLinks";
            this.btnAllLinks.Size = new System.Drawing.Size(101, 22);
            this.btnAllLinks.TabIndex = 8;
            this.btnAllLinks.Text = "Select all Links";
            this.btnAllLinks.UseVisualStyleBackColor = true;
            this.btnAllLinks.Click += new System.EventHandler(this.btnAllLinks_Click);
            // 
            // btnQualifiedLinks
            // 
            this.btnQualifiedLinks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQualifiedLinks.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQualifiedLinks.Location = new System.Drawing.Point(147, 500);
            this.btnQualifiedLinks.Margin = new System.Windows.Forms.Padding(2);
            this.btnQualifiedLinks.Name = "btnQualifiedLinks";
            this.btnQualifiedLinks.Size = new System.Drawing.Size(152, 22);
            this.btnQualifiedLinks.TabIndex = 9;
            this.btnQualifiedLinks.Text = "Select Qualified Links";
            this.btnQualifiedLinks.UseVisualStyleBackColor = true;
            this.btnQualifiedLinks.Click += new System.EventHandler(this.btnQualifiedLinks_Click);
            // 
            // btnCopyLinks
            // 
            this.btnCopyLinks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopyLinks.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyLinks.Location = new System.Drawing.Point(336, 500);
            this.btnCopyLinks.Margin = new System.Windows.Forms.Padding(2);
            this.btnCopyLinks.Name = "btnCopyLinks";
            this.btnCopyLinks.Size = new System.Drawing.Size(152, 22);
            this.btnCopyLinks.TabIndex = 10;
            this.btnCopyLinks.Text = "Copy Selected Links";
            this.btnCopyLinks.UseVisualStyleBackColor = true;
            this.btnCopyLinks.Click += new System.EventHandler(this.btnCopyLinks_Click);
            // 
            // pbSpectrum
            // 
            this.pbSpectrum.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSpectrum.Location = new System.Drawing.Point(413, 205);
            this.pbSpectrum.Name = "pbSpectrum";
            this.pbSpectrum.Size = new System.Drawing.Size(587, 289);
            this.pbSpectrum.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbSpectrum.TabIndex = 12;
            this.pbSpectrum.TabStop = false;
            this.pbSpectrum.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 730);
            this.Controls.Add(this.pbSpectrum);
            this.Controls.Add(this.btnCopyLinks);
            this.Controls.Add(this.btnQualifiedLinks);
            this.Controls.Add(this.btnAllLinks);
            this.Controls.Add(this.lvSongs);
            this.Controls.Add(this.rb128kbps);
            this.Controls.Add(this.rb320kbps);
            this.Controls.Add(this.rb500kbps);
            this.Controls.Add(this.rbLossless);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.btnGetLinks);
            this.Controls.Add(this.txtResponse);
            this.Controls.Add(this.txtLink);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Tag = "500kbps";
            this.Text = "CSN Get Direct Links. Version 3.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pbSpectrum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLink;
        private System.Windows.Forms.TextBox txtResponse;
        private System.Windows.Forms.Button btnGetLinks;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbLossless;
        private System.Windows.Forms.RadioButton rb500kbps;
        private System.Windows.Forms.RadioButton rb320kbps;
        private System.Windows.Forms.RadioButton rb128kbps;
        private System.Windows.Forms.ListView lvSongs;
        private System.Windows.Forms.ColumnHeader chSong;
        private System.Windows.Forms.ColumnHeader chLossless;
        private System.Windows.Forms.ColumnHeader chQuality;
        private System.Windows.Forms.ColumnHeader chLink;
        private System.Windows.Forms.ColumnHeader chChecked;
        private System.Windows.Forms.ColumnHeader chMaxQuality;
        private System.Windows.Forms.Button btnAllLinks;
        private System.Windows.Forms.Button btnQualifiedLinks;
        private System.Windows.Forms.Button btnCopyLinks;
        private System.Windows.Forms.PictureBox pbSpectrum;
    }
}

