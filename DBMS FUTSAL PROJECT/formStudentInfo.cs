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
    public partial class formStudentInfo : Form
    {
        const string strFileName = "ConnectionString.ini";
        private string strConnectionString = "";
        private int bookingID;
        private int scheduleID;

        public formStudentInfo(int bookingID, int scheduleID)
        {
            InitializeComponent();
            this.bookingID = bookingID; // รับค่า Booking_ID ที่มาจากฟอร์มก่อนหน้านี้
            this.scheduleID = scheduleID; ;
        }

        private void formStudentInfo_Load(object sender, EventArgs e)
        {
            if (File.Exists(strFileName))
                strConnectionString = File.ReadAllText(strFileName, Encoding.GetEncoding("Windows-874"));

            if (string.IsNullOrEmpty(strConnectionString))
            {
                MessageBox.Show("ไม่สามารถอ่านค่า Connection String ได้จากไฟล์", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnSaveStudentID_Click(object sender, EventArgs e)
        {
            List<string> studentIDs = new List<string>();

            // ตรวจสอบค่าในแต่ละ TextBox
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    string studentID = (control as TextBox).Text.Trim();
                    if (!string.IsNullOrEmpty(studentID)) // ตรวจสอบว่าไม่เป็นค่าว่าง
                    {
                        studentIDs.Add(studentID);
                    }
                }
            }
            // ตรวจสอบว่ามีรหัสนักศึกษาที่ซ้ำกันหรือไม่
            var duplicateStudentIDs = studentIDs.GroupBy(id => id)
                                                .Where(group => group.Count() > 1)
                                                .Select(group => group.Key)
                                                .ToList();

            if (duplicateStudentIDs.Any())
            {
                MessageBox.Show("พบรหัสนักศึกษาซ้ำกัน: " + string.Join(", ", duplicateStudentIDs), "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // ตรวจสอบจำนวนรหัสนักศึกษา
            if (studentIDs.Count < 5)
            {
                MessageBox.Show("กรุณากรอกรหัสนักศึกษาอย่างน้อย 5 คน", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (studentIDs.Count == 0)
            {
                MessageBox.Show("กรุณากรอกรหัสนักศึกษา", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();

                    // เริ่มต้นการทำธุรกรรม
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            foreach (string studentID in studentIDs)
                            {
                                // เช็คว่ารหัสนักศึกษามีในฐานข้อมูลหรือไม่
                                string checkQuery = "SELECT COUNT(*) FROM Student WHERE Student_ID = @StudentID";
                                using (SqlCommand cmdCheck = new SqlCommand(checkQuery, conn, transaction))
                                {
                                    cmdCheck.Parameters.AddWithValue("@StudentID", studentID);

                                    int studentCount = (int)cmdCheck.ExecuteScalar();
                                    if (studentCount == 0) // ถ้าไม่พบข้อมูลนักศึกษาในฐานข้อมูล
                                    {
                                        MessageBox.Show("ไม่พบข้อมูลนักศึกษารหัส " + studentID, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        transaction.Rollback();  // Rollback การทำธุรกรรมทั้งหมด
                                        return;
                                    }
                                }

                                // ถ้ามีข้อมูลนักศึกษาในฐานข้อมูลแล้ว ให้บันทึกลงในตาราง Participants
                                string insertQuery = "INSERT INTO Participants (Student_ID, Booking_ID) VALUES (@StudentID, @BookingID)";
                                using (SqlCommand cmd = new SqlCommand(insertQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@StudentID", studentID);
                                    cmd.Parameters.AddWithValue("@BookingID", bookingID);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            // ยืนยันการทำธุรกรรม
                            transaction.Commit();
                            MessageBox.Show("การจองสำเร็จ!", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            // Rollback การทำธุรกรรมหากเกิดข้อผิดพลาด
                            transaction.Rollback();
                            MessageBox.Show("เกิดข้อผิดพลาดในการบันทึกข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการเชื่อมต่อฐานข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancle_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("คุณต้องการยกเลิกการจองนี้หรือไม่?", "ยกเลิกการจอง", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    // เชื่อมต่อฐานข้อมูล
                    using (SqlConnection conn = new SqlConnection(strConnectionString))
                    {
                        conn.Open();

                        // เริ่มต้น Transaction
                        using (SqlTransaction transaction = conn.BeginTransaction())
                        {
                            try
                            {
                                // อัปเดตสถานะสนามให้เป็น "ว่าง" (Status_ID = 1) และทำให้ Booking_ID เป็น NULL ในทุกแถวที่เลือก

                                string updateStatus = "UPDATE Schedule SET Status_ID = 1, Booking_ID = NULL WHERE Schedule_ID = @ScheduleID";
                                using (SqlCommand cmdUpdateStatus = new SqlCommand(updateStatus, conn, transaction))
                                {
                                    cmdUpdateStatus.Parameters.AddWithValue("@ScheduleID", scheduleID);
                                    cmdUpdateStatus.ExecuteNonQuery();
                                }

                                // ลบ Booking_ID ในตาราง Bookings
                                string deleteBooking = "DELETE FROM Booking WHERE Booking_ID = @BookingID";
                                using (SqlCommand cmdDeleteBooking = new SqlCommand(deleteBooking, conn, transaction))
                                {
                                    cmdDeleteBooking.Parameters.AddWithValue("@BookingID", bookingID);
                                    cmdDeleteBooking.ExecuteNonQuery();
                                }

                                // ยืนยันการทำธุรกรรม
                                transaction.Commit();
                                MessageBox.Show("การจองถูกยกเลิกเรียบร้อยแล้ว", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // ปิดฟอร์มเมื่อยกเลิก
                                this.Close();
                            }
                            catch (Exception ex)
                            {
                                // หากเกิดข้อผิดพลาดให้ Rollback การทำธุรกรรม
                                transaction.Rollback();
                                MessageBox.Show("เกิดข้อผิดพลาดในการยกเลิกการจอง: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // แสดงข้อผิดพลาดที่เกิดขึ้น
                    MessageBox.Show("เกิดข้อผิดพลาดในการเชื่อมต่อฐานข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}