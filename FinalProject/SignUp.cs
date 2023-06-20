using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace MidtermProject
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();

            // Depo bilgilerini ComboBox'a yükle
            LoadWarehouseNames();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("INSERT INTO FACTORIES (FACTORY_E_MAIL, PASSWORD, FACTORY_NAME, FACTORY_TYPE_ID, FACTORY_PHONE_NUMBER, FACTORY_COUNTRY, FACTORY_ADDRESS, FACTORY_TAX_NUMBER, FACTORY_PROVINCE) VALUES (@FACTORY_E_MAIL, @PASSWORD, @FACTORY_NAME, @FACTORY_TYPE_ID, @FACTORY_PHONE_NUMBER, @FACTORY_COUNTRY, @FACTORY_ADDRESS, @FACTORY_TAX_NUMBER, @FACTORY_PROVINCE); SELECT SCOPE_IDENTITY();", connection);
                    command.Parameters.AddWithValue("@FACTORY_E_MAIL", txtFactoryEmail.Text);
                    command.Parameters.AddWithValue("@PASSWORD", txtFactoryPassword.Text);
                    command.Parameters.AddWithValue("@FACTORY_NAME", txtFactoryName.Text);
                    command.Parameters.AddWithValue("@FACTORY_TYPE_ID", radioBtnProducer.Checked ? 1 : 2);
                    command.Parameters.AddWithValue("@FACTORY_PHONE_NUMBER", txtFactoryPhone.Text);
                    command.Parameters.AddWithValue("@FACTORY_COUNTRY", comboBoxCountries.Text);
                    command.Parameters.AddWithValue("@FACTORY_ADDRESS", txtFactoryAddress.Text);
                    command.Parameters.AddWithValue("@FACTORY_PROVINCE", txtFactoryProvince.Text);
                    command.Parameters.AddWithValue("@FACTORY_TAX_NUMBER", txtFactoryTaxNumber.Text);

                    connection.Open();
                    int factoryId = Convert.ToInt32(command.ExecuteScalar());

                    if (factoryId > 0)
                    {
                        int warehouseId = GetSelectedWarehouseId();
                        if (warehouseId > 0)
                        {
                            SqlCommand linkCommand = new SqlCommand("INSERT INTO WH_AND_FACTORY (WAREHOUSES_ID, FACTORY_ID) VALUES (@WAREHOUSES_ID, @FACTORY_ID)", connection);
                            linkCommand.Parameters.AddWithValue("@WAREHOUSES_ID", warehouseId);
                            linkCommand.Parameters.AddWithValue("@FACTORY_ID", factoryId);
                            linkCommand.ExecuteNonQuery();
                        }
                    }

                    connection.Close();
                }

                LoginPage loginForm = new LoginPage();
                loginForm.Show();
                this.Hide();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Sorry an errror occured :" + ex.Message);
            }
        }

        private int GetSelectedWarehouseId()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            int warehouseId = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string warehouseName = comboBoxWarehouses.SelectedItem.ToString();
                string query = "SELECT ID FROM WAREHOUSE WHERE WAREHOUSE_NAME = @WAREHOUSE_NAME";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@WAREHOUSE_NAME", warehouseName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    warehouseId = Convert.ToInt32(reader["ID"]);
                }

                reader.Close();
                connection.Close();
            }

            return warehouseId;
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

        private void comboBoxWarehouses_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Seçilen depo değiştiğinde yapılacak işlemler
        }

        private void LoadWarehouseNames()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT WAREHOUSE_NAME FROM WAREHOUSE";
                    SqlCommand command = new SqlCommand(query, connection);

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
            catch(Exception ex)
            {
                MessageBox.Show("Sorry an errror occured :" + ex.Message);
            }
        }
    }
}