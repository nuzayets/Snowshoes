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
            this.SuspendLayout();
            // 
            // lstCoordinates
            // 
            this.lstCoordinates.FormattingEnabled = true;
            this.lstCoordinates.Location = new System.Drawing.Point(195, 12);
            this.lstCoordinates.Name = "lstCoordinates";
            this.lstCoordinates.Size = new System.Drawing.Size(146, 121);
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
            this.txtY.Location = new System.Drawing.Point(21, 49);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(155, 20);
            this.txtY.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(21, 84);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(64, 49);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // DebugUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 156);
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


    }
}