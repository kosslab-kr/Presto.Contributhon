using Presto.Plugin.YouTube.Dialogs;
using Presto.Plugin.YouTube.Supports;
using System.Collections.Generic;
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
        public ICommand Add { get; }

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
            Add = new DelegateCommand(Add_Execute);
            Cancel = new DelegateCommand(Cancel_Execute);
        }
        #endregion

        #region 커멘드 함수
        public void Add_Execute(object obj)
        {

        }

        public void Cancel_Execute(object obj)
        {
            new SearchDialog().Show();
            RaiseCloseRequested();
        }
        #endregion
    }
}
