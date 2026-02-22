using Microsoft.VisualBasic.ApplicationServices;
using ReaLTaiizor.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMS_FUTSAL_PROJECT
{
    public partial class formIndex : Form
    {
        [DllImport("user32.dll")]
        private static extern void ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern void SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        formLogin login;
        formHome home;
        formSchedule schedule;
        formCleaning cleaning;
        formBooking booking;
        formBudget budget;
        formBudgetData budgetData;
        formMyBooking myBooking;
        formRevenue revenue;
        formRevenueData revenueData;
        formScheduleMac scheduleMac;
        formAllPR allPR;
        formAllPO allPO;
        formExpense expense;
        formAllPayment payment;
        formManageEmp manageEmp;
        formOR formOR;
        public formIndex()
        {
            InitializeComponent();
            mdiProp();
        }

        bool sidebarExpand = true;
        private void mdiProp()
        {
            this.SetBevel(false);
            Controls.OfType<MdiClient>().FirstOrDefault().BackColor = Color.White;
        }
        private void sidebarTransition_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 5;
                if (sidebar.Width <= 50)
                {
                    sidebarExpand = false;
                    sidebarTransition.Stop();
                }
            }
            else
            {
                sidebar.Width += 5;
                if (sidebar.Width >= 190)
                {
                    sidebarExpand = true;
                    sidebarTransition.Stop();

                    pnHome.Width = sidebar.Width;
                }
            }
        }
        private void btnMenubar_Click(object sender, EventArgs e)
        {
            sidebarTransition.Start();
        }

        private void formIndex_Load(object sender, EventArgs e)
        {
            // เปิด formHome ทันทีเมื่อเข้าหน้านี้
            OpenHomeForm();

            if (CurrentUser.Role == "Student")
            {
                buttonSchedule.Visible = false;
                buttonCleaning.Visible = false;
                buttonPurchaseRequest.Visible = false;
                buttonPurchaseOrder.Visible = false;
                btnBudget.Visible = false;
                btnAllBud.Visible = false;
                btnAllRevenue.Visible = false;
                btnRevenue.Visible = false;
                buttonRepair.Visible = false;
                buttonPayment.Visible = false;
                buttonExpense.Visible = false;
                buttonEmployee.Visible = false;
                buttonOR.Visible = false;
            }
            if (CurrentUser.Role == "Employee" && CurrentUser.EmpPosition == "Cleaner")
            {
                btnBooking.Visible = false;
                buttonMyBooking.Visible = false;
                buttonSchedule.Visible = false;
                buttonPurchaseRequest.Visible = false;
                buttonPurchaseOrder.Visible = false;
                btnBudget.Visible = false;
                btnAllBud.Visible = false;
                btnAllRevenue.Visible = false;
                btnRevenue.Visible = false;
                buttonEmployee.Visible = true;
                buttonPayment.Visible = false;
                buttonExpense.Visible = false;
                buttonEmployee.Visible = false;
                buttonRepair.Visible = false;
                buttonOR.Visible = false;
            }
            if (CurrentUser.Role == "Employee" && CurrentUser.EmpPosition == "Mechanic")
            {
                buttonCleaning.Visible = false;
                btnBooking.Visible = false;
                buttonMyBooking.Visible = false;
                buttonSchedule.Visible = false;
                btnBudget.Visible = false;
                btnAllBud.Visible = false;
                btnAllRevenue.Visible = false;
                btnRevenue.Visible = false;
                buttonEmployee.Visible = true;
                buttonPayment.Visible = false;
                buttonExpense.Visible = false;
                buttonEmployee.Visible = false;
            }
            if (CurrentUser.Role == "Employee" && CurrentUser.EmpPosition == "Admin")
            {
                buttonCleaning.Visible = false;
                btnBooking.Visible = false;
                buttonMyBooking.Visible = false;
                buttonSchedule.Visible = false;
                btnBudget.Visible = false;
                btnAllBud.Visible = false;
                btnAllRevenue.Visible = false;
                btnRevenue.Visible = false;
                buttonExpense.Visible = false;
                buttonOR.Visible = false;
                buttonPayment.Visible = false;
                buttonPurchaseOrder.Visible = false;
                buttonPurchaseRequest.Visible = false;
                buttonRepair.Visible = false;
            }
            if (CurrentUser.Role == "Employee" && CurrentUser.EmpPosition == "Account")
            {
                buttonCleaning.Visible = false;
                btnBooking.Visible = false;
                buttonMyBooking.Visible = false;
                buttonSchedule.Visible = false;
                btnBudget.Visible = false;
                btnAllBud.Visible = false;
                buttonOR.Visible = false;
                buttonPurchaseRequest.Visible = false;
                buttonRepair.Visible = false;
                buttonEmployee.Visible = false;
            }
            if (CurrentUser.Role == "Employee" && CurrentUser.EmpPosition == "Finance")
            {
                buttonCleaning.Visible = false;
                btnBooking.Visible = false;
                buttonMyBooking.Visible = false;
                buttonSchedule.Visible = false;
                btnAllRevenue.Visible = false;
                btnRevenue.Visible = false;
                buttonExpense.Visible = false;
                buttonOR.Visible = false;
                buttonRepair.Visible = false;
                buttonEmployee.Visible = false;
            }
            if (CurrentUser.Role == "Employee" && CurrentUser.EmpPosition == "Futsal court supervisor")
            {
                buttonCleaning.Visible = false;
                btnBooking.Visible = false;
                buttonMyBooking.Visible = false;
                btnBudget.Visible = false;
                btnAllBud.Visible = false;
                btnAllRevenue.Visible = false;
                btnRevenue.Visible = false;
                buttonExpense.Visible = false;
                buttonOR.Visible = false;
                buttonPayment.Visible = false;
                buttonPurchaseOrder.Visible = false;
                buttonPurchaseRequest.Visible = false;
                buttonRepair.Visible = false;
                buttonEmployee.Visible = false;
            }
            if (CurrentUser.Role == "Employee" && CurrentUser.EmpPosition == "Futsal court scheduling manager")
            {
                buttonCleaning.Visible = false;
                btnBooking.Visible = false;
                buttonMyBooking.Visible = false;
                btnBudget.Visible = false;
                btnAllBud.Visible = false;
                btnAllRevenue.Visible = false;
                btnRevenue.Visible = false;
                buttonExpense.Visible = false;
                buttonOR.Visible = false;
                buttonPayment.Visible = false;
                buttonPurchaseOrder.Visible = false;
                buttonPurchaseRequest.Visible = false;
                buttonRepair.Visible = false;
                buttonEmployee.Visible = false;
            }
            if (CurrentUser.Role == "Employee" && CurrentUser.EmpPosition == "Futsal court scheduling manager")
            {
                buttonCleaning.Visible = false;
                btnBooking.Visible = false;
                buttonMyBooking.Visible = false;
                btnBudget.Visible = false;
                btnAllBud.Visible = false;
                btnAllRevenue.Visible = false;
                btnRevenue.Visible = false;
                buttonExpense.Visible = false;
                buttonOR.Visible = false;
                buttonPayment.Visible = false;
                buttonPurchaseOrder.Visible = false;
                buttonPurchaseRequest.Visible = false;
                buttonRepair.Visible = false;
                buttonEmployee.Visible = false;
            }
            if (CurrentUser.Role == "Employee" && CurrentUser.EmpPosition == "Account")
            {
                buttonCleaning.Visible = false;
                btnBooking.Visible = false;
                buttonMyBooking.Visible = false;
                buttonSchedule.Visible = false;
                btnBudget.Visible = false;
                btnAllBud.Visible = false;
                buttonOR.Visible = false;
                buttonPurchaseRequest.Visible = false;
                buttonRepair.Visible = false;
                buttonEmployee.Visible = false;
            }
        }
        private void OpenHomeForm()
        {
            // ตรวจสอบว่า home ถูกสร้างขึ้นแล้วหรือไม่
            if (home == null || home.IsDisposed)
            {
                home = new formHome();
                home.MdiParent = this; // ถ้าใช้ MDI Parent
                home.Show();
                home.SetBounds(0, 0, home.Width, home.Height);
            }
            else
            {
                home.BringToFront();
                home.Activate();
            }
        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            // ปิดฟอร์มที่เปิดอยู่ทั้งหมด ยกเว้นฟอร์ม cleaning
            foreach (Form openForm in this.MdiChildren)
            {
                if (openForm != booking)  // เช็คว่าฟอร์มที่เปิดไม่ใช่ฟอร์ม cleaning
                {
                    openForm.Close();  // ปิดฟอร์มนั้น
                }
            }

            // ตรวจสอบว่า cleaning ฟอร์มยังไม่ได้เปิดอยู่
            if (booking == null)
            {
                booking = new formBooking();
                booking.FormClosed += Booking_FormClosed;
                booking.MdiParent = this;
                booking.Show();
                booking.SetBounds(0, 0, booking.Width, booking.Height);
            }
            else
            {
                booking.Activate();  // ถ้าเปิดแล้วให้เลือกฟอร์มนี้
            }
        }
        private void Booking_FormClosed(object sender, FormClosedEventArgs e)
        {
            booking = null;
        }

        private void buttonCleaning_Click(object sender, EventArgs e)
        {
            // ปิดฟอร์มที่เปิดอยู่ทั้งหมด ยกเว้นฟอร์ม cleaning
            foreach (Form openForm in this.MdiChildren)
            {
                if (openForm != cleaning)  // เช็คว่าฟอร์มที่เปิดไม่ใช่ฟอร์ม cleaning
                {
                    openForm.Close();  // ปิดฟอร์มนั้น
                }
            }

            // ตรวจสอบว่า cleaning ฟอร์มยังไม่ได้เปิดอยู่
            if (cleaning == null)
            {
                cleaning = new formCleaning();
                cleaning.FormClosed += Cleaning_FormClosed;
                cleaning.MdiParent = this;
                cleaning.Show();
                cleaning.SetBounds(0, 0, cleaning.Width, cleaning.Height);


            }
            else
            {
                cleaning.Activate();  // ถ้าเปิดแล้วให้เลือกฟอร์มนี้
            }
        }
        private void Cleaning_FormClosed(object sender, FormClosedEventArgs e)
        {
            cleaning = null;
        }

        private void buttonSchedule_Click(object sender, EventArgs e)
        {
            // ปิดฟอร์มที่เปิดอยู่ทั้งหมด ยกเว้นฟอร์ม cleaning
            foreach (Form openForm in this.MdiChildren)
            {
                if (openForm != schedule)  // เช็คว่าฟอร์มที่เปิดไม่ใช่ฟอร์ม cleaning
                {
                    openForm.Close();  // ปิดฟอร์มนั้น
                }
            }
            if (schedule == null)
            {
                schedule = new formSchedule();
                schedule.FormClosed += Schedule_FormClosed;
                schedule.MdiParent = this;
                schedule.Show();
                schedule.SetBounds(0, 0, schedule.Width, schedule.Height);

            }
            else
            {
                schedule.Activate();
            }
        }

        private void Schedule_FormClosed(object sender, FormClosedEventArgs e)
        {
            schedule = null;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void btnBudget_Click(object sender, EventArgs e)
        {
            // ปิดฟอร์มที่เปิดอยู่ทั้งหมด ยกเว้นฟอร์ม cleaning
            foreach (Form openForm in this.MdiChildren)
            {
                if (openForm != budget)  // เช็คว่าฟอร์มที่เปิดไม่ใช่ฟอร์ม cleaning
                {
                    openForm.Close();  // ปิดฟอร์มนั้น
                }
            }
            if (budget == null)
            {
                budget = new formBudget();
                budget.FormClosed += Budget_FormClosed;
                budget.MdiParent = this;
                budget.Show();
                budget.SetBounds(0, 0, budget.Width, budget.Height);

            }
            else
            {
                budget.Activate();
            }
        }
        private void Budget_FormClosed(object sender, FormClosedEventArgs e)
        {
            budget = null;
        }

        private void btnAllBud_Click(object sender, EventArgs e)
        {
            // ปิดฟอร์มที่เปิดอยู่ทั้งหมด ยกเว้นฟอร์ม cleaning
            foreach (Form openForm in this.MdiChildren)
            {
                if (openForm != budgetData)  // เช็คว่าฟอร์มที่เปิดไม่ใช่ฟอร์ม cleaning
                {
                    openForm.Close();  // ปิดฟอร์มนั้น
                }
            }
            if (budgetData == null)
            {
                budgetData = new formBudgetData();
                budgetData.FormClosed += BudgetData_FormClosed;
                budgetData.MdiParent = this;
                budgetData.Show();
                budgetData.SetBounds(0, 0, budgetData.Width, budgetData.Height);

            }
            else
            {
                budgetData.Activate();
            }
        }
        private void BudgetData_FormClosed(object sender, FormClosedEventArgs e)
        {
            budgetData = null;
        }

        private void buttonMyBooking_Click(object sender, EventArgs e)
        {
            // ปิดฟอร์มที่เปิดอยู่ทั้งหมด ยกเว้นฟอร์ม cleaning
            foreach (Form openForm in this.MdiChildren)
            {
                if (openForm != myBooking)  // เช็คว่าฟอร์มที่เปิดไม่ใช่ฟอร์ม cleaning
                {
                    openForm.Close();  // ปิดฟอร์มนั้น
                }
            }
            if (myBooking == null)
            {
                myBooking = new formMyBooking();
                myBooking.FormClosed += MyBooking_FormClosed;
                myBooking.MdiParent = this;
                myBooking.Show();
                myBooking.SetBounds(0, 0, myBooking.Width, myBooking.Height);

            }
            else
            {
                myBooking.Activate();
            }
        }
        private void MyBooking_FormClosed(object sender, FormClosedEventArgs e)
        {
            myBooking = null;
        }

        private void btnRevenue_Click(object sender, EventArgs e)
        {
            // ปิดฟอร์มที่เปิดอยู่ทั้งหมด ยกเว้น revenue
            foreach (Form openForm in this.MdiChildren)
            {
                if (openForm != revenue)
                {
                    openForm.Close();
                }
            }

            // ถ้า revenue เป็น null หรือถูกปิดไปแล้ว (Disposed) ให้สร้างใหม่
            if (revenue == null || revenue.IsDisposed)
            {
                revenue = new formRevenue();
                revenue.FormClosed += formRevenue_FormClosed;
                revenue.MdiParent = this;
                revenue.Show();
                revenue.SetBounds(0, 0, revenue.Width, revenue.Height);
            }
            else
            {
                revenue.Activate();
            }
        }
        private void formRevenue_FormClosed(object sender, FormClosedEventArgs e)
        {
            revenue = null;
        }

        private void btnAllRevenue_Click(object sender, EventArgs e)
        {
            // ปิดฟอร์มที่เปิดอยู่ทั้งหมด ยกเว้น revenue
            foreach (Form openForm in this.MdiChildren)
            {
                if (openForm != revenueData)
                {
                    openForm.Close();
                }
            }

            // ถ้า revenue เป็น null หรือถูกปิดไปแล้ว (Disposed) ให้สร้างใหม่
            if (revenueData == null || revenueData.IsDisposed)
            {
                revenueData = new formRevenueData();
                revenueData.FormClosed += formRevenueData_FormClosed;
                revenueData.MdiParent = this;
                revenueData.Show();
                revenueData.SetBounds(0, 0, revenueData.Width, revenueData.Height);
            }
            else
            {
                revenueData.Activate();
            }
        }
        private void formRevenueData_FormClosed(object sender, FormClosedEventArgs e)
        {
            revenueData = null;
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            CurrentUser.Role = null;
            this.Hide();
            formLogin formLogin = new formLogin();
            formLogin.Show();
        }

        private void buttonRepair_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in this.MdiChildren)
            {
                if (openForm != scheduleMac)
                {
                    openForm.Close();
                }
            }

            // ถ้า revenue เป็น null หรือถูกปิดไปแล้ว (Disposed) ให้สร้างใหม่
            if (scheduleMac == null || scheduleMac.IsDisposed)
            {
                scheduleMac = new formScheduleMac();
                scheduleMac.FormClosed += ScheduleMac_FormClosed;
                scheduleMac.MdiParent = this;
                scheduleMac.Show();
                scheduleMac.SetBounds(0, 0, scheduleMac.Width, scheduleMac.Height);
            }
            else
            {
                scheduleMac.Activate();
            }
        }
        private void ScheduleMac_FormClosed(object sender, FormClosedEventArgs e)
        {
            scheduleMac = null;
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            homeTransition.Start();

            // ปิดทุกฟอร์มย่อย (ยกเว้น home)
            foreach (Form openForm in this.MdiChildren)
            {
                if (openForm != home)
                {
                    openForm.Close();
                }
            }

            // ตรวจสอบว่า home ถูกสร้างขึ้นแล้วหรือไม่
            if (home == null || home.IsDisposed)
            {
                home = new formHome();
                home.FormClosed += formRevenue_FormClosed;
                home.MdiParent = this;
                home.Show();
                home.SetBounds(0, 0, home.Width, home.Height);
            }
            else
            {
                // ถ้า home ยังเปิดอยู่ให้ดึงขึ้นมาแสดง
                home.BringToFront();
                home.Activate();
            }
        }
        bool homeExpand = false;
        private void homeTransition_Tick_1(object sender, EventArgs e)
        {
            if (homeExpand == false)
            {
                pnSubhome.Height -= 50;
                if (pnSubhome.Height <= 7)
                {
                    homeTransition.Stop();
                    homeExpand = true;
                }
            }
            else
            {
                pnSubhome.Height += 50;
                if (pnSubhome.Height >= 260)
                {
                    homeTransition.Stop();
                    homeExpand = false;
                }
            }
        }

        private void buttonPurchaseOrder_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in this.MdiChildren)
            {
                if (openForm != allPO)
                {
                    openForm.Close();
                }
            }

            // ถ้า revenue เป็น null หรือถูกปิดไปแล้ว (Disposed) ให้สร้างใหม่
            if (allPO == null || allPO.IsDisposed)
            {
                allPO = new formAllPO();
                allPO.FormClosed += AllPO_FormClosed;
                allPO.MdiParent = this;
                allPO.Show();
                allPO.SetBounds(0, 0, allPO.Width, allPO.Height);
            }
            else
            {
                allPO.Activate();
            }
        }
        private void AllPO_FormClosed(object sender, FormClosedEventArgs e)
        {
            allPO = null;
        }
        private void AllPR_FormClosed(object sender, FormClosedEventArgs e)
        {
            allPR = null;
        }

        private void buttonPurchaseRequest_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in this.MdiChildren)
            {
                if (openForm != allPR)
                {
                    openForm.Close();
                }
            }

            // ถ้า revenue เป็น null หรือถูกปิดไปแล้ว (Disposed) ให้สร้างใหม่
            if (allPR == null || allPR.IsDisposed)
            {
                allPR = new formAllPR();
                allPR.FormClosed += AllPR_FormClosed;
                allPR.MdiParent = this;
                allPR.Show();
                allPR.SetBounds(0, 0, allPR.Width, allPR.Height);
            }
            else
            {
                allPR.Activate();
            }
        }

        private void buttonExpense_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in this.MdiChildren)
            {
                if (openForm != expense)
                {
                    openForm.Close();
                }
            }

            // ถ้า revenue เป็น null หรือถูกปิดไปแล้ว (Disposed) ให้สร้างใหม่
            if (expense == null || expense.IsDisposed)
            {
                expense = new formExpense();
                expense.FormClosed += Expense_FormClosed;
                expense.MdiParent = this;
                expense.Show();
                expense.SetBounds(0, 0, expense.Width, expense.Height);
            }
            else
            {
                expense.Activate();
            }
        }
        private void Expense_FormClosed(object sender, FormClosedEventArgs e)
        {
            expense = null;
        }

        private void buttonPayment_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in this.MdiChildren)
            {
                if (openForm != payment)
                {
                    openForm.Close();
                }
            }

            // ถ้า revenue เป็น null หรือถูกปิดไปแล้ว (Disposed) ให้สร้างใหม่
            if (payment == null || payment.IsDisposed)
            {
                payment = new formAllPayment();
                payment.FormClosed += AllPayment_FormClosed;
                payment.MdiParent = this;
                payment.Show();
                payment.SetBounds(0, 0, payment.Width, payment.Height);
            }
            else
            {
                payment.Activate();
            }
        }
        private void AllPayment_FormClosed(object sender, FormClosedEventArgs e)
        {
            payment = null;
        }

        private void buttonEmployee_Click(object sender, EventArgs e)
        {
            
            foreach (Form openForm in this.MdiChildren)
            {
                if (openForm != manageEmp)
                {
                    openForm.Close();
                }
            }

            // ถ้า revenue เป็น null หรือถูกปิดไปแล้ว (Disposed) ให้สร้างใหม่
            if (manageEmp == null || manageEmp.IsDisposed)
            {
                manageEmp = new formManageEmp();
                manageEmp.FormClosed += ManageEmp_FormClosed;
                manageEmp.MdiParent = this;
                manageEmp.Show();
                manageEmp.SetBounds(0, 0, manageEmp.Width, manageEmp.Height);
            }
            else
            {
                manageEmp.Activate();
            }
        }
        private void ManageEmp_FormClosed(object sender, FormClosedEventArgs e)
        {
            manageEmp = null;
        }

        private void buttonOR_Click(object sender, EventArgs e)
        {

            foreach (Form openForm in this.MdiChildren)
            {
                if (openForm != formOR)
                {
                    openForm.Close();
                }
            }

            // ถ้า revenue เป็น null หรือถูกปิดไปแล้ว (Disposed) ให้สร้างใหม่
            if (formOR == null ||formOR.IsDisposed)
            {
                formOR = new formOR();
                formOR.FormClosed += FormOR_FormClosed;
                formOR.MdiParent = this;
                formOR.Show();
                formOR.SetBounds(0, 0, formOR.Width, formOR.Height);
            }
            else
            {
                formOR.Activate();
            }
        }
        private void FormOR_FormClosed(object sender, FormClosedEventArgs e)
        {
            formOR = null;
        }
    }
}

