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
    public partial class FrmReceiving : Form
    {
        DataTable dtImportFile = new DataTable();

        public FrmReceiving()
        {
            InitializeComponent();
        }

        private void FrmReceiving_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;

            this.WindowState = FormWindowState.Minimized;
            this.WindowState = FormWindowState.Maximized;
        }


        private void BtnImportInboundContainers_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String StrFileName = openFileDialog.FileName;

                ProcessInputFile(StrFileName);
            }
        }

        private void ProcessInputFile(String FileName)
        {
            DataTable dt = ListToDT.CSVtoDatatable(FileName);

            DgImportInbound.DataSource = dt;
            dtImportFile = dt.Copy();
            dt.Dispose();
        }

        private Boolean StoreInbondContainerData(DataTable dt)
        {
            return STGComm.SubmitInboundImport(dt);
        }

        private void BtnImportInbound_Click(object sender, EventArgs e)
        {
            String message = "You are Importing File Data in System. Continue this operation?";
            String caption = "Irreversible Action";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            var result = MessageBox.Show(message, caption, buttons);

            if (result == DialogResult.No)
            {
                return;
            }

            if (dtImportFile == null || dtImportFile.Rows.Count < 1)
            {
                message = "Please Select a File Before Import";
                caption = "Import Failed";
                buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
                return;
            }

            try
            {
                if (StoreInbondContainerData(dtImportFile))
                {

                    message = @"Data Import Successfull";
                    caption = "Import Successful";
                    buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, caption, buttons);
                    //SqlDataLoad_Click(sender, e);
                    this.Refresh();
                }
                else
                {
                    message = "Data Import Failed, Contact Administrator";
                    caption = "Import Failed";
                    buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, caption, buttons);
                }
            }
            catch (Exception ex)
            {
                message = "File Import failed. Message: " + ex.Message;
                caption = "Import Failed";

            }
        }
    }
}