using Presto.Component.Controls;
using Presto.Component.Supports;
using System.Windows;

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
            DataContextChanged += AddDialog_DataContextChanged;
        }

        private void AddDialog_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            (e.NewValue as ViewModelBase).CloseRequested += delegate
            {
                Close();
            };
        }
    }
}
