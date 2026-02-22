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
    public partial class formScheduleMac : Form
    {
        const string strFileName = "ConnectionString.ini";
        private string strConnectionString = "";  // ประกาศตัวแปรสำหรับ Connection String
        public formScheduleMac()
        {
            InitializeComponent();
        }

        private void formScheduleMac_Load(object sender, EventArgs e)
        {
            LoadScheduleData(DateTime.Today);
        }
        private void LoadScheduleData()
        {
            if (File.Exists(strFileName))
                strConnectionString = File.ReadAllText(strFileName, Encoding.GetEncoding("Windows-874"));

            if (string.IsNullOrEmpty(strConnectionString))
            {
                MessageBox.Show("ไม่สามารถอ่านค่า Connection String ได้จากไฟล์", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string query = "SELECT Schedule_ID, Booking_ID, Emp_ID, Status_ID, Schedule_Date, Schedule_StartTime, Schedule_EndTime " +
                           "FROM dbo.Schedule " +
                           "WHERE Status_ID NOT IN (4,5)";  // เลือกข้อมูลที่ Status_ID ไม่เท่ากับ 4

            using (SqlConnection connection = new SqlConnection(strConnectionString))
            {
                try
                {

                    connection.Open(); // เปิดการเชื่อมต่อ

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable); // เติมข้อมูลจากฐานข้อมูลลงใน DataTable

                    // ตั้งค่า DataGridView เพื่อแสดงข้อมูลจาก DataTable
                    dataGridViewSchedule.DataSource = dataTable;

                    // เพิ่มคอลัมน์ ComboBox สำหรับ Status_ID
                    if (!dataGridViewSchedule.Columns.Contains("StatusType"))
                    {
                        DataGridViewComboBoxColumn statusColumn = new DataGridViewComboBoxColumn();
                        statusColumn.HeaderText = "Status Type";
                        statusColumn.Name = "StatusType";
                        statusColumn.DataPropertyName = "Status_ID";  // ผูกกับ Status_ID ใน DataTable

                        // ดึงข้อมูลจากตาราง Status
                        List<Status> statusList = GetStatusList();

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

                    // เพิ่มคอลัมน์ Checkbox สำหรับเลือกแถว
                    if (!dataGridViewSchedule.Columns.Contains("Select"))
                    {
                        DataGridViewCheckBoxColumn selectColumn = new DataGridViewCheckBoxColumn();
                        selectColumn.HeaderText = "";
                        selectColumn.Name = "Select";
                        dataGridViewSchedule.Columns.Insert(0, selectColumn); // เพิ่มคอลัมน์ Checkbox ที่ตำแหน่งแรก
                    }

                    // ปิดการแก้ไขคอลัมน์อื่นๆ
                    foreach (DataGridViewColumn column in dataGridViewSchedule.Columns)
                    {
                        if (column.Name != "Select")  // ปิดการแก้ไขในคอลัมน์ที่ไม่ใช่ StatusType หรือ Select
                        {
                            column.ReadOnly = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private void LoadScheduleData(DateTime? selectedDate = null)
        {
            if (File.Exists(strFileName))
                strConnectionString = File.ReadAllText(strFileName, Encoding.GetEncoding("Windows-874"));

            if (string.IsNullOrEmpty(strConnectionString))
            {
                MessageBox.Show("ไม่สามารถอ่านค่า Connection String ได้จากไฟล์", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // สร้างคำสั่ง SQL สำหรับกรองข้อมูลตามวันที่
            string query = "SELECT Schedule_ID, Booking_ID, Emp_ID, Status_ID, Schedule_Date, Schedule_StartTime, Schedule_EndTime " +
                           "FROM dbo.Schedule " +
                           "WHERE Status_ID NOT IN (4,5)";  // เลือกข้อมูลที่ Status_ID ไม่เท่ากับ 4

            // ถ้ามีการเลือกวันที่จาก DateTimePicker
            if (selectedDate.HasValue)
            {
                query += " AND CAST(Schedule_Date AS DATE) = @SelectedDate"; // กรองข้อมูลตามวันที่ที่เลือก
            }

            using (SqlConnection connection = new SqlConnection(strConnectionString))
            {
                try
                {
                    connection.Open(); // เปิดการเชื่อมต่อ

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);

                    // ถ้ามีวันที่ที่เลือก ให้ใช้พารามิเตอร์ส่งวันที่ไปในคำสั่ง SQL
                    if (selectedDate.HasValue)
                    {
                        dataAdapter.SelectCommand.Parameters.AddWithValue("@SelectedDate", selectedDate.Value.Date); // ส่งวันที่ที่เลือกไปในพารามิเตอร์
                    }

                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable); // เติมข้อมูลจากฐานข้อมูลลงใน DataTable

                    // ตั้งค่า DataGridView เพื่อแสดงข้อมูลจาก DataTable
                    dataGridViewSchedule.DataSource = dataTable;

                    // เพิ่มคอลัมน์ ComboBox สำหรับ Status_ID
                    if (!dataGridViewSchedule.Columns.Contains("StatusType"))
                    {
                        DataGridViewComboBoxColumn statusColumn = new DataGridViewComboBoxColumn();
                        statusColumn.HeaderText = "Status Type";
                        statusColumn.Name = "StatusType";
                        statusColumn.DataPropertyName = "Status_ID";  // ผูกกับ Status_ID ใน DataTable

                        // ดึงข้อมูลจากตาราง Status
                        List<Status> statusList = GetStatusList();

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

                    // เพิ่มคอลัมน์ Checkbox สำหรับเลือกแถว
                    if (!dataGridViewSchedule.Columns.Contains("Select"))
                    {
                        DataGridViewCheckBoxColumn selectColumn = new DataGridViewCheckBoxColumn();
                        selectColumn.HeaderText = "";
                        selectColumn.Name = "Select";
                        dataGridViewSchedule.Columns.Insert(0, selectColumn); // เพิ่มคอลัมน์ Checkbox ที่ตำแหน่งแรก
                    }

                    // ปิดการแก้ไขคอลัมน์อื่นๆ
                    foreach (DataGridViewColumn column in dataGridViewSchedule.Columns)
                    {
                        if (column.Name != "Select")  // ปิดการแก้ไขในคอลัมน์ที่ไม่ใช่ StatusType หรือ Select
                        {
                            column.ReadOnly = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
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

        private void buttonRepair_Click(object sender, EventArgs e)
        {
            try
            {
                int updatedCount = 0; // เพิ่มตัวแปรนับจำนวนรายการที่อัปเดต
                List<int> bookingsToCancel = new List<int>();
                List<int> allSchedulesToUpdate = new List<int>();

                // รวบรวมรายการที่ต้องการยกเลิกและอัปเดต
                foreach (DataGridViewRow row in dataGridViewSchedule.Rows)
                {
                    if (row.Cells["Select"].Value != null && Convert.ToBoolean(row.Cells["Select"].Value) == true)
                    {
                        int scheduleID = row.Cells["Schedule_ID"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["Schedule_ID"].Value) : 0;
                        int statusID = row.Cells["Status_ID"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["Status_ID"].Value) : 0;
                        int bookingID = row.Cells["Booking_ID"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["Booking_ID"].Value) : 0;

                        // เก็บทุกตารางเวลาที่เลือกเพื่ออัปเดตเป็นสถานะ 3
                        if (scheduleID > 0)
                        {
                            allSchedulesToUpdate.Add(scheduleID);
                        }

                        // เก็บเฉพาะการจองที่มีสถานะเป็น 2 เพื่อยกเลิก
                        if (statusID == 2 && bookingID > 0)
                        {
                            bookingsToCancel.Add(bookingID);
                        }
                    }
                }

                // ดำเนินการยกเลิกการจองทั้งหมดในครั้งเดียว (ถ้ามี)
                if (bookingsToCancel.Count > 0)
                {
                    bool success = CancelMultipleBookings(bookingsToCancel);
                    if (!success)
                    {
                        MessageBox.Show("เกิดข้อผิดพลาดในการยกเลิกการจอง กรุณาลองใหม่อีกครั้ง", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // อัปเดตสถานะเป็น 3 สำหรับทุกตารางเวลาที่เลือก
                if (allSchedulesToUpdate.Count > 0)
                {
                    using (SqlConnection scheduleConnection = new SqlConnection(strConnectionString))
                    {
                        scheduleConnection.Open();
                        foreach (int scheduleID in allSchedulesToUpdate)
                        {
                            string query = "UPDATE Schedule SET Status_ID = 3 , Emp_ID = @EmpID WHERE Schedule_ID = @ScheduleID";
                            SqlCommand updateCommand = new SqlCommand(query, scheduleConnection);
                            updateCommand.Parameters.AddWithValue("@ScheduleID", scheduleID);
                            updateCommand.Parameters.AddWithValue("@EmpID", CurrentUser.EmpID);
                            int result = updateCommand.ExecuteNonQuery();
                            if (result > 0)
                            {
                                updatedCount++;
                            }
                        }
                    }
                }

                // รีเฟรช DataGridView หลังจากทำการอัปเดตข้อมูล
                LoadScheduleData(dtpDatePicker.Value);

                if (updatedCount > 0)
                {
                    MessageBox.Show($"สถานะการซ่อมได้รับการอัปเดต {updatedCount} รายการเรียบร้อยแล้ว");
                }
                else
                {
                    MessageBox.Show("ไม่มีรายการที่ถูกอัปเดต โปรดตรวจสอบว่าได้เลือกรายการหรือไม่");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private bool CancelMultipleBookings(List<int> bookingIDs)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            foreach (int bookingID in bookingIDs)
                            {
                                // อัปเดต Schedule
                                string queryUpdateSchedule = @"
                        UPDATE Schedule
                        SET Booking_ID = NULL, Status_ID = 1
                        WHERE Booking_ID = @BookingID AND Status_ID = 2";
                                SqlCommand cmdUpdateSchedule = new SqlCommand(queryUpdateSchedule, conn, transaction);
                                cmdUpdateSchedule.Parameters.AddWithValue("@BookingID", bookingID);
                                cmdUpdateSchedule.ExecuteNonQuery();

                                // ลบข้อมูลจาก Participants
                                string queryDeleteParticipants = @"
                        DELETE FROM Participants
                        WHERE Booking_ID = @BookingID";
                                SqlCommand cmdDeleteParticipants = new SqlCommand(queryDeleteParticipants, conn, transaction);
                                cmdDeleteParticipants.Parameters.AddWithValue("@BookingID", bookingID);
                                cmdDeleteParticipants.ExecuteNonQuery();

                                // ลบข้อมูลจาก Booking
                                string queryDeleteBooking = @"
                        DELETE FROM Booking
                        WHERE Booking_ID = @BookingID";
                                SqlCommand cmdDeleteBooking = new SqlCommand(queryDeleteBooking, conn, transaction);
                                cmdDeleteBooking.Parameters.AddWithValue("@BookingID", bookingID);
                                cmdDeleteBooking.ExecuteNonQuery();
                            }

                            // คอมมิทการทำธุรกรรมทั้งหมด
                            transaction.Commit();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Error occurred while cancelling bookings: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while cancelling bookings: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void buttonComplete_Click(object sender, EventArgs e)
        {
            try
            {
                int updatedCount = 0; // เพิ่มตัวแปรนับจำนวนรายการที่อัปเดต

                using (SqlConnection scheduleConnection = new SqlConnection(strConnectionString))
                {
                    scheduleConnection.Open();
                    // ลูปตรวจสอบแถวที่ถูกเลือก (checked)
                    foreach (DataGridViewRow row in dataGridViewSchedule.Rows)
                    {
                        // ตรวจสอบว่าแถวนี้ถูกเลือกหรือไม่ และไม่ใช่แถวใหม่ที่ว่างเปล่า
                        if (row.Cells["Select"].Value != null && Convert.ToBoolean(row.Cells["Select"].Value) == true)
                        {
                            // ตรวจสอบค่า DBNull ก่อนการแปลงค่าจากเซลล์
                            int scheduleID = row.Cells["Schedule_ID"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["Schedule_ID"].Value) : 0;
                            int statusID = row.Cells["Status_ID"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["Status_ID"].Value) : 0;

                            // ตรวจสอบว่าสถานะเป็น 3 หรือไม่
                            if (statusID == 3)
                            {
                                // สร้างคำสั่ง SQL สำหรับการอัปเดตสถานะเป็น 1
                                string query = "UPDATE Schedule SET Status_ID = 1 , Emp_ID = @EmpID WHERE Schedule_ID = @ScheduleID";
                                SqlCommand updateCommand = new SqlCommand(query, scheduleConnection);
                                updateCommand.Parameters.AddWithValue("@EmpID", CurrentUser.EmpID);
                                updateCommand.Parameters.AddWithValue("@ScheduleID", scheduleID);


                                // ทำการอัปเดตสถานะ
                                int result = updateCommand.ExecuteNonQuery();
                                if (result > 0)
                                {
                                    updatedCount++;
                                }
                            }
                        }
                    }

                    // รีเฟรช DataGridView หลังจากทำการอัปเดตข้อมูล
                    LoadScheduleData(dtpDatePicker.Value);

                    if (updatedCount > 0)
                    {
                        MessageBox.Show($"สถานะการซ่อมได้รับการอัปเดต {updatedCount} รายการเรียบร้อยแล้ว");
                    }
                    else
                    {
                        MessageBox.Show("ไม่มีรายการที่ถูกอัปเดต โปรดตรวจสอบว่าได้เลือกรายการที่มีสถานะเป็น Maintenance หรือไม่");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
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
    }
}