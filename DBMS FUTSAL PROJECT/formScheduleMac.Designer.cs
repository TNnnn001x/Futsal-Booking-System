namespace DBMS_FUTSAL_PROJECT
{
    partial class formScheduleMac
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
            this.dataGridViewSchedule = new System.Windows.Forms.DataGridView();
            this.buttonRepair = new System.Windows.Forms.Button();
            this.buttonComplete = new System.Windows.Forms.Button();
            this.dtpDatePicker = new System.Windows.Forms.DateTimePicker();
            this.buttonRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSchedule)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewSchedule
            // 
            this.dataGridViewSchedule.AllowUserToAddRows = false;
            this.dataGridViewSchedule.AllowUserToDeleteRows = false;
            this.dataGridViewSchedule.AllowUserToResizeColumns = false;
            this.dataGridViewSchedule.AllowUserToResizeRows = false;
            this.dataGridViewSchedule.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewSchedule.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSchedule.Location = new System.Drawing.Point(23, 63);
            this.dataGridViewSchedule.Name = "dataGridViewSchedule";
            this.dataGridViewSchedule.RowHeadersVisible = false;
            this.dataGridViewSchedule.RowHeadersWidth = 51;
            this.dataGridViewSchedule.RowTemplate.Height = 24;
            this.dataGridViewSchedule.Size = new System.Drawing.Size(1032, 380);
            this.dataGridViewSchedule.TabIndex = 29;
            // 
            // buttonRepair
            // 
            this.buttonRepair.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRepair.ForeColor = System.Drawing.Color.DarkGreen;
            this.buttonRepair.Location = new System.Drawing.Point(52, 452);
            this.buttonRepair.Name = "buttonRepair";
            this.buttonRepair.Size = new System.Drawing.Size(116, 41);
            this.buttonRepair.TabIndex = 30;
            this.buttonRepair.Text = "Repair";
            this.buttonRepair.UseVisualStyleBackColor = true;
            this.buttonRepair.Click += new System.EventHandler(this.buttonRepair_Click);
            // 
            // buttonComplete
            // 
            this.buttonComplete.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonComplete.ForeColor = System.Drawing.Color.DarkGreen;
            this.buttonComplete.Location = new System.Drawing.Point(925, 452);
            this.buttonComplete.Name = "buttonComplete";
            this.buttonComplete.Size = new System.Drawing.Size(116, 41);
            this.buttonComplete.TabIndex = 31;
            this.buttonComplete.Text = "Complete";
            this.buttonComplete.UseVisualStyleBackColor = true;
            this.buttonComplete.Click += new System.EventHandler(this.buttonComplete_Click);
            // 
            // dtpDatePicker
            // 
            this.dtpDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDatePicker.Location = new System.Drawing.Point(53, 20);
            this.dtpDatePicker.Name = "dtpDatePicker";
            this.dtpDatePicker.Size = new System.Drawing.Size(114, 22);
            this.dtpDatePicker.TabIndex = 41;
            this.dtpDatePicker.ValueChanged += new System.EventHandler(this.dtpDatePicker_ValueChanged);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRefresh.ForeColor = System.Drawing.Color.DarkGreen;
            this.buttonRefresh.Location = new System.Drawing.Point(183, 15);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(103, 31);
            this.buttonRefresh.TabIndex = 43;
            this.buttonRefresh.Text = "Show all";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // formScheduleMac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1081, 505);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.dtpDatePicker);
            this.Controls.Add(this.buttonComplete);
            this.Controls.Add(this.buttonRepair);
            this.Controls.Add(this.dataGridViewSchedule);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "formScheduleMac";
            this.Text = "formScheduleMac";
            this.Load += new System.EventHandler(this.formScheduleMac_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSchedule)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewSchedule;
        private System.Windows.Forms.Button buttonRepair;
        private System.Windows.Forms.Button buttonComplete;
        private System.Windows.Forms.DateTimePicker dtpDatePicker;
        private System.Windows.Forms.Button buttonRefresh;
    }
}