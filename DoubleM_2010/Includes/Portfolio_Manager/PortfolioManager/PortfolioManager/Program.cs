using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.OleDb;
namespace PortfolioManager
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
            Application.Run(new mainForm   ());
        }

        

    }
}