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
    public partial class ReportingScreen_ToProducts : Form
    {
        // Veritabanı bağlantısı
        string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        private void ReportingScreen_ToProducts_Load(object sender, EventArgs e)
        {
            LoadDataGrid();

        }
        private void LoadDataGrid()
        {
            try
            {
                // Mevcut kullanıcının fabrika bilgisini ve bağlı olduğu warehouse'ları tespit et
                string factoryName = "";
                int factoryId = 0;
                string warehouseName = "";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Factory bilgisini tespit et
                    string factoryQuery = "SELECT FACTORY_NAME, ID FROM FACTORIES WHERE FACTORY_E_MAIL = @Email";
                    SqlCommand factoryCommand = new SqlCommand(factoryQuery, connection);
                    factoryCommand.Parameters.AddWithValue("@Email", CurrentUser.Email);
                    SqlDataReader factoryReader = factoryCommand.ExecuteReader();
                    if (factoryReader.Read())
                    {
                        factoryName = factoryReader["FACTORY_NAME"].ToString();
                        factoryId = Convert.ToInt32(factoryReader["ID"]);
                    }
                    factoryReader.Close();

                    // Warehouse bilgilerini tespit et
                    string warehouseQuery = "SELECT WAREHOUSE_NAME FROM WAREHOUSE " +
                                            "INNER JOIN WH_AND_FACTORY ON WAREHOUSE.ID = WH_AND_FACTORY.WAREHOUSES_ID " +
                                            "WHERE WH_AND_FACTORY.FACTORY_ID = @FactoryId";
                    SqlCommand warehouseCommand = new SqlCommand(warehouseQuery, connection);
                    warehouseCommand.Parameters.AddWithValue("@FactoryId", factoryId);
                    SqlDataReader warehouseReader = warehouseCommand.ExecuteReader();
                    while (warehouseReader.Read())
                    {
                        warehouseName += warehouseReader["WAREHOUSE_NAME"].ToString() + ", ";
                    }
                    warehouseReader.Close();
                }

                // Verileri DataGridView'e yükle
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT FR.FACTORY_NAME, CR.RequestDate, CR.Quantity, WH.WAREHOUSE_NAME " +
                                    "FROM CUSTOMER_REQUEST CR " +
                                    "INNER JOIN FACTORIES FR ON CR.FACTORY_ID = FR.ID " +
                                    "INNER JOIN WH_AND_FACTORY WF ON FR.ID = WF.FACTORY_ID " +
                                    "INNER JOIN WAREHOUSE WH ON WF.WAREHOUSES_ID = WH.ID " +
                                    "WHERE FR.FACTORY_NAME = @FactoryName";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FactoryName", factoryName);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // DataGridView'i doldur
                    dataGridViewProductReporting.DataSource = dataTable;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Sorry an errror occured :" + ex.Message);
            }
        }
        public ReportingScreen_ToProducts()
        {
            InitializeComponent();
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

        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                // DataGridView'de uygulanacak tarih filtresini alıyoruz
                DateTime filterDate = dateTimePickerProductReportingPage.Value;

                // DataGridView'deki verileri DataTable'dan alın
                DataTable dataTable = (DataTable)dataGridViewProductReporting.DataSource;

                // Yeni bir DataView oluşturuyoruz ve RequestDate sütunundaki veriler için tarih filtresini uyguluyoruz
                DataView dataView = new DataView(dataTable);
                dataView.RowFilter = string.Format("RequestDate >= #{0}# AND RequestDate <= #{1}#",
                                                    filterDate.ToString("yyyy-MM-dd 00:00:00"),
                                                    filterDate.ToString("yyyy-MM-dd 23:59:59"));

                // Filtrelenmiş verileri DataGridView'e yükle
                dataGridViewProductReporting.DataSource = dataView;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Sorry an errror occured :" + ex.Message);
            }
        }

        private void dateTimePickerProductReportingPage_ValueChanged(object sender, EventArgs e)
        {
            LoadDataGrid();
        }

        private void btnToPassConsumerFactoryPage_Click(object sender, EventArgs e)
        {
            ConsumerFactory consumerFactory = new ConsumerFactory();
            consumerFactory.Show();
            this.Hide();
        }
    }
}
