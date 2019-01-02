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

        public bool stop = true; // by default stop is true
        public bool paused = false;
        SemaphoreSlim pauseLock = new SemaphoreSlim(1);

        string filename;
        string tempFilename;
        string saveDir;

        public int DownloadFile(string downloadLink, string dir, WebProxy proxy = null)
        {
            saveDir = dir;
            Uri uri = new Uri(downloadLink);
            filename = System.IO.Path.GetFileName(uri.LocalPath);

            tempFilename = filename + ".tmp";
            stop = false; // always set this bool to false, everytime this method is called

            var tempFileInfo = new FileInfo(dir + "\\" + tempFilename);
            long existingLength = 0;
            if (tempFileInfo.Exists)
                existingLength = tempFileInfo.Length;

            var request = (HttpWebRequest)HttpWebRequest.Create(downloadLink);
            request.Proxy = proxy;
            request.AddRange(existingLength);

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    long fileSize = existingLength + response.ContentLength; //response.ContentLength gives me the size that is remaining to be downloaded
                    bool downloadResumable; // need it for not sending any progress

                    if ((int)response.StatusCode == 206) //same as: response.StatusCode == HttpStatusCode.PartialContent
                    {
                        _logger.LogMessage("Resumable");
                        downloadResumable = true;
                    }
                    else // sometimes a server that supports partial content will lose its ability to send partial content(weird behavior) and thus the download will lose its resumability
                    {
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
                        while (!stop && (byteSize = stream.Read(downBuffer, 0, downBuffer.Length)) > 0)
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
                if (!stop) OnCompleted(EventArgs.Empty);
            }
            catch (WebException e)
            {
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
            return 0;
        }

        public void Pause()
        {
            if (!paused)
            {
                paused = true;
                // Note this cannot block for more than a moment
                // since the download thread doesn't keep the lock held
                pauseLock.Wait();
            }
        }

        public void Unpause()
        {
            if (paused)
            {
                paused = false;
                pauseLock.Release();
            }
        }

        public void StopDownload()
        {
            stop = true;
            this.Unpause();  // stop waiting on lock if needed
        }

        public void Rename()
        {
            if (File.Exists(saveDir + "\\" + tempFilename)) {
                if (!File.Exists(saveDir + "\\" + filename))
                {
                    File.Move(saveDir + "\\" + tempFilename, saveDir + "\\" + filename);
                }
            }
        }

        public bool ResumeUnsupportedWarning()
        {
            var result = MessageBox.Show("When trying to resume the download , Mackerel got a response from the server that it doesn't support resuming the download. It's possible that it's a temporary error of the server, and you will be able to resume the file at a later time, but at this time Mackerel can download this file from the beginning.\n\nDo you want to download this file from the beginning?", filename, MessageBoxButtons.YesNo);
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