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
    public partial class Expedition_wh : Form
    {
        public Expedition_wh()
        {
            InitializeComponent();
            RetrieveDataFromDatabase();
        }

        private void RetrieveDataFromDatabase()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                int factoryId = CurrentUser.FactoryTypeId;

                string query = "SELECT ProductName, Quantity, RequestDate FROM CUSTOMER_REQUEST WHERE FACTORY_ID = @FactoryId";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Parametre ekleme
                        command.Parameters.AddWithValue("@FactoryId", factoryId);

                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            lblCargoNameSection.Text = reader["ProductName"].ToString();
                            lblCargoParcelAmount.Text = reader["Quantity"].ToString();

                            DateTime requestDate = (DateTime)reader["RequestDate"];
                            DateTime estimatedDepartureDate = requestDate.AddDays(4);
                            lblEstimatedDepartureDateSection.Text = estimatedDepartureDate.ToString("dd/MM/yyyy");
                        }

                        reader.Close();
                        connection.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Sorry an errror occured :" + ex.Message);

            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                int requestNumber = int.Parse(txtRequestNumber.Text);

                // Veritabanına bağlantıyı kur
                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // İlgili request numarasına ait kaydı al
                    SqlCommand selectCmd = new SqlCommand("SELECT cr.ProductName, cr.Quantity, cr.RequestDate, w.WAREHOUSE_PROVINCE, w.WAREHOUSE_COUNTRY, f.FACTORY_PROVINCE, f.FACTORY_COUNTRY FROM CUSTOMER_REQUEST cr INNER JOIN WH_AND_FACTORY wf ON cr.FACTORY_ID = wf.FACTORY_ID INNER JOIN WAREHOUSE w ON wf.WAREHOUSES_ID = w.ID INNER JOIN FACTORIES f ON wf.FACTORY_ID = f.ID WHERE cr.REQUEST_NUMBER = @requestNumber", con);
                    selectCmd.Parameters.AddWithValue("@requestNumber", requestNumber);
                    SqlDataReader reader = selectCmd.ExecuteReader();

                    if (reader.Read()) // Kayıt bulunduysa
                    {
                        // Verileri formdaki etiketlere ata
                        lblCargoNameSection.Text = reader["ProductName"].ToString();
                        lblCargoParcelAmountSection.Text = reader["Quantity"].ToString();
                        lblSourceProvinceSection.Text = reader["WAREHOUSE_PROVINCE"].ToString();
                        lblSourceCountrySection.Text = reader["WAREHOUSE_COUNTRY"].ToString();
                        lblDestinationProvinceSection.Text = reader["FACTORY_PROVINCE"].ToString();
                        lblDestinationCountrySection.Text = reader["FACTORY_COUNTRY"].ToString();

                        DateTime requestDate = Convert.ToDateTime(reader["RequestDate"]);
                        DateTime estimatedDepartureDate = requestDate.AddDays(4);
                        lblEstimatedDepartureDateSection.Text = estimatedDepartureDate.ToString("dd/MM/yyyy");

                        DateTime estimatedArrivalDate = requestDate.AddDays(10);
                        lblEstimatedArrivalDateSection.Text = estimatedArrivalDate.ToString("dd/MM/yyyy");

                        // Ürünün toplam hacmini ve ağırlığını hesapla
                        string productName = reader["ProductName"].ToString();
                        int quantity = int.Parse(reader["Quantity"].ToString());

                        reader.Close();

                        // Ürünün toplam hacmini ve ağırlığını hesapla
                        SqlCommand calculateCmd = new SqlCommand("SELECT p.UNIT_VOLUME * @quantity AS TotalCargoVolume, p.UNIT_WEIGHT * @quantity AS TotalCargoWeight FROM PRODUCTS p WHERE p.PRODUCT_NAME = @productName", con);
                        calculateCmd.Parameters.AddWithValue("@quantity", quantity);
                        calculateCmd.Parameters.AddWithValue("@productName", productName);
                        SqlDataReader calculateReader = calculateCmd.ExecuteReader();

                        if (calculateReader.Read()) // Hesaplama sonucu bulunduysa
                        {
                            lblTotalCargoVolumeSection.Text = calculateReader["TotalCargoVolume"].ToString();
                            lblTotalCargoWeightSection.Text = calculateReader["TotalCargoWeight"].ToString();
                        }
                        else
                        {
                            // Hesaplama sonucu bulunamadıysa, hata mesajı göster
                            MessageBox.Show("Ürünün toplam hacmi ve ağırlığı hesaplanamadı!");
                        }

                        calculateReader.Close();
                    }
                    else // Kayıt bulunamadıysa
                    {
                        MessageBox.Show("Belirtilen request numarasına ait kayıt bulunamadı!");
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry an errror occured :" + ex.Message);
            }
        }

        private void btnTo_GoBackTo_MngPage_Click(object sender, EventArgs e)
        {
            WarehouseManagement warehouseManagement = new WarehouseManagement();
            warehouseManagement.Show();
            this.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int requestNumber = int.Parse(txtRequestNumber.Text);
                DateTime actualDepartureDate;
                DateTime actualArrivalDate;
                string confirmation;

                // Girilen değerleri kontrol ederek, sadece doldurulan değerlere göre işlem yapma
                if (!string.IsNullOrEmpty(txtActualDepartureDate.Text))
                {
                    actualDepartureDate = DateTime.Parse(txtActualDepartureDate.Text);

                    string updateActualDepartureDateQuery = "UPDATE EXPEDITION SET ActualDepartureDate = @ActualDepartureDate WHERE CUSTOMER_REQUEST_ID IN (SELECT ID FROM CUSTOMER_REQUEST WHERE REQUEST_NUMBER = @RequestNumber)";

                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
                    {
                        connection.Open();

                        SqlCommand updateActualDepartureDateCommand = new SqlCommand(updateActualDepartureDateQuery, connection);
                        updateActualDepartureDateCommand.Parameters.AddWithValue("@ActualDepartureDate", actualDepartureDate);
                        updateActualDepartureDateCommand.Parameters.AddWithValue("@RequestNumber", requestNumber);
                        updateActualDepartureDateCommand.ExecuteNonQuery();

                        connection.Close();
                    }
                }

                if (!string.IsNullOrEmpty(txtActualArrivalDate.Text))
                {
                    actualArrivalDate = DateTime.Parse(txtActualArrivalDate.Text);

                    string updateActualArrivalDateQuery = "UPDATE EXPEDITION SET ActualArrivalDate = @ActualArrivalDate WHERE CUSTOMER_REQUEST_ID IN (SELECT ID FROM CUSTOMER_REQUEST WHERE REQUEST_NUMBER = @RequestNumber)";

                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
                    {
                        connection.Open();

                        SqlCommand updateActualArrivalDateCommand = new SqlCommand(updateActualArrivalDateQuery, connection);
                        updateActualArrivalDateCommand.Parameters.AddWithValue("@ActualArrivalDate", actualArrivalDate);
                        updateActualArrivalDateCommand.Parameters.AddWithValue("@RequestNumber", requestNumber);
                        updateActualArrivalDateCommand.ExecuteNonQuery();

                        connection.Close();
                    }
                }

                if (comboBoxConfirmation.SelectedItem != null)
                {
                    confirmation = comboBoxConfirmation.SelectedItem.ToString();

                    string updateConfirmationQuery = "UPDATE EXPEDITION SET Confirmation = @Confirmation WHERE CUSTOMER_REQUEST_ID IN (SELECT ID FROM CUSTOMER_REQUEST WHERE REQUEST_NUMBER = @RequestNumber)";

                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
                    {
                        connection.Open();

                        SqlCommand updateConfirmationCommand = new SqlCommand(updateConfirmationQuery, connection);
                        updateConfirmationCommand.Parameters.AddWithValue("@Confirmation", confirmation);
                        updateConfirmationCommand.Parameters.AddWithValue("@RequestNumber", requestNumber);
                        updateConfirmationCommand.ExecuteNonQuery();

                        connection.Close();
                    }
                }

                MessageBox.Show("Veriler başarıyla güncellendi!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry an errror occured :" + ex.Message);
            }
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}