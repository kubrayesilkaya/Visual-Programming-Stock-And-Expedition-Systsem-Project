namespace MidtermProject
{
    partial class ReportingScreen_ToProducts
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportingScreen_ToProducts));
            this.pictureBoxReportingScreen_ToProducts = new System.Windows.Forms.PictureBox();
            this.menuStripReportingPage = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tipsAndTricksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblReportingPageWelcome = new System.Windows.Forms.Label();
            this.finalProjectDataSet = new MidtermProject.FinalProjectDataSet();
            this.cUSTOMERREQUESTBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cUSTOMER_REQUESTTableAdapter = new MidtermProject.FinalProjectDataSetTableAdapters.CUSTOMER_REQUESTTableAdapter();
            this.fKCUSTOMERREQUESTCUSTOMERREQUEST1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewProductReporting = new System.Windows.Forms.DataGridView();
            this.dateTimePickerProductReportingPage = new System.Windows.Forms.DateTimePicker();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnToPassConsumerFactoryPage = new System.Windows.Forms.Button();
            this.lblReportingScreenMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxReportingScreen_ToProducts)).BeginInit();
            this.menuStripReportingPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.finalProjectDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cUSTOMERREQUESTBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKCUSTOMERREQUESTCUSTOMERREQUEST1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProductReporting)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxReportingScreen_ToProducts
            // 
            this.pictureBoxReportingScreen_ToProducts.Image = global::MidtermProject.Properties.Resources.spring;
            this.pictureBoxReportingScreen_ToProducts.Location = new System.Drawing.Point(12, 61);
            this.pictureBoxReportingScreen_ToProducts.Name = "pictureBoxReportingScreen_ToProducts";
            this.pictureBoxReportingScreen_ToProducts.Size = new System.Drawing.Size(286, 190);
            this.pictureBoxReportingScreen_ToProducts.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxReportingScreen_ToProducts.TabIndex = 0;
            this.pictureBoxReportingScreen_ToProducts.TabStop = false;
            // 
            // menuStripReportingPage
            // 
            this.menuStripReportingPage.BackColor = System.Drawing.Color.Linen;
            this.menuStripReportingPage.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripReportingPage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.formToolStripMenuItem});
            this.menuStripReportingPage.Location = new System.Drawing.Point(0, 0);
            this.menuStripReportingPage.Name = "menuStripReportingPage";
            this.menuStripReportingPage.Size = new System.Drawing.Size(1408, 38);
            this.menuStripReportingPage.TabIndex = 1;
            this.menuStripReportingPage.Text = "menuStripReportingPage";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(60, 24);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewHelpToolStripMenuItem,
            this.tipsAndTricksToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(124, 26);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // viewHelpToolStripMenuItem
            // 
            this.viewHelpToolStripMenuItem.Name = "viewHelpToolStripMenuItem";
            this.viewHelpToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.viewHelpToolStripMenuItem.Text = "View Help";
            this.viewHelpToolStripMenuItem.Click += new System.EventHandler(this.viewHelpToolStripMenuItem_Click);
            // 
            // tipsAndTricksToolStripMenuItem
            // 
            this.tipsAndTricksToolStripMenuItem.Name = "tipsAndTricksToolStripMenuItem";
            this.tipsAndTricksToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.tipsAndTricksToolStripMenuItem.Text = "Tips And Tricks";
            this.tipsAndTricksToolStripMenuItem.Click += new System.EventHandler(this.tipsAndTricksToolStripMenuItem_Click);
            // 
            // formToolStripMenuItem
            // 
            this.formToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.formToolStripMenuItem.Name = "formToolStripMenuItem";
            this.formToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.formToolStripMenuItem.Text = "Form";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(116, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // lblReportingPageWelcome
            // 
            this.lblReportingPageWelcome.AutoSize = true;
            this.lblReportingPageWelcome.Font = new System.Drawing.Font("Microsoft YaHei", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblReportingPageWelcome.ForeColor = System.Drawing.Color.IndianRed;
            this.lblReportingPageWelcome.Location = new System.Drawing.Point(410, 30);
            this.lblReportingPageWelcome.Name = "lblReportingPageWelcome";
            this.lblReportingPageWelcome.Size = new System.Drawing.Size(489, 37);
            this.lblReportingPageWelcome.TabIndex = 3;
            this.lblReportingPageWelcome.Text = "Welcome to the reporting screen!";
            // 
            // finalProjectDataSet
            // 
            this.finalProjectDataSet.DataSetName = "FinalProjectDataSet";
            this.finalProjectDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cUSTOMERREQUESTBindingSource
            // 
            this.cUSTOMERREQUESTBindingSource.DataMember = "CUSTOMER_REQUEST";
            this.cUSTOMERREQUESTBindingSource.DataSource = this.finalProjectDataSet;
            // 
            // cUSTOMER_REQUESTTableAdapter
            // 
            this.cUSTOMER_REQUESTTableAdapter.ClearBeforeFill = true;
            // 
            // fKCUSTOMERREQUESTCUSTOMERREQUEST1BindingSource
            // 
            this.fKCUSTOMERREQUESTCUSTOMERREQUEST1BindingSource.DataMember = "FK_CUSTOMER_REQUEST_CUSTOMER_REQUEST1";
            this.fKCUSTOMERREQUESTCUSTOMERREQUEST1BindingSource.DataSource = this.cUSTOMERREQUESTBindingSource;
            // 
            // dataGridViewProductReporting
            // 
            this.dataGridViewProductReporting.BackgroundColor = System.Drawing.Color.SeaShell;
            this.dataGridViewProductReporting.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewProductReporting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProductReporting.Location = new System.Drawing.Point(119, 297);
            this.dataGridViewProductReporting.Name = "dataGridViewProductReporting";
            this.dataGridViewProductReporting.RowHeadersWidth = 51;
            this.dataGridViewProductReporting.RowTemplate.Height = 24;
            this.dataGridViewProductReporting.Size = new System.Drawing.Size(891, 291);
            this.dataGridViewProductReporting.TabIndex = 4;
            // 
            // dateTimePickerProductReportingPage
            // 
            this.dateTimePickerProductReportingPage.Location = new System.Drawing.Point(810, 244);
            this.dateTimePickerProductReportingPage.Name = "dateTimePickerProductReportingPage";
            this.dateTimePickerProductReportingPage.Size = new System.Drawing.Size(200, 22);
            this.dateTimePickerProductReportingPage.TabIndex = 5;
            this.dateTimePickerProductReportingPage.ValueChanged += new System.EventHandler(this.dateTimePickerProductReportingPage_ValueChanged);
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(659, 229);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(136, 57);
            this.btnFilter.TabIndex = 7;
            this.btnFilter.Text = "Click To Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnToPassConsumerFactoryPage
            // 
            this.btnToPassConsumerFactoryPage.BackColor = System.Drawing.Color.PowderBlue;
            this.btnToPassConsumerFactoryPage.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnToPassConsumerFactoryPage.ForeColor = System.Drawing.Color.DarkCyan;
            this.btnToPassConsumerFactoryPage.Location = new System.Drawing.Point(31, 601);
            this.btnToPassConsumerFactoryPage.Name = "btnToPassConsumerFactoryPage";
            this.btnToPassConsumerFactoryPage.Size = new System.Drawing.Size(76, 32);
            this.btnToPassConsumerFactoryPage.TabIndex = 8;
            this.btnToPassConsumerFactoryPage.Text = "<---";
            this.btnToPassConsumerFactoryPage.UseVisualStyleBackColor = false;
            this.btnToPassConsumerFactoryPage.Click += new System.EventHandler(this.btnToPassConsumerFactoryPage_Click);
            // 
            // lblReportingScreenMessage
            // 
            this.lblReportingScreenMessage.AutoSize = true;
            this.lblReportingScreenMessage.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblReportingScreenMessage.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblReportingScreenMessage.Location = new System.Drawing.Point(325, 91);
            this.lblReportingScreenMessage.Name = "lblReportingScreenMessage";
            this.lblReportingScreenMessage.Size = new System.Drawing.Size(619, 125);
            this.lblReportingScreenMessage.TabIndex = 9;
            this.lblReportingScreenMessage.Text = resources.GetString("lblReportingScreenMessage.Text");
            this.lblReportingScreenMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ReportingScreen_ToProducts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Linen;
            this.ClientSize = new System.Drawing.Size(1126, 647);
            this.Controls.Add(this.lblReportingScreenMessage);
            this.Controls.Add(this.btnToPassConsumerFactoryPage);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.dateTimePickerProductReportingPage);
            this.Controls.Add(this.dataGridViewProductReporting);
            this.Controls.Add(this.lblReportingPageWelcome);
            this.Controls.Add(this.pictureBoxReportingScreen_ToProducts);
            this.Controls.Add(this.menuStripReportingPage);
            this.MainMenuStrip = this.menuStripReportingPage;
            this.Name = "ReportingScreen_ToProducts";
            this.Text = "ReportingScreen_ToProducts";
            this.Load += new System.EventHandler(this.ReportingScreen_ToProducts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxReportingScreen_ToProducts)).EndInit();
            this.menuStripReportingPage.ResumeLayout(false);
            this.menuStripReportingPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.finalProjectDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cUSTOMERREQUESTBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKCUSTOMERREQUESTCUSTOMERREQUEST1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProductReporting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxReportingScreen_ToProducts;
        private System.Windows.Forms.MenuStrip menuStripReportingPage;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tipsAndTricksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label lblReportingPageWelcome;
        private FinalProjectDataSet finalProjectDataSet;
        private System.Windows.Forms.BindingSource cUSTOMERREQUESTBindingSource;
        private FinalProjectDataSetTableAdapters.CUSTOMER_REQUESTTableAdapter cUSTOMER_REQUESTTableAdapter;
        private System.Windows.Forms.BindingSource fKCUSTOMERREQUESTCUSTOMERREQUEST1BindingSource;
        private System.Windows.Forms.DataGridView dataGridViewProductReporting;
        private System.Windows.Forms.DateTimePicker dateTimePickerProductReportingPage;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnToPassConsumerFactoryPage;
        private System.Windows.Forms.Label lblReportingScreenMessage;
    }
}