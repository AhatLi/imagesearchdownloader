using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {
        string imgsrc;
        string response;
        string size;
        int click;
        int dirCount;
        WebBrowser webBrowser1 = new WebBrowser();

        PictureBox[] p = new PictureBox[4];
        TextBox[] t = new TextBox[4];
        Label[] l = new Label[4];

        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < 4; i++)
            {
                p[i] = new PictureBox();
                p[i].Location = new Point(10 + (i * 310), 70);
                p[i].Size = new Size(300, 200);
                p[i].Parent = this;
                this.Controls.Add(p[i]);

                t[i] = new TextBox();
                t[i].Location = new Point(10 + (i * 310), 300);
                t[i].Size = new Size(300, 21);
                t[i].Parent = this;
                this.Controls.Add(t[i]);

                l[i] = new Label();
                l[i].Location = new Point(10 + (i * 310), 280);
                l[i].Size = new Size(300, 21);
                l[i].Parent = this;
                this.Controls.Add(l[i]);

            }

            p[0].MouseClick += new System.Windows.Forms.MouseEventHandler(this.goText0);
            p[1].MouseClick += new System.Windows.Forms.MouseEventHandler(this.goText1);
            p[2].MouseClick += new System.Windows.Forms.MouseEventHandler(this.goText2);
            p[3].MouseClick += new System.Windows.Forms.MouseEventHandler(this.goText3);
            p[0].DoubleClick += new EventHandler(this.doubleGoText0);
            p[1].DoubleClick += new EventHandler(this.doubleGoText1);
            p[2].DoubleClick += new EventHandler(this.doubleGoText2);
            p[3].DoubleClick += new EventHandler(this.doubleGoText3);


            textBox4.Text = "http://www.freeproxylists.net/";
            click = 0;
        }

        void getPic()
        {
            int imgs;
            if (response != null)
            {
                for (int i = 0; i < 4; i++)
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
                        stream.Dispose();
                    }
                }
            }
        }

        bool retry = true;

        void downloadPic()
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




                string path = ".\\imgFile\\" + textBox3.Text + ".jpg";

                MyWebClient WClient = new MyWebClient(10);

                if (checkBox1.Checked)
                {
                    WebProxy proxy = new WebProxy(textBox4.Text, Convert.ToInt32(textBox5.Text));
                    proxy.UseDefaultCredentials = false;
                    proxy.BypassProxyOnLocal = false;

                    WClient.Proxy = proxy;
                }

                WClient.Headers.Add("Accept:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                WClient.Headers.Add("User-Agent:Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2300.0 Safari/537.36");

                WClient.DownloadFile(textBox1.Text, path);

                if(!checkFile(path))
                {
                    if(retry)
                    {
                        retry = false;
                        checkBox1.Checked = !checkBox1.Checked;
                        downloadPic();
                        retry = true;
                        checkBox1.Checked = !checkBox1.Checked;
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
            if (textBox7.Text == "")
            {
                utf8Bytes = utf8.GetBytes(textBox2.Text);
            }
            else
            {
                utf8Bytes = utf8.GetBytes(textBox7.Text + " " + textBox2.Text);
            }

            //UTF-8을 string으로 변한
            string utf8String = "";
            Console.Write(" - Encode: ");
            foreach (byte b in utf8Bytes)
            {
                utf8String += "%" + String.Format("{0:X}", b);
            }
            Console.WriteLine(utf8String);

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
                textBox1.Text = t[0].Text;
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
                textBox1.Text = t[1].Text;
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
                textBox1.Text = t[2].Text;
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
                textBox1.Text = t[3].Text;
                Clipboard.SetText(t[3].Text);
            }
            catch
            {

            }
        }

        private void doubleGoText0(object sender, EventArgs e)
        {
            downloadPic();
        }
        private void doubleGoText1(object sender, EventArgs e)
        {
            downloadPic();
        }
        private void doubleGoText2(object sender, EventArgs e)
        {
            downloadPic();
        }
        private void doubleGoText3(object sender, EventArgs e)
        {
            downloadPic();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            getPage();
        }

        private void button1_Click_1(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            downloadPic();
            /*
           try
            {
                string sDirPath;
                sDirPath = Application.StartupPath + "\\imgFile";
                DirectoryInfo di = new DirectoryInfo(sDirPath);
                if (di.Exists == false)
                {
                    di.Create();
                }




                string path = ".\\imgFile\\" + textBox3.Text + ".jpg";

                MyWebClient WClient = new MyWebClient(10);

                if (checkBox1.Checked)
                {
                    WebProxy proxy = new WebProxy(textBox4.Text, Convert.ToInt32(textBox5.Text));
                    proxy.UseDefaultCredentials = false;
                    proxy.BypassProxyOnLocal = false;

                    WClient.Proxy = proxy;
                }
               
                WClient.DownloadFile(textBox1.Text, path);


                WClient.Dispose();
            }
            catch(WebException eee)
           {
           }
            catch(ArgumentException ee)
           {
           }
            */
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox6.Text != "")
            {
                try
                {
                    click = Convert.ToInt32(textBox6.Text);
                }
                catch
                {

                }
            }
            click++;
            textBox3.Text = click.ToString();
            textBox6.Text = click.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox6.Text != "")
            {
                try
                {
                    click = Convert.ToInt32(textBox6.Text);
                }
                catch
                {

                }
            }
            click--;
            textBox3.Text = click.ToString();
            textBox6.Text = click.ToString();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox3_TextClick(object sender, KeyEventArgs e)
        {
            textBox3.Text = textBox6.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(textBox1.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox8.Text != "")
            {
                try
                {
                    dirCount = Convert.ToInt32(textBox8.Text);
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

            textBox8.Text = dirCount.ToString();

            if (fi.Length == 0) ;
            else
            {
                textBox2.Text = fi[dirCount-1].Name.ToString();
            }


            if (textBox6.Text != "")
            {
                try
                {
                    click = Convert.ToInt32(textBox6.Text);
                }
                catch
                {

                }
            }
            click = dirCount;
            textBox3.Text = dirCount.ToString();
            textBox6.Text = dirCount.ToString();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox8.Text != "")
            {
                try
                {
                    dirCount = Convert.ToInt32(textBox8.Text);
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

            textBox8.Text = dirCount.ToString();

            if (fi.Length == 0) ;
            else
            {
                textBox2.Text = fi[dirCount-1].Name.ToString();
            }



            if (textBox6.Text != "")
            {
                try
                {
                    click = Convert.ToInt32(textBox6.Text);
                }
                catch
                {

                }
            }
            click = dirCount;
            textBox3.Text = dirCount.ToString();
            textBox6.Text = dirCount.ToString();

            getPage();
        }
    }
}
