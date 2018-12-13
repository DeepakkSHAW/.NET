using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Reflection;
using System.Collections;
namespace DoubleM
{
    public static class ExcelDoubleM
    {
        #region Constants
        /// <summary>
        /// string to use for setting up connection string to Excel
        /// </summary>
        private const string _excelConnectionString =
            "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"{0}\";User ID=" +
            "Admin;Password=;Extended Properties=\"Excel 8.0;HDR=YES\";";

        /// <summary>
        /// select statement to read from Excel
        /// </summary>
        private const string _excelSelect = "select * from [{0}]";

        /// <summary>
        /// tablename column for DataRow
        /// </summary>
        private const string _tableName = "TABLE_NAME";

        /// <summary>
        /// CREATE TABLE Template
        /// </summary>
        private const string _tableCreate = "CREATE TABLE [{0}] (";

        /// <summary>
        /// COLUMN Template for CREATE TABLE
        /// </summary>
        private const string _tableColumn = "[{0}] {1}{2}";
        /// <summary>
        /// Row Template for new row in TABLE
        /// </summary>
        private const string _newRow = "Insert INTO [{0}$] ({1}) Values ({2})";
        #endregion

        #region Private Methods
        /// <summary>
        /// Very simple function to specify Excel DataType mapping.
        /// </summary>
        /// <param name="dc"><see cref="DataColumn"/> to map to Excel type</param>
        /// <returns>Excel data type name</returns>
        private static string getColumnType(DataColumn dc)
        {
            string columnType = "TEXT";
            switch (dc.DataType.ToString())
            {
                case "System.Int64":
                case "System.Double":
                case "System.Int32":
                    columnType = "NUMERIC";
                    break;
                default:
                    columnType = "TEXT";
                    break;
            }
            return columnType;
        }
        private static string getColumnType(string dc)
        {
            string columnType = "TEXT";
            switch (dc.ToString())
            {
                case "System.Int64":
                case "System.Double":
                case "System.Int32":
                    columnType = "NUMERIC";
                    break;
                default:
                    columnType = "TEXT";
                    break;
            }
            return columnType;
        }
        #endregion

        #region Public Methods

        #region WriteToExcel(DataSet ds)
        /// <summary>
        /// Write data from a dataset to a new filename.
        /// </summary>
        /// <param name="ds"><see cref="DataSet"/> to read from</param>
        public static void WriteToExcel(DataSet ds)
        {
            WriteToExcel(ds, ds.DataSetName + ".xls", false);
        }
        #endregion

        #region WriteToExcel(DataSet ds, String fileName, bool append)
        /// <summary>
        /// Write data from a dataset to a filename.  
        /// This method can either create a new file or append to
        /// an existing Excel file. If append is specified and file does
        /// not exist, the file will be created.
        /// </summary>
        /// <param name="ds"><see cref="DataSet"/> to read from</param>
        /// <param name="fileName">File name to write to</param>
        /// <param name="append"><see cref="bool"/> true to append; false to create new file</param>
        public static void WriteToExcel(DataSet ds, String fileName, bool append)
        {
            string excelConnectionString = string.Format(_excelConnectionString, fileName);
            OleDbConnection excelFile = null;
            OleDbCommand excelCmd = null;
            OleDbDataAdapter excelDataAdapter = null;
            OleDbCommandBuilder excelCommandBuilder = null;
            StringBuilder sb = null;
            try
            {
                GC.Collect();
                if (File.Exists(fileName) && !append) File.Delete(fileName);
                excelFile = new OleDbConnection(excelConnectionString);
                excelFile.Open();
                // write each DataTable to Excel Spreadsheet
                foreach (DataTable dt in ds.Tables)
                {
                    // file does not exist or we don't want to append
                    if (!File.Exists(fileName) || !append)
                    {
                        // build the CREATE TABLE statement
                        sb = new StringBuilder();
                        string table = dt.TableName;
                        table = table.Contains("-") ? "'" + table + "'" : table;
                        sb.AppendFormat(_tableCreate, table);
                        foreach (DataColumn dc in ds.Tables[dt.TableName].Columns)
                        {
                            sb.AppendFormat(_tableColumn, dc.ColumnName,
                                getColumnType(dc)
                                , (dc.Ordinal == dt.Columns.Count - 1 ?
                                ")" : ","));
                        }
                        excelCmd = new OleDbCommand(sb.ToString(), excelFile);
                        excelCmd.ExecuteNonQuery();
                    }
                    // use the command builder to generate insert command for DataSet Update to work
                    excelDataAdapter = new OleDbDataAdapter(string.Format(_excelSelect, dt.TableName), excelFile);
                    excelCommandBuilder = new OleDbCommandBuilder(excelDataAdapter);
                    excelCommandBuilder.QuotePrefix = "[";
                    excelCommandBuilder.QuoteSuffix = "]";
                    try
                    {
                        excelDataAdapter.InsertCommand = excelCommandBuilder.GetInsertCommand();
                        excelDataAdapter.UpdateBatchSize = 10;
                        int k = excelDataAdapter.Update(ds.Tables[0]);
                        
                        Console.WriteLine(k);
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }
            }
            finally
            {
                sb = null;
                if (excelDataAdapter != null) excelDataAdapter.Dispose();
                excelDataAdapter = null;
                excelCommandBuilder = null;
                if (excelCmd != null) excelCmd.Dispose();
                excelCmd = null;
                if (excelFile != null)
                {
                    excelFile.Close();
                    excelFile.Dispose();
                }
                excelFile = null;
            }
        }

        public static void ExportToExcel(System.Windows.Forms.DataGridView dgv,string fileName)
        {
            string excelConnectionString = string.Format(_excelConnectionString, fileName );
            OleDbConnection excelFile = null;
            OleDbCommand excelCmd = null;
            StringBuilder sb = null;

            //DataTable dt = dgv.DataSource;
            excelFile = new OleDbConnection( excelConnectionString);
            excelCmd = new OleDbCommand();
            excelCmd.Connection = excelFile;
            Console.WriteLine(excelConnectionString);

            try
            {
                GC.Collect();
                excelFile.Open();
                ////////////////
                // build the CREATE TABLE statement
                sb = new StringBuilder();
                string table = "Deepak";
                table = table.Contains("-") ? "'" + table + "'" : table;
                sb.AppendFormat(_tableCreate, table);

                foreach (System.Windows.Forms.DataGridViewColumn dc in dgv.Columns)
                {
                    if(dc.Visible)
                    sb.AppendFormat(_tableColumn, dc.Name,
                        getColumnType(dc.ValueType.ToString())
                        , (dc.Index == dgv.Columns.Count - 1 ?
                        ")" : ","));
                }
                sb = sb.Replace(",", ")", sb.Length - 1, 1);
                ////////////////
                excelCmd.CommandText = sb.ToString();// "Create Table MySheet (F1 char(255), F2 char(255))";
                excelCmd.ExecuteNonQuery();
                DataView dv = (DataView)dgv.DataSource;
                DataTable dt = dv.Table;

                excelCmd.CommandText = "Insert INTO [Deepak$] (F1, F2) Values ('1','A')";
                excelCmd.CommandText ="Insert INTO [Deepak$] (StockCatID,StockName,ShortName,YCode,RCode,BSECode,HDFCCode,Active) Values " +
                                        "('1','2','3','4','5','6','7','8')";
                sb.Append( string.Empty);

                foreach (DataRow dr in dt.Rows)
                {
                    foreach (DataColumn dc in dt.Columns)
                    {
                        
                        Console.WriteLine(dr[dc]);
                    }
                }


                try
                {
                    excelCmd.ExecuteNonQuery();
                }

                catch (Exception ex)
                { Console.WriteLine(ex.Message); }
                excelFile.Close();

            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
            
            finally
            {excelFile.Close();}
        }

        public static void WriteToExcel(DataTable dt, String fileName, bool append)
        {
            string excelConnectionString = string.Format(_excelConnectionString, fileName);
            OleDbConnection excelFile = null;
            OleDbCommand excelCmd = null;
            OleDbDataAdapter excelDataAdapter = null;
            OleDbCommandBuilder excelCommandBuilder = null;
            StringBuilder sb = null;
            try
            {
                GC.Collect();
                if (File.Exists(fileName) && !append) File.Delete(fileName);
                excelFile = new OleDbConnection(excelConnectionString);
                excelFile.Open();

                    // file does not exist or we don't want to append
                    if (!File.Exists(fileName) || !append)
                    {
                        // build the CREATE TABLE statement
                        sb = new StringBuilder();
                        string table = dt.TableName;
                        table = table.Contains("-") ? "'" + table + "'" : table;
                        sb.AppendFormat(_tableCreate, table);
                        foreach (DataColumn dc in dt.Columns)
                        {
                            sb.AppendFormat(_tableColumn, dc.ColumnName,
                                getColumnType(dc)
                                , (dc.Ordinal == dt.Columns.Count - 1 ?
                                ")" : ","));
                        }
                        excelCmd = new OleDbCommand(sb.ToString(), excelFile);
                        excelCmd.ExecuteNonQuery();
                    }
                    // use the command builder to generate insert command for DataSet Update to work
                    excelDataAdapter = new OleDbDataAdapter(string.Format(_excelSelect, dt.TableName), excelFile);
                    excelCommandBuilder = new OleDbCommandBuilder(excelDataAdapter);
                    excelCommandBuilder.QuotePrefix = "[";
                    excelCommandBuilder.QuoteSuffix = "]";
                    try
                    {
                        excelDataAdapter.InsertCommand = excelCommandBuilder.GetInsertCommand();
                        int k = excelDataAdapter.Update(dt);

                        Console.WriteLine(k);
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                
            }
            finally
            {
                sb = null;
                if (excelDataAdapter != null) excelDataAdapter.Dispose();
                excelDataAdapter = null;
                excelCommandBuilder = null;
                if (excelCmd != null) excelCmd.Dispose();
                excelCmd = null;
                if (excelFile != null)
                {
                    excelFile.Close();
                    excelFile.Dispose();
                }
                excelFile = null;
            }
        }

        #endregion

        #region ReadFromExcel(string fileName)
        /// <summary>
        /// Read from an Excel file into a new DataSet
        /// </summary>
        /// <param name="fileName">file name to read from</param>
        /// <returns><see cref="DataSet"/> filled with data</returns>
        public static DataSet ReadFromExcel(string fileName)
        {
            return ReadFromExcel(fileName, new DataSet());
        }
        #endregion

        #region ReadFromExcel(string fileName, DataSet ds)
        /// <summary>
        /// Read from an Excel file into an existing DataSet
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="ds"></param>
        /// <returns><see cref="DataSet"/> filled with data</returns>
        public static DataSet ReadFromExcel(string fileName, DataSet ds)
        {
            string excelConnectionString = string.Format(_excelConnectionString, fileName);
            OleDbConnection excelFile = null;
            DataTable schemaTable;
            OleDbDataAdapter excelDataAdapter = null;
            try
            {
                excelFile = new OleDbConnection(excelConnectionString);
                excelFile.Open();
                schemaTable = excelFile.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
                new object[] { null, null, null, "TABLE" });
                // Read each DataTable (i.e. Excel Spreadsheet) into the DataSet
                foreach (DataRow dr in schemaTable.Rows)
                {
                    excelDataAdapter = new OleDbDataAdapter(dr[_tableName].ToString(), excelFile);
                    excelDataAdapter.SelectCommand.CommandType = CommandType.TableDirect;
                    excelDataAdapter.AcceptChangesDuringFill = false;
                    string table = dr[_tableName].ToString().Replace("$", string.Empty).Replace("'", string.Empty);
                    if (dr[_tableName].ToString().Contains("$"))
                        excelDataAdapter.Fill(ds, table);
                }
                excelFile.Close();
            }
            finally
            {
                if (excelDataAdapter != null) excelDataAdapter.Dispose();
                excelDataAdapter = null;
                schemaTable = null;
                if (excelFile != null)
                {
                    excelFile.Close();
                    excelFile.Dispose();
                }
                excelFile = null;
            }
            return ds;
        }
        #endregion

        #endregion
    }

    //http://dhruval-dotnet.blogspot.com/2009/04/export-data-to-excel-sheet.html
    internal static class ExportExcel
    {


        public static void Export(string sheetToCreate, DataTable dtToExport, string tableName)
        {
            // List rows = new List();
             List<DataRow> rows = new List<DataRow>();
            foreach (DataRow row in dtToExport.Rows) rows.Add(row);
            subExportToExcel(sheetToCreate, rows, dtToExport, tableName);
        }
        private static void subExportToExcel(string sheetToCreate, List<DataRow> selectedRows, DataTable origDataTable, string tableName)
        {
            char Space = ' ';
            string dest = sheetToCreate;

            while (File.Exists(dest))
            {
                /*dest = Path.GetDirectoryName(sheetToCreate) + "\\" + Path.GetFileName(sheetToCreate) + i + Path.GetExtension(sheetToCreate);
                i += 1;*/
                File.Delete(dest);
            }
            sheetToCreate = dest;
            if (tableName == null) tableName = string.Empty;
            tableName = tableName.Trim().Replace(Space, '_');
            if (tableName.Length == 0) tableName = origDataTable.TableName.Replace(Space, '_');
            if (tableName.Length == 0) tableName = "NoTableName";
            if (tableName.Length > 30) tableName = tableName.Substring(0, 30);
            //Excel names are less than 31 chars
            string queryCreateExcelTable = "CREATE TABLE [" + tableName + "] (";
            //Dictionary colNames = new Dictionary();
            var colNames = new Dictionary<string, string>();
            foreach (DataColumn dc in origDataTable.Columns)
            {
                //Cause the query to name each of the columns to be created.
                string modifiedcolName = dc.ColumnName.Replace(Space, '_').Replace('.', '#');
                string origColName = dc.ColumnName;
                colNames.Add(modifiedcolName, origColName);
                queryCreateExcelTable += " [" + modifiedcolName + "] " + CommonDoubleM.getColumnType(dc.DataType) + ",";
            }
            queryCreateExcelTable = queryCreateExcelTable.TrimEnd(new char[] { Convert.ToChar(",") }) + ")";
            //adds the closing parentheses to the query string
            if (selectedRows.Count > 65000 && sheetToCreate.ToLower().EndsWith(".xls"))
            {
                //use Excel 2007 for large sheets.
                sheetToCreate = sheetToCreate.ToLower().Replace(".xls", string.Empty) + ".xlsx";
            }
            string strCn = string.Empty;
            string ext = System.IO.Path.GetExtension(sheetToCreate).ToLower();
            if (ext == ".xls") strCn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sheetToCreate + "; Extended Properties='Excel 8.0;HDR=YES'";
            if (ext == ".xlsx") strCn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + sheetToCreate + ";Extended Properties='Excel 12.0 Xml;HDR=YES' ";
            if (ext == ".xlsb") strCn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + sheetToCreate + ";Extended Properties='Excel 12.0;HDR=YES' ";
            if (ext == ".xlsm") strCn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + sheetToCreate + ";Extended Properties='Excel 12.0 Macro;HDR=YES' ";
            System.Data.OleDb.OleDbConnection cn = new System.Data.OleDb.OleDbConnection(strCn);
            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(queryCreateExcelTable, cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [" + tableName + "]", cn);
            System.Data.OleDb.OleDbCommandBuilder cb = new System.Data.OleDb.OleDbCommandBuilder(da);
            //creates the INSERT INTO command
            cb.QuotePrefix = "[";
            cb.QuoteSuffix = "]";
            cmd = cb.GetInsertCommand();
            //gets a hold of the INSERT INTO command.
            foreach (DataRow row in selectedRows)
            {
                foreach (System.Data.OleDb.OleDbParameter param in cmd.Parameters)
                    param.Value = row[colNames[param.SourceColumn]];
                cmd.ExecuteNonQuery(); //INSERT INTO command.
            }
            cn.Close();
            cn.Dispose();
            da.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        public static DataTable ReadCSV(string file)
        {
            string path = System.IO.Path.GetDirectoryName(file).ToString();
            string filename = System.IO.Path.GetFileName(file).ToString();
            string strConn = @"Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=" + path + @"\;Extensions=asc,csv,tab,txt";
            try
            {
                System.Data.Odbc.OdbcConnection conn = new System.Data.Odbc.OdbcConnection(strConn);
                System.Data.Odbc.OdbcDataAdapter da = new System.Data.Odbc.OdbcDataAdapter("Select * from [" + filename + "]", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Console.WriteLine(dt.Rows.Count.ToString());
                return dt;
            }
            catch (Exception ex)
            {
                CommonDoubleM.LogDM("Import CSV failed: " + ex.Message);
                return null;
            }

        }


    } 
}
