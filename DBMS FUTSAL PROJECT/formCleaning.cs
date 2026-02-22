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
    public partial class formCleaning : Form
    {
        const string strFileName = "ConnectionString.ini";
        private string strConnectionString = "";
        public formCleaning()
        {
            InitializeComponent();
        }

        private void formCleaning_Load(object sender, EventArgs e)
        {
            comboBoxDate.Items.Add(DateTime.Now.ToString("dd-MM-yyyy")); // เพิ่มวันที่ปัจจุบัน
            comboBoxDate.SelectedIndex = 0;  // เลือกวันที่ปัจจุบันเป็นค่าเริ่มต้น
            LoadCleaningScheduleData();
        }
        private void SetupDataGridView()
        { // ตรวจสอบว่าคอลัมน์ "StatusType" ถูกเพิ่มไว้แล้วหรือไม่
            if (!dataGridViewCleaningSchedule.Columns.Contains("StatusType"))
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
                dataGridViewCleaningSchedule.Columns.Add(statusColumn);
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

                    // แก้ไข SQL query เพื่อดึงเฉพาะ Status_ID ที่เป็น 1 และ 4
                    string query = "SELECT Status_ID, Status FROM Status WHERE Status_ID IN (4, 5)";

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
        // ฟังก์ชันเพื่อดึงข้อมูลการจองสนามที่มีสถานะเป็น "Cleaning"
        private void LoadCleaningScheduleData()
        {
            try
            {
                // อ่านค่า Connection String จากไฟล์
                if (File.Exists(strFileName))
                    strConnectionString = File.ReadAllText(strFileName, Encoding.GetEncoding("Windows-874"));

                if (string.IsNullOrEmpty(strConnectionString))
                {
                    MessageBox.Show("ไม่สามารถอ่านค่า Connection String ได้จากไฟล์", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // เชื่อมต่อฐานข้อมูล
                using (SqlConnection scheduleConnection = new SqlConnection(strConnectionString))
                {
                    scheduleConnection.Open();

                    // ดึงวันที่จาก ComboBox
                    DateTime selectedDate = DateTime.ParseExact(comboBoxDate.SelectedItem.ToString(), "dd-MM-yyyy", null);

                    // สร้างคำสั่ง SQL เพื่อดึงข้อมูลจากตาราง Schedule โดยกรองสถานะเป็น "Cleaning" และกรองตามวันที่ที่เลือก
                    string query = @"
                    SELECT * 
                    FROM Schedule 
                    WHERE Status_ID IN (4,5) AND CAST(Schedule_Date AS DATE) = @SelectedDate";  // เพิ่มเงื่อนไขกรองตามวันที่

                    SqlDataAdapter scheduleAdapter = new SqlDataAdapter(query, scheduleConnection);
                    scheduleAdapter.SelectCommand.Parameters.AddWithValue("@SelectedDate", selectedDate); // ส่งวันที่ที่เลือก

                    DataTable scheduleTable = new DataTable();
                    scheduleAdapter.Fill(scheduleTable);


                    // ตรวจสอบจำนวนแถวที่ได้จากฐานข้อมูล
                    if (scheduleTable.Rows.Count == 0)
                    {
                        MessageBox.Show("ไม่พบข้อมูลการจองที่สถานะเป็น Cleaning", "ข้อมูลไม่พบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // ผูกข้อมูลจาก DataTable กับ DataGridView
                    dataGridViewCleaningSchedule.DataSource = scheduleTable;
                    dataGridViewCleaningSchedule.Columns["Booking_ID"].Visible = false;
                    dataGridViewCleaningSchedule.Columns["Emp_ID"].Visible = false;
                    dataGridViewCleaningSchedule.Columns["status_ID"].Visible = false;
                    dataGridViewCleaningSchedule.Columns["Schedule_Date"].HeaderText = "Date";
                    dataGridViewCleaningSchedule.Columns["Schedule_StartTime"].HeaderText = "Start time";
                    dataGridViewCleaningSchedule.Columns["Schedule_EndTime"].HeaderText = "End time";

                    // ตั้งค่า ReadOnly สำหรับคอลัมน์ที่ไม่ใช่ "StatusType"
                    foreach (DataGridViewColumn col in dataGridViewCleaningSchedule.Columns)
                    {
                        if (col.Name != "StatusType")  // คอลัมน์ "StatusType" เท่านั้นที่สามารถแก้ไขได้
                        {
                            col.ReadOnly = true;  // ป้องกันการแก้ไขในคอลัมน์อื่นๆ
                        }
                    }
                    // เรียกใช้เมธอด SetupDataGridView() สำหรับการตั้งค่าข้อมูลกริด
                    SetupDataGridView();
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

                    // สร้างคำสั่ง SQL สำหรับการอัปเดตสถานะในตาราง Schedule
                    SqlCommand updateCommand = new SqlCommand("UPDATE Schedule SET Status_ID = @StatusID,Emp_ID = @EmpID WHERE Schedule_ID = @ScheduleID ", scheduleConnection);

                    // ใช้ DataAdapter สำหรับการอัปเดตข้อมูล
                    SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Schedule", scheduleConnection);
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                    // อัปเดตข้อมูลที่ถูกเลือกใน DataGridView
                    foreach (DataGridViewRow row in dataGridViewCleaningSchedule.SelectedRows)
                    {
                        if (row.Cells["Schedule_ID"].Value != null)
                        {
                            int scheduleID = Convert.ToInt32(row.Cells["Schedule_ID"].Value);
                            int statusID = ((Status)row.Cells["StatusType"].Value).StatusID;  // ดึงสถานะจาก ComboBox

                            // เพิ่มพารามิเตอร์เพื่ออัปเดตข้อมูลในฐานข้อมูล
                            updateCommand.Parameters.Clear();
                            updateCommand.Parameters.AddWithValue("@ScheduleID", scheduleID);
                            updateCommand.Parameters.AddWithValue("@StatusID", statusID);
                            updateCommand.Parameters.AddWithValue("@EmpID", CurrentUser.EmpID);

                            // ทำการอัปเดตข้อมูล
                            updateCommand.ExecuteNonQuery();  // ทำการบันทึกข้อมูลที่แก้ไข
                        }
                    }

                    // ใช้ DataAdapter.Update() เพื่ออัปเดตการเปลี่ยนแปลงกลับไปที่ฐานข้อมูล
                    dataAdapter.Update((DataTable)dataGridViewCleaningSchedule.DataSource);

                    // รีเฟรช DataGridView หลังจากบันทึก
                    LoadCleaningScheduleData();  // ฟังก์ชันนี้จะโหลดข้อมูลจากฐานข้อมูลใหม่เพื่อแสดงข้อมูลที่อัปเดตแล้ว

                    MessageBox.Show("ข้อมูลสถานะได้รับการบันทึกเรียบร้อยแล้ว");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการบันทึกข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}