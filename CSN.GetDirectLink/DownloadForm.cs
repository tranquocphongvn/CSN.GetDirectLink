using CSN.GetDirectLink;
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
        private List<String> directLinks = null;
        private bool stopped = true;
        private DownloaderPool downloaderPool = null;

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

            downloaderPool = new DownloaderPool();
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
            Utils.Proxies = GetProxiesList();
            txtConsole.Clear();

            List<Thread> threads = new List<Thread>();
            
            downloaderPool.Add(new DownloaderThread(prb1, lblFile1, lblProxy1, lblDownload1, lblSpeed1, lblTimeLeft1));
            downloaderPool.Add(new DownloaderThread(prb2, lblFile2, lblProxy2, lblDownload2, lblSpeed2, lblTimeLeft2));
            downloaderPool.Add(new DownloaderThread(prb3, lblFile3, lblProxy3, lblDownload3, lblSpeed3, lblTimeLeft3));

            int i = 0;
            while (i < directLinks.Count)
            {
                DownloaderThread downloaderThread = downloaderPool.Take();
                if (downloaderThread != null)
                {
                    string link = directLinks[i];
                    if (!Utils.DownloadedLinks.Contains(link))
                    {
                        AddDownloadSongToConsole(link);
                        downloaderThread.Downloader = new Downloader(link, txtDownloadFolder.Text.Trim());
                        Thread t = new Thread(downloaderThread.Download);
                        threads.Add(t);

                        t.Start();
                    }
                    else
                    {
                        AddDownloadSongToConsole(link + ". Downloaded Already!");
                    }
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

        private void StopDownloads()
        {
            stopped = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopDownloads();
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

        private void DownloadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopDownloads();
        }
    }
}
