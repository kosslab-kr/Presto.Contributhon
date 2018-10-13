using Presto.Component.Supports;
using Presto.Plugin.YouTube.Dialogs;
using Presto.Plugin.YouTube.Models;
using Presto.Plugin.YouTube.Properties;
using Presto.SDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;

using TagLibPicture = TagLib.Picture;

namespace Presto.Plugin.YouTube.ViewModels
{
    public class AddViewModel : ViewModelBase
    {
        #region 변수
        private IEnumerable<Music> _musics;
        #endregion

        #region 속성
        public ICommand Add { get; }

        public ICommand Cancel { get; }

        public IEnumerable<Music> Musics
        {
            get => _musics;
            set
            {
                _musics = value;
                RaisePropertyChanged();
            }
        }

        public List<string> Genres { get; } = new List<string>();
        #endregion

        #region 생성자
        public AddViewModel()
        {
            // 커멘드 초기화
            Add = new DelegateCommand(Add_Execute);
            Cancel = new DelegateCommand(Cancel_Execute);

            // 음악 장르 추가
            var genres = Encoding.Default.GetString(Resources.Genres).Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            Genres.AddRange(genres);
        }
        #endregion

        #region 커멘드 함수
        private void Add_Execute(object obj)
        {
            foreach (var music in Musics)
            {
                // 태그 정보 작성
                using (var audioFile = TagLib.File.Create(music.Path))
                {
                    audioFile.Tag.Title = music.Title;
                    audioFile.Tag.Album = music.Album;
                    audioFile.Tag.AlbumArtists = Pack(music.Artist);
                    audioFile.Tag.Performers = Pack(music.Artist);
                    audioFile.Tag.Pictures = File.Exists(music.Picture) ? new[] { new TagLibPicture(music.Picture) } : null;
                    audioFile.Tag.Genres = Pack(music.Genre);
                    audioFile.Save();
                }

                // 라이브러리 추가
                var prestoMusic = PrestoSDK.PrestoService?.Library.LoadMusic(music.Path);
                if (prestoMusic != null)
                {
                    PrestoSDK.PrestoService.Library.AddMusic(prestoMusic);

                    // 재생목록에도 추가
                    if (PluginData.CurrentPlaylist != null)
                    {
                        PluginData.CurrentPlaylist.AddMusic(prestoMusic);
                    }
                }
            }

            RaiseCloseRequested();
        }

        T[] Pack<T>(T item)
        {
            if (Equals(item, default(T)) || item == null)
                return null;

            return new T[] { item };
        }

        private void Cancel_Execute(object obj)
        {
            // 임시 파일 삭제
            foreach (var music in Musics)
            {
                File.Delete(music.Path);
            }

            new SearchDialog().Show();
            RaiseCloseRequested();
        }
        #endregion
    }
}
