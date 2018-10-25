using Microsoft.Win32;
using Presto.Common;
using Presto.Component.Supports;
using System.Windows.Input;

namespace Presto.Plugin.YouTube.Models
{
    public class Music : NotifyObject
    {
        #region 변수
        private string _path;
        private string _title;
        private string _album;
        private string _genre;
        private string _artist;
        private string _picture;
        #endregion

        #region 속성
        public string Path
        {
            get => _path;
            set
            {
                _path = value;
                RaisePropertyChanged();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        public string Album
        {
            get => _album;
            set
            {
                _album = value;
                RaisePropertyChanged();
            }
        }

        public string Genre
        {
            get => _genre;
            set
            {
                _genre = value;
                RaisePropertyChanged();
            }
        }

        public string Artist
        {
            get => _artist;
            set
            {
                _artist = value;
                RaisePropertyChanged();
            }
        }

        public string Picture
        {
            get => _picture;
            set
            {
                _picture = value;
                RaisePropertyChanged();
            }
        }

        public ICommand ChangePicture { get; }
        #endregion

        #region 생성자
        public Music()
        {
            ChangePicture = new DelegateCommand(ChangePicture_Execute);
        }
        #endregion

        #region 커멘드 함수
        private void ChangePicture_Execute(object obj)
        {
            var openDialog = new OpenFileDialog()
            {
                Filter = "이미지 파일 (*.jpg, *.jpeg, *.jpe, *.png)|*.jpg;*.jpeg;*.jpe;*.png"
            };

            if (openDialog.ShowDialog() == true)
            {
                Picture = openDialog.FileName;
            }
        }
        #endregion
    }
}
