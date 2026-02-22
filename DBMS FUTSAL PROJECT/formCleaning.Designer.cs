namespace DBMS_FUTSAL_PROJECT
{
    partial class formCleaning
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
            this.dataGridViewCleaningSchedule = new System.Windows.Forms.DataGridView();
            this.buttonSave = new System.Windows.Forms.Button();
            this.comboBoxDate = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCleaningSchedule)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewCleaningSchedule
            // 
            this.dataGridViewCleaningSchedule.AllowUserToAddRows = false;
            this.dataGridViewCleaningSchedule.AllowUserToDeleteRows = false;
            this.dataGridViewCleaningSchedule.AllowUserToResizeColumns = false;
            this.dataGridViewCleaningSchedule.AllowUserToResizeRows = false;
            this.dataGridViewCleaningSchedule.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewCleaningSchedule.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewCleaningSchedule.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewCleaningSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCleaningSchedule.Location = new System.Drawing.Point(85, 87);
            this.dataGridViewCleaningSchedule.Name = "dataGridViewCleaningSchedule";
            this.dataGridViewCleaningSchedule.RowHeadersVisible = false;
            this.dataGridViewCleaningSchedule.RowHeadersWidth = 51;
            this.dataGridViewCleaningSchedule.RowTemplate.Height = 24;
            this.dataGridViewCleaningSchedule.Size = new System.Drawing.Size(905, 305);
            this.dataGridViewCleaningSchedule.TabIndex = 29;
            // 
            // buttonSave
            // 
            this.buttonSave.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.Location = new System.Drawing.Point(903, 443);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 32);
            this.buttonSave.TabIndex = 31;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // comboBoxDate
            // 
            this.comboBoxDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBoxDate.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDate.FormattingEnabled = true;
            this.comboBoxDate.Location = new System.Drawing.Point(12, 42);
            this.comboBoxDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxDate.Name = "comboBoxDate";
            this.comboBoxDate.Size = new System.Drawing.Size(147, 25);
            this.comboBoxDate.TabIndex = 32;
            // 
            // formCleaning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1084, 496);
            this.Controls.Add(this.comboBoxDate);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.dataGridViewCleaningSchedule);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "formCleaning";
            this.Text = "formCleaning";
            this.Load += new System.EventHandler(this.formCleaning_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCleaningSchedule)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewCleaningSchedule;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ComboBox comboBoxDate;
    }
}