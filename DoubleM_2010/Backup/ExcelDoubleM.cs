using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Reflection;

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
                        excelDataAdapter.Update(ds, dt.TableName);
                    }
                    catch { }
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

}
