using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace STG
{
    public partial class FrmImportShippedData : Form
    {
        DataTable dtImport = new DataTable();

        public FrmImportShippedData()
        {
            InitializeComponent();
        }

        private void FrmImportShippedData_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;

            this.WindowState = FormWindowState.Minimized;
            this.WindowState = FormWindowState.Maximized;
        }


        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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

            DataSet ds = CompareData(dt.Copy());

            if (ds.Tables.Contains("Duplicate") && ds.Tables["Duplicate"].Rows.Count > 0)
            {
                DuplicateData(ds.Tables["Duplicate"]);

                String message = @"Duplicate Records Found";
                String caption = "Import Successful";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            } else
            {
                String message = @"No Duplicate Rows Found";
                String caption = "Import Successful";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }

            if (ds.Tables.Contains("Result") && ds.Tables["Result"].Rows.Count > 0)
            {
                HideUneccessaryColumns(ds.Tables["Result"]);

                var rows = ds.Tables["Result"].Rows.OfType<DataRow>();
                var columnTotal = rows.Sum(r => Convert.ToInt32((r["Shipped"].ToString() == "") ? 0 : int.Parse(r["Shipped"].ToString())));

                if (columnTotal == 0)
                {
                    String message = @"No New Records Found";
                    String caption = "Import Successful";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, caption, buttons);
                } else
                {
                    String message = @"New Records Found";
                    String caption = "Import Successful";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, caption, buttons);
                }

            } else
            {
                String message = @"No Records Found";
                String caption = "Import Successful";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }




        }

        private void DuplicateData(DataTable dt)
        {
            dgDuplicate.Columns.Clear();
            dt = ReadyDataTable(dt);
            dgDuplicate.DataSource = dt;

            dgDuplicate.Columns["Ship To Code"].Visible = false;
            dgDuplicate.Columns["Ship To Name"].Visible = false;
            dgDuplicate.Columns["Bill To Name"].Visible = false;
            dgDuplicate.Columns["DC"].Visible = false;
            dgDuplicate.Columns["Retailer"].Visible = false;
            dgDuplicate.Columns["UPC"].Visible = false;
            dgDuplicate.Columns["BHFWave"].Visible = false;
            dgDuplicate.Columns["Reference"].Visible = false;
            dgDuplicate.Columns["PO"].Visible = false;
            dgDuplicate.Columns["Load"].Visible = false;
            dgDuplicate.Columns["Status"].Visible = false;
            dgDuplicate.Columns["Seal"].Visible = false;
            dgDuplicate.Columns["Retailer Name"].Visible = false;
            dgDuplicate.Columns["Bill To"].Visible = false;
            dgDuplicate.Columns["Order Entry"].Visible = false;
            dgDuplicate.Columns["Cancel Date"].Visible = false;
            dgDuplicate.Columns["App Date"].Visible = false;
            dgDuplicate.Columns["OrderID"].Visible = false;
            dgDuplicate.Columns["External"].Visible = false;
            dgDuplicate.Columns["ItemNo"].Visible = false;
            dgDuplicate.Columns["Ordered"].Visible = false;
            dgDuplicate.Columns["Wave-Order-Item"].Visible = false;
            
            dgDuplicate.AutoResizeColumns();
        }

        private void HideUneccessaryColumns(DataTable dt)
        {
            dgImportData.Columns.Clear();

            dt = ReadyDataTable(dt);

            dgImportData.DataSource = dt;

            dgImportData.Columns["Ship To Code"].Visible = false;
            dgImportData.Columns["Ship To Name"].Visible = false;
            dgImportData.Columns["Bill To Name"].Visible = false;
            dgImportData.Columns["DC"].Visible = false;
            dgImportData.Columns["Retailer"].Visible = false;
            dgImportData.Columns["UPC"].Visible = false;
            dgImportData.Columns["BHFWave"].Visible = false;
            dgImportData.Columns["Reference"].Visible = false;
            dgImportData.Columns["PO"].Visible = false;
            dgImportData.Columns["Load"].Visible = false;
            dgImportData.Columns["Status"].Visible = false;
            dgImportData.Columns["Seal"].Visible = false;
            dgImportData.Columns["Retailer Name"].Visible = false;
            dgImportData.Columns["Bill To"].Visible = false;
            dgImportData.Columns["Order Entry"].Visible = false;
            dgImportData.Columns["Cancel Date"].Visible = false;
            dgImportData.Columns["App Date"].Visible = false;
            dgImportData.Columns["OrderID"].Visible = false;
            dgImportData.Columns["External"].Visible = false;
            dgImportData.Columns["ItemNo"].Visible = false;
            dgImportData.Columns["Ordered"].Visible = false;
            dgImportData.Columns["Wave-Order-Item"].Visible = false;

            dgImportData.AutoResizeColumns();

        }

        private DataTable ReadyDataTable(DataTable dt)
        {

            dt.Columns["Wave"].SetOrdinal(1);
            dt.Columns["OrderNo"].SetOrdinal(2);
            dt.Columns["PO Number"].SetOrdinal(3);
            dt.Columns["Item"].SetOrdinal(4);
            dt.Columns["Description"].SetOrdinal(5);
            dt.Columns["Mode"].SetOrdinal(6);
            dt.Columns["Load Auth/MVDP"].SetOrdinal(7);
            dt.Columns["Trailer"].SetOrdinal(8);
                      
            dt.Columns["Weight"].SetOrdinal(9);
            dt.Columns["Cube"].SetOrdinal(10);
            dt.Columns["BOL"].SetOrdinal(11);
            dt.Columns["Pro Number"].SetOrdinal(12);
            dt.Columns["Carrier"].SetOrdinal(13);
            dt.Columns["Upload Time"].SetOrdinal(14);
            dt.Columns["Status Update"].SetOrdinal(15);
            dt.Columns["Ordered Qty"].SetOrdinal(16);
            dt.Columns["Shipped"].SetOrdinal(17);
            dt.Columns["Cartons"].SetOrdinal(18);
            dt.Columns["Difference"].SetOrdinal(19);
            dt.Columns["Shipment Confirmation"].SetOrdinal(20);

            return dt;
        }

        private DataSet CompareData(DataTable dtCSV)
        {
            DataSet ds = new DataSet();
          

            DataTable dtSTGDataFromDB = STGComm.GetSTGDataFromWave(null);

            DataColumn dataColumn = new DataColumn("Wave-Order-Item");
            dataColumn.Expression = string.Format("{0}+'-'+{1}+'-'+{2}", "BHFWave", "Reference", "Item");

            dtCSV.Columns.Add(dataColumn);
            dtCSV.Columns[22].ColumnName = "ItemNo";


            dtSTGDataFromDB.Columns[1].ColumnName = "OrderNo";
            DataColumn dColumn = new DataColumn("Wave-Order-Item");
            dColumn.Expression = string.Format("{0}+'-'+{1}+'-'+{2}", "Wave", "OrderNo", "Item");

            dtSTGDataFromDB.Columns.Add(dColumn);

            
            DataTable dtResult = DataTableHelper.JoinTwoDataTablesOnOneColumn(dtSTGDataFromDB, dtCSV, "Wave-Order-Item", DataTableHelper.JoinType.Left);
            dtResult.TableName = "Result";
            dtResult.Columns.Add("Difference");

            DataTable dtDuplicate = dtResult.Clone();
            dtDuplicate.TableName = "Duplicate";

            DataRow newRow = dtDuplicate.NewRow();

            var rowsToDelete = new List<DataRow>();

            foreach (DataRow dr in dtResult.Rows)
            {
                dr["Difference"] = ((dr["Ordered Qty"].ToString() == "") ? 0 : int.Parse(dr["Ordered Qty"].ToString()))  - ((dr["Shipped"].ToString() == "") ? 0 : int.Parse(dr["Shipped"].ToString()));
                if( dr["Shipment Confirmation"].ToString() == "Yes" && int.Parse(dr["Difference"].ToString()) < 1 )
                {
                    newRow = dr;
                    dtDuplicate.ImportRow(newRow);
                    dtDuplicate.AcceptChanges();
                    rowsToDelete.Add(dr);

                    DataRow[] DrImportDelete = dtImport.Select("BHFWave = '"+dr["Wave"].ToString()+"' AND Reference = '"+dr["OrderNo"].ToString()+"' AND Item = '"+dr["Item"].ToString()+"'");
                    foreach (DataRow row in DrImportDelete)
                    {
                        dtImport.Rows.Remove(row);
                    }
                }

            }
            rowsToDelete.ForEach(x => dtResult.Rows.Remove(x));

            ds.Tables.Add(dtResult);
            ds.Tables.Add(dtDuplicate);

            

            return ds;
           

        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            String message = "You are Importing File Data in System. Continue this operation?";
            String caption = "Irreversible Action";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            var result = MessageBox.Show(message, caption, buttons);

            if (result == DialogResult.No)
            {
                return;
            }

            if(dtImport == null || dtImport.Rows.Count < 1)
            {
                message = "Please Select a File Before Import";
                caption = "Import Failed";
                buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
                return;
            }
            DataTable dtShippedFile = dtImport;
            dt = dtShippedFile.Copy();

            try
            {
                if (PrepareDataInsert(dt))
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
                buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
            
        }

        private Boolean PrepareDataInsert(DataTable dt)
        {
            return STGComm.SubmitShippedDatainSystem(dt);
        }

        private void buttonReLoad_Click(object sender, EventArgs e)
        {
            dtImport = null;
            dgImportData.DataSource = null;
            dgImportData.Update();
            dgImportData.Refresh();
        }

        //Export to Excel
        //
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "Shippent_Comparison.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // Copy DataGridView results to clipboard
                copyAlltoClipboard();

                object misValue = System.Reflection.Missing.Value;
                Excel.Application xlexcel = new Excel.Application();

                xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                Excel.Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);
                Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                // Format column D as text before pasting results, this was required for my data
                Excel.Range rng = xlWorkSheet.get_Range("D:D").Cells;
                rng.NumberFormat = "@";

                // Paste clipboard results to worksheet range
                Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[1, 1];
                CR.Select();
                xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                // For some reason column A is always blank in the worksheet. ¯\_(ツ)_/¯
                // Delete blank column A and select cell A1
                Excel.Range delRng = xlWorkSheet.get_Range("A:A").Cells;
                delRng.Delete(Type.Missing);
                xlWorkSheet.get_Range("A1").Select();

                // Save the excel file under the captured location from the SaveFileDialog
                xlWorkBook.SaveAs(sfd.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlexcel.DisplayAlerts = true;
                xlWorkBook.Close(true, misValue, misValue);
                xlexcel.Quit();

                ReleaseObject(xlWorkSheet);
                ReleaseObject(xlWorkBook);
                ReleaseObject(xlexcel);

                // Clear Clipboard and DataGridView selection
                Clipboard.Clear();
                dgImportData.ClearSelection();

                // Open the newly saved excel file
                if (File.Exists(sfd.FileName))
                    System.Diagnostics.Process.Start(sfd.FileName);
            }
        }

        private void copyAlltoClipboard()
        {
            dgImportData.SelectAll();
            DataObject dataObj = dgImportData.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occurred while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
