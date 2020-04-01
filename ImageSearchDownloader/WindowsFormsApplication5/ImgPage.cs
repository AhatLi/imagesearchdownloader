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
        public TextBox fileName;
        public TextBox pageNoBox;
        public String response;
        public String size;
        public String imgsrc;
        public Button reload;
        public Button nextBtn;
        public Panel panel;
        WebBrowser webBrowser;
        Form1 mainForm;
        

        public PictureBox[] imgPage_img = new PictureBox[5];
        public Label[] imgPage_title = new Label[5];
        public Label[] imgSize_label = new Label[5];
        public Button[] c = new Button[5];

        public ImagePage(WebBrowser webBrowser, Form1 form)
        {
            mainForm = form;
            fileName = new TextBox();
            pageNoBox = new TextBox();
            reload = new Button();
            nextBtn = new Button();
            panel = new Panel();
            this.webBrowser = webBrowser;

            for (int i = 0; i < 5; i++)
            {
                imgPage_title[i] = new Label();
                imgPage_img[i] = new PictureBox();
                imgSize_label[i] = new Label();
                c[i] = new Button();
            }
        }

        public void getPage(String searchText)
        {
            Encoding encoding = Encoding.GetEncoding(737);
            System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
            //변환하고자 하는 문자열을 UTF8 방식으로 변환하여 byte 배열로 반환
            byte[] utf8Bytes;

            fileName.Text = searchText;
            utf8Bytes = utf8.GetBytes(searchText);

            //UTF-8을 string으로 변한
            string utf8String = "";
            foreach (byte b in utf8Bytes)
            {
                utf8String += "%" + String.Format("{0:X}", b);
            }
            /*
             * https://www.googleapis.com/customsearch/v1?searchType=image&cx=000379657054140898502:5o98cmmg4va&key=AIzaSyCa_x19zmBMcvo5fKOluRFBNaJmg6_C-z8&q=RJ166716
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
            */
            response = "{  \"kind\": \"customsearch#search\",  \"url\": {    \"type\": \"application/json\",    \"template\": \"https://www.googleapis.com/customsearch/v1?q={searchTerms}&num={count?}&start={startIndex?}&lr={language?}&safe={safe?}&cx={cx?}&sort={sort?}&filter={filter?}&gl={gl?}&cr={cr?}&googlehost={googleHost?}&c2coff={disableCnTwTranslation?}&hq={hq?}&hl={hl?}&siteSearch={siteSearch?}&siteSearchFilter={siteSearchFilter?}&exactTerms={exactTerms?}&excludeTerms={excludeTerms?}&linkSite={linkSite?}&orTerms={orTerms?}&relatedSite={relatedSite?}&dateRestrict={dateRestrict?}&lowRange={lowRange?}&highRange={highRange?}&searchType={searchType}&fileType={fileType?}&rights={rights?}&imgSize={imgSize?}&imgType={imgType?}&imgColorType={imgColorType?}&imgDominantColor={imgDominantColor?}&alt=json\"  },  \"queries\": {    \"request\": [      {        \"title\": \"Google Custom Search - RJ166716\",        \"totalResults\": \"197\",        \"searchTerms\": \"RJ166716\",        \"count\": 10,        \"startIndex\": 1,        \"inputEncoding\": \"utf8\",        \"outputEncoding\": \"utf8\",        \"safe\": \"off\",        \"cx\": \"000379657054140898502:5o98cmmg4va\",        \"searchType\": \"image\"      }    ],    \"nextPage\": [      {        \"title\": \"Google Custom Search - RJ166716\",        \"totalResults\": \"197\",        \"searchTerms\": \"RJ166716\",        \"count\": 10,        \"startIndex\": 11,        \"inputEncoding\": \"utf8\",        \"outputEncoding\": \"utf8\",        \"safe\": \"off\",        \"cx\": \"000379657054140898502:5o98cmmg4va\",        \"searchType\": \"image\"      }    ]  },  \"context\": {    \"title\": \"Dlsite\"  },  \"searchInformation\": {    \"searchTime\": 0.46574,    \"formattedSearchTime\": \"0.47\",    \"totalResults\": \"197\",    \"formattedTotalResults\": \"197\"  },  \"items\": [    {      \"kind\": \"customsearch#result\",      \"title\": \"50%OFF】星☆空すれいぶ前編 [GLAMOUR WORKS] | DLsite 同人 - R18\",      \"htmlTitle\": \"50%OFF】星☆空すれいぶ前編 [GLAMOUR WORKS] | DLsite 同人 - R18\",      \"link\": \"https://img.dlsite.jp/modpub/images2/work/doujin/RJ167000/RJ166716_img_main.jpg\",      \"displayLink\": \"www.dlsite.com\",      \"snippet\": \"50%OFF】星☆空すれいぶ前編 [GLAMOUR WORKS] | DLsite 同人 - R18\",      \"htmlSnippet\": \"50%OFF】星☆空すれいぶ前編 [GLAMOUR WORKS] | DLsite 同人 - R18\",      \"mime\": \"image/jpeg\",      \"fileFormat\": \"image/jpeg\",      \"image\": {        \"contextLink\": \"https://www.dlsite.com/maniax/work/=/product_id/RJ166716/?medium=mail&program=20151130&source=mail_dlpc_news&utm_campaign=mailmagazine&utm_medium=email&utm_content=dlpc_html_20151130\",        \"height\": 420,        \"width\": 560,        \"byteSize\": 98056,        \"thumbnailLink\": \"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTq0oruZgs757eA8LpKZ7A8nscvh6q0aFsT931NUg3aGVHzELtnFucE2hU&s\",        \"thumbnailHeight\": 100,        \"thumbnailWidth\": 133      }    },    {      \"kind\": \"customsearch#result\",      \"title\": \"151130] (同人CG集) [GLAMOUR WORKS (蓮斗)] 星☆空すれいぶ 総集編 ...\",      \"htmlTitle\": \"151130] (同人CG集) [GLAMOUR WORKS (蓮斗)] 星☆空すれいぶ 総集編 ...\",      \"link\": \"https://t20.pixhost.to/thumbs/300/73778344_rj166716_img_smp2.jpg\",      \"displayLink\": \"www.anime-sharing.com\",      \"snippet\": \"151130] (同人CG集) [GLAMOUR WORKS (蓮斗)] 星☆空すれいぶ 総集編 ...\",      \"htmlSnippet\": \"151130] (同人CG集) [GLAMOUR WORKS (蓮斗)] 星☆空すれいぶ 総集編 ...\",      \"mime\": \"image/jpeg\",      \"fileFormat\": \"image/jpeg\",      \"image\": {        \"contextLink\": \"http://www.anime-sharing.com/forum/hentai-cg-packs-24/%5B151130%5D-%E5%90%8C%E4%BA%BAcg%E9%9B%86-%5Bglamour-works-%E8%93%AE%E6%96%97-%5D-%E6%98%9F%E2%98%86%E7%A9%BA%E3%81%99%E3%82%8C%E3%81%84%E3%81%B6-%E7%B7%8F%E9%9B%86%E7%B7%A8-%E3%82%B9%E3%83%9E%E3%82%A4%E3%83%AB%E3%83%97%E3%83%AA%E3%82%AD%E3%83%A5%E3%82%A2-711725/\",        \"height\": 150,        \"width\": 200,        \"byteSize\": 7585,        \"thumbnailLink\": \"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTJG7D1l3WA4cNWASQqmQ1MsxgfqBNKYLY5eo3aGs7OueEOtTQyzbQiPg&s\",        \"thumbnailHeight\": 78,        \"thumbnailWidth\": 104      }    },    {      \"kind\": \"customsearch#result\",      \"title\": \"星☆空すれいぶ前編 [GLAMOUR WORKS] | DLsite 同人 - R18\",      \"htmlTitle\": \"星☆空すれいぶ前編 [GLAMOUR WORKS] | DLsite 同人 - R18\",      \"link\": \"https://img.dlsite.jp/modpub/images2/work/doujin/RJ167000/RJ166716_img_smp3.jpg\",      \"displayLink\": \"www.dlsite.com\",      \"snippet\": \"星☆空すれいぶ前編 [GLAMOUR WORKS] | DLsite 同人 - R18\",      \"htmlSnippet\": \"星☆空すれいぶ前編 [GLAMOUR WORKS] | DLsite 同人 - R18\",      \"mime\": \"image/jpeg\",      \"fileFormat\": \"image/jpeg\",      \"image\": {        \"contextLink\": \"https://www.dlsite.com/maniax/work/=/product_id/RJ166716.html\",        \"height\": 420,        \"width\": 560,        \"byteSize\": 56501,        \"thumbnailLink\": \"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTW3OoQAp6ZSIFpgwBrY9RftLhBAiKuTsGyZssDH-D6BQuUrXslk2nlME4&s\",        \"thumbnailHeight\": 100,        \"thumbnailWidth\": 133      }    },    {      \"kind\": \"customsearch#result\",      \"title\": \"151130] (同人CG集) [GLAMOUR WORKS (蓮斗)] 星☆空すれいぶ 総集編 ...\",      \"htmlTitle\": \"151130] (同人CG集) [GLAMOUR WORKS (蓮斗)] 星☆空すれいぶ 総集編 ...\",      \"link\": \"https://t20.pixhost.to/thumbs/300/73778364_rj166716_img_main.jpg\",      \"displayLink\": \"www.anime-sharing.com\",      \"snippet\": \"151130] (同人CG集) [GLAMOUR WORKS (蓮斗)] 星☆空すれいぶ 総集編 ...\",      \"htmlSnippet\": \"151130] (同人CG集) [GLAMOUR WORKS (蓮斗)] 星☆空すれいぶ 総集編 ...\",      \"mime\": \"image/jpeg\",      \"fileFormat\": \"image/jpeg\",      \"image\": {        \"contextLink\": \"http://www.anime-sharing.com/forum/hentai-cg-packs-24/%5B151130%5D-%E5%90%8C%E4%BA%BAcg%E9%9B%86-%5Bglamour-works-%E8%93%AE%E6%96%97-%5D-%E6%98%9F%E2%98%86%E7%A9%BA%E3%81%99%E3%82%8C%E3%81%84%E3%81%B6-%E7%B7%8F%E9%9B%86%E7%B7%A8-%E3%82%B9%E3%83%9E%E3%82%A4%E3%83%AB%E3%83%97%E3%83%AA%E3%82%AD%E3%83%A5%E3%82%A2-711725/\",        \"height\": 350,        \"width\": 466,        \"byteSize\": 46665,        \"thumbnailLink\": \"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTyBcJP2vnDSEn_15UtklF4EC8ypQR__tpwFW1ZueEQh0lrgKmrNLPsdfQ&s\",        \"thumbnailHeight\": 96,        \"thumbnailWidth\": 128      }    },    {      \"kind\": \"customsearch#result\",      \"title\": \"星☆空すれいぶ前編 [GLAMOUR WORKS] | DLsite 同人 - R18\",      \"htmlTitle\": \"星☆空すれいぶ前編 [GLAMOUR WORKS] | DLsite 同人 - R18\",      \"link\": \"https://img.dlsite.jp/modpub/images2/work/doujin/RJ167000/RJ166716_img_smp2.jpg\",      \"displayLink\": \"www.dlsite.com\",      \"snippet\": \"星☆空すれいぶ前編 [GLAMOUR WORKS] | DLsite 同人 - R18\",      \"htmlSnippet\": \"星☆空すれいぶ前編 [GLAMOUR WORKS] | DLsite 同人 - R18\",      \"mime\": \"image/jpeg\",      \"fileFormat\": \"image/jpeg\",      \"image\": {        \"contextLink\": \"https://www.dlsite.com/maniax/work/=/product_id/RJ166716.html\",        \"height\": 420,        \"width\": 560,        \"byteSize\": 49477,        \"thumbnailLink\": \"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS1ibJ6togriEVwmgsKWU3zxWXuF6-bEW1qowwy3PDfMY94yZiaCoE7yVA&s\",        \"thumbnailHeight\": 100,        \"thumbnailWidth\": 133      }    },    {      \"kind\": \"customsearch#result\",      \"title\": \"星☆空すれいぶ前編 [GLAMOUR WORKS] | DLsite 同人 - R18\",      \"htmlTitle\": \"星☆空すれいぶ前編 [GLAMOUR WORKS] | DLsite 同人 - R18\",      \"link\": \"https://media.dlsite.com/proxy/6567c4efbe22f8d5c4dcbfc8c488f489f6ee0c37/687474703a2f2f696d672e646c736974652e6a702f6d6f647075622f696d61676573322f776f726b2f646f756a696e2f524a3133303030302f524a3132393033375f696d675f736d70312e6a7067\",      \"displayLink\": \"www.dlsite.com\",      \"snippet\": \"星☆空すれいぶ前編 [GLAMOUR WORKS] | DLsite 同人 - R18\",      \"htmlSnippet\": \"星☆空すれいぶ前編 [GLAMOUR WORKS] | DLsite 同人 - R18\",      \"mime\": \"image/\",      \"fileFormat\": \"image/\",      \"image\": {        \"contextLink\": \"https://www.dlsite.com/maniax/work/=/product_id/RJ166716.html\",        \"height\": 593,        \"width\": 779,        \"byteSize\": 145495,        \"thumbnailLink\": \"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQx_i7faun-O-Ju5IcCjHJJc2GtcfAxM3_5zbSyWsJAtN_5Zzsf6GUjpHA&s\",        \"thumbnailHeight\": 108,        \"thumbnailWidth\": 142      }    },    {      \"kind\": \"customsearch#result\",      \"title\": \"星☆空すれいぶ前編 [GLAMOUR WORKS] | DLsite 同人 - R18\",      \"htmlTitle\": \"星☆空すれいぶ前編 [GLAMOUR WORKS] | DLsite 同人 - R18\",      \"link\": \"https://img.dlsite.jp/modpub/images2/work/doujin/RJ167000/RJ166716_img_smp1.jpg\",      \"displayLink\": \"www.dlsite.com\",      \"snippet\": \"星☆空すれいぶ前編 [GLAMOUR WORKS] | DLsite 同人 - R18\",      \"htmlSnippet\": \"星☆空すれいぶ前編 [GLAMOUR WORKS] | DLsite 同人 - R18\",      \"mime\": \"image/jpeg\",      \"fileFormat\": \"image/jpeg\",      \"image\": {        \"contextLink\": \"https://www.dlsite.com/maniax/work/=/product_id/RJ166716.html\",        \"height\": 420,        \"width\": 560,        \"byteSize\": 79965,        \"thumbnailLink\": \"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSaEj95swvgiqsI6Y9WbuYira8GGKtiLNjvtk0qbp_rd4Am2X8Ma7g66Tsp&s\",        \"thumbnailHeight\": 100,        \"thumbnailWidth\": 133      }    },    {      \"kind\": \"customsearch#result\",      \"title\": \"新＊ 【2016】[HCG][GLAMOUR WORKS] 星☆空すれいぶ前編2\",      \"htmlTitle\": \"新＊ 【2016】[HCG][GLAMOUR WORKS] 星☆空すれいぶ前編2\",      \"link\": \"http://i.want.tf/to/that/R4Ojd.jpg\",      \"displayLink\": \"www.anime-sharing.com\",      \"snippet\": \"新＊ 【2016】[HCG][GLAMOUR WORKS] 星☆空すれいぶ前編2\",      \"htmlSnippet\": \"新＊ 【2016】[HCG][GLAMOUR WORKS] 星☆空すれいぶ前編2\",      \"mime\": \"image/jpeg\",      \"fileFormat\": \"image/jpeg\",      \"image\": {        \"contextLink\": \"http://www.anime-sharing.com/forum/hentai-cg-packs-24/%EF%BC%8A%E6%96%B0%EF%BC%8A-%E3%80%902016%E3%80%91%5Bhcg%5D%5Bglamour-works%5D-%E6%98%9F%E2%98%86%E7%A9%BA%E3%81%99%E3%82%8C%E3%81%84%E3%81%B6%E5%89%8D%E7%B7%A82-462674/\",        \"height\": 300,        \"width\": 400,        \"byteSize\": 45601,        \"thumbnailLink\": \"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ3DY2ucfFM5J17kYmPin8EIqcK77RaO-IhtPxrNFJsRa8biCZuiC39wgc&s\",        \"thumbnailHeight\": 93,        \"thumbnailWidth\": 124      }    },    {      \"kind\": \"customsearch#result\",      \"title\": \"星☆空すれいぶ総集編 [GLAMOUR WORKS] | DLsite 同人 - R18\",      \"htmlTitle\": \"星☆空すれいぶ総集編 [GLAMOUR WORKS] | DLsite 同人 - R18\",      \"link\": \"https://img.dlsite.jp/modpub/images2/work/doujin/RJ209000/RJ208127_img_smp1.jpg\",      \"displayLink\": \"www.dlsite.com\",      \"snippet\": \"星☆空すれいぶ総集編 [GLAMOUR WORKS] | DLsite 同人 - R18\",      \"htmlSnippet\": \"星☆空すれいぶ総集編 [GLAMOUR WORKS] | DLsite 同人 - R18\",      \"mime\": \"image/jpeg\",      \"fileFormat\": \"image/jpeg\",      \"image\": {        \"contextLink\": \"https://www.dlsite.com/maniax/work/=/product_id/RJ208127.html\",        \"height\": 420,        \"width\": 560,        \"byteSize\": 89885,        \"thumbnailLink\": \"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRW9C-tmWEsPyJngJpVlnlDWmdGULwxzuZQ6qWhWb1wgqEXd659cTtKB-A&s\",        \"thumbnailHeight\": 100,        \"thumbnailWidth\": 133      }    },    {      \"kind\": \"customsearch#result\",      \"title\": \"RJ208127] 星☆空すれいぶ総集編 のDL情報 | DLDShare.net\",      \"htmlTitle\": \"RJ208127] 星☆空すれいぶ総集編 のDL情報 | DLDShare.net\",      \"link\": \"https://dldshare.net/wp-content/uploads/2017/11/RJ208127_img_main.jpg\",      \"displayLink\": \"dldshare.net\",      \"snippet\": \"RJ208127] 星☆空すれいぶ総集編 のDL情報 | DLDShare.net\",      \"htmlSnippet\": \"RJ208127] 星☆空すれいぶ総集編 のDL情報 | DLDShare.net\",      \"mime\": \"image/jpeg\",      \"fileFormat\": \"image/jpeg\",      \"image\": {        \"contextLink\": \"https://dldshare.net/archives/18439\",        \"height\": 420,        \"width\": 560,        \"byteSize\": 158144,        \"thumbnailLink\": \"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQkFucaBzjJ5cXK83ftf3YPKSnzpJhecWRon6PR94alFypP9d8y1YOehSc&s\",        \"thumbnailHeight\": 100,        \"thumbnailWidth\": 133      }    }  ]}";





            getPic();
        }

        void getPic()
        {
            MyWebClient WClient = new MyWebClient(10);

            JsonDocument document = JsonDocument.Parse(response);

            JsonElement root = document.RootElement;
            JsonElement items = root.GetProperty("items");

            try
            {
                int i = 0;
                foreach (JsonElement item in items.EnumerateArray())
                {
                    item.TryGetProperty("title", out JsonElement titleElement);
                    item.TryGetProperty("link", out JsonElement linkElement);
                    item.TryGetProperty("image", out JsonElement image);
                    image.TryGetProperty("height", out JsonElement heightElement);
                    image.TryGetProperty("width", out JsonElement widthElement);
                    image.TryGetProperty("thumbnailLink", out JsonElement thumbnailLinkElement);

                    String title = titleElement.GetString();
                    String link = linkElement.GetString();
                    int height = heightElement.GetInt32();
                    int width = widthElement.GetInt32();
                    String thumbnailLink = thumbnailLinkElement.GetString();

                    byte[] myDataBuffer = WClient.DownloadData(thumbnailLink);
                    Stream stream = new MemoryStream();
                    stream.Write(myDataBuffer, 0, myDataBuffer.Length);

                    imgPage_img[i].BackgroundImage = Image.FromStream(stream, true);
                    imgPage_title[i].Text = title;
                    imgPage_img[i].Name = (pageNo + 1).ToString() + ";" + link + ";" + pageNoBox.Text;
                    imgSize_label[i].Text = width + " * " + height;

                    i++;

                    if (i >= 5)
                        break;
                }
            }
            catch
            {

            }
        }

        public void makePage(int pageno)
        {
            for (int i = 0; i < 5; i++)
            {
                imgPage_title[i].Location = new Point(30 + (i * 310), 30);
                imgPage_title[i].Size = new Size(300, 20);
                imgPage_title[i].Parent = panel;
                panel.Controls.Add(imgPage_title[i]);

                imgPage_img[i].Location = new Point(20 + (i * 310), 50);
                imgPage_img[i].Size = new Size(300, 200);
                imgPage_img[i].Parent = panel;
                imgPage_img[i].BackgroundImageLayout = ImageLayout.Zoom;
                panel.Controls.Add(imgPage_img[i]);
                
           //     if (mode == 0)
                    imgPage_img[i].MouseClick += new System.Windows.Forms.MouseEventHandler(mainForm.goTextClass);
          //      else
          //          imgPage_img[i].MouseClick += new System.Windows.Forms.MouseEventHandler(mainForm.goOneTextClass);
                    

                
                imgPage_img[i].DoubleClick += new EventHandler(mainForm.downImageFunClass);
                

                imgSize_label[i].Location = new Point(50 + (i * 310), 255);
                imgSize_label[i].Size = new Size(300, 21);
                imgSize_label[i].Parent = panel;
                panel.Controls.Add(imgSize_label[i]);
                imgSize_label[i].Text = "333";
            }
            pageNoBox.Location = new Point(10, 0);
            pageNoBox.Size = new Size(30, 21);
            pageNoBox.Parent = panel;
            panel.Controls.Add(pageNoBox);
            pageNoBox.Text = (pageno + 1).ToString();

            reload.Location = new Point(360, 0);
            reload.Size = new Size(100, 21);
            reload.Text = "다시읽기";
            reload.MouseClick += (sender, evt) =>
            {
                reloadPage(fileName.Text);
            };
            panel.Controls.Add(reload);

            nextBtn.Location = new Point(460, 0);
            nextBtn.Size = new Size(100, 21);
            nextBtn.Text = "다음그림";
            nextBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(nextPic);

            panel.Controls.Add(nextBtn);

            fileName.Location = new Point(60, 0);
            fileName.Size = new Size(300, 21);
            fileName.Parent = panel;
            panel.Controls.Add(fileName);

            panel.BorderStyle = BorderStyle.FixedSingle;
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

        public void reloadPage(String name)
        {
            getPage(name);
        }
    };
}
