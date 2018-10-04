using Presto.Common.Services;
using Presto.Common;
using Presto.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Presto.Common.Models;

namespace NativeSkin.Small
{
    /// <summary>
    /// Home.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Home : Page
    {
        public IPlayerService Player => PrestoSDK.PrestoService.Player;
        public ILibraryService Library => PrestoSDK.PrestoService.Library;

        public Home()
        {
            InitializeComponent();
            this.listView.Visibility = Visibility.Hidden;
            this.topBar_playList.Visibility = Visibility.Hidden;
        }

        private void ListviewBtn_Click(object sender, RoutedEventArgs e)
        {
            this.topBar.Visibility = Visibility.Hidden;
            this.album.Visibility = Visibility.Hidden;
            this.buttonList.Visibility = Visibility.Hidden;

            this.listView.Visibility = Visibility.Visible;
            this.topBar_playList.Visibility = Visibility.Visible;
        }

        private void ExitToApp_Click(object sender, RoutedEventArgs e)
        {
            this.topBar.Visibility = Visibility.Visible;
            this.album.Visibility = Visibility.Visible;
            this.buttonList.Visibility = Visibility.Visible;

            this.listView.Visibility = Visibility.Hidden;
            this.topBar_playList.Visibility = Visibility.Hidden;
        }

        private void Power_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

 
}
