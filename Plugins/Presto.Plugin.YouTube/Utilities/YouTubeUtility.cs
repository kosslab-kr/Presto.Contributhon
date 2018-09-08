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
        public static async Task<Music> Download(Video video)
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

            var audioResult = ParallelDownloader.Download(audio.Url, Path.Combine(downloadPath, $"{Guid.NewGuid().ToString()}.m4a"), 2);
            if (audioResult.IsSuccess)
            {
                // 섬네일 다운로드
                var thumbResult = ParallelDownloader.Download(video.Thumbnails.HighResUrl, Path.GetTempFileName(), 2);
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

                    // 결과 반환
                    return new Music
                    {
                        Path = audioResult.FilePath,
                        Picture = thumbResult.FilePath,
                        Title = video.Title,
                        Artist = video.Author,
                        Album = "YouTube"
                    };
                }
            }

            return null;
        }
    }
}
