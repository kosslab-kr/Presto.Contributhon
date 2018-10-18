using Presto.Common.Models;
using Presto.Common.Services;
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

namespace NativeSkin.Small
{
    /// <summary>
    /// playListviewControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PlayListView : UserControl
    {
        private MainWindow mainWindow;

        public IPlayerService Player => PrestoSDK.PrestoService.Player;
        public ILibraryService Library => PrestoSDK.PrestoService.Library;

        public PlayListView(MainWindow window)
        {
            InitializeComponent();
            this.mainWindow = window;
        }

        private void ListView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var music = Library.LoadMusic(listview.SelectedItem.ToString());
            IMusic selectedMusic = listview.SelectedItem as IMusic;

            MessageBox.Show(selectedMusic.Title);

            Player.Play(selectedMusic);
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            switch (Player.PlaybackState)
            {
                case Presto.Common.PlaybackState.Playing:
                    this.musicControl.Kind = MaterialDesignThemes.Wpf.PackIconKind.Play;
                    Player.Pause();
                    break;

                case Presto.Common.PlaybackState.Paused:
                    this.musicControl.Kind = MaterialDesignThemes.Wpf.PackIconKind.Pause;
                    Player.Play();
                    break;

                default:
                    this.musicControl.Kind = MaterialDesignThemes.Wpf.PackIconKind.Pause;
                    Player.Play();
                    break;
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            Player.PlayNext();
        }

        private void Before_Click(object sender, RoutedEventArgs e)
        {
            Player.PlayPrevious();
        }

        private void BackHome_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.HomeControl();
        }

        private void Power_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
