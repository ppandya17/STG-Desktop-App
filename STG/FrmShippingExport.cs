using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Qlik.Engine;
using System.Data.SqlClient;

using System.Configuration;


namespace STG
{
    public partial class FrmShippingExport : Form
    {
       
        public FrmShippingExport()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            //this.Visible = false;
            //SplashForm.ShowSplashScreen();
            this.ControlBox = false;
            
            this.WindowState = FormWindowState.Minimized;
            this.WindowState = FormWindowState.Maximized;

            lblVerisonNumber.Text = "1.0.4";
            
            DataTable daSTG = GetSTGQuery(null);

            TextHelper.WriteLine("Data received from STG, Form_load");

            LoadDataTable(daSTG, "Shipped");

//            SplashForm.CloseForm();
            //this.Visible = true;
            //this.BringToFront();

        }

        private System.Data.DataTable PHSqlConnection(String connString, String query) {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            System.Data.DataTable daPowerhouse = new System.Data.DataTable();


            try
            {
                conn = new SqlConnection(connString);
                cmd = new SqlCommand(query, conn);
                conn.Open();

                

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(daPowerhouse);
                conn.Close();
                da.Dispose();
            }
            catch (Exception e) {
                TextHelper.Print(e.Message, "Exception in SQL connection");
                if (conn != null && conn.State != ConnectionState.Closed) {
                    conn.Close();
                }
            }

            return daPowerhouse;
            
        }

        private System.Data.DataTable BuildPHQuery() {

            String connetionString = ConfigurationManager.ConnectionStrings["Powerhouse"].ConnectionString;
            String query = @"select * from View_OrdersShippingToSTG";

            return PHSqlConnection(connetionString, query);
        }

        private void SqlDataLoad_Click(object sender, EventArgs e)
        {
            try
            {
                TextHelper.WriteLine("Log Path : " + TextHelper.GetTempPath());

                dataGridGP.DataSource = null;
                dataGridGP.Update();
                dataGridGP.Refresh();


                DataTable daPowerhouse = new DataTable();
                DataTable daGP = new DataTable();
                DataTable daSTG = new DataTable();
                DataTable dafinal = new DataTable();
                DataTable dtItems = new DataTable();

                daPowerhouse = BuildPHQuery();

                TextHelper.WriteLine("Data received from Powerhouse");
                //dataGridView.Columns.Clear();
                //dataGridView.DataSource = daPowerhouse;

                var itemIds = daPowerhouse.AsEnumerable().Select(r => r.Field<String>("StrItemId")).ToList();
                daGP = BuildGPQuery(itemIds);

                TextHelper.WriteLine("Data received from GP Inventory");

                List<StgOrdersCSV> ListOfObjects = daPowerhouse.DataTableToList<StgOrdersCSV>();
                ListOfObjects = ListToDT.AddGPDatatoObjects(ListOfObjects, daGP, "Inventory");



                var ordernNumber = daPowerhouse.AsEnumerable().Select(r => r.Field<String>("StrCustOrdNumber")).ToList();
                daGP = BuildGPSalesQuery(ordernNumber);

                TextHelper.WriteLine("Data received from GP Sales ");

                ListOfObjects = ListToDT.AddGPDatatoObjects(ListOfObjects, daGP, "Order");

                //Add Custom Vendor Numbers============================================================

                STGComm sTGComm = new STGComm();
                dtItems = sTGComm.GetItemsWithVendorNumber();

                ListOfObjects = ListToDT.AddSTGVendorNumberDatatoObjects(ListOfObjects, dtItems, "BBBVendorNumber");
                //End adding custom vendor numbres======================================================


                // START DATA FILTERATION PROCESS BASED ON STG DB
                //Get STG data to filter out old waves

                daSTG = GetSTGQuery(ordernNumber);

                TextHelper.WriteLine("Data received from STG");

                LoadDataTable(daSTG, "Shipped");
                //dataGridSTG.Columns.Clear();
                //dataGridSTG.DataSource = daSTG;

                ListOfObjects = ListToDT.FilterDataFromSTG(ListOfObjects, daSTG);

                // END FILTERATION PROCESS
                TextHelper.WriteLine("Final Table Loaded");
                if (ListOfObjects != null && ListOfObjects.Count > 0)
                {
                    dafinal = ListToDT.ToDataTable<StgOrdersCSV>(ListOfObjects);
                    LoadDataTable(dafinal, "Orders For Ship");

                    String message = "Data Loading Completed. Showing New Wave Found in PowerHouse";
                    String caption = "Loading Successfull";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, caption, buttons);

                } else
                {
                    String message = "No New Waves found in Powerhouse";
                    String caption = "Loading Successfull";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, caption, buttons);
                }

            }
            catch (Exception ex)
            {
                String message = "Error while Getting Data from Powerhouse. Message: " + ex.Message;
                String caption = "Data Loading Failed";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }

            //dataGridGP.Columns.Clear();
            //dataGridGP.DataSource = dafinal;

        }

        private  System.Data.DataTable JoinTables(System.Data.DataTable daPH, System.Data.DataTable daGP) {
            //System.Data.DataTable daFinal = new System.Data.DataTable();

            var joinResults = (from p in daPH.AsEnumerable()
                               join t in daGP.AsEnumerable()
                               on p.Field<String>("item_id") equals t.Field<String>("ITEMNMBR")
                               select new
                               {
                                   OrderId = p.Field<string>("order_id"),
                                   ItemNumber = p.Field<string>("item_id"),
                                   b_company = p.Field<string>("b_company"),
                                   b_address1 = p.Field<string>("b_address1"),
                                   b_address2 = p.Field<string>("b_address2"),
                                   b_address3 = p.Field<string>("b_address3"),
                                   b_city = p.Field<string>("b_city"),
                                   b_state = p.Field<string>("b_state"),
                                   b_zip = p.Field<string>("b_zip"),
                                   b_country = p.Field<string>("b_country"),
                                   b_contact = p.Field<string>("b_contact"),
                                   b_phone = p.Field<string>("b_phone"),
                                   s_company = p.Field<string>("s_company"),
                                   po_num = p.Field<string>("po_num"),
                                   date_ordered = p.Field<string>("date_ordered"),
                                   cust_id = p.Field<string>("cust_id"),
                                   //pieces_hard = p.Field<string>("pieces_hard"),
                                   store_id = p.Field<string>("store_id"),

                                   ITEMDESC = t.Field<string>("ITEMDESC"),
                                   ITMSHNAM = t.Field<int>("ITMSHNAM")

                               }).ToList();

            return ListToDT.ToDataTable(joinResults);
        }

        private System.Data.DataTable BuildGPQuery(List<String> itemID)
        {

            String connetionString = ConfigurationManager.ConnectionStrings["GP"].ConnectionString;
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"select distinct 
                                       LTRIM(RTRIM(ITEMNMBR)) as ITEMNMBR,
                                       LTRIM(RTRIM(ITEMDESC)) as ITEMDESC, 
                                       LTRIM(RTRIM(ITMSHNAM)) as ITMSHNAM from IV00101 where ITEMNMBR in (");
            foreach (String item in itemID) {
                queryBuilder.Append("'" + item + "',");
            }
            queryBuilder.Length--;
            queryBuilder.Append(")");

            String query = queryBuilder.ToString().TrimEnd(',');

            TextHelper.WriteLine(query);

            return GPSqlConnection(connetionString, query);
        }

        private System.Data.DataTable GPSqlConnection(String connString, String query)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            System.Data.DataTable daGP = new System.Data.DataTable();

            try
            {
                conn = new SqlConnection(connString);
                cmd = new SqlCommand(query, conn);
                conn.Open();



                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(daGP);
                conn.Close();
                da.Dispose();
            }
            catch (Exception e)
            {
                TextHelper.Print(e.Message, "Exception in SQL connection");
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }

            return daGP;

        }

        private System.Data.DataTable BuildGPSalesQuery(List<String> orderNumber)
        {

            String connetionString = ConfigurationManager.ConnectionStrings["GP"].ConnectionString;
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"select distinct 
                                       LTRIM(RTRIM(CUSTNMBR)) as GP_cust_number,
                                       LTRIM(RTRIM(SOPNUMBE)) as order_id 
                                  from SOP10100 where SOPType = '2' and SOPNUMBE in (");
            foreach (String item in orderNumber)
            {
                queryBuilder.Append("'" + item + "',");
            }
            queryBuilder.Length--;
            queryBuilder.Append(")");

            String query = queryBuilder.ToString().TrimEnd(',');

            TextHelper.WriteLine(query);

            return GPSqlConnection(connetionString, query);
        }


        private System.Data.DataTable GetSTGQuery(List<String> orderNumber)
        {
           StringBuilder queryBuilder = new StringBuilder();
 
            queryBuilder.Append(@" select * from [View_Item_Shipped] ");
            if (orderNumber != null)
            {
                queryBuilder.Append(" where [Customer Order Number] in (");
                foreach (String item in orderNumber)
                {
                    queryBuilder.Append("'" + item + "',");
                }
                queryBuilder.Length--;
                queryBuilder.Append(")");

            }

            String query = queryBuilder.ToString().TrimEnd(',');

            TextHelper.WriteLine(query);

            return STGComm.STGGetConnectionGenerator(query);
            
        }

        private System.Data.DataTable SaveSTGQuery(List<String> itemID)
        {

            String connetionString = ConfigurationManager.ConnectionStrings["GP"].ConnectionString;
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"select distinct 
                                       LTRIM(RTRIM(ITEMNMBR)) as ITEMNMBR,
                                       LTRIM(RTRIM(ITEMDESC)) as ITEMDESC, 
                                       LTRIM(RTRIM(ITMSHNAM)) as ITMSHNAM from IV00101 where ITEMNMBR in (");
            foreach (String item in itemID)
            {
                queryBuilder.Append("'" + item + "',");
            }
            queryBuilder.Length--;
            queryBuilder.Append(")");

            String query = queryBuilder.ToString().TrimEnd(',');

            TextHelper.WriteLine("SaveSTGQuery, " + query);

            return GPSqlConnection(connetionString, query);
        }

        private void SubmittoSTG(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            String message = "You are going to send Order Details to St. George warehouse. Continue this operation?";
            String caption = "Irreversible Action";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            var result = MessageBox.Show(message, caption, buttons);

            if(result == DialogResult.No)
            {
                
                return;
            }

            DataTable dtSTG = (DataTable)dataGridGP.DataSource;
            dt = dtSTG.Copy();

            try
            {
                if (STGComm.SubmitDatatoSTG(dt))
                {

                    message = @"Data Successfully Sent to FTP Drive";
                    caption = "Export Successful";
                    buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, caption, buttons);
                    SqlDataLoad_Click(sender, e);

                }
                else
                {
                    message = "File Generation failed, Contact Administrator";
                    caption = "Export Failed";
                    buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, caption, buttons);
                }
            } catch (Exception ex)
            {
                message = "File Generation failed. Message: " + ex.Message;
                caption = "Export Failed";
                buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
            

           
        }

        private void BtnGenerateExcel(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            DataTable dtSTG = (DataTable)dataGridGP.DataSource;

          

            try
            {
                dt = dtSTG.Copy();

                ListToDT.GenerateExcel(dt);

                String message = @"File exported in Path: C:\STG_Excel_Exports\STG_" + DateTime.Now.ToString("MMddyyyy_HH_mm_ss") + ".xlsx";
                String caption = "Export Successful";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
            catch (Exception ex)
            {
                String message = "File Generation failed. Message: "+ex.Message;
                String caption = "Export Failed";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }

        }

        public void LoadDataTable(DataTable dt, String Identifier)
        {
            TextHelper.WriteLine("Entering Data Grid for new naming");
            try
            {
                if (Identifier == "Shipped")
                {
                    dataGridSTG.Columns.Clear();

                    dataGridSTG.DataSource = dt;

                    //dataGridSTG.Columns[0].HeaderText = "Order Number";
                    //dataGridSTG.Columns[1].HeaderText = "PO Number";
                    //dataGridSTG.Columns[2].HeaderText = "Item No";



                }
                else
                {

                    dataGridGP.Columns.Clear();


                    dataGridGP.DataSource = dt;

                    dataGridGP.Columns[0].HeaderText = "Customer Order Number";
                    dataGridGP.Columns[1].HeaderText = "Customer PO";
                    dataGridGP.Columns[2].HeaderText = "Ship Type";
                    dataGridGP.Columns[3].HeaderText = "Carrier / SCAC";
                    dataGridGP.Columns[4].HeaderText = "Small Package Service";
                    dataGridGP.Columns[5].HeaderText = "Ship Method of Payment";
                    dataGridGP.Columns[6].HeaderText = "Ship to Code";
                    dataGridGP.Columns[7].HeaderText = "Ship to Name";
                    dataGridGP.Columns[8].HeaderText = "Bill to Name";
                    dataGridGP.Columns[9].HeaderText = "Bill to Address Line 1";
                    dataGridGP.Columns[10].HeaderText = "Bill to Address Line 2";
                    dataGridGP.Columns[11].HeaderText = "Bill to City";
                    dataGridGP.Columns[12].HeaderText = "Bill to State";
                    dataGridGP.Columns[13].HeaderText = "Bill to Postal Code";
                    dataGridGP.Columns[14].HeaderText = "Bill to Country";
                    dataGridGP.Columns[15].HeaderText = "Bill to Phone";
                    dataGridGP.Columns[16].HeaderText = "Bill to Email";
                    dataGridGP.Columns[17].HeaderText = "DC";
                    dataGridGP.Columns[18].HeaderText = "DEPT #";
                    dataGridGP.Columns[19].HeaderText = "CON ID / Retailer ID";
                    dataGridGP.Columns[20].HeaderText = "BBB Vendor Number";
                    dataGridGP.Columns[21].HeaderText = "Item";
                    dataGridGP.Columns[22].HeaderText = "Quantity Ordered";
                    dataGridGP.Columns[23].HeaderText = "DESCRIPTION";
                    dataGridGP.Columns[24].HeaderText = "UPC";
                    dataGridGP.Columns[25].HeaderText = "Wave Number";

                    var rows = dt.Rows.OfType<DataRow>();
                    //for (int i = 1; i < dt.Columns.Count; i++)
                    //{
                    var columnTotal = rows.Sum(r => Convert.ToInt32(r["StrQTYOrdered"]));
                    //}
                    TotalQuantity.Text = columnTotal.ToString();
                    //TotalQuantity.Text = dt.Compute("Sum(StrQTYOrdered)", String.Empty).ToString();
                    dataGridGP.AutoResizeColumns();

                }
                TextHelper.WriteLine("DataGrid Loaded with new names");
            }
            catch (Exception ex)
            {
                TextHelper.WriteLine("Error in Data Grid Loading : " +ex.Message);
            }
            
            TextHelper.WriteLine("DataGrid Loaded wit hnew names");
        }

        private void dataGridSTG_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dataGridSTG.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void dataGridGP_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dataGridGP.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }
        
        private void TxtWaveSearch_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["STG"].ConnectionString);
            con.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from [View_Item_Shipped] where [Wave] like '" + txtWaveSearch.Text + "%'", con);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dataGridSTG.DataSource = dt;
            con.Close();

        }

        private void BtnSentSTG_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            DataTable dtSTG = (DataTable)dataGridSTG.DataSource;

            dt = dtSTG.Copy();

            try
            {
                ListToDT.GenerateExcelShipped(dt);

                String message = @"File exported in Path: C:\STG_Excel_Exports\ShippedData_" + DateTime.Now.ToString("MMddyyyy_HH_mm_ss") + ".xlsx";
                String caption = "Export Successful";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
            catch (Exception ex)
            {
                String message = "File Generation failed. Message: " + ex.Message;
                String caption = "Export Failed";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
        }

       
    }

}
