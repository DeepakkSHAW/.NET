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
            _sConnDM =  "File Name = " + Environment.CurrentDirectory + "\\" + CommonDoubleM.udlDoubleM;
            //_sConnDM =  "File Name = " + Environment.CurrentDirectory + "\\" + CommonDoubleM.udlDoubleM +";Password=MM";
            _oConnDM = new OleDbConnection(_sConnDM);
            _lblMsgDoubleM = plbl;
            _pBarDoubleM = pPbar;
        }
        #region Private Methods

        private string[] FilterOnlineValues(string sInterNetUpdates)
        {
            try
            {
                string[] sFilter = sInterNetUpdates.Split(',');
                Console.WriteLine(sFilter[2]);
                return sFilter;
            }
            catch (Exception ex)
            { 
                _lblMsgDoubleM.Text = ex.Message;
                return null;
            }
        }
        
        #endregion


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
        public bool UpdateStockprice()
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

            _pBarDoubleM.Visible = true;
            try
            {
                if (_oConnDM.State != ConnectionState.Open)
                    _oConnDM.Open();

                drStock = cmdStockName.ExecuteReader();

                if (drStock.HasRows)
                {
                    _pBarDoubleM.Value = 0;
                    while (drStock.Read()) //Reading Stock Names
                    {
                        _pBarDoubleM.Maximum = drStock.GetInt32(9) + 1;
                        sUpdates = "";
                        sSName = drStock.GetString(4); //4 - YFCode
                        if (CommonDoubleM.YahooStockLatestUpdates(sSName, ref sUpdates))
                        {
                            //Add latest Rates
                            try
                            {
                                string sqlUpdateRate = "INSERT into [TRates] (StockID, PRICE, Ondate) VALUES (@StockID, @price, #@Ondate#)";
                                strOnlinevalue = FilterOnlineValues(sUpdates);
                                _lblMsgDoubleM.Text = "Latest value: " + sSName + " [" + strOnlinevalue[1] + "]";
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
                                _lblMsgDoubleM.Text = ex1.Message + " Unable to update latest Rate of :" + sSName;
                                CommonDoubleM.LogDM("Unable to update latest Rate of :" + sSName);
                            }
                            _pBarDoubleM.Value = _pBarDoubleM.Value+1;  
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                _lblMsgDoubleM.Text = ex.Message;
                CommonDoubleM.LogDM(ex.Message);
                return false;

            }
            finally
            {
                cmdStockName.Dispose();
                cmdStockRate.Dispose();
                drStock.Close();
                drStock.Dispose();
                _pBarDoubleM.Visible = false;
                if (_oConnDM.State != ConnectionState.Closed)
                    _oConnDM.Close();
            }
        }
        public int NewTrad(int iStock, double dblRate, DateTime dtTradOn, int iQuant, double dblBrok, double dblTax, string sTradeNotes)
        {
            int iResult = 0;
            int iMaxRateId;

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
                cmdTrading.CommandType = CommandType.StoredProcedure;
                cmdTrading.CommandText = "QryLatestRateID";
                cmdTrading.Parameters.Clear();

                //				cmdTrading.CommandText="SELECT Max(TRates.RateID) AS MaxOfRateID FROM TRates";
                iMaxRateId = (int)cmdTrading.ExecuteScalar();

                /* Insert to TTransactions Table */
                cmdTrading.CommandType = CommandType.StoredProcedure;
                cmdTrading.CommandText = "QryInsTrad";
                cmdTrading.Parameters.Clear();

                cmdTrading.Parameters.Add("@RateID", OleDbType.Integer).Value = iMaxRateId;
                cmdTrading.Parameters.Add("@Quantity", OleDbType.Integer).Value = iQuant;
                cmdTrading.Parameters.Add("@Brokerage", OleDbType.Currency).Value = dblBrok;
                cmdTrading.Parameters.Add("@Tax", OleDbType.Currency).Value = dblTax;
                cmdTrading.Parameters.Add("@TradeOn", OleDbType.Date).Value = dtTradOn;
                //cmdTrading.Parameters.Add("@TranDate", OleDbType.Date).Value = vTranDate;

                iResult = cmdTrading.ExecuteNonQuery();
                if (sTradeNotes.Trim().Length > 0)
                {
                    /*Getting the latest PK from TTrade Table */
                    cmdTrading.CommandType = CommandType.StoredProcedure;
                    cmdTrading.CommandText = "QryLatestTradeID";
                    cmdTrading.Parameters.Clear();

                    iMaxRateId = (int)cmdTrading.ExecuteScalar();

                    cmdTrading.CommandType = CommandType.StoredProcedure;
                    cmdTrading.CommandText = "QryInsTradNote";
                    cmdTrading.Parameters.Clear();

                    /* Insert to TTradeNotes Table */
                    cmdTrading.Parameters.Add("@TradeID", OleDbType.Integer).Value = iMaxRateId;
                    cmdTrading.Parameters.Add("@TradeNote", OleDbType.VarChar).Value = sTradeNotes;
                    iResult = cmdTrading.ExecuteNonQuery();
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


    }
}
