using System;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;

namespace ImageSearchDownloader
{
    public class ImagePage
    {
        public int pageNo;
        public TextBox fileName;
        public TextBox pageNoBox;
        public TextBox checkBox;
        public String response;
        public String size;
        public String imgsrc;
        public Button reload;
        public Button nextBtn;
        WebBrowser webBrowser;

        public PictureBox[] imgPage_img = new PictureBox[5];
        public TextBox[] imgPage_title = new TextBox[5];
        public Label[] imgPage_label = new Label[5];
        public Button[] c = new Button[5];

        public ImagePage(WebBrowser webBrowser)
        {
            fileName = new TextBox();
            checkBox = new TextBox();
            pageNoBox = new TextBox();
            reload = new Button();
            nextBtn = new Button();
            this.webBrowser = webBrowser;

            for (int i = 0; i < 5; i++)
            {
                imgPage_title[i] = new TextBox();
                imgPage_img[i] = new PictureBox();
                imgPage_label[i] = new Label();
                c[i] = new Button();
            }
        }

        public void getPage(String name, Boolean check)
        {
            Encoding encoding = Encoding.GetEncoding(737);
            System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
            //변환하고자 하는 문자열을 UTF8 방식으로 변환하여 byte 배열로 반환
            byte[] utf8Bytes;
            int index = fileName.Text.IndexOf("[");
            String search = "";
            if (check && index >= 0)
            {
                search = (name == "") ? "" : (name + " ");
                search += fileName.Text.Replace("-", "+").Substring(fileName.Text.IndexOf("["));
            }
            else
            {
                search = (name == "") ? "" : (name + " ");
                search += fileName.Text.Replace("-", "+");
            }
            utf8Bytes = utf8.GetBytes(search);

            //UTF-8을 string으로 변한
            string utf8String = "";
            foreach (byte b in utf8Bytes)
            {
                utf8String += "%" + String.Format("{0:X}", b);
            }

            string uri = "https://www.google.co.kr/search?q=" + utf8String + "&hl=ko&biw=1745&source=lnms&tbm=isch&sa=X&ved=0ahUKEwiy8O7m1Z7NAhXDYaYKHceBDOgQ_AUICCgB&bih=828#imgrc=";

            webBrowser.Navigate(uri);

            while (webBrowser.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents(); // 웹페이지 로딩이 완료될 때 까지 대기
            }

            HtmlElement elem;
            if (webBrowser.Document != null)
            {
                HtmlElementCollection elems = webBrowser.Document.GetElementsByTagName("HTML");
                if (elems.Count == 1)
                {
                    elem = elems[0];
                    response = elem.OuterHtml;
                }
                System.GC.Collect(0, GCCollectionMode.Forced);
                System.GC.WaitForFullGCComplete();
            }
            getPic();
        }

        void getPic()
        {
            int imgs;
            if (response != null)
            {
                response = response.Substring(response.IndexOf("이미지 검색결과"), response.IndexOf("background: rgb"));
                for (int i = 0; i < 5; i++)
                {
                    imgs = response.IndexOf("data:image");
                    if (imgs == -1)
                    {
                        imgPage_title[i].Text = "";
                    }
                    else
                    {

                        string data = response.Substring(response.IndexOf("data:image"));
                        response = response.Substring(response.IndexOf("data:image") + 10);
                        string src = data.Substring(data.IndexOf("https"));

                        data = data.Substring(0, data.IndexOf("\""));
                        src = src.Substring(0, src.IndexOf("\""));

                        var base64Data = Regex.Match(data, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
                        var binData = Convert.FromBase64String(base64Data);

                        using (var stream = new MemoryStream(binData))
                        {
                            imgPage_img[i].Image = new Bitmap(stream);
                            imgPage_title[i].Text = src;
                            imgPage_img[i].Name = (pageNo + 1).ToString() + ";" + src + ";" + pageNoBox.Text;
                        }
                    }
                }
            }
        }

        public void nextPic(object sender, MouseEventArgs e)
        {
            int imgs;
            if (response != null)
            {
                for (int i = 0; i < 5; i++)
                {
                    imgs = response.IndexOf("data:image");
                    if (imgs == -1)
                    {
                        imgPage_title[i].Text = "";
                    }
                    else
                    {
                        try
                        {
                            string data = response.Substring(response.IndexOf("data:image"));
                            response = response.Substring(response.IndexOf("data:image") + 10);
                            string src = data.Substring(data.IndexOf("https"));

                            if(data.IndexOf("'") > 0)
                                data = data.Substring(0, data.IndexOf("'"));
                            if (data.IndexOf("\"") > 0)
                                data = data.Substring(0, data.IndexOf("\""));
                            src = src.Substring(0, src.IndexOf("\""));

                            data = data.Replace("\\/", "/");

                            var base64Data = Regex.Match(data, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
                            var binData = Convert.FromBase64String(base64Data);

                            using (var stream = new MemoryStream(binData))
                            {
                                imgPage_img[i].Image = new Bitmap(stream);
                                imgPage_title[i].Text = src;
                                imgPage_img[i].Name = (pageNo + 1).ToString() + ";" + src + ";" + pageNoBox.Text;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        public void reloadPage(String name, Boolean check)
        {
            getPage(name, check);
        }
    };
}
