namespace DBMS_FUTSAL_PROJECT
{
    partial class formBudgetData
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
            this.dataGridViewBudget = new System.Windows.Forms.DataGridView();
            this.dateTimePickBud = new System.Windows.Forms.DateTimePicker();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnPrintAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBudget)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewBudget
            // 
            this.dataGridViewBudget.AllowUserToAddRows = false;
            this.dataGridViewBudget.AllowUserToDeleteRows = false;
            this.dataGridViewBudget.AllowUserToResizeColumns = false;
            this.dataGridViewBudget.AllowUserToResizeRows = false;
            this.dataGridViewBudget.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewBudget.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewBudget.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBudget.Location = new System.Drawing.Point(13, 13);
            this.dataGridViewBudget.Name = "dataGridViewBudget";
            this.dataGridViewBudget.RowHeadersWidth = 51;
            this.dataGridViewBudget.RowTemplate.Height = 24;
            this.dataGridViewBudget.Size = new System.Drawing.Size(1049, 427);
            this.dataGridViewBudget.TabIndex = 0;
            this.dataGridViewBudget.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridViewBudget_CellBeginEdit);
            // 
            // dateTimePickBud
            // 
            this.dateTimePickBud.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickBud.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickBud.Location = new System.Drawing.Point(24, 449);
            this.dateTimePickBud.Name = "dateTimePickBud";
            this.dateTimePickBud.Size = new System.Drawing.Size(200, 31);
            this.dateTimePickBud.TabIndex = 1;
            // 
            // btnLoadData
            // 
            this.btnLoadData.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadData.Location = new System.Drawing.Point(250, 446);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(116, 41);
            this.btnLoadData.TabIndex = 2;
            this.btnLoadData.Text = "Load";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(388, 446);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(116, 41);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(527, 446);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(116, 41);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(667, 446);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(116, 41);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnPrintAll
            // 
            this.btnPrintAll.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintAll.Location = new System.Drawing.Point(939, 449);
            this.btnPrintAll.Name = "btnPrintAll";
            this.btnPrintAll.Size = new System.Drawing.Size(116, 41);
            this.btnPrintAll.TabIndex = 34;
            this.btnPrintAll.Text = "Print all";
            this.btnPrintAll.UseVisualStyleBackColor = true;
            this.btnPrintAll.Click += new System.EventHandler(this.btnPrintAll_Click);
            // 
            // formBudgetData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1079, 496);
            this.Controls.Add(this.btnPrintAll);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnLoadData);
            this.Controls.Add(this.dateTimePickBud);
            this.Controls.Add(this.dataGridViewBudget);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "formBudgetData";
            this.Text = "formBudgetData";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBudget)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewBudget;
        private System.Windows.Forms.DateTimePicker dateTimePickBud;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnPrintAll;
    }
}