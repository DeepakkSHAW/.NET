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
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!IsPrevInstance())
                Application.Run(new frmMain());
            else
                MessageBox.Show("Already running");
            
        }
        //public static Process RunningInstance()
        //{

        //    Process current = Process.GetCurrentProcess();

        //    Process[] processes = Process.GetProcessesByName(current.ProcessName);



        //    //Loop through the running processes in with the same name 

        //    foreach (Process process in processes)
        //    {

        //        //Ignore the current process 

        //        if (process.Id != current.Id)
        //        {

        //            //Make sure that the process is running from the exe file. 

        //            if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
        //            {

        //                //Return the other process instance. 

        //                return process;

        //            }

        //        }

        //    }

        //    //No other instance was found, return null. 

        //    return null;

        //} 

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