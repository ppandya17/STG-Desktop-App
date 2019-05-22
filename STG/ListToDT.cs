using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using OfficeOpenXml;
using System.IO;

namespace STG
{
    public static class ListToDT
    {
        public static System.Data.DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            System.Data.DataTable table = new System.Data.DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static List<StgOrdersCSV> DataTableToList<StgOrdersCSV>(this DataTable table) where StgOrdersCSV : class, new()
        {
            try
            {
                List<StgOrdersCSV> list = new List<StgOrdersCSV>();

                foreach (var row in table.AsEnumerable())
                {
                    StgOrdersCSV obj = new StgOrdersCSV();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType));
                        }
                        catch(Exception e)
                        {
                            TextHelper.WriteLine(e.Message);
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                TextHelper.WriteLine("Error in DataTableToList");
                return null;
            }
        }

        public static List<StgOrdersCSV> AddGPDatatoObjects(List<StgOrdersCSV> listSTG, DataTable daGP, String Identifier)
        {
            try
            {
                StgOrdersCSV obj = new StgOrdersCSV();

                if (Identifier == "Inventory")
                {
                    foreach (var row in daGP.AsEnumerable())
                    {

                        listSTG.Where(o => o.StrItemId == (String)row["ITEMNMBR"].ToString()).ToList().ForEach(o => o.StrUPC = (String)row["ITMSHNAM"].ToString());
                        listSTG.Where(o => o.StrItemId == (String)row["ITEMNMBR"].ToString()).ToList().ForEach(o => o.StrDesription = (String)row["ITEMDESC"].ToString());

                    }
                }

                if (Identifier == "Order")
                {
                    foreach (var row in daGP.AsEnumerable())
                    {
                        listSTG.Where(o => o.StrCustOrdNumber == (String)row["order_id"].ToString()).ToList().ForEach(o => o.StrRetailerID = (String)row["GP_cust_number"].ToString());
                    }
                }
                

            } catch
            {
                TextHelper.WriteLine("Error in AddGPDatatoObjects");
                return null;
            }

            return listSTG;
        }

        public static List<StgOrdersCSV> AddSTGVendorNumberDatatoObjects(List<StgOrdersCSV> listSTG, DataTable daGP, String Identifier)
        {
            try
            {
                StgOrdersCSV obj = new StgOrdersCSV();

                if (Identifier == "BBBVendorNumber")
                {
                    foreach (var row in daGP.AsEnumerable())
                    {

                        listSTG.Where(o => o.StrItemId == (String)row["Item"].ToString()).ToList().ForEach(o => o.StrBBBVendorNumber = (String)row["Vendor Number"].ToString());
                        //listSTG.Where(o => o.StrItemId == (String)row["ITEMNMBR"].ToString()).ToList().ForEach(o => o.StrDesription = (String)row["ITEMDESC"].ToString());

                    }
                }

                if (Identifier != "BBBVendorNumber")
                {
                    //foreach (var row in daGP.AsEnumerable())
                    //{
                    //    listSTG.Where(o => o.StrCustOrdNumber == (String)row["order_id"].ToString()).ToList().ForEach(o => o.StrRetailerID = (String)row["GP_cust_number"].ToString());
                    //}
                }


            }
            catch
            {
                TextHelper.WriteLine("Error in AddGPDatatoObjects");
                return null;
            }

            return listSTG;
        }

        public static List<StgOrdersCSV> FilterDataFromSTG(List<StgOrdersCSV> listSTG, DataTable daSTG)
        {
            try
            {
                StgOrdersCSV obj = new StgOrdersCSV();
                foreach (var row in daSTG.AsEnumerable())
                {
                    
                    var item = listSTG.FirstOrDefault(x => x.StrCustOrdNumber == (String)row["Customer Order Number"].ToString() && x.StrItemId == (String)row["Item"].ToString());
                    //TextHelper.WriteLine("SQL  : [Order Number: " + (String)row["Customer Order Number"].ToString());
                    //TextHelper.WriteLine("List : [PO: " + item.StrCustPO + ", Item: " + item.StrItemId);
                    //TextHelper.WriteLine("SQL  : [PO: " + (String)row["PO Number"].ToString() + ", Item: " + (String)row["Item"].ToString());
                    if (item.StrCustPO == (String)row["PO Number"].ToString() && item.StrItemId == (String)row["Item"].ToString())
                    {
                        listSTG.Remove(item);
                        //TextHelper.WriteLine("Remove");
                    }
                }
                TextHelper.WriteLine("Object Remove Finished, FilterDataFromSTG");

            }
            catch (Exception e)
            {
                TextHelper.WriteLine("Error in FilterDataFromSTG");
                TextHelper.WriteLine(e.Message);
                return null;
            }

            return listSTG;
        }

        // Generate Excel for Main DataSet
        public static void GenerateExcel(DataTable dt)
        {
            ExcelPackage excelPkg = new ExcelPackage();
            ExcelWorksheet wsSheet1 = excelPkg.Workbook.Worksheets.Add("sheet1");
            using (ExcelRange r1 = wsSheet1.Cells[2, 2, 2, 2]) {
                r1.Value = "STG Orders for Shipping";
                r1.Style.Font.Size = 16;
                r1.Style.Font.Bold = true;    
            }

            dt = ReadyDataTableForExport(dt);
            
            
            wsSheet1.Cells["A4"].LoadFromDataTable(dt, true);


            wsSheet1.Protection.IsProtected = false;
            wsSheet1.Protection.AllowSelectLockedCells = false;
            System.IO.Directory.CreateDirectory(@"C:\STG_Excel_Exports");
            excelPkg.SaveAs(new FileInfo(@"C:\STG_Excel_Exports\STG_" + DateTime.Now.ToString("MMddyyyy_HH_mm_ss") + ".xlsx"));
            //excelPkg.SaveAs(new FileInfo(Path.Combine(Environment.CurrentDirectory, @"Excel_Reports\", "STG.xlsx")));

        }

        // Generate Excel for Data Already Sent to STG Warehouse
        public static void GenerateExcelShipped(DataTable dt)
        {
            ExcelPackage excelPkg = new ExcelPackage();
            ExcelWorksheet wsSheet1 = excelPkg.Workbook.Worksheets.Add("sheet1");
            using (ExcelRange r1 = wsSheet1.Cells[2, 2, 2, 2])
            {
                r1.Value = "Orders Already Sent to STG";
                r1.Style.Font.Size = 16;
                r1.Style.Font.Bold = true;
            }

            wsSheet1.Cells["A4"].LoadFromDataTable(dt, true);

            wsSheet1.Protection.IsProtected = false;
            wsSheet1.Protection.AllowSelectLockedCells = false;
            System.IO.Directory.CreateDirectory(@"C:\STG_Excel_Exports");
            excelPkg.SaveAs(new FileInfo(@"C:\STG_Excel_Exports\ShippedData_" + DateTime.Now.ToString("MMddyyyy_HH_mm_ss") + ".xlsx"));
            

        }

        public static DataTable ReadyDataTableForExport(DataTable dt)
        {
            try
            {
                //dt.Columns.Remove("StrWave");

                dt.Columns["StrCustOrdNumber"].ColumnName = "Customer Order Number";
                dt.Columns["StrCustPO"].ColumnName = "Customer PO";
                dt.Columns["StrShipType"].ColumnName = "Ship Type";
                dt.Columns["StrCarrier"].ColumnName = "Carrier / SCAC";
                dt.Columns["StrSmlPkgService"].ColumnName = "Small Package Service";
                dt.Columns["StrShpMethodPayment"].ColumnName = "Ship Method of Payment";
                dt.Columns["StrShpToCode"].ColumnName = "Ship to Code";
                dt.Columns["StrShpToName"].ColumnName = "Ship to Name";
                dt.Columns["StrBillToName"].ColumnName = "Bill to Name";
                dt.Columns["StrBillToAddress1"].ColumnName = "Bill to Address Line 1";
                dt.Columns["StrBillToAddress2"].ColumnName = "Bill to Address Line 2";
                dt.Columns["StrBillToCity"].ColumnName = "Bill to City";
                dt.Columns["StrBillToState"].ColumnName = "Bill to State";
                dt.Columns["StrBillToPostalCode"].ColumnName = "Bill to Postal Code";
                dt.Columns["StrBillToCountry"].ColumnName = "Bill to Country";
                dt.Columns["StrBillToPhone"].ColumnName = "Bill to Phone";
                dt.Columns["StrBillToEmail"].ColumnName = "Bill to Email";
                dt.Columns["StrDC"].ColumnName = "DC";
                dt.Columns["StrDept"].ColumnName = "DEPT #";
                dt.Columns["StrRetailerID"].ColumnName = "CON ID / Retailer ID";
                dt.Columns["StrBBBVendorNumber"].ColumnName = "BBB Vendor Number";
                dt.Columns["StrItemId"].ColumnName = "Item";
                dt.Columns["StrQTYOrdered"].ColumnName = "Quantity Ordered";
                dt.Columns["StrDesription"].ColumnName = "DESCRIPTION";
                dt.Columns["StrUPC"].ColumnName = "UPC";
                dt.Columns["StrWave"].ColumnName = "Wave";

                dt.Columns.Add("Ship to Address Line 1").SetOrdinal(8);
                dt.Columns.Add("Ship to Address Line 2").SetOrdinal(9);
                dt.Columns.Add("Ship to City").SetOrdinal(10);
                dt.Columns.Add("Ship to State").SetOrdinal(11);
                dt.Columns.Add("Ship to Zip").SetOrdinal(12);
                dt.Columns.Add("Ship to Country").SetOrdinal(13);
                dt.Columns.Add("Ship to Phone").SetOrdinal(14);
                dt.Columns.Add("Ship to Email").SetOrdinal(15);

                //dt.Columns.Add("BBB Vendor Number").SetOrdinal(28);
                dt.Columns.Add("3RD PARTY ACCT#").SetOrdinal(29);
                dt.Columns.Add("MARK FOR NAME").SetOrdinal(30);
                dt.Columns.Add("MARK FOR ADDR1").SetOrdinal(31);
                dt.Columns.Add("MARK FOR ADDR2").SetOrdinal(32);
                dt.Columns.Add("MARK FOR CITY").SetOrdinal(33);
                dt.Columns.Add("MARK FOR STATE").SetOrdinal(34);
                dt.Columns.Add("MARK FOR ZIP").SetOrdinal(35);
                dt.Columns.Add("MARK FOR STORE").SetOrdinal(36);
                dt.Columns.Add("MARK FOR COUNTRY").SetOrdinal(37);
                dt.Columns.Add("GIFT MESSAGE").SetOrdinal(38);
                dt.Columns.Add("CUSTOMER REF").SetOrdinal(39);
                dt.Columns.Add("Free Field").SetOrdinal(40);
                dt.Columns.Add("Tax").SetOrdinal(41);

                dt.Columns.Add("IN nbr (Buyer Catalog)").SetOrdinal(46);
                dt.Columns.Add("PLN  (Buyer’s Catalog or Stock Keeping)").SetOrdinal(47);
                dt.Columns.Add("Unit Price").SetOrdinal(48);
                dt.Columns.Add("Line Number").SetOrdinal(49);
                dt.Columns.Add("Customer").SetOrdinal(50);
                dt.Columns.Add("Tarriff/Unified Code").SetOrdinal(51);
                dt.Columns.Add("Currency").SetOrdinal(52);
                dt.Columns.Add("COO").SetOrdinal(53);
                dt.Columns.Add("Export Reason").SetOrdinal(54);
                dt.Columns.Add("Signature").SetOrdinal(55);
                

            } catch (Exception ex)
            {
                TextHelper.WriteLine("Error in ReadyDataTableForExport : " + ex.Message);
                return null;
            }
            
            return dt;
        }

        public static DataTable CSVtoDatatable(String FilePath)
        {
            DataTable dt = new DataTable();
            try
            {
                //string CSVFilePathName = FilePath;
                string[] Lines = File.ReadAllLines(FilePath);
                string[] Fields;
                Fields = Lines[0].Split(new char[] { ',' });
                int Cols = Fields.GetLength(0);
                

                //1st row must be column names; force lower case to ensure matching later on.
                for (int i = 0; i < Cols; i++)
                    dt.Columns.Add(Fields[i].Trim(), typeof(string));
                DataRow Row;
                for (int i = 1; i < Lines.GetLength(0); i++)
                {
                    Fields = Lines[i].Split(new char[] { ',' });
                    Row = dt.NewRow();
                    for (int f = 0; f < Cols; f++)
                        Row[f] = Fields[f];
                    dt.Rows.Add(Row);
                }
                //dataGridClients.DataSource = dt;
            }
            catch (Exception ex)
            {
                TextHelper.WriteLine("Error in CSVtoDatatable");
                throw ex;
            }
            return dt;
        }
    }
}
