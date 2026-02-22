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
    public partial class formPR : Form
    {
        private int prId;
        const string strFileName = "ConnectionString.ini";
        private string strConnectionString = "";
        public formPR(int prId)
        {
            InitializeComponent();
            this.prId = prId;
        }

        private void formPR_Load(object sender, EventArgs e)
        {
            if (File.Exists(strFileName))
            {
                try
                {
                    strConnectionString = File.ReadAllText(strFileName);  // อ่านข้อมูลจากไฟล์ .ini
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ไม่สามารถอ่านไฟล์ ConnectionString.ini: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("ไม่พบไฟล์ ConnectionString.ini", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            lbldepart.Text = "Mechanic";
            lbldate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            LoadData();
            if(CurrentUser.EmpPosition == "Mechanic")
            {
                btnSubmit.Enabled = false;
                btnCancel.Enabled = false;  
            }
        }
        private void LoadData()
        {
            string query = @"
            SELECT 
                pr.PR_ID,
                pr.PR_Date,
                pr.PR_Status,
                pr.Note,  -- เพิ่มการดึงคอลัมน์ Note
                orr.Product_Quantity,
                orr.Product_Price,
                s.SupplierName,  -- ดึงชื่อ Supplier จากตาราง Supplier
                p.Product_Name,
                pc.Product_Category
            FROM PurchaseRequest pr
            LEFT JOIN OrderRequest orr ON pr.PR_ID = orr.PR_ID
            LEFT JOIN Product p ON orr.Product_ID = p.Product_ID
            LEFT JOIN ProductCategory pc ON p.Product_Category_ID = pc.Product_Category_ID
            LEFT JOIN Supplier s ON orr.Supplier_ID = s.Supplier_ID 
            WHERE pr.PR_ID = @PR_ID";  // ใช้ PR_ID ในการดึงข้อมูล

            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@PR_ID", prId);  // ส่ง PR_ID ไปในคำสั่ง SQL
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                // ตรวจสอบว่ามีข้อมูลหรือไม่
                if (dataTable.Rows.Count > 0)
                {
                    // สร้างคอลัมน์ใน DataGridView
                    dataGridViewOrderRequest.Columns.Clear();
                    dataGridViewOrderRequest.Columns.Add("ItemNo", "Item No.");
                    dataGridViewOrderRequest.Columns.Add("Product_Name", "Product Name");
                    dataGridViewOrderRequest.Columns.Add("Product_Quantity", "Quantity");
                    dataGridViewOrderRequest.Columns.Add("Product_Price", "Price");

                    // ล้างข้อมูลเก่าก่อนที่จะเติมข้อมูลใหม่
                    dataGridViewOrderRequest.Rows.Clear();

                    // เพิ่มข้อมูลใน DataGridView
                    int rowNumber = 1;  // สำหรับลำดับรายการที่เริ่มจาก 1
                    foreach (DataRow row in dataTable.Rows)
                    {
                        dataGridViewOrderRequest.Rows.Add(
                            rowNumber++,  // ลำดับรายการ
                            row["Product_Name"].ToString(),
                            row["Product_Quantity"].ToString(),
                            row["Product_Price"].ToString()
                        );
                    }

                    // แสดงข้อมูล PR_ID และ PR_Date ใน Label
                    labelPRID.Text = dataTable.Rows[0]["PR_ID"].ToString();
                    if (dataTable.Rows[0]["PR_Date"] != DBNull.Value)
                    {
                        labelDate.Text = Convert.ToDateTime(dataTable.Rows[0]["PR_Date"]).ToString("yyyy-MM-dd HH:mm");
                    }
                    else
                    {
                        labelDate.Text = "No Date";
                    }

                    // แสดง Supplier ใน Label
                    if (dataTable.Rows[0]["SupplierName"] != DBNull.Value)
                    {
                        lblSupplier.Text = dataTable.Rows[0]["SupplierName"].ToString();  // เปลี่ยน labelSupplier เป็นชื่อ Supplier
                    }
                    else
                    {
                        lblSupplier.Text = "No Supplier";  // กรณีไม่พบข้อมูล Supplier
                    }

                    // แสดง Note ใน Label (ที่ชื่อว่า labelReason)
                    if (dataTable.Rows[0]["Note"] != DBNull.Value)
                    {
                        lblReason.Text = dataTable.Rows[0]["Note"].ToString();
                    }
                    else
                    {
                        lblReason.Text = " -";
                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูล PR_ID นี้", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                conn.Open();
                try
                {
                    // 1. เพิ่มข้อมูล PR_ID ลงในตาราง PurchaseOrder พร้อมกับ PO_Date
                    string insertPOQuery = @"
                INSERT INTO PurchaseOrder (PR_ID, PO_Date)
                VALUES (@PR_ID, @PO_Date);";

                    using (SqlCommand cmd = new SqlCommand(insertPOQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@PR_ID", prId);
                        cmd.Parameters.AddWithValue("@PO_Date", lbldate.Text);  // PO_Date จาก label
                        cmd.ExecuteNonQuery();
                    }

                    // 2. อัปเดต PR_Status เป็น 'Approve'
                    string updateStatusQuery = "UPDATE PurchaseRequest SET PR_Status = 'Approve' WHERE PR_ID = @PR_ID";

                    using (SqlCommand cmd = new SqlCommand(updateStatusQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@PR_ID", prId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("สถานะของคำขอซื้อได้ถูกเปลี่ยนเป็น 'Approve' เรียบร้อยแล้ว", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // รีเฟรชข้อมูลใน formAllPR หลังจาก submit สำเร็จ
                            formAllPR allPRForm = Application.OpenForms.OfType<formAllPR>().FirstOrDefault();
                            if (allPRForm != null)
                            {
                                allPRForm.LoadData();  // เรียกใช้เมธอด LoadData ใน formAllPR เพื่อรีเฟรชข้อมูล
                            }

                            this.Hide();  // ซ่อนฟอร์มปัจจุบัน (formPR)
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบข้อมูลที่เกี่ยวข้องกับ PR_ID นี้", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                conn.Open();
                try
                {
                    // 1. อัปเดต PR_Status เป็น 'Disapprove'
                    string updateStatusQuery = "UPDATE PurchaseRequest SET PR_Status = 'Disapprove' WHERE PR_ID = @PR_ID";

                    using (SqlCommand cmd = new SqlCommand(updateStatusQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@PR_ID", prId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("สถานะของคำขอซื้อได้ถูกเปลี่ยนเป็น 'Disapprove' เรียบร้อยแล้ว", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // รีเฟรชข้อมูลใน formAllPR หลังจาก cancel สำเร็จ
                            formAllPR allPRForm = Application.OpenForms.OfType<formAllPR>().FirstOrDefault();
                            if (allPRForm != null)
                            {
                                allPRForm.LoadData();  // เรียกใช้เมธอด LoadData ใน formAllPR เพื่อรีเฟรชข้อมูล
                            }

                            this.Hide();  // ซ่อนฟอร์มปัจจุบัน (formPR)
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบข้อมูลที่เกี่ยวข้องกับ PR_ID นี้", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

 
    }
}
