using Presto.Component.Controls;
using Presto.Component.Controls.Common;
using Presto.Plugin.YouTube.Supports;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using YoutubeExplode;
using YoutubeExplode.Models;

namespace Presto.Plugin.YouTube.ViewModels
{
    public class AddViewModel : BaseViewModel
    {
        #region 변수
        private bool _isProcessing = false;
        private string _url;
        #endregion

        #region 속성
        public ICommand Analyze { get; }

        public bool IsProcessing
        {
            get => _isProcessing;
            private set
            {
                _isProcessing = value;
                RaisePropertyChanged();
            }
        }

        public string Url
        {
            get => _url;
            set
            {
                _url = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 생성자
        public AddViewModel()
        {
            Analyze = new DelegateCommand(Analyze_Execute, Analyze_CanExecute);
        }
        #endregion

        #region 내부 함수
        private void ExtractVideo(Video video)
        {
            MessageBox.Show($"{video.Title}\n{video.Author}");
        }

        private void ExtractVideo(IEnumerable<Video> videos)
        {
            foreach (var video in videos)
            {
                ExtractVideo(video);
            }
        }
        #endregion

        #region 커멘드 함수
        private async void Analyze_Execute(object obj)
        {
            IsProcessing = true;
            var client = new YoutubeClient();

            if (YoutubeClient.TryParsePlaylistId(Url, out string playlistId))
            {
                // 재생목록 분석
                var playlist = await client.GetPlaylistAsync(playlistId);
                ExtractVideo(playlist.Videos);
                RaiseCloseRequested();
            }
            else if (YoutubeClient.TryParseVideoId(Url, out string videoId))
            {
                // 비디오 분석
                var video = await client.GetVideoAsync(videoId);
                ExtractVideo(video);
                RaiseCloseRequested();
            }
            else
            {
                // 분석 오류
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Url = null;
                    PrestoDialog.Show("입력한 주소는 올바른 동영상 또는 재생목록이 아닙니다.", "오류", PrestoDialogButton.OK);
                });
            }

            IsProcessing = false;
        }

        private bool Analyze_CanExecute(object obj)
        {
            return !IsProcessing;
        }
        #endregion
    }
}
