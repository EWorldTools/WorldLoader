using FlatUI;

namespace WorldLoader
{
    partial class LoaderMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.formSkin1 = new FlatUI.FormSkin();
            this.flatButton5 = new FlatUI.FlatButton();
            this.flatButton4 = new FlatUI.FlatButton();
            this.flatButton3 = new FlatUI.FlatButton();
            this.flatButton2 = new FlatUI.FlatButton();
            this.flatButton1 = new FlatUI.FlatButton();
            this.flatLabel3 = new FlatUI.FlatLabel();
            this.flatComboBox2 = new FlatUI.FlatComboBox();
            this.GroupBox1 = new FlatUI.FlatGroupBox();
            this.flatMini1 = new FlatUI.FlatMini();
            this.flatMax1 = new FlatUI.FlatMax();
            this.flatClose1 = new FlatUI.FlatClose();
            this.DownloadDataButton = new FlatUI.FlatButton();
            this.TabsConsole = new FlatUI.FlatTabControl();
            this.ConsoleTab = new System.Windows.Forms.TabPage();
            this.flatLabel1 = new FlatUI.FlatLabel();
            this.DebugTab = new System.Windows.Forms.TabPage();
            this.flatLabel2 = new FlatUI.FlatLabel();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.formSkin1.SuspendLayout();
            this.TabsConsole.SuspendLayout();
            this.ConsoleTab.SuspendLayout();
            this.DebugTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // formSkin1
            // 
            this.formSkin1.BackColor = System.Drawing.Color.White;
            this.formSkin1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(30)))), ((int)(((byte)(59)))));
            this.formSkin1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(58)))), ((int)(((byte)(60)))));
            this.formSkin1.Controls.Add(this.flatButton5);
            this.formSkin1.Controls.Add(this.flatButton4);
            this.formSkin1.Controls.Add(this.flatButton3);
            this.formSkin1.Controls.Add(this.flatButton2);
            this.formSkin1.Controls.Add(this.flatButton1);
            this.formSkin1.Controls.Add(this.flatLabel3);
            this.formSkin1.Controls.Add(this.flatComboBox2);
            this.formSkin1.Controls.Add(this.GroupBox1);
            this.formSkin1.Controls.Add(this.flatMini1);
            this.formSkin1.Controls.Add(this.flatMax1);
            this.formSkin1.Controls.Add(this.flatClose1);
            this.formSkin1.Controls.Add(this.DownloadDataButton);
            this.formSkin1.Controls.Add(this.TabsConsole);
            this.formSkin1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formSkin1.FlatColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(96)))), ((int)(((byte)(253)))));
            this.formSkin1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.formSkin1.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(22)))), ((int)(((byte)(43)))));
            this.formSkin1.HeaderMaximize = true;
            this.formSkin1.Location = new System.Drawing.Point(0, 0);
            this.formSkin1.Name = "formSkin1";
            this.formSkin1.Size = new System.Drawing.Size(730, 400);
            this.formSkin1.TabIndex = 0;
            this.formSkin1.Text = "WorldLoader";
            // 
            // flatButton5
            // 
            this.flatButton5.BackColor = System.Drawing.Color.Transparent;
            this.flatButton5.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(96)))), ((int)(((byte)(253)))));
            this.flatButton5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flatButton5.Font = new System.Drawing.Font("Tahoma", 10F);
            this.flatButton5.Location = new System.Drawing.Point(465, 338);
            this.flatButton5.Name = "flatButton5";
            this.flatButton5.Rounded = true;
            this.flatButton5.Size = new System.Drawing.Size(113, 22);
            this.flatButton5.TabIndex = 28;
            this.flatButton5.Text = "Load Assembly";
            this.flatButton5.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.flatButton5.Click += new System.EventHandler(this.flatButton5_Click);
            // 
            // flatButton4
            // 
            this.flatButton4.BackColor = System.Drawing.Color.Transparent;
            this.flatButton4.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(96)))), ((int)(((byte)(253)))));
            this.flatButton4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flatButton4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.flatButton4.Location = new System.Drawing.Point(426, 148);
            this.flatButton4.Name = "flatButton4";
            this.flatButton4.Rounded = true;
            this.flatButton4.Size = new System.Drawing.Size(117, 39);
            this.flatButton4.TabIndex = 27;
            this.flatButton4.Text = "Run Cpp2IL";
            this.flatButton4.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.flatButton4.Click += new System.EventHandler(this.RunCpp2ILGen);
            // 
            // flatButton3
            // 
            this.flatButton3.BackColor = System.Drawing.Color.Transparent;
            this.flatButton3.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(96)))), ((int)(((byte)(253)))));
            this.flatButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flatButton3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.flatButton3.Location = new System.Drawing.Point(579, 103);
            this.flatButton3.Name = "flatButton3";
            this.flatButton3.Rounded = true;
            this.flatButton3.Size = new System.Drawing.Size(117, 39);
            this.flatButton3.TabIndex = 26;
            this.flatButton3.Text = "Run Unhollower";
            this.flatButton3.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.flatButton3.Click += new System.EventHandler(this.RunUnhollowerButton);
            // 
            // flatButton2
            // 
            this.flatButton2.BackColor = System.Drawing.Color.Transparent;
            this.flatButton2.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(96)))), ((int)(((byte)(253)))));
            this.flatButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flatButton2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.flatButton2.Location = new System.Drawing.Point(426, 103);
            this.flatButton2.Name = "flatButton2";
            this.flatButton2.Rounded = true;
            this.flatButton2.Size = new System.Drawing.Size(117, 39);
            this.flatButton2.TabIndex = 25;
            this.flatButton2.Text = "Regen Assemblys";
            this.flatButton2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.flatButton2.Click += new System.EventHandler(this.RegenAssemblysButton);
            // 
            // flatButton1
            // 
            this.flatButton1.BackColor = System.Drawing.Color.Transparent;
            this.flatButton1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(96)))), ((int)(((byte)(253)))));
            this.flatButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flatButton1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.flatButton1.Location = new System.Drawing.Point(465, 366);
            this.flatButton1.Name = "flatButton1";
            this.flatButton1.Rounded = true;
            this.flatButton1.Size = new System.Drawing.Size(113, 22);
            this.flatButton1.TabIndex = 24;
            this.flatButton1.Text = "LoadMod";
            this.flatButton1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.flatButton1.Click += new System.EventHandler(this.LoadModButton);
            // 
            // flatLabel3
            // 
            this.flatLabel3.AutoSize = true;
            this.flatLabel3.BackColor = System.Drawing.Color.Transparent;
            this.flatLabel3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.flatLabel3.ForeColor = System.Drawing.Color.White;
            this.flatLabel3.Location = new System.Drawing.Point(592, 345);
            this.flatLabel3.Name = "flatLabel3";
            this.flatLabel3.Size = new System.Drawing.Size(80, 17);
            this.flatLabel3.TabIndex = 23;
            this.flatLabel3.Text = "Unload Mod";
            // 
            // flatComboBox2
            // 
            this.flatComboBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(34)))), ((int)(((byte)(65)))));
            this.flatComboBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flatComboBox2.DisplayMember = "1";
            this.flatComboBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.flatComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.flatComboBox2.Font = new System.Drawing.Font("Tahoma", 8F);
            this.flatComboBox2.ForeColor = System.Drawing.Color.White;
            this.flatComboBox2.FormattingEnabled = true;
            this.flatComboBox2.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(96)))), ((int)(((byte)(253)))));
            this.flatComboBox2.ItemHeight = 16;
            this.flatComboBox2.Items.AddRange(new object[] {
            "Test",
            "Test"});
            this.flatComboBox2.Location = new System.Drawing.Point(595, 365);
            this.flatComboBox2.Name = "flatComboBox2";
            this.flatComboBox2.Size = new System.Drawing.Size(121, 22);
            this.flatComboBox2.TabIndex = 22;
            this.flatComboBox2.ValueMember = "1";
            this.flatComboBox2.SelectedIndexChanged += new System.EventHandler(this.UnloadModDropDown);
            // 
            // GroupBox1
            // 
            this.GroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.GroupBox1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(35)))), ((int)(((byte)(63)))));
            this.GroupBox1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.GroupBox1.Location = new System.Drawing.Point(402, 43);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.ShowText = true;
            this.GroupBox1.Size = new System.Drawing.Size(318, 54);
            this.GroupBox1.TabIndex = 13;
            this.GroupBox1.Text = "Made By: _1254";
            // 
            // flatMini1
            // 
            this.flatMini1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flatMini1.BackColor = System.Drawing.Color.White;
            this.flatMini1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(22)))), ((int)(((byte)(43)))));
            this.flatMini1.Font = new System.Drawing.Font("Marlett", 14F);
            this.flatMini1.Location = new System.Drawing.Point(654, 10);
            this.flatMini1.Name = "flatMini1";
            this.flatMini1.Size = new System.Drawing.Size(18, 18);
            this.flatMini1.TabIndex = 10;
            this.flatMini1.Text = "flatMini1";
            this.flatMini1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(203)))), ((int)(((byte)(31)))));
            // 
            // flatMax1
            // 
            this.flatMax1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flatMax1.BackColor = System.Drawing.Color.White;
            this.flatMax1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(22)))), ((int)(((byte)(43)))));
            this.flatMax1.Font = new System.Drawing.Font("Marlett", 14F);
            this.flatMax1.Location = new System.Drawing.Point(678, 10);
            this.flatMax1.Name = "flatMax1";
            this.flatMax1.Size = new System.Drawing.Size(18, 18);
            this.flatMax1.TabIndex = 9;
            this.flatMax1.Text = "flatMax1";
            this.flatMax1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(190)))), ((int)(((byte)(69)))));
            // 
            // flatClose1
            // 
            this.flatClose1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flatClose1.BackColor = System.Drawing.Color.White;
            this.flatClose1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(22)))), ((int)(((byte)(43)))));
            this.flatClose1.Font = new System.Drawing.Font("Marlett", 14F);
            this.flatClose1.Location = new System.Drawing.Point(702, 11);
            this.flatClose1.Name = "flatClose1";
            this.flatClose1.Size = new System.Drawing.Size(18, 18);
            this.flatClose1.TabIndex = 8;
            this.flatClose1.Text = "flatClose1";
            this.flatClose1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(95)))), ((int)(((byte)(98)))));
            // 
            // DownloadDataButton
            // 
            this.DownloadDataButton.BackColor = System.Drawing.Color.Transparent;
            this.DownloadDataButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(96)))), ((int)(((byte)(253)))));
            this.DownloadDataButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DownloadDataButton.Font = new System.Drawing.Font("Tahoma", 12F);
            this.DownloadDataButton.Location = new System.Drawing.Point(16, 186);
            this.DownloadDataButton.Name = "DownloadDataButton";
            this.DownloadDataButton.Rounded = true;
            this.DownloadDataButton.Size = new System.Drawing.Size(371, 32);
            this.DownloadDataButton.TabIndex = 2;
            this.DownloadDataButton.Text = "Download Logs";
            this.DownloadDataButton.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            // 
            // TabsConsole
            // 
            this.TabsConsole.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(96)))), ((int)(((byte)(253)))));
            this.TabsConsole.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(22)))), ((int)(((byte)(43)))));
            this.TabsConsole.Controls.Add(this.ConsoleTab);
            this.TabsConsole.Controls.Add(this.DebugTab);
            this.TabsConsole.Controls.Add(this.tabPage1);
            this.TabsConsole.Font = new System.Drawing.Font("Tahoma", 10F);
            this.TabsConsole.ItemSize = new System.Drawing.Size(120, 40);
            this.TabsConsole.Location = new System.Drawing.Point(12, 43);
            this.TabsConsole.Name = "TabsConsole";
            this.TabsConsole.SelectedIndex = 0;
            this.TabsConsole.Size = new System.Drawing.Size(379, 137);
            this.TabsConsole.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabsConsole.TabIndex = 1;
            // 
            // ConsoleTab
            // 
            this.ConsoleTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(30)))), ((int)(((byte)(59)))));
            this.ConsoleTab.Controls.Add(this.flatLabel1);
            this.ConsoleTab.Location = new System.Drawing.Point(4, 44);
            this.ConsoleTab.Name = "ConsoleTab";
            this.ConsoleTab.Padding = new System.Windows.Forms.Padding(3);
            this.ConsoleTab.Size = new System.Drawing.Size(371, 89);
            this.ConsoleTab.TabIndex = 0;
            this.ConsoleTab.Text = "Console";
            // 
            // flatLabel1
            // 
            this.flatLabel1.AutoEllipsis = true;
            this.flatLabel1.AutoSize = true;
            this.flatLabel1.BackColor = System.Drawing.Color.Transparent;
            this.flatLabel1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.flatLabel1.ForeColor = System.Drawing.Color.White;
            this.flatLabel1.Location = new System.Drawing.Point(3, 0);
            this.flatLabel1.MaximumSize = new System.Drawing.Size(370, 0);
            this.flatLabel1.Name = "flatLabel1";
            this.flatLabel1.Size = new System.Drawing.Size(0, 17);
            this.flatLabel1.TabIndex = 0;
            // 
            // DebugTab
            // 
            this.DebugTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(30)))), ((int)(((byte)(59)))));
            this.DebugTab.Controls.Add(this.flatLabel2);
            this.DebugTab.Location = new System.Drawing.Point(4, 44);
            this.DebugTab.Name = "DebugTab";
            this.DebugTab.Padding = new System.Windows.Forms.Padding(3);
            this.DebugTab.Size = new System.Drawing.Size(371, 89);
            this.DebugTab.TabIndex = 1;
            this.DebugTab.Text = "DebugTab";
            // 
            // flatLabel2
            // 
            this.flatLabel2.AutoSize = true;
            this.flatLabel2.BackColor = System.Drawing.Color.Transparent;
            this.flatLabel2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.flatLabel2.ForeColor = System.Drawing.Color.White;
            this.flatLabel2.Location = new System.Drawing.Point(6, 3);
            this.flatLabel2.Name = "flatLabel2";
            this.flatLabel2.Size = new System.Drawing.Size(78, 17);
            this.flatLabel2.TabIndex = 0;
            this.flatLabel2.Text = "PlaceHolder";
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(30)))), ((int)(((byte)(59)))));
            this.tabPage1.Location = new System.Drawing.Point(4, 44);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(371, 89);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Config";
            // 
            // LoaderMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 400);
            this.Controls.Add(this.formSkin1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoaderMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.formSkin1.ResumeLayout(false);
            this.formSkin1.PerformLayout();
            this.TabsConsole.ResumeLayout(false);
            this.ConsoleTab.ResumeLayout(false);
            this.ConsoleTab.PerformLayout();
            this.DebugTab.ResumeLayout(false);
            this.DebugTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FormSkin formSkin1;
        private FlatButton DownloadDataButton;
        private FlatMini flatMini1;
        private FlatMax flatMax1;
        private FlatClose flatClose1;
        private FlatGroupBox GroupBox1;
        private FlatTabControl TabsConsole;
        private System.Windows.Forms.TabPage ConsoleTab;
        private System.Windows.Forms.TabPage DebugTab;
        private System.Windows.Forms.TabPage tabPage1;
        private FlatButton flatButton1;
        private FlatLabel flatLabel3;
        internal FlatComboBox flatComboBox2;
        private FlatButton flatButton4;
        private FlatButton flatButton3;
        private FlatButton flatButton2;
        internal FlatLabel flatLabel1;
        internal FlatLabel flatLabel2;
        private FlatButton flatButton5;
    }
}

