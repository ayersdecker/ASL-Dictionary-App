using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMARTSign.Models
{
    public class YouTubeInfoModel
    {

        // EXTRACTED Name
        public string YTID_Name { get; set; }
        // EXTRACTED Thumbnail URL
        public string Image_URL { get; set; }
        // EXTRACTED YouTube Video ID
        public string YTID { get; set; }
        // Parameterized Constructor
        public YouTubeInfoModel(string yTID_Name, string image_URL, string yTID)
        {
            YTID_Name = yTID_Name;
            Image_URL = image_URL;
            YTID = yTID;
        }
    }
}
