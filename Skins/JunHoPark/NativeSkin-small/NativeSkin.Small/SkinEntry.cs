using Presto.Common;
using Presto.SDK;
using System.Windows;

[assembly: PrestoTitle("NativeSkin.Small")]
[assembly: PrestoDescription("스킨 테스트 입니다")]
[assembly: PrestoAuthor("프레스토")]

namespace NativeSkin.Small
{
    public class SkinEntry : PrestoSkin
    {
        public override void Load()
        {
            base.Load();

            //MessageBox.Show(PrestoSDK.PrestoService.Library.Musics.Count.ToString());
        }

        protected override IPrestoWindow CreateWindow()
        {
            return new MainWindow();
        }
    }
}
