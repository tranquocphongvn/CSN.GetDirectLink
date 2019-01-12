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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSN
{
    public partial class DownloadForm : Form
    {
        public delegate void AppendTextDelegate(TextBox txt, string text);

        //private List<String> proxies = null;
        Downloader downloader = null;
        private List<String> directLinks = null;
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
            btnStop.Enabled = false;
        }

        public void SetDirectLinks(List<String> links)
        {
            directLinks = links;
        }

        public void AppendText(TextBox txt, string text)
        {
            txt.AppendText(text);
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
            txtDownloadFolder.Text = KnownFolders.GetPath(KnownFolder.Downloads) + "\\Music_CSN_1";
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            Application.UseWaitCursor = true;
            stopped = false;
            btnStop.Enabled = true;
            btnDownload.Enabled = false;
            List<String> proxies = GetProxiesList();
            txtConsole.Clear();

            List<Thread> threads = new List<Thread>();
            DownloaderPool downloaderPool = new DownloaderPool();

            downloaderPool.Add(new DownloaderThread(prb1, lblFile1, lblDownload1, lblSpeed1, lblTimeLeft1));
            downloaderPool.Add(new DownloaderThread(prb2, lblFile2, lblDownload2, lblSpeed2, lblTimeLeft2));
            downloaderPool.Add(new DownloaderThread(prb3, lblFile3, lblDownload3, lblSpeed3, lblTimeLeft3));

            int i = 0;
            while (i < directLinks.Count)
            {
                DownloaderThread downloaderThread = downloaderPool.Take();
                if (downloaderThread != null)
                {
                    string link = directLinks[i];
                    AddDownloadSongToConsole(link);
                    downloaderThread.Downloader = new Downloader(link, txtDownloadFolder.Text.Trim());
                    Thread t = new Thread(new ThreadStart(downloaderThread.Download));
                    threads.Add(t);
                    t.Start();
                    i++;
                }
                else
                {
                    Application.DoEvents();
                }

                /*
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
                */
            }

            bool running = true;
            while (running)
            {
                bool allStopped = true;
                foreach (Thread t in threads)
                {
                    allStopped = allStopped && ((t.ThreadState == ThreadState.Aborted) || (t.ThreadState == ThreadState.Stopped));
                }
                running = !allStopped;
                Application.DoEvents();
            }

            txtConsole.AppendText(Environment.NewLine);
            txtConsole.AppendText(" - DONE." + Environment.NewLine);

            btnDownload.Enabled = true;
            btnStop.Enabled = false;
            Application.UseWaitCursor = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            stopped = true;
            if (downloader != null)
                downloader.StopDownload();
            //this.Close();
        }

        private void AddDownloadSongToConsole(String directLink)
        {
            if (!string.IsNullOrEmpty(directLink))
            {
                lock (txtConsole)
                {
                    txtConsole.BeginInvoke(new AppendTextDelegate(AppendText), txtConsole, " - " + directLink + Environment.NewLine);
                }
            }

            Application.DoEvents();
        }

        private int DownloadFile(string directLink, WebProxy proxy = null)
        {
            downloader = new Downloader(directLink, txtDownloadFolder.Text.Trim(), proxy);
            downloader.CallingClass(new ConsoleLogger());
            downloader.Completed += Downloader_Completed;
            downloader.Error += Downloader_Error;
            downloader.ProgressChanged += Downloader_ProgressChanged;

            return downloader.Start();
        }

        private void Downloader_ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            int percentage = (int) (e.ProgressPercentage);
            if (percentage > 100)
                percentage = 100;
            if (percentage < 0)
                percentage = 0;
            prb1.Value = percentage;

            lblDownload1.Text = string.Format("D: {0:n}/{1:n} MB", e.BytesReceived / 1048576.0,  e.TotalBytesToReceive / 1048576.0);
            lblSpeed1.Text = string.Format("S: {0:n4} MB/s", e.CurrentSpeed / 1048576);
            lblTimeLeft1.Text = "L: " + e.TimeLeft.ToString(@"hh\:mm\:ss\.ffff");
            Application.DoEvents();
        }

        private void Downloader_Completed(object sender, EventArgs e)
        {
            Downloader downloader = (Downloader)sender;
            txtConsole.AppendText(" ... Proxy: [" + downloader.GetProxyAddress() + "]. DONE." + Environment.NewLine);
            Application.DoEvents();
        }

        private void Downloader_Error(object sender, ErrorEventArgs e)
        {
            Downloader downloader = (Downloader)sender;
            downloader.Rename();
            txtConsole.AppendText(" ... Proxy: [" + downloader.GetProxyAddress() + "]. ERROR: " + e.ErrorMessage + Environment.NewLine);
            Application.DoEvents();
        }

        private void DownloadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            stopped = true;
            if (downloader != null)
                downloader.StopDownload();
        }
    }
}
