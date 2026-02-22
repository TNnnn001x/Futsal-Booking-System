namespace DBMS_FUTSAL_PROJECT
{
    partial class formAllPayment
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
            this.dataGridViewPayment = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPayment)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewPayment
            // 
            this.dataGridViewPayment.AllowUserToAddRows = false;
            this.dataGridViewPayment.AllowUserToDeleteRows = false;
            this.dataGridViewPayment.AllowUserToResizeColumns = false;
            this.dataGridViewPayment.AllowUserToResizeRows = false;
            this.dataGridViewPayment.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPayment.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewPayment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPayment.Location = new System.Drawing.Point(12, 32);
            this.dataGridViewPayment.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewPayment.Name = "dataGridViewPayment";
            this.dataGridViewPayment.RowHeadersVisible = false;
            this.dataGridViewPayment.RowHeadersWidth = 51;
            this.dataGridViewPayment.RowTemplate.Height = 24;
            this.dataGridViewPayment.Size = new System.Drawing.Size(1058, 354);
            this.dataGridViewPayment.TabIndex = 0;
            this.dataGridViewPayment.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPayment_CellContentClick);
            // 
            // formAllPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1082, 431);
            this.Controls.Add(this.dataGridViewPayment);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formAllPayment";
            this.Text = "formPayment";
            this.Load += new System.EventHandler(this.formPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPayment)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewPayment;
    }
}