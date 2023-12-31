﻿using System;
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
    public partial class WarehousesSignUpPage : Form
    {
        public WarehousesSignUpPage()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            string warehouseName = txtWarehouseName.Text.Trim();
            string warehouseEmail = txtWarehouseEmail.Text.Trim();
            string warehousePhoneNumber = txtWarehousePhoneNumber.Text.Trim();
            string warehouseCountry = comboBoxWarehouseCountries.Text.Trim();
            string warehouseAddress = txtWarehouseAddress.Text.Trim();
            string warehouseProvince = txtWarehouseProvince.Text.Trim();
            string warehousePassword = txtWarehousePassword.Text.Trim();

            // Değerlerin boş olup olmadığını kontrol eder ve hata mesajı gösterir
            if (string.IsNullOrEmpty(warehouseName) || string.IsNullOrEmpty(warehouseEmail) || string.IsNullOrEmpty(warehousePhoneNumber) || string.IsNullOrEmpty(warehouseCountry) || string.IsNullOrEmpty(warehouseAddress) || string.IsNullOrEmpty(warehousePassword) || string.IsNullOrEmpty(warehouseProvince)) 
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.");
                return;
            }

            // Değerler geçerli ise, veritabanına kaydetmeye devam et
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("INSERT INTO WAREHOUSE (WAREHOUSE_NAME, WAREHOUSE_E_MAIL, WAREHOUSE_PHONE_NUMBER, WAREHOUSE_COUNTRY, WAREHOUSE_ADDRESS, WAREHOUSE_PASSWORD,  WAREHOUSE_PROVINCE) VALUES (@WAREHOUSE_NAME, @WAREHOUSE_E_MAIL, @WAREHOUSE_PHONE_NUMBER, @WAREHOUSE_COUNTRY, @WAREHOUSE_ADDRESS, @WAREHOUSE_PASSWORD, @WAREHOUSE_PROVINCE)", connection);
            command.Parameters.AddWithValue("@WAREHOUSE_NAME", warehouseName);
            command.Parameters.AddWithValue("@WAREHOUSE_E_MAIL", warehouseEmail);
            command.Parameters.AddWithValue("@WAREHOUSE_PHONE_NUMBER", warehousePhoneNumber);
            command.Parameters.AddWithValue("@WAREHOUSE_COUNTRY", warehouseCountry);
            command.Parameters.AddWithValue("@WAREHOUSE_ADDRESS", warehouseAddress);
            command.Parameters.AddWithValue("@WAREHOUSE_PASSWORD", warehousePassword);
            command.Parameters.AddWithValue("@WAREHOUSE_PROVINCE", warehouseProvince);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                WarehousesLoginPage warehousesLogin = new WarehousesLoginPage();
                warehousesLogin.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
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
