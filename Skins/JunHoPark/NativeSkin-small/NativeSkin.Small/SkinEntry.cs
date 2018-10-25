using Presto.Common;
using Presto.SDK;
using System.Windows;
using Presto.Component;

[assembly: PrestoTitle("MySkin")]
[assembly: PrestoDescription("skintest")]
[assembly: PrestoAuthor("skin")]

namespace NativeSkin.Small
{
    public class SkinEntry : PrestoSkin
    {
        public override void OnLoad()
        {
            base.OnLoad();

#if !DEBUG
            MessageBox.Show(PrestoSDK.PrestoService.Library.Musics.Count.ToString());
#endif
        }

        protected override IPrestoWindow CreateWindow()
        {
            return new MainWindow();
        }
    }
}
