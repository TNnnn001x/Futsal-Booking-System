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
using System.Drawing.Printing;

namespace DBMS_FUTSAL_PROJECT
{
    public partial class formAllPO : Form
    {
        const string strFileName = "ConnectionString.ini";
        private string strConnectionString = "";

        public formAllPO()
        {
            InitializeComponent();
        }

        private void formAllPO_Load(object sender, EventArgs e)
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
            LoadData();


        }

        private void LoadData()
        {
            // ปรับคำสั่ง SQL ให้ดึงข้อมูลจาก PO_ID
            string query = @"
            SELECT DISTINCT
                po.PO_ID,
                po.PO_Date,
                po.PR_ID,
                pr.PR_Status,
                s.SupplierName
            FROM PurchaseOrder po
            LEFT JOIN PurchaseRequest pr ON po.PR_ID = pr.PR_ID
            LEFT JOIN OrderRequest o ON pr.PR_ID = o.PR_ID
            LEFT JOIN Supplier s ON o.Supplier_ID = s.Supplier_ID
            WHERE pr.PR_Status = 'Approve'";  // กรองข้อมูลตามสถานะ

            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                // เคลียร์ข้อมูลเก่าของ DataGridView ก่อนที่จะเติมข้อมูลใหม่
                dataGridViewAllPO.Rows.Clear();

                // ตรวจสอบว่า DataGridView มีคอลัมน์ที่จำเป็นหรือไม่
                if (dataGridViewAllPO.Columns.Count == 0)
                {
                    // กำหนดคอลัมน์ใน DataGridView
                    dataGridViewAllPO.Columns.Add("PO_ID", "PO ID");
                    dataGridViewAllPO.Columns.Add("PO_Date", "PO Date");
                    dataGridViewAllPO.Columns.Add("PR_Status", "PR Status");
                    dataGridViewAllPO.Columns.Add("Supplier", "Supplier");


                    // ปรับขนาดคอลัมน์
                    dataGridViewAllPO.Columns["PO_ID"].Width = 120;
                    dataGridViewAllPO.Columns["PO_Date"].Width = 150;
                    dataGridViewAllPO.Columns["PR_Status"].Width = 100;
                    dataGridViewAllPO.Columns["Supplier"].Width = 150;
                }

                // สร้างข้อมูลใน DataGridView จาก DataTable
                foreach (DataRow row in dataTable.Rows)
                {
                    // เพิ่มแถวใหม่ใน DataGridView
                    dataGridViewAllPO.Rows.Add(
                        row["PO_ID"].ToString(),
                        row["PO_Date"] != DBNull.Value ? Convert.ToDateTime(row["PO_Date"]).ToString("yyyy-MM-dd HH:mm") : "No Date",
                        row["PR_Status"].ToString(),
                        row["SupplierName"].ToString()
                    );
                }
                SetupDataGridView();
            }
        }
        private void SetupDataGridView()
        {
            if (!dataGridViewAllPO.Columns.Contains("Action"))
            {
                // เพิ่มคอลัมน์ปุ่มเพื่อดูรายละเอียด
                DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
                btnColumn.HeaderText = "";
                btnColumn.Name = "Action";
                btnColumn.Text = "View Details";
                btnColumn.UseColumnTextForButtonValue = true;  // ใช้ข้อความเป็นปุ่ม
                dataGridViewAllPO.Columns.Add(btnColumn);

            }
        }

        private void dataGridViewAllPO_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // ตรวจสอบว่าเป็นการคลิกในคอลัมน์ปุ่ม
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewAllPO.Columns["Action"].Index)
            {
                // ดึง PO_ID จากแถวที่เลือก
                int selectedPOID = int.Parse(dataGridViewAllPO.Rows[e.RowIndex].Cells["PO_ID"].Value.ToString());

                // เปิด formPO และส่ง PO_ID ไป
                formPO frmPO = new formPO(selectedPOID);
                frmPO.Show();
            }

        }

        private void btnBack_Click(object sender, EventArgs e)
        {

        }
    }
}