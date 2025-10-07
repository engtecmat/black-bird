using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackBird
{
    /// <summary>
    /// this form is used to display widgets
    /// </summary>
    public partial class WidgetForm : Form
    {
        public WidgetForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// handle the event when confirm building button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void confirmBuildingButton_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
