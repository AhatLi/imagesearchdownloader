using System;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;
using System.Text.Json;

namespace ImageSearchDownloader
{
    public class ImagePage
    {
        public int pageNo;
        public int start;
        public TextBox fileName;
        public TextBox pageNoBox;
        public Button reload;
        public Button nextBtn;
        public Button prevBtn;
        public Panel panel;
        MyWebClient client;
        Form1 mainForm;
        JsonElement items;
        int contentNo;
        static Label label;

        public MyPictureBox[] imgPage_img = new MyPictureBox[5];
        public Label[] imgPage_title = new Label[5];
        public Label[] imgSize_label = new Label[5];
        public Button[] c = new Button[5];

        public ImagePage(Form1 form)
        {
            mainForm = form;
            fileName = new TextBox();
            pageNoBox = new TextBox();
            reload = new Button();
            nextBtn = new Button();
            prevBtn = new Button();
            panel = new Panel();
            client = new MyWebClient(3);
            client.Encoding = Encoding.UTF8;
            contentNo = 0;
            start = 0;

            for (int i = 0; i < 5; i++)
            {
                imgPage_title[i] = new Label();
                imgPage_img[i] = new MyPictureBox();
                imgSize_label[i] = new Label();
                c[i] = new Button();
            }
        }

        public void makePage(int pageno, int mode = 0)
        {
            for (int i = 0; i < 5; i++)
            {
                imgPage_title[i].Location = new Point(10 + (i * 310), 30);
                imgPage_title[i].Size = new Size(300, 20);
                imgPage_title[i].Parent = panel;
                panel.Controls.Add(imgPage_title[i]);

                imgPage_img[i].Location = new Point(10 + (i * 310), 50);
                imgPage_img[i].Size = new Size(300, 200);
                imgPage_img[i].Parent = panel;
                imgPage_img[i].BackgroundImageLayout = ImageLayout.Zoom;
                panel.Controls.Add(imgPage_img[i]);

                if (mode == 0)
                {
                    imgPage_img[i].MouseClick += new System.Windows.Forms.MouseEventHandler(mainForm.goTextMode0Class);
                    imgPage_img[i].DoubleClick += new EventHandler(mainForm.downImageFunMode0Class);
                }
                else
                {
                    imgPage_img[i].MouseClick += new System.Windows.Forms.MouseEventHandler(mainForm.goTextMode1Class);
                    imgPage_img[i].DoubleClick += new EventHandler(mainForm.downImageFunMode1Class);
                }

                imgSize_label[i].Location = new Point(100 + (i * 310), 255);
                imgSize_label[i].Size = new Size(100, 20);
                imgSize_label[i].Parent = panel;
                panel.Controls.Add(imgSize_label[i]);
                imgSize_label[i].Text = "333";
            }
            pageNoBox.Location = new Point(10, 0);
            pageNoBox.Size = new Size(30, 21);
            pageNoBox.Parent = panel;
            panel.Controls.Add(pageNoBox);
            pageNoBox.Text = (pageno + 1).ToString();

            reload.Location = new Point(560, 0);
            reload.Size = new Size(100, 21);
            reload.Text = "다시읽기";
            reload.MouseClick += (sender, evt) =>
            {
                reloadPage(fileName.Text);
            };
            panel.Controls.Add(reload);

            nextBtn.Location = new Point(455, 0);
            nextBtn.Size = new Size(100, 21);
            nextBtn.Text = "다음그림";
            nextBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(nextPic);
            panel.Controls.Add(nextBtn);

            prevBtn.Location = new Point(45, 0);
            prevBtn.Size = new Size(100, 21);
            prevBtn.Text = "이전그림";
            prevBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(prevPic);
            panel.Controls.Add(prevBtn);

            fileName.Location = new Point(150, 0);
            fileName.Size = new Size(300, 21);
            fileName.Parent = panel;
            panel.Controls.Add(fileName);

            panel.BorderStyle = BorderStyle.FixedSingle;
        }

        public void clearPage(int pageno)
        {
            panel.Controls.Clear();
        }

        public void getPage(String searchText)
        {
            try
            {
                fileName.Text = searchText;

                string uri = "https://cse.google.com/cse/element/v1?rsz=filtered_cse&num=20&hl=en&source=gcsc&gss=.com&cselibv=8b2252448421acb3&searchtype=image&cx=000379657054140898502:5o98cmmg4va&safe=off&cse_tok=AJvRUv2amDHFh_Uj1EDdkxaUznKA:1585806963254&exp=csqr,cc&callback=google.search.cse.api12203&start=" + start + "&q=" + searchText;

                String response = client.DownloadString(uri);
                response = response.Substring(response.IndexOf("(") + 1, response.LastIndexOf(")") - (response.IndexOf("(") + 1));
                JsonDocument document = JsonDocument.Parse(response);
                JsonElement root = document.RootElement;

                if (!root.TryGetProperty("results", out items))
                {
                    JsonElement error;
                    if (root.TryGetProperty("error", out error))
                    {
                        JsonElement code;
                        JsonElement message;
                        String errorMessage = "ERROR !, ";
                        if (error.TryGetProperty("error", out code))
                        {
                            errorMessage = code.GetInt32().ToString();
                        }
                        errorMessage += " : ";
                        if (error.TryGetProperty("error", out message))
                        {
                            errorMessage += message.GetString().ToString();
                        }
                        MessageBox.Show(errorMessage);
                    }
                    else
                    {
                        MessageBox.Show("아이템이 없음");
                    }
                }
                else
                {
                    getPic();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        void getPic()
        {
            MyWebClient WClient = new MyWebClient(10);

            try
            {
                int i = -1;
                foreach (JsonElement item in items.EnumerateArray())
                {
                    i++;
                    if (contentNo > i)
                        continue;

                    if (i - contentNo >= 5)
                        break;

                    if(item.TryGetProperty("title", out JsonElement titleElement))
                    {
                        String title = titleElement.GetString().Replace("<b>", "").Replace("</b>", ""); ;
                        imgPage_title[i - contentNo].Text = title;
                    }
                    if(item.TryGetProperty("url", out JsonElement linkElement))
                    {
                        String link = linkElement.GetString();

                        imgPage_img[i - contentNo].pageNo = pageNoBox.Text.ToString();
                        imgPage_img[i - contentNo].imgLink = link;
                    }
                    if (item.TryGetProperty("height", out JsonElement heightElement) && item.TryGetProperty("width", out JsonElement widthElement))
                    {
                        String height = heightElement.GetString();
                        String width = widthElement.GetString();

                        imgSize_label[i - contentNo].Text = width + " * " + height;
                    }
                    if (item.TryGetProperty("tbMedUrl", out JsonElement thumbnailLinkElement))
                    {
                        String thumbnailLink = thumbnailLinkElement.GetString();
                        byte[] myDataBuffer = WClient.DownloadData(thumbnailLink);
                        Stream stream = new MemoryStream();
                        stream.Write(myDataBuffer, 0, myDataBuffer.Length);
                        imgPage_img[i - contentNo].BackgroundImage = Image.FromStream(stream, true);
                    }
                    if (item.TryGetProperty("fileFormat", out JsonElement extElement))
                    {
                        String ext = extElement.GetString();
                        if(ext == "image/jpeg")
                        {
                            imgPage_img[i - contentNo].ext = ".jpg";
                        }
                        else if (ext == "image/png")
                        {
                            imgPage_img[i - contentNo].ext = ".png";
                        }
                        else if (ext == "image/gif")
                        {
                            imgPage_img[i - contentNo].ext = ".gif";
                        }
                        else
                        {
                            imgPage_img[i - contentNo].ext = ".jpg";
                        }
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void nextPic(object sender, MouseEventArgs e)
        {
            int tmp = contentNo;
            contentNo += 5;

            if(start < 500 && contentNo >= items.GetArrayLength())
            {
                contentNo = 0;
                start += 20;
                getPage(fileName.Text);
                getPic();
            }
            else if(start >= 500 && contentNo >= items.GetArrayLength())
            {
                contentNo = 15;
            }

            if(tmp != contentNo)
            {
                getPic();
            }
        }
        public void prevPic(object sender, MouseEventArgs e)
        {
            int tmp = contentNo;
            contentNo -= 5;

            if (start > 1 && contentNo < 0)
            {
                contentNo = 15;
                start -= 20;
                getPage(fileName.Text);
                getPic();
            }
            else if (start < 1 && contentNo < 0)
            {
                contentNo = 0;
            }

            if (tmp != contentNo)
            {
                getPic();
            }
        }

        public void reloadPage(String name)
        {
            start = 1;
            contentNo = 0;
            getPage(name);
        }
    };
}
