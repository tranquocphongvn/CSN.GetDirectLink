using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSN
{
    public class DownloaderThread
    {
        public delegate void SetLabelTextDelegate(Label lbl, string text);
        public delegate void SetProgressBarValueDelegate(ProgressBar prb, int value);

        private Downloader downloader;
        private ProgressBar progressBar;
        private Label lblFilename, lblDownload, lblSpeed, lblTimeLeft;
        public DownloaderThread(ProgressBar progressBar, Label lblFilename, Label lblDownload, Label lblSpeed, Label lblTimeLeft)
        {
            Downloader = null;

            this.progressBar = progressBar;
            this.lblFilename = lblFilename;
            this.lblDownload = lblDownload;
            this.lblSpeed = lblSpeed;
            this.lblTimeLeft = lblTimeLeft;

            this.progressBar.Value = 0;
            this.lblFilename.Text = "F:";
            this.lblDownload.Text = "D: 0 MB/0 MB";
            this.lblSpeed.Text = "S: 0 MB/s";
            this.lblTimeLeft.Text = "L: 00:00:00.0000";
        }

        public DownloaderThread(Downloader downloader, ProgressBar progressBar, Label lblFilename, Label lblDownload, Label lblSpeed, Label lblTimeLeft) : this(progressBar, lblFilename, lblDownload, lblSpeed, lblTimeLeft)
        {
            Downloader = downloader;
        }


        public Downloader Downloader
        {
            get
            {
                return this.downloader;
            }
            set
            {
                this.downloader = value;
                if (this.downloader != null)
                {
                    //this.downloader.CallingClass(new ConsoleLogger());
                    this.downloader.ProgressChanged += Downloader_ProgressChanged;
                    this.downloader.Completed += Downloader_Completed;
                    this.downloader.Error += Downloader_Error;
                }
            }
        }

        public Thread GetCurrentThread()
        {
            return Thread.CurrentThread;
        }

        public void Download()
        {
            downloader.Start();
        }

        public void SetLabelText(Label lbl, string text)
        {
            lbl.Text = text;
        }

        public void SetProgressBarValue(ProgressBar prb, int value)
        {
            prb.Value = value;
        }


        private void Downloader_ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            int percentage = (int)(e.ProgressPercentage);
            if (percentage > 100)
                percentage = 100;
            else if (percentage < 0)
                percentage = 0;
            progressBar.BeginInvoke(new SetProgressBarValueDelegate(SetProgressBarValue), progressBar, percentage);

            lblFilename.BeginInvoke(new SetLabelTextDelegate(SetLabelText), lblFilename, downloader.Filename);
            lblDownload.BeginInvoke(new SetLabelTextDelegate(SetLabelText), lblDownload, string.Format("D: {0:n}/{1:n} MB", e.BytesReceived / 1048576.0, e.TotalBytesToReceive / 1048576.0));
            lblSpeed.BeginInvoke(new SetLabelTextDelegate(SetLabelText), lblSpeed, string.Format("S: {0:n4} MB/s", e.CurrentSpeed / 1048576));
            lblTimeLeft.BeginInvoke(new SetLabelTextDelegate(SetLabelText), lblTimeLeft, "L: " + e.TimeLeft.ToString(@"hh\:mm\:ss\.ffff"));

            Application.DoEvents();
        }

        private void Downloader_Error(object sender, ErrorEventArgs e)
        {
            if (Downloader != null && !Downloader.Stopped)
                Downloader.StopDownload();
            Downloader = null;
        }

        private void Downloader_Completed(object sender, EventArgs e)
        {
            if (Downloader != null && !Downloader.Stopped)
                Downloader.StopDownload();
            Downloader = null;
        }
    }
}
