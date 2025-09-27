using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fzzzt_game
{
    public partial class FormFzzztGame : Form
    {
        /// <summary>
        /// the game engine instance
        /// </summary>
        private FzzztGameEngine engine = new FzzztGameEngine();

        public FormFzzztGame()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the game when the start button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            panelPlayerOne.Visible = true;
            panelConveyorBelt.Visible = true;
            panelPlayTwo.Visible = true;

            if(engine.GetChiefMechanic() == 1)
            {
                labelChiefMechanicOne.Visible = true;
            }
            if(engine.GetChiefMechanic() == 2)
            {
                labelChiefMechanicTwo.Visible = true;
            }
        }
    }
}
