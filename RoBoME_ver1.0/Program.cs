using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RoBoME_ver1._0
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
            SetForm sform = new SetForm();
            Form1 f1 = new Form1();
            sform.ShowDialog();
            if (sform.DialogResult == DialogResult.Yes)
            {
                f1.onPC = true;
                Application.Run(f1);
            }
            else if (sform.DialogResult == DialogResult.No)
            {

                f1.onPC = false;
                Application.Run(f1);
            
            }
        }
    }
}
