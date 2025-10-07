using System;
using System.Windows.Forms;

namespace BlackBird
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LogForm messageLogForm = new LogForm();
            messageLogForm.Show();
            Application.Run(new GameForm(messageLogForm));
            //Application.Run(new FormFzzztGame());
        }
    }
}
