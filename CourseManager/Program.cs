using System;
using System.Text;
using System.Windows.Forms;

namespace CourseManager
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            Time time = new Time("UTR", new TimeOnly(9, 30), new TimeOnly(10, 0));
            Time time2 = new Time("UTR", new TimeOnly(9, 0), new TimeOnly(10, 0));
            bool conflict = time.conflictsWith(time2);
            MessageBox.Show($"Conflict: {conflict}");



            //ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());
        }

        
    }
}