using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STG
{
    public partial class FrmReceivingConfirmation : Form
    {
        DataTable dtImport = new DataTable();

        public FrmReceivingConfirmation()
        {
            InitializeComponent();
        }

        private void FrmReceivingConfirmation_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;

            this.WindowState = FormWindowState.Minimized;
            this.WindowState = FormWindowState.Maximized;
        }

        private void BtnFileSelection_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String StrFileName = openFileDialog.FileName;

                ProcessInputFile(StrFileName);
            }
        }

        //To process input file
        private void ProcessInputFile(String FileName)
        {
            DataTable dt = ListToDT.CSVtoDatatable(FileName);

            dtImport = dt.Copy();

            //dt = CompareData(dt.Copy());

            //HideUneccessaryColumns(dt);


        }

        //private void HideUneccessaryColumns(DataTable dt)
        //{
        //    dgImportData.Columns.Clear();
        //
        //    dt = ReadyDataTable(dt);
        //
        //    dgImportData.DataSource = dt;
        //
        //    dgImportData.Columns["Ship To Code"].Visible = false;
        //    dgImportData.Columns["Ship To Name"].Visible = false;
        //    dgImportData.Columns["Bill To Name"].Visible = false;
        //    dgImportData.Columns["DC"].Visible = false;
        //    dgImportData.Columns["Retailer"].Visible = false;
        //    dgImportData.Columns["UPC"].Visible = false;
        //    dgImportData.Columns["BHFWave"].Visible = false;
        //    dgImportData.Columns["Reference"].Visible = false;
        //    dgImportData.Columns["PO"].Visible = false;
        //    dgImportData.Columns["Load"].Visible = false;
        //    dgImportData.Columns["Status"].Visible = false;
        //    dgImportData.Columns["Seal"].Visible = false;
        //    dgImportData.Columns["Retailer Name"].Visible = false;
        //    dgImportData.Columns["Bill To"].Visible = false;
        //    dgImportData.Columns["Order Entry"].Visible = false;
        //    dgImportData.Columns["Cancel Date"].Visible = false;
        //    dgImportData.Columns["App Date"].Visible = false;
        //    dgImportData.Columns["OrderID"].Visible = false;
        //    dgImportData.Columns["External"].Visible = false;
        //    dgImportData.Columns["ItemNo"].Visible = false;
        //    dgImportData.Columns["Ordered"].Visible = false;
        //    dgImportData.Columns["Wave-Order-Item"].Visible = false;
        //
        //    dgImportData.AutoResizeColumns();
        //
        //}
    }
}
