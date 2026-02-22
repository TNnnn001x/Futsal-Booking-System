namespace DBMS_FUTSAL_PROJECT
{
    partial class formBooking
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
            this.dataGridViewScheduleBook = new System.Windows.Forms.DataGridView();
            this.comboBoxDate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewScheduleBook)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewScheduleBook
            // 
            this.dataGridViewScheduleBook.AllowUserToAddRows = false;
            this.dataGridViewScheduleBook.AllowUserToDeleteRows = false;
            this.dataGridViewScheduleBook.AllowUserToResizeColumns = false;
            this.dataGridViewScheduleBook.AllowUserToResizeRows = false;
            this.dataGridViewScheduleBook.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewScheduleBook.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewScheduleBook.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewScheduleBook.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewScheduleBook.Location = new System.Drawing.Point(77, 100);
            this.dataGridViewScheduleBook.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewScheduleBook.Name = "dataGridViewScheduleBook";
            this.dataGridViewScheduleBook.ReadOnly = true;
            this.dataGridViewScheduleBook.RowHeadersVisible = false;
            this.dataGridViewScheduleBook.RowHeadersWidth = 51;
            this.dataGridViewScheduleBook.RowTemplate.Height = 24;
            this.dataGridViewScheduleBook.Size = new System.Drawing.Size(977, 368);
            this.dataGridViewScheduleBook.TabIndex = 29;
            this.dataGridViewScheduleBook.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewScheduleBook_CellContentClick);
            // 
            // comboBoxDate
            // 
            this.comboBoxDate.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDate.FormattingEnabled = true;
            this.comboBoxDate.Location = new System.Drawing.Point(140, 54);
            this.comboBoxDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxDate.Name = "comboBoxDate";
            this.comboBoxDate.Size = new System.Drawing.Size(242, 25);
            this.comboBoxDate.TabIndex = 31;
            this.comboBoxDate.SelectedIndexChanged += new System.EventHandler(this.comboBoxDate_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(68, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 28);
            this.label2.TabIndex = 33;
            this.label2.Text = "Date";
            // 
            // formBooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1098, 497);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxDate);
            this.Controls.Add(this.dataGridViewScheduleBook);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "formBooking";
            this.Text = "formBooking";
            this.Load += new System.EventHandler(this.formBooking_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewScheduleBook)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewScheduleBook;
        private System.Windows.Forms.ComboBox comboBoxDate;
        private System.Windows.Forms.Label label2;
    }
}