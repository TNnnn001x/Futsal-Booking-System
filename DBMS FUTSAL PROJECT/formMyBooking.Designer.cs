namespace DBMS_FUTSAL_PROJECT
{
    partial class formMyBooking
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewBookingDetails = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBookingDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewBookingDetails
            // 
            this.dataGridViewBookingDetails.AllowUserToAddRows = false;
            this.dataGridViewBookingDetails.AllowUserToDeleteRows = false;
            this.dataGridViewBookingDetails.AllowUserToResizeColumns = false;
            this.dataGridViewBookingDetails.AllowUserToResizeRows = false;
            this.dataGridViewBookingDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewBookingDetails.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewBookingDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewBookingDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewBookingDetails.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewBookingDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewBookingDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBookingDetails.Location = new System.Drawing.Point(26, 12);
            this.dataGridViewBookingDetails.Name = "dataGridViewBookingDetails";
            this.dataGridViewBookingDetails.ReadOnly = true;
            this.dataGridViewBookingDetails.RowHeadersVisible = false;
            this.dataGridViewBookingDetails.RowHeadersWidth = 51;
            this.dataGridViewBookingDetails.RowTemplate.Height = 24;
            this.dataGridViewBookingDetails.Size = new System.Drawing.Size(999, 481);
            this.dataGridViewBookingDetails.TabIndex = 0;
            this.dataGridViewBookingDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBookingDetails_CellContentClick);
            // 
            // formMyBooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1037, 514);
            this.Controls.Add(this.dataGridViewBookingDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "formMyBooking";
            this.Load += new System.EventHandler(this.formMyBooking_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBookingDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewBookingDetails;
    }
}