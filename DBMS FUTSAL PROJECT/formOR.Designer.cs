namespace DBMS_FUTSAL_PROJECT
{
    partial class formOR
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
            this.dataGridViewOrderRequest = new System.Windows.Forms.DataGridView();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonAddCategory = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrderRequest)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewOrderRequest
            // 
            this.dataGridViewOrderRequest.AllowUserToResizeColumns = false;
            this.dataGridViewOrderRequest.AllowUserToResizeRows = false;
            this.dataGridViewOrderRequest.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewOrderRequest.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewOrderRequest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrderRequest.Location = new System.Drawing.Point(13, 43);
            this.dataGridViewOrderRequest.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewOrderRequest.Name = "dataGridViewOrderRequest";
            this.dataGridViewOrderRequest.RowHeadersWidth = 51;
            this.dataGridViewOrderRequest.Size = new System.Drawing.Size(1010, 390);
            this.dataGridViewOrderRequest.TabIndex = 0;
            // 
            // buttonSave
            // 
            this.buttonSave.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.ForeColor = System.Drawing.Color.DarkGreen;
            this.buttonSave.Location = new System.Drawing.Point(882, 452);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(141, 41);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonAddCategory
            // 
            this.buttonAddCategory.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddCategory.ForeColor = System.Drawing.Color.DarkGreen;
            this.buttonAddCategory.Location = new System.Drawing.Point(655, 452);
            this.buttonAddCategory.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddCategory.Name = "buttonAddCategory";
            this.buttonAddCategory.Size = new System.Drawing.Size(163, 41);
            this.buttonAddCategory.TabIndex = 2;
            this.buttonAddCategory.Text = "AddCategory";
            this.buttonAddCategory.UseVisualStyleBackColor = true;
            this.buttonAddCategory.Click += new System.EventHandler(this.buttonAddCategory_Click);
            // 
            // formOR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1041, 508);
            this.Controls.Add(this.buttonAddCategory);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.dataGridViewOrderRequest);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "formOR";
            this.Text = "formPR";
            this.Load += new System.EventHandler(this.formPR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrderRequest)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewOrderRequest;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonAddCategory;
    }
}