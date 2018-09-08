using Presto.Plugin.YouTube.Dialogs;
using Presto.Plugin.YouTube.Models;
using Presto.Plugin.YouTube.Supports;
using System.Collections.Generic;
using System.Windows.Input;

namespace Presto.Plugin.YouTube.ViewModels
{
    public class AddViewModel : BaseViewModel
    {
        #region 변수
        private IEnumerable<Music> _musics;
        #endregion

        #region 속성
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
        #endregion

        #region 생성자
        public AddViewModel()
        {
            Cancel = new DelegateCommand(Cancel_Execute);
        }
        #endregion

        #region 커멘드 함수
        private void Cancel_Execute(object obj)
        {
            new SearchDialog().Show();
            RaiseCloseRequested();
        }
        #endregion
    }
}
