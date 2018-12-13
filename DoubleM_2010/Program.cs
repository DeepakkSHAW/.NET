using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace DoubleM
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            //OS: Vista give control layout problems    
            //Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CommonDoubleM.LoadAppConfig();

                if (!IsPrevInstance())
                {
                    if (args.Length == 0)
                    {
                        DoubleM.CommonDoubleM.LogDM("DoubleM GUI starting..");
                        Application.Run(new frmMain());
                    }
                    else
                    {
                        //Start in bactch mode - just to update the stock price
                        DoubleM.CommonDoubleM.LogDM("Batch mode starting..");
                        DoubleM.CommonDoubleM.LoadAppConfig();
                        if (args[0].ToLower() == DoubleM.CommonDoubleM._sBatchKey.ToLower())
                        {
                            DoubleM.CommonDoubleM.LogDM("Batch DB updated starting..");
                            DALDoubleM _pdalStock;
                            _pdalStock = new DALDoubleM(null, null);
                            _pdalStock.UpdateStockprice(true);
                            CommonDoubleM.LogDM("DoubleM Batch mode closing..");
                        }
                    }

                }
                else
                {
                    MessageBox.Show("DoubleM - Already running\n"+
                        "Multiple instance not allowed in Version-" +
                        System.Reflection.Assembly.GetExecutingAssembly().GetName().Version,
                        "DoubleM - Info",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Stop);
                }
                        
        }
        private static bool IsPrevInstance()
        {
            //Find name of current process
            string currentProcessName = Process.GetCurrentProcess().ProcessName;
            //Find the name of all processes having current process name 
            Process[] processesNamesCollection = Process.GetProcessesByName(currentProcessName);
            //Check whether more than one process is running 
            if (processesNamesCollection.Length > 1)
            {
                return true;
            }
            else
                return false;
        }

    }
}