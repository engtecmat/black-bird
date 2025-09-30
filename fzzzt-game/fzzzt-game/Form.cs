using System;
using System.Windows.Forms;

namespace fzzzt_game
{
    public partial class FormFzzztGame : Form
    {
        /// <summary>
        /// the game engine instance
        /// </summary>
        private GameEngine engine = new GameEngine();

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

            panelTop.Visible = true;
            panelMiddle.Visible = true;
            panelBottom.Visible = true;

            buttonStartGame.Enabled = false;

            engine.StartGame();
            EnpowerChiefMechanic();

            DisplayPlayers();
        }

        /// <summary>
        /// display the players, e.g, name, card, .etc.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void DisplayPlayers()
        {
            foreach (Player player in engine.GetPlayers())
            {
                if (player.AtTop())
                {
                    labelPlayerTop.Text = player.GetName();
                    continue;
                }
                labelPlayerBottom.Text = player.GetName();
            }
        }

        /// <summary>
        /// Updates the UI to reflect the selected chief mechanic.
        /// </summary>
        private void EnpowerChiefMechanic()
        {
            Player chiefMechanic = engine.GetChiefMechanic();
            if (chiefMechanic.AtTop())
            {
                labelChiefMechanicTop.Visible = true;
                buttonStartAuctionTop.Visible = true;
                return;
            }
            if (chiefMechanic.AtBottom())
            {
                labelChiefMechanicBottom.Visible = true;
                buttonStartAuctionBottom.Visible = true;
            }
        }

        /// <summary>
        /// Reset the visibility of elements and re-enable the Start Game button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReset_Click(object sender, EventArgs e)
        {
            panelTop.Visible = false;
            panelMiddle.Visible = false;
            panelBottom.Visible = false;
            labelChiefMechanicTop.Visible = false;
            labelChiefMechanicBottom.Visible = false;
            buttonStartAuctionBottom.Visible = false;
            buttonStartAuctionTop.Visible = false;
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

            pictureBoxConveyorBeltDeck.Image = Properties.Resources.Conveyor_Belt_Deck;

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
