using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace fzzzt_game
{
    public partial class FormFzzztGame : Form, GameView
    {
        /// <summary>
        /// the game engine instance
        /// </summary>
        private GameEngine engine;

        public FormFzzztGame()
        {
            InitializeComponent();
            engine = new GameEngine(this);
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
        /// handle the click event of a card on the conveyor belt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxOnConveyorBelt_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;
            if (clickedPictureBox == null)
            {
                return;
            }

            // if no card is faced up and clickedCard is not the first card, do nothing
            Card clickedCard = ((Card)clickedPictureBox.Tag);
            if (engine.GetFacedUpCardCount() == 0)
            {
                if (!engine.IsFirstCardOnConveyorBelt(clickedCard))
                {
                    return;
                }
                clickedPictureBox.Image = clickedCard.GetFace();
                engine.AddFacedUpCard(clickedCard);
                engine.UpdateFacedUpCardCount(clickedCard);
                return;
            }

            // after the first card is turned up
            if (clickedPictureBox.Image == GameEngine.FzzztCardBack)
            {
                // if face-up cards is less than or equal to the allowed count, then turn up the card
                if(engine.GetFacedUpCards().Count <= engine.GetFacedUpCardCount())
                {
                    engine.AddFacedUpCard(clickedCard);
                    clickedPictureBox.Image = clickedCard.GetFace();
                    return;
                }
            }

            clickedPictureBox.Image = GameEngine.FzzztCardBack;
            engine.RemoveFacedUpCard(clickedCard);
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
            // the first card is the furthest away from the conveyor belt deck
            List<Card> cards = engine.GetAuctionCards();
            for (int i = cards.Count - 1; i >= 0; i--)
            {
                Card card = cards[i];
                PictureBox pictureBox = new PictureBox();
                pictureBox.Size = new Size(100, 140);
                pictureBox.Margin = new Padding(0);
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Image = GameEngine.FzzztCardBack;
                pictureBox.Tag = card;
                pictureBox.Click += new System.EventHandler(this.pictureBoxOnConveyorBelt_Click);

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

        /// <summary>
        /// reset UI to its initial state
        /// </summary>
        public void Reset()
        {
            panelTop.Visible = false;
            panelMiddle.Visible = false;
            panelBottom.Visible = false;

            labelChiefMechanicTop.Visible = false;
            labelChiefMechanicBottom.Visible = false;

            buttonStartAuctionBottom.Visible = false;
            buttonStartAuctionTop.Visible = false;

            buttonStartGame.Enabled = true;

            flowLayoutPanelTop.Controls.Clear();
            flowLayoutPanelMiddle.Controls.Clear();
            flowLayoutPanelBottom.Controls.Clear();
        }

        /// <summary>
        /// update message in the textbox
        /// </summary>
        public void UpdateMessag(string message)
        {
            textBoxMessage.AppendText(message);
            textBoxMessage.AppendText(Environment.NewLine);
        }
    }
}
