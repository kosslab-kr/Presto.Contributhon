using Presto.Component.Controls;
using Presto.Component.Supports;
using Presto.Plugin.YouTube.ViewModels;
using System.Windows;

namespace Presto.Plugin.YouTube.Dialogs
{
    /// <summary>
    /// AddDialog.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SearchDialog : PrestoWindow
    {
        public SearchDialog()
        {
            InitializeComponent();
            DataContextChanged += SearchDialog_DataContextChanged;
            DataContext = new SearchViewModel();
        }

        private void SearchDialog_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            (e.NewValue as ViewModelBase).CloseRequested += delegate
            {
                Close();
            };
        }
    }
}
