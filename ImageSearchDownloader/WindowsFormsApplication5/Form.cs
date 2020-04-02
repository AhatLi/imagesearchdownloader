using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;

namespace ImageSearchDownloader
{
    public partial class Form1 : Form
    {
        int click;
        int dirCount;
        ImagePage mainpage;

        bool fin = false;

        public Form1()
        {
            InitializeComponent();

            proxyText.Text = "http://www.freeproxylists.net/";
            click = 0;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }

        bool retry = true;

        public void allControlClear()
        {
            tabPage1.Controls.Clear();
            tabPage2.Controls.Clear();
        }
        public void allControlAdd()
        {
            this.tabPage1.Controls.Add(this.p1DownBtn);
            this.tabPage1.Controls.Add(this.p1NextBtn);
            this.tabPage1.Controls.Add(this.p1StartBtn);
            this.tabPage1.Controls.Add(this.p1SearchText);
            this.tabPage1.Controls.Add(this.p1DirText);
            this.tabPage1.Controls.Add(this.p1DirNextBtn);
            this.tabPage1.Controls.Add(this.p1DirPrevBtn);
            this.tabPage1.Controls.Add(this.p1Label3);
            this.tabPage1.Controls.Add(this.p1PageText);
            this.tabPage1.Controls.Add(this.p1PagePrevBtn);
            this.tabPage1.Controls.Add(this.p1PageNextBtn);
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.p2Label2);
            this.tabPage2.Controls.Add(this.p2SearchText);
            this.tabPage2.Controls.Add(this.p2Check1);
            this.tabPage2.Controls.Add(this.p2StartBtn);
        }
        
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

        public void goTextClass(object sender, MouseEventArgs e)
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

        public void goOneTextClass(object sender, MouseEventArgs e)
        {
            try
            {
                String infoText = ((PictureBox)sender).Name;
                String fileSrc = infoText.Substring(infoText.IndexOf(";") + 1, infoText.LastIndexOf(";"));
                fileSrc = fileSrc.Substring(0, fileSrc.LastIndexOf(";"));

                int a = infoText.IndexOf(";") + 1;
                int b = infoText.LastIndexOf(";");

                fileSrcText.Text = fileSrc;
                Clipboard.SetText(fileSrc);

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
            catch
            {
            }
        }

        public void downImageFunClass(object sender, EventArgs e)
        {
            downloadPic(nameText.Text);
        }

        private void p1StartBtn_Click(object sender, EventArgs e)
        {
            allControlClear();
            allControlAdd();
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

        void getPage()
        {
            mainpage = new ImagePage(this);

            mainpage.makePage(0, 1);

            mainpage.panel.Location = new Point(0, 35);
            mainpage.panel.Size = new Size(1620, 280);
            mainpage.panel.AutoScroll = true;
            tabPage1.Controls.Add(mainpage.panel);

            mainpage.fileName.Text = p1SearchText.Text;
            mainpage.getPage(mainpage.fileName.Text);
        }

        bool close = false;

        private void p2StartBtn_Click_1(object sender, EventArgs e)
        {
            allControlClear();
            allControlAdd();

            int imageNumber = 0;

            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(".");
            System.IO.DirectoryInfo[] fi = di.GetDirectories("*");

            while (imageNumber < fi.Length)
            {
                if (fin)
                {
                    return;
                }

                ImagePage page = new ImagePage(this);
                page.makePage(imageNumber);

                page.panel.Location = new Point(0, 35 + imageNumber * 290);
                page.panel.Size = new Size(1620, 280);
                page.panel.AutoScroll = true;
                tabPage2.Controls.Add(page.panel);

                String searchText = "";
                int index = fi[imageNumber].Name.IndexOf("[");
                if (p2Check1.Checked && index >= 0)
                {
                    searchText = (p2SearchText.Text == "") ? "" : (p2SearchText.Text + " ");
                    searchText += fi[imageNumber].Name.Replace("-", "+").Substring(fi[imageNumber].Name.IndexOf("["));
                }
                else
                {
                    searchText = (p2SearchText.Text == "") ? "" : (p2SearchText.Text + " ");
                    searchText += fi[imageNumber].Name.Replace("-", "+");
                }

                page.getPage(searchText);

                imageNumber++;

                Thread.Sleep(10);
                if (close)
                {
                    break;
                }
            }
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

        private void btnOpenDir_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + "\\imgFile");
        }

        private void p1NextBtn_Click(object sender, EventArgs e)
        {
            mainpage.nextPic(null, null);
        }
    }
}
