using Presto.Common;
using Presto.Common.Services;
using Presto.Plugin.YouTube.Dialogs;
using Presto.SDK;
using System.Linq;

[assembly: PrestoTitle("YouTube")]
[assembly: PrestoAuthor("Kodnix Software")]
[assembly: PrestoDescription("YouTube 영상을 라이브러리에 추가하는 기능을 제공합니다.")]

namespace Presto.Plugin.YouTube
{
    public class PluginEntry : PrestoPlugin
    {
        #region 변수
        private SearchDialog _searchDialog;
        #endregion

        #region 속성
        private IPlaylistService Playlist => PrestoSDK.PrestoService.Playlist;
        #endregion

        #region 내부 함수
        private void OpenDialog()
        {
            if (_searchDialog != null)
            {
                _searchDialog.Activate();
            }
            else
            {
                _searchDialog = new SearchDialog();
                _searchDialog.Closed += (s, e) =>
                {
                    _searchDialog = null;
                };

                _searchDialog.Show();
            }
        }
        #endregion

        public override void Load()
        {

        }

        public override void Unload()
        {
            _searchDialog?.Close();
            _searchDialog = null;
        }

        [PrestoMenu(PrestoKey.MenuLibrary, "음악 추가/YouTube에서 음악 추가", Priority = 2)]
        private void AddMusic()
        {
            PluginData.CurrentPlaylist = null;
            OpenDialog();
        }

        [PrestoMenu(PrestoKey.MenuPlaylistContent, "목록에 음악 추가/YouTube에서 음악 추가", Priority = 3)]
        private void AddMusicPlaylist()
        {
            PluginData.CurrentPlaylist = Playlist.Selector.SelectedItems.FirstOrDefault();
            if (PluginData.CurrentPlaylist != null)
            {
                OpenDialog();
            }
        }
    }
}
