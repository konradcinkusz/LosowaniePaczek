using LosowaniePaczek.Resources;
using ParcelNumberGenerator;
using ParcelNumberGenerator.OthersImplementations;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace LosowaniePaczek
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void numericUpDown2_MouseUp(object sender, EventArgs e)
        {
            if (rangeToup.Value < rangeFromup.Value)
            {
                MessageBox.Show(Labels.IncorrectValue);
            }
        }
        private void numericUpDown1_Click(object sender, EventArgs e)
        {
            rangeToup.Value = rangeFromup.Value;
        }
        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            subRangeToup.Value = subRangeFromup.Value;
            subRangeToup.BackColor = Color.White;
            subRangeToup.Enabled = true;
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            rangeToup.Value = rangeFromup.Value;
            rangeToup.BackColor = Color.White;
            rangeToup.Enabled = true;
        }
        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Labels.AppShutDown);
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = connectionStringtb.Text.Replace(@"\\", @"\");
            Tuple<int, int> range = new Tuple<int, int>((int)rangeFromup.Value, (int)rangeToup.Value);
            Tuple<int, int> rangeOff = new Tuple<int, int>((int)subRangeFromup.Value, (int)subRangeToup.Value);

            INumberPoolGenerator pool = rangeOff.Item1 != range.Item1 ?
                (INumberPoolGenerator)(new NumberPoolDBv2WithRangeOff(rangeOff, range, connectionString)) :
            new NumberPoolDBv2WithUBS(range, connectionString) { Mode = Mode.Recursive };

            progresBarActionName.Text = "Losuje numery";
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.RunWorkerAsync(pool);
        }
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            INumberPoolGenerator pool = e.Argument as INumberPoolGenerator;
            if (pool != null)
            {
                Stopwatch sw = new Stopwatch();
                Stopwatch swAll = new Stopwatch();
                swAll.Start();
                for (int i = 0; i < countNumberToDrawup.Value; i++)
                {
                    pool.Generate();
                    backgroundWorker1.ReportProgress((int)((float)i / (float)countNumberToDrawup.Value * 100));
                }
                swAll.Stop();
                backgroundWorker1.ReportProgress(100);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

        }
        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            timeProgressBar.Value = e.ProgressPercentage;
            progressBarlbl.Text = e.ProgressPercentage.ToString();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
