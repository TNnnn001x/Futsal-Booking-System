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
    public partial class formPayment : Form
    {
        private string strConnectionString = "";
        const string strFileName = "ConnectionString.ini";
        private int paymentId;

        // Constructor to initialize paymentId
        public formPayment(int paymentId)
        {
            InitializeComponent();
            this.paymentId = paymentId;
        }

        private void formPayment_Load(object sender, EventArgs e)
        {
            LoadPaymentDetails();
        }

        // Method to load payment details based on paymentId
        private void LoadPaymentDetails()
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

            // Query to get data for the specific Payment using paymentId
            string query = @"
                            SELECT 
                            pay.Payment_ID, 
                            po.PO_ID, 
                            po.PO_Date, 
                            s.SupplierName, 
                            sa.Address, 
                            s.ContactNumber, 
                            pay.Payment_Method_ID, 
                            pay.Payment_Date, 
                            pay.Payment_Status, 
                            pay.Payment_Amount,
                            pr.PR_Status,
                            p.Product_Name,
                            orr.Product_Quantity,
                            orr.Product_Price,
                            pm.Payment_Method
                        FROM Payment pay
                        LEFT JOIN PurchaseOrder po ON pay.PO_ID = po.PO_ID
                        LEFT JOIN PaymentMethod pm ON pay.Payment_Method_ID = pm.Payment_Method_ID
                        LEFT JOIN PurchaseRequest pr ON po.PR_ID = pr.PR_ID
                        LEFT JOIN OrderRequest orr ON pr.PR_ID = orr.PR_ID
                        LEFT JOIN Product p ON orr.Product_ID = p.Product_ID
                        LEFT JOIN Supplier s ON orr.Supplier_ID = s.Supplier_ID
                        LEFT JOIN SupplierAddress sa ON s.SupplierAddress_ID = sa.SupplierAddress_ID
                        WHERE pay.Payment_ID = @Payment_ID
                        ";  // ใช้ Payment_ID แทน PO_ID


            using (SqlConnection connection = new SqlConnection(strConnectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Payment_ID", paymentId);  // ใช้ Payment_ID ที่รับมาจาก constructor
                                                                                // Use the PO_ID associated with the paymentId

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        // Display data in labels
                        reader.Read();
                        lblPaymentMethod.Text = reader["Payment_Method"].ToString();
                        lblPaymentNo.Text = reader["Payment_ID"].ToString();
                        lblPayDate.Text = Convert.ToDateTime(reader["Payment_Date"]).ToString("yyyy-MM-dd");
                        lblAmount.Text = reader["Payment_Amount"].ToString();
                        lblPOID.Text = reader["PO_ID"].ToString();
                        lblPODate.Text = Convert.ToDateTime(reader["PO_Date"]).ToString("yyyy-MM-dd");
                        lblAttn.Text = reader["SupplierName"].ToString();
                        lblAdd.Text = reader["Address"].ToString();
                        lblTel.Text = reader["ContactNumber"].ToString();
                        lblStatus.Text = reader["Payment_Status"].ToString();
                        lblAttn.Text = reader["SupplierName"].ToString();
                        lblAdd.Text = reader["Address"].ToString();
                        lblTel.Text = reader["ContactNumber"].ToString();

                        // Set up columns in DataGridView
                        dataGridViewPO.Columns.Clear();
                        dataGridViewPO.Columns.Add("ItemNo", "Item No.");
                        dataGridViewPO.Columns.Add("Product_Name", "Product Name");
                        dataGridViewPO.Columns.Add("Product_Quantity", "Quantity");
                        dataGridViewPO.Columns.Add("Product_Price", "Price");

                        // Clear existing rows
                        dataGridViewPO.Rows.Clear();

                        // Loop through the reader and add all products to the DataGridView
                        int rowNumber = 1;
                        do
                        {
                            dataGridViewPO.Rows.Add(rowNumber++, reader["Product_Name"].ToString(), reader["Product_Quantity"].ToString(), reader["Product_Price"].ToString());
                        } while (reader.Read());
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบข้อมูลการสั่งซื้อนี้");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnPrint_Click(object sender, EventArgs e)
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
        private void PrintInvoice(object sender, PrintPageEventArgs e)
        {
            // กำหนดฟอนต์และ Brush สำหรับพิมพ์
            Font fontTitle = new Font("Arial", 14, FontStyle.Bold);
            Font fontNormal = new Font("Arial", 12);
            Brush brush = Brushes.Black;

            // ตั้งค่าตำแหน่งในการพิมพ์
            float x = 50;
            float y = 50;
            float lineHeight = fontNormal.GetHeight(e.Graphics);

            // พิมพ์ข้อมูลจากฟอร์ม
            e.Graphics.DrawString("Payment", fontTitle, brush, x, y);
            y += lineHeight + 10;
            e.Graphics.DrawString("No.: " + lblPaymentNo.Text, fontNormal, brush, x, y);
            y += lineHeight;
            e.Graphics.DrawString("Payment Method: " + lblPaymentMethod.Text, fontNormal, brush, x, y);
            y += lineHeight;
            e.Graphics.DrawString("Status: " + lblStatus.Text, fontNormal, brush, x, y);
            y += lineHeight;
            e.Graphics.DrawString("Amount: " + lblAmount.Text, fontNormal, brush, x, y);
            y += lineHeight + 20;

            // พิมพ์ข้อมูล Purchase Order
            e.Graphics.DrawString("Purchase Order", fontTitle, brush, x, y);
            y += lineHeight + 10;
            e.Graphics.DrawString("Attn: " + lblAttn.Text, fontNormal, brush, x, y);
            y += lineHeight;
            e.Graphics.DrawString("Add: " + lblAdd.Text, fontNormal, brush, x, y);
            y += lineHeight;
            e.Graphics.DrawString("Tel: " + lblTel.Text, fontNormal, brush, x, y);
            y += lineHeight;

            // ข้อมูลอื่น ๆ ที่ต้องการพิมพ์
            e.Graphics.DrawString("No.: " + lblPOID.Text, fontNormal, brush, x, y);
            y += lineHeight;
            e.Graphics.DrawString("Date: " + lblPODate.Text, fontNormal, brush, x, y);

            // พิมพ์ข้อมูลใน DataGridView (เช่น รายการสินค้าใน Purchase Order)
            y += lineHeight + 20;
            e.Graphics.DrawString("Items in Purchase Order:", fontTitle, brush, x, y);
            y += lineHeight;

            // พิมพ์หัวตาราง
            e.Graphics.DrawString("Item", fontNormal, brush, x, y);
            e.Graphics.DrawString("Quantity", fontNormal, brush, x + 150, y);
            e.Graphics.DrawString("Price", fontNormal, brush, x + 250, y);
            e.Graphics.DrawString("Amount", fontNormal, brush, x + 350, y);
            y += lineHeight;

            // ลูปพิมพ์ข้อมูลจาก DataGridView
            foreach (DataGridViewRow row in dataGridViewPO.Rows)
            {
                if (row.IsNewRow) continue;  // ข้ามแถวใหม่
                e.Graphics.DrawString(row.Cells["Product_Name"].Value.ToString(), fontNormal, brush, x, y);
                e.Graphics.DrawString(row.Cells["Product_Quantity"].Value.ToString(), fontNormal, brush, x + 150, y);
                e.Graphics.DrawString(row.Cells["Product_Price"].Value.ToString(), fontNormal, brush, x + 250, y);
                e.Graphics.DrawString((Convert.ToDecimal(row.Cells["Product_Quantity"].Value) * Convert.ToDecimal(row.Cells["Product_Price"].Value)).ToString(), fontNormal, brush, x + 350, y);
                y += lineHeight;
            }

            // พิมพ์ข้อมูลด้านล่าง (รวมราคา)
            y += 20;
            e.Graphics.DrawString("Total Amount", fontNormal, brush, x + 350, y);
            decimal totalAmount = 0;
            foreach (DataGridViewRow row in dataGridViewPO.Rows)
            {
                if (row.IsNewRow) continue;
                totalAmount += Convert.ToDecimal(row.Cells["Product_Quantity"].Value) * Convert.ToDecimal(row.Cells["Product_Price"].Value);
            }
            e.Graphics.DrawString(totalAmount.ToString("N2"), fontNormal, brush, x + 450, y);
        }
    }
}
