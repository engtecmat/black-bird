using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

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

            pictureBoxConveyorBeltDeck.Image = Properties.Resources.Conveyor_Belt_Deck;

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
                // player at the top
                if (player.AtTop())
                {
                    labelPlayerTop.Text = player.GetName();
                    pictureBoxPlayerTopMechanicFace.Image = player.GetMechanicFace();
                    DisplayCardsForPlayer(player, flowLayoutPanelTop);

                    continue;
                }

                // player at the bottom
                labelPlayerBottom.Text = player.GetName();
                pictureBoxPlayerBottomMechanicFace.Image = player.GetMechanicFace();
                DisplayCardsForPlayer(player, flowLayoutPanelBottom);
            }
        }

        /// <summary>
        /// display the player's cards
        /// </summary>
        /// <param name="player"></param>
        /// <param name="parent"></param>
        private void DisplayCardsForPlayer(Player player, Control parent)
        {
            foreach (Card card in player.GetCardsInHand())
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Size = new Size(100, 140);
                pictureBox.Margin = new Padding(0);
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Image = card.GetFace();
                pictureBox.Tag = GameEngine.FzzztCardBack;
                pictureBox.Click += new System.EventHandler(this.pictureBox_Click);

                parent.Controls.Add(pictureBox);
            }
        }

        /// <summary>
        /// handle the click event of a card in hand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;
            if (clickedPictureBox == null)
            {
                return;
            }

            if (clickedPictureBox.Image == GameEngine.FzzztCardBack)
            {
                clickedPictureBox.Image = (Image)clickedPictureBox.Tag;
                clickedPictureBox.Tag = GameEngine.FzzztCardBack;
                return;
            }

            Image faceImage = clickedPictureBox.Image;
            clickedPictureBox.Image = GameEngine.FzzztCardBack;
            clickedPictureBox.Tag = faceImage;
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
        /// Deal 8 cards to start an auction
        /// </summary>
        private void DealCards()
        {
            // conveyor belt
            List<Card> cards = engine.GetDeck();

            // the first card is the furthest away from the conveyor belt deck
            foreach (Card card in cards.GetRange(0, 8))
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Size = new Size(100, 140);
                pictureBox.Margin = new Padding(0);
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Image = GameEngine.FzzztCardBack;
                pictureBox.Tag = card.GetFace();
                pictureBox.Click += new System.EventHandler(this.pictureBox_Click);

                flowLayoutPanelMiddle.Controls.Add(pictureBox);
            }
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
