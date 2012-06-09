using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Snowshoes.UI
{
    public partial class MainUI : Form
    {
        
        public MainUI()
        {
            InitializeComponent();
            Snowshoes.StatusChanged += new StatusChangedHandler(Snowshoes_StatusChanged);

        }



        private void Snowshoes_StatusChanged(Status s)
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
            this.Invoke(
                new Action(() => 
                {
                    this.outputBox.Text += text + Environment.NewLine;
                    this.outputBox.SelectionStart = this.outputBox.Text.Length;
                    this.outputBox.ScrollToCaret();
                }
            ));
        }

        public void setGold(String gold, String delta_gold, String gph)
        {
            this.Invoke(
                new Action(() =>
                {
                    lblGold.Text = gold;
                    lblDeltaGold.Text = delta_gold;
                    lblGph.Text = gph;
                }
            ));
        }

        private void start_Click(object sender, EventArgs e)
        {
            Snowshoes.Start();
        }

        private void pause_Click(object sender, EventArgs e)
        {
            Snowshoes.Pause();
        }

        private void stop_Click(object sender, EventArgs e)
        {
            Snowshoes.Stop();
        }

        private void MainUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
        }

    }
}
