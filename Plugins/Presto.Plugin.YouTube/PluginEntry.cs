using Presto.Common;
using Presto.Plugin.YouTube;
using Presto.Plugin.YouTube.Dialogs;

[assembly: PrestoTitle("YouTube")]
[assembly: PrestoAuthor("Kodnix Software")]
[assembly: PrestoDescription("YouTube 영상을 라이브러리에 추가하는 기능을 제공합니다.")]
[assembly: PrestoPlugin(typeof(PluginEntry))]

namespace Presto.Plugin.YouTube
{
    public class PluginEntry
    {
        public static void AddVideo()
        {
            new AddDialog().Show();
        }

        [PrestoEntry]
        public static void Initialize()
        {
            // TODO 변경된 모듈 시스템 대응 필요
        }
    }
}
