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
    public partial class FrmBBBVendorNumberCheck : Form
    {
        DataTable dtItems = new DataTable();
        STGComm sTGComm = new STGComm();
        Boolean updateCheck = false;

        public FrmBBBVendorNumberCheck()
        {
            InitializeComponent();
            DisplayData();
        }

        //Display Item List
        private void DisplayItemList()
        {
            try
            {
                dtItems = sTGComm.GetItemsWithVendorNumber();

                dgvItemDetails.DataSource = dtItems;
                dgvItemDetails.AutoResizeColumns();
                dgvItemDetails.Refresh();
                

            } catch (Exception e)
            {
                TextHelper.WriteLine("Item And Vendor details Request failed with error : " + e.Message);
            } 
        }

        //Display Data in DataGridView
        private void DisplayData()
        {
            try
            {
                
                DataTable dtVendorNumber = new DataTable();
                dtVendorNumber =  sTGComm.GetBBBVendorNumber();

                cmbVendorNumber.DataSource = dtVendorNumber;
                cmbVendorNumber.DisplayMember = dtVendorNumber.Columns[0].ColumnName;
                cmbVendorNumber.ValueMember = dtVendorNumber.Columns[1].ColumnName;
                cmbVendorNumber.BindingContext = this.BindingContext;
                cmbVendorNumber.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbVendorNumber.SelectedIndex = -1;

                DisplayItemList();

            } catch (Exception e)
            {
                TextHelper.WriteLine("BBB Vendor Number Request failed with error : " + e.Message);
            }
            
        }

        //Clear Data
        private void ClearData()
        {
            txtItem.Text = "";
            cmbVendorNumber.SelectedIndex = -1;
            updateCheck = false;
        }

        private void FrmBBBVendorNumberCheck_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;

            this.WindowState = FormWindowState.Minimized;
            this.WindowState = FormWindowState.Maximized;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            bool parseOK = Int32.TryParse(cmbVendorNumber.SelectedValue.ToString(), out int vendorConnection);
            if (txtItem.Text != "" && cmbVendorNumber.SelectedIndex != -1 && txtItem.Text.Length == 5 && parseOK == true)
            {
                if(!sTGComm.SubmitVendorNumbercheck(txtItem.Text.Trim(), vendorConnection)){

                    String message = @"Data Entry Failed";
                    String caption = "Alert";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, caption, buttons);

                } else
                {
                    String message = @"Item Added Successfully";
                    String caption = "Alert";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, caption, buttons);

                    DisplayItemList();
                    ClearData();
                }


            } else
            {
                String message = @"Please Enter Valid Item Number And Select Vendor Number";
                String caption = "Alert";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
        }

        //DgvItemDetails RowHeaderMouseClick Event
        private void DgvItemDetails_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtItem.Text = dgvItemDetails.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtItem.ReadOnly = true;
            cmbVendorNumber.SelectedIndex = cmbVendorNumber.FindStringExact(dgvItemDetails.Rows[e.RowIndex].Cells[1].Value.ToString());
            updateCheck = true;
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (updateCheck == true && txtItem.Text != "" && cmbVendorNumber.SelectedIndex != -1 && txtItem.Text.Length == 5)
                {
                    bool parseOK = Int32.TryParse(cmbVendorNumber.SelectedValue.ToString(), out int vendorConnection);

                    if (!sTGComm.UpdateVendorNumbercheck(txtItem.Text.Trim(), vendorConnection))
                    {

                        String message = @"Data Update Failed";
                        String caption = "Alert";
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        MessageBox.Show(message, caption, buttons);

                    }
                    else
                    {
                        String message = @"Item Updated Successfully";
                        String caption = "Alert";
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        MessageBox.Show(message, caption, buttons);

                        DisplayItemList();
                        ClearData();
                    }

                }
                else
                {
                    String message = @"Please Select the Row Again or Select Vendor Number";
                    String caption = "Alert";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, caption, buttons);
                }
            } catch (Exception ex)
            {
                TextHelper.WriteLine("BBB Vendor Number Request failed with error : " + ex.Message);

                String message = @"Please Enter Valid Item Number And Select Vendor Number";
                String caption = "Alert";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);

            }
            
        }
    }
}
