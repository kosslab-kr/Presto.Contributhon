using Presto.Component.Supports;
using Presto.Plugin.YouTube.Dialogs;
using Presto.Plugin.YouTube.Models;
using Presto.Plugin.YouTube.Utilities;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using YoutubeExplode;

namespace Presto.Plugin.YouTube.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        #region 변수
        private bool _isProcessing = false;
        private double _progress;
        private string _status;
        private string _input;
        #endregion

        #region 속성
        public ICommand Search { get; }

        public bool IsProcessing
        {
            get => _isProcessing;
            private set
            {
                _isProcessing = value;
                RaisePropertyChanged();
            }
        }

        public double Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                RaisePropertyChanged();
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                RaisePropertyChanged();
            }
        }

        public string Input
        {
            get => _input;
            set
            {
                _input = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 생성자
        public SearchViewModel()
        {
            // 커멘드 초기화
            Search = new DelegateCommand(Search_Execute, Search_CanExecute);
        }
        #endregion

        #region 커멘드 함수
        private async void Search_Execute(object obj)
        {
            IsProcessing = true;
            var client = new YoutubeClient();

            if (YoutubeClient.TryParsePlaylistId(Input, out string playlistId))
            {
                // 재생목록 분석
                var playlist = await client.GetPlaylistAsync(playlistId);
                var resultDialog = new ResultDialog
                {
                    DataContext = new ResultViewModel
                    {
                        Playlist = playlist,
                        Videos = playlist.Videos
                    }
                };

                resultDialog.Show();
            }
            else if (YoutubeClient.TryParseVideoId(Input, out string videoId))
            {
                // 비디오 분석
                var video = await client.GetVideoAsync(videoId);

                Status = video.Title;
                Progress = default(double);
                var progressHandler = new Progress<double>(p => Progress = p);

                var addDialog = new AddDialog
                {
                    DataContext = new AddViewModel
                    {
                        Musics = new List<Music>()
                        {
                            await YouTubeUtility.Download(video, progressHandler)
                        }
                    }
                };

                addDialog.Show();
            }
            else
            {
                // 검색어 검색
                var results = await client.SearchVideosAsync(Input, 1);
                var resultDialog = new ResultDialog
                {
                    DataContext = new ResultViewModel
                    {
                        Videos = results
                    }
                };

                resultDialog.Show();
            }

            RaiseCloseRequested();
            IsProcessing = false;
        }

        private bool Search_CanExecute(object obj)
        {
            return !IsProcessing && !string.IsNullOrWhiteSpace(Input);
        }
        #endregion
    }
}
