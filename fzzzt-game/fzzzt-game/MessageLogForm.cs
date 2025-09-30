using System;
using System.Windows.Forms;

namespace fzzzt_game
{
    public partial class MessageLogForm : Form
    {
        /// <summary>
        /// a form for displaying game inforamiton 
        /// </summary>
        public MessageLogForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// pdate message
        /// </summary>
        /// <param name="message"></param>
        public void UpdateMessage(string message)
        {
            messageBox.AppendText(message);
            messageBox.AppendText(Environment.NewLine);
        }
    }
}
