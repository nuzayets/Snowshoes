namespace Snowshoes.UI
{
    partial class DebugUI
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
            this.lstCoordinates = new System.Windows.Forms.ListBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtLevelArea = new System.Windows.Forms.TextBox();
            this.txtQuestID = new System.Windows.Forms.TextBox();
            this.txtQuestStep = new System.Windows.Forms.TextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // lstCoordinates
            // 
            this.lstCoordinates.FormattingEnabled = true;
            this.lstCoordinates.Location = new System.Drawing.Point(195, 12);
            this.lstCoordinates.Name = "lstCoordinates";
            this.lstCoordinates.Size = new System.Drawing.Size(146, 186);
            this.lstCoordinates.TabIndex = 0;
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(21, 12);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(155, 20);
            this.txtX.TabIndex = 1;
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(21, 38);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(155, 20);
            this.txtY.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(21, 149);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(64, 49);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAddClick);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(112, 149);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(64, 49);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSaveClick);
            // 
            // txtLevelArea
            // 
            this.txtLevelArea.Location = new System.Drawing.Point(21, 64);
            this.txtLevelArea.Name = "txtLevelArea";
            this.txtLevelArea.Size = new System.Drawing.Size(155, 20);
            this.txtLevelArea.TabIndex = 5;
            // 
            // txtQuestID
            // 
            this.txtQuestID.Location = new System.Drawing.Point(21, 90);
            this.txtQuestID.Name = "txtQuestID";
            this.txtQuestID.Size = new System.Drawing.Size(155, 20);
            this.txtQuestID.TabIndex = 6;
            // 
            // txtQuestStep
            // 
            this.txtQuestStep.Location = new System.Drawing.Point(21, 116);
            this.txtQuestStep.Name = "txtQuestStep";
            this.txtQuestStep.Size = new System.Drawing.Size(155, 20);
            this.txtQuestStep.TabIndex = 7;
            // 
            // DebugUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 210);
            this.Controls.Add(this.txtQuestStep);
            this.Controls.Add(this.txtQuestID);
            this.Controls.Add(this.txtLevelArea);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.lstCoordinates);
            this.Name = "DebugUI";
            this.Text = "DebugUI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListBox lstCoordinates;
        public System.Windows.Forms.TextBox txtX;
        public System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.TextBox txtLevelArea;
        public System.Windows.Forms.TextBox txtQuestID;
        public System.Windows.Forms.TextBox txtQuestStep;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;


    }
}