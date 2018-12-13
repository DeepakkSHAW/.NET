using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;


namespace PortfolioManager
{
   
    class ClosingStock
    {
       
        OleDbCommand cmd1;
       // OleDbDataAdapter da1;
        OleDbDataReader dr1;
        public float  CalculateClosingStock(string ScriptCode, OleDbConnection conn1,string holder)
        {
            string strCheckQty;
            strCheckQty = "select iif(isnull(sum(qty)),0,sum(qty))-(select iif(isnull(sum(qty)),0,sum(qty)) from stocksold where scriptcode='" + ScriptCode + "' and holder='"+ holder + "') as ClosingStock from stockPurchase where scriptcode='" + ScriptCode + "' and holder='" + holder + "'";
            cmd1 = new OleDbCommand();
            cmd1.CommandText = strCheckQty;
            cmd1.Connection = conn1;
             
            dr1 =  cmd1.ExecuteReader();
            while (dr1.Read())
            {
                return float.Parse(0 +dr1["closingStock"].ToString());
            }
            return 0;
        }
        public float[,] ReturnSellingNumbers(string ScriptCode, OleDbConnection conn1,float  SellQty)
        {
            string strQuery;
            float Bal=SellQty ;
            float[,] Sell;



            strQuery = "select trnid,qty,qty-(select iif(isnull(sum(qty)),0,sum(qty)) from stocksold where purchasetrnid=a.trnid) as Balance from stockpurchase a where scriptcode='" + ScriptCode + "' and qty-(select iif(isnull(sum(qty)),0,sum(qty)) from stocksold where purchasetrnid=a.trnid) >0 order by DateOfPurchase";
            cmd1 = new OleDbCommand();
            cmd1.CommandText = strQuery;
            cmd1.Connection=conn1 ;
            dr1 = cmd1.ExecuteReader();
            Sell = new float[10,2];
            int ctr=0;
            while (Bal>0)
            {
                           dr1.Read(); 

                if (float.Parse(dr1["Balance"].ToString()) >= Bal)
                {
                    Sell[ctr, 0] = int.Parse(dr1["trnid"].ToString());
                    Sell[ctr,1] =  float.Parse(Bal.ToString());
                    Bal = 0;
                }
                else
                {
                    Sell[ctr, 0] = int.Parse(dr1["trnid"].ToString());
                    Sell[ctr,1] = float.Parse(dr1["Balance"].ToString());
                    Bal = Bal - float.Parse(dr1["Balance"].ToString());
                    
                }
                ctr+=1;
                
            }
            return Sell;

        }

        public OleDbConnection GetConnection()
        {
            OleDbConnection conn1;
            conn1 = new OleDbConnection();
            string txtconn = "provider=microsoft.jet.oledb.4.0;data source=c:\\temp\\sharereport.mdb";
            conn1.ConnectionString = txtconn;
                return conn1;
            
            
        }

    }
}
