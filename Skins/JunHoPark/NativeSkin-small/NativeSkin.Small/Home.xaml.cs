using Microsoft.Win32;
using Presto.Common;
using Presto.Common.Services;
using Presto.Component.Controls;
using Presto.Component.Controls.Common;
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
using HtmlAgilityPack;
using System.IO;
using System.Text.RegularExpressions;
using Presto.Component.Utilities;

namespace NativeSkin.Small
{
    /// <summary>
    /// HomeControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Home : UserControl
    {
        private MainWindow mainWindow;

        public IPlayerService Player => PrestoSDK.PrestoService.Player;
        public ILibraryService Library => PrestoSDK.PrestoService.Library;

        public Home(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            Player.RepeatMode = RepeatMode.All; // 전곡반복 세팅
            Player.PlaybackStateChanged += Player_PlaybackStateChanged;
        }

        private void Player_PlaybackStateChanged(object sender, PlaybackStateChangedEventArgs e)
        {
            CheckPlayingState();
        }

        private void Power_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ListView_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.ListViewControl();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            switch (Player.PlaybackState)
            {
                case Presto.Common.PlaybackState.Playing:
                    Player.Pause();
                    break;

                case Presto.Common.PlaybackState.Paused:
                    Player.Resume();
                    break;

                default:
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

        private void Repeat_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Player.RepeatMode.ToString());
            switch (Player.RepeatMode)
            {
                case RepeatMode.All:
                    Player.RepeatMode = RepeatMode.One;
                    this.repeatControl.Kind = MaterialDesignThemes.Wpf.PackIconKind.Rotate3d;
                    break;

               
                case RepeatMode.One:
                    Player.RepeatMode = RepeatMode.All;
                    this.repeatControl.Kind = MaterialDesignThemes.Wpf.PackIconKind.RotateRight;
                    break;
            }
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            //string filter = string.Join(";", PrestoSDK.PrestoService.Player.SupportExtensions.Select(ext => $"*.{ext}"));

            var dialog = new OpenFileDialog()
            {
                Filter = $"음악 파일|{string.Join(";", PrestoSDK.PrestoService.Player.SupportExtensions.Select(ext => $"*.{ext}"))}",
                Multiselect = true
            };

            if (dialog.ShowDialog() == false)
                return;

            var result = PrestoDialog.Show(
                $"{dialog.FileNames.Length} 곡 추가?",
                "곡 추가",
                PrestoDialogButton.YesNo);

            if (result != PrestoDialogResult.Yes)
                return;

            Task.Factory.StartNew(() =>
            {
                var musics = Library.LoadMusic(dialog.FileNames);
                Library.AddMusic(musics);
            });
        }

        private void Question_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.ArtistDetailControl();
        }

        private void CheckPlayingState()
        {
            if(Player.PlaybackState == PlaybackState.Playing)
            {
                this.musicControl.Kind = MaterialDesignThemes.Wpf.PackIconKind.Pause;
            } 
            else
            {
                this.musicControl.Kind = MaterialDesignThemes.Wpf.PackIconKind.Play;
            }
        }

        private void CaptionBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {   
            var window = VisualTreeHelper2.FindVisualParents<Window>(this).First();
            window.DragMove();
        }
    }
}
