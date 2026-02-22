namespace DBMS_FUTSAL_PROJECT
{
    partial class formAllPO
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
            this.dataGridViewAllPO = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllPO)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewAllPO
            // 
            this.dataGridViewAllPO.AllowUserToAddRows = false;
            this.dataGridViewAllPO.AllowUserToDeleteRows = false;
            this.dataGridViewAllPO.AllowUserToResizeColumns = false;
            this.dataGridViewAllPO.AllowUserToResizeRows = false;
            this.dataGridViewAllPO.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewAllPO.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewAllPO.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewAllPO.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridViewAllPO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAllPO.Location = new System.Drawing.Point(13, 13);
            this.dataGridViewAllPO.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewAllPO.Name = "dataGridViewAllPO";
            this.dataGridViewAllPO.ReadOnly = true;
            this.dataGridViewAllPO.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridViewAllPO.RowHeadersVisible = false;
            this.dataGridViewAllPO.RowHeadersWidth = 51;
            this.dataGridViewAllPO.Size = new System.Drawing.Size(1050, 469);
            this.dataGridViewAllPO.TabIndex = 4;
            this.dataGridViewAllPO.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAllPO_CellContentClick);
            // 
            // formAllPO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1076, 490);
            this.Controls.Add(this.dataGridViewAllPO);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formAllPO";
            this.Text = "formAllPO";
            this.Load += new System.EventHandler(this.formAllPO_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllPO)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewAllPO;
    }
}