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

namespace DBMS_FUTSAL_PROJECT
{
    public partial class formLogin : Form
    {
        const string strFileName = "ConnectionString.ini";  // ไฟล์เก็บ connection string
        string strConnectionString;  // ตัวแปรเก็บ connection string

        public formLogin()
        {
            InitializeComponent();
        }

        // ฟังก์ชันโหลดฟอร์มเมื่อเปิด
        private void formLogin_Load(object sender, EventArgs e)
        {
            textBoxPassword.PasswordChar = '*'; // ซ่อนรหัสผ่านตั้งแต่เริ่มต้น
            btnShowPass.Image = Properties.Resources.eyeclose; // ตั้งค่ารูปตาปิดเป็นค่าเริ่มต้น

            if (System.IO.File.Exists(strFileName))
            {
                // อ่านค่าจากไฟล์ .ini สำหรับ connection string
                strConnectionString = System.IO.File.ReadAllText(strFileName);
            }
            else
            {
                // แจ้งข้อผิดพลาดหากไฟล์ .ini ไม่พบ
                MessageBox.Show("Connection string file is missing or invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        // ฟังก์ชันการแสดงและซ่อนรหัสผ่าน
        private bool isPasswordVisible = false; // ตัวแปรเก็บสถานะการแสดงรหัส
        private void btnShowPass_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible; // เปลี่ยนค่าจาก true -> false หรือ false -> true

            if (isPasswordVisible)
            {
                textBoxPassword.PasswordChar = '\0'; // โชว์รหัสผ่าน
                btnShowPass.Image = Properties.Resources.eyeopen; // เปลี่ยนเป็นรูปตาเปิด
            }
            else
            {
                textBoxPassword.PasswordChar = '*'; // ซ่อนรหัสผ่าน
                btnShowPass.Image = Properties.Resources.eyeclose; // เปลี่ยนเป็นรูปตาปิด
            }
        }

        // ฟังก์ชันการล็อกอิน
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            // การเชื่อมต่อกับฐานข้อมูล
            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                conn.Open();

                // คำสั่ง SQL เพื่อล็อกอินของพนักงาน
                string queryEmployee = "SELECT 'Employee' AS Role, Emp_Position, Emp_Id " +
                                       "FROM Employee WHERE Emp_Username = @username AND Emp_Password = @password";

                // คำสั่ง SQL เพื่อล็อกอินของนักศึกษา
                string queryStudent = "SELECT 'Student' AS Role, Student.Student_ID, Faculty.Faculty_Name " +
                                      "FROM Student " +
                                      "INNER JOIN Major ON Student.Major_ID = Major.Major_ID " +
                                      "INNER JOIN Faculty ON Major.Faculty_ID = Faculty.Faculty_ID " +
                                      "WHERE Student.Student_Username = @username AND Student.Student_Password = @password";

                using (SqlCommand cmd = new SqlCommand(queryEmployee, conn))
                {
                    // ใช้ parameterized query เพื่อลดความเสี่ยงจาก SQL Injection
                    cmd.Parameters.AddWithValue("@username", textBoxUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", textBoxPassword.Text.Trim());

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read()) // หากข้อมูลตรง
                    {
                        string role = reader["Role"].ToString();  // ดึงบทบาทผู้ใช้

                        // เก็บข้อมูลพนักงาน
                        CurrentUser.EmpID = Convert.ToInt32(reader["Emp_Id"]);
                        CurrentUser.EmpPosition = reader["Emp_Position"].ToString();

                        // แสดงข้อความว่า "ล็อกอินสำเร็จ"
                        MessageBox.Show("ล็อกอินสำเร็จ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (role == "Employee")
                        {
                            // เก็บข้อมูลบทบาท
                            CurrentUser.Role = role;
                            // เปิดฟอร์มสำหรับพนักงาน
                            Form formIndex = new formIndex();
                            formIndex.Show();
                            this.Hide();
                        }
                    }
                    else
                    {
                        // ลองค้นหานักศึกษาถ้าพนักงานไม่พบ
                        reader.Close(); // ปิด reader ก่อนที่จะใช้งานคำสั่ง SQL อื่นๆ

                        using (SqlCommand cmdStudent = new SqlCommand(queryStudent, conn))
                        {
                            cmdStudent.Parameters.AddWithValue("@username", textBoxUsername.Text.Trim());
                            cmdStudent.Parameters.AddWithValue("@password", textBoxPassword.Text.Trim());

                            SqlDataReader studentReader = cmdStudent.ExecuteReader();

                            if (studentReader.Read()) // หากข้อมูลนักศึกษาตรง
                            {
                                string role = studentReader["Role"].ToString(); // บทบาทของนักศึกษา

                                // เก็บข้อมูลนักศึกษา
                                CurrentUser.StudentID = Convert.ToInt64(studentReader["Student_ID"]);
                                CurrentUser.StudentFaculty = studentReader["Faculty_Name"].ToString();

                                // แสดงข้อความว่า "ล็อกอินสำเร็จ"
                                MessageBox.Show("ล็อกอินสำเร็จ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                if (role == "Student")
                                {
                                    // เก็บข้อมูลบทบาท
                                    CurrentUser.Role = role;
                                    // เปิดฟอร์มสำหรับนักศึกษา
                                    Form formIndex = new formIndex();
                                    formIndex.Show();
                                    this.Hide();
                                }
                            }
                            else
                            {
                                // หากไม่ตรงกับข้อมูลที่มีในฐานข้อมูล
                                MessageBox.Show("คุณกรอกชื่อผู้ใช้งานหรือรหัสผ่านผิด", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://account.kmutnb.ac.th/web/recovery/index");
        }
    }
}