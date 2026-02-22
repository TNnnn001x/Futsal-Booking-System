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
    public partial class formExpense : Form
    {
        private string strConnectionString = "";
        private bool isEditing = false;

        public formExpense()
        {
            InitializeComponent();
            LoadConnectionString();
            SetupDataGridView();
            LoadAllExpenseData();
            dataGridViewExpense.DefaultCellStyle.BackColor = Color.LightGray;
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

        private void LoadAllExpenseData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    string query = @"SELECT Expense_ID, Payment_ID, Expense_Type_ID, Expense_Amount, Expense_Date 
                                 FROM Expense";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        SetupDataGridView();
                        dataGridViewExpense.DataSource = dt;
                        dataGridViewExpense.ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading expense data: " + ex.Message);
            }
        }

        private void LoadExpenseData(DateTime selectedDate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    string query = @"SELECT Expense_ID, Payment_ID, Expense_Type_ID, Expense_Amount, Expense_Date 
                                 FROM Expense 
                                 WHERE CAST(Expense_Date AS DATE) = @selectedDate";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@selectedDate", selectedDate.Date);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridViewExpense.DataSource = dt;
                        LoadExpenseTypes();  // เรียกใช้ LoadExpenseTypes เพื่อดึงข้อมูลประเภทการใช้จ่าย
                        dataGridViewExpense.ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading expense data: " + ex.Message);
            }
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker.Value;
            LoadExpenseData(selectedDate);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            isEditing = true;
            dataGridViewExpense.ReadOnly = false; // เปิดให้แก้ไขทั้งหมดก่อน
            dataGridViewExpense.DefaultCellStyle.BackColor = Color.White;
            btnSave.Enabled = true;
            btnEdit.Enabled = false;

            // กำหนดให้ทุกคอลัมน์เป็น ReadOnly = false ก่อน
            foreach (DataGridViewColumn col in dataGridViewExpense.Columns)
            {
                col.ReadOnly = false;
            }

            // กำหนด ExpenseID, PaymentID, Amount ห้ามแก้ไข
            dataGridViewExpense.Columns["Expense_ID"].ReadOnly = true;
            dataGridViewExpense.Columns["Payment_ID"].ReadOnly = true;
            dataGridViewExpense.Columns["Expense_Amount"].ReadOnly = true;
            dataGridViewExpense.Columns["Expense_Date"].ReadOnly= true;

            // เปลี่ยนสีพื้นหลังของแต่ละเซลล์ตามสถานะ ReadOnly
            foreach (DataGridViewRow row in dataGridViewExpense.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (dataGridViewExpense.Columns[cell.ColumnIndex].ReadOnly)
                    {
                        cell.Style.BackColor = Color.LightGray; // คอลัมน์ที่ห้ามแก้เป็นสีเทา
                    }
                    else
                    {
                        cell.Style.BackColor = Color.White; // คอลัมน์ที่แก้ได้เป็นสีขาว
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    foreach (DataGridViewRow row in dataGridViewExpense.Rows)
                    {
                        if (row.IsNewRow) continue;

                        int expenseId = Convert.ToInt32(row.Cells["Expense_ID"].Value);
                        int paymentId = Convert.ToInt32(row.Cells["Payment_ID"].Value);
                        int expenseTypeId = Convert.ToInt32(row.Cells["Expense_Type"].Value);
                        decimal expenseAmount = Convert.ToDecimal(row.Cells["Expense_Amount"].Value);
                        DateTime expenseDate = Convert.ToDateTime(row.Cells["Expense_Date"].Value);

                        string query = @"UPDATE Expense 
                                     SET Payment_ID = @paymentId, Expense_Type_ID = @expenseTypeId, 
                                         Expense_Amount = @expenseAmount, Expense_Date = @expenseDate
                                     WHERE Expense_ID = @expenseId";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@expenseId", expenseId);
                            cmd.Parameters.AddWithValue("@paymentId", paymentId);
                            cmd.Parameters.AddWithValue("@expenseTypeId", expenseTypeId);
                            cmd.Parameters.AddWithValue("@expenseAmount", expenseAmount);
                            cmd.Parameters.AddWithValue("@expenseDate", expenseDate);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("บันทึกข้อมูลรายจ่ายสำเร็จ!");
                    LoadExpenseData(dateTimePicker.Value);  // รีเฟรช DataGridView หลังบันทึกสำเร็จ
                    dataGridViewExpense.ReadOnly = true;
                    isEditing = false;
                    dataGridViewExpense.DefaultCellStyle.BackColor = Color.LightGray;
                    btnEdit.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving expense data: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewExpense.SelectedRows.Count == 0)
            {
                MessageBox.Show("กรุณาเลือกแถวที่ต้องการลบ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("คุณแน่ใจหรือไม่ว่าต้องการลบข้อมูลนี้?", "ยืนยันการลบ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    foreach (DataGridViewRow row in dataGridViewExpense.SelectedRows)
                    {
                        if (row.IsNewRow) continue;

                        int expenseId = Convert.ToInt32(row.Cells["Expense_ID"].Value);
                        string query = "DELETE FROM Expense WHERE Expense_ID = @expenseId";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@expenseId", expenseId);
                            cmd.ExecuteNonQuery();
                        }

                        dataGridViewExpense.Rows.Remove(row);
                    }
                }

                MessageBox.Show("ลบข้อมูลสำเร็จ!", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting expense data: " + ex.Message);
            }
        }

        private void SetupDataGridView()
        {
            dataGridViewExpense.AutoGenerateColumns = false; // ไม่ให้สร้างคอลัมน์อัตโนมัติ
            dataGridViewExpense.ColumnHeadersVisible = true; // แสดงหัวตาราง
            dataGridViewExpense.AllowUserToAddRows = false;

            // เคลียร์คอลัมน์เดิมก่อน (เผื่อโหลดข้อมูลใหม่)
            dataGridViewExpense.Columns.Clear();

            // เพิ่มคอลัมน์ใน DataGridView ตามโครงสร้างฐานข้อมูล
            dataGridViewExpense.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Expense_ID",
                HeaderText = "Expense ID",
                DataPropertyName = "Expense_ID",
                ReadOnly = true
            });

            dataGridViewExpense.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Payment_ID",
                HeaderText = "Payment ID",
                DataPropertyName = "Payment_ID"
            });

            // คอลัมน์ Expense_Type ที่เป็น ComboBox
            DataGridViewComboBoxColumn expenseTypeColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = "Expense Type",
                Name = "Expense_Type",
                DataPropertyName = "Expense_Type_ID",
                DisplayMember = "Expense_Type",
                ValueMember = "Expense_Type_ID"
            };
            dataGridViewExpense.Columns.Add(expenseTypeColumn);

            dataGridViewExpense.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Expense_Amount",
                HeaderText = "Amount",
                DataPropertyName = "Expense_Amount"
            });

            dataGridViewExpense.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Expense_Date",
                HeaderText = "Date",
                DataPropertyName = "Expense_Date",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });
        }

        private void LoadExpenseTypes()
        {
            string query = "SELECT Expense_Type_ID, Expense_Type FROM Expense_Type";  // Query สำหรับดึงข้อมูลประเภทการใช้จ่าย

            using (SqlConnection connection = new SqlConnection(strConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // นำข้อมูลประเภทการใช้จ่ายมาแสดงใน ComboBox ของ DataGridView
                    foreach (DataGridViewRow row in dataGridViewExpense.Rows)
                    {
                        if (row.IsNewRow) continue;

                        DataGridViewComboBoxCell comboCell = row.Cells["Expense_Type"] as DataGridViewComboBoxCell;
                        if (comboCell != null)
                        {
                            comboCell.DataSource = dataTable;
                            comboCell.DisplayMember = "Expense_Type";
                            comboCell.ValueMember = "Expense_Type_ID";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void formExpense_Load(object sender, EventArgs e)
        {
            LoadExpenseTypes();
        }
    }

}
