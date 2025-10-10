using System;
using System.Windows.Forms;

namespace BlackBird
{
    public partial class LogForm : Form
    {
        /// <summary>
        /// a form for displaying game inforamiton 
        /// </summary>
        public LogForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// add message to the message box
        /// </summary>
        /// <param name="message"></param>
        public void AddMessage(string message)
        {
            messageBox.AppendText(message);
            messageBox.AppendText(Environment.NewLine);
        }
    }
}
