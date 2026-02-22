namespace DBMS_FUTSAL_PROJECT
{
    partial class formRevenue
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
            this.label5 = new System.Windows.Forms.Label();
            this.txtBudgetName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimeRev = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cbRevenueType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRevenueAmount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBudgetID = new System.Windows.Forms.TextBox();
            this.dgvBudget = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBudget)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(744, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 25);
            this.label5.TabIndex = 23;
            this.label5.Text = "Budget Name :";
            // 
            // txtBudgetName
            // 
            this.txtBudgetName.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBudgetName.Location = new System.Drawing.Point(903, 83);
            this.txtBudgetName.Name = "txtBudgetName";
            this.txtBudgetName.Size = new System.Drawing.Size(121, 31);
            this.txtBudgetName.TabIndex = 22;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(908, 423);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(116, 41);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(820, 229);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 25);
            this.label4.TabIndex = 20;
            this.label4.Text = "Date :";
            // 
            // dateTimeRev
            // 
            this.dateTimeRev.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimeRev.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimeRev.Location = new System.Drawing.Point(886, 224);
            this.dateTimeRev.Name = "dateTimeRev";
            this.dateTimeRev.Size = new System.Drawing.Size(139, 31);
            this.dateTimeRev.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(820, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 25);
            this.label3.TabIndex = 18;
            this.label3.Text = "Type :";
            // 
            // cbRevenueType
            // 
            this.cbRevenueType.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRevenueType.FormattingEnabled = true;
            this.cbRevenueType.Location = new System.Drawing.Point(903, 175);
            this.cbRevenueType.Name = "cbRevenueType";
            this.cbRevenueType.Size = new System.Drawing.Size(121, 33);
            this.cbRevenueType.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(791, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 25);
            this.label2.TabIndex = 16;
            this.label2.Text = "Amount :";
            // 
            // txtRevenueAmount
            // 
            this.txtRevenueAmount.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRevenueAmount.Location = new System.Drawing.Point(903, 128);
            this.txtRevenueAmount.Name = "txtRevenueAmount";
            this.txtRevenueAmount.Size = new System.Drawing.Size(121, 31);
            this.txtRevenueAmount.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(775, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 25);
            this.label1.TabIndex = 14;
            this.label1.Text = "Budget ID :";
            // 
            // txtBudgetID
            // 
            this.txtBudgetID.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBudgetID.Location = new System.Drawing.Point(903, 38);
            this.txtBudgetID.Name = "txtBudgetID";
            this.txtBudgetID.Size = new System.Drawing.Size(121, 31);
            this.txtBudgetID.TabIndex = 13;
            // 
            // dgvBudget
            // 
            this.dgvBudget.AllowUserToAddRows = false;
            this.dgvBudget.AllowUserToDeleteRows = false;
            this.dgvBudget.AllowUserToResizeColumns = false;
            this.dgvBudget.AllowUserToResizeRows = false;
            this.dgvBudget.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBudget.BackgroundColor = System.Drawing.Color.White;
            this.dgvBudget.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBudget.Location = new System.Drawing.Point(12, 25);
            this.dgvBudget.Name = "dgvBudget";
            this.dgvBudget.RowHeadersWidth = 51;
            this.dgvBudget.RowTemplate.Height = 24;
            this.dgvBudget.Size = new System.Drawing.Size(703, 439);
            this.dgvBudget.TabIndex = 12;
            this.dgvBudget.SelectionChanged += new System.EventHandler(this.dgvBudget_SelectionChanged);
            // 
            // formRevenue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1054, 485);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtBudgetName);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dateTimeRev);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbRevenueType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtRevenueAmount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBudgetID);
            this.Controls.Add(this.dgvBudget);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formRevenue";
            this.Text = "formRevenue";
            this.Load += new System.EventHandler(this.formRevenue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBudget)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBudgetName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimeRev;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbRevenueType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRevenueAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBudgetID;
        private System.Windows.Forms.DataGridView dgvBudget;
    }
}