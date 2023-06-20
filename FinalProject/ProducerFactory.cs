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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;
using System.Configuration;


namespace MidtermProject
{
    public partial class ProducerFactory : Form
    {
        public ProducerFactory()
        {
            InitializeComponent();
        }

        private void btnMessage_Click(object sender, EventArgs e)
        {
            string Message = "!\nYou can save the products you produce \nfrom this page to the warehouse.\nPlease do the product update carefully.";
            lblMessage.Text = lblMessage.Text + Message;
        }

        private void btnToSeeWarehouses_Click(object sender, EventArgs e)
        {
            Warehouses warehousePage = new Warehouses();
            warehousePage.Show();
            this.Hide();
        }

        private void ProducerFactory_Load(object sender, EventArgs e)
        {
            try
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
                            comboBoxWarehouses.Items.Clear(); // ComboBox'ı temizleyin

                            while (reader.Read())
                            {
                                string warehouseName = reader["WAREHOUSE_NAME"].ToString();
                                comboBoxWarehouses.Items.Add(warehouseName); // ComboBox'a depo adını ekleyin
                            }

                            reader.Close();
                            connection.Close();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Sorry an errror occured :" + ex.Message);

            }
        }

        private void lblSubmitUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string depoAdi = comboBoxWarehouses.SelectedItem.ToString();
                string urunAdi = txtEnterTheProduct.Text;
                int urunMiktari = Convert.ToInt32(txtAmountOfProduct.Text);
                decimal urunHacmi = Convert.ToDecimal(txtUnitVolumeOfProduct.Text);
                decimal urunAgirligi = Convert.ToDecimal(txtUnitWeightOfProduct.Text);

                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM PRODUCTS WHERE PRODUCT_NAME = @productName AND WAREHOUSE_ID IN (SELECT WAREHOUSES_ID FROM WH_AND_FACTORY WHERE FACTORY_ID = (SELECT ID FROM FACTORIES WHERE FACTORY_E_MAIL = @factoryEmail))", con);
                    checkCmd.Parameters.AddWithValue("@productName", urunAdi);
                    checkCmd.Parameters.AddWithValue("@factoryEmail", CurrentUser.Email);
                    int existCount = (int)checkCmd.ExecuteScalar();

                    if (existCount > 0)
                    {
                        SqlCommand updateCmd = new SqlCommand("UPDATE PRODUCTS SET QUANTITY = QUANTITY + @quantity, UNIT_VOLUME = @unitVolume, UNIT_WEIGHT = @unitWeight WHERE PRODUCT_NAME = @productName AND WAREHOUSE_ID IN (SELECT WAREHOUSES_ID FROM WH_AND_FACTORY WHERE FACTORY_ID = (SELECT ID FROM FACTORIES WHERE FACTORY_E_MAIL = @factoryEmail))", con);
                        updateCmd.Parameters.AddWithValue("@productName", urunAdi);
                        updateCmd.Parameters.AddWithValue("@quantity", urunMiktari);
                        updateCmd.Parameters.AddWithValue("@unitVolume", urunHacmi);
                        updateCmd.Parameters.AddWithValue("@unitWeight", urunAgirligi);
                        updateCmd.Parameters.AddWithValue("@factoryEmail", CurrentUser.Email);
                        updateCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand insertCmd = new SqlCommand("INSERT INTO PRODUCTS (PRODUCT_NAME, QUANTITY, WAREHOUSE_ID, FACTORY_ID, UNIT_VOLUME, UNIT_WEIGHT) SELECT @productName, @quantity, WAREHOUSES_ID, (SELECT ID FROM FACTORIES WHERE FACTORY_E_MAIL = @factoryEmail), @unitVolume, @unitWeight FROM WH_AND_FACTORY WHERE FACTORY_ID = (SELECT ID FROM FACTORIES WHERE FACTORY_E_MAIL = @factoryEmail)", con);
                        insertCmd.Parameters.AddWithValue("@productName", urunAdi);
                        insertCmd.Parameters.AddWithValue("@quantity", urunMiktari);
                        insertCmd.Parameters.AddWithValue("@unitVolume", urunHacmi);
                        insertCmd.Parameters.AddWithValue("@unitWeight", urunAgirligi);
                        insertCmd.Parameters.AddWithValue("@factoryEmail", CurrentUser.Email);
                        insertCmd.ExecuteNonQuery();
                    }

                    con.Close();
                }

                MessageBox.Show("Ürün başarıyla eklendi!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Sorry an errror occured :" + ex.Message);

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

        private void tipsAndTrickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TipsAndTricks tipsAndTricks = new TipsAndTricks();
            tipsAndTricks.ShowDialog();
        }
    }
}
