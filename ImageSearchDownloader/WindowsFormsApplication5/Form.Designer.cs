namespace ImageSearchDownloader
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.p1NextBtn = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.p1StartBtn = new System.Windows.Forms.Button();
            this.p1SearchText = new System.Windows.Forms.TextBox();
            this.fileSrcText = new System.Windows.Forms.TextBox();
            this.p1NameText = new System.Windows.Forms.TextBox();
            this.p1DownBtn = new System.Windows.Forms.Button();
            this.p1Label3 = new System.Windows.Forms.Label();
            this.fileLabel = new System.Windows.Forms.Label();
            this.p1Label2 = new System.Windows.Forms.Label();
            this.proxyText = new System.Windows.Forms.TextBox();
            this.proxyLabel = new System.Windows.Forms.Label();
            this.proxyCheck = new System.Windows.Forms.CheckBox();
            this.p1PageNextBtn = new System.Windows.Forms.Button();
            this.proxyPortText = new System.Windows.Forms.TextBox();
            this.p1PagePrevBtn = new System.Windows.Forms.Button();
            this.p1PageText = new System.Windows.Forms.TextBox();
            this.p1Label1 = new System.Windows.Forms.Label();
            this.p1SearchAddText = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.p1DirPrevBtn = new System.Windows.Forms.Button();
            this.p1DirNextBtn = new System.Windows.Forms.Button();
            this.p1DirText = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.p2Label2 = new System.Windows.Forms.Label();
            this.p2SearchText = new System.Windows.Forms.TextBox();
            this.p2Check1 = new System.Windows.Forms.CheckBox();
            this.p2StartBtn = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.webBrowser2 = new System.Windows.Forms.WebBrowser();
            this.p3Text = new System.Windows.Forms.TextBox();
            this.p3StartBtn = new System.Windows.Forms.Button();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // p1NextBtn
            // 
            this.p1NextBtn.Location = new System.Drawing.Point(314, 5);
            this.p1NextBtn.Name = "p1NextBtn";
            this.p1NextBtn.Size = new System.Drawing.Size(75, 23);
            this.p1NextBtn.TabIndex = 0;
            this.p1NextBtn.Text = "다음이미지";
            this.p1NextBtn.UseVisualStyleBackColor = true;
            this.p1NextBtn.Click += new System.EventHandler(this.p1NextBtn_Click_1);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // p1StartBtn
            // 
            this.p1StartBtn.Location = new System.Drawing.Point(189, 5);
            this.p1StartBtn.Name = "p1StartBtn";
            this.p1StartBtn.Size = new System.Drawing.Size(119, 23);
            this.p1StartBtn.TabIndex = 6;
            this.p1StartBtn.Text = "페이지 읽어들이기";
            this.p1StartBtn.UseVisualStyleBackColor = true;
            this.p1StartBtn.Click += new System.EventHandler(this.p1StartBtn_Click);
            // 
            // p1SearchText
            // 
            this.p1SearchText.Location = new System.Drawing.Point(57, 35);
            this.p1SearchText.Name = "p1SearchText";
            this.p1SearchText.Size = new System.Drawing.Size(311, 21);
            this.p1SearchText.TabIndex = 7;
            // 
            // fileSrcText
            // 
            this.fileSrcText.Location = new System.Drawing.Point(876, 8);
            this.fileSrcText.Name = "fileSrcText";
            this.fileSrcText.Size = new System.Drawing.Size(181, 21);
            this.fileSrcText.TabIndex = 8;
            // 
            // p1NameText
            // 
            this.p1NameText.Location = new System.Drawing.Point(989, 7);
            this.p1NameText.Name = "p1NameText";
            this.p1NameText.Size = new System.Drawing.Size(103, 21);
            this.p1NameText.TabIndex = 8;
            this.p1NameText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.p1NameText_TextClick);
            // 
            // p1DownBtn
            // 
            this.p1DownBtn.Location = new System.Drawing.Point(1098, 7);
            this.p1DownBtn.Name = "p1DownBtn";
            this.p1DownBtn.Size = new System.Drawing.Size(75, 23);
            this.p1DownBtn.TabIndex = 9;
            this.p1DownBtn.Text = "다운로드";
            this.p1DownBtn.UseVisualStyleBackColor = true;
            this.p1DownBtn.Click += new System.EventHandler(this.p1DownBtn_Click);
            // 
            // p1Label3
            // 
            this.p1Label3.AutoSize = true;
            this.p1Label3.Location = new System.Drawing.Point(10, 41);
            this.p1Label3.Name = "p1Label3";
            this.p1Label3.Size = new System.Drawing.Size(41, 12);
            this.p1Label3.TabIndex = 10;
            this.p1Label3.Text = "검색어";
            // 
            // fileLabel
            // 
            this.fileLabel.AutoSize = true;
            this.fileLabel.Location = new System.Drawing.Point(765, 14);
            this.fileLabel.Name = "fileLabel";
            this.fileLabel.Size = new System.Drawing.Size(105, 12);
            this.fileLabel.TabIndex = 10;
            this.fileLabel.Text = "다운받을 파일주소";
            // 
            // p1Label2
            // 
            this.p1Label2.AutoSize = true;
            this.p1Label2.Location = new System.Drawing.Point(915, 13);
            this.p1Label2.Name = "p1Label2";
            this.p1Label2.Size = new System.Drawing.Size(57, 12);
            this.p1Label2.TabIndex = 10;
            this.p1Label2.Text = "파일 이름";
            // 
            // proxyText
            // 
            this.proxyText.Location = new System.Drawing.Point(411, 12);
            this.proxyText.Name = "proxyText";
            this.proxyText.Size = new System.Drawing.Size(181, 21);
            this.proxyText.TabIndex = 8;
            // 
            // proxyLabel
            // 
            this.proxyLabel.AutoSize = true;
            this.proxyLabel.Location = new System.Drawing.Point(304, 17);
            this.proxyLabel.Name = "proxyLabel";
            this.proxyLabel.Size = new System.Drawing.Size(105, 12);
            this.proxyLabel.TabIndex = 10;
            this.proxyLabel.Text = "프록시 주소 : 포트";
            // 
            // proxyCheck
            // 
            this.proxyCheck.AutoSize = true;
            this.proxyCheck.Location = new System.Drawing.Point(652, 14);
            this.proxyCheck.Name = "proxyCheck";
            this.proxyCheck.Size = new System.Drawing.Size(88, 16);
            this.proxyCheck.TabIndex = 11;
            this.proxyCheck.Text = "프록시 사용";
            this.proxyCheck.UseVisualStyleBackColor = true;
            // 
            // p1PageNextBtn
            // 
            this.p1PageNextBtn.Location = new System.Drawing.Point(816, 7);
            this.p1PageNextBtn.Name = "p1PageNextBtn";
            this.p1PageNextBtn.Size = new System.Drawing.Size(75, 23);
            this.p1PageNextBtn.TabIndex = 12;
            this.p1PageNextBtn.Text = "다음";
            this.p1PageNextBtn.UseVisualStyleBackColor = true;
            this.p1PageNextBtn.Click += new System.EventHandler(this.p1PageNextBtn_Click);
            // 
            // proxyPortText
            // 
            this.proxyPortText.Location = new System.Drawing.Point(594, 12);
            this.proxyPortText.Name = "proxyPortText";
            this.proxyPortText.Size = new System.Drawing.Size(48, 21);
            this.proxyPortText.TabIndex = 14;
            this.proxyPortText.DoubleClick += new System.EventHandler(this.goText0);
            // 
            // p1PagePrevBtn
            // 
            this.p1PagePrevBtn.Location = new System.Drawing.Point(675, 7);
            this.p1PagePrevBtn.Name = "p1PagePrevBtn";
            this.p1PagePrevBtn.Size = new System.Drawing.Size(75, 23);
            this.p1PagePrevBtn.TabIndex = 15;
            this.p1PagePrevBtn.Text = "이전";
            this.p1PagePrevBtn.UseVisualStyleBackColor = true;
            this.p1PagePrevBtn.Click += new System.EventHandler(this.p1PagePrevBtn_Click);
            // 
            // p1PageText
            // 
            this.p1PageText.Location = new System.Drawing.Point(756, 8);
            this.p1PageText.Name = "p1PageText";
            this.p1PageText.Size = new System.Drawing.Size(54, 21);
            this.p1PageText.TabIndex = 16;
            // 
            // p1Label1
            // 
            this.p1Label1.AutoSize = true;
            this.p1Label1.Location = new System.Drawing.Point(8, 12);
            this.p1Label1.Name = "p1Label1";
            this.p1Label1.Size = new System.Drawing.Size(69, 12);
            this.p1Label1.TabIndex = 10;
            this.p1Label1.Text = "추가 검색어";
            // 
            // p1SearchAddText
            // 
            this.p1SearchAddText.Location = new System.Drawing.Point(83, 6);
            this.p1SearchAddText.Name = "p1SearchAddText";
            this.p1SearchAddText.Size = new System.Drawing.Size(100, 21);
            this.p1SearchAddText.TabIndex = 17;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(1067, 6);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 18;
            this.btnOpen.Text = "열기";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // p1DirPrevBtn
            // 
            this.p1DirPrevBtn.Location = new System.Drawing.Point(478, 5);
            this.p1DirPrevBtn.Name = "p1DirPrevBtn";
            this.p1DirPrevBtn.Size = new System.Drawing.Size(75, 23);
            this.p1DirPrevBtn.TabIndex = 19;
            this.p1DirPrevBtn.Text = "이전";
            this.p1DirPrevBtn.UseVisualStyleBackColor = true;
            this.p1DirPrevBtn.Click += new System.EventHandler(this.p1DirPrevBtn_Click);
            // 
            // p1DirNextBtn
            // 
            this.p1DirNextBtn.Location = new System.Drawing.Point(559, 5);
            this.p1DirNextBtn.Name = "p1DirNextBtn";
            this.p1DirNextBtn.Size = new System.Drawing.Size(75, 23);
            this.p1DirNextBtn.TabIndex = 19;
            this.p1DirNextBtn.Text = "다음";
            this.p1DirNextBtn.UseVisualStyleBackColor = true;
            this.p1DirNextBtn.Click += new System.EventHandler(this.p1DirNextBtn_Click);
            // 
            // p1DirText
            // 
            this.p1DirText.Location = new System.Drawing.Point(412, 7);
            this.p1DirText.Name = "p1DirText";
            this.p1DirText.Size = new System.Drawing.Size(60, 21);
            this.p1DirText.TabIndex = 20;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 33);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1664, 875);
            this.tabControl1.TabIndex = 23;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.p1SearchAddText);
            this.tabPage1.Controls.Add(this.p1Label1);
            this.tabPage1.Controls.Add(this.p1DownBtn);
            this.tabPage1.Controls.Add(this.p1NextBtn);
            this.tabPage1.Controls.Add(this.p1StartBtn);
            this.tabPage1.Controls.Add(this.p1SearchText);
            this.tabPage1.Controls.Add(this.p1DirText);
            this.tabPage1.Controls.Add(this.p1DirNextBtn);
            this.tabPage1.Controls.Add(this.p1NameText);
            this.tabPage1.Controls.Add(this.p1DirPrevBtn);
            this.tabPage1.Controls.Add(this.p1Label3);
            this.tabPage1.Controls.Add(this.p1PageText);
            this.tabPage1.Controls.Add(this.p1PagePrevBtn);
            this.tabPage1.Controls.Add(this.p1Label2);
            this.tabPage1.Controls.Add(this.p1PageNextBtn);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1656, 849);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "한개폴더대상";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.p2Label2);
            this.tabPage2.Controls.Add(this.p2SearchText);
            this.tabPage2.Controls.Add(this.p2Check1);
            this.tabPage2.Controls.Add(this.p2StartBtn);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1656, 849);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "전체폴더대상";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // p2Label2
            // 
            this.p2Label2.AutoSize = true;
            this.p2Label2.Location = new System.Drawing.Point(175, 11);
            this.p2Label2.Name = "p2Label2";
            this.p2Label2.Size = new System.Drawing.Size(65, 12);
            this.p2Label2.TabIndex = 25;
            this.p2Label2.Text = "추가검색어";
            // 
            // p2SearchText
            // 
            this.p2SearchText.Location = new System.Drawing.Point(250, 8);
            this.p2SearchText.Name = "p2SearchText";
            this.p2SearchText.Size = new System.Drawing.Size(181, 21);
            this.p2SearchText.TabIndex = 24;
            // 
            // p2Check1
            // 
            this.p2Check1.AutoSize = true;
            this.p2Check1.Location = new System.Drawing.Point(437, 10);
            this.p2Check1.Name = "p2Check1";
            this.p2Check1.Size = new System.Drawing.Size(66, 16);
            this.p2Check1.TabIndex = 23;
            this.p2Check1.Text = "( ) 빼기";
            this.p2Check1.UseVisualStyleBackColor = true;
            // 
            // p2StartBtn
            // 
            this.p2StartBtn.Location = new System.Drawing.Point(6, 6);
            this.p2StartBtn.Name = "p2StartBtn";
            this.p2StartBtn.Size = new System.Drawing.Size(75, 23);
            this.p2StartBtn.TabIndex = 22;
            this.p2StartBtn.Text = "모든폴더";
            this.p2StartBtn.UseVisualStyleBackColor = true;
            this.p2StartBtn.Click += new System.EventHandler(this.p2StartBtn_Click_1);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.webBrowser2);
            this.tabPage3.Controls.Add(this.p3Text);
            this.tabPage3.Controls.Add(this.p3StartBtn);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1656, 849);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "로그인";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // webBrowser2
            // 
            this.webBrowser2.Location = new System.Drawing.Point(3, 27);
            this.webBrowser2.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser2.Name = "webBrowser2";
            this.webBrowser2.Size = new System.Drawing.Size(1462, 519);
            this.webBrowser2.TabIndex = 26;
            // 
            // p3Text
            // 
            this.p3Text.Location = new System.Drawing.Point(81, 1);
            this.p3Text.Name = "p3Text";
            this.p3Text.Size = new System.Drawing.Size(181, 21);
            this.p3Text.TabIndex = 25;
            // 
            // p3StartBtn
            // 
            this.p3StartBtn.Location = new System.Drawing.Point(264, 0);
            this.p3StartBtn.Name = "p3StartBtn";
            this.p3StartBtn.Size = new System.Drawing.Size(75, 23);
            this.p3StartBtn.TabIndex = 0;
            this.p3StartBtn.Text = "검색";
            this.p3StartBtn.UseVisualStyleBackColor = true;
            this.p3StartBtn.Click += new System.EventHandler(this.p3StartBtn_Click);
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(1148, 8);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(41, 21);
            this.textBox9.TabIndex = 24;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1674, 913);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.proxyText);
            this.Controls.Add(this.proxyCheck);
            this.Controls.Add(this.proxyPortText);
            this.Controls.Add(this.fileSrcText);
            this.Controls.Add(this.proxyLabel);
            this.Controls.Add(this.fileLabel);
            this.Controls.Add(this.btnOpen);
            this.Name = "Form1";
            this.Text = "ImageSearchDownloader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button p1NextBtn;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.TextBox p1SearchText;
        private System.Windows.Forms.Button p1StartBtn;
        private System.Windows.Forms.TextBox fileSrcText;
        private System.Windows.Forms.TextBox p1NameText;
        private System.Windows.Forms.Button p1DownBtn;
        private System.Windows.Forms.Label p1Label3;
        private System.Windows.Forms.Label fileLabel;
        private System.Windows.Forms.Label p1Label2;
        private System.Windows.Forms.TextBox proxyText;
        private System.Windows.Forms.Label proxyLabel;
        private System.Windows.Forms.CheckBox proxyCheck;
        private System.Windows.Forms.Button p1PageNextBtn;
        private System.Windows.Forms.TextBox proxyPortText;
        private System.Windows.Forms.Button p1PagePrevBtn;
        private System.Windows.Forms.TextBox p1PageText;
        private System.Windows.Forms.Label p1Label1;
        private System.Windows.Forms.TextBox p1SearchAddText;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button p1DirPrevBtn;
        private System.Windows.Forms.Button p1DirNextBtn;
        private System.Windows.Forms.TextBox p1DirText;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button p2StartBtn;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.WebBrowser webBrowser2;
        private System.Windows.Forms.TextBox p3Text;
        private System.Windows.Forms.Button p3StartBtn;
        private System.Windows.Forms.Label p2Label2;
        private System.Windows.Forms.TextBox p2SearchText;
        private System.Windows.Forms.CheckBox p2Check1;
    }
}

