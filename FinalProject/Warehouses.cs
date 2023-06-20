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
using System.Configuration;

namespace MidtermProject
{
    public partial class Warehouses : Form
    {
        public Warehouses()
        {
            InitializeComponent();
        }

        private void Warehouses_Load(object sender, EventArgs e)
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
                            comboBoxWarehousesPage.Items.Clear(); // ComboBox'ı temizleyin

                            while (reader.Read())
                            {
                                string warehouseName = reader["WAREHOUSE_NAME"].ToString();
                                comboBoxWarehousesPage.Items.Add(warehouseName); // ComboBox'a depo adını ekleyin
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

        private void btnClickWarehouse_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectedWarehouse = comboBoxWarehousesPage.SelectedItem.ToString();
                    string queryProducts = "SELECT PRODUCT_NAME, PRODUCT_PRICE, QUANTITY FROM PRODUCTS WHERE WAREHOUSE_ID = (SELECT ID FROM WAREHOUSE WHERE WAREHOUSE_NAME = @SelectedWarehouse)";
                    using (SqlCommand command = new SqlCommand(queryProducts, connection))
                    {
                        command.Parameters.AddWithValue("@SelectedWarehouse", selectedWarehouse);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            dataGridViewProducts.DataSource = table;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Sorry an errror occured :" + ex.Message);
            }
        }

        private void btnToPassFactoriesPage_Click(object sender, EventArgs e)
        {
            FactoriesList factoriesList = new FactoriesList();
            factoriesList.Show();
            this.Hide();
            

        }

        private void btnToPreviousPage_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentUser.FactoryTypeId == 1)
                {
                    ProducerFactory producerFactoryForm = new ProducerFactory();
                    producerFactoryForm.Show();
                    this.Hide();
                }
                else if (CurrentUser.FactoryTypeId == 2)
                {
                    ConsumerFactory consumerFactoryForm = new ConsumerFactory();
                    consumerFactoryForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid factory type.");
                }
            }
            catch( Exception ex )
            {
                MessageBox.Show("Sorry an errror occured :" + ex.Message);
            }
        }

        private void btnInformationsOfWarehouses_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectedWarehouse = comboBoxWarehousesPage.SelectedItem.ToString();
                    string query = "SELECT WAREHOUSE_E_MAIL, WAREHOUSE_PHONE_NUMBER, WAREHOUSE_COUNTRY, WAREHOUSE_ADDRESS FROM WAREHOUSE WHERE WAREHOUSE_NAME = @SelectedWarehouse";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SelectedWarehouse", selectedWarehouse);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            dataGridViewProducts.DataSource = table;
                        }
                    }
                }
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

        private void tipsAndTricksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TipsAndTricks tipsAndTricks = new TipsAndTricks();
            tipsAndTricks.ShowDialog();
        }
    }
}