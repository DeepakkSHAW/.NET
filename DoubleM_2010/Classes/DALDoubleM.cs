using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
namespace DoubleM
{
    class DALDoubleM
    {
        private static string _sConnDM = "";
        private static OleDbConnection _oConnDM;
        private System.Windows.Forms.ToolStripStatusLabel _lblMsgDoubleM;
        private System.Windows.Forms.ToolStripProgressBar _pBarDoubleM;


        public DALDoubleM(System.Windows.Forms.ToolStripStatusLabel plbl, System.Windows.Forms.ToolStripProgressBar pPbar)
        {
            _sConnDM =  "File Name = " + CommonDoubleM._DoubleMPath + "\\" + CommonDoubleM.udlDoubleM;
            //_sConnDM =  "File Name = " + Environment.CurrentDirectory + "\\" + CommonDoubleM.udlDoubleM +";Password=MM";
            _oConnDM = new OleDbConnection(_sConnDM);
            _lblMsgDoubleM = plbl;
            _pBarDoubleM = pPbar;
        }

        /* Calculate average of price for the particular stocks*/
        public void MarqueeUpdate()
        {
            //2,5
            OleDbCommand cmdStockName = new OleDbCommand();
            OleDbDataReader drStock = null;

            cmdStockName.Connection = _oConnDM;
            cmdStockName.CommandType = CommandType.StoredProcedure;
            cmdStockName.CommandText = "QryStockLatestValue";

            _pBarDoubleM.Visible = true;
            try
            {
               if (_oConnDM.State != ConnectionState.Open)
                    _oConnDM.Open();

                drStock = cmdStockName.ExecuteReader();

                if (drStock.HasRows)
                {
                    //_pBarDoubleM.Value = 0;
                    while (drStock.Read()) //Reading Stock Names
                    {
                        CommonDoubleM.MarqueeString = CommonDoubleM.MarqueeString + " [" + drStock.GetString(2) + ": " + drStock.GetDecimal(5).ToString() + "] ";
                    }
                }
                else
                    CommonDoubleM.MarqueeString = "Welcome to Market Manager - Double'M'";
            }
            catch (Exception ex)
            {
                _lblMsgDoubleM.Text = ex.Message;
                CommonDoubleM.LogDM(ex.Message);
            }
            finally
            {
                cmdStockName.Dispose();
                _pBarDoubleM.Visible = false;
                if (_oConnDM.State != ConnectionState.Closed)
                    _oConnDM.Close();
            }


        }
        public decimal StockAvg(int iStockID)
        {
            decimal SAvg = 0, dblPrice = 0, sumPrice=0;
            int sumStock = 0, iNo = 0;
            OleDbCommand cmdSharesAvg = new OleDbCommand();
            OleDbDataReader drSAvg;

            cmdSharesAvg.Connection = _oConnDM;
            cmdSharesAvg.CommandType = CommandType.StoredProcedure;
            cmdSharesAvg.CommandText = "QryGetAvg";
            cmdSharesAvg.Parameters.Clear();
            cmdSharesAvg.Parameters.Add("@StockID", OleDbType.Integer).Value = iStockID;
            try
            {
                if (_oConnDM.State != ConnectionState.Open)
                    _oConnDM.Open();

                drSAvg = cmdSharesAvg.ExecuteReader();

                if (drSAvg.HasRows)
                {
                    sumPrice = 0;
                    sumStock = 0;
                    while (drSAvg.Read()) //Reading Stock Names
                    {
                        dblPrice = drSAvg.GetDecimal(1);
                        iNo = drSAvg.GetInt32(2);

                        /*Business logic to calculate the average*/
                        sumStock = sumStock + iNo;
                        if (iNo > 0)
                        {
                            sumPrice = sumPrice + dblPrice * iNo;
                            SAvg = sumPrice / sumStock;
                        }
                        else
                        {
                            sumPrice = SAvg * sumStock;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _lblMsgDoubleM.Text = ex.Message;
                return decimal.Round(SAvg, 2);
            }
            finally
            {
                if (_oConnDM.State != ConnectionState.Closed) _oConnDM.Close();
            }
            return decimal.Round(SAvg, 2);
        }

        public DataTable StockAvgAfterSale()
        {
            DataTable dtAvgAfterSale = null;
            OleDbCommand cmdAvgAfterSale = new OleDbCommand();
            try
            {
                cmdAvgAfterSale.Connection = _oConnDM;
                cmdAvgAfterSale.CommandType = CommandType.StoredProcedure;
                cmdAvgAfterSale.CommandText = "QryAvgAfterSale";

                cmdAvgAfterSale.Parameters.Clear(); //Avg on Current data at 23h
                cmdAvgAfterSale.Parameters.Add("@Date", OleDbType.Date).Value = DateTime.Today.AddHours(23);
                
                OleDbDataAdapter daAvgAfterSale = new OleDbDataAdapter(cmdAvgAfterSale);
                dtAvgAfterSale = new DataTable("AvgAfterSale");

                daAvgAfterSale.Fill(dtAvgAfterSale);
                return dtAvgAfterSale;
            }
            catch (Exception ex)
            {
                _lblMsgDoubleM.Text = ex.Message;
                CommonDoubleM.LogDM(ex.Message);
                return null;
            }
            finally
            {
                if (_oConnDM.State != ConnectionState.Closed)
                    _oConnDM.Close();
            }

        }
        public DataView GraphCatStocks(DateTime vDate)
        {
            _lblMsgDoubleM.Text = "Reading your Total Investment..";
            OleDbCommand cmdGraphCat = new OleDbCommand();

            cmdGraphCat.Connection = _oConnDM;
            cmdGraphCat.CommandType = CommandType.StoredProcedure;
            cmdGraphCat.CommandText = "QryCatSumGraph";
            cmdGraphCat.Parameters.Clear();
            cmdGraphCat.Parameters.Add("@Date", OleDbType.Date).Value = vDate;

            //Date
            try
            {
                OleDbDataAdapter daGraphCat = new OleDbDataAdapter(cmdGraphCat);
                DataSet dsGraphCat = new DataSet();

                daGraphCat.Fill(dsGraphCat, "QryCatSumGraph");//, "sGraphCat");
                DataView dvShare = new DataView(dsGraphCat.Tables[0]);
                //dvShare.Sort
                _lblMsgDoubleM.Text = "Sector wise Stock reading finished";
                return dvShare;
            }
            catch (Exception ex)
            {
                _lblMsgDoubleM.Text = ex.Message;
                return null;
            }
            finally
            {
                cmdGraphCat.Dispose();
                if (_oConnDM.State != ConnectionState.Closed)
                    _oConnDM.Close();
            }
        }
        /* Complete details of all your trading with latest stock value Used in work sheet form */
        public DataView WorkSheet()
        {
            DataTable dtWorkSheet = null;
            _lblMsgDoubleM.Text = "Reading database please wait..";
            OleDbCommand cmdWorkSheet = new OleDbCommand();
            try
            {
                cmdWorkSheet.Connection = _oConnDM;
                cmdWorkSheet.CommandType = CommandType.StoredProcedure;
                cmdWorkSheet.CommandText = "QryAllDetails";
                OleDbDataAdapter daWorkSheet = new OleDbDataAdapter(cmdWorkSheet);
                dtWorkSheet = new DataTable("WorkSheet");

                daWorkSheet.Fill(dtWorkSheet);
                return new DataView(dtWorkSheet);
            }
            catch (Exception ex)
            {
                _lblMsgDoubleM.Text = ex.Message;
                CommonDoubleM.LogDM(ex.Message);
                return null;
            }
            finally
            {
                _lblMsgDoubleM.Text = "Done";
                if (_oConnDM.State != ConnectionState.Closed)
                    _oConnDM.Close();
            }
        }
        /*Trading details for new Sell*/
        public DataView AllTradAccount(short blnNOTActive)
        {
            _lblMsgDoubleM.Text = "Reading Database please wait..";
            OleDbCommand cmdTradeAc = new OleDbCommand();
            DataTable dtTradeAc = null;
            try
            {
            cmdTradeAc.Connection = _oConnDM;
            cmdTradeAc.CommandType = CommandType.StoredProcedure;
            cmdTradeAc.CommandText = "QryAllTradAccount";
            cmdTradeAc.Parameters.Clear();
            cmdTradeAc.Parameters.Add("@Active", OleDbType.SmallInt).Value = blnNOTActive;

                OleDbDataAdapter daTAc = new OleDbDataAdapter(cmdTradeAc);

                dtTradeAc = new DataTable("TradeAc");
                daTAc.Fill(dtTradeAc);
                return new DataView(dtTradeAc);
            }
            catch (Exception ex)
            {
                _lblMsgDoubleM.Text = ex.Message;
                return null;
            }
            finally
            {
                _lblMsgDoubleM.Text = "Done";
                cmdTradeAc.Dispose();
                if (_oConnDM.State != ConnectionState.Closed)
                    _oConnDM.Close();
            }
        }
        //internal DataView GetRateList(int StockID)
        internal DataSet GetRateList(int StockID)
        {
            _lblMsgDoubleM.Text = "Reading Database please wait..";
            OleDbCommand cmdPrice = new OleDbCommand();
            DataTable dtPrice = null;
            DataSet ds = new DataSet("Price");
            try
            {
                cmdPrice.Connection = _oConnDM;
                cmdPrice.CommandType = CommandType.StoredProcedure;
                cmdPrice.CommandText = "QryRateList";
                cmdPrice.Parameters.Clear();
                cmdPrice.Parameters.Add("@SID", OleDbType.Integer).Value = StockID;

                OleDbDataAdapter daRateList = new OleDbDataAdapter(cmdPrice);

                dtPrice = new DataTable("TradeAc");
                daRateList.Fill(ds);
               // return new DataView(dtPrice);
                return ds;
            }
            catch (Exception ex)
            {
                _lblMsgDoubleM.Text = ex.Message;
                return null;
            }
            finally
            {
                _lblMsgDoubleM.Text = "Rate List loaded..";
                cmdPrice.Dispose();
                if (_oConnDM.State != ConnectionState.Closed)
                    _oConnDM.Close();
            }
        }
        public DataView getSectors(ref string rDM, ref string rVM)
        {
            _lblMsgDoubleM.Text = "Reading available sectors";
            OleDbCommand cmdSectorsView = new OleDbCommand();

            cmdSectorsView.Connection = _oConnDM;
            cmdSectorsView.CommandType = CommandType.StoredProcedure;
            cmdSectorsView.CommandText = "QryCatList";

            try
            {
                OleDbDataAdapter daSSectors = new OleDbDataAdapter(cmdSectorsView);
                DataSet dsSView = new DataSet();

                daSSectors.Fill(dsSView, "sSectors");
                DataView dvSectors = new DataView(dsSView.Tables[0]);
                //dvShare.Sort
                _lblMsgDoubleM.Text = "Sectors reading finished";
                //return dsTrView;
                rDM = "Category";
                rVM = "StockCatID";
                return dvSectors;
            }
            catch (Exception ex)
            {
                _lblMsgDoubleM.Text = ex.Message;
                return null;
            }
            finally
            {
                cmdSectorsView.Dispose();
                if (_oConnDM.State != ConnectionState.Closed)
                    _oConnDM.Close();
            }
        }
        public DataView Stocks(ref string rDM, ref string rVM, short blnNOTActive)
        {
            _lblMsgDoubleM.Text = "Reading your active Shares list";
            OleDbCommand cmdSharesView = new OleDbCommand();

            cmdSharesView.Connection = _oConnDM;
            cmdSharesView.CommandType = CommandType.StoredProcedure;
            cmdSharesView.CommandText = "QryActiveStocks";
            cmdSharesView.Parameters.Clear();
            cmdSharesView.Parameters.Add("@Active", OleDbType.SmallInt).Value = blnNOTActive;


            try
            {
                OleDbDataAdapter daSView = new OleDbDataAdapter(cmdSharesView);
                DataSet dsSView = new DataSet();

                daSView.Fill(dsSView, "sViewName");
                DataView dvShare = new DataView(dsSView.Tables[0]);
                //dvShare.Sort
                _lblMsgDoubleM.Text = "Stock reading finished";
                //return dsTrView;
                rDM = "StockName";
                rVM = "StockID";
                return dvShare;
            }
            catch (Exception ex)
            {
                _lblMsgDoubleM.Text = ex.Message;
                return null;
            }
            finally
            {
                cmdSharesView.Dispose();
                if (_oConnDM.State != ConnectionState.Closed)
                    _oConnDM.Close();
            }
        }

        public DataTable GraphSingle(int iStockID)
        {
            DataTable dtSingleGraph = null;

            OleDbCommand cmdSingleGraph = new OleDbCommand();
            try
            {
                cmdSingleGraph.Connection = _oConnDM;
                cmdSingleGraph.CommandType = CommandType.StoredProcedure;
                cmdSingleGraph.CommandText = "QryGraph";
                cmdSingleGraph.Parameters.Clear();
                cmdSingleGraph.Parameters.Add("@StockID", OleDbType.Numeric).Value = iStockID;
                OleDbDataAdapter daSingleGraph = new OleDbDataAdapter(cmdSingleGraph);
                dtSingleGraph = new DataTable("strGraphSingle");
                daSingleGraph.Fill(dtSingleGraph);
                //Console.WriteLine(dvShare.Table.Columns[1].DataType.ToString());
                
                return dtSingleGraph;
            }
            catch (Exception ex)
            {
                _lblMsgDoubleM.Text = ex.Message;
                CommonDoubleM.LogDM(ex.Message);
                return null;
            }
            finally
            {
                if (_oConnDM.State != ConnectionState.Closed)
                    _oConnDM.Close();
                //dtLatestValue.Dispose();
            }
        }
        public DataTable LatestValue()
        {
            DataTable dtLatestValue = null;
            OleDbCommand cmdLatestView = new OleDbCommand();
            try
            {
                cmdLatestView.Connection = _oConnDM;
                cmdLatestView.CommandType = CommandType.StoredProcedure;
                cmdLatestView.CommandText = "QryCurrentStatus";
                OleDbDataAdapter daLatestView = new OleDbDataAdapter(cmdLatestView);
                dtLatestValue = new DataTable("strSheetName");

                //daLatestView.FillSchema(dtLatestValue, SchemaType.Source);
                daLatestView.Fill(dtLatestValue);

//                daLatestView.Fill(dtLatestValue);
                return dtLatestValue;
            }
            catch (Exception ex)
            {
                _lblMsgDoubleM.Text = ex.Message;
                CommonDoubleM.LogDM(ex.Message);
                return null;
            }
            finally
            {
                if (_oConnDM.State != ConnectionState.Closed)
                    _oConnDM.Close();
                //dtLatestValue.Dispose();
            }

        }
        public DataTable TradingValue()
        {
            DataTable dtLatestValue = null;
            OleDbCommand cmdLatestView = new OleDbCommand();
            try
            {
                cmdLatestView.Connection = _oConnDM;
                cmdLatestView.CommandType = CommandType.StoredProcedure;
                cmdLatestView.CommandText = "QryTradView";
                OleDbDataAdapter daLatestView = new OleDbDataAdapter(cmdLatestView);
                dtLatestValue = new DataTable("TradingView");

                //daLatestView.FillSchema(dtLatestValue, SchemaType.Source);
                daLatestView.Fill(dtLatestValue);

                //                daLatestView.Fill(dtLatestValue);
                return dtLatestValue;
            }
            catch (Exception ex)
            {
                _lblMsgDoubleM.Text = ex.Message;
                CommonDoubleM.LogDM(ex.Message);
                return null;
            }
            finally
            {
                if (_oConnDM.State != ConnectionState.Closed)
                    _oConnDM.Close();
                //dtLatestValue.Dispose();
            }

        }
        public DataTable TradingValue(string sSQL)
        {
            DataTable dtLatestValue = null;
            OleDbCommand cmdLatestView = new OleDbCommand();
            try
            {
                cmdLatestView.Connection = _oConnDM;
                cmdLatestView.CommandType = CommandType.Text;
                cmdLatestView.CommandText = sSQL;
                OleDbDataAdapter daLatestView = new OleDbDataAdapter(cmdLatestView);
                dtLatestValue = new DataTable("TradingView");

                //daLatestView.FillSchema(dtLatestValue, SchemaType.Source);
                daLatestView.Fill(dtLatestValue);

                //                daLatestView.Fill(dtLatestValue);
                return dtLatestValue;
            }
            catch (Exception ex)
            {
                _lblMsgDoubleM.Text = ex.Message;
                CommonDoubleM.LogDM(ex.Message);
                return null;
            }
            finally
            {
                if (_oConnDM.State != ConnectionState.Closed)
                    _oConnDM.Close();
                //dtLatestValue.Dispose();
            }
        }
        public bool UpdateStockprice(bool IsBatch)
        {
            string sSName = "";
            string sUpdates = "";
            string []strOnlinevalue;

            OleDbCommand cmdStockName = new OleDbCommand();
            OleDbCommand cmdStockRate = new OleDbCommand();
            OleDbDataReader drStock = null;

            cmdStockRate.Connection = _oConnDM;
            cmdStockRate.CommandType = CommandType.Text;

            cmdStockName.Connection = _oConnDM;
            cmdStockName.CommandType = CommandType.StoredProcedure;
            cmdStockName.CommandText = "QryActiveStocks";
            cmdStockName.Parameters.Clear();
            cmdStockName.Parameters.Add("@Active", OleDbType.Boolean).Value = false;

            if (!IsBatch) _pBarDoubleM.Visible = true;

            try
            {
                if (_oConnDM.State != ConnectionState.Open)
                    _oConnDM.Open();

                drStock = cmdStockName.ExecuteReader();

                if (drStock.HasRows)
                {
                    if (!IsBatch) _pBarDoubleM.Value = 0;
                    while (drStock.Read()) //Reading Stock Names
                    {
                        if (!IsBatch) _pBarDoubleM.Maximum = drStock.GetInt32(10) + 1;
                        sUpdates = "";
                        sSName = drStock.GetString(4); //4 - YFCode
                        if (CommonDoubleM.YahooStockLatestUpdates(sSName, ref sUpdates))
                        {
                            //Add latest Rates
                            try
                            {
                                string sqlUpdateRate = "INSERT into [TRates] (StockID, PRICE, Ondate) VALUES (@StockID, @price, #@Ondate#)";
                                strOnlinevalue = CommonDoubleM.FilterOnlineValues(sUpdates);
                                if (!IsBatch) _lblMsgDoubleM.Text = "Latest value: " + sSName + " [" + strOnlinevalue[1] + "]";
                                System.Windows.Forms.Application.DoEvents();

                                if (Convert.ToDouble(strOnlinevalue[1]) > 0)
                                {
                                    sqlUpdateRate = sqlUpdateRate.Replace("@StockID", drStock.GetInt32(0).ToString());
                                    sqlUpdateRate = sqlUpdateRate.Replace("@price", strOnlinevalue[1]);
                                    //sqlUpdateRate = sqlUpdateRate.Replace("@Ondate", strOnlinevalue[2].Trim('"', '/') + " " + strOnlinevalue[3].Trim('"', '/'));
                                    sqlUpdateRate = sqlUpdateRate.Replace("@Ondate", DateTime.Now.ToString("dd/MMMM/yyyy HH:mm:ss"));
                                    cmdStockRate.CommandText = sqlUpdateRate;
                                    cmdStockRate.ExecuteNonQuery();
                                }
                            }
                            catch(Exception ex1) 
                            {
                                if (!IsBatch) _lblMsgDoubleM.Text = ex1.Message + " Unable to update latest Rate of :" + sSName;
                                CommonDoubleM.LogDM("Unable to update latest Rate of :" + sSName);
                            }
                            if (!IsBatch) _pBarDoubleM.Value = _pBarDoubleM.Value + 1;  
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                if (!IsBatch) _lblMsgDoubleM.Text = ex.Message;
                CommonDoubleM.LogDM(ex.Message);
                return false;

            }
            finally
            {
                cmdStockName.Dispose();
                cmdStockRate.Dispose();
                drStock.Close();
                drStock.Dispose();
                if (!IsBatch) _pBarDoubleM.Visible = false;
                if (_oConnDM.State != ConnectionState.Closed)
                    _oConnDM.Close();
            }
        }
        public bool BulkUpdateStockprice(DataTable dtBulkData)
        {
            if (dtBulkData == null) return false;

            OleDbDataAdapter adBulkRate = new OleDbDataAdapter();
            adBulkRate = new OleDbDataAdapter("Select * from Rates", _oConnDM);
            
            adBulkRate.InsertCommand = new OleDbCommand();
            adBulkRate.InsertCommand.CommandType = CommandType.StoredProcedure;
            adBulkRate.InsertCommand.CommandText = "QryInsRates";
            adBulkRate.InsertCommand.Connection = _oConnDM;

            try
            {

                adBulkRate.InsertCommand.Parameters.Add("@StockID", OleDbType.Numeric, 8, dtBulkData.Columns[0].ColumnName);
                adBulkRate.InsertCommand.Parameters.Add("@Price", OleDbType.Currency, 32, dtBulkData.Columns[1].ColumnName);
                adBulkRate.InsertCommand.Parameters.Add("@Ondate", OleDbType.Date, 20, dtBulkData.Columns[2].ColumnName);

                adBulkRate.InsertCommand.Connection.Open();
                adBulkRate.Update(dtBulkData);
                adBulkRate.InsertCommand.Connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                _lblMsgDoubleM.Text = ex.Message;
                CommonDoubleM.LogDM(ex.Message);
                return false; 
            }
            finally
            { adBulkRate.Dispose(); }

            
        }
        public int NewTrad(int iStock, double dblRate, DateTime dtTradOn, int iQuant, double dblBrok, double dblTax, string sTradeNotes, DataTable dtAc)
        {
            int iResult = 0;
            int iLatestRateId, iLatestTradeId;
            if (_oConnDM.State != ConnectionState.Open) _oConnDM.Open();
            OleDbCommand cmdTrading = _oConnDM.CreateCommand();
            OleDbTransaction txnTrading;
            txnTrading = _oConnDM.BeginTransaction(); // Start Local Transaction

            cmdTrading.Connection = _oConnDM;
            cmdTrading.Transaction = txnTrading;
            try
            {
                /* Insert to TRates Table */
                cmdTrading.CommandType = CommandType.StoredProcedure;
                cmdTrading.CommandText = "QryInsRates";
                cmdTrading.Parameters.Clear();
                cmdTrading.Parameters.Add("@StockID", OleDbType.Numeric).Value = iStock;
                cmdTrading.Parameters.Add("@Price", OleDbType.Currency).Value = dblRate;
                cmdTrading.Parameters.Add("@Ondate", OleDbType.Date).Value = dtTradOn;

                iResult = cmdTrading.ExecuteNonQuery();

                /*Getting the latest PK from TRates Table */
                //Another way to get the auto generated id
                cmdTrading.CommandType = CommandType.StoredProcedure;
                cmdTrading.CommandText = "QryGetAutoID";
                cmdTrading.Parameters.Clear();

                iLatestRateId = (int)cmdTrading.ExecuteScalar();

                /* Insert to TTrade Table */
                cmdTrading.CommandType = CommandType.StoredProcedure;
                cmdTrading.CommandText = "QryInsTrad";
                cmdTrading.Parameters.Clear();

                cmdTrading.Parameters.Add("@RateID", OleDbType.Integer).Value = iLatestRateId;
                cmdTrading.Parameters.Add("@Quantity", OleDbType.Integer).Value = iQuant;
                cmdTrading.Parameters.Add("@Brokerage", OleDbType.Currency).Value = dblBrok;
                cmdTrading.Parameters.Add("@Tax", OleDbType.Currency).Value = dblTax;
                cmdTrading.Parameters.Add("@TradeOn", OleDbType.Date).Value = dtTradOn;
                //cmdTrading.Parameters.Add("@TranDate", OleDbType.Date).Value = vTranDate;
                iResult = cmdTrading.ExecuteNonQuery();

                 /*Best way to get latest auto generated id*/
                 cmdTrading.CommandType = CommandType.StoredProcedure;
                 cmdTrading.CommandText = "QryGetAutoID";
                 cmdTrading.Parameters.Clear();
                 iLatestTradeId = (int)cmdTrading.ExecuteScalar();

                 /*Inserting into TTradeNotes Table*/
                if (sTradeNotes.Trim().Length > 0)
                {
                    cmdTrading.CommandType = CommandType.StoredProcedure;
                    cmdTrading.CommandText = "QryInsTradNote";
                    cmdTrading.Parameters.Clear();

                    /* Insert to TTradeNotes Table */
                    cmdTrading.Parameters.Add("@TradeID", OleDbType.Integer).Value = iLatestTradeId;
                    cmdTrading.Parameters.Add("@TradeNote", OleDbType.VarChar).Value = sTradeNotes;
                    iResult = cmdTrading.ExecuteNonQuery();
                }
                /*Inserting into TAccounts Table*/
                //Means if its a sell, we need to update the Accounts Table as well
                //To make sure the current sell associates with which buy. In order to 
                //Calculate Profit/Loss of that particular script
                if (dtAc != null) 
                {
                    foreach (DataRow dr in dtAc.Rows)
                    {
                        cmdTrading.CommandType = CommandType.StoredProcedure;
                        cmdTrading.CommandText = "QryInsAccounts";
                        cmdTrading.Parameters.Clear();

                        cmdTrading.Parameters.Add("@BuyID", OleDbType.Integer).Value = dr[0];
                        cmdTrading.Parameters.Add("@SellID", OleDbType.Integer).Value = iLatestTradeId;
                        cmdTrading.Parameters.Add("@Quantity", OleDbType.Integer).Value = dr[1];
                        cmdTrading.Parameters.Add("@OnDate", OleDbType.Date).Value = dtTradOn;
                        iResult = cmdTrading.ExecuteNonQuery();
                    }
                }
                txnTrading.Commit();
                _lblMsgDoubleM.Text = "New Transaction Added Successfully!";
            }
            catch (Exception e)
            {
                try
                {
                    txnTrading.Rollback();
                }
                catch (OleDbException ex)
                {
                    if (txnTrading.Connection != null)
                    {
                        Console.WriteLine("An exception of type " + ex.GetType() +
                            " was encountered while attempting to roll back the transaction.");
                    }
                }
                iResult = -1;
                _lblMsgDoubleM.Text = "An exception of type " + e.GetType() +
                    " was encountered while inserting the data.";
                Console.WriteLine("Neither record was written to database.");

            }
            finally
            {
                if (_oConnDM.State != ConnectionState.Closed) _oConnDM.Close();
            }

            return iResult;
        }
        internal int NewRate(int iStock, double dblRate, DateTime dtTradOn)
        {
            int iResult = -1;
            int iLatestRateId;
            if (_oConnDM.State != ConnectionState.Open) _oConnDM.Open();
            OleDbCommand cmdRates = _oConnDM.CreateCommand();
            cmdRates.Connection = _oConnDM;
            try
            {
                /* Insert to TRates Table */
                cmdRates.CommandType = CommandType.StoredProcedure;
                cmdRates.CommandText = "QryInsRates";
                cmdRates.Parameters.Clear();
                cmdRates.Parameters.Add("@StockID", OleDbType.Numeric).Value = iStock;
                cmdRates.Parameters.Add("@Price", OleDbType.Currency).Value = dblRate;
                cmdRates.Parameters.Add("@Ondate", OleDbType.Date).Value = dtTradOn;

                iResult = cmdRates.ExecuteNonQuery();

                /*Getting the latest PK from TRates Table */
                //Another way to get the auto generated id
                cmdRates.CommandType = CommandType.StoredProcedure;
                cmdRates.CommandText = "QryGetAutoID";
                cmdRates.Parameters.Clear();

                iLatestRateId = (int)cmdRates.ExecuteScalar();
                iResult = iLatestRateId; 
            }
            catch (Exception ex)
            {
                iResult = -1;
                _lblMsgDoubleM.Text = "An exception of type " + ex.GetType() +
                    " was encountered while inserting the data.";
                CommonDoubleM.LogDM("NewRate for StockID "+iStock.ToString()+ " Not inserted");
            }
            finally
            {
                if (_oConnDM.State != ConnectionState.Closed) _oConnDM.Close();
            }
            return iResult;
        }
        internal int UpdateRate(long iRate, double dblRate, DateTime dtTradOn)
        {
            int iResult = -1;

            if (_oConnDM.State != ConnectionState.Open) _oConnDM.Open();
            OleDbCommand cmdRates = _oConnDM.CreateCommand();
            cmdRates.Connection = _oConnDM;
            try
            {
                /* Update to TRates Table */
                cmdRates.CommandType = CommandType.StoredProcedure;
                cmdRates.CommandText = "QryUpdateRates";
                cmdRates.Parameters.Clear(); //Assign parameters should be in order
                cmdRates.Parameters.Add("@Price", OleDbType.Currency).Value = dblRate;
                cmdRates.Parameters.Add("@Date", OleDbType.Date).Value = dtTradOn;
                cmdRates.Parameters.Add("@RID", OleDbType.Numeric).Value = iRate;
                iResult = cmdRates.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                iResult = -1;
                _lblMsgDoubleM.Text = "An exception of type " + ex.Message +
                    " was encountered while inserting the data.";
                CommonDoubleM.LogDM("Rate has been not update for RateID " + iRate.ToString());
            }
            finally
            {
                if (_oConnDM.State != ConnectionState.Closed) _oConnDM.Close();
            }
            return iResult;
        }
        internal int DeleteRate(long iRate)
        {
            int iResult = -1;

            if (_oConnDM.State != ConnectionState.Open) _oConnDM.Open();
            OleDbCommand cmdRates = _oConnDM.CreateCommand();
            cmdRates.Connection = _oConnDM;
            try
            {
                /* Update to TRates Table */
                cmdRates.CommandType = CommandType.StoredProcedure;
                cmdRates.CommandText = "QryDeleteRates";
                cmdRates.Parameters.Clear(); //Assign parameters should be in order
                cmdRates.Parameters.Add("@RID", OleDbType.Numeric).Value = iRate;
                iResult = cmdRates.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                iResult = -1;
                _lblMsgDoubleM.Text = "An exception of type " + ex.GetType() +
                    " was encountered while inserting the data.";
                CommonDoubleM.LogDM("Rate deletion wasn't executed successfully RateID " + iRate.ToString());
            }
            finally
            {
                if (_oConnDM.State != ConnectionState.Closed) _oConnDM.Close();
            }
            return iResult;
        }
        internal int getRates_Count(int SID)
        {
            int iResult = -1;

            if (_oConnDM.State != ConnectionState.Open) _oConnDM.Open();
            OleDbCommand cmdStocks = _oConnDM.CreateCommand();
            cmdStocks.Connection = _oConnDM;
            try
            {
                /* TRates Table - rate count*/
                cmdStocks.CommandType = CommandType.StoredProcedure;
                cmdStocks.CommandText = "QryRatesCount";
                cmdStocks.Parameters.Clear(); //Assign parameters should be in order

                cmdStocks.Parameters.Add("@SID", OleDbType.Integer).Value = SID;
                iResult = (int)cmdStocks.ExecuteScalar();
            }
            catch (Exception ex)
            {
                iResult = -1;
                _lblMsgDoubleM.Text = "An exception of type " + ex.Message +
                    " was encountered while counting rates.";
                CommonDoubleM.LogDM("Stock rates count not completed for StockID " + SID.ToString());
            }
            finally
            {
                if (_oConnDM.State != ConnectionState.Closed) _oConnDM.Close();
            }
            return iResult;
        }
        internal int NewStock(int StockCatID, string StockName, string ShortName, string YFCode, string RCode, string BSECode, string HDFCCode, bool Active)
        {
            int iResult = -1;
            int iLatestStockId;
            if (_oConnDM.State != ConnectionState.Open) _oConnDM.Open();
            OleDbCommand cmdStocks = _oConnDM.CreateCommand();
            cmdStocks.Connection = _oConnDM;
            try
            {
                /* Update to TRates Table */
                cmdStocks.CommandType = CommandType.StoredProcedure;
                cmdStocks.CommandText = "QryInsStocks";
                cmdStocks.Parameters.Clear(); //Assign parameters should be in order

                cmdStocks.Parameters.Add("@StockCatID", OleDbType.Integer).Value = StockCatID;
                cmdStocks.Parameters.Add("@StockName", OleDbType.VarChar).Value = StockName;
                cmdStocks.Parameters.Add("@ShortName", OleDbType.VarChar).Value = ShortName;
                cmdStocks.Parameters.Add("@YFCode", OleDbType.VarChar).Value = YFCode;
                cmdStocks.Parameters.Add("@RCode", OleDbType.VarChar).Value = RCode;
                cmdStocks.Parameters.Add("@BSECode", OleDbType.VarChar).Value = BSECode;
                cmdStocks.Parameters.Add("@HDFCCode", OleDbType.VarChar).Value = HDFCCode;
                cmdStocks.Parameters.Add("@Active", OleDbType.Boolean).Value = Active;

                iResult = cmdStocks.ExecuteNonQuery();

                /*Getting the latest PK from TRates Table */
                //Another way to get the auto generated id
                cmdStocks.CommandType = CommandType.StoredProcedure;
                cmdStocks.CommandText = "QryGetAutoID";
                cmdStocks.Parameters.Clear();

                iLatestStockId = (int)cmdStocks.ExecuteScalar();
                iResult = iLatestStockId; 
            }
            catch (Exception ex)
            {
                iResult = -1;
                _lblMsgDoubleM.Text = "An exception of type " + ex.Message +
                    " was encountered while inserting the data.";
                CommonDoubleM.LogDM("New stock [" + StockName + "] has not been added.");
            }
            finally
            {
                if (_oConnDM.State != ConnectionState.Closed) _oConnDM.Close();
            }
            return iResult;
        }
        internal int UpdateStock(int SID, int StockCatID, string StockName, string ShortName, string YFCode, string RCode, string BSECode, string HDFCCode, bool Active)
        {
            int iResult = -1;

            if (_oConnDM.State != ConnectionState.Open) _oConnDM.Open();
            OleDbCommand cmdStocks = _oConnDM.CreateCommand();
            cmdStocks.Connection = _oConnDM;
            try
            {
                /* Update to TRates Table */
                cmdStocks.CommandType = CommandType.StoredProcedure;
                cmdStocks.CommandText = "QryUpdateStocks";
                cmdStocks.Parameters.Clear(); //Assign parameters should be in order

                cmdStocks.Parameters.Add("@StockCatID", OleDbType.Integer).Value = StockCatID;
                cmdStocks.Parameters.Add("@StockName", OleDbType.VarChar).Value = StockName;
                cmdStocks.Parameters.Add("@ShortName", OleDbType.VarChar).Value = ShortName;
                cmdStocks.Parameters.Add("@YFCode", OleDbType.VarChar).Value = YFCode;
                cmdStocks.Parameters.Add("@RCode", OleDbType.VarChar).Value = RCode;
                cmdStocks.Parameters.Add("@BSECode", OleDbType.VarChar).Value = BSECode;
                cmdStocks.Parameters.Add("@HDFCCode", OleDbType.VarChar).Value = HDFCCode;
                cmdStocks.Parameters.Add("@Active", OleDbType.Boolean).Value = Active;
                cmdStocks.Parameters.Add("@SID", OleDbType.Integer).Value = SID;

                iResult = cmdStocks.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                iResult = -1;
                _lblMsgDoubleM.Text = "An exception of type " + ex.Message +
                    " was encountered while updating the data.";
                CommonDoubleM.LogDM("Stock has been not update for StockID " + SID.ToString());
            }
            finally
            {
                if (_oConnDM.State != ConnectionState.Closed) _oConnDM.Close();
            }
            return iResult;
        }
        internal int DeleteStock(int SID)
        {
            int iResult = -1;

            if (_oConnDM.State != ConnectionState.Open) _oConnDM.Open();
            OleDbCommand cmdStocks = _oConnDM.CreateCommand();
            cmdStocks.Connection = _oConnDM;
            try
            {
                /* Delete to TRates Table */
                cmdStocks.CommandType = CommandType.StoredProcedure;
                cmdStocks.CommandText = "QryDeleteStocks";
                cmdStocks.Parameters.Clear(); //Assign parameters should be in order

                cmdStocks.Parameters.Add("@SID", OleDbType.Integer).Value = SID;
                iResult = cmdStocks.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                iResult = -1;
                _lblMsgDoubleM.Text = "An exception of type " + ex.Message +
                    " was encountered while Deleting the data.";
                CommonDoubleM.LogDM("Stock has been not deleted for StockID " + SID.ToString());
            }
            finally
            {
                if (_oConnDM.State != ConnectionState.Closed) _oConnDM.Close();
            }
            return iResult;
        }
        internal int NewSector(string sSector)
        {
            int iResult = -1;

            if (_oConnDM.State != ConnectionState.Open) _oConnDM.Open();
            OleDbCommand cmdSector = _oConnDM.CreateCommand();
            
            cmdSector.Connection = _oConnDM;
            try
            {
                /* Add to TStockCat Table */
                cmdSector.CommandType = CommandType.StoredProcedure;
                cmdSector.CommandText = "QryInsCats";
                cmdSector.Parameters.Clear(); //Assign parameters should be in order

                cmdSector.Parameters.Add("@Category", OleDbType.VarChar).Value = sSector;

                iResult = cmdSector.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                iResult = -1;
                _lblMsgDoubleM.Text = "An exception of type " + ex.Message +
                    " was encountered while inserting into table TStockCat.";
                CommonDoubleM.LogDM("New Sector [" + sSector + "] has not been added.");
            }
            finally
            {
                if (_oConnDM.State != ConnectionState.Closed) _oConnDM.Close();
            }
            return iResult;

        }
        internal int UpdateSector(string sSector, int iSectorID)
        {
            int iResult = -1;

            if (_oConnDM.State != ConnectionState.Open) _oConnDM.Open();
            OleDbCommand cmdSector = _oConnDM.CreateCommand();
            cmdSector.Connection = _oConnDM;
            try
            {
                /* Update to TStockCat Table */
                cmdSector.CommandType = CommandType.StoredProcedure;
                cmdSector.CommandText = "QryUpdateCats";
                cmdSector.Parameters.Clear(); //Assign parameters should be in order

                cmdSector.Parameters.Add("@Category", OleDbType.VarChar).Value = sSector;
                cmdSector.Parameters.Add("@SectorID", OleDbType.Integer).Value = iSectorID;

                iResult = cmdSector.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                iResult = -1;
                _lblMsgDoubleM.Text = "An exception of type " + ex.Message +
                    " was encountered while updating Table TStockCat.";
                CommonDoubleM.LogDM("Sector has been not update for CatID " + iSectorID.ToString());
            }
            finally
            {
                if (_oConnDM.State != ConnectionState.Closed) _oConnDM.Close();
            }
            return iResult;
        }
        internal int DeleteSector(int iSectorID)
        {
            int iResult = -1;

            if (_oConnDM.State != ConnectionState.Open) _oConnDM.Open();
            OleDbCommand cmdSector = _oConnDM.CreateCommand();
            cmdSector.Connection = _oConnDM;
            try
            {
                /* Update to TStockCat Table */
                cmdSector.CommandType = CommandType.StoredProcedure;
                cmdSector.CommandText = "QryDeleteCats";
                cmdSector.Parameters.Clear(); //Assign parameters should be in order

                cmdSector.Parameters.Add("@SectorID", OleDbType.Integer).Value = iSectorID;

                iResult = cmdSector.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                iResult = -1;
                _lblMsgDoubleM.Text = "An exception of type " + ex.Message +
                    " was encountered while Delete sector from Table TStockCat.";
                CommonDoubleM.LogDM("Sector has been not deleted for CatID " + iSectorID.ToString());
            }
            finally
            {
                if (_oConnDM.State != ConnectionState.Closed) _oConnDM.Close();
            }
            return iResult;
        }
        internal DataView Accouting_ProfitLoss(DateTime inParam)
        {
            _lblMsgDoubleM.Text = "Please wait..data loading";
            OleDbCommand cmdAcPL = new OleDbCommand();
            DataTable dtAcPL = null;
            try
            {
                cmdAcPL.Connection = _oConnDM;
                cmdAcPL.CommandType = CommandType.StoredProcedure;
                cmdAcPL.CommandText = "QryAccounts";
                cmdAcPL.Parameters.Clear();
                cmdAcPL.Parameters.Add("@onDate", OleDbType.Date).Value = inParam;

                OleDbDataAdapter daTAc = new OleDbDataAdapter(cmdAcPL);

                dtAcPL = new DataTable("TradeAc");
                daTAc.Fill(dtAcPL);
                return new DataView(dtAcPL);
            }
            catch (Exception ex)
            {
                _lblMsgDoubleM.Text = ex.Message;
                return null;
            }
            finally
            {
                _lblMsgDoubleM.Text = "Done";
                cmdAcPL.Dispose();
                if (_oConnDM.State != ConnectionState.Closed)
                    _oConnDM.Close();
            }

        }
        /*Methode retrives All stocks (Active/Inactive) with Avialeble quantity 
         * with Average price*/
        internal DataView Stocks_AvailAvg_BSE(DateTime LastDate, short NOTActive)
        {
            _lblMsgDoubleM.Text = "Please wait..data loading";
            OleDbCommand cmdBSE = new OleDbCommand();
            DataTable dtBSE = null;
            try
            {
                cmdBSE.Connection = _oConnDM;
                cmdBSE.CommandType = CommandType.StoredProcedure;
                cmdBSE.CommandText = "QryAvgAvail_BSE";
                cmdBSE.Parameters.Clear();
                cmdBSE.Parameters.Add("@onDate", OleDbType.Date).Value = LastDate;
                cmdBSE.Parameters.Add("@Active", OleDbType.SmallInt).Value = NOTActive;

                OleDbDataAdapter daBSE = new OleDbDataAdapter(cmdBSE);

                dtBSE = new DataTable("TradeAc");
                daBSE.Fill(dtBSE);
                return new DataView(dtBSE);
            }
            catch (Exception ex)
            {
                _lblMsgDoubleM.Text = ex.Message;
                return null;
            }
            finally
            {
                _lblMsgDoubleM.Text = "Execution QryAvgAvail_BSE completed";
                cmdBSE.Dispose();
                if (_oConnDM.State != ConnectionState.Closed)
                    _oConnDM.Close();
            }

        }
        internal DataView Stocks_AvailAvg_YFRM(DateTime LastDate, short NOTActive)
        {
            _lblMsgDoubleM.Text = "Please wait..data loading";
            OleDbCommand cmdBSE = new OleDbCommand();
            DataTable dtBSE = null;
            try
            {
                cmdBSE.Connection = _oConnDM;
                cmdBSE.CommandType = CommandType.StoredProcedure;
                cmdBSE.CommandText = "QryAvgAvail_YFMR";
                cmdBSE.Parameters.Clear();
                cmdBSE.Parameters.Add("@onDate", OleDbType.Date).Value = LastDate;
                cmdBSE.Parameters.Add("@Active", OleDbType.SmallInt).Value = NOTActive;

                OleDbDataAdapter daBSE = new OleDbDataAdapter(cmdBSE);

                dtBSE = new DataTable("StockList");
                daBSE.Fill(dtBSE);
                return new DataView(dtBSE);
            }
            catch (Exception ex)
            {
                _lblMsgDoubleM.Text = ex.Message;
                return null;
            }
            finally
            {
                _lblMsgDoubleM.Text = "Execution: QryAvgAvail_YFMR completed";
                cmdBSE.Dispose();
                if (_oConnDM.State != ConnectionState.Closed)
                    _oConnDM.Close();
            }

        }

    }
}
