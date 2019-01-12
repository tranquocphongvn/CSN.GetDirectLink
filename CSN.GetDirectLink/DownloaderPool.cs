using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSN
{
    class DownloaderPool
    {
        private List<DownloaderThread> pool = new List<DownloaderThread>();
        public void Add(DownloaderThread downloaderThread)
        {
            pool.Add(downloaderThread);
        }

        public DownloaderThread Take()
        {
            lock (pool)
            {
                foreach (DownloaderThread downloaderThread in pool)
                {
                    if (downloaderThread.Downloader == null)
                        return downloaderThread;
                }
                return null;
            }
        }
    }
}
