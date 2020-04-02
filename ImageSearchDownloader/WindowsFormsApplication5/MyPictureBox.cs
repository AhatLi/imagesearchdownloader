using System;
using System.Windows.Forms;

namespace ImageSearchDownloader
{
    public class MyPictureBox : PictureBox
    {
        public String pageNo;
        public String imgLink;
        public String ext;

        public MyPictureBox()
        {
            pageNo = "";
            imgLink = "";
            ext = "";
        }
    }
}
