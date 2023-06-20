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
    public partial class Expedition_factory : Form
    {
        private int requestNumber;
        public Expedition_factory()
        {
            InitializeComponent();
            grpToChooseSuccess.Visible = false; // Başlangıçta grup kutusu ve radyo düğmelerini gizle
            btnSubmit.Visible = false; // Başlangıçta btnSubmit butonunu gizle, confirmation kısmı için

        }

        private void UpdateConfirmationStatus(int requestNumber, string confirmationStatus)
        {
            // Veritabanına bağlantıyı kur
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Confirmation değerini güncelle
                SqlCommand updateCmd = new SqlCommand("UPDATE EXPEDITION SET Confirmation = @confirmationStatus WHERE CUSTOMER_REQUEST_ID = @requestNumber", con);
                updateCmd.Parameters.AddWithValue("@confirmationStatus", confirmationStatus);
                updateCmd.Parameters.AddWithValue("@requestNumber", requestNumber);
                updateCmd.ExecuteNonQuery();
            }
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            try 
            { 
                if (string.IsNullOrEmpty(txtRequestNumber.Text))
                {
                    MessageBox.Show("Please enter a valid request number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    requestNumber = int.Parse(txtRequestNumber.Text);

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
                            lblSouceProvinceSection.Text = reader["WAREHOUSE_PROVINCE"].ToString();
                            lblSouceCountrySection.Text = reader["WAREHOUSE_COUNTRY"].ToString();
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

                            // EXPEDITION tablosundan warehouse'un manuel olarak doldurduğu değerleri çek ve formdaki etiketlere ata
                            SqlCommand expeditionCmd = new SqlCommand("SELECT ActualDepartureDate, ActualArrivalDate, Confirmation FROM EXPEDITION WHERE CUSTOMER_REQUEST_ID = @requestNumber", con);
                            expeditionCmd.Parameters.AddWithValue("@requestNumber", requestNumber);
                            SqlDataReader expeditionReader = expeditionCmd.ExecuteReader();

                            if (expeditionReader.Read()) // Kayıt girilmiş ise
                            {
                                DateTime actualDepartureDate;
                                DateTime actualArrivalDate;
                                string confirmationNumber;

                                if (!expeditionReader.IsDBNull(expeditionReader.GetOrdinal("ActualDepartureDate")))
                                {
                                    actualDepartureDate = Convert.ToDateTime(expeditionReader["ActualDepartureDate"]);
                                    lblActualDepartureDateSection.Text = actualDepartureDate.ToString("dd/MM/yyyy");
                                }
                                else
                                {
                                    lblActualDepartureDateSection.Text = "Not approved yet";
                                }

                                if (!expeditionReader.IsDBNull(expeditionReader.GetOrdinal("ActualArrivalDate")))
                                {
                                    actualArrivalDate = Convert.ToDateTime(expeditionReader["ActualArrivalDate"]);
                                    lblActualArrivalDateSection.Text = actualArrivalDate.ToString("dd/MM/yyyy");
                                }
                                else
                                {
                                    lblActualArrivalDateSection.Text = "Not approved yet";
                                }

                                if (!expeditionReader.IsDBNull(expeditionReader.GetOrdinal("Confirmation")))
                                {
                                    confirmationNumber = expeditionReader["Confirmation"].ToString();
                                    lblConfirmationSection.Text = confirmationNumber;
                                }
                                else
                                {
                                    lblConfirmationSection.Text = "Not approved yet";
                                }

                                if (lblConfirmationSection.Text == "Delivered")
                                {
                                    lblCustomerApproval.Text = "Has Voyage successfully \n reached you?";
                                    grpToChooseSuccess.Visible = true; // "Delivered" durumunda grup kutusunu ve radyo düğmelerini görünür yap
                                    btnSubmit.Visible = true; // Confirmation "Delivered" ise butonu görünür yap
                                }
                                else
                                {
                                    grpToChooseSuccess.Visible = false; // Diğer durumlarda grup kutusunu ve radyo düğmelerini gizle
                                    btnSubmit.Visible = false; // Diğer durumlarda butonu gizle
                                }
                            }
                            else
                            {
                                // Kayıt bulunamadıysa, mesaj göster
                                lblActualDepartureDateSection.Text = "Not approved yet";
                                lblActualArrivalDateSection.Text = "Not approved yet";
                                lblConfirmationSection.Text = "Not approved yet";
                                grpToChooseSuccess.Visible = false; // Diğer durumlarda grup kutusunu ve radyo düğmelerini gizle
                                btnSubmit.Visible = false; // Diğer durumlarda butonu gizle
                                MessageBox.Show("The warehouse does not approved yet!");
                            }

                            expeditionReader.Close();
                        }
                        else // Kayıt bulunamadıysa
                        {
                            MessageBox.Show("Belirtilen request numarasına ait kayıt bulunamadı!");
                        }

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry an error occured:" + ex.Message);
            }
    }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // Seçili radyo düğmesini kontrol et
                if (radioButtonYes.Checked)
                {
                    // Onay durumunu güncelle: Successful
                    UpdateConfirmationStatus(requestNumber, "Successful");
                    MessageBox.Show("Your update has been saved. Thank you for your confirmation!", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (radioButtonNo.Checked)
                {
                    // Onay durumunu güncelle: Unsuccessful
                    UpdateConfirmationStatus(requestNumber, "Unsuccessful");
                    MessageBox.Show("We have recorded your update. Thank you!", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void viewHelpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ViewHelp viewHelp = new ViewHelp();
            viewHelp.ShowDialog();
        }

        private void tipsAndTricksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TipsAndTricks tipsAndTricks = new TipsAndTricks();
            tipsAndTricks.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConsumerFactory consumerFactory = new ConsumerFactory();
            consumerFactory.Show();
            this.Hide();
        }
    }
}