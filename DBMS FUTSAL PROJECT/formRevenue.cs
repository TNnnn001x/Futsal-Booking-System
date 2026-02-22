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
using System.IO;

namespace DBMS_FUTSAL_PROJECT
{
    public partial class formRevenue : Form
    {
        private string strConnectionString = "";

        public formRevenue()
        {
            InitializeComponent();
            LoadConnectionString();
            LoadBudgets();
            LoadRevenueTypes();
            dateTimeRev.Format = DateTimePickerFormat.Short;
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

        private void LoadBudgets()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    string query = @"
                SELECT b.Budget_ID, b.Budget_Name, b.Budget_Amount 
                FROM Budget b
                LEFT JOIN Revenue r ON b.Budget_ID = r.Budget_ID
                WHERE r.Budget_ID IS NULL"; // ดึงเฉพาะ Budget_ID ที่ยังไม่มีใน Revenue

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvBudget.DataSource = dt;  // แสดงเฉพาะงบประมาณที่ยังไม่ถูกบันทึก
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading budgets: " + ex.Message);
            }
        }

        private void LoadRevenueTypes()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    string query = "SELECT Revenue_Type_ID, Revenue_Type FROM RevenueType";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        cbRevenueType.DataSource = dt;
                        cbRevenueType.DisplayMember = "Revenue_Type";
                        cbRevenueType.ValueMember = "Revenue_Type_ID";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading revenue types: " + ex.Message);
            }
        }

        private void dgvBudget_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBudget.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvBudget.SelectedRows[0];
                txtBudgetID.Text = selectedRow.Cells["Budget_ID"].Value.ToString();
                txtBudgetName.Text = selectedRow.Cells["Budget_Name"].Value.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string budgetId = txtBudgetID.Text;
            string revenueTypeId = cbRevenueType.SelectedValue.ToString();
            decimal revenueAmount;
            DateTime revenueDate = dateTimeRev.Value;

            // ตรวจสอบข้อมูลเบื้องต้น
            if (string.IsNullOrEmpty(budgetId))
            {
                MessageBox.Show("กรุณาเลือกงบประมาณ");
                return;
            }
            if (string.IsNullOrEmpty(revenueTypeId))
            {
                MessageBox.Show("กรุณาเลือกประเภทของรายรับ");
                return;
            }
            if (!decimal.TryParse(txtRevenueAmount.Text, out revenueAmount) || revenueAmount <= 0)
            {
                MessageBox.Show("กรุณากรอกจำนวนเงินที่ถูกต้อง");
                return;
            }

            // ดึงจำนวนงบประมาณจากฐานข้อมูลเพื่อตรวจสอบ
            decimal budgetAmount = GetBudgetAmount(budgetId);
            if (revenueAmount != budgetAmount)
            {
                MessageBox.Show($"จำนวนรายรับต้องเท่ากับงบประมาณที่กำหนด ({budgetAmount})");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Revenue (Budget_ID, Revenue_Type_ID, Revenue_Date, Revenue_Amount) " +
                                   "VALUES (@budgetId, @revenueTypeId, @revenueDate, @revenueAmount)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@budgetId", budgetId);
                        cmd.Parameters.AddWithValue("@revenueTypeId", revenueTypeId);
                        cmd.Parameters.AddWithValue("@revenueDate", revenueDate);
                        cmd.Parameters.AddWithValue("@revenueAmount", revenueAmount);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("บันทึกรายรับสำเร็จ!");
                            LoadBudgets(); // โหลดใหม่ ไม่ให้เห็นอันที่บันทึกไปแล้ว
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
            txtRevenueAmount.Clear();
            cbRevenueType.SelectedIndex = -1;
            dateTimeRev.Value = DateTime.Now;
        }

        private void formRevenue_Load(object sender, EventArgs e)
        {
            LoadBudgets();
            LoadRevenueTypes();
            txtBudgetID.ReadOnly = true;
            txtBudgetID.BackColor = Color.Red;

            txtBudgetName.ReadOnly = true;
            txtBudgetName.BackColor = Color.Red;
        }
        private decimal GetBudgetAmount(string budgetId)
        {
            decimal budgetAmount = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    string query = "SELECT Budget_Amount FROM Budget WHERE Budget_ID = @budgetId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@budgetId", budgetId);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            budgetAmount = Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving budget amount: " + ex.Message);
            }
            return budgetAmount;
        }
    }
}
