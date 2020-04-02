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

        public PictureBox[] imgPage_img = new PictureBox[5];
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
            contentNo = 0;
            start = 1;
            client.Encoding = Encoding.UTF8;

            for (int i = 0; i < 5; i++)
            {
                imgPage_title[i] = new Label();
                imgPage_img[i] = new PictureBox();
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
                    imgPage_img[i].MouseClick += new System.Windows.Forms.MouseEventHandler(mainForm.goTextClass);
                else
                    imgPage_img[i].MouseClick += new System.Windows.Forms.MouseEventHandler(mainForm.goOneTextClass);

                imgPage_img[i].DoubleClick += new EventHandler(mainForm.downImageFunClass);

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
                string uri = "https://www.googleapis.com/customsearch/v1?searchType=image&cx=000379657054140898502:5o98cmmg4va&key=AIzaSyCa_x19zmBMcvo5fKOluRFBNaJmg6_C-z8&start=" + start + "&q=" + searchText;

                String response = client.DownloadString(uri);
                JsonDocument document = JsonDocument.Parse(response);
                JsonElement root = document.RootElement;

                if (!root.TryGetProperty("items", out items))
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
                    items = root.GetProperty("items");

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
                        String title = titleElement.GetString();
                        imgPage_title[i - contentNo].Text = title;
                    }
                    if(item.TryGetProperty("link", out JsonElement linkElement))
                    {
                        String link = linkElement.GetString();
                        imgPage_img[i - contentNo].Name = (pageNo + 1).ToString() + ";" + link + ";" + pageNoBox.Text;
                    }
                    if (item.TryGetProperty("image", out JsonElement image))
                    {
                        if (image.TryGetProperty("height", out JsonElement heightElement) && image.TryGetProperty("width", out JsonElement widthElement))
                        {
                            int height = heightElement.GetInt32();
                            int width = widthElement.GetInt32();

                            imgSize_label[i - contentNo].Text = width + " * " + height;
                        }
                        if (image.TryGetProperty("thumbnailLink", out JsonElement thumbnailLinkElement))
                        {
                            String thumbnailLink = thumbnailLinkElement.GetString();
                            byte[] myDataBuffer = WClient.DownloadData(thumbnailLink);
                            Stream stream = new MemoryStream();
                            stream.Write(myDataBuffer, 0, myDataBuffer.Length);
                            imgPage_img[i - contentNo].BackgroundImage = Image.FromStream(stream, true);
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
            contentNo += 5;

            if(start < 100 && contentNo >= items.GetArrayLength())
            {
                contentNo = 0;
                start += 10;
                getPage(fileName.Text);
                getPic();
            }
            else if(start < 100)
            {
                contentNo = 5;
                getPic();
            }
            else
            {
                contentNo = 5;
            }
        }
        public void prevPic(object sender, MouseEventArgs e)
        {
            contentNo -= 5;

            if (start > 1 && contentNo < 0)
            {
                contentNo = 5;
                start -= 10;
                getPage(fileName.Text);
                getPic();
            }
            else if(start > 1)
            {
                contentNo = 0;
                getPic();
            }
            else
            {
                contentNo = 0;
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
