using System;
using System.Windows.Forms;
using System.Collections.Generic;

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
                    pictureBoxPlayerTopMechanicFace.Image = player.GetMechanicFace();
                    pictureBoxPlayerTopFirstCard.Image = Properties.Resources.Fzzzt_Card_Back;
                    pictureBoxPlayerTopSecondCard.Image = Properties.Resources.Fzzzt_Card_Back;
                    pictureBoxPlayerTopThirdCard.Image = Properties.Resources.Fzzzt_Card_Back;

                    // player two
                    pictureBoxPlayerTwoCardTwo.Image = Properties.Resources.Fzzzt_Card_Back;
                    pictureBoxPlayerTwoCardThree.Image = Properties.Resources.Fzzzt_Card_Back;
                    pictureBoxPlayerTwoCardFour.Image = Properties.Resources.Fzzzt_Card_Back;
                    continue;
                }
                labelPlayerBottom.Text = player.GetName();
                pictureBoxPlayerBottomMechanicFace.Image = player.GetMechanicFace();
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
        /// Handles the click event for the "Start Auction Top" button.
        /// </summary>
        private void buttonStartAuctionTop_Click(object sender, EventArgs e)
        {
            DealCards();
        }

        /// <summary>
        /// Sets the images of all conveyor belt card slots to display the back of the cards
        /// </summary>
        private void DealCards()
        {

            pictureBoxConveyorBeltDeck.Image = Properties.Resources.Conveyor_Belt_Deck;

            // conveyor belt
            List<Card> cards = engine.GetDeck();

            // the first card is the furthest away from the conveyor belt deck


            pictureBoxEighthCard.Image = cards[7].GetFace();
            pictureBoxSeventhCard.Image = cards[6].GetFace();
            pictureBoxSixthCard.Image = cards[5].GetFace();
            pictureBoxFifthCard.Image = cards[4].GetFace();
            pictureBoxFourthCard.Image = cards[3].GetFace();
            pictureBoxThirdCard.Image = cards[2].GetFace();
            pictureBoxSecondCard.Image = cards[1].GetFace();
            pictureBoxFristCard.Image = cards[0].GetFace();
        }

        /// <summary>
        /// Handles the click event for the "Start Auction Bottom" button.
        /// </summary>
        private void buttonStartAuctionBottom_Click(object sender, EventArgs e)
        {
            DealCards();
        }
    }
}
