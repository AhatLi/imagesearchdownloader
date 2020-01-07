using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace ImageSearchDownloader
{
    public partial class Form1 : Form
    {
        int click;
        int dirCount;
        static WebBrowser webBrowser = new WebBrowser();

        bool fin = false;

        public Form1()
        {
            InitializeComponent();

            proxyText.Text = "http://www.freeproxylists.net/";
            click = 0;

            webBrowser.ScriptErrorsSuppressed = true;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }

        bool retry = true;

        
        void downloadPic(String fileName)
        {
            try
            {
                string sDirPath;
                sDirPath = Application.StartupPath + "\\imgFile";
                DirectoryInfo di = new DirectoryInfo(sDirPath);
                if (di.Exists == false)
                {
                    di.Create();
                }


                string path = ".\\imgFile\\" + fileName + ".jpg";
                MyWebClient WClient = new MyWebClient(10);

                if (proxyCheck.Checked)
                {
                    WebProxy proxy = new WebProxy(proxyText.Text, Convert.ToInt32(proxyPortText.Text));
                    proxy.UseDefaultCredentials = false;
                    proxy.BypassProxyOnLocal = false;

                    WClient.Proxy = proxy;
                }

                WClient.Headers.Add("Accept:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                WClient.Headers.Add("User-Agent:Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2300.0 Safari/537.36");
                WClient.DownloadFile(fileSrcText.Text, path);

                if(!checkFile(path))
                {
                    if(retry)
                    {
                        retry = false;
                        proxyCheck.Checked = !proxyCheck.Checked;
                        downloadPic(fileName);
                        retry = true;
                        proxyCheck.Checked = !proxyCheck.Checked;
                    }
                }

                WClient.Dispose();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        bool checkFile(string filename)
        {
            bool ret = false;
            System.IO.FileInfo leftFileInfo = new System.IO.FileInfo(filename);
            
            if (leftFileInfo.Length > 5120)
                ret = true;

            return ret;
        }
        
        void getPage()
        {
            ImagePage page = new ImagePage();
            makePage(page, tabPage1, 0, p1SearchText.Text, p1SearchAddText.Text);
            page.fileName.Text = p1SearchText.Text;
            page.getPage("", false);
        }

        private void goTextClass(object sender, MouseEventArgs e)
        {
            try
            {
                String infoText = ((PictureBox)sender).Name;
                String fileSrc = infoText.Substring(infoText.IndexOf(";") + 1, infoText.LastIndexOf(";"));
                fileSrc = fileSrc.Substring(0, fileSrc.LastIndexOf(";"));
                String fileName = infoText.Substring(infoText.LastIndexOf(";") + 1);

                int a = infoText.IndexOf(";") + 1;
                int b = infoText.LastIndexOf(";");

                fileSrcText.Text = fileSrc;
                nameText.Text = fileName;
                Clipboard.SetText(fileSrc);
            }
            catch
            {
            }
        }

        private void downImageFunClass(object sender, EventArgs e)
        {
            downloadPic(nameText.Text);
        }

        private void reloadPage(object sender, EventArgs e, ImagePage page)
        {
            page.getPage(p2SearchText.Text, p2Check1.Checked);
        }

        private void p1StartBtn_Click(object sender, EventArgs e)
        {
            getPage();
        }

        private void p1DownBtn_Click(object sender, EventArgs e)
        {
            downloadPic(nameText.Text);
        }

        private void p1PageNextBtn_Click(object sender, EventArgs e)
        {
            if (p1PageText.Text != "")
            {
                try
                {
                    click = Convert.ToInt32(p1PageText.Text);
                }
                catch
                {

                }
            }
            click++;
            nameText.Text = click.ToString();
            p1PageText.Text = click.ToString();
        }

        private void p1PagePrevBtn_Click(object sender, EventArgs e)
        {
            if (p1PageText.Text != "")
            {
                try
                {
                    click = Convert.ToInt32(p1PageText.Text);
                }
                catch
                {

                }
            }
            click--;
            nameText.Text = click.ToString();
            p1PageText.Text = click.ToString();
        }

        private void p1NameText_TextClick(object sender, KeyEventArgs e)
        {
            nameText.Text = p1PageText.Text;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(fileSrcText.Text);
        }

        private void p1DirPrevBtn_Click(object sender, EventArgs e)
        {
            if (p1DirText.Text != "")
            {
                try
                {
                    dirCount = Convert.ToInt32(p1DirText.Text);
                }
                catch
                {

                }
            }
            dirCount--;

            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(".");
            System.IO.DirectoryInfo[] fi = di.GetDirectories("*");
            
            if (dirCount <= 0)
                dirCount = 1;
            if (dirCount >= fi.Length)
                dirCount = fi.Length;

            p1DirText.Text = dirCount.ToString();

            if (fi.Length != 0)
            {
                p1SearchText.Text = fi[dirCount-1].Name.ToString();
            }


            if (p1PageText.Text != "")
            {
                try
                {
                    click = Convert.ToInt32(p1PageText.Text);
                }
                catch
                {

                }
            }
            click = dirCount;
            nameText.Text = dirCount.ToString();
            p1PageText.Text = dirCount.ToString();

        }

        private void p1DirNextBtn_Click(object sender, EventArgs e)
        {
            if (p1DirText.Text != "")
            {
                try
                {
                    dirCount = Convert.ToInt32(p1DirText.Text);
                }
                catch
                {

                }
            }
            dirCount++;

            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(".");
            System.IO.DirectoryInfo[] fi = di.GetDirectories("*");

            if (dirCount < 0)
                dirCount = 1;
            if (dirCount >= fi.Length)
                dirCount = fi.Length;

            p1DirText.Text = dirCount.ToString();

            if (fi.Length != 0)
            {
                p1SearchText.Text = fi[dirCount-1].Name.ToString();
            }
            
            if (p1PageText.Text != "")
            {
                try
                {
                    click = Convert.ToInt32(p1PageText.Text);
                }
                catch
                {

                }
            }
            click = dirCount;
            nameText.Text = dirCount.ToString();
            p1PageText.Text = dirCount.ToString();

            getPage();
        }

        List<ImagePage> imageVector = new List<ImagePage>();

        public class ImagePage
        {
            public int pageNo;
            public TextBox fileName;
            public TextBox pageNoBox;
            public TextBox checkBox;
            public String response;
            public String size;
            public String imgsrc;
            public Button reload = new Button();

            public PictureBox[] imgPage_img = new PictureBox[5];
            public TextBox[] imgPage_title = new TextBox[5];
            public Label[] imgPage_label = new Label[5];
            public Button[] c = new Button[5];

            public ImagePage()
            {
                fileName = new TextBox();
                checkBox = new TextBox();
                pageNoBox = new TextBox();
                reload = new Button();

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
                if(check && index >= 0)
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
                    for (int i = 0; i < 5; i++)
                    {
                        imgs = response.IndexOf("이미지 검색결과");
                        if (imgs == -1)
                        {
                            imgPage_title[i].Text = "";
                        }
                        else
                        {
                            response = response.Substring(response.IndexOf("이미지 검색결과"));
                            response = response.Substring(response.IndexOf("data:image"));

                            string data = response.Substring(response.IndexOf("data:image"));
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
        };

        bool close = false;
        void getPageAllImage()
        {
            int imageNumber = 0;

            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(".");
            System.IO.DirectoryInfo[] fi = di.GetDirectories("*");

            while (imageNumber < fi.Length)
            {
                if(fin)
                {
                    return;
                }

                ImagePage page = new ImagePage();
                imageNumber = makePage(page, tabPage2, imageNumber, fi[imageNumber].Name, p2SearchText.Text, p2Check1.Checked);

                Thread.Sleep(10);
                if (close)
                {
                    break;
                }
            }
        }

        public int makePage(ImagePage page, TabPage tabPage, int imageNumber, string filename, String name = "", Boolean check = false)
        {
            for (int i = 0; i < 5; i++)
            {
                page.imgPage_title[i].Location = new Point(tabPage.AutoScrollPosition.X + 60 + (i * 310), tabPage.AutoScrollPosition.Y + 70 + (imageNumber * 280));
                page.imgPage_title[i].Size = new Size(200, 20);
                page.imgPage_title[i].Parent = tabPage;
                tabPage.Controls.Add(page.imgPage_title[i]);

                page.imgPage_img[i].Location = new Point(tabPage.AutoScrollPosition.X + 60 + (i * 310), tabPage.AutoScrollPosition.Y + 100 + (imageNumber * 280));
                page.imgPage_img[i].Size = new Size(300, 200);
                page.imgPage_img[i].Parent = tabPage;
                tabPage.Controls.Add(page.imgPage_img[i]);
                page.imgPage_img[i].MouseClick += new System.Windows.Forms.MouseEventHandler(this.goTextClass);
                page.imgPage_img[i].DoubleClick += new EventHandler(this.downImageFunClass);

                page.imgPage_label[i].Location = new Point(tabPage.AutoScrollPosition.X + 60 + (i * 310), tabPage.AutoScrollPosition.Y + 280 + (imageNumber * 280));
                page.imgPage_label[i].Size = new Size(300, 21);
                page.imgPage_label[i].Parent = tabPage;
                tabPage.Controls.Add(page.imgPage_label[i]);
            }
            page.pageNoBox.Location = new Point(tabPage.AutoScrollPosition.X + 10, tabPage.AutoScrollPosition.Y + 40 + (imageNumber * 280));
            page.pageNoBox.Size = new Size(30, 21);
            page.pageNoBox.Parent = tabPage;
            tabPage.Controls.Add(page.pageNoBox);
            page.pageNoBox.Text = (imageNumber + 1).ToString();

            page.reload.Location = new Point(tabPage.AutoScrollPosition.X + 380, tabPage.AutoScrollPosition.Y + 40 + (imageNumber * 280));
            page.reload.Size = new Size(100, 21);
            page.reload.Text = "다시읽기";
            page.reload.MouseClick += new System.Windows.Forms.MouseEventHandler(this.goTextClass);
            page.reload.MouseClick += (sender, evt) =>
            {
                reloadPage(null, null, page);
            };
            tabPage.Controls.Add(page.reload);

            page.checkBox.Location = new Point(tabPage.AutoScrollPosition.X + 10, tabPage.AutoScrollPosition.Y + 70 + (imageNumber * 280));
            page.checkBox.Size = new Size(30, 21);
            page.checkBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.goTextClass);
            tabPage.Controls.Add(page.checkBox);

            page.fileName.Location = new Point(tabPage.AutoScrollPosition.X + 60, tabPage.AutoScrollPosition.Y + 40 + (imageNumber * 280));
            page.fileName.Size = new Size(300, 21);
            page.fileName.Parent = tabPage;
            tabPage.Controls.Add(page.fileName);

            page.fileName.Text = filename;
            page.pageNo = imageNumber;
            page.getPage(name, check);

            return imageNumber + 1;
        }

        private void p2StartBtn_Click_1(object sender, EventArgs e)
        {
            getPageAllImage();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            close = true;
        }

        private void p3StartBtn_Click(object sender, EventArgs e)
        {
            Encoding encoding = Encoding.GetEncoding(737);
            System.Text.Encoding utf8 = System.Text.Encoding.UTF8;

            //변환하고자 하는 문자열을 UTF8 방식으로 변환하여 byte 배열로 반환
            byte[] utf8Bytes;
            utf8Bytes = utf8.GetBytes(p3Text.Text);

            //UTF-8을 string으로 변한
            string utf8String = "";
            Console.Write(" - Encode: ");
            foreach (byte b in utf8Bytes)
            {
                utf8String += "%" + String.Format("{0:X}", b);
            }

            string uri = "https://www.google.co.kr/search?q=" + utf8String.Replace(" ", "+") + "&hl=ko&biw=1745&source=lnms&tbm=isch&sa=X&ved=0ahUKEwiy8O7m1Z7NAhXDYaYKHceBDOgQ_AUICCgB&bih=828#imgrc=";
            
            webBrowser2.Navigate(uri);
        }
    }
}
