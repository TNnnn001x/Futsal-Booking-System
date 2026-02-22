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
using System.Windows.Forms.VisualStyles;

namespace DBMS_FUTSAL_PROJECT
{
    public partial class formBooking : Form
    {
        const string strFileName = "ConnectionString.ini";
        private string strConnectionString = "";
        

        public formBooking()
        {
            InitializeComponent();
            
        }

        private void formBooking_Load(object sender, EventArgs e)
        {
            PopulateDateComboBox();
            LoadScheduleData();

        }
        private void dataGridViewScheduleBook_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // ตรวจสอบว่าเราคลิกในคอลัมน์ "Book"
            if (e.ColumnIndex == dataGridViewScheduleBook.Columns["Book"].Index)
            {
                // ดึงข้อมูลสถานะจากแถวที่คลิก
                string status = dataGridViewScheduleBook.Rows[e.RowIndex].Cells["Status"].Value.ToString();

                // ตรวจสอบสถานะเป็น "Available"
                if (status == "Available")
                {
                    int scheduleID = Convert.ToInt32(dataGridViewScheduleBook.Rows[e.RowIndex].Cells["Schedule_ID"].Value);

                    // ทำการจอง
                    BookSchedule(scheduleID);
                }
                else
                {
                    MessageBox.Show("ไม่สามารถจองได้ กรุณาเลือกช่วงเวลาอื่น", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BookSchedule(int scheduleID)
        {
            int bookingID = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // สร้างคำสั่ง SQL สำหรับการเพิ่มการจอง
                            string insertBooking = "INSERT INTO Booking (Booking_Date) OUTPUT INSERTED.Booking_ID VALUES (@BookingDate)";
                            using (SqlCommand cmdBooking = new SqlCommand(insertBooking, conn, transaction))
                            {
                                cmdBooking.Parameters.AddWithValue("@BookingDate", DateTime.Now);
                                bookingID = (int)cmdBooking.ExecuteScalar();
                            }

                            // อัปเดตสถานะเป็น "จองแล้ว" สำหรับช่วงเวลาที่เลือก
                            string updateStatus = "UPDATE Schedule SET Status_ID = 2, Booking_ID = @BookingID WHERE Schedule_ID = @ScheduleID AND Status_ID = 1";  // 1 คือ "Available"
                            using (SqlCommand cmdUpdate = new SqlCommand(updateStatus, conn, transaction))
                            {
                                cmdUpdate.Parameters.AddWithValue("@BookingID", bookingID);
                                cmdUpdate.Parameters.AddWithValue("@ScheduleID", scheduleID);
                                cmdUpdate.ExecuteNonQuery();
                            }

                            // ยืนยันการทำธุรกรรม
                            transaction.Commit();
                            formStudentInfo studentForm = new formStudentInfo(bookingID, scheduleID); // ส่งทั้ง Booking_ID และ Schedule_IDs
                            studentForm.ShowDialog();
                            LoadScheduleData(); // รีเฟรชข้อมูล
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("เกิดข้อผิดพลาดในการจอง: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการเชื่อมต่อฐานข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PopulateDateComboBox()
        {
            // กำหนดวันที่เริ่มต้นเป็นวันปัจจุบัน
            DateTime startDate = DateTime.Today;

            // เพิ่มวันที่ใน ComboBox (วันนี้และวันล่วงหน้า 3 วัน)
            for (int i = 0; i < 4; i++)
            {
                DateTime date = startDate.AddDays(i);
                comboBoxDate.Items.Add(date.ToString("dd-MM-yyyy"));  // เพิ่มวันที่ในรูปแบบ dd/MM/yyyy
            }

            // ตั้งค่าเริ่มต้นให้เลือกวันที่ปัจจุบัน
            comboBoxDate.SelectedIndex = 0;
        }
        private void SetupDataGridView()
        {
            // ตรวจสอบว่าคอลัมน์ "Book" ถูกเพิ่มไว้แล้วหรือไม่
            if (!dataGridViewScheduleBook.Columns.Contains("Book"))
            {
                // เพิ่มคอลัมน์ Button สำหรับการจอง
                DataGridViewButtonColumn bookButtonColumn = new DataGridViewButtonColumn();
                bookButtonColumn.HeaderText = "";
                bookButtonColumn.Name = "Book";
                bookButtonColumn.Text = "Book";
                bookButtonColumn.UseColumnTextForButtonValue = true;

                // เพิ่มคอลัมน์ปุ่มจองเข้าไปใน DataGridView
                dataGridViewScheduleBook.Columns.Add(bookButtonColumn);
            }
        }

        private void LoadScheduleData()
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
                {// แปลงวันที่ที่เลือกจาก ComboBox เป็น DateTime
                    DateTime selectedDate = DateTime.ParseExact(comboBoxDate.SelectedItem.ToString(), "dd-MM-yyyy", null);
                    scheduleConnection.Open();

                    string query = @"SELECT s.Schedule_ID, s.Schedule_Date, s.Schedule_StartTime, s.Schedule_EndTime, st.Status
                                 FROM Schedule s
                                 JOIN Status st ON s.Status_ID = st.Status_ID
                                 WHERE CONVERT(DATE, s.Schedule_Date) = @SelectedDate AND s.Status_ID IN (1,2,3)";  // ใช้ฟังก์ชัน CONVERT เพื่อเปรียบเทียบวัน

                    SqlDataAdapter scheduleAdapter = new SqlDataAdapter(query, scheduleConnection);
                    scheduleAdapter.SelectCommand.Parameters.AddWithValue("@SelectedDate", selectedDate);
                    DataTable scheduleTable = new DataTable();
                    scheduleAdapter.Fill(scheduleTable);

                    // ตรวจสอบจำนวนแถวที่ได้จากฐานข้อมูล
                    if (scheduleTable.Rows.Count == 0)
                    {
                        MessageBox.Show("ไม่พบข้อมูลในการโหลด", "ข้อมูลไม่พบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // ผูกข้อมูลจาก DataTable กับ DataGridView
                    dataGridViewScheduleBook.DataSource = scheduleTable;

                    // ตั้งค่าคอลัมน์ใน DataGridView
                    dataGridViewScheduleBook.Columns["Schedule_ID"].Visible = false; // ซ่อนคอลัมน์ Schedule_ID
                    dataGridViewScheduleBook.Columns["Schedule_Date"].HeaderText = "Date";
                    dataGridViewScheduleBook.Columns["Schedule_StartTime"].HeaderText = "Start time";
                    dataGridViewScheduleBook.Columns["Schedule_EndTime"].HeaderText = "End time";
                    dataGridViewScheduleBook.Columns["Status"].HeaderText = "Status"; // เพิ่มหัวคอลัมน์สำหรับ Status_Type
                    SetupDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการโหลดข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadScheduleData();
        }
    }
}