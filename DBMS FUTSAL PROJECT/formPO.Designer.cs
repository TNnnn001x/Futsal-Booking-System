namespace DBMS_FUTSAL_PROJECT
{
    partial class formPO
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
            this.lblTel = new System.Windows.Forms.Label();
            this.lblAdd = new System.Windows.Forms.Label();
            this.lblAttn = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lbDepart = new System.Windows.Forms.Label();
            this.lbDate = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbNo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCompaTel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboPayment = new System.Windows.Forms.ComboBox();
            this.dataGridViewPO = new System.Windows.Forms.DataGridView();
            this.lblPODate = new System.Windows.Forms.Label();
            this.lblPOID = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPO)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTel
            // 
            this.lblTel.AutoSize = true;
            this.lblTel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTel.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTel.Location = new System.Drawing.Point(70, 120);
            this.lblTel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTel.Name = "lblTel";
            this.lblTel.Size = new System.Drawing.Size(61, 27);
            this.lblTel.TabIndex = 30;
            this.lblTel.Text = "label8";
            // 
            // lblAdd
            // 
            this.lblAdd.AutoSize = true;
            this.lblAdd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAdd.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdd.Location = new System.Drawing.Point(71, 87);
            this.lblAdd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAdd.Name = "lblAdd";
            this.lblAdd.Size = new System.Drawing.Size(61, 27);
            this.lblAdd.TabIndex = 29;
            this.lblAdd.Text = "label8";
            // 
            // lblAttn
            // 
            this.lblAttn.AutoSize = true;
            this.lblAttn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAttn.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAttn.Location = new System.Drawing.Point(71, 56);
            this.lblAttn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAttn.Name = "lblAttn";
            this.lblAttn.Size = new System.Drawing.Size(61, 27);
            this.lblAttn.TabIndex = 28;
            this.lblAttn.Text = "label8";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.Location = new System.Drawing.Point(125, 41);
            this.lblAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(515, 27);
            this.lblAddress.TabIndex = 27;
            this.lblAddress.Text = "1518 ถนน ​ประชา​ราษฎร์​1 แขวงวงศ์สว่าง บางซื่อ กรุงเทพมหานคร 10800";
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCompany.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompany.Location = new System.Drawing.Point(125, 16);
            this.lblCompany.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(278, 27);
            this.lblCompany.TabIndex = 26;
            this.lblCompany.Text = "มหาวิทยาลัยพระจอมเกล้าพระนครเหนือ";
            this.lblCompany.Click += new System.EventHandler(this.lblCompany_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(994, 538);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(98, 32);
            this.btnPrint.TabIndex = 25;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(13, 539);
            this.btnBack.Margin = new System.Windows.Forms.Padding(4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(94, 31);
            this.btnBack.TabIndex = 24;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lbDepart
            // 
            this.lbDepart.AutoSize = true;
            this.lbDepart.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDepart.Location = new System.Drawing.Point(16, 120);
            this.lbDepart.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDepart.Name = "lbDepart";
            this.lbDepart.Size = new System.Drawing.Size(41, 25);
            this.lbDepart.TabIndex = 21;
            this.lbDepart.Text = "Tel :";
            // 
            // lbDate
            // 
            this.lbDate.AutoSize = true;
            this.lbDate.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDate.Location = new System.Drawing.Point(11, 87);
            this.lbDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDate.Name = "lbDate";
            this.lbDate.Size = new System.Drawing.Size(55, 25);
            this.lbDate.TabIndex = 20;
            this.lbDate.Text = "Add :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 44);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 25);
            this.label4.TabIndex = 19;
            this.label4.Text = "Address :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 21);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 25);
            this.label3.TabIndex = 18;
            this.label3.Text = "Company :";
            // 
            // lbNo
            // 
            this.lbNo.AutoSize = true;
            this.lbNo.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNo.Location = new System.Drawing.Point(16, 56);
            this.lbNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbNo.Name = "lbNo";
            this.lbNo.Size = new System.Drawing.Size(50, 25);
            this.lbNo.TabIndex = 17;
            this.lbNo.Text = "Attn:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 25);
            this.label1.TabIndex = 16;
            this.label1.Text = "Purchase Order";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(52, 66);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 25);
            this.label6.TabIndex = 32;
            this.label6.Text = "Tel :";
            // 
            // lblCompaTel
            // 
            this.lblCompaTel.AutoSize = true;
            this.lblCompaTel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCompaTel.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompaTel.Location = new System.Drawing.Point(125, 66);
            this.lblCompaTel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCompaTel.Name = "lblCompaTel";
            this.lblCompaTel.Size = new System.Drawing.Size(114, 27);
            this.lblCompaTel.TabIndex = 33;
            this.lblCompaTel.Text = "02 555 2000";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboPayment);
            this.groupBox1.Controls.Add(this.dataGridViewPO);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lbNo);
            this.groupBox1.Controls.Add(this.lblPODate);
            this.groupBox1.Controls.Add(this.lbDate);
            this.groupBox1.Controls.Add(this.lblPOID);
            this.groupBox1.Controls.Add(this.lblTel);
            this.groupBox1.Controls.Add(this.lbDepart);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblAdd);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lblAttn);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Location = new System.Drawing.Point(16, 92);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1076, 439);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            // 
            // comboPayment
            // 
            this.comboPayment.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboPayment.FormattingEnabled = true;
            this.comboPayment.Location = new System.Drawing.Point(893, 124);
            this.comboPayment.Name = "comboPayment";
            this.comboPayment.Size = new System.Drawing.Size(121, 33);
            this.comboPayment.TabIndex = 43;
            // 
            // dataGridViewPO
            // 
            this.dataGridViewPO.AllowUserToAddRows = false;
            this.dataGridViewPO.AllowUserToDeleteRows = false;
            this.dataGridViewPO.AllowUserToResizeColumns = false;
            this.dataGridViewPO.AllowUserToResizeRows = false;
            this.dataGridViewPO.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPO.Location = new System.Drawing.Point(20, 168);
            this.dataGridViewPO.Name = "dataGridViewPO";
            this.dataGridViewPO.RowHeadersVisible = false;
            this.dataGridViewPO.RowHeadersWidth = 51;
            this.dataGridViewPO.RowTemplate.Height = 24;
            this.dataGridViewPO.Size = new System.Drawing.Size(1027, 262);
            this.dataGridViewPO.TabIndex = 42;
            // 
            // lblPODate
            // 
            this.lblPODate.AutoSize = true;
            this.lblPODate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPODate.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPODate.Location = new System.Drawing.Point(893, 93);
            this.lblPODate.Name = "lblPODate";
            this.lblPODate.Size = new System.Drawing.Size(61, 27);
            this.lblPODate.TabIndex = 40;
            this.lblPODate.Text = "label8";
            // 
            // lblPOID
            // 
            this.lblPOID.AutoSize = true;
            this.lblPOID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPOID.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPOID.Location = new System.Drawing.Point(893, 56);
            this.lblPOID.Name = "lblPOID";
            this.lblPOID.Size = new System.Drawing.Size(61, 27);
            this.lblPOID.TabIndex = 39;
            this.lblPOID.Text = "label8";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(733, 124);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(157, 25);
            this.label9.TabIndex = 37;
            this.label9.Text = "Payment Method :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(829, 93);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 25);
            this.label10.TabIndex = 36;
            this.label10.Text = "Date :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(839, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 25);
            this.label11.TabIndex = 35;
            this.label11.Text = "No. :";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(888, 539);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(98, 32);
            this.btnSave.TabIndex = 35;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // formPO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1105, 573);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCompaTel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.lblCompany);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formPO";
            this.Text = "formPO";
            this.Load += new System.EventHandler(this.formPO_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPO)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTel;
        private System.Windows.Forms.Label lblAdd;
        private System.Windows.Forms.Label lblAttn;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lbDepart;
        private System.Windows.Forms.Label lbDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblCompaTel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPOID;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dataGridViewPO;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox comboPayment;
        private System.Windows.Forms.Label lblPODate;
    }
}