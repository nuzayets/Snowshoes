#region

using System;
using System.Windows.Forms;

#endregion

namespace Snowshoes.UI
{
    public partial class MainUI : Form
    {
        public MainUI()
        {
            InitializeComponent();
            Snowshoes.StatusChanged += SnowshoesStatusChanged;
        }


        private void SnowshoesStatusChanged(Status s)
        {
            switch (s)
            {
                case Status.Running:
                    statusLabel.Text = "Running";
                    start.Enabled = false;
                    pause.Enabled = true;
                    stop.Enabled = true;
                    break;
                case Status.Paused:
                    statusLabel.Text = "Paused";
                    start.Enabled = true;
                    pause.Enabled = false;
                    stop.Enabled = true;
                    break;
                case Status.Stopped:
                    statusLabel.Text = "Stopped";
                    start.Enabled = true;
                    pause.Enabled = false;
                    stop.Enabled = false;
                    break;
            }
        }

        public void PrintLine(String text)
        {
            Invoke(
                new Action(() =>
                               {
                                   outputBox.Text += text + Environment.NewLine;
                                   outputBox.SelectionStart = outputBox.Text.Length;
                                   outputBox.ScrollToCaret();
                               }
                    ));
        }

        public void SetGold(String gold, String deltaGold, String gph)
        {
            Invoke(
                new Action(() =>
                               {
                                   lblGold.Text = gold;
                                   lblDeltaGold.Text = deltaGold;
                                   lblGph.Text = gph;
                               }
                    ));
        }

        private void StartClick(object sender, EventArgs e)
        {
            Snowshoes.Start();
        }

        private void PauseClick(object sender, EventArgs e)
        {
            Snowshoes.Pause();
        }

        private void StopClick(object sender, EventArgs e)
        {
            Snowshoes.Stop();
        }

        private void MainUIFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            WindowState = FormWindowState.Minimized;
        }
    }
}