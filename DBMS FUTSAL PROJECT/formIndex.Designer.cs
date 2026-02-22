namespace DBMS_FUTSAL_PROJECT
{
    partial class formIndex
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nightControlBox1 = new ReaLTaiizor.Controls.NightControlBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMenubar = new System.Windows.Forms.PictureBox();
            this.sidebar = new System.Windows.Forms.FlowLayoutPanel();
            this.pnHome = new System.Windows.Forms.Panel();
            this.pnSubhome = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonOR = new System.Windows.Forms.Button();
            this.buttonRepair = new System.Windows.Forms.Button();
            this.buttonCleaning = new System.Windows.Forms.Button();
            this.buttonMyBooking = new System.Windows.Forms.Button();
            this.buttonSchedule = new System.Windows.Forms.Button();
            this.btnBooking = new System.Windows.Forms.Button();
            this.buttonPurchaseOrder = new System.Windows.Forms.Button();
            this.buttonPurchaseRequest = new System.Windows.Forms.Button();
            this.buttonPayment = new System.Windows.Forms.Button();
            this.buttonExpense = new System.Windows.Forms.Button();
            this.btnRevenue = new System.Windows.Forms.Button();
            this.btnAllRevenue = new System.Windows.Forms.Button();
            this.btnBudget = new System.Windows.Forms.Button();
            this.btnAllBud = new System.Windows.Forms.Button();
            this.buttonEmployee = new System.Windows.Forms.Button();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.buttonHome = new System.Windows.Forms.Button();
            this.homeTransition = new System.Windows.Forms.Timer(this.components);
            this.sidebarTransition = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMenubar)).BeginInit();
            this.sidebar.SuspendLayout();
            this.pnHome.SuspendLayout();
            this.pnSubhome.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkRed;
            this.panel1.Controls.Add(this.nightControlBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnMenubar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1367, 57);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // nightControlBox1
            // 
            this.nightControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nightControlBox1.BackColor = System.Drawing.Color.Transparent;
            this.nightControlBox1.CloseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.nightControlBox1.CloseHoverForeColor = System.Drawing.Color.White;
            this.nightControlBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nightControlBox1.DefaultLocation = true;
            this.nightControlBox1.DisableMaximizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.nightControlBox1.DisableMinimizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.nightControlBox1.EnableCloseColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.nightControlBox1.EnableMaximizeButton = true;
            this.nightControlBox1.EnableMaximizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.nightControlBox1.EnableMinimizeButton = true;
            this.nightControlBox1.EnableMinimizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.nightControlBox1.Location = new System.Drawing.Point(1228, 0);
            this.nightControlBox1.MaximizeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.nightControlBox1.MaximizeHoverForeColor = System.Drawing.Color.White;
            this.nightControlBox1.MinimizeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.nightControlBox1.MinimizeHoverForeColor = System.Drawing.Color.White;
            this.nightControlBox1.Name = "nightControlBox1";
            this.nightControlBox1.Size = new System.Drawing.Size(139, 31);
            this.nightControlBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkOrange;
            this.label1.Location = new System.Drawing.Point(89, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(516, 38);
            this.label1.TabIndex = 1;
            this.label1.Text = "KMUTNB | FUTSAL BOOKING SYSTEM";
            // 
            // btnMenubar
            // 
            this.btnMenubar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMenubar.Image = global::DBMS_FUTSAL_PROJECT.Properties.Resources.menubar;
            this.btnMenubar.Location = new System.Drawing.Point(26, 12);
            this.btnMenubar.Name = "btnMenubar";
            this.btnMenubar.Size = new System.Drawing.Size(44, 35);
            this.btnMenubar.TabIndex = 0;
            this.btnMenubar.TabStop = false;
            this.btnMenubar.Click += new System.EventHandler(this.btnMenubar_Click);
            // 
            // sidebar
            // 
            this.sidebar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.sidebar.Controls.Add(this.pnHome);
            this.sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebar.Location = new System.Drawing.Point(0, 57);
            this.sidebar.Name = "sidebar";
            this.sidebar.Size = new System.Drawing.Size(255, 513);
            this.sidebar.TabIndex = 1;
            // 
            // pnHome
            // 
            this.pnHome.BackColor = System.Drawing.Color.Transparent;
            this.pnHome.Controls.Add(this.pnSubhome);
            this.pnHome.Controls.Add(this.buttonLogout);
            this.pnHome.Controls.Add(this.buttonHome);
            this.pnHome.ForeColor = System.Drawing.Color.Transparent;
            this.pnHome.Location = new System.Drawing.Point(3, 3);
            this.pnHome.Name = "pnHome";
            this.pnHome.Size = new System.Drawing.Size(260, 510);
            this.pnHome.TabIndex = 3;
            // 
            // pnSubhome
            // 
            this.pnSubhome.Controls.Add(this.buttonOR);
            this.pnSubhome.Controls.Add(this.buttonRepair);
            this.pnSubhome.Controls.Add(this.buttonCleaning);
            this.pnSubhome.Controls.Add(this.buttonMyBooking);
            this.pnSubhome.Controls.Add(this.buttonSchedule);
            this.pnSubhome.Controls.Add(this.btnBooking);
            this.pnSubhome.Controls.Add(this.buttonPurchaseOrder);
            this.pnSubhome.Controls.Add(this.buttonPurchaseRequest);
            this.pnSubhome.Controls.Add(this.buttonPayment);
            this.pnSubhome.Controls.Add(this.buttonExpense);
            this.pnSubhome.Controls.Add(this.btnRevenue);
            this.pnSubhome.Controls.Add(this.btnAllRevenue);
            this.pnSubhome.Controls.Add(this.btnBudget);
            this.pnSubhome.Controls.Add(this.btnAllBud);
            this.pnSubhome.Controls.Add(this.buttonEmployee);
            this.pnSubhome.Location = new System.Drawing.Point(0, 59);
            this.pnSubhome.Name = "pnSubhome";
            this.pnSubhome.Size = new System.Drawing.Size(250, 336);
            this.pnSubhome.TabIndex = 0;
            // 
            // buttonOR
            // 
            this.buttonOR.BackColor = System.Drawing.Color.White;
            this.buttonOR.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOR.ForeColor = System.Drawing.Color.Green;
            this.buttonOR.Image = global::DBMS_FUTSAL_PROJECT.Properties.Resources.or;
            this.buttonOR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOR.Location = new System.Drawing.Point(3, 3);
            this.buttonOR.Name = "buttonOR";
            this.buttonOR.Size = new System.Drawing.Size(239, 50);
            this.buttonOR.TabIndex = 17;
            this.buttonOR.Text = "   Order Request";
            this.buttonOR.UseVisualStyleBackColor = false;
            this.buttonOR.Click += new System.EventHandler(this.buttonOR_Click);
            // 
            // buttonRepair
            // 
            this.buttonRepair.BackColor = System.Drawing.Color.White;
            this.buttonRepair.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRepair.ForeColor = System.Drawing.Color.Green;
            this.buttonRepair.Image = global::DBMS_FUTSAL_PROJECT.Properties.Resources.repair;
            this.buttonRepair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRepair.Location = new System.Drawing.Point(3, 59);
            this.buttonRepair.Name = "buttonRepair";
            this.buttonRepair.Size = new System.Drawing.Size(239, 50);
            this.buttonRepair.TabIndex = 13;
            this.buttonRepair.Text = "Repair";
            this.buttonRepair.UseVisualStyleBackColor = false;
            this.buttonRepair.Click += new System.EventHandler(this.buttonRepair_Click);
            // 
            // buttonCleaning
            // 
            this.buttonCleaning.BackColor = System.Drawing.Color.White;
            this.buttonCleaning.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCleaning.ForeColor = System.Drawing.Color.Green;
            this.buttonCleaning.Image = global::DBMS_FUTSAL_PROJECT.Properties.Resources.cleaning;
            this.buttonCleaning.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCleaning.Location = new System.Drawing.Point(3, 115);
            this.buttonCleaning.Name = "buttonCleaning";
            this.buttonCleaning.Size = new System.Drawing.Size(239, 50);
            this.buttonCleaning.TabIndex = 5;
            this.buttonCleaning.Text = "Cleaning";
            this.buttonCleaning.UseVisualStyleBackColor = false;
            this.buttonCleaning.Click += new System.EventHandler(this.buttonCleaning_Click);
            // 
            // buttonMyBooking
            // 
            this.buttonMyBooking.BackColor = System.Drawing.Color.White;
            this.buttonMyBooking.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMyBooking.ForeColor = System.Drawing.Color.Green;
            this.buttonMyBooking.Image = global::DBMS_FUTSAL_PROJECT.Properties.Resources.booking;
            this.buttonMyBooking.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMyBooking.Location = new System.Drawing.Point(3, 171);
            this.buttonMyBooking.Name = "buttonMyBooking";
            this.buttonMyBooking.Size = new System.Drawing.Size(239, 50);
            this.buttonMyBooking.TabIndex = 8;
            this.buttonMyBooking.Text = "  My Booking";
            this.buttonMyBooking.UseVisualStyleBackColor = false;
            this.buttonMyBooking.Click += new System.EventHandler(this.buttonMyBooking_Click);
            // 
            // buttonSchedule
            // 
            this.buttonSchedule.BackColor = System.Drawing.Color.White;
            this.buttonSchedule.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSchedule.ForeColor = System.Drawing.Color.Green;
            this.buttonSchedule.Image = global::DBMS_FUTSAL_PROJECT.Properties.Resources.schedule;
            this.buttonSchedule.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSchedule.Location = new System.Drawing.Point(3, 227);
            this.buttonSchedule.Name = "buttonSchedule";
            this.buttonSchedule.Size = new System.Drawing.Size(239, 50);
            this.buttonSchedule.TabIndex = 2;
            this.buttonSchedule.Text = "Schedule";
            this.buttonSchedule.UseVisualStyleBackColor = false;
            this.buttonSchedule.Click += new System.EventHandler(this.buttonSchedule_Click);
            // 
            // btnBooking
            // 
            this.btnBooking.BackColor = System.Drawing.Color.White;
            this.btnBooking.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBooking.ForeColor = System.Drawing.Color.Green;
            this.btnBooking.Image = global::DBMS_FUTSAL_PROJECT.Properties.Resources.booking1;
            this.btnBooking.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBooking.Location = new System.Drawing.Point(3, 283);
            this.btnBooking.Name = "btnBooking";
            this.btnBooking.Size = new System.Drawing.Size(239, 50);
            this.btnBooking.TabIndex = 4;
            this.btnBooking.Text = "Booking";
            this.btnBooking.UseVisualStyleBackColor = false;
            this.btnBooking.Click += new System.EventHandler(this.btnBooking_Click);
            // 
            // buttonPurchaseOrder
            // 
            this.buttonPurchaseOrder.BackColor = System.Drawing.Color.White;
            this.buttonPurchaseOrder.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPurchaseOrder.ForeColor = System.Drawing.Color.Green;
            this.buttonPurchaseOrder.Image = global::DBMS_FUTSAL_PROJECT.Properties.Resources.po;
            this.buttonPurchaseOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonPurchaseOrder.Location = new System.Drawing.Point(3, 339);
            this.buttonPurchaseOrder.Name = "buttonPurchaseOrder";
            this.buttonPurchaseOrder.Size = new System.Drawing.Size(239, 77);
            this.buttonPurchaseOrder.TabIndex = 12;
            this.buttonPurchaseOrder.Text = "Purchase\r\nOrder";
            this.buttonPurchaseOrder.UseVisualStyleBackColor = false;
            this.buttonPurchaseOrder.Click += new System.EventHandler(this.buttonPurchaseOrder_Click);
            // 
            // buttonPurchaseRequest
            // 
            this.buttonPurchaseRequest.BackColor = System.Drawing.Color.White;
            this.buttonPurchaseRequest.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPurchaseRequest.ForeColor = System.Drawing.Color.Green;
            this.buttonPurchaseRequest.Image = global::DBMS_FUTSAL_PROJECT.Properties.Resources.pr;
            this.buttonPurchaseRequest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonPurchaseRequest.Location = new System.Drawing.Point(3, 422);
            this.buttonPurchaseRequest.Name = "buttonPurchaseRequest";
            this.buttonPurchaseRequest.Size = new System.Drawing.Size(239, 79);
            this.buttonPurchaseRequest.TabIndex = 11;
            this.buttonPurchaseRequest.Text = "Purchase\r\nRequest";
            this.buttonPurchaseRequest.UseVisualStyleBackColor = false;
            this.buttonPurchaseRequest.Click += new System.EventHandler(this.buttonPurchaseRequest_Click);
            // 
            // buttonPayment
            // 
            this.buttonPayment.BackColor = System.Drawing.Color.White;
            this.buttonPayment.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPayment.ForeColor = System.Drawing.Color.Green;
            this.buttonPayment.Image = global::DBMS_FUTSAL_PROJECT.Properties.Resources.payment;
            this.buttonPayment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonPayment.Location = new System.Drawing.Point(3, 507);
            this.buttonPayment.Name = "buttonPayment";
            this.buttonPayment.Size = new System.Drawing.Size(239, 50);
            this.buttonPayment.TabIndex = 15;
            this.buttonPayment.Text = "Payment";
            this.buttonPayment.UseVisualStyleBackColor = false;
            this.buttonPayment.Click += new System.EventHandler(this.buttonPayment_Click);
            // 
            // buttonExpense
            // 
            this.buttonExpense.BackColor = System.Drawing.Color.White;
            this.buttonExpense.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExpense.ForeColor = System.Drawing.Color.Green;
            this.buttonExpense.Image = global::DBMS_FUTSAL_PROJECT.Properties.Resources.expense;
            this.buttonExpense.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExpense.Location = new System.Drawing.Point(3, 563);
            this.buttonExpense.Name = "buttonExpense";
            this.buttonExpense.Size = new System.Drawing.Size(239, 50);
            this.buttonExpense.TabIndex = 12;
            this.buttonExpense.Text = "Expense";
            this.buttonExpense.UseVisualStyleBackColor = false;
            this.buttonExpense.Click += new System.EventHandler(this.buttonExpense_Click);
            // 
            // btnRevenue
            // 
            this.btnRevenue.BackColor = System.Drawing.Color.White;
            this.btnRevenue.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRevenue.ForeColor = System.Drawing.Color.Green;
            this.btnRevenue.Image = global::DBMS_FUTSAL_PROJECT.Properties.Resources.revenue1;
            this.btnRevenue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRevenue.Location = new System.Drawing.Point(3, 619);
            this.btnRevenue.Name = "btnRevenue";
            this.btnRevenue.Size = new System.Drawing.Size(239, 50);
            this.btnRevenue.TabIndex = 9;
            this.btnRevenue.Text = "Revenue";
            this.btnRevenue.UseVisualStyleBackColor = false;
            this.btnRevenue.Click += new System.EventHandler(this.btnRevenue_Click);
            // 
            // btnAllRevenue
            // 
            this.btnAllRevenue.BackColor = System.Drawing.Color.White;
            this.btnAllRevenue.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAllRevenue.ForeColor = System.Drawing.Color.Green;
            this.btnAllRevenue.Image = global::DBMS_FUTSAL_PROJECT.Properties.Resources.revenue0;
            this.btnAllRevenue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAllRevenue.Location = new System.Drawing.Point(3, 675);
            this.btnAllRevenue.Name = "btnAllRevenue";
            this.btnAllRevenue.Size = new System.Drawing.Size(239, 50);
            this.btnAllRevenue.TabIndex = 10;
            this.btnAllRevenue.Text = "  All Revenue";
            this.btnAllRevenue.UseVisualStyleBackColor = false;
            this.btnAllRevenue.Click += new System.EventHandler(this.btnAllRevenue_Click);
            // 
            // btnBudget
            // 
            this.btnBudget.BackColor = System.Drawing.Color.White;
            this.btnBudget.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBudget.ForeColor = System.Drawing.Color.Green;
            this.btnBudget.Image = global::DBMS_FUTSAL_PROJECT.Properties.Resources.budget1;
            this.btnBudget.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBudget.Location = new System.Drawing.Point(3, 731);
            this.btnBudget.Name = "btnBudget";
            this.btnBudget.Size = new System.Drawing.Size(239, 50);
            this.btnBudget.TabIndex = 6;
            this.btnBudget.Text = "Budget";
            this.btnBudget.UseVisualStyleBackColor = false;
            this.btnBudget.Click += new System.EventHandler(this.btnBudget_Click);
            // 
            // btnAllBud
            // 
            this.btnAllBud.BackColor = System.Drawing.Color.White;
            this.btnAllBud.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAllBud.ForeColor = System.Drawing.Color.Green;
            this.btnAllBud.Image = global::DBMS_FUTSAL_PROJECT.Properties.Resources.budget;
            this.btnAllBud.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAllBud.Location = new System.Drawing.Point(3, 787);
            this.btnAllBud.Name = "btnAllBud";
            this.btnAllBud.Size = new System.Drawing.Size(239, 50);
            this.btnAllBud.TabIndex = 7;
            this.btnAllBud.Text = "All budget";
            this.btnAllBud.UseVisualStyleBackColor = false;
            this.btnAllBud.Click += new System.EventHandler(this.btnAllBud_Click);
            // 
            // buttonEmployee
            // 
            this.buttonEmployee.BackColor = System.Drawing.Color.White;
            this.buttonEmployee.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEmployee.ForeColor = System.Drawing.Color.Green;
            this.buttonEmployee.Image = global::DBMS_FUTSAL_PROJECT.Properties.Resources.employee;
            this.buttonEmployee.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEmployee.Location = new System.Drawing.Point(3, 843);
            this.buttonEmployee.Name = "buttonEmployee";
            this.buttonEmployee.Size = new System.Drawing.Size(239, 50);
            this.buttonEmployee.TabIndex = 16;
            this.buttonEmployee.Text = "Employee";
            this.buttonEmployee.UseVisualStyleBackColor = false;
            this.buttonEmployee.Click += new System.EventHandler(this.buttonEmployee_Click);
            // 
            // buttonLogout
            // 
            this.buttonLogout.BackColor = System.Drawing.Color.White;
            this.buttonLogout.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLogout.ForeColor = System.Drawing.Color.Green;
            this.buttonLogout.Image = global::DBMS_FUTSAL_PROJECT.Properties.Resources.logout;
            this.buttonLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLogout.Location = new System.Drawing.Point(6, 457);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(239, 50);
            this.buttonLogout.TabIndex = 3;
            this.buttonLogout.Text = "LOG OUT";
            this.buttonLogout.UseVisualStyleBackColor = false;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // buttonHome
            // 
            this.buttonHome.BackColor = System.Drawing.Color.White;
            this.buttonHome.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonHome.ForeColor = System.Drawing.Color.Green;
            this.buttonHome.Image = global::DBMS_FUTSAL_PROJECT.Properties.Resources.home;
            this.buttonHome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonHome.Location = new System.Drawing.Point(3, 3);
            this.buttonHome.Name = "buttonHome";
            this.buttonHome.Size = new System.Drawing.Size(242, 50);
            this.buttonHome.TabIndex = 3;
            this.buttonHome.Text = "Home";
            this.buttonHome.UseVisualStyleBackColor = false;
            this.buttonHome.Click += new System.EventHandler(this.buttonHome_Click);
            // 
            // homeTransition
            // 
            this.homeTransition.Interval = 10;
            this.homeTransition.Tick += new System.EventHandler(this.homeTransition_Tick_1);
            // 
            // sidebarTransition
            // 
            this.sidebarTransition.Interval = 10;
            this.sidebarTransition.Tick += new System.EventHandler(this.sidebarTransition_Tick);
            // 
            // formIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1367, 570);
            this.Controls.Add(this.sidebar);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.Name = "formIndex";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Index";
            this.Load += new System.EventHandler(this.formIndex_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMenubar)).EndInit();
            this.sidebar.ResumeLayout(false);
            this.pnHome.ResumeLayout(false);
            this.pnSubhome.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox btnMenubar;
        private System.Windows.Forms.Label label1;
        private ReaLTaiizor.Controls.NightControlBox nightControlBox1;
        private System.Windows.Forms.FlowLayoutPanel sidebar;
        private System.Windows.Forms.Timer homeTransition;
        private System.Windows.Forms.Timer sidebarTransition;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.Button btnBooking;
        private System.Windows.Forms.Button buttonCleaning;
        private System.Windows.Forms.Button btnBudget;
        private System.Windows.Forms.Button btnAllBud;
        private System.Windows.Forms.Button buttonMyBooking;
        private System.Windows.Forms.Button btnRevenue;
        private System.Windows.Forms.Button btnAllRevenue;
        private System.Windows.Forms.Button buttonSchedule;
        private System.Windows.Forms.FlowLayoutPanel pnSubhome;
        private System.Windows.Forms.Panel pnHome;
        private System.Windows.Forms.Button buttonHome;
        private System.Windows.Forms.Button buttonPurchaseRequest;
        private System.Windows.Forms.Button buttonPurchaseOrder;
        private System.Windows.Forms.Button buttonRepair;
        private System.Windows.Forms.Button buttonExpense;
        private System.Windows.Forms.Button buttonPayment;
        private System.Windows.Forms.Button buttonEmployee;
        private System.Windows.Forms.Button buttonOR;
    }
}

