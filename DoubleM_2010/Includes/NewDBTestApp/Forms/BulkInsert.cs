using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace NewDBTestApp
{
    public partial class BulkInsert : Form
    {
        bool blnOnce = false;
        DataTable dt = new DataTable();
        DataView dv;
        const string ConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\Stock.mdb";
        string SELECT = "SELECT * from TStockCat";
        string INSERT = "INSERT INTO TStockCat(Category) VALUES (@Meter1)";

        OleDbConnection oConn = new OleDbConnection(ConnString);
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataAdapter ad = new OleDbDataAdapter();

        public BulkInsert()
        {
            InitializeComponent();
        }

        private void BulkInsert_Load(object sender, EventArgs e)
        {
            if (!blnOnce)
            {
                //dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("myCategory", typeof(string));
                //dt.Columns.Add("SName", typeof(string));
                //dt.Columns.Add("City", typeof(string));
                //dt.Columns.Add("DOB", typeof(DateTime));
                //dt.Columns.Add("PL", typeof(double));
                blnOnce = true;
            }
            dt.Rows.Add(new string[] { "Deepak" });
            dt.Rows.Add(new string[] { "Saurav" });
            dt.Rows.Add(new string[] { "Supam" });
            dt.Rows.Add(new string[] { "Jitendra" });
            dt.Rows.Add(new string[] { "Ive" });

            try
            {
                cmd.Connection = oConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SELECT;
                ad = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (oConn.State != ConnectionState.Closed)
                    oConn.Close();
            }


        }

        public static void UpDataDB(string DB, DataTable DT)
        {
            //OleDbDataAdapter OleAdp = new OleDbDataAdapter(SQL, OleConn);
            //OleAdp.InsertCommand = new OleDbCommand(INSERT);
            //OleAdp.InsertCommand.Parameters.Add("@Meter1", OleDbType.Integer, 8, "Meter1");
            //OleAdp.InsertCommand.Parameters.Add("@Meter2", OleDbType.Integer, 8, "Meter2");
            //OleAdp.InsertCommand.Parameters.Add("@Meter3", OleDbType.Integer, 8, "Meter3");
            //OleAdp.InsertCommand.Parameters.Add("@Meter4", OleDbType.Integer, 8, "Meter4");
            //OleAdp.InsertCommand.Connection = OleConn;
            //OleAdp.InsertCommand.Connection.Open();
            //OleAdp.Update(DT);
            //OleAdp.InsertCommand.Connection.Close();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ad = new OleDbDataAdapter(SELECT, oConn);
            ad.InsertCommand = new OleDbCommand(INSERT);
            ad.InsertCommand.Parameters.Add("@Meter1", OleDbType.VarChar, 30, "myCategory");
            ad.InsertCommand.Connection = oConn;
            try
            {
                ad.InsertCommand.Connection.Open();
                ad.Update(dt);
                ad.InsertCommand.Connection.Close();
                DataSet ds = new DataSet();
                ad.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (oConn.State != ConnectionState.Closed)
                    oConn.Close();
                dataGridView1.Refresh();
            }

        }

    }
}
