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
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace DBMS_FUTSAL_PROJECT
{
    public partial class formAllPR : Form
    {
        const string strFileName = "ConnectionString.ini";
        private string strConnectionString = "";
        public formAllPR()
        {
            InitializeComponent();
        }

        private void formAllPR_Load(object sender, EventArgs e)
        {
            // อ่านค่า connection string จากไฟล์ .ini
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
            LoadData();
        }


        public void LoadData()
        {
            // ตั้งคำสั่ง SQL ที่ต้องการดึงข้อมูล
            
            string query = @"
                    SELECT
                        pr.PR_ID,
                        pr.PR_Date,
                        pr.PR_Status,
                        orr.Product_Quantity,
                        orr.Product_Price,
                        s.SupplierName,  --ดึงชื่อ Supplier จากตาราง Supplier
                        p.Product_Name,
                        pc.Product_Category
                    FROM PurchaseRequest pr
                    LEFT JOIN OrderRequest orr ON pr.PR_ID = orr.PR_ID
                    LEFT JOIN Product p ON orr.Product_ID = p.Product_ID
                    LEFT JOIN ProductCategory pc ON p.Product_Category_ID = pc.Product_Category_ID
                    LEFT JOIN Supplier s ON orr.Supplier_ID = s.Supplier_ID-- เชื่อมกับ Supplier โดยใช้ Supplier_ID
                    WHERE pr.PR_Status = 'Pending'";

            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                // เคลียร์ข้อมูลเก่าของ ListView ก่อนที่จะเติมข้อมูลใหม่
                listViewAllPR.Items.Clear();

                // กำหนดคอลัมน์ใน ListView ถ้ายังไม่กำหนด
                if (listViewAllPR.Columns.Count == 0)
                {
                    listViewAllPR.View = View.Details;
                    listViewAllPR.FullRowSelect = true;
                    listViewAllPR.GridLines = true;
                    listViewAllPR.Columns.Add("Purchase Request ID", 120, HorizontalAlignment.Center);
                    listViewAllPR.Columns.Add("Request Date", 150, HorizontalAlignment.Center);
                    listViewAllPR.Columns.Add("Status", 100, HorizontalAlignment.Center);
                    listViewAllPR.Columns.Add("Quantity", 100, HorizontalAlignment.Center);
                    listViewAllPR.Columns.Add("Price", 100, HorizontalAlignment.Center);
                    listViewAllPR.Columns.Add("Supplier", 150, HorizontalAlignment.Center);
                    listViewAllPR.Columns.Add("Product Name", 150, HorizontalAlignment.Center);
                }

                // สร้างข้อมูลใน ListView จาก DataTable
                foreach (DataRow row in dataTable.Rows)
                {
                    // สร้าง Item ใหม่ใน ListView
                    ListViewItem item = new ListViewItem(row["PR_ID"].ToString());

                    // ตรวจสอบว่า PR_Date เป็น NULL หรือไม่
                    if (row["PR_Date"] != DBNull.Value)
                    {
                        item.SubItems.Add(Convert.ToDateTime(row["PR_Date"]).ToString("yyyy-MM-dd HH:mm"));
                    }
                    else
                    {
                        item.SubItems.Add("No Date");
                    }

                    item.SubItems.Add(row["PR_Status"].ToString());
                    item.SubItems.Add(row["Product_Quantity"].ToString());
                    item.SubItems.Add(row["Product_Price"].ToString());
                    item.SubItems.Add(row["SupplierName"].ToString());
                    item.SubItems.Add(row["Product_Name"].ToString());

                    // เพิ่ม Item ไปใน ListView
                    listViewAllPR.Items.Add(item);
                }
            }
        }

        private void listViewAllPR_Click(object sender, EventArgs e)
        {
            if (listViewAllPR.SelectedItems.Count > 0)
            {
                // ดึงค่า PR_ID จากแถวที่เลือก
                ListViewItem selectedItem = listViewAllPR.SelectedItems[0];
                int prId = int.Parse(selectedItem.Text);  // สมมติว่า PR_ID อยู่ในคอลัมน์แรก

                // เปิด formPR และส่ง PR_ID ที่เลือกไป
                formPR detailForm = new formPR(prId);  // สร้าง formPR ใหม่พร้อมส่ง PR_ID
                detailForm.Show();  // แสดง formPR
            }
        }
    }

}
