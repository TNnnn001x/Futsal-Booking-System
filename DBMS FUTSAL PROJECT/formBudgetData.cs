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
using System.Drawing.Printing;

namespace DBMS_FUTSAL_PROJECT
{
    public partial class formBudgetData : Form
    {
        private string strConnectionString = "";  // ประกาศตัวแปรสำหรับ Connection String
        private bool isEditing = false;
        public formBudgetData()
        {
            InitializeComponent();
            LoadConnectionString();
            SetupDataGridView();
            LoadAllBudgetData();
            dataGridViewBudget.DefaultCellStyle.BackColor = Color.LightGray;
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
        private void LoadAllBudgetData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    string query = "SELECT Budget_ID, Budget_Type_ID, Emp_ID , Budget_Name, Budget_Date, Budget_Amount FROM Budget";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        SetupDataGridView();
                        dataGridViewBudget.DataSource = dt;

                        // ปิดการแก้ไขใน DataGridView โดยเริ่มต้น
                        dataGridViewBudget.ReadOnly = true;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading budget data: " + ex.Message);
            }
        }


        // ฟังก์ชันเพื่อโหลดข้อมูลทั้งหมดใน DataGridView ตามวันที่
        private void LoadBudgetData(DateTime selectedDate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    string query = "SELECT Budget_ID, Budget_Type_ID, Emp_ID , Budget_Name, Budget_Date, Budget_Amount " +
                                   "FROM Budget WHERE CAST(Budget_Date AS DATE) = @selectedDate";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@selectedDate", selectedDate.Date); // กรองข้อมูลตามวันที่

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        SetupDataGridView();
                        dataGridViewBudget.DataSource = dt;

                        // ปิดการแก้ไขใน DataGridView โดยเริ่มต้น
                        dataGridViewBudget.ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading budget data: " + ex.Message);
            }
        }


        private void btnLoadData_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePickBud.Value; // เลือกวันที่จาก DateTimePicker
            LoadBudgetData(selectedDate);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            isEditing = true;
            dataGridViewBudget.ReadOnly = false;
            dataGridViewBudget.DefaultCellStyle.BackColor = Color.White;
            foreach (DataGridViewColumn col in dataGridViewBudget.Columns)
            {
                if (col.Name == "Budget_ID" || col.Name == "Emp_ID" || col.Name == "Budget_Amount" || col.Name == "Budget_Date")
                {
                    col.ReadOnly = true;  // ล็อกไม่ให้แก้ไข
                    col.DefaultCellStyle.BackColor = Color.LightGray;  // เปลี่ยนสีพื้นหลังให้ดูว่าแก้ไขไม่ได้
                }
                else
                {
                    col.ReadOnly = false; // อนุญาตให้แก้ไข
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
                    foreach (DataGridViewRow row in dataGridViewBudget.Rows)
                    {
                        if (row.IsNewRow) continue; // ข้ามแถวใหม่ที่ยังไม่ได้กรอกข้อมูล
                        int budgetId = Convert.ToInt32(row.Cells["Budget_ID"].Value);
                        int budgettypeid = Convert.ToInt32(row.Cells["Budget_Type_ID"].Value);  // รับค่า Budget_Type_ID จาก ComboBox
                        int empid = Convert.ToInt32(row.Cells["Emp_ID"].Value);
                        string budgetName = row.Cells["Budget_Name"].Value.ToString();
                        DateTime budgetDate = Convert.ToDateTime(row.Cells["Budget_Date"].Value);
                        decimal budgetAmount = Convert.ToDecimal(row.Cells["Budget_Amount"].Value);

                        string query = "UPDATE Budget SET Budget_Name = @budgetName, Budget_Date = @budgetDate, Budget_Amount = @budgetAmount, Budget_Type_Id = @budgettypeid, Emp_ID = @empid " +
                                       "WHERE Budget_ID = @budgetId";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@budgetName", budgetName);
                            cmd.Parameters.AddWithValue("@budgetDate", budgetDate);
                            cmd.Parameters.AddWithValue("@budgetAmount", budgetAmount);
                            cmd.Parameters.AddWithValue("@budgetId", budgetId);
                            cmd.Parameters.AddWithValue("@empid", empid);
                            cmd.Parameters.AddWithValue("@budgettypeid", budgettypeid); // ส่งค่า Budget_Type_ID

                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("บันทึกข้อมูลสำเร็จ!");
                    dataGridViewBudget.ReadOnly = true;
                    isEditing = false; // ปิดโหมดแก้ไข
                    dataGridViewBudget.ReadOnly = true; // ปิดการแก้ไขหลังจากบันทึก
                    dataGridViewBudget.DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving budget data: " + ex.Message);
            }
        }
        private void SetupDataGridView()
        {
            dataGridViewBudget.AutoGenerateColumns = false; // ไม่ให้สร้างคอลัมน์อัตโนมัติ
            dataGridViewBudget.ColumnHeadersVisible = true; // แสดงหัวตาราง
            dataGridViewBudget.AllowUserToAddRows = false;


            // เคลียร์คอลัมน์เดิมก่อน (เผื่อโหลดข้อมูลใหม่)
            dataGridViewBudget.Columns.Clear();

            // เพิ่มคอลัมน์ใน DataGridView ตามโครงสร้างฐานข้อมูล
            dataGridViewBudget.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Budget_ID",
                HeaderText = "Budget ID",
                DataPropertyName = "Budget_ID",
                ReadOnly = true // ไม่ให้แก้ไข
            });

            // คอลัมน์ Budget_Type_ID ที่จะเปลี่ยนเป็น ComboBox
            DataGridViewComboBoxColumn budgetTypeColumn = new DataGridViewComboBoxColumn();
            budgetTypeColumn.HeaderText = "Budget Type";
            budgetTypeColumn.Name = "Budget_Type_ID";
            budgetTypeColumn.DataPropertyName = "Budget_Type_ID";
            budgetTypeColumn.DisplayMember = "BudgetTypeName";  // ใช้ชื่อประเภทงบประมาณเพื่อแสดง
            budgetTypeColumn.ValueMember = "BudgetTypeID";  // ใช้ BudgetTypeID เป็นค่าจริง
            budgetTypeColumn.DefaultCellStyle.BackColor = Color.LightGray;


            // ดึงข้อมูลจากตาราง BudgetType
            List<BudgetType> budgetTypeList = GetBudgetTypes();
            foreach (var budgetType in budgetTypeList)
            {
                budgetTypeColumn.Items.Add(budgetType);
            }
            dataGridViewBudget.Columns.Add(budgetTypeColumn);

            dataGridViewBudget.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Emp_ID",
                HeaderText = "Employee ID",
                DataPropertyName = "Emp_ID"
            });

            dataGridViewBudget.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Budget_Name",
                HeaderText = "Budget Name",
                DataPropertyName = "Budget_Name"
            });

            dataGridViewBudget.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Budget_Date",
                HeaderText = "Date",
                DataPropertyName = "Budget_Date",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dataGridViewBudget.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Budget_Amount",
                HeaderText = "Amount",
                DataPropertyName = "Budget_Amount",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } // แสดงเลขทศนิยม 2 ตำแหน่ง
            });

            // ตั้งค่าให้คอลัมน์ทั้งหมดอยู่ตรงกลาง
            foreach (DataGridViewColumn col in dataGridViewBudget.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

        }
        private List<BudgetType> GetBudgetTypes()
        {
            List<BudgetType> budgetTypeList = new List<BudgetType>();
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    string query = "SELECT Budget_Type_ID, Budget_Type FROM BudgetType"; // ดึงข้อมูลจากตาราง BudgetType
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            BudgetType budgetType = new BudgetType
                            {
                                BudgetTypeID = Convert.ToInt32(reader["Budget_Type_ID"]),
                                BudgetTypeName = reader["Budget_Type"].ToString()
                            };
                            budgetTypeList.Add(budgetType); // เพิ่มข้อมูลในรายการ
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching budget types: " + ex.Message);
            }

            return budgetTypeList;
        }


        private void dataGridViewBudget_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!isEditing) // ถ้ายังไม่ได้กดปุ่ม Edit
            {
                MessageBox.Show("กรุณากดปุ่ม 'Edit' ก่อนแก้ไขข้อมูล", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true; // ยกเลิกการแก้ไข
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewBudget.SelectedRows.Count == 0)
            {
                MessageBox.Show("กรุณาเลือกแถวที่ต้องการลบ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("คุณแน่ใจหรือไม่ว่าต้องการลบข้อมูลนี้?", "ยืนยันการลบ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    foreach (DataGridViewRow row in dataGridViewBudget.SelectedRows)
                    {
                        if (row.IsNewRow) continue;

                        int budgetId = Convert.ToInt32(row.Cells["Budget_ID"].Value);

                        string query = "DELETE FROM Budget WHERE Budget_ID = @budgetId";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@budgetId", budgetId);
                            cmd.ExecuteNonQuery();
                        }

                        dataGridViewBudget.Rows.Remove(row); // ลบออกจาก DataGridView
                    }
                }

                MessageBox.Show("ลบข้อมูลสำเร็จ!", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting budget data: " + ex.Message);
            }
        }

        private void btnPrintAll_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);

            PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            previewDialog.Document = printDocument;
            previewDialog.ShowDialog();
        }
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                Graphics g = e.Graphics;
                Font font = new Font("Arial", 10);
                Font boldFont = new Font("Arial", 10, FontStyle.Bold);
                float fontHeight = font.GetHeight();
                int startX = 50;
                int startY = 50;

                // วาดหัวตาราง
                g.DrawString("รายงานงบประมาณ", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, startX, startY);
                g.DrawString("วันที่พิมพ์: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"), font, Brushes.Black, startX, startY + 30);
                startY += 80;

                // กำหนดขนาดคอลัมน์ (เพิ่มช่องลำดับไว้หน้าสุด)
                int[] colWidths = { 50, 80, 100, 100, 150, 100, 100 };
                string[] headers = { "ลำดับ", "รหัส", "ประเภท", "พนักงาน", "ชื่อ", "วันที่", "จำนวน" };

                // วาดชื่อคอลัมน์
                int colX = startX;
                for (int i = 0; i < headers.Length; i++)
                {
                    g.DrawString(headers[i], boldFont, Brushes.Black, colX, startY);
                    colX += colWidths[i];
                }
                startY += (int)fontHeight + 10;
                g.DrawLine(Pens.Black, startX, startY, startX + colWidths.Sum(), startY);
                startY += 5;

                // โหลด BudgetType ล่วงหน้า
                List<BudgetType> budgetTypes = GetBudgetTypes();

                int totalItems = 0;
                decimal totalAmount = 0;

                foreach (DataGridViewRow row in dataGridViewBudget.Rows)
                {
                    if (row.IsNewRow) continue;
                    colX = startX;
                    totalItems++;

                    // เพิ่มคอลัมน์ "ลำดับ"
                    g.DrawString(totalItems.ToString(), font, Brushes.Black, colX, startY);
                    colX += colWidths[0];

                    for (int i = 1; i < headers.Length; i++)
                    {
                        string cellValue = row.Cells[i - 1].Value?.ToString() ?? "-"; // ปรับ index เพราะเพิ่มลำดับเข้ามา

                        // แปลง Budget_Type_ID เป็น BudgetTypeName
                        if (headers[i] == "ประเภท" && int.TryParse(cellValue, out int budgetTypeId))
                        {
                            BudgetType matchingType = budgetTypes.FirstOrDefault(bt => bt.BudgetTypeID == budgetTypeId);
                            if (matchingType != null)
                            {
                                cellValue = matchingType.BudgetTypeName;
                            }
                        }

                        // แปลงวันที่เป็น "dd/MM/yyyy"
                        if (headers[i] == "วันที่" && DateTime.TryParse(cellValue, out DateTime dateValue))
                        {
                            cellValue = dateValue.ToString("dd/MM/yyyy");
                        }

                        // คำนวณผลรวมของ "จำนวน"
                        if (headers[i] == "จำนวน" && decimal.TryParse(cellValue, out decimal budgetAmount))
                        {
                            totalAmount += budgetAmount;
                        }

                        // ตัดข้อความถ้ายาวเกินขนาดคอลัมน์
                        while (g.MeasureString(cellValue, font).Width > colWidths[i])
                        {
                            cellValue = cellValue.Substring(0, cellValue.Length - 1);
                        }

                        g.DrawString(cellValue, font, Brushes.Black, colX, startY);
                        colX += colWidths[i];
                    }
                    startY += (int)fontHeight + 5;
                }

                // เส้นคั่น
                startY += 20;
                g.DrawLine(Pens.Black, startX, startY, startX + colWidths.Sum(), startY);
                startY += 10;

                // แสดงจำนวนรายการทั้งหมด และผลรวมจำนวนเงิน
                g.DrawString($"จำนวนรายการทั้งหมด: {totalItems} รายการ", boldFont, Brushes.Black, startX, startY);
                g.DrawString($"รวมทั้งสิ้น: {totalAmount:N2} บาท", boldFont, Brushes.Black, startX + 350, startY);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error printing: " + ex.Message);
            }
        }


    }
}
