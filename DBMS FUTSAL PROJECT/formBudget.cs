using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;

namespace DBMS_FUTSAL_PROJECT
{
    public partial class formBudget : Form
    {
        private string strConnectionString = "";  // ประกาศตัวแปรสำหรับ Connection String
        public formBudget()
        {
            InitializeComponent();
            LoadConnectionString();
            LoadBudgetTypes();
            dateTimeBudget.Format = DateTimePickerFormat.Short; // ตั้งค่าให้เป็น Short Date
        }
        private void LoadConnectionString()
        {
            try
            {
                if (File.Exists("ConnectionString.ini"))
                {
                    strConnectionString = File.ReadAllText("ConnectionString.ini").Trim();
                }
                else
                {
                    MessageBox.Show("ไม่พบไฟล์ ConnectionString.ini");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading connection string: " + ex.Message);
            }
        }
        private void LoadBudgetTypes()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT Budget_Type_ID, Budget_Type FROM BudgetType", conn))
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        cbType.DataSource = dt;
                        cbType.DisplayMember = "Budget_Type"; // แสดงชื่อประเภท
                        cbType.ValueMember = "Budget_Type_ID"; // ใช้ ID ในฐานข้อมูล
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading budget types: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtBudName.Text.Trim();
            DateTime date = dateTimeBudget.Value;
            decimal amount;

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("กรุณากรอกชื่อรายการงบประมาณ");
                txtBudName.Focus();
                return;
            }

            if (cbType.SelectedValue == null)
            {
                MessageBox.Show("กรุณาเลือกประเภทงบประมาณ");
                cbType.Focus();
                return;
            }

            int budgetTypeId = Convert.ToInt32(cbType.SelectedValue);

            if (!decimal.TryParse(txtBudAmount.Text, out amount) || amount <= 0)
            {
                MessageBox.Show("กรุณากรอกจำนวนเงินที่ถูกต้อง");
                txtBudAmount.Focus();
                return;
            }

            // ตรวจสอบว่า empID มีค่าหรือไม่
            if (CurrentUser.EmpID <= 0)
            {
                MessageBox.Show("ไม่พบ EmpID ของผู้ใช้งาน กรุณาล็อกอินใหม่");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Budget (Budget_Name, Budget_Date, Budget_Type_ID, Budget_Amount, Emp_ID) " +
                                   "VALUES (@name, @date, @budgetTypeId, @amount, @empId)";  // เพิ่ม Emp_ID
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@budgetTypeId", budgetTypeId);
                        cmd.Parameters.AddWithValue("@amount", amount);
                        cmd.Parameters.AddWithValue("@empId", CurrentUser.EmpID);  // ใช้ empID จาก CurrentUser

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("บันทึกข้อมูลสำเร็จ!");
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show("เกิดข้อผิดพลาดในการบันทึกข้อมูล");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void ClearForm()
        {
            txtBudName.Clear();
            txtBudAmount.Clear();
            if (cbType.Items.Count > 0) cbType.SelectedIndex = 0;
            dateTimeBudget.Value = DateTime.Now;
            txtBudName.Focus();
        }
    }
}
