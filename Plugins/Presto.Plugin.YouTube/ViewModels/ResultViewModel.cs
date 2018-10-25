using Presto.Component.Supports;
using Presto.Plugin.YouTube.Dialogs;
using Presto.Plugin.YouTube.Models;
using Presto.Plugin.YouTube.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using YoutubeExplode.Models;

namespace Presto.Plugin.YouTube.ViewModels
{
    public class ResultViewModel : ViewModelBase
    {
        #region 변수
        private Playlist _playlist;
        private IEnumerable<Video> _videos;
        private bool _isProcessing = false;
        private double _progress;
        private double _maximum;
        private string _status;
        #endregion

        #region 속성
        public ICommand Select { get; }

        public ICommand Cancel { get; }

        public Playlist Playlist
        {
            get => _playlist;
            set
            {
                _playlist = value;
                RaisePropertyChanged();
            }
        }

        public IEnumerable<Video> Videos
        {
            get => _videos;
            set
            {
                _videos = value;
                RaisePropertyChanged();
            }
        }

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
        #endregion

        #region 생성자
        public ResultViewModel()
        {
            // 커멘드 초기화
            Select = new DelegateCommand(Select_Execute, Select_CanExecute);
            Cancel = new DelegateCommand(Cancel_Execute);
        }
        #endregion

        #region 커멘드 함수
        private async void Select_Execute(object obj)
        {
            IsProcessing = true;

            if (obj is IEnumerable<object> items)
            {
                // 음악 다운로드
                var results = new List<Music>();
                foreach (var video in items.OfType<Video>())
                {
                    Status = video.Title;
                    Progress = default(double);
                    Maximum = 1 + (YouTubeUtility.IsEncodeRequired ? 0.5d : 0);
                    
                    var progressHandler = new Progress<double>(p =>
                    {
                        Progress = p;
                        Status = $"[{p / Maximum:P0}] {video.Title}";
                    });

                    var music = await YouTubeUtility.Download(video, progressHandler);
                    if (Playlist != null)
                    {
                        music.Album = Playlist.Title;
                    }

                    results.Add(music);
                }

                new AddDialog
                {
                    DataContext = new AddViewModel
                    {
                        Musics = results
                    }
                }.Show();
            }

            RaiseCloseRequested();
            IsProcessing = false;
        }

        private bool Select_CanExecute(object obj)
        {
            if (obj is IEnumerable<object> items)
            {
                return items.Count() > 0;
            }

            return false;
        }

        private void Cancel_Execute(object obj)
        {
            new SearchDialog().Show();
            RaiseCloseRequested();
        }
        #endregion
    }
}
