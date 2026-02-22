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
    public partial class formManageEmpAddress : Form
    {
        private int _empId;  // ตัวแปรเก็บค่า Member_ID
        const string strFileName = "ConnectionString.ini";
        private string strConnectionString = "";
        SqlConnection dbConnection;
        SqlCommand addressCommand;
        SqlDataAdapter addressAdapter;
        DataTable addressTable;
        CurrencyManager addressManager;
        string currentState;
        public formManageEmpAddress(int empId)
        {
            InitializeComponent();
            _empId = empId;
            _addressId = -1; // ค่าเริ่มต้น AddressId, -1 แสดงว่าเป็นการสร้างใหม่
        }

        public formManageEmpAddress(int addressId, int empId)
        {
            InitializeComponent();
            _addressId = addressId;
            _empId = empId;
        }

        private int _addressId;

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();

                    // เพิ่มข้อมูลที่อยู่ใหม่ และดึง Address_ID ที่สร้างขึ้น
                    string insertQuery = "INSERT INTO EmployeeAddress (Address, Subdistrict, District, Province, PostCode) " +
                                       "OUTPUT INSERTED.Address_ID " +
                                       "VALUES (@Address, @Subdistrict, @District, @Province, @PostCode)";

                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);

                    insertCmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    insertCmd.Parameters.AddWithValue("@Subdistrict", txtSubdistrict.Text);
                    insertCmd.Parameters.AddWithValue("@District", txtDistrict.Text);
                    insertCmd.Parameters.AddWithValue("@Province", txtProvince.Text);
                    insertCmd.Parameters.AddWithValue("@PostCode", txtPostCode.Text);

                    int newAddressId = (int)insertCmd.ExecuteScalar();

                    // อัปเดตตาราง Employee โดยใช้ Address_ID ที่สร้างขึ้น
                    string updateEmployeeQuery = "UPDATE Employee SET Address_ID = @AddressId WHERE Emp_ID = @EmpId";
                    SqlCommand updateEmployeeCmd = new SqlCommand(updateEmployeeQuery, conn);
                    updateEmployeeCmd.Parameters.AddWithValue("@AddressId", newAddressId);
                    updateEmployeeCmd.Parameters.AddWithValue("@EmpId", _empId);
                    int rowsAffected = updateEmployeeCmd.ExecuteNonQuery(); // เพิ่มบรรทัดนี้เพื่อตรวจสอบจำนวนแถวที่ได้รับผลกระทบ

                    if (rowsAffected == 0)
                    {
                        MessageBox.Show("ไม่สามารถอัปเดต Address_ID ในตาราง Employee ได้"); // เพิ่มบรรทัดนี้เพื่อตรวจสอบว่าอัปเดตสำเร็จหรือไม่
                    }

                    // ดึงข้อมูลที่อยู่ที่สร้างขึ้นมาแสดง
                    string selectQuery = "SELECT * FROM EmployeeAddress WHERE Address_ID = @AddressId";
                    SqlCommand selectCmd = new SqlCommand(selectQuery, conn);
                    selectCmd.Parameters.AddWithValue("@AddressId", newAddressId);

                    SqlDataAdapter addressAdapter = new SqlDataAdapter(selectCmd);
                    addressTable = new DataTable();
                    addressAdapter.Fill(addressTable);

                    if (addressTable.Rows.Count > 0)
                    {
                        DataRow row = addressTable.Rows[0];
                        txtAddress.Text = row["Address"].ToString();
                        txtSubdistrict.Text = row["Subdistrict"].ToString();
                        txtDistrict.Text = row["District"].ToString();
                        txtProvince.Text = row["Province"].ToString();
                        txtPostCode.Text = row["PostCode"].ToString();
                    }

                    MessageBox.Show("บันทึกข้อมูลที่อยู่เรียบร้อยแล้ว!", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    SetState("View");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            addressManager.CancelCurrentEdit();
            SetState("View");
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            SetState("Edit");
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณแน่ใจหรือไม่ว่าต้องการลบข้อมูลนี้?", "ลบข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();

                    // ดึง Address_ID จาก Employee
                    string selectAddressIdQuery = "SELECT Address_ID FROM Employee WHERE Emp_ID = @EmpId";
                    SqlCommand selectAddressIdCmd = new SqlCommand(selectAddressIdQuery, conn);
                    selectAddressIdCmd.Parameters.AddWithValue("@EmpId", _empId);
                    int addressIdToDelete = Convert.ToInt32(selectAddressIdCmd.ExecuteScalar());

                    // ตั้งค่า Address_ID ใน Employee เป็น NULL ก่อน
                    string updateEmployeeQuery = "UPDATE Employee SET Address_ID = NULL WHERE Emp_ID = @EmpId";
                    SqlCommand updateEmployeeCmd = new SqlCommand(updateEmployeeQuery, conn);
                    updateEmployeeCmd.Parameters.AddWithValue("@EmpId", _empId);
                    updateEmployeeCmd.ExecuteNonQuery();
                    // เคลียร์ TextBox
                    txtAddress.Text = "";
                    txtSubdistrict.Text = "";
                    txtDistrict.Text = "";
                    txtProvince.Text = "";
                    txtPostCode.Text = "";

                    // ลบข้อมูลที่อยู่จาก EmployeeAddress
                    string deleteQuery = "DELETE FROM EmployeeAddress WHERE Address_ID = @AddressId";
                    SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                    deleteCmd.Parameters.AddWithValue("@AddressId", addressIdToDelete);
                    deleteCmd.ExecuteNonQuery();

                    // สร้าง SqlDataAdapter ใหม่เพื่อเติมข้อมูล
                    SqlDataAdapter addressAdapter = new SqlDataAdapter("SELECT * FROM EmployeeAddress", conn);
                    addressTable.Clear();
                    addressAdapter.Fill(addressTable);

                    MessageBox.Show("ลบข้อมูลที่อยู่เรียบร้อยแล้ว!", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetState("View");
                }
            }
        }

        private void formManageEmpAddress_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(strFileName))
                    strConnectionString = File.ReadAllText(strFileName, Encoding.GetEncoding("Windows-874"));

                if (string.IsNullOrEmpty(strConnectionString))
                {
                    MessageBox.Show("ไม่สามารถอ่านค่า Connection String ได้", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_addressId == -1)
                {
                    // สร้างใหม่
                    MessageBox.Show("กรุณากรอกข้อมูลที่อยู่ใหม่", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetState("Edit");
                }
                else
                {
                    // แก้ไข
                    using (SqlConnection conn = new SqlConnection(strConnectionString))
                    {
                        conn.Open();
                        string selectQuery = "SELECT * FROM EmployeeAddress WHERE Address_ID = @AddressId";
                        SqlCommand selectCmd = new SqlCommand(selectQuery, conn);
                        selectCmd.Parameters.AddWithValue("@AddressId", _addressId);

                        SqlDataAdapter addressAdapter = new SqlDataAdapter(selectCmd);
                        addressTable = new DataTable();
                        addressAdapter.Fill(addressTable);

                        if (addressTable.Rows.Count > 0)
                        {
                            DataRow row = addressTable.Rows[0];
                            txtAddress.Text = row["Address"].ToString();
                            txtSubdistrict.Text = row["Subdistrict"].ToString();
                            txtDistrict.Text = row["District"].ToString();
                            txtProvince.Text = row["Province"].ToString();
                            txtPostCode.Text = row["PostCode"].ToString();
                            SetState("View");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetState(string state)
        {
            currentState = state;
            switch (state)
            {
                case "View":
                    txtAddress.ReadOnly = true;
                    txtSubdistrict.ReadOnly = true;
                    txtDistrict.ReadOnly = true;
                    txtProvince.ReadOnly = true;
                    txtPostCode.ReadOnly = true;
                    buttonEdit.Enabled = true;
                    buttonDelete.Enabled = true;
                    buttonSave.Enabled = false;
                    buttonCancel.Enabled = false;
                    buttonDone.Enabled = true;
                    break;

                default:
                    txtAddress.ReadOnly = false;
                    txtSubdistrict.ReadOnly = false;
                    txtDistrict.ReadOnly = false;
                    txtProvince.ReadOnly = false;
                    txtPostCode.ReadOnly = false;
                    buttonEdit.Enabled = false;
                    buttonDelete.Enabled = false;
                    buttonSave.Enabled = true;
                    buttonCancel.Enabled = true;
                    buttonDone.Enabled = false;
                    break;
            }
        }

        private void buttonBackMCS_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}