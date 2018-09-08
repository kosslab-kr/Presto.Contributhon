using Presto.Component.Controls;
using Presto.Plugin.YouTube.ViewModels;
using System;

namespace Presto.Plugin.YouTube.Dialogs
{
    /// <summary>
    /// AddDialog.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AddDialog : PrestoWindow
    {
        public AddDialog()
        {
            InitializeComponent();
            DataContext = new AddViewModel();
            (DataContext as AddViewModel).CloseRequested += AddViewModel_CloseRequested;
        }

        private void AddViewModel_CloseRequested(object sender, EventArgs e)
        {
            Close();
        }
    }
}
