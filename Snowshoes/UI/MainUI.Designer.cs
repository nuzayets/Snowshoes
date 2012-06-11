namespace Snowshoes.UI
{
    partial class MainUI
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label7;
            this.outputBox = new System.Windows.Forms.TextBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.pause = new System.Windows.Forms.Button();
            this.stop = new System.Windows.Forms.Button();
            this.start = new System.Windows.Forms.Button();
            this.lblGold = new System.Windows.Forms.Label();
            this.lblDeltaGold = new System.Windows.Forms.Label();
            this.lblGph = new System.Windows.Forms.Label();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dumpAllUnitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(13, 13);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(40, 13);
            label1.TabIndex = 1;
            label1.Text = "Status:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(13, 30);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(32, 13);
            label2.TabIndex = 6;
            label2.Text = "Gold:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(104, 30);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(38, 13);
            label5.TabIndex = 8;
            label5.Text = "+Gold:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(203, 30);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(52, 13);
            label7.TabIndex = 10;
            label7.Text = "+Gold/hr:";
            // 
            // outputBox
            // 
            this.outputBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.outputBox.Location = new System.Drawing.Point(12, 55);
            this.outputBox.Multiline = true;
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputBox.Size = new System.Drawing.Size(486, 215);
            this.outputBox.TabIndex = 0;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(50, 13);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(58, 13);
            this.statusLabel.TabIndex = 2;
            this.statusLabel.Text = "StatusText";
            // 
            // pause
            // 
            this.pause.Enabled = false;
            this.pause.FlatAppearance.BorderSize = 0;
            this.pause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pause.Image = global::Snowshoes.Properties.Resources.pause;
            this.pause.Location = new System.Drawing.Point(428, 8);
            this.pause.Name = "pause";
            this.pause.Size = new System.Drawing.Size(32, 32);
            this.pause.TabIndex = 4;
            this.pause.UseVisualStyleBackColor = true;
            this.pause.Click += new System.EventHandler(this.PauseClick);
            // 
            // stop
            // 
            this.stop.Enabled = false;
            this.stop.FlatAppearance.BorderSize = 0;
            this.stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stop.Image = global::Snowshoes.Properties.Resources.stop_2;
            this.stop.Location = new System.Drawing.Point(466, 8);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(32, 32);
            this.stop.TabIndex = 5;
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.StopClick);
            // 
            // start
            // 
            this.start.FlatAppearance.BorderSize = 0;
            this.start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.start.Image = global::Snowshoes.Properties.Resources.play;
            this.start.Location = new System.Drawing.Point(390, 8);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(32, 32);
            this.start.TabIndex = 3;
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.StartClick);
            // 
            // lblGold
            // 
            this.lblGold.AutoSize = true;
            this.lblGold.Location = new System.Drawing.Point(41, 30);
            this.lblGold.Name = "lblGold";
            this.lblGold.Size = new System.Drawing.Size(27, 13);
            this.lblGold.TabIndex = 7;
            this.lblGold.Text = "N/A";
            // 
            // lblDeltaGold
            // 
            this.lblDeltaGold.AutoSize = true;
            this.lblDeltaGold.Location = new System.Drawing.Point(138, 30);
            this.lblDeltaGold.Name = "lblDeltaGold";
            this.lblDeltaGold.Size = new System.Drawing.Size(27, 13);
            this.lblDeltaGold.TabIndex = 9;
            this.lblDeltaGold.Text = "N/A";
            // 
            // lblGph
            // 
            this.lblGph.AutoSize = true;
            this.lblGph.Location = new System.Drawing.Point(250, 30);
            this.lblGph.Name = "lblGph";
            this.lblGph.Size = new System.Drawing.Size(27, 13);
            this.lblGph.TabIndex = 11;
            this.lblGph.Text = "N/A";
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dumpAllUnitsToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(153, 48);
            // 
            // dumpAllUnitsToolStripMenuItem
            // 
            this.dumpAllUnitsToolStripMenuItem.Name = "dumpAllUnitsToolStripMenuItem";
            this.dumpAllUnitsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dumpAllUnitsToolStripMenuItem.Text = "Dump all units";
            this.dumpAllUnitsToolStripMenuItem.Click += new System.EventHandler(this.DumpAllUnitsToolStripMenuItemClick);
            // 
            // MainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 282);
            this.Controls.Add(this.lblGph);
            this.Controls.Add(label7);
            this.Controls.Add(this.lblDeltaGold);
            this.Controls.Add(label5);
            this.Controls.Add(this.lblGold);
            this.Controls.Add(label2);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.pause);
            this.Controls.Add(this.start);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(label1);
            this.Controls.Add(this.outputBox);
            this.MaximizeBox = false;
            this.Name = "MainUI";
            this.Text = "Snowshoes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainUIFormClosing);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox outputBox;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button pause;
        private System.Windows.Forms.Button stop;
        private System.Windows.Forms.Label lblGold;
        private System.Windows.Forms.Label lblDeltaGold;
        private System.Windows.Forms.Label lblGph;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem dumpAllUnitsToolStripMenuItem;
    }
}