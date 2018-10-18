using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeSkin.Small
{
    class ArtistDetailModel
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public ArtistDetailModel(string title, string content)
        {
            this.Title = title;
            this.Content = content;
        }
    }
}
