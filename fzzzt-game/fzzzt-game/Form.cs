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
            PickChiefMechanic();
        }

        /// <summary>
        /// Updates the UI to reflect the selected chief mechanic.
        /// </summary>
        private void PickChiefMechanic()
        {
            if (engine.GetChiefMechanic() == 1)
            {
                labelChiefMechanicOne.Visible = true;
                buttonStartAuctionOne.Visible = true;
                return;
            }
            if (engine.GetChiefMechanic() == 2)
            {
                labelChiefMechanicTwo.Visible = true;
                buttonStartAuctionTwo.Visible = true;
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
            buttonStartAuctionTwo.Visible = false;
            buttonStartAuctionOne.Visible = false;
            buttonStartGame.Enabled = true;
            engine.ResetGame();
        }

        /// <summary>
        /// Handles the click event for the "Start Auction One" button.
        /// </summary>
        private void buttonStartAuctionOne_Click(object sender, EventArgs e)
        {
            PlaceDeckFaceDown();
        }

        /// <summary>
        /// Sets the images of all conveyor belt card slots to display the back of the cards
        /// </summary>
        private void PlaceDeckFaceDown()
        {
            
            pictureBoxConveyorBeltDeck.Image = Properties.Resources.Fzzzt_Card_Back;

            // conveyor belt
            pictureBoxConveyorBeltCardOne.Image = Properties.Resources.Fzzzt_Card_Back;
            pictureBoxConveyorBeltCardTwo.Image = Properties.Resources.Fzzzt_Card_Back;
            pictureBoxConveyorBeltCardThree.Image = Properties.Resources.Fzzzt_Card_Back;
            pictureBoxConveyorBeltCardFour.Image = Properties.Resources.Fzzzt_Card_Back;
            pictureBoxConveyorBeltCardFive.Image = Properties.Resources.Fzzzt_Card_Back;
            pictureBoxConveyorBeltCardSix.Image = Properties.Resources.Fzzzt_Card_Back;
            pictureBoxConveyorBeltCardSeven.Image = Properties.Resources.Fzzzt_Card_Back;
            pictureBoxConveyorBeltCardEight.Image = Properties.Resources.Fzzzt_Card_Back;

            // player one
            pictureBoxPlayerOneCardOne.Image = Properties.Resources.Fzzzt_Card_Back;
            pictureBoxPlayerOneCardTwo.Image = Properties.Resources.Fzzzt_Card_Back;
            pictureBoxPlayerOneCardThree.Image = Properties.Resources.Fzzzt_Card_Back;
            pictureBoxPlayerOneCardFour.Image = Properties.Resources.Fzzzt_Card_Back;


            // player two
            pictureBoxPlayerTwoCardOne.Image = Properties.Resources.Fzzzt_Card_Back;
            pictureBoxPlayerTwoCardTwo.Image = Properties.Resources.Fzzzt_Card_Back;
            pictureBoxPlayerTwoCardThree.Image = Properties.Resources.Fzzzt_Card_Back;
            pictureBoxPlayerTwoCardFour.Image = Properties.Resources.Fzzzt_Card_Back;

        }

        /// <summary>
        /// Handles the click event for the "Start Auction One" button.
        /// </summary>
        private void buttonStartAuctionTwo_Click(object sender, EventArgs e)
        {
            PlaceDeckFaceDown();
        }
    }
}
