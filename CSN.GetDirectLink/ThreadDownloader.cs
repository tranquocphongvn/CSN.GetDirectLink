using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSN
{
    public class ThreadDownloader
    {
        private Downloader downloader;
        private ProgressBar progressBar;
        private Label lblFilename, lblDownload, lblSpeed, lblTimeLeft;
        public ThreadDownloader(Downloader downloader, ProgressBar progressBar, Label lblFilename, Label lblDownload, Label lblSpeed, Label lblTimeLeft)
        {
            this.downloader = downloader;
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
            this.downloader.CallingClass(new ConsoleLogger());
            this.downloader.ProgressChanged += Downloader_ProgressChanged;
        }

        private void Downloader_ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            int percentage = (int)(e.ProgressPercentage);
            if (percentage > 100)
                percentage = 100;
            else if (percentage < 0)
                percentage = 0;
            progressBar.Value = percentage;

            lblDownload.Text = string.Format("D: {0:n}/{1:n} MB", e.BytesReceived / 1048576.0, e.TotalBytesToReceive / 1048576.0);
            lblSpeed.Text = string.Format("S: {0:n4} MB/s", e.CurrentSpeed / 1048576);
            lblTimeLeft.Text = "L: " + e.TimeLeft.ToString(@"hh\:mm\:ss\.ffff");
            Application.DoEvents();
        }

        private void Downloader_Error(object sender, ErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Downloader_Completed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
