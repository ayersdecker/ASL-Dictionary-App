using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace SMARTSign.Models
{
    public class ImageCardModel
    {
        public string Name { get; set; }
        public string YTID { get; set; }
        public string ImageSource { get; set; }
        public bool IsVideo { get { return !IsImage; } }
        public bool IsImage { get; set; }
        public int BHeight { get; set; }

        public HtmlWebViewSource VideoSource { get { return VideoPath(); } }
        public ImageCardModel(string name, string yTID, string imageSource, int bHeight, bool isImage)
        {
            Name = name;
            YTID = yTID;
            ImageSource = imageSource;
            BHeight = 270;
            IsImage = isImage;
        }
        public HtmlWebViewSource VideoPath()
        {
            HtmlWebViewSource hal = new HtmlWebViewSource();
            hal.Html = $"<iframe width=\"375\" height=\"250\" src=\"https://www.youtube.com/embed/{YTID}&autoplay=0\" frameborder=\"0\" allowfullscreen></iframe>";
            return hal;
        }
    }
}
