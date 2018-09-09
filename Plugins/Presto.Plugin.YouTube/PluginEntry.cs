using Presto.Common;
using Presto.Plugin.YouTube.Dialogs;

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
    }
}
