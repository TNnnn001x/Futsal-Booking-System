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
    public partial class formRevenueData : Form
    {
        private string strConnectionString = "";
        private bool isEditing = false;

        public formRevenueData()
        {
            InitializeComponent();
            LoadConnectionString();
            SetupDataGridView();
            LoadAllRevenueData();
            dataGridViewRevenue.DefaultCellStyle.BackColor = Color.LightGray;
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

        private void LoadAllRevenueData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    string query = @"SELECT Revenue_ID, Budget_ID, Revenue_Type_ID, Revenue_Date, Revenue_Amount 
                                     FROM Revenue";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        SetupDataGridView();
                        dataGridViewRevenue.DataSource = dt;
                        dataGridViewRevenue.ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading revenue data: " + ex.Message);
            }
        }

        private void LoadRevenueData(DateTime selectedDate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    string query = @"SELECT Revenue_ID, Revenue_Type_ID, Budget_ID, Revenue_Date, Revenue_Amount 
                                     FROM Revenue 
                                     WHERE CAST(Revenue_Date AS DATE) = @selectedDate";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@selectedDate", selectedDate.Date);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            SetupDataGridView();
                            dataGridViewRevenue.DataSource = dt;
                            dataGridViewRevenue.ReadOnly = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading revenue data: " + ex.Message);
            }
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePickRev.Value;
            LoadRevenueData(selectedDate);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            isEditing = true;
            dataGridViewRevenue.ReadOnly = false;
            dataGridViewRevenue.DefaultCellStyle.BackColor = Color.LightGray;

            // ลูปทุกคอลัมน์แล้วล็อกคอลัมน์ที่ไม่ต้องการให้แก้ไข
            foreach (DataGridViewColumn column in dataGridViewRevenue.Columns)
            {
                if (column.Name == "Revenue_Type_ID")
                {
                    column.ReadOnly = false; // ให้แก้ไขได้
                    column.DefaultCellStyle.BackColor = Color.White;
                }
                else
                {
                    column.ReadOnly = true; // ล็อกไว้
                    column.DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dataGridViewRevenue.DefaultCellStyle.BackColor = Color.LightGray;
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    foreach (DataGridViewRow row in dataGridViewRevenue.Rows)
                    {
                        if (row.IsNewRow) continue;

                        int revenueId = Convert.ToInt32(row.Cells["Revenue_ID"].Value);
                        int revenueTypeId = Convert.ToInt32(row.Cells["Revenue_Type_ID"].Value);
                        int revenueBudgetID = Convert.ToInt32(row.Cells["Budget_ID"].Value);
                        DateTime revenueDate = Convert.ToDateTime(row.Cells["Revenue_Date"].Value);
                        decimal revenueAmount = Convert.ToDecimal(row.Cells["Revenue_Amount"].Value);

                        string query = @"UPDATE Revenue 
                                         SET Budget_ID = @budgetID, Revenue_Date = @revenueDate, 
                                             Revenue_Amount = @revenueAmount, Revenue_Type_ID = @revenueTypeId
                                         WHERE Revenue_ID = @revenueId";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@budgetID", revenueBudgetID);
                            cmd.Parameters.AddWithValue("@revenueDate", revenueDate);
                            cmd.Parameters.AddWithValue("@revenueAmount", revenueAmount);
                            cmd.Parameters.AddWithValue("@revenueId", revenueId);
                            cmd.Parameters.AddWithValue("@revenueTypeId", revenueTypeId);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("บันทึกข้อมูลสำเร็จ!");
                    dataGridViewRevenue.ReadOnly = true;
                    isEditing = false;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving revenue data: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewRevenue.SelectedRows.Count == 0)
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
                    foreach (DataGridViewRow row in dataGridViewRevenue.SelectedRows)
                    {
                        if (row.IsNewRow) continue;

                        int revenueId = Convert.ToInt32(row.Cells["Revenue_ID"].Value);
                        string query = "DELETE FROM Revenue WHERE Revenue_ID = @revenueId";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@revenueId", revenueId);
                            cmd.ExecuteNonQuery();
                        }

                        dataGridViewRevenue.Rows.Remove(row);
                    }
                }

                MessageBox.Show("ลบข้อมูลสำเร็จ!", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting revenue data: " + ex.Message);
            }
        }
        private void SetupDataGridView()
        {
            dataGridViewRevenue.AutoGenerateColumns = false; // ไม่ให้สร้างคอลัมน์อัตโนมัติ
            dataGridViewRevenue.ColumnHeadersVisible = true; // แสดงหัวตาราง

            // เคลียร์คอลัมน์เดิมก่อน (เผื่อโหลดข้อมูลใหม่)
            dataGridViewRevenue.Columns.Clear();

            // เพิ่มคอลัมน์ใน DataGridView ตามโครงสร้างฐานข้อมูล
            dataGridViewRevenue.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Revenue_ID",
                HeaderText = "Revenue ID",
                DataPropertyName = "Revenue_ID",
                ReadOnly = true // ไม่ให้แก้ไข
            });

            dataGridViewRevenue.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Budget_ID",
                HeaderText = "Budget ID",
                DataPropertyName = "Budget_ID"
            });

            // คอลัมน์ Budget_Type_ID ที่จะเปลี่ยนเป็น ComboBox
            DataGridViewComboBoxColumn revenueTypeColumn = new DataGridViewComboBoxColumn();
            revenueTypeColumn.HeaderText = "Revenue Type";
            revenueTypeColumn.Name = "Revenue_Type_ID";
            revenueTypeColumn.DataPropertyName = "Revenue_Type_ID";
            revenueTypeColumn.DisplayMember = "RevenueTypeName";  // ใช้ชื่อประเภทงบประมาณเพื่อแสดง
            revenueTypeColumn.ValueMember = "RevenueTypeID";  // ใช้ BudgetTypeID เป็นค่าจริง
            revenueTypeColumn.DefaultCellStyle.BackColor = Color.LightGray;


            // ดึงข้อมูลจากตาราง BudgetType
            List<RevenueType> revenueTypeList = GetRevenueTypes();
            foreach (var revenueType in revenueTypeList)
            {
                revenueTypeColumn.Items.Add(revenueType);
            }
            dataGridViewRevenue.Columns.Add(revenueTypeColumn);

            dataGridViewRevenue.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Revenue_Amount",
                HeaderText = "Amount",
                DataPropertyName = "Revenue_Amount"
            });

            dataGridViewRevenue.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Revenue_Date",
                HeaderText = "Date",
                DataPropertyName = "Revenue_Date",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });
            // ตั้งค่าให้คอลัมน์ทั้งหมดอยู่ตรงกลาง
            foreach (DataGridViewColumn col in dataGridViewRevenue.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void dataGridViewRevenue_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridViewRevenue.Columns[e.ColumnIndex].Name == "Revenue_Amount")
            {
                // ตรวจสอบค่าที่กรอกในคอลัมน์ Revenue_Amount
                if (!decimal.TryParse(e.FormattedValue.ToString(), out _))
                {
                    // ถ้าไม่ใช่ตัวเลข แสดงข้อความเตือน
                    MessageBox.Show("กรุณากรอกจำนวนเงินเป็นตัวเลขเท่านั้น", "ข้อมูลไม่ถูกต้อง", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true; // ยกเลิกการกรอกข้อมูล
                }
            }
        }
        private List<RevenueType> GetRevenueTypes()
        {
            List<RevenueType> revenueTypeList = new List<RevenueType>();
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    string query = "SELECT Revenue_Type_ID, Revenue_Type FROM RevenueType"; // ดึงข้อมูลจากตาราง BudgetType
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            RevenueType revenueType = new RevenueType
                            {
                                RevenueTypeID = Convert.ToInt32(reader["Revenue_Type_ID"]),
                                RevenueTypeName = reader["Revenue_Type"].ToString()
                            };
                           revenueTypeList.Add(revenueType); // เพิ่มข้อมูลในรายการ
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching budget types: " + ex.Message);
            }

            return revenueTypeList;
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

                // วาดหัวรายงาน
                g.DrawString("รายงานรายรับ", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, startX, startY);
                g.DrawString("วันที่พิมพ์: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"), font, Brushes.Black, startX, startY + 30);
                startY += 80;

                // กำหนดขนาดคอลัมน์ (เพิ่มช่องลำดับไว้หน้าสุด)
                int[] colWidths = { 50, 90, 100, 120, 120, 100 };
                string[] headers = { "ลำดับ", "รหัสรายรับ", "รหัสงบประมาณ", "ประเภท", "วันที่", "จำนวน" };

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

                int totalItems = 0;
                decimal totalAmount = 0;

                foreach (DataGridViewRow row in dataGridViewRevenue.Rows)
                {
                    if (row.IsNewRow) continue;
                    colX = startX;
                    totalItems++;

                    // ใส่ข้อมูลทีละคอลัมน์
                    string[] rowData = {
                totalItems.ToString(),
                row.Cells["Revenue_ID"].Value?.ToString() ?? "-",
                row.Cells["Budget_ID"].Value?.ToString() ?? "-",
                row.Cells["Revenue_Type_ID"].FormattedValue?.ToString() ?? "-",
                Convert.ToDateTime(row.Cells["Revenue_Date"].Value).ToString("dd/MM/yyyy"),
                Convert.ToDecimal(row.Cells["Revenue_Amount"].Value).ToString("N2")
            };

                    for (int i = 0; i < rowData.Length; i++)
                    {
                        // ตัดข้อความถ้ายาวเกินขนาดคอลัมน์
                        while (g.MeasureString(rowData[i], font).Width > colWidths[i])
                        {
                            rowData[i] = rowData[i].Substring(0, rowData[i].Length - 1);
                        }

                        g.DrawString(rowData[i], font, Brushes.Black, colX, startY);
                        colX += colWidths[i];
                    }

                    totalAmount += Convert.ToDecimal(row.Cells["Revenue_Amount"].Value);
                    startY += (int)fontHeight + 5;
                }

                // เส้นคั่น
                startY += 20;
                g.DrawLine(Pens.Black, startX, startY, startX + colWidths.Sum(), startY);
                startY += 10;

                // แสดงจำนวนรายการทั้งหมด และยอดรวม
                g.DrawString($"จำนวนรายการทั้งหมด: {totalItems} รายการ", boldFont, Brushes.Black, startX, startY);
                g.DrawString($"ยอดรวมรายรับทั้งหมด: {totalAmount:N2} บาท", boldFont, Brushes.Black, startX + 350, startY);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error printing: " + ex.Message);
            }
        }



    }
}
