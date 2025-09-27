using System;
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
            if (engine.IsGameStarted())
            {
                return;
            }
            panelPlayerOne.Visible = true;
            panelConveyorBelt.Visible = true;
            panelPlayTwo.Visible = true;
            buttonStartGame.Enabled = false;
            engine.StartGame();
            if (engine.GetChiefMechanic() == 1)
            {
                labelChiefMechanicOne.Visible = true;
            }
            if (engine.GetChiefMechanic() == 2)
            {
                labelChiefMechanicTwo.Visible = true;
            }
        }

        /// <summary>
        /// Reset the visibility of elements and re-enable the Start Game button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReset_Click(object sender, EventArgs e)
        {
            panelPlayerOne.Visible = false;
            panelConveyorBelt.Visible = false;
            panelPlayTwo.Visible = false;
            labelChiefMechanicOne.Visible = false;
            labelChiefMechanicTwo.Visible = false;
            buttonStartGame.Enabled = true;
            engine.ResetGame();
        }
    }
}
