using HtmlAgilityPack;
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
    /// ArtistDetail.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ArtistDetail : UserControl
    {
        private MainWindow mainWindow;

        public IPlayerService Player => PrestoSDK.PrestoService.Player;
        public ILibraryService Library => PrestoSDK.PrestoService.Library;

        public ArtistDetail(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

            //findArtist();
        }

        private void findArtist()
        {
            
        }

        private void ArtistInfo_Click(object sender, RoutedEventArgs e)
        {
            ArtistDetailModel[] model;

            // 재생중이거나 정지상태이거나
            MessageBox.Show(Player.CurrentMusic.Artist.Name);
            var url = "https://ko.wikipedia.org/wiki/" + Player.CurrentMusic.Artist.Name;
            var web = new HtmlWeb();
            var doc = web.Load(url);

            if (doc == null)
            {
                
            }

            var documentNode = doc.DocumentNode;
            var tableNode = documentNode.SelectSingleNode("//table[contains(@class, 'infobox')]//tbody");
            var rowsNodesList = tableNode.SelectNodes("tr");
            var rowCount = 1;

            model = new ArtistDetailModel[rowsNodesList.Count];

            List<string> contentList = new List<string>();
            List<string> titleList = new List<string>();
            foreach (var row in rowsNodesList)
            {
                var td = row.SelectNodes("td");
                var th = row.SelectNodes("th");

                if (td != null && td.Count > 0 && th != null && th.Count > 0)
                {
                    var content = td[0].InnerText;
                    var title = th[0].InnerText;
                    titleList.Add(td[0].InnerText);
                    title = title.Replace("\r\n", "").Trim();

                    contentList.Add(td[0].InnerText);
                    content = content.Replace("\r\n", "").Trim();

                    model[rowCount] = new ArtistDetailModel(title, content);

                    rowCount++;
                }
            }

            artistDetail.ItemsSource = model; 
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
