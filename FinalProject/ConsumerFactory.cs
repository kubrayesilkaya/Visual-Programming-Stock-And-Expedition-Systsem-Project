using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System;

namespace MidtermProject
{
    public partial class ConsumerFactory : Form
    {
        public ConsumerFactory()
        {
            InitializeComponent();
        }

        private void btnMessage_Click(object sender, EventArgs e)
        {
            string Message = "!\nYou can save the products you consume \nfrom this page to the warehouse.\nPlease do the product update carefully.";
            lblMessageConsumer.Text = lblMessageConsumer.Text + Message;
        }

        private void btnToSeeWarehouses_Click(object sender, EventArgs e)
        {
            Warehouses warehouseForm = new Warehouses();
            warehouseForm.Show();
            this.Hide();
        }

        private void btnRequestProduct_Click(object sender, EventArgs e)
        {
            try { 
            string depoAdi = comboBoxToChooseWarehouse.Text;
            string urunAdi = txtProductName.Text;
            int urunMiktari = Convert.ToInt32(txtAmountOfPrdouct.Text);

            // Veritabanına bağlantıyı kur
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Ürünün veritabanında olup olmadığını kontrol et
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM PRODUCTS WHERE PRODUCT_NAME = @productName AND QUANTITY >= @quantity AND WAREHOUSE_ID = (SELECT ID FROM WAREHOUSE WHERE WAREHOUSE_NAME=@warehouseName)", con);
                checkCmd.Parameters.AddWithValue("@productName", urunAdi);
                checkCmd.Parameters.AddWithValue("@quantity", urunMiktari);
                checkCmd.Parameters.AddWithValue("@warehouseName", depoAdi);
                int existCount = (int)checkCmd.ExecuteScalar();

                if (existCount > 0) // ürün varsa, miktarını azalt
                {
                    SqlCommand updateCmd = new SqlCommand("UPDATE PRODUCTS SET QUANTITY = QUANTITY - @quantity WHERE PRODUCT_NAME = @productName AND WAREHOUSE_ID = (SELECT ID FROM WAREHOUSE WHERE WAREHOUSE_NAME=@warehouseName)", con);
                    updateCmd.Parameters.AddWithValue("@productName", urunAdi);
                    updateCmd.Parameters.AddWithValue("@quantity", urunMiktari);
                    updateCmd.Parameters.AddWithValue("@warehouseName", depoAdi);
                    updateCmd.ExecuteNonQuery();

                    // Tüketilen ürünü kaydet
                    SqlCommand insertCmd = new SqlCommand("INSERT INTO CUSTOMER_REQUEST (ProductName, Quantity, FACTORY_ID, REQUEST_NUMBER) VALUES (@productName, @quantity, (SELECT ID FROM FACTORIES WHERE FACTORY_E_MAIL = @factoryEmail), (SELECT ISNULL(MAX(REQUEST_NUMBER), 0) FROM CUSTOMER_REQUEST) + 1)", con);
                    insertCmd.Parameters.AddWithValue("@productName", urunAdi);
                    insertCmd.Parameters.AddWithValue("@quantity", urunMiktari);
                    insertCmd.Parameters.AddWithValue("@factoryEmail", CurrentUser.Email);
                    insertCmd.ExecuteNonQuery();

                    // Son talep numarasını al
                    SqlCommand selectRequestNumberCmd = new SqlCommand("SELECT MAX(REQUEST_NUMBER) FROM CUSTOMER_REQUEST WHERE FACTORY_ID = (SELECT ID FROM FACTORIES WHERE FACTORY_E_MAIL = @factoryEmail)", con);
                    selectRequestNumberCmd.Parameters.AddWithValue("@factoryEmail", CurrentUser.Email);
                    int requestNumber = (int)selectRequestNumberCmd.ExecuteScalar();
                    lblRequestNumberSection.Text = requestNumber.ToString();

                    // Veritabanındaki EXPEDITION tablosuna yeni kaydı ekle
                    SqlCommand insertExpeditionCmd = new SqlCommand("INSERT INTO EXPEDITION (CUSTOMER_REQUEST_ID) VALUES (@requestId)", con);
                    insertExpeditionCmd.Parameters.AddWithValue("@requestId", requestNumber);
                    insertExpeditionCmd.ExecuteNonQuery();

                    MessageBox.Show("Ürün başarıyla tüketildi ve kaydedildi!");

                }
                else // ürün yoksa, hata mesajı göster
                {
                    MessageBox.Show("Tüketmek istediğiniz ürün stokta yok veya yeterli miktarda değil!");
                }
            }
        }
                catch(Exception ex)
                { 
                    MessageBox.Show("Sorry an errror occured :" + ex.Message); 
                }
            }

        private void ConsumerFactory_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CurrentUser.Email))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT W.WAREHOUSE_NAME FROM WAREHOUSE W INNER JOIN WH_AND_FACTORY WF ON W.ID = WF.WAREHOUSES_ID INNER JOIN FACTORIES F ON WF.FACTORY_ID = F.ID WHERE F.FACTORY_E_MAIL = @FACTORY_EMAIL";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FACTORY_EMAIL", CurrentUser.Email);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        comboBoxToChooseWarehouse.Items.Clear(); // ComboBox'ı temizleyin

                        while (reader.Read())
                        {
                            string warehouseName = reader["WAREHOUSE_NAME"].ToString();
                            comboBoxToChooseWarehouse.Items.Add(warehouseName); // ComboBox'a depo adını ekleyin
                        }

                        reader.Close();
                        connection.Close();
                    }
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewHelp viewHelp = new ViewHelp();
            viewHelp.ShowDialog();
        }

        private void tipsAndTricksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TipsAndTricks tipsAndTricks = new TipsAndTricks();
            tipsAndTricks.ShowDialog();
        }

        private void btnExpedition_Click(object sender, EventArgs e)
        {
            Expedition_factory expedition_factory = new Expedition_factory();
            expedition_factory.Show();
            this.Hide();
        }

        private void btnToProductReportPage_Click(object sender, EventArgs e)
        {
            ReportingScreen_ToProducts ProductReportPage = new ReportingScreen_ToProducts();
            ProductReportPage.Show();
            this.Hide();
        }
    }
}