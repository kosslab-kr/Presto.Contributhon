using Presto.Plugin.YouTube.Dialogs;
using Presto.Plugin.YouTube.Models;
using Presto.Plugin.YouTube.Supports;
using Presto.Plugin.YouTube.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using YoutubeExplode.Models;

namespace Presto.Plugin.YouTube.ViewModels
{
    public class ResultViewModel : BaseViewModel
    {
        #region 변수
        private Playlist _playlist;
        private IEnumerable<Video> _videos;
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
        #endregion

        #region 생성자
        public ResultViewModel()
        {
            Select = new DelegateCommand(Select_Execute);
            Cancel = new DelegateCommand(Cancel_Execute);
        }
        #endregion

        #region 커멘드 함수
        private async void Select_Execute(object obj)
        {
            if (obj is IEnumerable<object> items)
            {
                var results = new List<Music>();
                foreach (var video in items.OfType<Video>())
                {
                    var music = await YouTubeUtility.Download(video);
                    music.Album = Playlist?.Title;
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
        }

        private void Cancel_Execute(object obj)
        {
            new SearchDialog().Show();
            RaiseCloseRequested();
        }
        #endregion
    }
}
