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
        private string name;
        private bool isImage;
        public string Name { get { return name; } set { name = char.ToUpper(value[0]) + value.Substring(1); }}
        public string YTID { get; set; }
        public string ImageSource { get; set; }
        public bool IsVideo { get { return !IsImage; } }
        public bool IsImage
        {
            get { return isImage; } 
            set
            {
                isImage = value;
                OnPropertyChanged(nameof(IsImage));
                OnPropertyChanged(nameof(IsVideo));
            }
        }
        public int BHeight { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public HtmlWebViewSource VideoSource { get { return VideoPath(); } }
        public ImageCardModel(string _name, string yTID, string imageSource, int bHeight, bool isImage)
        {
            Name = _name;
            YTID = yTID;
            ImageSource = imageSource;
            BHeight = 270;
            IsImage = isImage;
        }
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
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
