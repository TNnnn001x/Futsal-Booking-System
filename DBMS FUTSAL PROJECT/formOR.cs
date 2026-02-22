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
    public partial class formOR : Form
    {
        const string strFileName = "ConnectionString.ini";  // ไฟล์เก็บ connection string
        private string strConnectionString = "";
        public formOR()
        {
            InitializeComponent();
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

            // เชื่อมต่อกับฐานข้อมูล
            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                conn.Open();

                // สร้าง DataGridView และเพิ่มคอลัมน์
                dataGridViewOrderRequest.Columns.Clear();
                dataGridViewOrderRequest.Columns.Add("Product_Name", "Product Name");
                dataGridViewOrderRequest.Columns.Add("Product_Quantity", "Quantity");
                dataGridViewOrderRequest.Columns.Add("Product_Price", "Price");

                // เพิ่มคอลัมน์ Product_Category ให้เป็น ComboBox
                DataGridViewComboBoxColumn categoryComboBox = new DataGridViewComboBoxColumn();
                categoryComboBox.HeaderText = "Category";  // ชื่อคอลัมน์
                categoryComboBox.Name = "Product_Category";  // ให้แน่ใจว่าใช้ชื่อที่ตรง
                categoryComboBox.Width = 150;

                // เติมค่า ComboBox สำหรับ Product_Category โดยดึงข้อมูลจาก ProductCategory
                string getCategoryQuery = "SELECT Product_Category FROM ProductCategory";  // ใช้ชื่อคอลัมน์ 'Product_Category'
                SqlDataAdapter categoryAdapter = new SqlDataAdapter(getCategoryQuery, conn);
                DataTable categoryData = new DataTable();
                categoryAdapter.Fill(categoryData);

                categoryComboBox.DisplayMember = "Product_Category"; // ใช้ชื่อคอลัมน์ที่ถูกต้อง
                categoryComboBox.ValueMember = "Product_Category";
                categoryComboBox.DataSource = categoryData;

                // เพิ่มแค่คอลัมน์ Product_Category เท่านั้น
                dataGridViewOrderRequest.Columns.Add(categoryComboBox);

                // เติมค่า ComboBox สำหรับ Supplier โดยดึงข้อมูลจาก Supplier table
                string getSupplierQuery = "SELECT Supplier_ID, SupplierName FROM Supplier";  // ใช้ชื่อคอลัมน์ที่ถูกต้อง
                SqlDataAdapter supplierAdapter = new SqlDataAdapter(getSupplierQuery, conn);
                DataTable supplierData = new DataTable();
                supplierAdapter.Fill(supplierData);

                DataGridViewComboBoxColumn supplierComboBox = new DataGridViewComboBoxColumn();
                supplierComboBox.HeaderText = "Supplier";
                supplierComboBox.Name = "Supplier";  // ตั้งชื่อคอลัมน์ให้ตรง
                supplierComboBox.DisplayMember = "SupplierName";  // ใช้ชื่อที่แสดงใน ComboBox
                supplierComboBox.ValueMember = "Supplier_ID";  // ใช้ Supplier_ID เป็นค่าของ ComboBox
                supplierComboBox.DataSource = supplierData;

                // เพิ่ม ComboBox สำหรับ Supplier ใน DataGridView
                dataGridViewOrderRequest.Columns.Add(supplierComboBox);

                // ตั้งค่าความกว้างของคอลัมน์
                dataGridViewOrderRequest.Columns["Product_Name"].Width = 200;
                dataGridViewOrderRequest.Columns["Product_Quantity"].Width = 100;
                dataGridViewOrderRequest.Columns["Product_Price"].Width = 100;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                conn.Open();
                int prId = 0; // ตัวแปรสำหรับเก็บ PR_ID ที่จะใช้ใน OrderRequest

                // ถ้าไม่มี PR_ID (เช่นในกรณีที่ยังไม่เคยมีการสร้าง PR_ID), ให้สร้างใหม่
                string checkPRQuery = "SELECT PR_ID FROM PurchaseRequest WHERE PR_Status IS NULL"; // สมมติว่า PR_Status ใช้เพื่อระบุการสั่งซื้อที่ยังไม่ถูกปิด
                using (SqlCommand checkPRCmd = new SqlCommand(checkPRQuery, conn))
                {
                    object result = checkPRCmd.ExecuteScalar();  // ดึง PR_ID ล่าสุด
                    if (result != null)
                    {
                        prId = Convert.ToInt32(result); // ใช้ PR_ID ที่มีอยู่แล้ว
                    }
                    else
                    {
                        // สร้าง PR_ID ใหม่ในกรณีที่ยังไม่มี
                        string insertPRQuery = "INSERT INTO PurchaseRequest (PR_Status, PR_Date) VALUES ('Pending', GETDATE()); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand insertPRCmd = new SqlCommand(insertPRQuery, conn))
                        {
                            prId = Convert.ToInt32(insertPRCmd.ExecuteScalar());
                        }
                    }
                }

                // บันทึกข้อมูลสินค้าลงใน OrderRequest พร้อมกับ PR_ID
                foreach (DataGridViewRow row in dataGridViewOrderRequest.Rows)
                {
                    // ตรวจสอบให้แน่ใจว่าค่าของ Product_Name, Product_Quantity, Product_Price, Product_Category และ Supplier ไม่เป็น null
                    if (row.Cells["Product_Name"].Value != null && row.Cells["Product_Quantity"].Value != null && row.Cells["Product_Price"].Value != null && row.Cells["Product_Category"].Value != null && row.Cells["Supplier"].Value != null)
                    {
                        string productName = row.Cells["Product_Name"].Value.ToString();
                        int quantity = Convert.ToInt32(row.Cells["Product_Quantity"].Value);
                        decimal price = Convert.ToDecimal(row.Cells["Product_Price"].Value);  // ดึง Product_Price
                        string productCategory = row.Cells["Product_Category"].Value.ToString();  // ใช้ชื่อคอลัมน์ Product_Category
                        int supplierId = Convert.ToInt32(row.Cells["Supplier"].Value);  // ดึงค่า Supplier_ID จาก ComboBox

                        // ค้นหา Product_ID จาก Product_Name
                        string getProductIdQuery = "SELECT Product_ID FROM Product WHERE Product_Name = @Product_Name";
                        int productId = 0;

                        using (SqlCommand getProductIdCmd = new SqlCommand(getProductIdQuery, conn))
                        {
                            getProductIdCmd.Parameters.AddWithValue("@Product_Name", productName);
                            object result = getProductIdCmd.ExecuteScalar();

                            if (result != null)
                            {
                                productId = Convert.ToInt32(result);
                            }
                            else
                            {
                                // ถ้าไม่พบ Product_Name ให้เพิ่มสินค้าใหม่
                                string insertProductQuery = "INSERT INTO Product (Product_Name, Product_Category_ID) VALUES (@Product_Name, @Product_Category_ID); SELECT SCOPE_IDENTITY();";
                                int productCategoryId = 0;

                                using (SqlCommand getCategoryIdCmd = new SqlCommand("SELECT Product_Category_ID FROM ProductCategory WHERE Product_Category = @Product_Category", conn))
                                {
                                    getCategoryIdCmd.Parameters.AddWithValue("@Product_Category", productCategory);
                                    productCategoryId = Convert.ToInt32(getCategoryIdCmd.ExecuteScalar());
                                }

                                using (SqlCommand insertProductCmd = new SqlCommand(insertProductQuery, conn))
                                {
                                    insertProductCmd.Parameters.AddWithValue("@Product_Name", productName);
                                    insertProductCmd.Parameters.AddWithValue("@Product_Category_ID", productCategoryId);

                                    productId = Convert.ToInt32(insertProductCmd.ExecuteScalar());
                                }
                            }
                        }

                        // บันทึกข้อมูลสินค้าใน OrderRequest โดยใช้ Product_ID, PR_ID, Product_Price และ Supplier_ID
                        string insertOrderRequestQuery = "INSERT INTO OrderRequest (PR_ID, Product_ID, Product_Quantity, Product_Price, Supplier_ID) VALUES (@PR_ID, @Product_ID, @Product_Quantity, @Product_Price, @Supplier_ID)";
                        using (SqlCommand insertOrderCmd = new SqlCommand(insertOrderRequestQuery, conn))
                        {
                            insertOrderCmd.Parameters.AddWithValue("@PR_ID", prId);
                            insertOrderCmd.Parameters.AddWithValue("@Product_ID", productId);
                            insertOrderCmd.Parameters.AddWithValue("@Product_Quantity", quantity);
                            insertOrderCmd.Parameters.AddWithValue("@Product_Price", price);  // บันทึก Product_Price ใน OrderRequest
                            insertOrderCmd.Parameters.AddWithValue("@Supplier_ID", supplierId);  // บันทึก Supplier_ID ใน OrderRequest
                            insertOrderCmd.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("ข้อมูลสินค้าถูกบันทึกเรียบร้อยแล้ว!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void buttonAddCategory_Click(object sender, EventArgs e)
        {
            // เปิดกล่องข้อความให้ผู้ใช้พิมพ์ชื่อ Product Category ใหม่
            string newCategory = Microsoft.VisualBasic.Interaction.InputBox("กรุณากรอกชื่อ Category ใหม่", "เพิ่ม Category", "");

            // ตรวจสอบว่าไม่ว่าง
            if (!string.IsNullOrWhiteSpace(newCategory))
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();

                    // ตรวจสอบว่า Category ที่พิมพ์มาใหม่มีอยู่ในฐานข้อมูลหรือยัง
                    string checkCategoryQuery = "SELECT COUNT(*) FROM ProductCategory WHERE Product_Category = @Product_Category";
                    using (SqlCommand checkCmd = new SqlCommand(checkCategoryQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@Product_Category", newCategory);
                        int exists = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (exists == 0) // หากยังไม่มีในฐานข้อมูล
                        {
                            // เพิ่ม Category ใหม่ในฐานข้อมูล
                            string insertCategoryQuery = "INSERT INTO ProductCategory (Product_Category) VALUES (@Product_Category)";
                            using (SqlCommand insertCmd = new SqlCommand(insertCategoryQuery, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@Product_Category", newCategory);
                                insertCmd.ExecuteNonQuery();
                            }
                            MessageBox.Show("เพิ่ม Category ใหม่เรียบร้อยแล้ว", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // รีเฟรช ComboBox ที่แสดงใน DataGridView
                            LoadProductCategoryData();
                        }
                        else
                        {
                            MessageBox.Show("Category นี้มีอยู่แล้วในฐานข้อมูล", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("กรุณากรอกชื่อ Category", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadProductCategoryData()
        {
            // เชื่อมต่อกับฐานข้อมูล
            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                conn.Open();

                // ดึงข้อมูล Product_Category จากฐานข้อมูล
                string getCategoryQuery = "SELECT Product_Category FROM ProductCategory";
                SqlDataAdapter categoryAdapter = new SqlDataAdapter(getCategoryQuery, conn);
                DataTable categoryData = new DataTable();
                categoryAdapter.Fill(categoryData);

                // รีเฟรช ComboBox ใน DataGridView
                DataGridViewComboBoxColumn categoryComboBox = (DataGridViewComboBoxColumn)dataGridViewOrderRequest.Columns["Product_Category"];
                categoryComboBox.DataSource = categoryData;
                categoryComboBox.DisplayMember = "Product_Category";
                categoryComboBox.ValueMember = "Product_Category";
            }
        }


    }
}
