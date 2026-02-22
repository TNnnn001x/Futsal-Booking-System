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
    public partial class formAllPayment : Form
    {
        private string strConnectionString = "";
        const string strFileName = "ConnectionString.ini";
        public formAllPayment()
        {
            InitializeComponent();
        }
        private void LoadPaymentData()
        {
            if (File.Exists(strFileName))
            {
                try
                {
                    strConnectionString = File.ReadAllText(strFileName);  // อ่านข้อมูลจากไฟล์ .ini
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ไม่สามารถอ่านไฟล์ ConnectionString.ini: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("ไม่พบไฟล์ ConnectionString.ini", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // สร้างคำสั่ง SQL
            string query = "SELECT Payment_ID, PO_ID, Payment_Method_ID, Payment_Date, Payment_Status, Payment_Amount FROM dbo.Payment";

            // เชื่อมต่อกับฐานข้อมูล
            using (SqlConnection connection = new SqlConnection(strConnectionString))
            {
                try
                {
                    // เปิดการเชื่อมต่อ
                    connection.Open();

                    // สร้าง SqlDataAdapter เพื่อนำข้อมูลจาก SQL มาเก็บใน DataTable
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();

                    // เติมข้อมูลจากฐานข้อมูลลงใน DataTable
                    dataAdapter.Fill(dataTable);

                    // ตั้งค่า DataGridView เพื่อแสดงข้อมูลจาก DataTable
                    dataGridViewPayment.DataSource = dataTable;

                    // ตรวจสอบว่ามีปุ่ม "Save" หรือไม่ หากไม่มีให้เพิ่ม
                    if (!dataGridViewPayment.Columns.Contains("Save"))
                    {
                        DataGridViewButtonColumn btnSaveColumn = new DataGridViewButtonColumn();
                        btnSaveColumn.HeaderText = "Save";
                        btnSaveColumn.Text = "Save";
                        btnSaveColumn.Name = "btnSave";
                        btnSaveColumn.UseColumnTextForButtonValue = true;
                        dataGridViewPayment.Columns.Add(btnSaveColumn);
                    }

                    // ตรวจสอบปุ่ม "Action" ว่ามีอยู่แล้วหรือไม่
                    if (!dataGridViewPayment.Columns.Contains("Action"))
                    {
                        DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
                        btnColumn.HeaderText = "Action";
                        btnColumn.Text = "View";
                        btnColumn.Name = "btnView";
                        btnColumn.UseColumnTextForButtonValue = true;
                        dataGridViewPayment.Columns.Add(btnColumn);
                    }

                    // เพิ่ม ComboBox สำหรับเลือกประเภทการใช้จ่าย

                    // โหลดประเภทการใช้จ่าย
                    LoadExpenseTypes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void formPayment_Load(object sender, EventArgs e)
        {
            // เรียกฟังก์ชันเพื่อโหลดข้อมูล
            LoadPaymentData();

            // เรียกฟังก์ชันเพื่อโหลดประเภทการใช้จ่าย
            LoadExpenseTypes();
            if(CurrentUser.EmpPosition == "Finance")
            {
               
            }
        }

        private void dataGridViewPayment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // ตรวจสอบว่าคลิกที่คอลัมน์ปุ่มหรือไม่
            if (e.RowIndex >= 0)
            {
                // ถ้าคลิกที่ปุ่ม "View"
                if (e.ColumnIndex == dataGridViewPayment.Columns["btnView"].Index)
                {
                    int paymentId = Convert.ToInt32(dataGridViewPayment.Rows[e.RowIndex].Cells["Payment_ID"].Value);
                    formPayment paymentForm = new formPayment(paymentId);
                    paymentForm.ShowDialog();
                }

                // ถ้าคลิกที่ปุ่ม "Save"
                if (e.ColumnIndex == dataGridViewPayment.Columns["btnSave"].Index)
                {
                    // ตรวจสอบตำแหน่งของพนักงาน
                    if (CurrentUser.EmpPosition == "Finance")
                    {
                        MessageBox.Show("พนักงานตำแหน่ง Finance ไม่สามารถบันทึกรายการได้", "ไม่อนุญาต", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // ยกเลิกการดำเนินการเมื่อเป็นตำแหน่ง Finance
                    }

                    int paymentId = Convert.ToInt32(dataGridViewPayment.Rows[e.RowIndex].Cells["Payment_ID"].Value);
                    int paymentMethodId = Convert.ToInt32(dataGridViewPayment.Rows[e.RowIndex].Cells["Payment_Method_ID"].Value);
                    decimal paymentAmount = Convert.ToDecimal(dataGridViewPayment.Rows[e.RowIndex].Cells["Payment_Amount"].Value);
                    int expenseTypeId = Convert.ToInt32(dataGridViewPayment.Rows[e.RowIndex].Cells["Expense_Type"].Value);  // เลือกประเภทการใช้จ่าย

                    // เรียกฟังก์ชันบันทึกรายจ่าย
                    SaveExpense(paymentId, expenseTypeId, paymentAmount);
                }
            }
        }

        private void SaveExpense(int paymentId, int expenseTypeId, decimal expenseAmount)
        {   
            string query = @"
    INSERT INTO dbo.Expense (Payment_ID, Expense_Type_ID, Expense_Amount, Expense_Date)
    VALUES (@Payment_ID, @Expense_Type_ID, @Expense_Amount, @Expense_Date)";

            using (SqlConnection connection = new SqlConnection(strConnectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Payment_ID", paymentId);
                    command.Parameters.AddWithValue("@Expense_Type_ID", expenseTypeId);
                    command.Parameters.AddWithValue("@Expense_Amount", expenseAmount);
                    command.Parameters.AddWithValue("@Expense_Date", DateTime.Now);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("บันทึกรายจ่ายสำเร็จ!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("ไม่สามารถบันทึกรายจ่ายได้", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        private void LoadExpenseTypes()
        {
            string query = "SELECT Expense_Type_ID, Expense_Type FROM Expense_Type";  // Query ที่ถูกต้องจากตาราง ExpenseType

            using (SqlConnection connection = new SqlConnection(strConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // เพิ่มคอลัมน์ ComboBox สำหรับเลือกประเภทการใช้จ่าย
                    DataGridViewComboBoxColumn expenseColumn = new DataGridViewComboBoxColumn();
                    expenseColumn.HeaderText = "Expense Type";
                    expenseColumn.Name = "Expense_Type";
                    expenseColumn.DataSource = dataTable;
                    expenseColumn.DisplayMember = "Expense_Type";
                    expenseColumn.ValueMember = "Expense_Type_ID";

                    // ถ้าไม่พบคอลัมน์ใน DataGridView ให้เพิ่ม
                    if (!dataGridViewPayment.Columns.Contains("Expense_Type"))
                    {
                        dataGridViewPayment.Columns.Add(expenseColumn);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                foreach (DataGridViewColumn col in dataGridViewPayment.Columns)
                {
                    if (col.Name != "Expense_Type")  // คอลัมน์ "StatusType" เท่านั้นที่สามารถแก้ไขได้
                    {
                        col.ReadOnly = true;  // ป้องกันการแก้ไขในคอลัมน์อื่นๆ
                    }
                }
            }
        }



    }
}
