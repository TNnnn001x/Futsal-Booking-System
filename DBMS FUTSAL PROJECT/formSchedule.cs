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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing.Printing;

namespace DBMS_FUTSAL_PROJECT
{
    public partial class formSchedule : Form
    {
        const string strFileName = "ConnectionString.ini";
        private string strConnectionString = "";  // ประกาศตัวแปรสำหรับ Connection String

        SqlConnection scheduleConnection;
        SqlCommand scheduleCommand;
        SqlDataAdapter scheduleAdapter;
        DataTable scheduleTable;
        CurrencyManager scheduleManager;
        BindingSource bindingSource = new BindingSource(); // เพิ่ม BindingSource
        public formSchedule()
        {
            InitializeComponent();
        }

        private void formSchedule_Load(object sender, EventArgs e)
        {
            LoadScheduleData(); // โหลดข้อมูลทั้งหมดเมื่อฟอร์มโหลด
            dtpDatePicker.Value = DateTime.Today; // ตั้งค่า DateTimePicker ให้เลือกวันที่ปัจจุบัน
            if (CurrentUser.EmpPosition == "Futsal court supervisor")
            {
                buttonAddSchedule.Enabled = false;
                buttonDelete.Enabled = false;
            }
        }
        private void SetupDataGridView()
        { // ตรวจสอบว่าคอลัมน์ "StatusType" ถูกเพิ่มไว้แล้วหรือไม่
            if (!dataGridViewSchedule.Columns.Contains("StatusType"))
            {
                // เพิ่มคอลัมน์ ComboBox สำหรับเลือกสถานะใน DataGridView
                DataGridViewComboBoxColumn statusColumn = new DataGridViewComboBoxColumn();
                statusColumn.HeaderText = "Status Type";
                statusColumn.Name = "StatusType";
                statusColumn.DataPropertyName = "Status_ID";  // ผูกกับ Status_ID ใน DataTable

                // ดึงข้อมูลจากตาราง Status
                List<Status> statusList = GetStatusList(); // ฟังก์ชันที่ดึงค่าจากตาราง Status

                // ตั้งค่า DisplayMember และ ValueMember
                statusColumn.DisplayMember = "StatusType";  // ใช้ชื่อสถานะเพื่อแสดง
                statusColumn.ValueMember = "StatusID";  // ใช้ StatusID เป็นค่าจริง

                // เพิ่มสถานะลงใน ComboBox
                foreach (var status in statusList)
                {
                    statusColumn.Items.Add(status);
                }

                // เพิ่มคอลัมน์สถานะเข้าไปใน DataGridView
                dataGridViewSchedule.Columns.Add(statusColumn);
            }
        }
        // ฟังก์ชันเพื่อดึงข้อมูลจากตาราง Status
        private List<Status> GetStatusList()
        {
            List<Status> statusList = new List<Status>();
            try
            {
                using (SqlConnection scheduleConnection = new SqlConnection(strConnectionString))
                {
                    scheduleConnection.Open();
                    string query = "SELECT Status_ID, Status FROM Status"; // ดึงข้อมูลจากตาราง Status
                    SqlCommand cmd = new SqlCommand(query, scheduleConnection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Status status = new Status
                        {
                            StatusID = Convert.ToInt32(reader["Status_ID"]),
                            StatusType = reader["Status"].ToString()
                        };
                        statusList.Add(status); // เพิ่มสถานะในรายการ
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching status types: " + ex.Message);
            }

            return statusList;
        }
        private void buttonAddSchedule_Click(object sender, EventArgs e)
        {
            DateTime startDate = dtpStartDate.Value; // วันที่เริ่มต้น
            DateTime endDate = dtpEndDate.Value; // วันที่สิ้นสุด
            TimeSpan daySpan = endDate - startDate; // คำนวณช่วงเวลาระหว่างวันที่เริ่มต้นและสิ้นสุด

            try
            {
                // ใช้ using block เพื่อจัดการการเชื่อมต่อ SQL และคำสั่ง SQL
                using (SqlConnection scheduleConnection = new SqlConnection(strConnectionString))
                {
                    scheduleConnection.Open(); // เปิดการเชื่อมต่อกับฐานข้อมูล

                    // ลูปเพิ่มตารางเวลาทีละวันในช่วงเวลาที่เลือก
                    for (int i = 0; i <= daySpan.Days; i++) // ใช้ลูปเพื่อเพิ่มตารางเวลาในแต่ละวัน
                    {
                        DateTime currentDate = startDate.AddDays(i); // คำนวณวันที่ในแต่ละวัน

                        // ตั้งเวลาเริ่มต้นเป็น 10:00 AM และเวลาสิ้นสุดเป็น 18:00 PM
                        DateTime currentStartTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 8, 0, 0); // เวลาเริ่มต้น 10:00 AM
                        DateTime currentEndTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 19, 0, 0); // เวลาสิ้นสุด 18:00 PM

                        // ลูปเพิ่มเวลา 1 ชั่วโมงจนถึงเวลาสิ้นสุด (18:00)
                        while (currentStartTime < currentEndTime) // เช็คว่าเวลาเริ่มต้นน้อยกว่า 18:00
                        {
                            int statusID = 1;  // ใช้สถานะ 1 ตามปกติ

                            // ตรวจสอบช่วงเวลา 06:00 - 07:00 และ 18:00 - 19:00 เพื่อกำหนดสถานะเป็น 4
                            if ((currentStartTime.Hour == 8 && currentStartTime.Minute == 0) || (currentStartTime.Hour == 18 && currentStartTime.Minute == 0))
                            {
                                statusID = 4; // กำหนดสถานะเป็น 4
                            }

                            // สร้างคำสั่ง SQL สำหรับการเพิ่มตารางเวลา
                            string query = "INSERT INTO Schedule (Schedule_Date, Schedule_StartTime, Schedule_EndTime, Emp_ID, Status_ID) " +
                                           "VALUES (@ScheduleDate, @StartTime, @EndTime, @EmpID, @StatusID)";

                            using (SqlCommand scheduleCommand = new SqlCommand(query, scheduleConnection))
                            {
                                scheduleCommand.Parameters.AddWithValue("@ScheduleDate", currentDate);  // วันที่จอง
                                scheduleCommand.Parameters.AddWithValue("@StartTime", currentStartTime);  // เวลาเริ่มต้น
                                scheduleCommand.Parameters.AddWithValue("@EndTime", currentStartTime.AddHours(1));  // เวลาสิ้นสุดเพิ่ม 1 ชั่วโมง
                                scheduleCommand.Parameters.AddWithValue("@EmpID", CurrentUser.EmpID);  // บันทึก EmpID จาก currentUser
                                scheduleCommand.Parameters.AddWithValue("@StatusID", statusID);  // ใช้ StatusID ตามที่กำหนด
                                scheduleCommand.ExecuteNonQuery();  // ทำการบันทึกข้อมูลในแต่ละช่วงเวลา
                            }

                            // เพิ่มเวลา 1 ชั่วโมง
                            currentStartTime = currentStartTime.AddHours(1);  // เพิ่ม 1 ชั่วโมง
                        }
                    }
                    LoadScheduleData();
                    MessageBox.Show("ตารางเวลาได้รับการบันทึกทั้งหมด");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadScheduleData()
        {
            try
            {
                if (File.Exists(strFileName))
                    strConnectionString = File.ReadAllText(strFileName, Encoding.GetEncoding("Windows-874"));

                if (string.IsNullOrEmpty(strConnectionString))
                {
                    MessageBox.Show("ไม่สามารถอ่านค่า Connection String ได้จากไฟล์", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                using (SqlConnection scheduleConnection = new SqlConnection(strConnectionString))
                {
                    scheduleConnection.Open();

                    // สร้างคำสั่ง SQL เพื่อดึงข้อมูลจากตาราง Schedule
                    string query = "SELECT * FROM Schedule";
                    SqlDataAdapter scheduleAdapter = new SqlDataAdapter(query, scheduleConnection);
                    DataTable scheduleTable = new DataTable();
                    scheduleAdapter.Fill(scheduleTable);

                    // ผูกข้อมูลจาก DataTable กับ DataGridView
                    dataGridViewSchedule.DataSource = scheduleTable;
                    // ผูกข้อมูล Status_Type ลงใน ComboBox ของ DataGridView
                    SetupDataGridView(); // ฟังก์ชันที่ตั้งค่า ComboBox ใน DataGridView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการโหลดข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadScheduleData(DateTime? selectedDate = null)
        {
            try
            {
                if (File.Exists(strFileName))
                    strConnectionString = File.ReadAllText(strFileName, Encoding.GetEncoding("Windows-874"));

                if (string.IsNullOrEmpty(strConnectionString))
                {
                    MessageBox.Show("ไม่สามารถอ่านค่า Connection String ได้จากไฟล์", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SqlConnection scheduleConnection = new SqlConnection(strConnectionString))
                {
                    scheduleConnection.Open();

                    string query = @"
                SELECT * 
                FROM Schedule ";

                    if (selectedDate.HasValue)
                    {
                        query += "WHERE CAST(Schedule_Date AS DATE) = @SelectedDate"; // กรองข้อมูลตามวันที่ที่เลือก
                    }

                    SqlDataAdapter scheduleAdapter = new SqlDataAdapter(query, scheduleConnection);

                    if (selectedDate.HasValue)
                    {
                        scheduleAdapter.SelectCommand.Parameters.AddWithValue("@SelectedDate", selectedDate.Value.Date); // ส่งวันที่ที่เลือกไปในพารามิเตอร์
                    }

                    DataTable scheduleTable = new DataTable();
                    scheduleAdapter.Fill(scheduleTable);

                    // ผูกข้อมูลจาก DataTable กับ DataGridView
                    dataGridViewSchedule.DataSource = scheduleTable;
                    SetupDataGridView(); // ฟังก์ชันที่ตั้งค่า ComboBox ใน DataGridView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการโหลดข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                // ใช้ using block เพื่อจัดการการเชื่อมต่อ SQL และคำสั่ง SQL
                using (SqlConnection scheduleConnection = new SqlConnection(strConnectionString))
                {
                    scheduleConnection.Open();

                    // สร้างคำสั่ง SQL สำหรับการอัปเดตข้อมูล
                    SqlCommand updateCommand = new SqlCommand("UPDATE Schedule SET Schedule_StartTime = @StartTime, Schedule_EndTime = @EndTime, EmpID = @EmpID, Status_ID = @StatusID WHERE Schedule_ID = @ScheduleID", scheduleConnection);
                    // ใช้ DataAdapter สำหรับการอัปเดตข้อมูล
                    SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Schedule", scheduleConnection);
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                    // อัปเดตข้อมูลที่ถูกเลือกใน DataGridView
                    foreach (DataGridViewRow row in dataGridViewSchedule.SelectedRows)
                    {
                        if (row.Cells["Schedule_ID"].Value != null)
                        {
                            int scheduleID = Convert.ToInt32(row.Cells["Schedule_ID"].Value);
                            DateTime startTime = Convert.ToDateTime(row.Cells["Schedule_StartTime"].Value);
                            DateTime endTime = Convert.ToDateTime(row.Cells["Schedule_EndTime"].Value);
                            int statusID = ((Status)row.Cells["StatusType"].Value).StatusID;  // ดึงสถานะจาก ComboBox
                            int empID = CurrentUser.EmpID;

                            // เพิ่มพารามิเตอร์เพื่ออัปเดตข้อมูลในฐานข้อมูล
                            updateCommand.Parameters.Clear();
                            updateCommand.Parameters.AddWithValue("@ScheduleID", scheduleID);
                            updateCommand.Parameters.AddWithValue("@StartTime", startTime);
                            updateCommand.Parameters.AddWithValue("@EndTime", endTime);
                            updateCommand.Parameters.AddWithValue("@EmpID", CurrentUser.EmpID);
                            updateCommand.Parameters.AddWithValue("@StatusID", statusID);

                            // ทำการอัปเดตข้อมูล
                            updateCommand.ExecuteNonQuery();  // ทำการบันทึกข้อมูลที่แก้ไข
                        }
                    }

                    // ใช้ DataAdapter.Update() เพื่ออัปเดตการเปลี่ยนแปลงกลับไปที่ฐานข้อมูล
                    dataAdapter.Update((DataTable)dataGridViewSchedule.DataSource);
                    // รีเฟรช DataGridView หลังจากบันทึก
                    LoadScheduleData(dtpDatePicker.Value); // ฟังก์ชันนี้จะโหลดข้อมูลจากฐานข้อมูลใหม่เพื่อแสดงข้อมูลที่อัปเดตแล้ว

                    MessageBox.Show("ข้อมูลได้รับการบันทึกเรียบร้อยแล้ว");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการบันทึกข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่าได้เลือกแถวใน DataGridView หรือไม่
            if (dataGridViewSchedule.SelectedRows.Count > 0)
            {
                try
                {
                    // ถ้ามีการเลือกแถว
                    foreach (DataGridViewRow row in dataGridViewSchedule.SelectedRows)
                    {
                        if (row.Cells["Schedule_ID"].Value != null)
                        {
                            // ดึงรหัสของตารางเวลา (Schedule_ID) ที่ต้องการลบ
                            int scheduleID = Convert.ToInt32(row.Cells["Schedule_ID"].Value);

                            // สร้างคำสั่ง SQL สำหรับการลบข้อมูลจากฐานข้อมูล
                            using (SqlConnection scheduleConnection = new SqlConnection(strConnectionString))
                            {
                                scheduleConnection.Open();

                                string query = "DELETE FROM Schedule WHERE Schedule_ID = @ScheduleID";
                                SqlCommand deleteCommand = new SqlCommand(query, scheduleConnection);
                                deleteCommand.Parameters.AddWithValue("@ScheduleID", scheduleID);

                                // ทำการลบข้อมูล
                                deleteCommand.ExecuteNonQuery();

                                // รีเฟรช DataGridView หลังจากลบข้อมูล
                                LoadScheduleData();

                                MessageBox.Show("ข้อมูลได้รับการลบเรียบร้อยแล้ว");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการลบข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("กรุณาเลือกแถวที่ต้องการลบ", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void PrintFilteredData(string statusType)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();

                    // ตรวจสอบว่ามีคอลัมน์ Emp_FName และ Emp_LName หรือไม่
                    string checkColumnQuery = @"
        SELECT COLUMN_NAME 
        FROM INFORMATION_SCHEMA.COLUMNS 
        WHERE TABLE_NAME = 'Employee'";

                    SqlCommand checkCmd = new SqlCommand(checkColumnQuery, conn);
                    SqlDataReader reader = checkCmd.ExecuteReader();

                    bool hasEmpFName = false;
                    bool hasEmpLName = false;

                    List<string> columns = new List<string>();
                    while (reader.Read())
                    {
                        string columnName = reader["COLUMN_NAME"].ToString();
                        columns.Add(columnName);

                        if (columnName.Equals("Emp_FName", StringComparison.OrdinalIgnoreCase))
                        {
                            hasEmpFName = true;
                        }
                        else if (columnName.Equals("Emp_LName", StringComparison.OrdinalIgnoreCase))
                        {
                            hasEmpLName = true;
                        }
                    }
                    reader.Close();

                    // สร้างคำสั่ง SQL เพื่อดึงข้อมูลตามสถานะ
                    string query;

                    if (hasEmpFName && hasEmpLName)
                    {
                        // ถ้ามีทั้งชื่อและนามสกุล ให้รวมเข้าด้วยกัน
                        query = @"SELECT s.Schedule_ID, s.Schedule_Date, s.Schedule_StartTime, s.Schedule_EndTime, 
                  CONCAT(e.Emp_FName, ' ', e.Emp_LName) AS EmployeeName, st.Status 
                  FROM Schedule s 
                  LEFT JOIN Employee e ON s.Emp_ID = e.Emp_ID 
                  INNER JOIN Status st ON s.Status_ID = st.Status_ID 
                  WHERE st.Status = @StatusType
                  ORDER BY s.Schedule_Date, s.Schedule_StartTime";
                    }
                    else if (columns.Any(c => c.Equals("Emp_Name", StringComparison.OrdinalIgnoreCase)))
                    {
                        // ถ้ามี Emp_Name
                        query = @"SELECT s.Schedule_ID, s.Schedule_Date, s.Schedule_StartTime, s.Schedule_EndTime, 
                  e.Emp_Name AS EmployeeName, st.Status 
                  FROM Schedule s 
                  LEFT JOIN Employee e ON s.Emp_ID = e.Emp_ID 
                  INNER JOIN Status st ON s.Status_ID = st.Status_ID 
                  WHERE st.Status = @StatusType
                  ORDER BY s.Schedule_Date, s.Schedule_StartTime";
                    }
                    else
                    {
                        // ถ้าไม่มีคอลัมน์ชื่อพนักงาน ให้ใช้แค่ Emp_ID
                        query = @"SELECT s.Schedule_ID, s.Schedule_Date, s.Schedule_StartTime, s.Schedule_EndTime, 
                  CAST(s.Emp_ID AS NVARCHAR(50)) AS EmployeeName, st.Status 
                  FROM Schedule s 
                  INNER JOIN Status st ON s.Status_ID = st.Status_ID 
                  WHERE st.Status = @StatusType
                  ORDER BY s.Schedule_Date, s.Schedule_StartTime";
                    }

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StatusType", statusType);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable filteredData = new DataTable();
                    adapter.Fill(filteredData);

                    if (filteredData.Rows.Count > 0)
                    {
                        PrintData(filteredData, statusType);  // Call the print function if data is found
                    }
                    else
                    {
                        MessageBox.Show($"ไม่พบข้อมูลสถานะ '{statusType}'", "ข้อมูลไม่พอ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการดึงข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // ฟังก์ชันสำหรับปริ้นข้อมูล
        private void PrintData(DataTable data, string reportTitle)
        {
            PrintDialog printDialog = new PrintDialog();
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            PrintDocument printDocument = new PrintDocument();
            printPreviewDialog.Document = printDocument;

            printDocument.PrintPage += (sender, e) =>
            {
                // กำหนดฟอนต์และขนาด
                Font titleFont = new Font("Arial", 18, FontStyle.Bold);
                Font headerFont = new Font("Arial", 12, FontStyle.Bold);
                Font contentFont = new Font("Arial", 10);

                // กำหนดตำแหน่งและขนาด
                int pageWidth = e.PageBounds.Width;
                int margin = 50;
                int yPos = margin;

                // วาดหัวรายงาน
                string title = $"รายงานตารางเวลา - สถานะ: {reportTitle}";
                SizeF titleSize = e.Graphics.MeasureString(title, titleFont);
                e.Graphics.DrawString(title, titleFont, Brushes.Black, (pageWidth - titleSize.Width) / 2, yPos);

                yPos += (int)titleSize.Height + 20;

                // วาดวันที่พิมพ์
                string dateStr = $"วันที่พิมพ์: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}";
                e.Graphics.DrawString(dateStr, contentFont, Brushes.Black, margin, yPos);

                yPos += 30;

                // กำหนดความกว้างของคอลัมน์
                int[] colWidths = { 80, 150, 150, 150, 150 };
                string[] headers = { "ลำดับ", "วันที่", "เวลาเริ่ม", "เวลาสิ้นสุด", "พนักงาน" };

                // วาดหัวตาราง
                int xPos = margin;
                for (int i = 0; i < headers.Length; i++)
                {
                    e.Graphics.DrawString(headers[i], headerFont, Brushes.Black, xPos, yPos);
                    xPos += colWidths[i];
                }

                yPos += 25;

                // วาดเส้นคั่น
                e.Graphics.DrawLine(Pens.Black, margin, yPos - 5, pageWidth - margin, yPos - 5);

                // วาดข้อมูล
                int rowNum = 1;
                foreach (DataRow row in data.Rows)
                {
                    xPos = margin;

                    // ลำดับ
                    e.Graphics.DrawString(rowNum.ToString(), contentFont, Brushes.Black, xPos, yPos);
                    xPos += colWidths[0];

                    // วันที่
                    DateTime scheduleDate = Convert.ToDateTime(row["Schedule_Date"]);
                    e.Graphics.DrawString(scheduleDate.ToString("dd/MM/yyyy"), contentFont, Brushes.Black, xPos, yPos);
                    xPos += colWidths[1];

                    // เวลาเริ่ม
                    string startTime = row["Schedule_StartTime"].ToString();
                    e.Graphics.DrawString(startTime.Length > 5 ? startTime.Substring(0, 5) : startTime, contentFont, Brushes.Black, xPos, yPos);
                    xPos += colWidths[2];

                    // เวลาสิ้นสุด
                    string endTime = row["Schedule_EndTime"].ToString();
                    e.Graphics.DrawString(endTime.Length > 5 ? endTime.Substring(0, 5) : endTime, contentFont, Brushes.Black, xPos, yPos);
                    xPos += colWidths[3];

                    // พนักงาน
                    e.Graphics.DrawString(row["EmployeeName"].ToString(), contentFont, Brushes.Black, xPos, yPos);

                    yPos += 20;
                    rowNum++;

                    // ตรวจสอบว่าต้องขึ้นหน้าใหม่หรือไม่
                    if (yPos > e.PageBounds.Height - margin)
                    {
                        e.HasMorePages = false;
                        return;
                    }
                }

                // วาดเส้นปิดตาราง
                e.Graphics.DrawLine(Pens.Black, margin, yPos, pageWidth - margin, yPos);

                // วาดจำนวนรายการทั้งหมด
                yPos += 20;
                e.Graphics.DrawString($"จำนวนรายการทั้งหมด: {data.Rows.Count} รายการ", headerFont, Brushes.Black, margin, yPos);

                e.HasMorePages = false;
            };

            if (printPreviewDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();  // เริ่มการพิมพ์
            }
        }

        private void btnAvailable_Click(object sender, EventArgs e)
        {
            PrintFilteredData("Available");
        }

        private void btnBooked_Click(object sender, EventArgs e)
        {
            PrintFilteredData("Booked");
        }

        private void btnCleaning_Click(object sender, EventArgs e)
        {
            PrintFilteredData("Cleaned");
        }

        private void btnMaintenance_Click(object sender, EventArgs e)
        {
            PrintFilteredData("Maintenance");
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadScheduleData();  
        }

        private void dtpDatePicker_ValueChanged(object sender, EventArgs e)
        {
            // โหลดตารางเวลาตามวันที่ที่เลือกใน DateTimePicker
            LoadScheduleData(dtpDatePicker.Value);
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            PrintFilteredData("Check in");
        }

        private void btnNoShow_Click(object sender, EventArgs e)
        {
            PrintFilteredData("No show");
        }
    }
}
