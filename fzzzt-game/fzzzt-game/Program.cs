using System;
using System.Windows.Forms;

namespace fzzzt_game
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

            MessageLogForm messageLogForm = new MessageLogForm();
            messageLogForm.Show();
            Application.Run(new FormFzzztGame(messageLogForm));
            //Application.Run(new FormFzzztGame());
        }
    }
}
