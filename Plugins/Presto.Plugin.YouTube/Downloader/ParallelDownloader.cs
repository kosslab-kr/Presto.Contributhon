using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Presto.Plugin.YouTube.Downloader
{
    public static class ParallelDownloader
    {
        static ParallelDownloader()
        {
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.DefaultConnectionLimit = 100;
            ServicePointManager.MaxServicePointIdleTime = 1000;
        }

        public static DownloadResult Download(string url, string destination, int parallelCount = 0)
        {
            // 초기화
            if (parallelCount <= 0)
                parallelCount = Environment.ProcessorCount;

            var result = new DownloadResult
            {
                ParallelCount = parallelCount,
                FilePath = destination
            };

            try
            {
                // 파일 크기 읽기
                long responseLength;
                var headRequest = WebRequest.Create(url);
                headRequest.Method = "HEAD";

                using (var webResponse = headRequest.GetResponse())
                {
                    responseLength = long.Parse(webResponse.Headers.Get("Content-Length"));
                    result.Size = responseLength;
                }

                // 청크 크기 계산
                using (var fileStream = new FileStream(destination, FileMode.Create))
                {
                    var readRanges = new List<DownloadRange>();
                    var tempFilesDictionary = new ConcurrentDictionary<long, string>();
                    for (int chunk = 0; chunk < parallelCount - 1; chunk++)
                    {
                        var range = new DownloadRange
                        {
                            Start = chunk * (responseLength / parallelCount),
                            End = ((chunk + 1) * (responseLength / parallelCount)) - 1
                        };

                        readRanges.Add(range);
                    }

                    readRanges.Add(new DownloadRange
                    {
                        Start = readRanges.Any() ? readRanges.Last().End + 1 : 0,
                        End = responseLength - 1
                    });

                    // 청크 다운로드 시작
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();

                    Parallel.ForEach(readRanges, new ParallelOptions { MaxDegreeOfParallelism = parallelCount }, readRange =>
                    {
                        var getRequest = WebRequest.Create(url) as HttpWebRequest;
                        getRequest.Method = "GET";
                        getRequest.AddRange(readRange.Start, readRange.End);

                        using (var webResponse = getRequest.GetResponse())
                        {
                            string tempFilePath = Path.GetTempFileName();
                            using (var tempStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write, FileShare.Write))
                            {
                                webResponse.GetResponseStream().CopyTo(tempStream);
                                tempFilesDictionary.TryAdd(readRange.Start, tempFilePath);
                            }
                        }
                    });

                    stopwatch.Stop();
                    result.ElapsedTime = stopwatch.Elapsed;

                    // 데이터 병합
                    foreach (var tempFile in tempFilesDictionary.OrderBy(t => t.Key))
                    {
                        var tempFileBytes = File.ReadAllBytes(tempFile.Value);
                        fileStream.Write(tempFileBytes, 0, tempFileBytes.Length);
                        File.Delete(tempFile.Value);
                    }

                    result.IsSuccess = true;
                    return result;
                }
            }
            catch
            {
                return result;
            }
        }

        public static async Task<DownloadResult> DownloadAsync(string url, string destination, int parallelCount = 0)
        {
            return await Task.Run(() => Download(url, destination, parallelCount));
        }
    }
}
