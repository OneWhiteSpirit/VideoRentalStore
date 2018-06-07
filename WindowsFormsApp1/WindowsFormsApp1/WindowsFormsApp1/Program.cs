using System;
using System.Windows.Forms;
using VideoRentalStore.GUI_Classes;

namespace VideoRentalStore
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Startpage());
        }
    }
}
