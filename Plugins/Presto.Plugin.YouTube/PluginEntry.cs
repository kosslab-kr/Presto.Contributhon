using Presto.Common;
using Presto.Plugin.YouTube;

[assembly: PrestoTitle("YouTube")]
[assembly: PrestoAuthor("Kodnix Software")]
[assembly: PrestoDescription("YouTube 영상을 라이브러리에 추가하는 기능을 제공합니다.")]
[assembly: PrestoPlugin(typeof(PluginEntry))]

namespace Presto.Plugin.YouTube
{
    public class PluginEntry
    {
        [PrestoEntry]
        public static void Initialize()
        {

        }
    }
}
