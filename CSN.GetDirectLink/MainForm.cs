﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace CSN.GetDirectLink
{
    public partial class MainForm : Form
    {
        private string albumName = string.Empty;
        private string requestSongQuality = string.Empty;

        private ToolTip ttLvSongs;
        private Point ptLastMousePoint;
        private bool listLoading = false;
        private bool stopped = true;

        public MainForm()
        {
            InitializeComponent();
            
            txtConsole.Text = string.Empty;
            txtResponse.Text = string.Empty;

            ttLvSongs = new ToolTip();
            ptLastMousePoint = new Point(-1, -1);
            listLoading = false;
            this.Text = Utils.COPYRIGHT;
        }

        private string GetHttpWebResponse(string url, WebProxy proxy = null)
        {
            txtConsole.AppendText("Get Http Web Response from: " + url);
            txtConsole.AppendText(Environment.NewLine);
            Application.DoEvents();

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "GET";
            if (proxy != null)
                myRequest.Proxy = proxy;

            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            string result = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();

            return result;
        }


        private List<string> GetSongUrls(HtmlAgilityPack.HtmlDocument htmlDoc)
        {
            List<string> songUrls = new List<string>();

            HtmlNode albumNode = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='playlist']//th//span");

            string album = string.Empty;
            if (albumNode != null)
            {
                album = albumNode.InnerHtml;
                album = album.Substring(album.IndexOf(":") + 2);
            }

            albumName = album;

            var songNodes = htmlDoc.DocumentNode.SelectNodes("//div[@id='playlist']//a[@class='musictitle']/preceding-sibling::a[last()]");

            txtConsole.Text = string.Empty;
            if (albumNode != null)
            {
                txtConsole.AppendText(albumNode.InnerHtml);
                txtConsole.AppendText(Environment.NewLine);
            }

            if (songNodes != null)
            {
                foreach (var songNode in songNodes)
                {
                    if (songNode != null)
                    {
                        string songUrl = songNode.GetAttributeValue("href", string.Empty);
                        if (!string.IsNullOrEmpty(songUrl))
                        {
                            txtConsole.AppendText(songUrl);
                            txtConsole.AppendText(Environment.NewLine);
                            songUrls.Add(songUrl);
                        }
                    }
                }
            }
            else // get download link from a song link
            {
                HtmlNode songNode = htmlDoc.DocumentNode.SelectSingleNode("//img[@alt='Downloads']/parent::a");
                if (songNode != null)
                {
                    string songUrl = songNode.GetAttributeValue("href", string.Empty);
                    if (!string.IsNullOrEmpty(songUrl))
                    {
                        txtConsole.AppendText(songUrl);
                        txtConsole.AppendText(Environment.NewLine);
                        songUrls.Add(songUrl);
                    }
                }
            }

            return songUrls;
        }

        // Song Quality: Lossless, 500kbps, 320kbps, 128kbps
        private SongDetail GetSongDetail(HtmlAgilityPack.HtmlDocument htmlDoc, string songQuality, bool includeSpectrum)
        {
            SongDetail song = new SongDetail();

            HtmlNode nameNode = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='plt-text']//a[last()]");
            song.FullName = nameNode.InnerHtml;
            HtmlNode losslessNode = htmlDoc.DocumentNode.SelectSingleNode("//img[contains(@src, '/lossless_checked/')]");
            song.VerifiedLossless = (losslessNode != null);
        
            foreach (string quality in Utils.SongQuality)
            {
                HtmlNode maxQualityNode = htmlDoc.DocumentNode.SelectSingleNode("//span[text()='" + quality + "']/parent::a");
                if (maxQualityNode != null)
                {
                    song.MaximumQuality = quality;
                    break;
                }
            }

            HtmlNode downloadNode = htmlDoc.DocumentNode.SelectSingleNode("//span[text()='" + songQuality + "']/parent::a");
            if (downloadNode != null)
            {
                song.DownloadURL = downloadNode.GetAttributeValue("href", string.Empty);
                song.Quality = songQuality;
                if (song.VerifiedLossless)
                    song.MaximumQuality = Utils.FLAC_LOSSLESS;
            }
            else
            {
                downloadNode = htmlDoc.DocumentNode.SelectSingleNode("//span[text()='" + song.MaximumQuality + "']/parent::a");
                if (downloadNode != null)
                {
                    song.DownloadURL = downloadNode.GetAttributeValue("href", string.Empty);
                    if (song.MaximumQuality == Utils.MP3_320kbps)
                    {
                        if (songQuality == Utils.FLAC_LOSSLESS)
                        {
                            song.DownloadURL = song.DownloadURL.Replace("/320/", "/flac/").Replace("[320kbps_MP3].mp3", "[Lossless_FLAC].flac");
                        }
                        else if (songQuality == Utils.M4A_500kbps)
                        {
                            song.DownloadURL = song.DownloadURL.Replace("/320/", "/m4a/").Replace("[320kbps_MP3].mp3", "[500kbps_M4A].m4a");
                        }
                        song.Quality = songQuality;
                    }
                    else
                    {
                        song.Quality = song.MaximumQuality;
                    }
                    if (song.VerifiedLossless)
                        song.MaximumQuality = Utils.FLAC_LOSSLESS;
                }
            }
            song.DownloadURL = song.DownloadURL.Replace(" ", "%20"); // added to fix bug, cannot download from batch urls

            if (includeSpectrum && !song.VerifiedLossless && (song.MaximumQuality == Utils.FLAC_LOSSLESS))
            {
                HtmlNode spectrumNode = htmlDoc.DocumentNode.SelectSingleNode("//img[contains(@src, '/spectrum/')]");
                if (spectrumNode != null) // fixed nullpointer error
                {
                    string spectrumUrl = spectrumNode.GetAttributeValue("src", string.Empty);

                    MemoryStream ms = null;
                    WebResponse myResponse = null;
                    try
                    {
                        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(spectrumUrl);
                        myRequest.Method = "GET";
                        myResponse = myRequest.GetResponse();
                        ms = new MemoryStream();
                        myResponse.GetResponseStream().CopyTo(ms);
                        song.Spectrum = ms.ToArray();
                        //song.Spectrum = Utils.ReadFully(myResponse.GetResponseStream());
                    }
                    catch (Exception ex)
                    {
                        txtConsole.AppendText("===========================");
                        txtConsole.AppendText(Environment.NewLine);
                        txtConsole.AppendText(ex.ToString());
                        txtConsole.AppendText(Environment.NewLine);
                        txtConsole.AppendText("===========================");
                        txtConsole.AppendText(Environment.NewLine);
                    }
                    finally
                    {
                        if (ms != null)
                            ms.Close();
                        if (myResponse != null)
                            myResponse.Close();
                    }
                }
            }

            return song;
        }

        private void EnableButtons(bool enable)
        {
            //btnGetLinks.Enabled = enable;
            btnGetLinks.Text = enable ? "Get Links" : "Stop";

            btnAllLinks.Enabled = enable;
            btnCopyLinks.Enabled = enable;
            btnQualifiedLinks.Enabled = enable;
            btnDownload.Enabled = enable;
        }

        private void btnGetLinks_Click(object sender, EventArgs e)
        {
            Application.UseWaitCursor = true;
            Application.DoEvents();

            EnableButtons(false);
            if (stopped)
            {
                listLoading = true;
                txtConsole.Text = string.Empty;
                txtResponse.Text = string.Empty;
                lvSongs.Items.Clear();
                stopped = false;
                try
                {
                    //txtLink.Text = "http://m2.chiasenhac.vn/mp3/vietnam/v-pop/tam-su-tuoi-30~trinh-thang-binh~tsvcsc0dqv4vnm.html";
                    string response = GetHttpWebResponse(txtLink.Text, null);

                    txtResponse.Text = response;
                    HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
                    htmlDoc.LoadHtml(response);

                    // Song Quality: Lossless, 500kbps, 320kbps, 128kbps
                    List<RadioButton> rbQuality = new List<RadioButton> { rbLossless, rb500kbps, rb320kbps, rb128kbps };
                    requestSongQuality = Utils.M4A_500kbps;
                    foreach (RadioButton rb in rbQuality)
                    {
                        if (rb.Checked)
                            requestSongQuality = (string)rb.Tag;
                    }

                    List<string> songUrls = GetSongUrls(htmlDoc);

                    txtConsole.AppendText(Environment.NewLine);
                    txtConsole.AppendText("============================================");
                    txtConsole.AppendText(Environment.NewLine);
                    txtConsole.AppendText(albumName);
                    txtConsole.AppendText(Environment.NewLine);
                    txtConsole.AppendText("Maximum request song quality: " + requestSongQuality);
                    txtConsole.AppendText(Environment.NewLine);
                    txtConsole.AppendText("Songs list:");
                    txtConsole.AppendText(Environment.NewLine);
                    txtConsole.AppendText("========================");
                    txtConsole.AppendText(Environment.NewLine);

                    List<SongDetail> songs = new List<SongDetail>();
                    foreach (string songUrl in songUrls)
                    {
                        if (!string.IsNullOrEmpty(songUrl))
                        {
                            response = GetHttpWebResponse(songUrl, null);
                            txtResponse.Text = response;
                            htmlDoc = new HtmlAgilityPack.HtmlDocument();
                            htmlDoc.LoadHtml(response);

                            SongDetail song = GetSongDetail(htmlDoc, requestSongQuality, true);
                            songs.Add(song);

                            AddSongDetailToConsole(song, requestSongQuality);
                            AddSongDetailToListView(song, requestSongQuality);

                        }

                        if (stopped)
                            break;
                    }
                }
                catch (Exception ex)
                {
                    txtConsole.AppendText("===========================");
                    txtConsole.AppendText(Environment.NewLine);
                    txtConsole.AppendText(ex.ToString());
                    txtConsole.AppendText(Environment.NewLine);
                    txtConsole.AppendText("===========================");
                    txtConsole.AppendText(Environment.NewLine);

                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    txtConsole.AppendText("============================================");
                    txtConsole.AppendText(Environment.NewLine);
                    txtConsole.AppendText(Environment.NewLine);
                    txtConsole.AppendText("Click on the '" + Utils.NOT_SURE + "' text (under Lossless? column) to view Spectrum of the song.");
                    txtConsole.AppendText(Environment.NewLine);
                    txtConsole.AppendText(Environment.NewLine);
                    txtConsole.AppendText("============================================");
                    txtConsole.AppendText(Environment.NewLine);
                    txtConsole.AppendText(Utils.COPYRIGHT);
                    txtConsole.AppendText(Environment.NewLine);
                }
            }
            stopped = true;
            EnableButtons(true);
            listLoading = false;
            Application.UseWaitCursor = false;
            Application.DoEvents();
        }

        private void AddSongDetailToConsole(SongDetail song, string requestSongQuality)
        {
            if (!string.IsNullOrEmpty(song.FullName))
            {
                txtConsole.AppendText(" - " + song.FullName);
                txtConsole.AppendText(Environment.NewLine);
                string lossless = song.VerifiedLossless ? Utils.VERIFIED : (song.MaximumQuality == Utils.FLAC_LOSSLESS ? Utils.NOT_SURE : Utils.NONE);
                txtConsole.AppendText("           - Request Quality: " + requestSongQuality + ". Maximum Quality: " + song.MaximumQuality + ". Lossless: " + lossless);
            }
            txtConsole.AppendText(Environment.NewLine);

            Application.DoEvents();
        }

        private void AddSongDetailToListView(SongDetail song, string requestSongQuality)
        {
            ListViewItem item = new ListViewItem();
            item.UseItemStyleForSubItems = false;

            Color foreColor = (song.Quality == requestSongQuality) ? Color.DarkBlue : (song.Quality == Utils.MP3_128kbps ? Color.DimGray : Color.DarkGreen);
            string lossless = song.VerifiedLossless ? Utils.VERIFIED : (song.MaximumQuality == Utils.FLAC_LOSSLESS ? Utils.NOT_SURE : Utils.NONE);
            Font losslessFont = null;
            // Bold if Verified Lossless
            if (lossless == Utils.VERIFIED)
                losslessFont = new Font(lvSongs.Font, FontStyle.Bold);
            else if (lossless == Utils.NONE)
                losslessFont = new Font(lvSongs.Font, FontStyle.Italic);
            else
                losslessFont = lvSongs.Font;

            item.SubItems.Add(song.FullName, foreColor, lvSongs.BackColor, lvSongs.Font);
            item.SubItems.Add(song.DownloadURL, foreColor, lvSongs.BackColor, lvSongs.Font);
            item.SubItems.Add(song.Quality, foreColor, lvSongs.BackColor, lvSongs.Font);
            item.SubItems.Add(lossless, foreColor, lvSongs.BackColor, losslessFont);


            if (song.MaximumQuality == Utils.FLAC_LOSSLESS)
                losslessFont = new Font(lvSongs.Font, FontStyle.Bold);
            else if (song.MaximumQuality == Utils.MP3_320kbps)
                losslessFont = new Font(lvSongs.Font, FontStyle.Italic);
            else if (song.MaximumQuality == Utils.MP3_128kbps)
                losslessFont = new Font(lvSongs.Font, FontStyle.Strikeout);
            else
                losslessFont = lvSongs.Font;
            item.SubItems.Add(song.MaximumQuality, foreColor, lvSongs.BackColor, losslessFont);

            item.ToolTipText = "Click on the '" + Utils.NOT_SURE + "' text (under Lossless? column) to view Spectrum of the song.";
            item.Checked = (requestSongQuality == song.Quality);
            item.Tag = song.Spectrum;

            lvSongs.Items.Add(item);
            Application.DoEvents();
            item.EnsureVisible();
            Application.DoEvents();
        }

        private void txtLink_TextChanged(object sender, EventArgs e)
        {
            btnGetLinks.Enabled = !string.IsNullOrEmpty(txtLink.Text);
        }

        private void btnAllLinks_Click(object sender, EventArgs e)
        {
            if (lvSongs.CheckedItems.Count == lvSongs.Items.Count)
            {
                foreach (ListViewItem item in lvSongs.Items)
                {
                    item.Checked = false;
                }
            }
            else
            {
                StringBuilder console = new StringBuilder(string.Empty);
                console.AppendLine(albumName);
                console.AppendLine("Songs list:");
                console.AppendLine("========================");

                foreach (ListViewItem item in lvSongs.Items)
                {
                    console.AppendLine(item.SubItems[2].Text);
                    item.Checked = true;
                }
                console.AppendLine("========================");
                console.AppendLine(Utils.COPYRIGHT);
                txtConsole.Text = console.ToString();
            }
        }

        private void btnQualifiedLinks_Click(object sender, EventArgs e)
        {
            StringBuilder console = new StringBuilder(string.Empty);
            console.AppendLine(albumName);
            console.AppendLine("Songs list:");
            console.AppendLine("========================");

            foreach (ListViewItem item in lvSongs.Items)
            {
                if (requestSongQuality == item.SubItems[3].Text)
                {
                    console.AppendLine(item.SubItems[2].Text);
                    item.Checked = true;
                }
                else
                    item.Checked = false;
            }
            console.AppendLine("========================");
            console.AppendLine(Utils.COPYRIGHT);
            txtConsole.Text = console.ToString();
        }

        private void btnCopyLinks_Click(object sender, EventArgs e)
        {
            StringBuilder links = new StringBuilder(string.Empty);
            foreach (ListViewItem item in lvSongs.Items)
            {
                if (item.Checked)
                {
                    links.AppendLine(item.SubItems[2].Text);
                }
            }

            StringBuilder console = new StringBuilder(string.Empty);
            console.AppendLine(albumName);
            console.AppendLine("Songs list:");
            console.AppendLine("========================");
            console.AppendLine(links.ToString());
            console.AppendLine("========================");
            console.AppendLine(Utils.COPYRIGHT);
            txtConsole.Text = console.ToString();

            links.AppendLine("");
            links.AppendLine(Utils.COPYRIGHT);
            if (links.Length > 0)
                Clipboard.SetText(links.ToString());
        }

        private void lvSongs_MouseClick(object sender, MouseEventArgs e)
        {
            if (!listLoading)
            {
                ttLvSongs.Hide(lvSongs);
                ListViewHitTestInfo info = lvSongs.HitTest(e.X, e.Y);
                if (info.Item != null && info.SubItem != null && Utils.NOT_SURE == info.SubItem.Text)
                {
                    byte[] spectrum = (byte[])info.Item.Tag;
                    if (spectrum != null)
                    {
                        pbSpectrum.Image = Utils.byteArrayToImage(spectrum);
                        pbSpectrum.Visible = true;
                    }
                }
            }
        }

        private void lvSongs_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            if (!listLoading)
            {
                if (!string.IsNullOrEmpty(e.Item.ToolTipText))
                    ttLvSongs.Show(e.Item.ToolTipText, lvSongs, ptLastMousePoint.X, ptLastMousePoint.Y + 20);
                if (pbSpectrum.Visible)
                    pbSpectrum.Visible = false;
            }
        }

        private void lvSongs_MouseMove(object sender, MouseEventArgs e)
        {
            if (!listLoading)
                ptLastMousePoint = e.Location;
        }

        private void lvSongs_Leave(object sender, EventArgs e)
        {
            ttLvSongs.Hide(lvSongs);
            if (pbSpectrum.Visible)
                pbSpectrum.Visible = false;
        }

        private void btnProxy_Click(object sender, EventArgs e)
        {
            ProxyForm form = new ProxyForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
            }
            form.Dispose();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            List<String> links = new List<String>();
            foreach (ListViewItem item in lvSongs.Items)
            {
                if (item.Checked)
                {
                    links.Add(item.SubItems[2].Text);
                }
            }

            if (links.Count > 0)
            {
                DownloadForm form = new DownloadForm();
                form.SetDirectLinks(links);
                form.ShowDialog();
                form.Dispose();
            }
            else
            {
                MessageBox.Show("Please selecte any songs!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            stopped = true;
        }
    }
}
