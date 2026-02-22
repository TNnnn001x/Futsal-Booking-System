using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Dapper;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace DBMS_FUTSAL_PROJECT
{
    public partial class formManageEmp : Form
    {
        const string strFileName = "ConnectionString.ini";  // ไฟล์เก็บ connection string
        string strConnectionString;
        SqlConnection FutsalConnection;
        SqlCommand employeeCommand;
        SqlDataAdapter employeeAdapter;
        DataTable employeeTable;
        CurrencyManager employeeManager;
        string myState;
        int myEmp;

        public formManageEmp()
        {
            InitializeComponent();
        }

        private void formManageEmp_Load(object sender, EventArgs e)
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

                // Connect to books database
                FutsalConnection = new SqlConnection(strConnectionString);
                FutsalConnection.Open();

                // Establish command object
                employeeCommand = new SqlCommand("SELECT * FROM Employee ORDER BY Emp_ID", FutsalConnection);

                // Establish data adapter/data table
                employeeAdapter = new SqlDataAdapter(employeeCommand);
                employeeTable = new DataTable();
                employeeAdapter.Fill(employeeTable);

                // Bind controls to data table
                txtEmpID.DataBindings.Add("Text", employeeTable, "Emp_ID");
                txtUsername.DataBindings.Add("Text", employeeTable, "Emp_Username");
                txtPassword.DataBindings.Add("Text", employeeTable, "Emp_Password");
                txtFName.DataBindings.Add("Text", employeeTable, "Emp_FName");
                txtLName.DataBindings.Add("Text", employeeTable, "Emp_LName");
                txtEmail.DataBindings.Add("Text", employeeTable, "Emp_Email");
                dateTimePickerBirthdate.DataBindings.Add("Text", employeeTable, "Emp_BirthDate");
                comboBoxGender.DataBindings.Add("Text", employeeTable, "Emp_Gender");
                txtPhone.DataBindings.Add("Text", employeeTable, "Emp_Phone");
                txtPosition.DataBindings.Add("Text", employeeTable, "Emp_Position");
                // Establish currency manager
                employeeManager = (CurrencyManager)this.BindingContext[employeeTable];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "เกิดข้อผิดพลาดในการสร้างการทำงานกับตารางผู้แต่ง", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Show();
            SetState("View");
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                // ตรวจสอบข้อมูลก่อนบันทึก
                if (!ValidateData())
                {
                    return; // ถ้าข้อมูลไม่ถูกต้องจะหยุดการทำงาน
                }
                // ตรวจสอบว่า Connection String ถูกต้องหรือไม่
                string strConnectionString = "";
                if (System.IO.File.Exists(strFileName))
                {
                    strConnectionString = System.IO.File.ReadAllText(strFileName);
                }
                else
                {
                    MessageBox.Show("ไม่พบไฟล์ ConnectionString.ini", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();

                    if (myState == "Add")
                    {
                        // SQL Query สำหรับเพิ่มข้อมูลใหม่
                        string insertQuery = @"INSERT INTO Employee (Emp_Username, Emp_Password, Emp_FName, Emp_LName, Emp_Phone, Emp_Position, Emp_Email, Emp_Gender, Emp_BirthDate) 
                                            VALUES (@Emp_Username, @Emp_Password, @Emp_FName, @Emp_LName, @Emp_Phone, @Emp_Position, @Emp_Email, @Emp_Gender, @Emp_BirthDate)";

                        using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@Emp_Username", txtUsername.Text);
                            cmd.Parameters.AddWithValue("@Emp_Password", txtPassword.Text);
                            cmd.Parameters.AddWithValue("@Emp_FName", txtFName.Text);
                            cmd.Parameters.AddWithValue("@Emp_LName", txtLName.Text);
                            cmd.Parameters.AddWithValue("@Emp_Phone", Convert.ToInt32(txtPhone.Text));
                            cmd.Parameters.AddWithValue("@Emp_Position", txtPosition.Text);
                            cmd.Parameters.AddWithValue("@Emp_Email", txtEmail.Text);
                            cmd.Parameters.AddWithValue("@Emp_Gender", comboBoxGender.Text);
                            cmd.Parameters.AddWithValue("@Emp_BirthDate", dateTimePickerBirthdate.Value.Date);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("เพิ่มข้อมูลสำเร็จ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadEmployeeData();
                                SetState("View");
                            }
                            else
                            {
                                MessageBox.Show("ไม่สามารถเพิ่มข้อมูลได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else if (myState == "Edit")
                    {
                        // SQL Query สำหรับอัปเดตข้อมูล
                        string updateQuery = @"UPDATE Employee SET 
                                            Emp_FName = @Emp_FName,
                                            Emp_LName = @Emp_LName,
                                            Emp_Phone = @Emp_Phone,
                                            Emp_Position = @Emp_Position,
                                            Emp_Username = @Emp_Username,
                                            Emp_Email = @Emp_Email,
                                            Emp_Gender = @Emp_Gender,
                                            Emp_BirthDate = @Emp_BirthDate,
                                            Emp_Password = @Emp_Password
                                            WHERE Emp_ID = @Emp_ID";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@Emp_ID", Convert.ToInt32(txtEmpID.Text));
                            cmd.Parameters.AddWithValue("@Emp_FName", txtFName.Text);
                            cmd.Parameters.AddWithValue("@Emp_LName", txtLName.Text);
                            cmd.Parameters.AddWithValue("@Emp_Phone", Convert.ToInt32(txtPhone.Text));
                            cmd.Parameters.AddWithValue("@Emp_Position", txtPosition.Text);
                            cmd.Parameters.AddWithValue("@Emp_Email", txtEmail.Text);
                            cmd.Parameters.AddWithValue("@Emp_Gender", comboBoxGender.Text);
                            cmd.Parameters.AddWithValue("@Emp_BirthDate", dateTimePickerBirthdate.Value.Date);
                            cmd.Parameters.AddWithValue("@Emp_Username", txtUsername.Text);
                            cmd.Parameters.AddWithValue("@Emp_Password", txtPassword.Text);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("อัปเดตข้อมูลสำเร็จ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadEmployeeData();
                                SetState("View");
                            }
                            else
                            {
                                MessageBox.Show("ไม่มีข้อมูลที่ถูกอัปเดต", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการบันทึกข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetState(string appState)
        {
            myState = appState;
            switch (appState)
            {
                case "View":
                    txtEmpID.ReadOnly = true;
                    txtUsername.ReadOnly = true;
                    txtPassword.ReadOnly = true;
                    txtFName.ReadOnly = true;
                    txtLName.ReadOnly = true;
                    txtPhone.ReadOnly = true;
                    txtEmail.ReadOnly = true;
                    comboBoxGender.Enabled = false;
                    dateTimePickerBirthdate.Enabled = false;
                    txtPosition.ReadOnly = true;
                    buttonFirst.Enabled = true;
                    buttonPrevious.Enabled = true;
                    buttonNext.Enabled = true;
                    buttonLast.Enabled = true;
                    buttonAddNew.Enabled = true;
                    buttonSave.Enabled = false;
                    buttonCancel.Enabled = false;
                    buttonEdit.Enabled = true;
                    buttonDelete.Enabled = true;
                    buttonDone.Enabled = true;
                    txtUsername.Focus();
                    break;
                default: // Add or Edit if not View
                    txtEmpID.ReadOnly = false;
                    txtUsername.ReadOnly = false;
                    txtPassword.ReadOnly = false;
                    txtFName.ReadOnly = false;
                    txtLName.ReadOnly = false;
                    txtPhone.ReadOnly = false;
                    txtEmail.ReadOnly = false;
                    comboBoxGender.Enabled = true;
                    dateTimePickerBirthdate.Enabled = true;
                    txtPosition.ReadOnly = false;
                    buttonFirst.Enabled = false;
                    buttonPrevious.Enabled = false;
                    buttonNext.Enabled = false;
                    buttonLast.Enabled = false;
                    buttonAddNew.Enabled = false;
                    buttonSave.Enabled = true;
                    buttonCancel.Enabled = true;
                    buttonEdit.Enabled = false;
                    buttonDelete.Enabled = false;
                    buttonDone.Enabled = false;
                    txtUsername.Focus();
                    break;
            }
        }

        private void buttonFirst_Click(object sender, EventArgs e)
        {
            employeeManager.Position = 0;
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (employeeManager.Position == 0)
            {
                Console.Beep();
            }
            employeeManager.Position--;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (employeeManager.Position == employeeManager.Count - 1)
            {
                Console.Beep();
            }
            employeeManager.Position++;
        }

        private void buttonLast_Click(object sender, EventArgs e)
        {
            employeeManager.Position = employeeManager.Count - 1;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            SetState("Edit");
        }
        private void LoadEmployeeData()
        {
            try
            {
                string strConnectionString = "";
                if (System.IO.File.Exists(strFileName))
                {
                    strConnectionString = System.IO.File.ReadAllText(strFileName);
                }
                else
                {
                    MessageBox.Show("ไม่พบไฟล์ ConnectionString.ini", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();

                    // ดึงข้อมูลใหม่จาก Shipping Table
                    string selectQuery = "SELECT * FROM Employee ORDER BY Emp_ID";
                    employeeTable = new DataTable();
                    employeeTable.Load(conn.ExecuteReader(selectQuery));

                    // อัปเดต DataBinding
                    txtEmpID.DataBindings.Clear();
                    txtUsername.DataBindings.Clear();
                    txtPassword.DataBindings.Clear();
                    txtFName.DataBindings.Clear();
                    txtLName.DataBindings.Clear();
                    txtPhone.DataBindings.Clear();
                    comboBoxGender.DataBindings.Clear();
                    dateTimePickerBirthdate.DataBindings.Clear();
                    txtEmail.DataBindings.Clear();
                    txtPosition.DataBindings.Clear();

                    // Bind ข้อมูลใหม่
                    txtEmpID.DataBindings.Add("Text", employeeTable, "Emp_ID");
                    txtUsername.DataBindings.Add("Text", employeeTable, "Emp_Username");
                    txtPassword.DataBindings.Add("Text", employeeTable, "Emp_Password");
                    txtFName.DataBindings.Add("Text", employeeTable, "Emp_FName");
                    txtLName.DataBindings.Add("Text", employeeTable, "Emp_LName");
                    txtPhone.DataBindings.Add("Text", employeeTable, "Emp_Phone");
                    txtEmail.DataBindings.Add("Text", employeeTable, "Emp_Email");
                    txtPosition.DataBindings.Add("Text", employeeTable, "Emp_Position");
                    comboBoxGender.DataBindings.Add("Text", employeeTable, "Emp_Gender");
                    dateTimePickerBirthdate.DataBindings.Add("Text", employeeTable, "Emp_BirthDate");

                    // แสดงเฉพาะวันที่ใน Estimated Delivery

                    // ตั้งค่า Currency Manager
                    employeeManager = (CurrencyManager)this.BindingContext[employeeTable];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการโหลดข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidateData()
        {
            string message = "";
            bool allOK = true;

            // ตรวจสอบชื่อผู้ใช้
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                message += "กรุณากรอกชื่อผู้ใช้\n";
                txtUsername.Focus();
                allOK = false;
            }

            // ตรวจสอบรหัสผ่าน
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                message += "กรุณากรอกรหัสผ่าน\n";
                txtPassword.Focus();
                allOK = false;
            }

            // ตรวจสอบชื่อจริง
            if (string.IsNullOrWhiteSpace(txtFName.Text))
            {
                message += "กรุณากรอกชื่อจริง\n";
                txtFName.Focus();
                allOK = false;
            }

            // ตรวจสอบนามสกุล
            if (string.IsNullOrWhiteSpace(txtLName.Text))
            {
                message += "กรุณากรอกนามสกุล\n";
                txtLName.Focus();
                allOK = false;
            }
            // ตรวจสอบหมายเลขโทรศัพท์
            if (string.IsNullOrWhiteSpace(txtPhone.Text) || txtPhone.Text.Length < 10 || txtPhone.Text.Length > 10)
            {
                message += "กรุณากรอกหมายเลขโทรศัพท์ที่ถูกต้อง\n";
                txtPhone.Focus();
                allOK = false;
            }
            // ตรวจสอบตำแหน่ง
            if (string.IsNullOrWhiteSpace(txtPosition.Text))
            {
                message += "กรุณากรอกตำแหน่ง\n";
                txtPosition.Focus();
                allOK = false;
            }

            // ตรวจสอบอีเมล
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !ValidateEmail(txtEmail.Text))
            {
                message += "กรุณากรอกอีเมลที่ถูกต้อง\n";
                txtEmail.Focus();
                allOK = false;
            }
            // หากตรวจสอบไม่ผ่าน จะขึ้นข้อความแจ้งเตือน
            if (!allOK)
            {
                MessageBox.Show(message, "ข้อผิดพลาดในการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return allOK;
        }
        private bool ValidateEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(emailPattern);
            return regex.IsMatch(email);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            employeeManager.CancelCurrentEdit();
            if (myState.Equals("Add"))
            {
                employeeManager.Position = myEmp;
            }
            SetState("View");
        }

        private void buttonAddNew_Click(object sender, EventArgs e)
        {
            try
            { // ล้างรูปภาพใน PictureBox เมื่อกดปุ่ม Add
                myEmp = employeeManager.Position;
                employeeManager.AddNew();
                SetState("Add");
            }
            catch
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการเพิ่มข้อมูล", "ข้อผิดพลาด",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DialogResult response;
            response = MessageBox.Show("คุณแน่ใจหรือไม่ว่าต้องการลบข้อมูลนี้", "ลบ",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question,
                                        MessageBoxDefaultButton.Button2);

            if (response == DialogResult.No)
            {
                return;
            }

            try
            {
                // ลบข้อมูลออกจากฐานข้อมูล
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();

                    string deleteQuery = "DELETE FROM Employee WHERE Emp_ID = @EmployeeID";
                    SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                    deleteCmd.Parameters.AddWithValue("@EmployeeID", Convert.ToInt32(txtEmpID.Text));

                    int rowsAffected = deleteCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("ลบข้อมูลเรียบร้อยแล้ว", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // ลบข้อมูลออกจาก employeeTable ที่อยู่ในหน่วยความจำ
                        int rowIndex = employeeManager.Position;
                        employeeTable.Rows.RemoveAt(rowIndex);

                        // ปรับตำแหน่งใน employeeManager
                        if (employeeManager.Position >= employeeManager.Count)
                        {
                            employeeManager.Position = employeeManager.Count - 1; // กรณีที่ลบรายการสุดท้าย
                        }
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบข้อมูลที่ต้องการลบ", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการลบข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void formManageEmp_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myState.Equals("Edit") || myState.Equals("Add"))
            {
                MessageBox.Show("คุณต้องแก้ไขข้อมูลปัจจุบันให้เสร็จก่อนที่จะหยุดโปรแกรม", "",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                e.Cancel = true;
            }
            else
            {
                try
                {
                    SqlCommandBuilder employeeAdapterCommands
                        = new SqlCommandBuilder(employeeAdapter);
                    employeeAdapter.Update(employeeTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการบันทึกฐานข้อมูล: \r\n" + ex.Message,
                        "ข้อผิดพลาดในการบันทึก",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            // Close the connection
            FutsalConnection.Close();
            // Dispose of the objects
            FutsalConnection.Dispose();
            employeeCommand.Dispose();
            employeeAdapter.Dispose();
            employeeTable.Dispose();
        }

        private void buttonEditadd_Click(object sender, EventArgs e)
        {
            if (employeeManager.Count == 0)
            {
                MessageBox.Show("ไม่มีข้อมูลพนักงานให้แก้ไขที่อยู่", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ดึงค่า Emp_ID จาก employeeTable
            int EmpId = Convert.ToInt32(employeeTable.Rows[employeeManager.Position]["Emp_ID"]);

            // ตรวจสอบว่า EmpId นี้มี AddressId หรือยัง
            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                conn.Open();

                string selectAddressIdQuery = "SELECT Address_ID FROM Employee WHERE Emp_ID = @EmpId";
                SqlCommand selectAddressIdCmd = new SqlCommand(selectAddressIdQuery, conn);
                selectAddressIdCmd.Parameters.AddWithValue("@EmpId", EmpId);

                object addressIdObj = selectAddressIdCmd.ExecuteScalar();

                if (addressIdObj != null && addressIdObj != DBNull.Value)
                {
                    // EmpId นี้มี AddressId แล้ว ให้ดึงข้อมูลจาก EmployeeAddress มาแสดง
                    int addressId = Convert.ToInt32(addressIdObj);

                    // เรียกใช้ Constructor ที่รับ AddressId และ EmpId
                    formManageEmpAddress manageempForm = new formManageEmpAddress(addressId, EmpId);
                    manageempForm.ShowDialog();
                }
                else
                {
                    // EmpId นี้ยังไม่มี AddressId ให้เพิ่มที่อยู่ใหม่
                    // เรียกใช้ Constructor ที่รับ EmpId อย่างเดียว
                    formManageEmpAddress manageempForm = new formManageEmpAddress(EmpId);
                    manageempForm.ShowDialog();
                }
            }

        }
    }
}