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
            this.flatTextBox1 = new FlatUI.FlatTextBox();
            this.flatButton1 = new FlatUI.FlatButton();
            this.flatLabel3 = new FlatUI.FlatLabel();
            this.flatComboBox2 = new FlatUI.FlatComboBox();
            this.GroupBox1 = new FlatUI.FlatGroupBox();
            this.flatMini1 = new FlatUI.FlatMini();
            this.flatMax1 = new FlatUI.FlatMax();
            this.flatClose1 = new FlatUI.FlatClose();
            this.DownloadDataButton = new FlatUI.FlatButton();
            this.TabsConsole = new FlatUI.FlatTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ResetBtn = new FlatUI.FlatButton();
            this.UnhollowerLogTraceTgl = new FlatUI.FlatRadioButton();
            this.HollowerPassAllNamesTlg = new FlatUI.FlatRadioButton();
            this.DebugTog = new FlatUI.FlatRadioButton();
            this.ConsoleTab = new System.Windows.Forms.TabPage();
            this.flatLabel1 = new FlatUI.FlatLabel();
            this.DebugTab = new System.Windows.Forms.TabPage();
            this.flatLabel2 = new FlatUI.FlatLabel();
            this.formSkin1.SuspendLayout();
            this.TabsConsole.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.ConsoleTab.SuspendLayout();
            this.DebugTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // formSkin1
            // 
            this.formSkin1.BackColor = System.Drawing.Color.White;
            this.formSkin1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(1)))), ((int)(((byte)(77)))));
            this.formSkin1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(58)))), ((int)(((byte)(60)))));
            this.formSkin1.Controls.Add(this.flatTextBox1);
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
            this.formSkin1.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.formSkin1.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(1)))), ((int)(((byte)(67)))));
            this.formSkin1.HeaderMaximize = true;
            this.formSkin1.Location = new System.Drawing.Point(0, 0);
            this.formSkin1.Name = "formSkin1";
            this.formSkin1.Size = new System.Drawing.Size(730, 400);
            this.formSkin1.TabIndex = 0;
            this.formSkin1.Text = "WorldLoader";
            // 
            // flatTextBox1
            // 
            this.flatTextBox1.BackColor = System.Drawing.Color.Transparent;
            this.flatTextBox1.FocusOnHover = false;
            this.flatTextBox1.Location = new System.Drawing.Point(661, 360);
            this.flatTextBox1.MaxLength = 32767;
            this.flatTextBox1.Multiline = false;
            this.flatTextBox1.Name = "flatTextBox1";
            this.flatTextBox1.ReadOnly = false;
            this.flatTextBox1.Size = new System.Drawing.Size(57, 28);
            this.flatTextBox1.TabIndex = 25;
            this.flatTextBox1.Text = "Beta UI";
            this.flatTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.flatTextBox1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.flatTextBox1.UseSystemPasswordChar = false;
            // 
            // flatButton1
            // 
            this.flatButton1.BackColor = System.Drawing.Color.Transparent;
            this.flatButton1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(2)))), ((int)(((byte)(105)))));
            this.flatButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flatButton1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.flatButton1.Location = new System.Drawing.Point(410, 144);
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
            this.flatLabel3.Location = new System.Drawing.Point(399, 96);
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
            this.flatComboBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.flatComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.flatComboBox2.Font = new System.Drawing.Font("Tahoma", 8F);
            this.flatComboBox2.ForeColor = System.Drawing.Color.White;
            this.flatComboBox2.FormattingEnabled = true;
            this.flatComboBox2.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(0)))), ((int)(((byte)(45)))));
            this.flatComboBox2.ItemHeight = 16;
            this.flatComboBox2.Location = new System.Drawing.Point(402, 116);
            this.flatComboBox2.Name = "flatComboBox2";
            this.flatComboBox2.Size = new System.Drawing.Size(121, 22);
            this.flatComboBox2.TabIndex = 22;
            this.flatComboBox2.ValueMember = "1";
            this.flatComboBox2.SelectedIndexChanged += new System.EventHandler(this.UnloadModDropDown);
            // 
            // GroupBox1
            // 
            this.GroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.GroupBox1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(5)))), ((int)(((byte)(50)))));
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
            this.flatMini1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(1)))), ((int)(((byte)(67)))));
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
            this.flatMax1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(1)))), ((int)(((byte)(67)))));
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
            this.flatClose1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(1)))), ((int)(((byte)(67)))));
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
            this.DownloadDataButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(2)))), ((int)(((byte)(105)))));
            this.DownloadDataButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DownloadDataButton.Font = new System.Drawing.Font("Tahoma", 12F);
            this.DownloadDataButton.Location = new System.Drawing.Point(16, 368);
            this.DownloadDataButton.Name = "DownloadDataButton";
            this.DownloadDataButton.Rounded = true;
            this.DownloadDataButton.Size = new System.Drawing.Size(375, 29);
            this.DownloadDataButton.TabIndex = 2;
            this.DownloadDataButton.Text = "Download Logs";
            this.DownloadDataButton.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.DownloadDataButton.Click += new System.EventHandler(this.DownloadDataButton_Click);
            // 
            // TabsConsole
            // 
            this.TabsConsole.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(2)))), ((int)(((byte)(105)))));
            this.TabsConsole.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(22)))), ((int)(((byte)(43)))));
            this.TabsConsole.Controls.Add(this.tabPage1);
            this.TabsConsole.Controls.Add(this.ConsoleTab);
            this.TabsConsole.Controls.Add(this.DebugTab);
            this.TabsConsole.Font = new System.Drawing.Font("Tahoma", 10F);
            this.TabsConsole.ItemSize = new System.Drawing.Size(120, 40);
            this.TabsConsole.Location = new System.Drawing.Point(12, 43);
            this.TabsConsole.Name = "TabsConsole";
            this.TabsConsole.SelectedIndex = 0;
            this.TabsConsole.Size = new System.Drawing.Size(379, 319);
            this.TabsConsole.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabsConsole.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(30)))), ((int)(((byte)(59)))));
            this.tabPage1.Controls.Add(this.ResetBtn);
            this.tabPage1.Controls.Add(this.UnhollowerLogTraceTgl);
            this.tabPage1.Controls.Add(this.HollowerPassAllNamesTlg);
            this.tabPage1.Controls.Add(this.DebugTog);
            this.tabPage1.Location = new System.Drawing.Point(4, 44);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(371, 271);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Config";
            // 
            // ResetBtn
            // 
            this.ResetBtn.BackColor = System.Drawing.Color.Transparent;
            this.ResetBtn.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(2)))), ((int)(((byte)(105)))));
            this.ResetBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ResetBtn.Font = new System.Drawing.Font("Tahoma", 12F);
            this.ResetBtn.Location = new System.Drawing.Point(130, 236);
            this.ResetBtn.Name = "ResetBtn";
            this.ResetBtn.Rounded = true;
            this.ResetBtn.Size = new System.Drawing.Size(106, 32);
            this.ResetBtn.TabIndex = 28;
            this.ResetBtn.Text = "Reset";
            this.ResetBtn.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.ResetBtn.Click += new System.EventHandler(this.ResetBtn_Click);
            // 
            // UnhollowerLogTraceTgl
            // 
            this.UnhollowerLogTraceTgl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(30)))), ((int)(((byte)(59)))));
            this.UnhollowerLogTraceTgl.Checked = false;
            this.UnhollowerLogTraceTgl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UnhollowerLogTraceTgl.Font = new System.Drawing.Font("Tahoma", 10F);
            this.UnhollowerLogTraceTgl.Location = new System.Drawing.Point(7, 66);
            this.UnhollowerLogTraceTgl.Name = "UnhollowerLogTraceTgl";
            this.UnhollowerLogTraceTgl.Options = FlatUI.FlatRadioButton._Options.Style1;
            this.UnhollowerLogTraceTgl.Size = new System.Drawing.Size(161, 22);
            this.UnhollowerLogTraceTgl.TabIndex = 27;
            this.UnhollowerLogTraceTgl.Text = "UnhollowerLogTrace";
            this.UnhollowerLogTraceTgl.CheckedChanged += new FlatUI.FlatRadioButton.CheckedChangedEventHandler(this.UnhollowerLogTraceTgl_CheckedChanged);
            // 
            // HollowerPassAllNamesTlg
            // 
            this.HollowerPassAllNamesTlg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(30)))), ((int)(((byte)(59)))));
            this.HollowerPassAllNamesTlg.Checked = false;
            this.HollowerPassAllNamesTlg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HollowerPassAllNamesTlg.Font = new System.Drawing.Font("Tahoma", 10F);
            this.HollowerPassAllNamesTlg.Location = new System.Drawing.Point(6, 37);
            this.HollowerPassAllNamesTlg.Name = "HollowerPassAllNamesTlg";
            this.HollowerPassAllNamesTlg.Options = FlatUI.FlatRadioButton._Options.Style1;
            this.HollowerPassAllNamesTlg.Size = new System.Drawing.Size(162, 22);
            this.HollowerPassAllNamesTlg.TabIndex = 26;
            this.HollowerPassAllNamesTlg.Text = "HollowerPassAllNames";
            this.HollowerPassAllNamesTlg.CheckedChanged += new FlatUI.FlatRadioButton.CheckedChangedEventHandler(this.HollowerPassAllNamesTlg_CheckedChanged);
            // 
            // DebugTog
            // 
            this.DebugTog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(30)))), ((int)(((byte)(59)))));
            this.DebugTog.Checked = false;
            this.DebugTog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DebugTog.Font = new System.Drawing.Font("Tahoma", 10F);
            this.DebugTog.Location = new System.Drawing.Point(6, 9);
            this.DebugTog.Name = "DebugTog";
            this.DebugTog.Options = FlatUI.FlatRadioButton._Options.Style1;
            this.DebugTog.Size = new System.Drawing.Size(100, 22);
            this.DebugTog.TabIndex = 25;
            this.DebugTog.Text = "Debug";
            this.DebugTog.CheckedChanged += new FlatUI.FlatRadioButton.CheckedChangedEventHandler(this.DebugTog_CheckedChanged);
            // 
            // ConsoleTab
            // 
            this.ConsoleTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(30)))), ((int)(((byte)(59)))));
            this.ConsoleTab.Controls.Add(this.flatLabel1);
            this.ConsoleTab.Location = new System.Drawing.Point(4, 44);
            this.ConsoleTab.Name = "ConsoleTab";
            this.ConsoleTab.Padding = new System.Windows.Forms.Padding(3);
            this.ConsoleTab.Size = new System.Drawing.Size(371, 271);
            this.ConsoleTab.TabIndex = 0;
            this.ConsoleTab.Text = "Console";
            this.ConsoleTab.Click += new System.EventHandler(this.ConsoleTab_Click);
            // 
            // flatLabel1
            // 
            this.flatLabel1.AutoEllipsis = true;
            this.flatLabel1.AutoSize = true;
            this.flatLabel1.BackColor = System.Drawing.Color.Transparent;
            this.flatLabel1.Font = new System.Drawing.Font("Bahnschrift", 8F);
            this.flatLabel1.ForeColor = System.Drawing.Color.White;
            this.flatLabel1.Location = new System.Drawing.Point(3, 0);
            this.flatLabel1.MaximumSize = new System.Drawing.Size(3700, 0);
            this.flatLabel1.Name = "flatLabel1";
            this.flatLabel1.Size = new System.Drawing.Size(0, 13);
            this.flatLabel1.TabIndex = 0;
            // 
            // DebugTab
            // 
            this.DebugTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(30)))), ((int)(((byte)(59)))));
            this.DebugTab.Controls.Add(this.flatLabel2);
            this.DebugTab.Location = new System.Drawing.Point(4, 44);
            this.DebugTab.Name = "DebugTab";
            this.DebugTab.Padding = new System.Windows.Forms.Padding(3);
            this.DebugTab.Size = new System.Drawing.Size(371, 271);
            this.DebugTab.TabIndex = 1;
            this.DebugTab.Text = "DebugTab";
            // 
            // flatLabel2
            // 
            this.flatLabel2.AutoSize = true;
            this.flatLabel2.BackColor = System.Drawing.Color.Transparent;
            this.flatLabel2.Font = new System.Drawing.Font("Bahnschrift", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flatLabel2.ForeColor = System.Drawing.Color.White;
            this.flatLabel2.Location = new System.Drawing.Point(6, 3);
            this.flatLabel2.Name = "flatLabel2";
            this.flatLabel2.Size = new System.Drawing.Size(0, 13);
            this.flatLabel2.TabIndex = 0;
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
            this.tabPage1.ResumeLayout(false);
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
        internal FlatLabel flatLabel1;
        internal FlatLabel flatLabel2;
        private FlatRadioButton DebugTog;
        private FlatRadioButton HollowerPassAllNamesTlg;
        private FlatRadioButton UnhollowerLogTraceTgl;
        private FlatButton ResetBtn;
        private FlatTextBox flatTextBox1;
    }
}

