using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSN
{
    public partial class DownloadForm : Form
    {
        private WebProxy mainProxy = null;
        //private List<String> proxies = null;
        private List<String> directLinks = null;
        Downloader downloader = null;
        private bool stopped = true;

        public DownloadForm()
        {
            InitializeComponent();

            StringBuilder proxies = new StringBuilder();
            proxies.AppendLine("117.2.155.139:9090");
            proxies.AppendLine("101.99.23.136:3128");
            proxies.AppendLine("42.115.26.154:8080");
            proxies.AppendLine("103.15.51.160:8080");
            proxies.AppendLine("27.68.131.218:8080");
            proxies.AppendLine("118.70.185.14:8080");
            proxies.AppendLine("14.188.166.19:8080");
            proxies.AppendLine("123.31.47.8:3128");
            proxies.AppendLine("116.105.225.104:9090");
            proxies.AppendLine("117.4.247.218:3128");
            proxies.AppendLine("113.161.90.156:3128");
            proxies.AppendLine("123.30.172.60:3128");

            txtProxies.Text = proxies.ToString();
            txtConsole.Clear();
        }

        public void SetDirectLinks(List<String> links)
        {
            directLinks = links;
        }

        private List<String> GetProxiesList()
        {
            List<String> proxies = null;
            if (chkUseProxy.Checked)
            {
                proxies = new List<string>(Regex.Split(txtProxies.Text, Environment.NewLine));
            }
            return proxies;
        }

        private void DownloadForm_Load(object sender, EventArgs e)
        {
            txtDownloadFolder.Text = KnownFolders.GetPath(KnownFolder.Downloads);
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            Application.UseWaitCursor = true;
            stopped = false;
            btnDownload.Enabled = false;
            List<String> proxies = GetProxiesList();
            txtConsole.Clear();

            foreach (String link in directLinks)
            {
                AddDownloadSongToConsole(link);
                int result = DownloadFile(link);
                string addr;
                while ((result == 403 || result == -1) && (proxies.Count() > 0))
                {
                    addr = proxies[0];
                    if (!String.IsNullOrEmpty(addr))
                    {
                        WebProxy proxy = new WebProxy(addr);
                        AddDownloadSongToConsole(link);
                        result = DownloadFile(link, proxy);
                    }
                    proxies.RemoveAt(0);
                }
                if (stopped)
                    break;
            }
            btnDownload.Enabled = true;
            Application.UseWaitCursor = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            stopped = true;
            if (downloader != null)
                downloader.StopDownload();
            this.Close();
        }

        private void AddDownloadSongToConsole(String directLink)
        {
            if (!string.IsNullOrEmpty(directLink))
            {
                txtConsole.AppendText(" - " + directLink);
            }

            Application.DoEvents();
        }

        private int DownloadFile(string directLink, WebProxy proxy = null)
        {
            downloader = new Downloader();
            downloader.CallingClass(new ConsoleLogger());
            downloader.Completed += Downloader_Completed;
            downloader.Error += Downloader_Error;
            downloader.ProgressChanged += Downloader_ProgressChanged;

            return downloader.DownloadFile(directLink, txtDownloadFolder.Text + "\\Music_Test", proxy);
        }

        private void Downloader_ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            int percentage = (int) (e.ProgressPercentage);
            if (percentage > 100)
                percentage = 100;
            if (percentage < 0)
                percentage = 0;
            prb1.Value = percentage;

            lblMB1.Text = Math.Round(e.BytesReceived / 1048576.0, 2) + "/" + Math.Round(e.TotalBytesToReceive / 1048576.0, 2) + " MB";
            lblSpeed1.Text = Math.Round(e.CurrentSpeed / 1048576, 4) + " MB/s";
            lblTimeLeft1.Text = e.TimeLeft.ToString(@"hh\:mm\:ss\.ffff");
            Application.DoEvents();
        }

        private void Downloader_Completed(object sender, EventArgs e)
        {
            Downloader downloader = (Downloader)sender;
            downloader.Rename();
            txtConsole.AppendText(" ... DONE." + Environment.NewLine);
            Application.DoEvents();
        }

        private void Downloader_Error(object sender, ErrorEventArgs e)
        {
            Downloader downloader = (Downloader)sender;
            downloader.Rename();
            txtConsole.AppendText(" ... Error: " + e.ErrorMessage + Environment.NewLine);
            Application.DoEvents();
        }
    }
}
