using System;

namespace Presto.Plugin.YouTube.Downloader
{
    public class DownloadResult
    {
        public long Size { get; set; }

        public string FilePath { get; set; }

        public TimeSpan ElapsedTime { get; set; }

        public int ParallelCount { get; set; }

        public bool IsSuccess { get; set; } = false;
    }
}
