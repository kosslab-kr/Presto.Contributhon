using MediaToolkit;
using MediaToolkit.Model;
using Presto.Plugin.YouTube.Downloader;
using Presto.Plugin.YouTube.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Models;
using YoutubeExplode.Models.MediaStreams;

namespace Presto.Plugin.YouTube.Utilities
{
    static class YouTubeUtility
    {
        public static bool IsEncodeRequired
        {
            get
            {
                var version = Environment.OSVersion.Version;
                return version.Major <= 6 && version.Minor <= 1;
            }
        }

        public static async Task<Music> Download(Video video, IProgress<double> progress = null)
        {
            // 오디오 검색
            var client = new YoutubeClient();
            var infos = await client.GetVideoMediaStreamInfosAsync(video.Id);
            var audio = infos.Audio.Where(x => x.AudioEncoding == AudioEncoding.Aac).OrderByDescending(x => x.Bitrate).First();

            // 오디오 다운로드
            var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var downloadPath = Path.Combine(currentPath, "Downloads");

            if (!Directory.Exists(downloadPath))
                Directory.CreateDirectory(downloadPath);

            var filePath = Path.Combine(downloadPath, $"{Guid.NewGuid().ToString()}.m4a");
            await client.DownloadMediaStreamAsync(audio, filePath, progress);

            // 섬네일 다운로드
            var thumbResult = await ParallelDownloader.DownloadAsync(video.Thumbnails.HighResUrl, Path.GetTempFileName(), 2);
            if (thumbResult.IsSuccess)
            {
                // 레터 박스 제거
                using (var bitmap = BitmapUtility.DeleteLetterBox(thumbResult.FilePath))
                {
                    bitmap.Save(thumbResult.FilePath);
                }

                // 비율에 맞춰 자르기
                using (var bitmap = BitmapUtility.CenterCrop(thumbResult.FilePath))
                {
                    bitmap.Save(thumbResult.FilePath);
                }

                // 인코딩 필요 검사
                string encodePath = null;
                if (IsEncodeRequired)
                {
                    await Task.Run(() =>
                    {
                        var inputFile = new MediaFile(filePath);
                        var outputFile = new MediaFile(Path.Combine(downloadPath, $"{Path.GetFileNameWithoutExtension(filePath)}.mp3"));

                        using (var engine = new Engine())
                        {
                            engine.ConvertProgressEvent += (s, e) =>
                            {
                                progress.Report(1 + (e.ProcessedDuration.TotalMilliseconds / e.TotalDuration.TotalMilliseconds) * 0.5d);
                            };

                            engine.Convert(inputFile, outputFile);
                        }

                        // 기존 파일 삭제
                        File.Delete(inputFile.Filename);

                        // 경로 업데이트
                        encodePath = outputFile.Filename;
                    });
                }

                // 결과 반환
                return new Music
                {
                    Path = encodePath ?? filePath,
                    Picture = thumbResult.FilePath,
                    Title = video.Title,
                    Artist = video.Author,
                    Album = video.Title
                };
            }

            return null;
        }
    }
}
