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
        string imgsrc;
        string response;
        string size;
        int click;
        int dirCount;
        static WebBrowser webBrowser1 = new WebBrowser();

        PictureBox[] p = new PictureBox[5];
        TextBox[] t = new TextBox[5];
        Label[] l = new Label[5];

        bool fin = false;

        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < 5; i++)
            {
                p[i] = new PictureBox();
                p[i].Location = new Point(10 + (i * 310), 70);
                p[i].Size = new Size(300, 200);
                p[i].Parent = tabPage1;
                tabPage1.Controls.Add(p[i]);

                t[i] = new TextBox();
                t[i].Location = new Point(10 + (i * 310), 300);
                t[i].Size = new Size(300, 21);
                t[i].Parent = tabPage1;
                tabPage1.Controls.Add(t[i]);

                l[i] = new Label();
                l[i].Location = new Point(10 + (i * 310), 280);
                l[i].Size = new Size(300, 21);
                l[i].Parent = tabPage1;
                tabPage1.Controls.Add(l[i]);

            }

            p[0].MouseClick += new System.Windows.Forms.MouseEventHandler(this.goText0);
            p[1].MouseClick += new System.Windows.Forms.MouseEventHandler(this.goText1);
            p[2].MouseClick += new System.Windows.Forms.MouseEventHandler(this.goText2);
            p[3].MouseClick += new System.Windows.Forms.MouseEventHandler(this.goText3);
            p[4].MouseClick += new System.Windows.Forms.MouseEventHandler(this.goText4);
            p[0].DoubleClick += new EventHandler(this.downImageFun);
            p[1].DoubleClick += new EventHandler(this.downImageFun);
            p[2].DoubleClick += new EventHandler(this.downImageFun);
            p[3].DoubleClick += new EventHandler(this.downImageFun);
            p[4].DoubleClick += new EventHandler(this.downImageFun);


            proxyText.Text = "http://www.freeproxylists.net/";
            click = 0;
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
                        t[i].Text = "";
                    }
                    else
                    {
                        int test = response.IndexOf("이미지 검색결과");
                        response = response.Substring(response.IndexOf("이미지 검색결과"));
                        response = response.Substring(response.IndexOf("data:image"));
                        string data = response.Substring(response.IndexOf("data:image"), response.IndexOf("\""));

                        var base64Data = Regex.Match(data, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
                        var binData = Convert.FromBase64String(base64Data);

                        using (var stream = new MemoryStream(binData))
                        {
                            p[i].Image = new Bitmap(stream);
                        }
                    }
                }
            }
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
            catch (WebException eee)
            {
            }
            catch (ArgumentException ee)
            {
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
            Encoding encoding = Encoding.GetEncoding(737);
            System.Text.Encoding utf8 = System.Text.Encoding.UTF8;

            //변환하고자 하는 문자열을 UTF8 방식으로 변환하여 byte 배열로 반환
            byte[] utf8Bytes;
            if (p1SearchAddText.Text == "")
            {
                utf8Bytes = utf8.GetBytes(p1SearchText.Text);
            }
            else
            {
                utf8Bytes = utf8.GetBytes(p1SearchAddText.Text + " " + p1SearchText.Text);
            }

            //UTF-8을 string으로 변한
            string utf8String = "";
            Console.Write(" - Encode: ");
            foreach (byte b in utf8Bytes)
            {
                utf8String += "%" + String.Format("{0:X}", b);
            }

            string uri = "https://www.google.co.kr/search?q=" + utf8String.Replace(" ", "+") + "&hl=ko&biw=1745&source=lnms&tbm=isch&sa=X&ved=0ahUKEwiy8O7m1Z7NAhXDYaYKHceBDOgQ_AUICCgB&bih=828#imgrc=";

            webBrowser1.Navigate(uri);
            //webReady = true;

            while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents(); // 웹페이지 로딩이 완료될 때 까지 대기
            }

            HtmlElement elem;
            if (webBrowser1.Document != null)
            {
                HtmlElementCollection elems = webBrowser1.Document.GetElementsByTagName("HTML");
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

        private void goText0(object sender, EventArgs e)
        {
            try
            {
                fileSrcText.Text = t[0].Text;
                Clipboard.SetText(t[0].Text);
            }
            catch
            {

            }
        }
        private void goText1(object sender, EventArgs e)
        {
            try
            {
                fileSrcText.Text = t[1].Text;
                Clipboard.SetText(t[1].Text);
            }
            catch
            {

            }
        }
        private void goText2(object sender, EventArgs e)
        {
            try
            {
                fileSrcText.Text = t[2].Text;
                Clipboard.SetText(t[2].Text);
            }
            catch
            {

            }
        }
        private void goText3(object sender, EventArgs e)
        {
            try
            {
                fileSrcText.Text = t[3].Text;
                Clipboard.SetText(t[3].Text);
            }
            catch
            {

            }
        }
        private void goText4(object sender, EventArgs e)
        {
            try
            {
                fileSrcText.Text = t[4].Text;
                Clipboard.SetText(t[4].Text);
            }
            catch
            {

            }
        }
        
        private void goTextClass(object sender, MouseEventArgs e)
        {
            try
            {
                String infoText = ((PictureBox)sender).Name;
                String infoNo = infoText.Substring(0, infoText.IndexOf(";"));
                String fileName = infoText.Substring(infoText.IndexOf(";") + 1);

                fileSrcText.Text = fileName;
                textBox9.Text = infoNo;
                Clipboard.SetText(fileName);
            }
            catch
            {
            }
        }

        private void downImageFun(object sender, EventArgs e)
        {
            downloadPic(p1NameText.Text);
        }

        private void downImageFunClass(object sender, EventArgs e)
        {
            downloadPic(textBox9.Text);
        }

        private void reloadPage(object sender, EventArgs e, ImagePage page)
        {
            page.getPage(p2SearchText.Text, p2Check1.Checked);
        }

        private void p1StartBtn_Click(object sender, EventArgs e)
        {
            getPage();
        }

        private void p1NextBtn_Click_1(object sender, EventArgs e)
        {
            getPic();
        }

        class MyWebClient : WebClient
        {
            int m_nTimeOut = 0;
            public MyWebClient(int timeOut)
            {
                m_nTimeOut = timeOut * 1000;
            }

            protected override WebRequest GetWebRequest(Uri address)
            {
                WebRequest request = base.GetWebRequest(address);
                request.Timeout = m_nTimeOut;
                return request;
            }
        };

        private void p1DownBtn_Click(object sender, EventArgs e)
        {
            downloadPic(p1NameText.Text);
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
            p1NameText.Text = click.ToString();
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
            p1NameText.Text = click.ToString();
            p1PageText.Text = click.ToString();
        }

        private void p1NameText_TextClick(object sender, KeyEventArgs e)
        {
            p1NameText.Text = p1PageText.Text;
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

            if (fi.Length == 0) ;
            else
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
            p1NameText.Text = dirCount.ToString();
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

            if (fi.Length == 0) ;
            else
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
            p1NameText.Text = dirCount.ToString();
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

            public PictureBox[] p = new PictureBox[5];
            public TextBox[] t = new TextBox[5];
            public Label[] l = new Label[5];
            public Button[] c = new Button[5];

            public void getPage(String name, Boolean check)
            {
                Encoding encoding = Encoding.GetEncoding(737);
                System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
                string aaa = this.fileName.Text;
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

                webBrowser1.Navigate(uri);

                while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents(); // 웹페이지 로딩이 완료될 때 까지 대기
                }

                HtmlElement elem;
                if (webBrowser1.Document != null)
                {
                    HtmlElementCollection elems = webBrowser1.Document.GetElementsByTagName("HTML");
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
                        imgs = response.IndexOf(",\"ou\":\"");
                        if (imgs == -1)
                        {
                            t[i].Text = "";
                        }
                        else
                        {
                            string original;

                            response = response.Substring(response.IndexOf("&amp;w=") + "&amp;".Length);
                            size = response.Substring(0, response.IndexOf("&amp;hl="));

                            response = response.Substring(response.IndexOf(",\"ou\":\"") + ",\"ou\":\"".Length);
                            original = response.Substring(0, response.IndexOf("\""));

                            response = response.Substring(response.IndexOf(",\"tu\":\"") + ",\"tu\":\"".Length);
                            imgsrc = response.Substring(0, response.IndexOf("\"")).Replace("\\u003d", "=");

                            WebClient client = new WebClient();
                            byte[] myDataBuffer = client.DownloadData(imgsrc);
                            Stream stream = new MemoryStream();
                            stream.Write(myDataBuffer, 0, myDataBuffer.Length);
                            p[i].Image = null;
                            p[i].Image = Image.FromStream(stream, true);
                            l[i].Text = size.Replace("&amp;", ", ");
                            t[i].Text = original;
                            p[i].Name = (pageNo+1).ToString() + ";" + original;
                            stream.Dispose();
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

                for (int i = 0; i < 5; i++)
                {
                    page.t[i] = new TextBox();
                    page.t[i].Location = new Point(tabPage2.AutoScrollPosition.X + 60 + (i * 310), tabPage2.AutoScrollPosition.Y + 70 + (imageNumber * 280));
                    page.t[i].Size = new Size(200, 20);
                    page.t[i].Parent = tabPage2;
                    tabPage2.Controls.Add(page.t[i]);

                    page.p[i] = new PictureBox();
                    page.p[i].Location = new Point(tabPage2.AutoScrollPosition.X + 60 + (i * 310), tabPage2.AutoScrollPosition.Y + 100 + (imageNumber * 280));
                    page.p[i].Size = new Size(300, 200);
                    page.p[i].Parent = tabPage2;
                    tabPage2.Controls.Add(page.p[i]);
                    page.p[i].MouseClick += new System.Windows.Forms.MouseEventHandler(this.goTextClass);
                    page.p[i].DoubleClick += new EventHandler(this.downImageFunClass);

                    page.l[i] = new Label();
                    page.l[i].Location = new Point(tabPage2.AutoScrollPosition.X + 60 + (i * 310), tabPage2.AutoScrollPosition.Y + 280 + (imageNumber * 280));
                    page.l[i].Size = new Size(300, 21);
                    page.l[i].Parent = tabPage2;
                    tabPage2.Controls.Add(page.l[i]);
                }
                page.pageNoBox = new TextBox();
                page.pageNoBox.Location = new Point(tabPage2.AutoScrollPosition.X + 10, tabPage2.AutoScrollPosition.Y + 40 + (imageNumber * 280));
                page.pageNoBox.Size = new Size(30, 21);
                page.pageNoBox.Parent = tabPage2;
                tabPage2.Controls.Add(page.pageNoBox);
                page.pageNoBox.Text = (imageNumber + 1).ToString();

                page.reload = new Button();
                page.reload.Location = new Point(tabPage2.AutoScrollPosition.X + 380, tabPage2.AutoScrollPosition.Y + 40 + (imageNumber * 280));
                page.reload.Size = new Size(100, 21);
                page.reload.Text = "다시읽기";
                page.reload.MouseClick += new System.Windows.Forms.MouseEventHandler(this.goTextClass);
                page.reload.MouseClick += (sender, evt) =>
                        {
                            reloadPage(null, null, page);
                        };
                tabPage2.Controls.Add(page.reload);

                page.checkBox = new TextBox();
                page.checkBox.Location = new Point(tabPage2.AutoScrollPosition.X + 10, tabPage2.AutoScrollPosition.Y + 70 + (imageNumber * 280));
                page.checkBox.Size = new Size(30, 21);
                page.checkBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.goTextClass);
                tabPage2.Controls.Add(page.checkBox);

                page.fileName = new TextBox();
                page.fileName.Location = new Point(tabPage2.AutoScrollPosition.X + 60, tabPage2.AutoScrollPosition.Y + 40 + (imageNumber * 280));
                page.fileName.Size = new Size(300, 21);
                page.fileName.Parent = tabPage2;
                tabPage2.Controls.Add(page.fileName);

                page.fileName.Text = fi[imageNumber].Name;
                page.pageNo = imageNumber;
                page.getPage(p2SearchText.Text, p2Check1.Checked);

                imageNumber++;
                Thread.Sleep(10);
                if (close)
                {
                    break;
                }
            }
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
