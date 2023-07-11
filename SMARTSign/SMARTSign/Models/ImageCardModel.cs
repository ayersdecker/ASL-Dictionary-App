using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMARTSign.Models
{
    public class ImageCardModel
    {
        public string Name { get; set; }
        public string YTID { get; set; }
        public string ImageSource { get; set; }
        public ImageCardModel(string name, string yTID, string imageSource)
        {
            Name = name;
            YTID = yTID;
            ImageSource = imageSource;
        }
    }
}
