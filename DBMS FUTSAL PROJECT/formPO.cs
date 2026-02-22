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
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace DBMS_FUTSAL_PROJECT
{
    public partial class formPO : Form
    {
        private int poId;
        const string strFileName = "ConnectionString.ini";
        private string strConnectionString = "";

        public formPO(int poId)
        {
            InitializeComponent();
            this.poId = poId;
        }

        private void formPO_Load(object sender, EventArgs e)
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
            LoadPaymentMethods();
            LoadData();
            CheckPOIDExists();
            if (CurrentUser.EmpPosition == "Mechanic")
            {
                btnSave.Enabled = false;
            }
        }

        private void LoadData()
        {
            string query = @"
            SELECT 
                po.PO_ID,
                po.PO_Date,
                po.PR_ID,
                s.SupplierName,
                sa.Address,
                s.ContactNumber,
                orr.Product_Quantity,
                orr.Product_Price,
                p.Product_Name
            FROM PurchaseOrder po
            LEFT JOIN PurchaseRequest pr ON po.PR_ID = pr.PR_ID
            LEFT JOIN OrderRequest orr ON pr.PR_ID = orr.PR_ID
            LEFT JOIN Product p ON orr.Product_ID = p.Product_ID
            LEFT JOIN Supplier s ON orr.Supplier_ID = s.Supplier_ID
            LEFT JOIN SupplierAddress sa ON s.SupplierAddress_ID = sa.SupplierAddress_ID
            WHERE po.PO_ID = @PO_ID
            ";

            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@PO_ID", poId);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    // แสดงข้อมูลใน Labels
                    lblPOID.Text = dataTable.Rows[0]["PO_ID"].ToString();
                    lblPODate.Text = Convert.ToDateTime(dataTable.Rows[0]["PO_Date"]).ToString("yyyy-MM-dd HH:mm");
                    lblAttn.Text = dataTable.Rows[0]["SupplierName"].ToString();
                    lblAdd.Text = dataTable.Rows[0]["Address"].ToString();
                    lblTel.Text = dataTable.Rows[0]["ContactNumber"].ToString();

                    // แสดง Payment Method ใน ComboBox // Set Payment Method

                    // สร้างคอลัมน์ใน DataGridView
                    dataGridViewPO.Columns.Clear();
                    dataGridViewPO.Columns.Add("ItemNo", "Item No.");
                    dataGridViewPO.Columns.Add("Product_Name", "Product Name");
                    dataGridViewPO.Columns.Add("Product_Quantity", "Quantity");
                    dataGridViewPO.Columns.Add("Product_Price", "Price");

                    // ล้างข้อมูลเก่าก่อนที่จะเติมข้อมูลใหม่
                    dataGridViewPO.Rows.Clear();

                    // เพิ่มข้อมูลใน DataGridView
                    int rowNumber = 1;  // สำหรับลำดับรายการที่เริ่มจาก 1
                    foreach (DataRow row in dataTable.Rows)
                    {
                        dataGridViewPO.Rows.Add(
                            rowNumber++,  // ลำดับรายการ
                            row["Product_Name"].ToString(),
                            row["Product_Quantity"].ToString(),
                            row["Product_Price"].ToString()
                        );
                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูล PO_ID นี้", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadPaymentMethods()
        {
            string query = "SELECT Payment_Method_ID, Payment_Method FROM PaymentMethod";

            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                DataTable paymentMethods = new DataTable();
                dataAdapter.Fill(paymentMethods);

                // เพิ่มข้อมูลใน ComboBox สำหรับ Payment Method
                comboPayment.DisplayMember = "Payment_Method";  // ชื่อที่จะแสดงใน ComboBox
                comboPayment.ValueMember = "Payment_Method_ID";  // ค่าที่จะถูกเลือกและใช้งาน
                comboPayment.DataSource = paymentMethods;
            }
        }
        private void PrintInvoice(object sender, PrintPageEventArgs e)
        {
            // รับข้อมูลจากฟอร์ม
            string company = lblAttn.Text;  // ชื่อบริษัทจากฟอร์ม (สามารถใส่ค่าจริงจาก TextBox หรือ Label)
            string address = lblAdd.Text;  // ที่อยู่จาก labelAdd
            string tel = lblTel.Text;  // โทรศัพท์จาก labelTel
            string date = lblPODate.Text;  // วันที่จาก labelPODate
            string poNumber = lblPOID.Text;  // PO ID จาก labelPOID
            string paymentMethod = comboPayment.Text;  // Payment Method จาก ComboBox

            // กำหนดฟอนต์ที่ใช้ในการพิมพ์
            Font fontTitle = new Font("Arial", 14, FontStyle.Bold);
            Font fontNormal = new Font("Arial", 12);
            Brush brush = Brushes.Black;

            // ตั้งค่าตำแหน่งในการพิมพ์
            float x = 50;
            float y = 50;
            float lineHeight = fontNormal.GetHeight(e.Graphics);

            // พิมพ์ข้อมูลบริษัท
            e.Graphics.DrawString(company, fontTitle, brush, x, y);
            y += lineHeight + 10;
            e.Graphics.DrawString("ที่อยู่: " + address, fontNormal, brush, x, y);
            y += lineHeight;
            e.Graphics.DrawString("โทร: " + tel, fontNormal, brush, x, y);
            y += lineHeight + 20;

            // พิมพ์หัวข้อใบสั่งซื้อ
            e.Graphics.DrawString("ใบสั่งซื้อ / Purchase Order", fontTitle, brush, x, y);
            y += lineHeight + 10;

            // ข้อมูลในใบสั่งซื้อ
            e.Graphics.DrawString("วันที่ / Date: " + date, fontNormal, brush, x, y);
            y += lineHeight;
            e.Graphics.DrawString("เลขที่ใบสั่งซื้อ / No.: " + poNumber, fontNormal, brush, x, y);
            y += lineHeight;
            e.Graphics.DrawString("เงื่อนไขการชำระเงิน: " + paymentMethod, fontNormal, brush, x, y);
            y += lineHeight;

            // พิมพ์หัวข้อรายละเอียดสินค้า
            y += 20;  // เพิ่มช่องว่าง
            e.Graphics.DrawString("รายละเอียดสินค้า", fontNormal, brush, x, y);
            y += lineHeight;

            // สร้างตารางสินค้า
            e.Graphics.DrawString("รายการ", fontNormal, brush, x, y);
            e.Graphics.DrawString("จำนวน", fontNormal, brush, x + 250, y);
            e.Graphics.DrawString("ราคาต่อหน่วย", fontNormal, brush, x + 350, y);
            e.Graphics.DrawString("จำนวนเงิน", fontNormal, brush, x + 450, y);
            y += lineHeight;

            // ข้อมูลสินค้าจาก DataGridView
            foreach (DataGridViewRow row in dataGridViewPO.Rows)
            {
                if (row.IsNewRow) continue; // ข้ามแถวใหม่
                e.Graphics.DrawString(row.Cells["Product_Name"].Value.ToString(), fontNormal, brush, x, y);
                e.Graphics.DrawString(row.Cells["Product_Quantity"].Value.ToString(), fontNormal, brush, x + 250, y);
                e.Graphics.DrawString(row.Cells["Product_Price"].Value.ToString(), fontNormal, brush, x + 350, y);
                e.Graphics.DrawString((Convert.ToDecimal(row.Cells["Product_Quantity"].Value) * Convert.ToDecimal(row.Cells["Product_Price"].Value)).ToString(), fontNormal, brush, x + 450, y);
                y += lineHeight;
            }

            // พิมพ์ข้อมูลด้านล่าง (รวมราคา, การลงลายเซ็นต์)
            y += 20;
            e.Graphics.DrawString("รวมราคา", fontNormal, brush, x + 450, y);
            // คำนวณรวมราคา
            decimal total = 0;
            foreach (DataGridViewRow row in dataGridViewPO.Rows)
            {
                if (row.IsNewRow) continue;
                total += Convert.ToDecimal(row.Cells["Product_Quantity"].Value) * Convert.ToDecimal(row.Cells["Product_Price"].Value);
            }
            e.Graphics.DrawString(total.ToString("N2"), fontNormal, brush, x + 550, y);
            y += lineHeight + 30;

            // ลายเซ็นต์
            e.Graphics.DrawString("ผู้สั่งซื้อ: ____________________   ผู้อนุมัติ: ____________________", fontNormal, brush, x, y);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่ามีการบันทึก Payment หรือยัง
            if (IsPaymentRecorded())
            {
                // สร้าง PrintDocument
                PrintDocument printDocument = new PrintDocument();

                // ผูกกับเหตุการณ์ PrintPage
                printDocument.PrintPage += new PrintPageEventHandler(PrintInvoice);

                PrintPreviewDialog previewDialog = new PrintPreviewDialog();
                previewDialog.Document = printDocument;

                if (previewDialog.ShowDialog() == DialogResult.OK)
                {
                    printDocument.Print();
                }
            }
            else
            {
                MessageBox.Show("กรุณาบันทึกการชำระเงินก่อนทำการพิมพ์", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private bool IsPaymentRecorded()
        {
            bool isRecorded = false;

            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                conn.Open();
                string checkQuery = "SELECT COUNT(*) FROM Payment WHERE PO_ID = @PO_ID";

                using (SqlCommand cmd = new SqlCommand(checkQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@PO_ID", poId);
                    int count = (int)cmd.ExecuteScalar();

                    isRecorded = count > 0;
                }
            }

            return isRecorded;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                conn.Open();

                // คำสั่ง SQL เพื่อบันทึกข้อมูลลงในตาราง Payment
                string insertPaymentQuery = @"
            INSERT INTO Payment (PO_ID, Payment_Method_ID, Payment_Date, Payment_Status, Payment_Amount)
            VALUES (@PO_ID, @Payment_Method_ID, @Payment_Date, @Payment_Status, @Payment_Amount);";

                using (SqlCommand cmd = new SqlCommand(insertPaymentQuery, conn))
                {
                    // กำหนดค่าให้กับพารามิเตอร์
                    cmd.Parameters.AddWithValue("@PO_ID", poId);  // ใช้ PO_ID ที่รับมาจากฟอร์ม
                    cmd.Parameters.AddWithValue("@Payment_Method_ID", comboPayment.SelectedValue);  // Payment Method จาก ComboBox
                    cmd.Parameters.AddWithValue("@Payment_Date", DateTime.Now);  // วันที่การชำระเงิน
                    cmd.Parameters.AddWithValue("@Payment_Status", "Paid");  // สถานะการชำระเงิน (สามารถปรับเป็นค่าที่ต้องการ)
                    cmd.Parameters.AddWithValue("@Payment_Amount", CalculateTotalAmount());  // คำนวณยอดรวมทั้งหมดจาก DataGridView

                    // ทำการบันทึกข้อมูล
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("บันทึกข้อมูลการชำระเงินเรียบร้อยแล้ว!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("เกิดข้อผิดพลาดในการบันทึกข้อมูล!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            CheckPOIDExists();

        }
        private decimal CalculateTotalAmount()
        {
            decimal totalAmount = 0;

            foreach (DataGridViewRow row in dataGridViewPO.Rows)
            {
                if (row.IsNewRow) continue;

                decimal quantity = Convert.ToDecimal(row.Cells["Product_Quantity"].Value);
                decimal price = Convert.ToDecimal(row.Cells["Product_Price"].Value);
                totalAmount += quantity * price;
            }

            return totalAmount;
        }
        private void CheckPOIDExists()
        {
            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                conn.Open();
                string checkQuery = "SELECT COUNT(*) FROM Payment WHERE PO_ID = @PO_ID";

                using (SqlCommand cmd = new SqlCommand(checkQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@PO_ID", poId);
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        // ถ้า PO_ID มีอยู่ในตาราง Payment แล้ว ให้ทำการปิดการใช้งาน ComboBox และปุ่ม Save
                        comboPayment.Enabled = false;  // ปิดการใช้งาน ComboBox
                        btnSave.Enabled = false;  // ปิดการใช้งานปุ่ม Save
                    }
                    else
                    {
                        // ถ้า PO_ID ยังไม่ถูกบันทึกลงใน Payment สามารถเลือก Payment Method และบันทึกได้
                        comboPayment.Enabled = true;  // เปิดการใช้งาน ComboBox
                        btnSave.Enabled = true;  // เปิดการใช้งานปุ่ม Save
                    }
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void lblCompany_Click(object sender, EventArgs e)
        {

        }
    }
}