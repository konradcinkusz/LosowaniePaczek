using System.Windows.Forms;

namespace LosowaniePaczek
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.plikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zamknijToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pomocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionStringlbl = new System.Windows.Forms.Label();
            this.mainRangelbl = new System.Windows.Forms.Label();
            this.subRangelbl = new System.Windows.Forms.Label();
            this.countNumberToDrawlbl = new System.Windows.Forms.Label();
            this.rangeFromlbl = new System.Windows.Forms.Label();
            this.rangeTolbl = new System.Windows.Forms.Label();
            this.subRangeTolbl = new System.Windows.Forms.Label();
            this.connectionStringtb = new System.Windows.Forms.TextBox();
            this.drawbtn = new System.Windows.Forms.Button();
            this.numberOfDrawlbl = new System.Windows.Forms.Label();
            this.totalTimeCountlbl = new System.Windows.Forms.Label();
            this.subRangeFromlbl = new System.Windows.Forms.Label();
            this.rangeFromup = new System.Windows.Forms.NumericUpDown();
            this.rangeToup = new System.Windows.Forms.NumericUpDown();
            this.subRangeFromup = new System.Windows.Forms.NumericUpDown();
            this.subRangeToup = new System.Windows.Forms.NumericUpDown();
            this.countNumberToDrawup = new System.Windows.Forms.NumericUpDown();
            this.numberOfDrawlbl2 = new System.Windows.Forms.Label();
            this.totalCountTimelbl2 = new System.Windows.Forms.Label();
            this.generateDBbtn = new System.Windows.Forms.Button();
            this.clearDBbtn = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timeProgressBar = new System.Windows.Forms.ProgressBar();
            this.progressBarlbl = new System.Windows.Forms.Label();
            this.progresBarActionName = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rangeFromup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rangeToup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subRangeFromup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subRangeToup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countNumberToDrawup)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plikToolStripMenuItem,
            this.pomocToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(442, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // plikToolStripMenuItem
            // 
            this.plikToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zamknijToolStripMenuItem});
            this.plikToolStripMenuItem.Name = "plikToolStripMenuItem";
            this.plikToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.plikToolStripMenuItem.Text = "Plik";
            // 
            // zamknijToolStripMenuItem
            // 
            this.zamknijToolStripMenuItem.Name = "zamknijToolStripMenuItem";
            this.zamknijToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.zamknijToolStripMenuItem.Text = "Zamknij";
            this.zamknijToolStripMenuItem.Click += new System.EventHandler(this.zamknijToolStripMenuItem_Click);
            // 
            // pomocToolStripMenuItem
            // 
            this.pomocToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoToolStripMenuItem});
            this.pomocToolStripMenuItem.Name = "pomocToolStripMenuItem";
            this.pomocToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.pomocToolStripMenuItem.Text = "Pomoc";
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.infoToolStripMenuItem.Text = "Info";
            // 
            // connectionStringlbl
            // 
            this.connectionStringlbl.AutoSize = true;
            this.connectionStringlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectionStringlbl.Location = new System.Drawing.Point(9, 44);
            this.connectionStringlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.connectionStringlbl.Name = "connectionStringlbl";
            this.connectionStringlbl.Size = new System.Drawing.Size(137, 20);
            this.connectionStringlbl.TabIndex = 1;
            this.connectionStringlbl.Text = "Connection string:";
            this.connectionStringlbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // mainRangelbl
            // 
            this.mainRangelbl.AutoSize = true;
            this.mainRangelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainRangelbl.Location = new System.Drawing.Point(11, 140);
            this.mainRangelbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.mainRangelbl.Name = "mainRangelbl";
            this.mainRangelbl.Size = new System.Drawing.Size(58, 20);
            this.mainRangelbl.TabIndex = 2;
            this.mainRangelbl.Text = "Zakres";
            // 
            // subRangelbl
            // 
            this.subRangelbl.AutoSize = true;
            this.subRangelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subRangelbl.Location = new System.Drawing.Point(11, 179);
            this.subRangelbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.subRangelbl.Name = "subRangelbl";
            this.subRangelbl.Size = new System.Drawing.Size(84, 20);
            this.subRangelbl.TabIndex = 5;
            this.subRangelbl.Text = "Podzakres";
            // 
            // countNumberToDrawlbl
            // 
            this.countNumberToDrawlbl.AutoSize = true;
            this.countNumberToDrawlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countNumberToDrawlbl.Location = new System.Drawing.Point(11, 231);
            this.countNumberToDrawlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.countNumberToDrawlbl.Name = "countNumberToDrawlbl";
            this.countNumberToDrawlbl.Size = new System.Drawing.Size(155, 20);
            this.countNumberToDrawlbl.TabIndex = 6;
            this.countNumberToDrawlbl.Text = "Wylosuj (podaj ilość):";
            // 
            // rangeFromlbl
            // 
            this.rangeFromlbl.AutoSize = true;
            this.rangeFromlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rangeFromlbl.Location = new System.Drawing.Point(134, 140);
            this.rangeFromlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.rangeFromlbl.Name = "rangeFromlbl";
            this.rangeFromlbl.Size = new System.Drawing.Size(27, 20);
            this.rangeFromlbl.TabIndex = 8;
            this.rangeFromlbl.Text = "od";
            // 
            // rangeTolbl
            // 
            this.rangeTolbl.AutoSize = true;
            this.rangeTolbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rangeTolbl.Location = new System.Drawing.Point(305, 140);
            this.rangeTolbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.rangeTolbl.Name = "rangeTolbl";
            this.rangeTolbl.Size = new System.Drawing.Size(27, 20);
            this.rangeTolbl.TabIndex = 9;
            this.rangeTolbl.Text = "do";
            // 
            // subRangeTolbl
            // 
            this.subRangeTolbl.AutoSize = true;
            this.subRangeTolbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subRangeTolbl.Location = new System.Drawing.Point(305, 180);
            this.subRangeTolbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.subRangeTolbl.Name = "subRangeTolbl";
            this.subRangeTolbl.Size = new System.Drawing.Size(27, 20);
            this.subRangeTolbl.TabIndex = 10;
            this.subRangeTolbl.Text = "do";
            // 
            // connectionStringtb
            // 
            this.connectionStringtb.BackColor = System.Drawing.SystemColors.HighlightText;
            this.connectionStringtb.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectionStringtb.Location = new System.Drawing.Point(149, 47);
            this.connectionStringtb.Margin = new System.Windows.Forms.Padding(2);
            this.connectionStringtb.Name = "connectionStringtb";
            this.connectionStringtb.Size = new System.Drawing.Size(281, 23);
            this.connectionStringtb.TabIndex = 11;
            this.connectionStringtb.Text = "Integrated Security = SSPI; Initial Catalog = ParcelNumberGenerator; Data Source " +
    "= localhost\\\\SQLEXPRESS;";
            // 
            // drawbtn
            // 
            this.drawbtn.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.drawbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.drawbtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.drawbtn.FlatAppearance.BorderSize = 10;
            this.drawbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drawbtn.Location = new System.Drawing.Point(15, 264);
            this.drawbtn.Margin = new System.Windows.Forms.Padding(2);
            this.drawbtn.Name = "drawbtn";
            this.drawbtn.Size = new System.Drawing.Size(122, 54);
            this.drawbtn.TabIndex = 12;
            this.drawbtn.Text = "Losuj";
            this.drawbtn.UseVisualStyleBackColor = false;
            this.drawbtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // numberOfDrawlbl
            // 
            this.numberOfDrawlbl.AutoSize = true;
            this.numberOfDrawlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numberOfDrawlbl.Location = new System.Drawing.Point(141, 264);
            this.numberOfDrawlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.numberOfDrawlbl.Name = "numberOfDrawlbl";
            this.numberOfDrawlbl.Size = new System.Drawing.Size(212, 20);
            this.numberOfDrawlbl.TabIndex = 19;
            this.numberOfDrawlbl.Text = "Ilość wylosowanych wyników:";
            // 
            // totalTimeCountlbl
            // 
            this.totalTimeCountlbl.AutoSize = true;
            this.totalTimeCountlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalTimeCountlbl.Location = new System.Drawing.Point(141, 298);
            this.totalTimeCountlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.totalTimeCountlbl.Name = "totalTimeCountlbl";
            this.totalTimeCountlbl.Size = new System.Drawing.Size(191, 20);
            this.totalTimeCountlbl.TabIndex = 20;
            this.totalTimeCountlbl.Text = "Całkowity czas losowania:";
            // 
            // subRangeFromlbl
            // 
            this.subRangeFromlbl.AutoSize = true;
            this.subRangeFromlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subRangeFromlbl.Location = new System.Drawing.Point(134, 176);
            this.subRangeFromlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.subRangeFromlbl.Name = "subRangeFromlbl";
            this.subRangeFromlbl.Size = new System.Drawing.Size(27, 20);
            this.subRangeFromlbl.TabIndex = 31;
            this.subRangeFromlbl.Text = "od";
            // 
            // rangeFromup
            // 
            this.rangeFromup.BackColor = System.Drawing.SystemColors.HighlightText;
            this.rangeFromup.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.rangeFromup.Location = new System.Drawing.Point(165, 140);
            this.rangeFromup.Margin = new System.Windows.Forms.Padding(2);
            this.rangeFromup.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.rangeFromup.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.rangeFromup.Name = "rangeFromup";
            this.rangeFromup.Size = new System.Drawing.Size(90, 20);
            this.rangeFromup.TabIndex = 500;
            this.rangeFromup.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.rangeFromup.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            this.rangeFromup.Click += new System.EventHandler(this.numericUpDown1_Click);
            // 
            // rangeToup
            // 
            this.rangeToup.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.rangeToup.Enabled = false;
            this.rangeToup.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.rangeToup.Location = new System.Drawing.Point(340, 137);
            this.rangeToup.Margin = new System.Windows.Forms.Padding(2);
            this.rangeToup.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.rangeToup.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.rangeToup.Name = "rangeToup";
            this.rangeToup.Size = new System.Drawing.Size(90, 20);
            this.rangeToup.TabIndex = 100002;
            this.rangeToup.Value = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.rangeToup.BackColorChanged += new System.EventHandler(this.numericUpDown1_Click);
            this.rangeToup.MouseUp += new System.Windows.Forms.MouseEventHandler(this.numericUpDown2_MouseUp);
            // 
            // subRangeFromup
            // 
            this.subRangeFromup.BackColor = System.Drawing.SystemColors.HighlightText;
            this.subRangeFromup.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.subRangeFromup.Location = new System.Drawing.Point(165, 179);
            this.subRangeFromup.Margin = new System.Windows.Forms.Padding(2);
            this.subRangeFromup.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.subRangeFromup.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.subRangeFromup.Name = "subRangeFromup";
            this.subRangeFromup.Size = new System.Drawing.Size(90, 20);
            this.subRangeFromup.TabIndex = 100003;
            this.subRangeFromup.Value = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.subRangeFromup.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
            // 
            // subRangeToup
            // 
            this.subRangeToup.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.subRangeToup.Enabled = false;
            this.subRangeToup.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.subRangeToup.Location = new System.Drawing.Point(340, 183);
            this.subRangeToup.Margin = new System.Windows.Forms.Padding(2);
            this.subRangeToup.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.subRangeToup.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.subRangeToup.Name = "subRangeToup";
            this.subRangeToup.Size = new System.Drawing.Size(90, 20);
            this.subRangeToup.TabIndex = 100004;
            this.subRangeToup.Value = new decimal(new int[] {
            40000,
            0,
            0,
            0});
            // 
            // countNumberToDrawup
            // 
            this.countNumberToDrawup.BackColor = System.Drawing.SystemColors.HighlightText;
            this.countNumberToDrawup.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.countNumberToDrawup.Location = new System.Drawing.Point(229, 233);
            this.countNumberToDrawup.Margin = new System.Windows.Forms.Padding(2);
            this.countNumberToDrawup.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.countNumberToDrawup.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.countNumberToDrawup.Name = "countNumberToDrawup";
            this.countNumberToDrawup.Size = new System.Drawing.Size(90, 20);
            this.countNumberToDrawup.TabIndex = 100005;
            this.countNumberToDrawup.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // numberOfDrawlbl2
            // 
            this.numberOfDrawlbl2.AutoSize = true;
            this.numberOfDrawlbl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numberOfDrawlbl2.Location = new System.Drawing.Point(383, 264);
            this.numberOfDrawlbl2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.numberOfDrawlbl2.Name = "numberOfDrawlbl2";
            this.numberOfDrawlbl2.Size = new System.Drawing.Size(0, 20);
            this.numberOfDrawlbl2.TabIndex = 100007;
            // 
            // totalCountTimelbl2
            // 
            this.totalCountTimelbl2.AutoSize = true;
            this.totalCountTimelbl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalCountTimelbl2.Location = new System.Drawing.Point(383, 299);
            this.totalCountTimelbl2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.totalCountTimelbl2.Name = "totalCountTimelbl2";
            this.totalCountTimelbl2.Size = new System.Drawing.Size(0, 20);
            this.totalCountTimelbl2.TabIndex = 100008;
            // 
            // generateDBbtn
            // 
            this.generateDBbtn.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.generateDBbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.generateDBbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generateDBbtn.Location = new System.Drawing.Point(149, 74);
            this.generateDBbtn.Margin = new System.Windows.Forms.Padding(2);
            this.generateDBbtn.Name = "generateDBbtn";
            this.generateDBbtn.Size = new System.Drawing.Size(88, 41);
            this.generateDBbtn.TabIndex = 100009;
            this.generateDBbtn.Text = "Generate DB";
            this.generateDBbtn.UseVisualStyleBackColor = false;
            // 
            // clearDBbtn
            // 
            this.clearDBbtn.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.clearDBbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clearDBbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearDBbtn.Location = new System.Drawing.Point(348, 74);
            this.clearDBbtn.Margin = new System.Windows.Forms.Padding(2);
            this.clearDBbtn.Name = "clearDBbtn";
            this.clearDBbtn.Size = new System.Drawing.Size(82, 41);
            this.clearDBbtn.TabIndex = 100010;
            this.clearDBbtn.Text = "Clear DB";
            this.clearDBbtn.UseVisualStyleBackColor = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // timeProgressBar
            // 
            this.timeProgressBar.Location = new System.Drawing.Point(13, 323);
            this.timeProgressBar.Name = "timeProgressBar";
            this.timeProgressBar.Size = new System.Drawing.Size(417, 41);
            this.timeProgressBar.TabIndex = 100011;
            // 
            // progressBarlbl
            // 
            this.progressBarlbl.AutoSize = true;
            this.progressBarlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progressBarlbl.Location = new System.Drawing.Point(44, 333);
            this.progressBarlbl.Name = "progressBarlbl";
            this.progressBarlbl.Size = new System.Drawing.Size(0, 20);
            this.progressBarlbl.TabIndex = 100012;
            // 
            // progresBarActionName
            // 
            this.progresBarActionName.AutoSize = true;
            this.progresBarActionName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progresBarActionName.Location = new System.Drawing.Point(125, 333);
            this.progresBarActionName.Name = "progresBarActionName";
            this.progresBarActionName.Size = new System.Drawing.Size(0, 20);
            this.progresBarActionName.TabIndex = 100013;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(442, 371);
            this.Controls.Add(this.progresBarActionName);
            this.Controls.Add(this.progressBarlbl);
            this.Controls.Add(this.timeProgressBar);
            this.Controls.Add(this.clearDBbtn);
            this.Controls.Add(this.generateDBbtn);
            this.Controls.Add(this.totalCountTimelbl2);
            this.Controls.Add(this.numberOfDrawlbl2);
            this.Controls.Add(this.countNumberToDrawup);
            this.Controls.Add(this.subRangeToup);
            this.Controls.Add(this.subRangeFromup);
            this.Controls.Add(this.rangeToup);
            this.Controls.Add(this.rangeFromup);
            this.Controls.Add(this.subRangeFromlbl);
            this.Controls.Add(this.totalTimeCountlbl);
            this.Controls.Add(this.numberOfDrawlbl);
            this.Controls.Add(this.drawbtn);
            this.Controls.Add(this.connectionStringtb);
            this.Controls.Add(this.subRangeTolbl);
            this.Controls.Add(this.rangeTolbl);
            this.Controls.Add(this.rangeFromlbl);
            this.Controls.Add(this.countNumberToDrawlbl);
            this.Controls.Add(this.subRangelbl);
            this.Controls.Add(this.mainRangelbl);
            this.Controls.Add(this.connectionStringlbl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Generator losowania paczek";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rangeFromup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rangeToup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subRangeFromup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subRangeToup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countNumberToDrawup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem plikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zamknijToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pomocToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.Label connectionStringlbl;
        private System.Windows.Forms.Label mainRangelbl;
        private System.Windows.Forms.Label subRangelbl;
        private System.Windows.Forms.Label countNumberToDrawlbl;
        private System.Windows.Forms.Label rangeFromlbl;
        private System.Windows.Forms.Label rangeTolbl;
        private System.Windows.Forms.Label subRangeTolbl;
        private System.Windows.Forms.TextBox connectionStringtb;
        private System.Windows.Forms.Button drawbtn;
        private System.Windows.Forms.Label numberOfDrawlbl;
        private System.Windows.Forms.Label totalTimeCountlbl;
        private System.Windows.Forms.Label subRangeFromlbl;
        private System.Windows.Forms.NumericUpDown rangeFromup;
        private System.Windows.Forms.NumericUpDown rangeToup;
        private System.Windows.Forms.NumericUpDown subRangeFromup;
        private System.Windows.Forms.NumericUpDown subRangeToup;
        private System.Windows.Forms.NumericUpDown countNumberToDrawup;
        private Label numberOfDrawlbl2;
        private Label totalCountTimelbl2;
        private Button generateDBbtn;
        private Button clearDBbtn;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private ProgressBar timeProgressBar;
        private Label progressBarlbl;
        private Label progresBarActionName;
    }
}

