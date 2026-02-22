namespace DBMS_FUTSAL_PROJECT
{
    partial class formAllPR
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
            this.listViewAllPR = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listViewAllPR
            // 
            this.listViewAllPR.HideSelection = false;
            this.listViewAllPR.Location = new System.Drawing.Point(13, 13);
            this.listViewAllPR.Margin = new System.Windows.Forms.Padding(4);
            this.listViewAllPR.Name = "listViewAllPR";
            this.listViewAllPR.Size = new System.Drawing.Size(996, 436);
            this.listViewAllPR.TabIndex = 2;
            this.listViewAllPR.UseCompatibleStateImageBehavior = false;
            this.listViewAllPR.Click += new System.EventHandler(this.listViewAllPR_Click);
            // 
            // formAllPR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1074, 458);
            this.Controls.Add(this.listViewAllPR);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formAllPR";
            this.Text = "formAllPR";
            this.Load += new System.EventHandler(this.formAllPR_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView listViewAllPR;
    }
}