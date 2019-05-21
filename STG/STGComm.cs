using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace STG
{
    class STGComm
    {
        public static bool SubmitDatatoSTG(DataTable dtCSV)
        {
            DataTable dt = dtCSV.Copy();
            
            try
            {
                if (!GenerateCSV(dtCSV))
                {
                    return false;
                }
            } catch (Exception ext)
            {
                TextHelper.WriteLine("CSV Generation failed with error : " + ext.Message);
                return false;
            }

            try
            {
                foreach (var row in dt.AsEnumerable())
                {
                    if (!BuildInsertQuery(row))
                        return false;

                }
                TextHelper.WriteLine("Data Submitted to STG SQL, SubmitDatatoSTG");
            }
            catch (Exception ex)
            {
                TextHelper.WriteLine("Data Submit failed with error, SubmitDatatoSTG : " + ex.Message);
            }
            
            return true;
        }

        private static bool GenerateCSV(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            dt = ListToDT.ReadyDataTableForExport(dt);

            try
            {
                if (dt != null)
                {
                    IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                                  Select(column => column.ColumnName);
                    sb.AppendLine(string.Join(",", columnNames));

                    foreach (DataRow row in dt.Rows)
                    {
                        IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                        sb.AppendLine(string.Join(",", fields));
                    }

                    File.WriteAllText(@"C:\STG_Excel_Exports\CSV_" + DateTime.Now.ToString("MMddyyyy_HH_mm_ss") + ".csv", sb.ToString());
                    
                } else
                {
                    TextHelper.WriteLine("Empty Table Return from GenerateCSV");
                    return false;
                }
            } catch (Exception ex)
            {
                TextHelper.WriteLine("CSV generation failed with error : " + ex.Message);
            }
            
            return true;
        }

        private static bool BuildInsertQuery(DataRow dr)
        {
            
            String connetionString = ConfigurationManager.ConnectionStrings["STG"].ConnectionString;
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"INSERT INTO [dbo].[tblItemsShipped]
                                        ([Wave]
                                        ,[CustomerOrderNumber]
                                        ,[CustPO]
                                        ,[ShipToCode]
                                        ,[ShipToName]
                                        ,[BillToName]
                                        ,[DC]
                                        ,[Dept]
                                        ,[RetailerID]
                                        ,[QTYOrdered]
                                        ,[Description]
                                        ,[ItemID]
                                        ,[UPC]
                                        ,[UploadTime])
                            VALUES (@StrWave, @StrCustOrdNumber, @StrCustPO, @StrShpToCode, @StrShpToName, @StrBillToName, @StrDC, @StrDept, @StrRetailerID, @StrQTYOrdered, 
                                    @StrDesription, @StrItemId, @StrUPC, @Uploadtime)");
                          
                           // );
                           
            String query = queryBuilder.ToString();

            //TextHelper.WriteLine(query);

            try
            {
                if (!STGSqlConnection(connetionString, query, dr))
                        return false;
            } catch
            {
                TextHelper.WriteLine("Excetion in STGComm.BuildInsertQuery");
            }

            return true;
        }

        private static bool STGSqlConnection(String connString, String query, DataRow dr)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(connString);
                cmd = new SqlCommand(query, conn);

                
                cmd.Parameters.Add("@StrWave", SqlDbType.Int);
                cmd.Parameters.Add("@StrCustOrdNumber", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@StrCustPO", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@StrShpToCode", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@StrShpToName", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@StrBillToName", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@StrDC", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@StrDept", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@StrRetailerID", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@StrQTYOrdered", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@StrDesription", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@StrItemId", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@StrUPC", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@Uploadtime", SqlDbType.DateTime);


                //cmd.Parameters["@StrWave"].Value = dr["StrWave"];
                cmd.Parameters["@StrWave"].Value = dr.GetValue("StrWave");
                cmd.Parameters["@StrCustOrdNumber"].Value = dr["StrCustOrdNumber"].ToString();
                cmd.Parameters["@StrCustPO"].Value = dr["StrCustPO"].ToString();
                cmd.Parameters["@StrShpToCode"].Value = dr["StrShpToCode"].ToString();
                cmd.Parameters["@StrShpToName"].Value = dr["StrShpToName"].ToString();
                cmd.Parameters["@StrBillToName"].Value = dr["StrBillToName"].ToString();
                cmd.Parameters["@StrDC"].Value = dr["StrDC"].ToString();
                cmd.Parameters["@StrDept"].Value = dr["StrDept"].ToString();
                cmd.Parameters["@StrRetailerID"].Value = dr["StrRetailerID"].ToString();
                cmd.Parameters["@StrQTYOrdered"].Value = dr["StrQTYOrdered"].ToString();
                cmd.Parameters["@StrDesription"].Value = dr["StrDesription"].ToString();
                cmd.Parameters["@StrItemId"].Value = dr["StrItemId"].ToString();
                cmd.Parameters["@StrUPC"].Value = dr["StrUPC"].ToString();
                cmd.Parameters["@Uploadtime"].Value = DateTime.Now;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();


            }
            catch (Exception e)
            {
                TextHelper.Print(e.Message, "Exception in SQL connection, STGComm.STGSqlConnection");
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
                return false;
            }

            return true;
        }

        public static DataTable GetSTGDataFromWave(List<String> ListWave)
        {
            
            StringBuilder queryBuilder = new StringBuilder();

            queryBuilder.Append(@" select * from [View_Item_Shippment_NotConfirmed] ");
            if (ListWave != null)
            {
                queryBuilder.Append(" where Wave in (");
                foreach (String item in ListWave)
                {
                    queryBuilder.Append("'" + item + "',");
                }
                queryBuilder.Length--;
                queryBuilder.Append(")");

            }

            String query = queryBuilder.ToString().TrimEnd(',');

            TextHelper.WriteLine(query);

            return STGGetConnectionGenerator(query);

        }

        public static DataTable STGGetConnectionGenerator(String StrQuery)
        {
            String connetionString = ConfigurationManager.ConnectionStrings["STG"].ConnectionString;

            return STGSqlGetConnection(connetionString, StrQuery);
        }

        private static DataTable STGSqlGetConnection(String connString, String query)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            System.Data.DataTable daGP = new System.Data.DataTable();

            try
            {
                TextHelper.WriteLine(" Connecting to STG Database , STGSqlConnection");
                conn = new SqlConnection(connString);
                cmd = new SqlCommand(query, conn);
                conn.Open();
                TextHelper.WriteLine("Connection Open for STG Database , STGSqlConnection");


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(daGP);
                TextHelper.WriteLine("Query Run Successfull , STGSqlConnection");
                conn.Close();
                TextHelper.WriteLine("Connection Close for STG Database , STGSqlConnection");
                da.Dispose();
            }
            catch (Exception e)
            {
                TextHelper.WriteLine("Exception in SQL connection, STGSqlConnection : Message: " + e.Message);
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }

            return daGP;

        }

        public static bool SubmitShippedDatainSystem(DataTable dt)
        {
            try
            {
                foreach (var row in dt.AsEnumerable())
                {
                    if (!BuildInsertQueryShippedData(row))
                        return false;

                }
                TextHelper.WriteLine("Data Submitted to STG SQL, SubmitShippedDatainSystem");
            }
            catch (Exception ex)
            {
                TextHelper.WriteLine("Data Submit failed with error, SubmitShippedDatainSystem : " + ex.Message);
            }

            return true;
        }

        private static bool BuildInsertQueryShippedData(DataRow dr)
        {

            String connetionString = ConfigurationManager.ConnectionStrings["STG"].ConnectionString;
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"INSERT INTO [dbo].[tblShippingConfirmation]
                                                        ([Wave]
                                                        ,[CustomerOrderNumber]
                                                        ,[CustPO]
                                                        ,[Load]
                                                        ,[Status]
                                                        ,[Mode]
                                                        ,[MVDP]
                                                        ,[Seal]
                                                        ,[Trailer]
                                                        ,[RetailerName]
                                                        ,[BillTo]
                                                        ,[Weight]
                                                        ,[Cube]
                                                        ,[BOL]
                                                        ,[ProNumber]
                                                        ,[Order Entry]
                                                        ,[CancelDate]
                                                        ,[AppDate]
                                                        ,[StatusUpdate]
                                                        ,[OrderID]
                                                        ,[External]
                                                        ,[Carrier]
                                                        ,[Item]
                                                        ,[Ordered]
                                                        ,[Shipped]
                                                        ,[Cartons]
                                                        ,[DataInsertTime])
                                VALUES(@Wave, @CustomerOrderNumber, @CustPO, @Load, @Status, @Mode, @MVDP, @Seal, @Trailer, @RetailerName, @BillTo, @Weight, 
                                        @Cube, @BOL, @ProNumber, @OrderEntry, @CancelDate, @AppDate, @StatusUpdate, @OrderID, @External, 
                                        @Carrier, @Item, @Ordered, @Shipped,@Cartons,@DataInsertTime)");
            

            String query = queryBuilder.ToString();

            TextHelper.WriteLine(query);

            try
            {
                if (!STGSqlConnectionShippedImport(connetionString, query, dr))
                    return false;
            }
            catch
            {
                TextHelper.WriteLine("Excetion in STGComm.BuildInsertQueryShippedData");
            }

            return true;
        }

        private static bool STGSqlConnectionShippedImport(String connString, String query, DataRow dr)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(connString);
                cmd = new SqlCommand(query, conn);
                

                cmd.Parameters.Add("@Wave", SqlDbType.Int);
                cmd.Parameters.Add("@CustomerOrderNumber", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@CustPO", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@Load", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@MVDP", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@Seal", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@Trailer", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@RetailerName", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@BillTo", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@Weight", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@Cube", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@BOL", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@ProNumber", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@OrderEntry", SqlDbType.DateTime);
                cmd.Parameters.Add("@CancelDate", SqlDbType.DateTime);
                cmd.Parameters.Add("@AppDate", SqlDbType.DateTime);
                cmd.Parameters.Add("@StatusUpdate", SqlDbType.DateTime);
                cmd.Parameters.Add("@OrderID", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@External", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@Carrier", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@Item", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@Ordered", SqlDbType.Int);
                cmd.Parameters.Add("@Shipped", SqlDbType.Int);
                cmd.Parameters.Add("@Cartons", SqlDbType.Int);
                cmd.Parameters.Add("@DataInsertTime", SqlDbType.DateTime);


                cmd.Parameters["@Wave"].Value = dr["BHFWave"];
                cmd.Parameters["@CustomerOrderNumber"].Value = dr["Reference"].ToString();
                cmd.Parameters["@CustPO"].Value = dr["PO"].ToString();
                cmd.Parameters["@Load"].Value = dr["Load"].ToString();
                cmd.Parameters["@Status"].Value = dr["Status"].ToString();
                cmd.Parameters["@Mode"].Value = dr["Mode"].ToString();
                cmd.Parameters["@MVDP"].Value = dr["Load Auth/MVDP"].ToString();
                cmd.Parameters["@Seal"].Value = dr["Seal"].ToString();
                cmd.Parameters["@Trailer"].Value = dr["Trailer"].ToString();
                cmd.Parameters["@RetailerName"].Value = dr["Retailer Name"].ToString();
                cmd.Parameters["@BillTo"].Value = dr["Bill To"].ToString();
                cmd.Parameters["@Weight"].Value = dr["Weight"].ToString();
                cmd.Parameters["@Cube"].Value = dr["Cube"].ToString();
                cmd.Parameters["@BOL"].Value = dr["BOL"].ToString();
                cmd.Parameters["@ProNumber"].Value = dr["Pro Number"].ToString();
                cmd.Parameters["@OrderEntry"].Value = (dr["Order Entry"].ToString() == "") ? (object)DBNull.Value : Convert.ToDateTime(dr["Order Entry"].ToString());  //Convert.ToDateTime(dr["Order Entry"].ToString());  
                cmd.Parameters["@CancelDate"].Value = (dr["Cancel Date"].ToString() == "") ? (object)DBNull.Value : Convert.ToDateTime(dr["Cancel Date"].ToString());
                cmd.Parameters["@AppDate"].Value = (dr["App Date"].ToString() == "") ? (object)DBNull.Value : Convert.ToDateTime(dr["App Date"].ToString());
                cmd.Parameters["@StatusUpdate"].Value = (dr["Status Update"].ToString() == "") ? (object)DBNull.Value : Convert.ToDateTime(dr["Status Update"].ToString());
                cmd.Parameters["@OrderID"].Value = dr["OrderID"].ToString();
                cmd.Parameters["@External"].Value = dr["External"].ToString();
                cmd.Parameters["@Carrier"].Value = dr["Carrier"].ToString();
                cmd.Parameters["@Item"].Value = dr["Item"].ToString();
                cmd.Parameters["@Ordered"].Value = ((dr["Ordered"].ToString() == "") ? 0 : int.Parse(dr["Ordered"].ToString()));
                cmd.Parameters["@Shipped"].Value = ((dr["Shipped"].ToString() == "") ? 0 : int.Parse(dr["Shipped"].ToString()));
                cmd.Parameters["@Cartons"].Value = ((dr["Cartons"].ToString() == "") ? 0 : int.Parse(dr["Cartons"].ToString()));
                cmd.Parameters["@DataInsertTime"].Value = DateTime.Now;
                
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();


            }
            catch (Exception e)
            {
                TextHelper.Print(e.Message, "Exception in SQL connection, STGComm.STGSqlConnectionShippedImport");
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
                return false;
            }

            return true;
        }

        public static bool SubmitInboundImport(DataTable dt)
        {
            try
            {
                foreach (var row in dt.AsEnumerable())
                {
                    if (!BuildInsertQueryInboundImport(row))
                        return false;

                }
                TextHelper.WriteLine("Data Submitted to STG SQL, SubmitInboundImport");
            }
            catch (Exception ex)
            {
                TextHelper.WriteLine("Data Submit failed with error, SubmitInboundImport : " + ex.Message);
            }

            return true;
        }

        private static bool BuildInsertQueryInboundImport(DataRow dr)
        {

            String connetionString = ConfigurationManager.ConnectionStrings["STG"].ConnectionString;
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"INSERT INTO [dbo].[tblRecevingInbound]
                                                                  ([Container]
                                                                  ,[Vessel]
                                                                  ,[Seal]
                                                                  ,[ContainerSize]
                                                                  ,[ETALA]
                                                                  ,[CustomerPO]
                                                                  ,[Item]
                                                                  ,[Quantity]
                                                                  ,[UploadTime])
                                VALUES(@Container,@Vessel,@Seal,@ContainerSize,@ETALA,@CustomerPO,@Item,@Quantity,@Uploadtime)");


            String query = queryBuilder.ToString();

            TextHelper.WriteLine(query);

            try
            {
                if (!STGSqlConnectionInboundImport(connetionString, query, dr))
                    return false;
            }
            catch
            {
                TextHelper.WriteLine("Excetion in STGComm.BuildInsertQueryInboundImport");
            }

            return true;
        }

        private static bool STGSqlConnectionInboundImport(String connString, String query, DataRow dr)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(connString);
                cmd = new SqlCommand(query, conn);


                cmd.Parameters.Add("@Container", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@Vessel", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@Seal", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@ContainerSize", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@ETALA", SqlDbType.DateTime);
                cmd.Parameters.Add("@CustomerPO", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@Item", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@Quantity", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@Uploadtime", SqlDbType.DateTime);

                cmd.Parameters["@Container"].Value = dr["CONTAINER #"].ToString().Trim();
                cmd.Parameters["@Vessel"].Value = dr["VESSEL".Trim()].ToString().Trim(); 
                cmd.Parameters["@Seal"].Value = dr["SEAL".Trim()].ToString().Trim();
                cmd.Parameters["@ContainerSize"].Value = dr["CONTAINERSIZE"].ToString().Trim();
                cmd.Parameters["@ETALA"].Value = (dr["ETA LA"].ToString().Trim() == "") ? (object)DBNull.Value : Convert.ToDateTime(dr["ETA LA"].ToString().Trim());
                cmd.Parameters["@CustomerPO"].Value = dr["PO"].ToString().Trim();
                cmd.Parameters["@Item"].Value = dr["SKU"].ToString().Trim();
                cmd.Parameters["@Quantity"].Value = dr["QUANTITY"].ToString().Trim();
                cmd.Parameters["@Uploadtime"].Value = DateTime.Now;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();


            }
            catch (Exception e)
            {
                TextHelper.Print(e.Message, "Exception in SQL connection, STGComm.STGSqlConnectionInboundImport");
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
                return false;
            }

            return true;
        }

        public DataTable GetBBBVendorNumber()
        {

            String query = @" select [Vendor Number], [pk] FROM tblBBBVendorNumberMaster where isDelete = 'N' ";
            
            TextHelper.WriteLine(query);

            return STGGetConnectionGeneratorForBBBVendorNumber(query);

        }

        public DataTable STGGetConnectionGeneratorForBBBVendorNumber(String StrQuery)
        {
            String connetionString = ConfigurationManager.ConnectionStrings["STG"].ConnectionString;

            return GetBBBVendorNumbersForDropdown(connetionString, StrQuery);
        }

        private DataTable GetBBBVendorNumbersForDropdown(String connString, String query)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            System.Data.DataTable daVendorNumber = new System.Data.DataTable();

            try
            {
                TextHelper.WriteLine(" Connecting to STG Database , STGSqlConnection");
                conn = new SqlConnection(connString);
                cmd = new SqlCommand(query, conn);
                conn.Open();
                TextHelper.WriteLine("Connection Open for STG Database , STGSqlConnection");


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(daVendorNumber);
                TextHelper.WriteLine("Query Run Successfull , STGSqlConnection");
                conn.Close();
                TextHelper.WriteLine("Connection Close for STG Database , STGSqlConnection");
                da.Dispose();
            }
            catch (Exception e)
            {
                TextHelper.WriteLine("Exception in SQL connection, STGSqlConnection : Message: " + e.Message);
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }

            return daVendorNumber;
        }

        public bool SubmitVendorNumbercheck(String Item, int VendorNumber)
        {
            try
            {
                if(Item != "" && Item.Trim().Length == 5 && VendorNumber != -1)
                {
                    if (!GenerateQueryBBBVendorNumberCheck(Item, VendorNumber))
                        return false;
                }
                TextHelper.WriteLine("Data Submitted to STG SQL, SubmitVendorNumbercheck");
            }
            catch (Exception ex)
            {
                TextHelper.WriteLine("Data Submit failed with error, SubmitVendorNumbercheck : " + ex.Message);
            }

            return true;
        }

        private bool GenerateQueryBBBVendorNumberCheck(String Item, int VendorNumber)
        {

            String connetionString = ConfigurationManager.ConnectionStrings["STG"].ConnectionString;
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"INSERT INTO [dbo].[tblBBBVendorNumberCheck]
                                                               ([Item]
                                                               ,[Vendor Number]
                                                               ,[IsDelete]
                                                               ,[DataInsertTime]
                                                               ,[DataUpdateTime])
                                        VALUES(@Item,@VendorNumber,@IsDelete,@DataInserttime,@DataUpdateTime)");


            String query = queryBuilder.ToString();

            TextHelper.WriteLine(query);

            try
            {
                if (!InsertBBBVendorNumberCheck(connetionString, query, Item, VendorNumber))
                    return false;
            }
            catch
            {
                TextHelper.WriteLine("Excetion in STGComm.BuildInsertQueryInboundImport");
            }

            return true;
        }

        private bool InsertBBBVendorNumberCheck(String connString, String query, String Item, int VendorNumber)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(connString);
                cmd = new SqlCommand(query, conn);


                cmd.Parameters.Add("@Item", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@VendorNumber", SqlDbType.Int);
                cmd.Parameters.Add("@IsDelete", SqlDbType.NVarChar, 1);
                cmd.Parameters.Add("@DataInserttime", SqlDbType.DateTime);
                cmd.Parameters.Add("@DataUpdateTime", SqlDbType.DateTime);


                cmd.Parameters["@Item"].Value = Item.Trim();
                cmd.Parameters["@VendorNumber"].Value = VendorNumber;
                cmd.Parameters["@IsDelete"].Value = 'N';
                cmd.Parameters["@DataInserttime"].Value = DateTime.Now;
                cmd.Parameters["@DataUpdateTime"].Value = DateTime.Now;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {
                TextHelper.Print(e.Message, "Exception in SQL connection, STGComm.InsertBBBVendorNumberCheck");
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
                return false;
            }

            return true;
        }


        public DataTable GetItemsWithVendorNumber()
        {

            String query = @" select * from View_GetVendorNumberDetails";

            TextHelper.WriteLine(query);

            return STGGetItemsWithVendorNumber(query);

        }

        public DataTable STGGetItemsWithVendorNumber(String StrQuery)
        {
            String connetionString = ConfigurationManager.ConnectionStrings["STG"].ConnectionString;

            return SQLGetItemsWithVendorNumbers(connetionString, StrQuery);
        }

        private DataTable SQLGetItemsWithVendorNumbers(String connString, String query)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            System.Data.DataTable daVendorNumber = new System.Data.DataTable();

            try
            {
                TextHelper.WriteLine(" Connecting to STG Database , STGSqlConnection");
                conn = new SqlConnection(connString);
                cmd = new SqlCommand(query, conn);
                conn.Open();
                TextHelper.WriteLine("Connection Open for STG Database , STGSqlConnection");


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(daVendorNumber);
                TextHelper.WriteLine("Query Run Successfull , STGSqlConnection");
                conn.Close();
                TextHelper.WriteLine("Connection Close for STG Database , STGSqlConnection");
                da.Dispose();
            }
            catch (Exception e)
            {
                TextHelper.WriteLine("Exception in SQL connection, STGSqlConnection : Message: " + e.Message);
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }

            return daVendorNumber;
        }


        public bool UpdateVendorNumbercheck(String Item, int VendorNumber)
        {
            try
            {
                if (Item != "" && Item.Trim().Length == 5 && VendorNumber != -1)
                {
                    if (!GenerateUpdateQueryBBBVendorNumberCheck(Item, VendorNumber))
                        return false;
                }
                TextHelper.WriteLine("Data Submitted to STG SQL, SubmitVendorNumbercheck");
            }
            catch (Exception ex)
            {
                TextHelper.WriteLine("Data Submit failed with error, SubmitVendorNumbercheck : " + ex.Message);
            }

            return true;
        }

        private bool GenerateUpdateQueryBBBVendorNumberCheck(String Item, int VendorNumber)
        {

            String connetionString = ConfigurationManager.ConnectionStrings["STG"].ConnectionString;
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"UPDATE [dbo].[tblBBBVendorNumberCheck] 
                                    SET [Vendor Number] = @VendorNumber
                                        ,[DataUpdateTime] = @DataUpdateTime 
                                    WHERE [Item] = @Item");

            String query = queryBuilder.ToString();

            TextHelper.WriteLine(query);

            try
            {
                if (!UpdateBBBVendorNumberCheck(connetionString, query, Item, VendorNumber))
                    return false;
            }
            catch
            {
                TextHelper.WriteLine("Excetion in STGComm.BuildInsertQueryInboundImport");
            }

            return true;
        }

        private bool UpdateBBBVendorNumberCheck(String connString, String query, String Item, int VendorNumber)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection(connString);
                cmd = new SqlCommand(query, conn);


                cmd.Parameters.Add("@Item", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@VendorNumber", SqlDbType.Int);
                cmd.Parameters.Add("@DataUpdateTime", SqlDbType.DateTime);


                cmd.Parameters["@Item"].Value = Item.Trim();
                cmd.Parameters["@VendorNumber"].Value = VendorNumber;
                cmd.Parameters["@DataUpdateTime"].Value = DateTime.Now;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {
                TextHelper.Print(e.Message, "Exception in SQL connection, STGComm.InsertBBBVendorNumberCheck");
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
                return false;
            }

            return true;
        }

    }
}
