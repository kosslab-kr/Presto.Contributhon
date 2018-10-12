using Presto.Component.Controls;
using Presto.Component.Supports;
using Presto.Plugin.YouTube.Dialogs;
using Presto.Plugin.YouTube.Models;
using Presto.Plugin.YouTube.Utilities;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using YoutubeExplode;
using YoutubeExplode.Exceptions;

namespace Presto.Plugin.YouTube.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        #region 변수
        private bool _isProcessing = false;
        private double _progress;
        private double _maximum;
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

        public double Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
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
                RaiseCloseRequested();
            }
            else if (YoutubeClient.TryParseVideoId(Input, out string videoId))
            {
                // 비디오 분석
                try
                {
                    var video = await client.GetVideoAsync(videoId);

                    Status = video.Title;
                    Progress = default(double);
                    Maximum = 1 + (YouTubeUtility.IsEncodeRequired ? 0.5d : 0);
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
                    RaiseCloseRequested();
                }
                catch (VideoUnavailableException ex)
                {
                    switch (ex.Code)
                    {
                        case 150:
                            PrestoDialog.Show("저작권상의 이유로 해당 콘텐츠는 사용할 수 없습니다.", "동영상 오류");
                            break;

                        default:
                            PrestoDialog.Show(ex.Reason, "동영상 오류");
                            break;
                    }
                }
            }
            else
            {
                // 검색어 검색
                var results = await client.SearchVideosAsync(Input, 1);
                if (results.Count > 0)
                {
                    var resultDialog = new ResultDialog
                    {
                        DataContext = new ResultViewModel
                        {
                            Videos = results
                        }
                    };

                    resultDialog.Show();
                    RaiseCloseRequested();
                }
                else
                {
                    PrestoDialog.Show("입력된 검색어에 해당하는 동영상이 없습니다.", "동영상 검색 결과");
                }
            }

            IsProcessing = false;
        }

        private bool Search_CanExecute(object obj)
        {
            return !IsProcessing && !string.IsNullOrWhiteSpace(Input);
        }
        #endregion
    }
}
