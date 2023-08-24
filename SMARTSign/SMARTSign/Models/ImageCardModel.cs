using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace SMARTSign.Models
{
    public class ImageCardModel : INotifyPropertyChanged
    {
        // Video Name
        private string name;
        // Card Flip Property
        private bool isImage;
        // Captializes Name
        public string Name { get { return name; } set { name = char.ToUpper(value[0]) + value.Substring(1); }}
        // YouTube Video ID
        public string YTID { get; set; }
        // Source for the YouTube Video Thumbnail
        public string ImageSource { get; set; }
        // Gated Inverse Property for IsImage
        public bool IsVideo { get { return !IsImage; } }
        // Sets the Card Flip Orientation
        public bool IsImage { get { return isImage; } set { isImage = value; OnPropertyChanged(nameof(IsImage)); OnPropertyChanged(nameof(IsVideo)); } }
        // Adjusts Loaded IFrame Size
        public int BHeight { get; set; }
        // Interface Property
        public event PropertyChangedEventHandler PropertyChanged;
        // Source for the YouTube Video
        public HtmlWebViewSource VideoSource { get { return VideoPath(); } }
        // Parameterized Constructor
        public ImageCardModel(string _name, string yTID, string imageSource, bool isImage)
        {
            Name = _name;
            YTID = yTID;
            ImageSource = imageSource;
            BHeight = 270;
            IsImage = isImage;
        }
        // Builds HTML Insertion for IFrame
        public HtmlWebViewSource VideoPath()
        {
            HtmlWebViewSource hal = new HtmlWebViewSource();
            string iframeHtml = $@"
                <html>
                    <body>
                        <iframe width='370' height='250' src='https://www.youtube.com/embed/{YTID}' frameborder='0'></iframe>
                    </body>
                </html>";
            hal.Html = iframeHtml;
            return hal;
        }
        // Notifies UI when a property needs to be updated from backend
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
