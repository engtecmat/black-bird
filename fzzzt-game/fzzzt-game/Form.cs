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
        private GameEngine _engine;

        /// <summary>
        /// for displaying game informaiton
        /// </summary>
        private MessageLogForm _messageLogForm;

        /// <summary>
        /// build form without message log form
        /// </summary>
        public FormFzzztGame()
        {
            InitializeComponent();
            _engine = new GameEngine(this);
        }

        /// <summary>
        /// build form with message log form
        /// </summary>
        /// <param name="messageLogForm"></param>
        public FormFzzztGame(MessageLogForm messageLogForm)
        {
            InitializeComponent();
            _engine = new GameEngine(this);
            _messageLogForm = messageLogForm;
        }

        /// <summary>
        /// Set up the game when the start button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            if (_engine.IsGameStarted())
            {
                return;
            }

            panelTop.Visible = true;
            panelMiddle.Visible = true;
            panelBottom.Visible = true;

            buttonStartGame.Enabled = false;

            _engine.StartGame();

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
            foreach (Player player in _engine.GetPlayers())
            {
                // player at the top
                if (player.AtTop())
                {
                    labelPlayerTop.Text = player.GetName();
                    pictureBoxPlayerTopMechanicFace.Image = player.GetMechanicFace();
                    topCardInHandPanel.Controls.Clear();
                    topCardInHandPanel.Controls.AddRange(CreateCardsInHandForPlayer(player));
                    continue;
                }

                // player at the bottom
                labelPlayerBottom.Text = player.GetName();
                pictureBoxPlayerBottomMechanicFace.Image = player.GetMechanicFace();
                bottomCardInHandPanel.Controls.Clear();
                bottomCardInHandPanel.Controls.AddRange(CreateCardsInHandForPlayer(player));
            }
        }

        /// <summary>
        /// create cards that in player's hand
        /// </summary>
        /// <param name="player"></param>
        private PictureBox[] CreateCardsInHandForPlayer(Player player)
        {

            List<PictureBox> pictureBoxes = new List<PictureBox>();
            foreach (Card card in player.GetCardsInHand())
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Size = new Size(100, 140);
                pictureBox.Margin = new Padding(0);
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Image = card.GetFace();
                pictureBox.Tag = new CardContext(card, player);
                pictureBox.DoubleClick += new System.EventHandler(this.cardsInHand_DoubleClick);
                pictureBoxes.Add(pictureBox);
            }
            return pictureBoxes.ToArray();
        }

        /// <summary>
        /// create cards that are chosen to bid
        /// </summary>
        /// <param name="player"></param>
        private PictureBox[] CreateCardsInBidForPlayer(Player player)
        {

            List<PictureBox> pictureBoxes = new List<PictureBox>();
            foreach (Card card in player.GetCardsInBid())
            {
                PictureBox pictureBox = CreateDeafultPictureBox();
                pictureBox.Image = card.GetFace();
                pictureBox.Tag = new CardContext(card, player);
                pictureBox.DoubleClick += new System.EventHandler(this.cardsInBid_DoubleClick);
                pictureBoxes.Add(pictureBox);
            }
            return pictureBoxes.ToArray();
        }

        /// <summary>
        /// handl double click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cardsInHand_DoubleClick(object sender, EventArgs e)
        {
            if (!_engine.IsAuctionStarted())
            {
                return;
            }

            PictureBox clickedPictureBox = sender as PictureBox;

            CardContext cardContext = (CardContext)clickedPictureBox.Tag;
            cardContext.BidCard();

            DisplayCardsForPlayer(cardContext);
        }

        /// <summary>
        /// double click a card in bid panel to cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cardsInBid_DoubleClick(object sender, EventArgs e)
        {
            if (!_engine.IsAuctionStarted())
            {
                return;
            }

            PictureBox clickedPictureBox = sender as PictureBox;

            CardContext cardContext = (CardContext)clickedPictureBox.Tag;
            cardContext.CanelBidCard();
            DisplayCardsForPlayer(cardContext);
        }

        /// <summary>
        /// display cards for player
        /// </summary>
        /// <param name="cardContext"></param>
        private void DisplayCardsForPlayer(CardContext cardContext)
        {
            PictureBox[] cardsInHand = CreateCardsInHandForPlayer(cardContext.Onwer);
            PictureBox[] cardsInBid = CreateCardsInBidForPlayer(cardContext.Onwer);

            Panel cardsInHandPanel = GetCardInHandPanel(cardContext.Onwer);
            cardsInHandPanel.Controls.Clear();
            cardsInHandPanel.Controls.AddRange(cardsInHand);

            Panel cardsInBidPanel = GetCardBidPanel(cardContext.Onwer);
            cardsInBidPanel.Controls.Clear();
            cardsInBidPanel.Controls.AddRange(cardsInBid);
        }

        /// <summary>
        /// get the panel to display the cards in hand for the player
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private FlowLayoutPanel GetCardInHandPanel(Player player)
        {
            if (player.AtTop())
            {
                return topCardInHandPanel;
            }
            return bottomCardInHandPanel;
        }


        /// <summary>
        /// get the panel to display the cards to bid for the player
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private FlowLayoutPanel GetCardBidPanel(Player player)
        {
            if (player.AtTop())
            {
                return topBidPanel;
            }
            return bottomBidPanel;
        }

        /// <summary>
        /// handle the click event of a card in hand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cardsInHand_Click(object sender, EventArgs e)
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
            if (_engine.GetAllowedFacedUpCardCount() == 0)
            {
                if (!_engine.IsFirstCardOnConveyorBelt(clickedCard))
                {
                    return;
                }
                clickedPictureBox.Image = clickedCard.GetFace();
                _engine.AddFacedUpCard(clickedCard);
                _engine.UpdateFacedUpCardCount(clickedCard);
                return;
            }

            // if there are cards facing up, the first card should be faced down
            if ((_engine.IsFirstCardOnConveyorBelt(clickedCard) && _engine.GetFacedUpCards().Count > 1))
            {
                return;
            }

            // after the first card is turned up
            // if face-up cards is less than or equal to the allowed count, then turn up the card
            if (GameEngine.IsCardBack(clickedPictureBox.Image))
            {
                if (_engine.FacingUpAllowed())
                {
                    _engine.AddFacedUpCard(clickedCard);
                    clickedPictureBox.Image = clickedCard.GetFace();
                    return;
                }
                return;
            }

            clickedPictureBox.Image = GameEngine.FzzztCardBack;
            _engine.RemoveFacedUpCard(clickedCard);
        }

        /// <summary>
        /// Updates the UI to reflect the selected chief mechanic.
        /// </summary>
        private void EnpowerChiefMechanic()
        {
            Player chiefMechanic = _engine.GetChiefMechanic();
            if (chiefMechanic.AtTop())
            {
                labelChiefMechanicTop.Visible = true;
                topStartAuction.Visible = true;
                return;
            }
            if (chiefMechanic.AtBottom())
            {
                labelChiefMechanicBottom.Visible = true;
                bottomStartAuction.Visible = true;
            }
        }

        /// <summary>
        /// Reset the visibility of elements and re-enable the Start Game button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReset_Click(object sender, EventArgs e)
        {
            _engine.ResetGame();
        }

        /// <summary>
        /// Handles the click event for the "Start Auction Top" button.
        /// </summary>
        private void buttonStartAuctionTop_Click(object sender, EventArgs e)
        {
            _engine.StartAuction();
        }

        public void PrepareConveyorBelt()
        {
            // the first card is the furthest away from the conveyor belt deck
            List<Card> cards = _engine.GetAuctionCards();
            for (int i = cards.Count - 1; i >= 0; i--)
            {
                Card card = cards[i];
                PictureBox pictureBox = CreateDeafultPictureBox();
                pictureBox.Image = GameEngine.FzzztCardBack;
                pictureBox.Tag = card;
                pictureBox.Click += new System.EventHandler(this.pictureBoxOnConveyorBelt_Click);

                conveyorBeltPanel.Controls.Add(pictureBox);
            }
        }

        /// <summary>
        /// default picture box
        /// </summary>
        /// <returns></returns>
        private PictureBox CreateDeafultPictureBox()
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(100, 140);
            pictureBox.Margin = new Padding(0);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            return pictureBox;
        }

        /// <summary>
        /// Handles the click event for the "Start Auction Bottom" button.
        /// </summary>
        private void buttonStartAuctionBottom_Click(object sender, EventArgs e)
        {
            _engine.StartAuction();
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

            bottomStartAuction.Visible = false;
            topStartAuction.Visible = false;

            buttonStartGame.Enabled = true;

            topCardInHandPanel.Controls.Clear();
            conveyorBeltPanel.Controls.Clear();
            bottomCardInHandPanel.Controls.Clear();

            topBidPanel.Controls.Clear();
            bottomBidPanel.Controls.Clear();
        }

        /// <summary>
        /// update message in the textbox
        /// </summary>
        public void UpdateMessag(string message)
        {
            _messageLogForm.UpdateMessage(message);
        }

        /// <summary>
        /// disable start auction buttions
        /// </summary>
        public void HideStartAuctionButtons()
        {
            topStartAuction.Visible = false;
            bottomStartAuction.Visible = false;
        }
    }
}
