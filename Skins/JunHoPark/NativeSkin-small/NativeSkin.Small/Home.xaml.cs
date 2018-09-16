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
    /// Home.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }

        private void ListviewBtn_Click(object sender, RoutedEventArgs e)
        {
            PlayListView playListView = new PlayListView();
            this.NavigationService.Navigate(playListView);
        }
    }
}
