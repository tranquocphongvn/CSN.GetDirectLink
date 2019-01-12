using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace CSN
{
    public interface ILogger
    {
        void LogMessage(string message, params Object[] args);
    }

    public class ConsoleLogger : ILogger
    {
        public void LogMessage(string message, params Object[] args)
        {
            Console.WriteLine(string.Format(message, args));
        }
    }

    public class Downloader
    {
        private ILogger _logger;

        public void CallingClass(ILogger logger)
        {
            _logger = logger;
        }

        public event EventHandler<DownloadStatusChangedEventArgs> ResumablityChanged;
        public event EventHandler<DownloadProgressChangedEventArgs> ProgressChanged;
        public event EventHandler Completed;
        public event EventHandler<ErrorEventArgs> Error;

        private bool stopped = true; // by default stop is true
        private bool paused = false;
        SemaphoreSlim pauseLock = new SemaphoreSlim(1);

        private string filename;
        private string tempFilename;

        private string saveDirectory;
        private string downloadLink;
        private WebProxy proxy;

        public Downloader(string downloadLink, string saveDirectory, WebProxy proxy)
        {
            DownloadLink = downloadLink;
            SaveDirectory = new DirectoryInfo(saveDirectory.Trim()).FullName;
            Proxy = proxy;
        }

        public Downloader(string downloadLink, string saveDirectory) : this(downloadLink, saveDirectory, null)
        {
        }

        public bool Stopped { get => stopped; set => stopped = value; }
        public bool Paused { get => paused; set => paused = value; }

        public string DownloadLink { get => downloadLink; set => downloadLink = value; }
        public WebProxy Proxy { get => proxy; set => proxy = value; }
        public string SaveDirectory { get => saveDirectory; set => saveDirectory = value; }
        public string Filename { get => filename; set => filename = value; }
        public string TempFilename { get => tempFilename; set => tempFilename = value; }

        public int DownloadFile(string downloadLink, string saveDirectory, WebProxy proxy = null)
        {
            DownloadLink = downloadLink;
            SaveDirectory = new DirectoryInfo(saveDirectory.Trim()).FullName;
            Proxy = proxy;

            return Start();
        }

        public int Start()
        {
            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }

            Uri uri = new Uri(DownloadLink);
            Filename = System.IO.Path.GetFileName(uri.LocalPath);
            TempFilename = Filename + ".tmp";
            Stopped = false; // always set this bool to false, everytime this method is cVerycomplealled

            var tempFileInfo = new FileInfo(SaveDirectory + "\\" + TempFilename);
            long existingLength = 0;
            if (tempFileInfo.Exists)
                existingLength = tempFileInfo.Length;

            var request = (HttpWebRequest)HttpWebRequest.Create(downloadLink);
            request.Proxy = Proxy;
            request.AddRange(existingLength);

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    long fileSize = existingLength + response.ContentLength; //response.ContentLength gives me the size that is remaining to be downloaded
                    bool downloadResumable; // need it for not sending any progress

                    if ((int)response.StatusCode == 206) //same as: response.StatusCode == HttpStatusCode.PartialContent
                    {
                        if (_logger != null)
                            _logger.LogMessage("Resumable");
                        
                        downloadResumable = true;
                    }
                    else // sometimes a server that supports partial content will lose its ability to send partial content(weird behavior) and thus the download will lose its resumability
                    {
                        if (_logger != null)
                            _logger.LogMessage("Not Resumable");
                        if (existingLength > 0)
                        {
                            if (ResumeUnsupportedWarning() == false) // warn and ask for confirmation to continue if the half downloaded file is unresumable
                            {
                                return (int)response.StatusCode;
                            }
                        }
                        existingLength = 0;
                        downloadResumable = false;
                    }
                    OnResumabilityChanged(new DownloadStatusChangedEventArgs(downloadResumable));

                    using (var saveFileStream = tempFileInfo.Open(downloadResumable ? FileMode.Append : FileMode.Create, FileAccess.Write))
                    using (var stream = response.GetResponseStream())
                    {
                        byte[] downBuffer = new byte[4096];
                        int byteSize = 0;
                        long totalReceived = byteSize + existingLength;
                        var sw = Stopwatch.StartNew();
                        while (!Stopped && (byteSize = stream.Read(downBuffer, 0, downBuffer.Length)) > 0)
                        {
                            saveFileStream.Write(downBuffer, 0, byteSize);
                            totalReceived += byteSize;

                            float currentSpeed = totalReceived / (float)sw.Elapsed.TotalSeconds;
                            OnProgressChanged(new DownloadProgressChangedEventArgs(totalReceived, fileSize, (long)currentSpeed));

                            pauseLock.Wait();
                            pauseLock.Release();
                        }
                        sw.Stop();
                    }
                }
                if (!Stopped) OnCompleted(EventArgs.Empty);
            }
            catch (WebException e)
            {
                Stopped = true;
                HttpWebResponse response = (HttpWebResponse)e.Response;
                //MessageBox.Show(e.Message, filename);
                if (response != null)
                {
                    OnError(new ErrorEventArgs((int)response.StatusCode, e.Message));
                    return (int)response.StatusCode;
                }
                else
                {
                    OnError(new ErrorEventArgs(-1, e.Message));
                    return -1;
                }
            }
            Stopped = true;
            return 0;
        }

        public void Pause()
        {
            if (!Paused)
            {
                Paused = true;
                // Note this cannot block for more than a moment
                // since the download thread doesn't keep the lock held
                pauseLock.Wait();
            }
        }

        public void Unpause()
        {
            if (Paused)
            {
                Paused = false;
                pauseLock.Release();
            }
        }

        public void StopDownload()
        {
            Stopped = true;
            this.Unpause();  // stop waiting on lock if needed
        }

        public void Rename()
        {
            string tempFullPath = string.Format("{0}\\{1}", SaveDirectory, TempFilename);
            string newFullPath = string.Format("{0}\\{1}", SaveDirectory, Filename);
            if (File.Exists(tempFullPath)) {
                String name = Path.GetFileNameWithoutExtension(newFullPath);
                String ext = Path.GetExtension(newFullPath);
                int i = 1;
                while (File.Exists(newFullPath) && i <= 10000)
                {
                    newFullPath = string.Format("{0}\\{1}_{2}{3}", SaveDirectory, name, ++i, ext);
                }
                File.Move(tempFullPath, newFullPath);
            }
        }

        public string GetProxyAddress()
        {
            return (Proxy == null)? "NONE" : Proxy.Address.Authority;
        }

        public bool ResumeUnsupportedWarning()
        {
            var result = MessageBox.Show("When trying to resume the download , Mackerel got a response from the server that it doesn't support resuming the download. It's possible that it's a temporary error of the server, and you will be able to resume the file at a later time, but at this time Mackerel can download this file from the beginning.\n\nDo you want to download this file from the beginning?", Filename, MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected virtual void OnResumabilityChanged(DownloadStatusChangedEventArgs e)
        {
            var handler = ResumablityChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnProgressChanged(DownloadProgressChangedEventArgs e)
        {
            var handler = ProgressChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnCompleted(EventArgs e)
        {
            var handler = Completed;
            Rename();
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnError(ErrorEventArgs e)
        {
            var handler = Error;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }

    
    public class ErrorEventArgs : EventArgs
    {
        public ErrorEventArgs(int errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
        public int ErrorCode { get; private set; }
        public string ErrorMessage { get; private set; }
    }

    public class DownloadStatusChangedEventArgs : EventArgs
    {
        public DownloadStatusChangedEventArgs(bool canResume)
        {
            ResumeSupported = canResume;
        }
        public bool ResumeSupported { get; private set; }
    }

    public class DownloadProgressChangedEventArgs : EventArgs
    {
        public DownloadProgressChangedEventArgs(long totalReceived, long fileSize, long currentSpeed)
        {
            BytesReceived = totalReceived;
            TotalBytesToReceive = fileSize;
            CurrentSpeed = currentSpeed;
        }

        public long BytesReceived { get; private set; }
        public long TotalBytesToReceive { get; private set; }
        public float ProgressPercentage
        {
            get
            {
                return ((float)BytesReceived / (float)TotalBytesToReceive) * 100;
            }
        }
        public float CurrentSpeed { get; private set; } // in bytes
        public TimeSpan TimeLeft
        {
            get
            {
                var bytesRemainingtoBeReceived = TotalBytesToReceive - BytesReceived;
                return TimeSpan.FromSeconds(bytesRemainingtoBeReceived / CurrentSpeed);
            }
        }
    }
}