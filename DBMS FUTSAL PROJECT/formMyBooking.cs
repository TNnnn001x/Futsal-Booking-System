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
    public partial class formMyBooking : Form
    {
        const string strFileName = "ConnectionString.ini";
        private string strConnectionString = "";  // ประกาศตัวแปรสำหรับ Connection String
        public formMyBooking()
        {
            InitializeComponent();
        }
        private void SetupDataGridView()
        { // ตรวจสอบว่าคอลัมน์ "Book" ถูกเพิ่มไว้แล้วหรือไม่
            if (!dataGridViewBookingDetails.Columns.Contains("CancelButton"))
            {
                // เพิ่มคอลัมน์ปุ่มยกเลิก
                DataGridViewButtonColumn cancelButtonColumn = new DataGridViewButtonColumn();
                cancelButtonColumn.HeaderText = ""; // ไม่แสดงหัวคอลัมน์
                cancelButtonColumn.Text = "Cancel";
                cancelButtonColumn.UseColumnTextForButtonValue = true;
                cancelButtonColumn.Name = "CancelButton";

                // เพิ่มคอลัมน์ปุ่ม Cancel ไว้ที่ท้ายสุด
                cancelButtonColumn.DisplayIndex = dataGridViewBookingDetails.Columns.Count;  // ทำให้ปุ่มแสดงที่ท้ายสุด
                dataGridViewBookingDetails.Columns.Add(cancelButtonColumn);
            }            
        }
        private void dataGridViewBookingDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // ตรวจสอบว่าคลิกในแถวที่มีค่า RowIndex และ ColumnIndex ถูกต้อง
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // ตรวจสอบว่าคลิกในคอลัมน์ของปุ่ม CancelButton
                if (e.ColumnIndex == dataGridViewBookingDetails.Columns["CancelButton"].Index)
                {
                    // ตรวจสอบว่าคอลัมน์ "Booking_ID" และ "Status" มีอยู่ใน DataGridView
                    if (dataGridViewBookingDetails.Columns.Contains("Booking_ID") && dataGridViewBookingDetails.Columns.Contains("Status"))
                    {
                        // ตรวจสอบว่ามีแถวใน DataGridView ที่คลิกได้
                        if (dataGridViewBookingDetails.Rows.Count > e.RowIndex)
                        {
                            // ดึงข้อมูล Booking_ID จากแถวที่คลิก
                            int bookingID = Convert.ToInt32(dataGridViewBookingDetails.Rows[e.RowIndex].Cells["Booking_ID"].Value);
                            string status = dataGridViewBookingDetails.Rows[e.RowIndex].Cells["Status"].Value.ToString();

                            // ถ้าสถานะเป็น "Booked" ให้ทำการยกเลิกการจอง
                            if (status == "Booked")
                            {
                                // เรียกใช้ฟังก์ชันยกเลิกการจอง
                                CancelBooking(bookingID);
                            }
                            else
                            {
                                MessageBox.Show("Cannot cancel, status is not 'Booked'.", "Invalid Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("แถวนี้ไม่สามารถคลิกได้", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Booking_ID หรือ Status คอลัมน์ไม่พบใน DataGridView.", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void CancelBooking(int bookingID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();

                    // เริ่มต้นการทำธุรกรรม (Transaction)
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // คำสั่ง SQL อัปเดตการจองในตาราง Schedule
                            string queryUpdateSchedule = "UPDATE Schedule SET Booking_ID = NULL, Status_ID = 1 WHERE Booking_ID = @BookingID AND Status_ID = 2";
                            SqlCommand cmdUpdateSchedule = new SqlCommand(queryUpdateSchedule, conn, transaction);
                            cmdUpdateSchedule.Parameters.AddWithValue("@BookingID", bookingID);

                            int rowsAffectedSchedule = cmdUpdateSchedule.ExecuteNonQuery();

                            // คำสั่ง SQL ลบแถวในตาราง Participants
                            string queryDeleteParticipants = "DELETE FROM Participants WHERE Booking_ID = @BookingID";
                            SqlCommand cmdDeleteParticipants = new SqlCommand(queryDeleteParticipants, conn, transaction);
                            cmdDeleteParticipants.Parameters.AddWithValue("@BookingID", bookingID);

                            int rowsAffectedParticipants = cmdDeleteParticipants.ExecuteNonQuery();

                            // คำสั่ง SQL ลบแถวในตาราง Booking
                            string queryDeleteBooking = "DELETE FROM Booking WHERE Booking_ID = @BookingID";
                            SqlCommand cmdDeleteBooking = new SqlCommand(queryDeleteBooking, conn, transaction);
                            cmdDeleteBooking.Parameters.AddWithValue("@BookingID", bookingID);

                            int rowsAffectedBooking = cmdDeleteBooking.ExecuteNonQuery();

                            // ตรวจสอบว่ามีการอัปเดตหรือการลบทั้งสามตารางสำเร็จ
                            if (rowsAffectedSchedule > 0 && rowsAffectedParticipants > 0 && rowsAffectedBooking > 0)
                            {
                                transaction.Commit();
                                MessageBox.Show("Booking has been cancelled successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadBookingData(); // โหลดข้อมูลใหม่หลังจากยกเลิก
                            }
                            else
                            {
                                transaction.Rollback();
                                MessageBox.Show("Failed to cancel the booking or the status is not 'Booked'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Error occurred while cancelling booking: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while cancelling booking: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void formMyBooking_Load(object sender, EventArgs e)
        {

            LoadBookingData();
        }
        private void LoadBookingData()
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

                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();

                    // สร้างคำสั่ง SQL เพื่อดึงข้อมูลการจอง
                    string query = @"
                    SELECT b.Booking_ID, b.Booking_Date, s.Schedule_Date, s.Schedule_StartTime, s.Schedule_EndTime ,st.Status
                    FROM Booking b
                    JOIN Participants p ON b.Booking_ID = p.Booking_ID
                    JOIN Schedule s ON b.Booking_ID = s.Booking_ID
                    JOIN Status st ON s.Status_ID = st.Status_ID
                    WHERE p.Student_ID = @StudentID";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@StudentID", CurrentUser.StudentID);
                    DataTable bookingTable = new DataTable();
                    adapter.Fill(bookingTable);


                    // กำหนด DataGridView ให้แสดงข้อมูล
                    dataGridViewBookingDetails.DataSource = bookingTable;

                    // ตั้งชื่อคอลัมน์ใน DataGridView
                    dataGridViewBookingDetails.Columns["Booking_ID"].HeaderText = "Booking_ID";
                    dataGridViewBookingDetails.Columns["Booking_Date"].HeaderText = "Booking date";
                    dataGridViewBookingDetails.Columns["Schedule_Date"].HeaderText = "Schedule date";
                    dataGridViewBookingDetails.Columns["Schedule_StartTime"].HeaderText = "Time start";
                    dataGridViewBookingDetails.Columns["Schedule_EndTime"].HeaderText = "Time end";

                    SetupDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการโหลดข้อมูลการจอง: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}